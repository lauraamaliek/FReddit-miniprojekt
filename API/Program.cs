using API.Service;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Model;

var builder = WebApplication.CreateBuilder(args);

// Sætter CORS så API'en kan bruges fra andre domæner
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSomeStuff, builder => {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Tilføj DbContext factory som service.
builder.Services.AddDbContext<PostContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));

// Tilføj DataService så den kan bruges i endpoints
builder.Services.AddScoped<DataService>();

// Dette kode kan bruges til at fjerne "cykler" i JSON objekterne.

builder.Services.Configure<JsonOptions>(options =>
{
    // Her kan man fjerne fejl der opstår, når man returnerer JSON med objekter,
    // der refererer til hinanden i en cykel.
    // (altså dobbelrettede associeringer)
    options.SerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});


var app = builder.Build();

// Seed data hvis nødvendigt.
using (var scope = app.Services.CreateScope())
{
    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    dataService.SeedData(); // Fylder data på, hvis databasen er tom. Ellers ikke.
}

app.UseHttpsRedirection();
app.UseCors(AllowSomeStuff);

// Middlware der kører før hver request. Sætter ContentType for alle responses til "JSON".
app.Use(async (context, next) =>
{
    context.Response.ContentType = "application/json; charset=utf-8";
    await next(context);
});


//GET
app.MapGet("/api/posts", (DataService service) =>
{
    return service.GetPosts().Select(p => new
    {
        id = p.Id,
        title = p.Title,
        content = p.Content,
        upvotes = p.Upvotes,
        downvotes = p.Downvotes,
        user = new
        {
            Id = p.User.UserId,
            username=p.User.Username
        }
    });
});

app.MapGet("/api/posts/{id}",(DataService service, int id) =>
{
    return service.GetPostById(id);
});

/*
//PUT 
app.MapPut("/api/posts/{id}/upvote",() =>
{

});

app.MapPut("/api/posts/{id}/downvote",() =>
{

});

app.MapPut("/api/posts/{postid}/comments/{commentid}/upvote",() =>
{

});

app.MapPut("/api/posts/{postid}/comments/{commentid}/downvote",() =>
{

});

*/
//POST

app.MapPost("/api/posts",(DataService service, NewPostData data) =>
{
    string result = service.CreatePosts(data.Title, data.Content, data.UserId);
    return new { message = result };
});
/*
app.MapPost("/api/posts/{id}/comments",() =>
{

});
*/
app.Run();
record NewPostData(string Title, string Content, int UserId);