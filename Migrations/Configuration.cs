namespace Document_API.Migrations
{
    using Document_API.Models;
    using Document_API.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Document_API.DAL.DocumentContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Document_API.DAL.DocumentContext context)
        {
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User{Username = "lamtrinh1", FirstName = "Trinh1", LastName = "Lam1", Password = "12345678", Role = EnumRole.User},
                    new User{Username = "lamtrinh2", FirstName = "Trinh2", LastName = "Lam2", Password = "12345678", Role = EnumRole.Contributor},
                    new User{Username = "lamtrinh3", FirstName = "Trinh3", LastName = "Lam3", Password = "12345678", Role = EnumRole.Admin},
                };
                users.ForEach(s => context.Users.Add(s));
            }

            if (!context.Categorys.Any())
            {
                var categorys = new List<Category>
                {
                    new Category{Title = "Blog"},
                    new Category{Title = "News"}
                };
                categorys.ForEach(s => context.Categorys.Add(s));
            }

            context.SaveChanges();
        }
    }
}
