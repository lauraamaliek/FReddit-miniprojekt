var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//GET
app.MapGet("/api/posts", () =>
{
    
});

app.MapGet("/api/posts/{id}",() =>
{

});

//put
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


//POST

app.MapPost("/api/posts",() =>
{

});

app.MapPost("/api/posts/{id}/comments",() =>
{

});

app.Run();