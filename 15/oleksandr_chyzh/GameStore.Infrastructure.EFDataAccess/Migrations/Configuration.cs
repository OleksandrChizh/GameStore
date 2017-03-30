namespace GameStore.Infrastructure.EFDataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        /*
        protected override void Seed(ApplicationContext context)
        {
            var roles = new[]
            {
                new Role { Name = "Administrator" },
                new Role { Name = "Manager" },
                new Role { Name = "Moderator" },
                new Role { Name = "User" },
                new Role { Name = "Guest" }
            };
            context.Roles.AddOrUpdate(r => r.Name, roles);
            
            const int saltLength = 10;
            
            string firstSalt = SaltGenerator.GetSalt(saltLength);
            var firstPasswordHash = Encoding.ASCII.GetString(new SHA256Managed().ComputeHash(Encoding.ASCII.GetBytes(firstSalt + "administrator")));
            
            string secondSalt = SaltGenerator.GetSalt(saltLength);
            var secondPasswordHash = Encoding.ASCII.GetString(new SHA256Managed().ComputeHash(Encoding.ASCII.GetBytes(secondSalt + "manager")));
            
            string thirdSalt = SaltGenerator.GetSalt(saltLength);
            var thirdPasswordHash = Encoding.ASCII.GetString(new SHA256Managed().ComputeHash(Encoding.ASCII.GetBytes(thirdSalt + "moderator")));
            
            string fourthSalt = SaltGenerator.GetSalt(saltLength);
            var fourthPasswordHash = Encoding.ASCII.GetString(new SHA256Managed().ComputeHash(Encoding.ASCII.GetBytes(fourthSalt + "userpass")));
            
            var users = new[]
            {
                new User("admin@mail.com", firstPasswordHash, firstSalt, new List<Role> { roles[0] }),
                new User("manager@mail.com", secondPasswordHash, secondSalt, new List<Role> { roles[1] }) { NotificationType = "Email" },
                new User("moderator@mail.com", thirdPasswordHash, thirdSalt, new List<Role> { roles[2] }),
                new User("user@mail.com", fourthPasswordHash, fourthSalt, new List<Role> { roles[3] })
            };
            context.Users.AddOrUpdate(r => r.PasswordHash, users);
            
            var genres = new[]
            {
                new Genre("Other", null),
                new Genre("Strategy", null),
                new Genre("RPG", null),
                new Genre("Sports", null),
                new Genre("Races", null),
                new Genre("Action", null),
                new Genre("Adventure", null),
                new Genre("Puzzle&Skill", null),
                new Genre("Misc", null)
            };
            context.Genres.AddOrUpdate(g => g.Name, genres);
            
            var subGenres = new[]
            {
                new Genre("RTS", genres[0]),
                new Genre("TBS. Races", genres[0]),
                new Genre("Rally", genres[3]),
                new Genre("Arcade", genres[3]),
                new Genre("Formula", genres[3]),
                new Genre("Off-road", genres[3]),
                new Genre("FPS", genres[4]),
                new Genre("TPS", genres[4]),
                new Genre("SubAdventure", genres[5])
            };
            context.Genres.AddOrUpdate(g => g.Name, subGenres);
            
            var platformTypes = new[]
            {
                new PlatformType("Mobile"),
                new PlatformType("Browser"),
                new PlatformType("Desktop"),
                new PlatformType("Console"),
            };
            context.PlatformTypes.AddOrUpdate(pt => pt.Type, platformTypes);
            
            var publishers = new[]
            {
                new Publisher("EAGames", "Разрабатывает гонки", "www.eagames.com"),
                new Publisher("Blizzard", "Разрабатывает стратегии", "www.blizzard.com")
            };
            context.Publishers.AddOrUpdate(pt => pt.CompanyName, publishers);
            
            var publishersTranslations = new[]
            {
                new PublisherTranslation { Description = "Develops races", Publisher = publishers[0], Language = "en" },
                new PublisherTranslation { Description = "Develops strategies", Publisher = publishers[1], Language = "en" },
                new PublisherTranslation { Description = "Разрабатывает гонки", Publisher = publishers[0], Language = "ru" },
                new PublisherTranslation { Description = "Разрабатывает стратегии", Publisher = publishers[1], Language = "ru" }
            };
            context.PublisherTranslations.AddOrUpdate(pt => pt.Description, publishersTranslations);
            
            var games = new[]
            {
                new Game
                {
                    Description = "Игра об войне людей и орков",
                    Genres = new List<Genre>() { genres[0], subGenres[0] },
                    Key = "Warcraft3FT",
                    Name = "Военное ремесло 3. Ледяной трон",
                    PlatformTypes = new List<PlatformType> { platformTypes[2] },
                    Publishers = new List<Publisher> { publishers[1] },
                    Price = 25.4m,
                    Discounted = false,
                    UnitsInStock = 5,
                    PublishingDate = new DateTime(2005, 5, 5)
                },
                new Game
                {
                    Description = "Игра с гонками",
                    Genres = new List<Genre> { genres[3], subGenres[4] },
                    Key = "NFSCarbon",
                    Name = "Жажда скорости. Углерод",
                    PlatformTypes = new List<PlatformType> { platformTypes[0], platformTypes[2] },
                    Publishers = new List<Publisher> { publishers[0] },
                    Price = 14.3m,
                    Discounted = false,
                    UnitsInStock = 3,
                    PublishingDate = new DateTime(2004, 3, 2)
                }
            };
            context.Games.AddOrUpdate(g => g.Key, games);
            
            var gameTranslations = new[]
            {
                new GameTranslation
                {
                    Name = "Warcraft 3. Frozen Throne",
                    Description = "Game about war of people and orks",
                    Game = games[0],
                    Language = "en"
                },
                new GameTranslation
                {
                    Name = "Need for speed. Carbon",
                    Description = "Game with races",
                    Game = games[1],
                    Language = "en"
                },
                new GameTranslation
                {
                    Name = "Военное ремесло 3. Ледяной трон",
                    Description = "Игра об войне людей и орков",
                    Game = games[0],
                    Language = "ru"
                },
                new GameTranslation
                {
                    Name = "Жажда скорости. Углерод",
                    Description = "Игра с гонками",
                    Game = games[1],
                    Language = "ru"
                }
            };
            context.GameTranslations.AddOrUpdate(gt => gt.Description, gameTranslations);
            
            var firstLevelOfComments = new[]
            {
                new Comment("Alex", "Good game", null, games[0], false),
                new Comment("Jack", "Game is very well", null, games[0], false),
                new Comment("Tom", "Bad game", null, games[0], false)
            };
            context.Comments.AddOrUpdate(c => c.Body, firstLevelOfComments);
            
            var secondLevelOfComments = new[]
            {
                new Comment("Anna", "You are right", firstLevelOfComments[0], games[0], false),
                new Comment("Kate", "Why?", firstLevelOfComments[2], games[0], false)
            };
            context.Comments.AddOrUpdate(c => c.Body, secondLevelOfComments);
            
            var thirdLevelOfComments = new[]
            {
                new Comment("Tom", "Because it is boring", secondLevelOfComments[1], games[0], false)
            };
            context.Comments.AddOrUpdate(c => c.Body, thirdLevelOfComments);
            
            context.SaveChanges();
        }
        */
    }
}
