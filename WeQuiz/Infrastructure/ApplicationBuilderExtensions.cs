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
            SeedCategories(data);
            SeedSubcategories(data);
            SeedQuestionTypes(data);

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

        private static void SeedCategories(WeQuizDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category {Name = "Български език и литература", SchoolCode=0 },
                new Category {Name = "Английски език", SchoolCode=0 },
                new Category {Name = "Математика", SchoolCode=0 },
                new Category {Name = "Информатика", SchoolCode=0 },
                new Category {Name = "Информационни технологии", SchoolCode=0 },
                new Category {Name = "История и цивилизация", SchoolCode=0 },
                new Category {Name = "География и икономика", SchoolCode=0 },
                new Category {Name = "Философия", SchoolCode=0 },
                new Category {Name = "Биология и здравно образование", SchoolCode=0 },
                new Category {Name = "Физика и астрономия", SchoolCode=0 },
                new Category {Name = "Химия и опазване на околната среда", SchoolCode=0 },
                new Category {Name = "Общи", SchoolCode=0 },
            });

            data.SaveChanges();
        }

        private static void SeedSubcategories(WeQuizDbContext data)
        {
            if (data.Subcategories.Any())
            {
                return;
            }

            var categories = data.Categories.ToList();

            var subCategories = new List<Subcategory>();

            foreach (var category in categories)
            {
                if (category.Name != "Общи")
                {
                    for (int i = 5; i <= 12; i++)
                    {
                        subCategories.Add(new Subcategory
                        {
                            Name = i + " клас",
                            SchoolCode = 0,
                            CategoryId = category.Id
                        });
                    }
                }
            }

            data.Subcategories.AddRange(subCategories);
            data.SaveChanges();
        }

        private static void SeedQuestionTypes(WeQuizDbContext data)
        {
            if (data.QuestionTypes.Any())
            {
                return;
            }

            data.QuestionTypes.AddRange(new[]
            {
                new QuestionType { Type = "Избираем отговор"},
                new QuestionType { Type = "Вярно/невярно"},
                new QuestionType { Type = "Точен отговор"},
            });

            data.SaveChanges();
        }
    }
}
