

namespace Shop.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Identity;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly UserManager<User> userManager;
        private Random random;

        public SeedDb(DataContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            //creo un usuario
            //se inicia buscando si existe ya un usuario con ese correo identificador
            var user = await this.userManager.FindByEmailAsync("davidcoin87@gmail.com");
            if (user == null) //si no existe entonces proceda a crear el usuario
            {
                user = new User
                {
                    FirstName = "David",
                    LastName = "Ruiz",
                    Email = "davidcoin87@gmail.com",
                    UserName = "davidcoin87@gmail.com",
                    PhoneNumber = "3162980992"
                };

                var result = await this.userManager.CreateAsync(user, "123456");//a ese usuario que creo, asignele este password
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            if (!this.context.Products.Any())
            {
                this.AddProduct("iPhone X", user);
                this.AddProduct("Magic Mouse", user);
                this.AddProduct("iWatch Series 4", user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(1000),
                IsAvailabe = true,
                Stock = this.random.Next(100),
                User = user
            });
        }
    }

}
