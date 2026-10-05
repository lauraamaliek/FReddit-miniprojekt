using System.Data.Common;
using Model;
using shared.Model;

namespace API.Service;
using Microsoft.EntityFrameworkCore;
using shared.Model;


public class DataService
{
    private PostContext db { get; }

    public DataService(PostContext db) {
        this.db = db;
    }
    /// <summary>
    /// Seeder noget nyt data i databasen hvis det er nødvendigt.
    /// </summary>
    public void SeedData()
    {
        
        User user = db.Users.FirstOrDefault()!;
        // derfor list skal sættes!
        // author.Books.Add(new Book { Title = "??", Author = author });
        if (user == null) {
            user = new User { Username = "Anon" };
            db.Users.Add(user);
        }

        Post post = db.Posts.FirstOrDefault()!;
        if (post == null)
        {
            db.Posts.Add(new Post(
                user,
                "Min kat har et bedre job end mig",
                "Den sover 16 timer om dagen og får stadig mad. Jeg tror, jeg har valgt den forkerte karriere.",
                42,
                2
            ));

            db.Posts.Add(new Post(
                user,
                "Jeg satte kaffen på køl",
                "Jeg var så træt i morges, at jeg kom mælk i kaffen og satte koppen i køleskabet. Opdagede det først efter 10 minutter.",
                87,
                1
            ));

            db.Posts.Add(new Post(
                user,
                "Hvorfor er printere altid sure?",
                "Jeg trykkede på print én gang. Den blinkede rødt, lavede en mærkelig lyd og nægtede at samarbejde. Det føles personligt.",
                31,
                4
            ));

            db.Posts.Add(new Post(
                user,
                "Jeg har glemt min adgangskode",
                "Jeg prøvede at nulstille min adgangskode, men hjemmesiden krævede min gamle adgangskode for at nulstille den.",
                156,
                3
            ));

            db.Posts.Add(new Post(
                user,
                "Min plante har fået nok",
                "Jeg glemte at vande min plante i tre uger. I går gav den op og væltede. Jeg respekterer dens beslutning.",
                73,
                8
            ));
        }

        db.SaveChanges();


    }
}