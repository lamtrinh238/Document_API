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
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User{Username = "lamtrinh", FirstName = "Trinh1", LastName = "Lam1", Password = "1", Role = EnumRole.User},
                    new User{Username = "lamtrinh", FirstName = "Trinh2", LastName = "Lam2", Password = "1", Role = EnumRole.Admin},
                    new User{Username = "lamtrinh", FirstName = "Trinh2", LastName = "Lam2", Password = "1", Role = EnumRole.Contributor},
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
