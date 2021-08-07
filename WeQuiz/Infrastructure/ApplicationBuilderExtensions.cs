namespace WeQuiz.Infrastructure
{
    using System.Linq;
    using WeQuiz.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using WeQuiz.Data.Models;
    using System.Collections.Generic;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<WeQuizDbContext>();

            data.Database.Migrate();

            SeedDistricts(data);
            SeedPopulatedAreas(data);

            return app;
        }

        private static void SeedDistricts(WeQuizDbContext data)
        {
            if (data.Districts.Any())
            {
                return;
            }

            data.Districts.AddRange(new[]
            {
                new District {Name = "Благоевград" },
                new District {Name = "Бургас" },
                new District {Name = "Варна" },
                new District {Name = "Велико Търново" },
                new District {Name = "Видин" },
                new District {Name = "Враца" },
                new District {Name = "Габрово" },
                new District {Name = "Добрич" },
                new District {Name = "Кърджали" },
                new District {Name = "Кюстендил" },
                new District {Name = "Ловеч" },
                new District {Name = "Монтана" },
                new District {Name = "Пазарджик" },
                new District {Name = "Перник" },
                new District {Name = "Плевен" },
                new District {Name = "Пловдив" },
                new District {Name = "Разград" },
                new District {Name = "Русе" },
                new District {Name = "Силистра" },
                new District {Name = "Сливен" },
                new District {Name = "Смолян" },
                new District {Name = "София" },
                new District {Name = "Стара Загора" },
                new District {Name = "Търговище" },
                new District {Name = "Хасково" },
                new District {Name = "Шумен" },
                new District {Name = "Ямбол" },
            });

            data.SaveChanges();
        }

        private static void SeedPopulatedAreas(WeQuizDbContext data)
        {
            if (data.PopulatedAreas.Any())
            {
                return;
            }

            var districts = data.Districts.ToList();

            var populatedAreas = new List<PopulatedArea>();

            foreach (var district in districts)
            {
                populatedAreas.Add(new PopulatedArea
                {
                    DistrictId = district.Id,
                    Name = district.Name
                });
            }

            data.PopulatedAreas.AddRange(populatedAreas);
            data.SaveChanges();
        }
    }
}
