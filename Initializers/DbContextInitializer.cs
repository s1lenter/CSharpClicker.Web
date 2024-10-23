using CSharpClicker.Web.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.Initializers
{
    public class DbContextInitializer
    {
        public static void AddAppDbContext(IServiceCollection services)
        {
            var pathToDbFile = GetPathToDbFile();
            services
                .AddDbContext<AppDbContext>(options => options
                    .UseSqlite($"Data Source={pathToDbFile}"));

            string GetPathToDbFile()
            {
                var applicationFolder = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), "CSharpClicker");

                if (!Directory.Exists(applicationFolder))
                {
                    Directory.CreateDirectory(applicationFolder);
                }

                return Path.Combine(applicationFolder, "CSharpClicker.db");
            }
        }

        public static void InitializeDbContext(AppDbContext appDbContext)
        {
            const string Boost1 = "Boost1";
            const string Boost2 = "Boost2";
            const string Boost3 = "Boost3";
            const string Boost4 = "Boost4";
            const string Boost5 = "Boost5";

            appDbContext.Database.Migrate();

            var existingBoosts = appDbContext.Boosts.ToArray();

            AddBoostIfNotExist(Boost1, price: 100, profit: 1);
            AddBoostIfNotExist(Boost1, price: 500, profit: 15);
            AddBoostIfNotExist(Boost1, price: 2000, profit: 60, isAuto: true);
            AddBoostIfNotExist(Boost1, price: 10000, profit: 400);
            AddBoostIfNotExist(Boost1, price: 100000, profit: 5000, isAuto: true);

            appDbContext.SaveChanges();

            void AddBoostIfNotExist(string name, long price, long profit, bool isAuto = false)
            {
                if (!existingBoosts.Any(eb => eb.Title == name))
                {
                    var pathToImage = Path.Combine(".", "Resoureses", "BoostImages", $"{name}.png");
                    using var fileStream = File.OpenRead(pathToImage);
                    using var memoryStream = new MemoryStream();

                    appDbContext.Boosts.Add(new Domain.Boost
                    {
                        Title = name,
                        Price = price,
                        Profit = profit,
                        IsAuto = isAuto,
                        Image = memoryStream.ToArray(),
                    });
                }
            }
        }
    }
}
