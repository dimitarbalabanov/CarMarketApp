namespace CarMarket.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CarMarket.Data.Models;

    internal class MakesAndModelsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Makes.Any())
            {
                return;
            }

            var makes = new Make[]
            {
                new Make
                {
                    Name = "Audi",
                    Models =
                    {
                        new Model { Name = "A1" },
                        new Model { Name = "A2" },
                        new Model { Name = "A3" },
                        new Model { Name = "A4" },
                        new Model { Name = "A5" },
                        new Model { Name = "A6" },
                        new Model { Name = "A7" },
                        new Model { Name = "A8" },
                        new Model { Name = "Q2" },
                        new Model { Name = "Q3" },
                        new Model { Name = "Q4" },
                        new Model { Name = "Q5" },
                        new Model { Name = "Q7" },
                        new Model { Name = "Tt" },
                    },
                },
                new Make
                {
                    Name = "BMW",
                    Models =
                    {
                        new Model { Name = "1 Series" },
                        new Model { Name = "2 Series" },
                        new Model { Name = "3 Series" },
                        new Model { Name = "5 Series" },
                        new Model { Name = "7 Series" },
                        new Model { Name = "M3" },
                        new Model { Name = "M4" },
                        new Model { Name = "M5" },
                        new Model { Name = "X3" },
                        new Model { Name = "X5" },
                        new Model { Name = "X6" },
                        new Model { Name = "Z3" },
                        new Model { Name = "Z4" },
                    },
                },
                new Make
                {
                    Name = "Citroen",
                    Models =
                    {
                        new Model { Name = "C1" },
                        new Model { Name = "C2" },
                        new Model { Name = "C3" },
                        new Model { Name = "C4" },
                        new Model { Name = "C5" },
                        new Model { Name = "C6" },
                        new Model { Name = "Saxo" },
                        new Model { Name = "Xantia" },
                        new Model { Name = "C-Crosser" },
                        new Model { Name = "C-Elysee" },
                    },
                },
                new Make
                {
                    Name = "Dacia",
                    Models =
                    {
                        new Model { Name = "Logan" },
                        new Model { Name = "Duster" },
                        new Model { Name = "Dokker" },
                        new Model { Name = "Sandero" },
                    },
                },
                new Make
                {
                    Name = "Ford",
                    Models =
                    {
                        new Model { Name = "Ka" },
                        new Model { Name = "Fiesta" },
                        new Model { Name = "Focus" },
                        new Model { Name = "Mondeo" },
                        new Model { Name = "Mustang" },
                        new Model { Name = "F-150" },
                    },
                },
                new Make
                {
                    Name = "Honda",
                    Models =
                    {
                        new Model { Name = "Accord " },
                        new Model { Name = "Civic " },
                        new Model { Name = "CR-V" },
                        new Model { Name = "Prelude" },
                        new Model { Name = "Elysion" },
                        new Model { Name = "Insight" },
                    },
                },
                new Make
                {
                    Name = "Hyundai",
                    Models =
                    {
                        new Model { Name = "Accent" },
                        new Model { Name = "Elantra" },
                        new Model { Name = "Sonata" },
                        new Model { Name = "i10" },
                        new Model { Name = "i20" },
                        new Model { Name = "i30" },
                        new Model { Name = "Veloster" },
                    },
                },
                new Make
                {
                    Name = "Mercedes",
                    Models =
                    {
                        new Model { Name = "G-Class" },
                        new Model { Name = "S-Class" },
                        new Model { Name = "C-Class" },
                        new Model { Name = "E-Class" },
                        new Model { Name = "CLK-Class" },
                        new Model { Name = "A-Class" },
                        new Model { Name = "CL-Class" },
                        new Model { Name = "GLK-Class" },
                        new Model { Name = "GLC-Class" },
                    },
                },
                new Make
                {
                    Name = "Nissan",
                    Models =
                    {
                        new Model { Name = "Patrol" },
                        new Model { Name = "Skyline" },
                        new Model { Name = "Maxima" },
                        new Model { Name = "Micra" },
                        new Model { Name = "Sentra" },
                        new Model { Name = "Altima" },
                        new Model { Name = "X-Trail" },
                        new Model { Name = "Leaf" },
                    },
                },
                new Make
                {
                    Name = "Opel",
                    Models =
                    {
                        new Model { Name = "Adam" },
                        new Model { Name = "Corsa" },
                        new Model { Name = "Astra" },
                        new Model { Name = "Ampera" },
                        new Model { Name = "Vectra" },
                        new Model { Name = "Insignia" },
                        new Model { Name = "Meriva" },
                        new Model { Name = "Vivaro" },
                    },
                },
                new Make
                {
                    Name = "Peugeot",
                    Models =
                    {
                        new Model { Name = "206" },
                        new Model { Name = "208" },
                        new Model { Name = "308" },
                        new Model { Name = "408" },
                        new Model { Name = "2008" },
                        new Model { Name = "3008" },
                        new Model { Name = "5008" },
                        new Model { Name = "Boxer" },
                        new Model { Name = "Partner" },
                    },
                },
                new Make
                {
                    Name = "Renault",
                    Models =
                    {
                        new Model { Name = "Megane" },
                        new Model { Name = "Kadjar" },
                        new Model { Name = "Captur" },
                        new Model { Name = "Zoe" },
                        new Model { Name = "Clio" },
                    },
                },
                new Make
                {
                    Name = "Skoda",
                    Models =
                    {
                        new Model { Name = "Octavia" },
                        new Model { Name = "Fabia" },
                        new Model { Name = "Superb" },
                        new Model { Name = "Kamiq" },
                        new Model { Name = "Yeti" },
                    },
                },
                new Make
                {
                    Name = "Subaru",
                    Models =
                    {
                        new Model { Name = "Impreza" },
                        new Model { Name = "Crosstrek" },
                        new Model { Name = "Forester" },
                        new Model { Name = "Outback" },
                        new Model { Name = "BRZ" },
                        new Model { Name = "Legacy" },
                    },
                },
                new Make
                {
                    Name = "Toyota",
                    Models =
                    {
                        new Model { Name = "Camry" },
                        new Model { Name = "Supra" },
                        new Model { Name = "C-HR" },
                        new Model { Name = "Land Cruiser" },
                        new Model { Name = "Hilux" },
                        new Model { Name = "Corolla" },
                        new Model { Name = "Prius" },
                        new Model { Name = "Sienta" },
                        new Model { Name = "Yaris" },
                    },
                },
                new Make
                {
                    Name = "Volvo",
                    Models =
                    {
                        new Model { Name = "S40" },
                        new Model { Name = "S60" },
                        new Model { Name = "S80" },
                        new Model { Name = "XC40" },
                        new Model { Name = "XC60" },
                        new Model { Name = "XC70" },
                        new Model { Name = "V40" },
                        new Model { Name = "V60" },
                        new Model { Name = "V80" },
                        new Model { Name = "C30" },
                    },
                },
                new Make
                {
                    Name = "Volkswagen",
                    Models =
                    {
                        new Model { Name = "Amarok" },
                        new Model { Name = "Arteon" },
                        new Model { Name = "Caddy" },
                        new Model { Name = "Golf" },
                        new Model { Name = "Polo" },
                        new Model { Name = "Jetta" },
                        new Model { Name = "Passat" },
                        new Model { Name = "Touareg" },
                        new Model { Name = "Transporter" },
                        new Model { Name = "Tiguan" },
                    },
                },
            };

            await dbContext.Makes.AddRangeAsync(makes);
        }
    }
}
