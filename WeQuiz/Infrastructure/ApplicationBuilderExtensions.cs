
namespace WeQuiz.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Identity;
    using WeQuiz.Data;
    using WeQuiz.Data.Models;

    using static WeQuiz.WebConstants;
    using static WeQuiz.Areas.Admin.AdminConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedDistricts(services);
            SeedPopulatedAreas(services);
            SeedSchool(services);
            SeedCategories(services);
            SeedSubcategories(services);
            SeedQuestionTypes(services);
            SeedTestTypes(services);
            SeedAdministrator(services);
            SeedSchoolAdmin(services);
            SeedTeacher(services);
            SeedStudent(services);
            SeedQuestions(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<WeQuizDbContext>();

            data.Database.Migrate();
        }

        //Seeding towns data, needed for schools
        private static void SeedDistricts(IServiceProvider services)
        {
            var data = services.GetRequiredService<WeQuizDbContext>();

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

        private static void SeedPopulatedAreas(IServiceProvider services)
        {
            var data = services.GetRequiredService<WeQuizDbContext>();

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

        private static void SeedSchool(IServiceProvider services)
        {
            var data = services.GetRequiredService<WeQuizDbContext>();

            if (data.Schools.Any())
            {
                return;
            }

            var populatedAreaId = data.PopulatedAreas.First(p => p.Name == "Видин").Id;

            data.Schools.Add(new School
            {
                PopulatedAreaId = populatedAreaId,
                Name = "ППМГ Екзарх Антим I",
                SchoolCode = 500102
            });

            data.SaveChanges();
        }

        //Seeding base categories - school subjects, classes etc.
        private static void SeedCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<WeQuizDbContext>();

            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category {Name = "Български език и литература", SchoolId=0 },
                new Category {Name = "Английски език", SchoolId=0 },
                new Category {Name = "Математика", SchoolId=0 },
                new Category {Name = "Информатика", SchoolId=0 },
                new Category {Name = "Информационни технологии", SchoolId=0 },
                new Category {Name = "История и цивилизация", SchoolId=0 },
                new Category {Name = "География и икономика", SchoolId=0 },
                new Category {Name = "Философия", SchoolId=0 },
                new Category {Name = "Биология и здравно образование", SchoolId=0 },
                new Category {Name = "Физика и астрономия", SchoolId=0 },
                new Category {Name = "Химия и опазване на околната среда", SchoolId=0 },
                new Category {Name = "Общи", SchoolId = 0},
            });

            data.SaveChanges();
        }

        private static void SeedSubcategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<WeQuizDbContext>();

            if (data.Subcategories.Any())
            {
                return;
            }

            var categories = data.Categories.ToList();

            var subCategories = new List<Subcategory>();

            foreach (var category in categories)
            {
                subCategories.Add(new Subcategory
                {
                    Name = "Общи",
                    SchoolId = 0,
                    CategoryId = category.Id
                });

                if (category.Name != "Общи")
                {
                    for (int i = 2; i <= 12; i++)
                    {
                        subCategories.Add(new Subcategory
                        {
                            Name = i + " клас",
                            SchoolId = 0,
                            CategoryId = category.Id
                        });
                    }
                }

                if (category.Name == "Информатика" ||
                    category.Name == "Информационни технологии" ||
                    category.Name != "Общи")
                {
                    subCategories.Add(new Subcategory
                    {
                        Name = "ППМГ",
                        SchoolId = 1,
                        CategoryId = category.Id
                    });
                }
            }

            data.Subcategories.AddRange(subCategories);
            data.SaveChanges();
        }

        //Seeding question types
        private static void SeedQuestionTypes(IServiceProvider services)
        {
            var data = services.GetRequiredService<WeQuizDbContext>();

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

        private static void SeedTestTypes(IServiceProvider services)
        {
            var data = services.GetRequiredService<WeQuizDbContext>();

            if (data.TestTypes.Any())
            {
                return;
            }

            data.TestTypes.AddRange(new[]
            {
                new TestType { Type = "Смесен тип"},
                new TestType { Type = "Избираем отговор"},
                new TestType { Type = "Вярно/невярно"},
                new TestType { Type = "Точен отговор"},
            });

            data.SaveChanges();
        }


        //Seeding random questions data for testing purposes
        private static void SeedQuestions(IServiceProvider services)
        {
            var data = services.GetRequiredService<WeQuizDbContext>();

            if (data.Questions.Any())
            {
                return;
            }

            var exactAnswers = new List<ExactAnswer>();
            var trueFalseAnswers = new List<TrueFalseAnswer>();
            var choiceQuestions = new List<Question>();

            var subcategoryId = data.Subcategories
                .Where(s => s.Name == "2 клас" && s.Category.Name == "Математика")
                .First().Id;

            var subcategoriesCount = data.Subcategories.Count();

            Random r = new Random();


            //Generating exact answer questions

            for (int i = 0; i < 20; i++)
            {
                int a = r.Next(0, 51);
                int b = r.Next(0, 51);

                var answer = new ExactAnswer
                {
                    IntAnswer = a + b,
                    Points = 1,
                    Question = new Question
                    {
                        SubcategoryId = subcategoryId,
                        Class = 2,
                        SchoolId = 0,
                        Text = string.Format("Какъв е сбора на {0} и {1}", a, b),
                        QuestionTypeId = 3
                    }
                };

                exactAnswers.Add(answer);
            }

            for (int i = 0; i < 20; i++)
            {
                int a = r.Next(1, 11);
                int b = r.Next(1, 11);

                var answer = new ExactAnswer
                {
                    IntAnswer = a * b,
                    Points = 1,
                    Question = new Question
                    {
                        SubcategoryId = subcategoryId,
                        Class = 2,
                        SchoolId = 0,
                        Text = string.Format("Произведението на {0} и {1} е", a, b),
                        QuestionTypeId = 3
                    }
                };

                exactAnswers.Add(answer);
            }

            for (int i = 0; i < 20; i++)
            {
                int a = r.Next(0, 101);

                var answer = new ExactAnswer
                {
                    IntAnswer = a,
                    Points = 1,
                    Question = new Question
                    {
                        SubcategoryId = r.Next(1, subcategoriesCount + 1),
                        Class = r.Next(2, 13),
                        SchoolId = 0,
                        Text = string.Format("Произволен въпрос с точен отговор {0}", a),
                        QuestionTypeId = 3
                    }
                };

                exactAnswers.Add(answer);
            }


            //Generating true/false questions

            for (int i = 0; i < 10; i++)
            {
                int a = r.Next(0, 51);
                int b = r.Next(0, 51);

                var answer = new TrueFalseAnswer
                {
                    Answer = true,
                    Points = 1,
                    Question = new Question
                    {
                        SubcategoryId = subcategoryId,
                        Class = 3,
                        SchoolId = 0,
                        Text = string.Format("Сбора на {0} и {1} е {2}", a, b, a + b),
                        QuestionTypeId = 3
                    }
                };

                trueFalseAnswers.Add(answer);
            }

            for (int i = 0; i < 10; i++)
            {
                int a = r.Next(0, 51);
                int b = r.Next(0, 51);
                int c = r.Next(0, 51);

                var answer = new TrueFalseAnswer
                {
                    Answer = false,
                    Points = 1,
                    Question = new Question
                    {
                        SubcategoryId = subcategoryId,
                        Class = 3,
                        SchoolId = 0,
                        Text = string.Format("Сбора на {0} и {1} е {2}", a, b, a + b - c),
                        QuestionTypeId = 3
                    }
                };

                trueFalseAnswers.Add(answer);
            }

            for (int i = 0; i < 10; i++)
            {
                int a = r.Next(0, 11);
                int b = r.Next(0, 11);

                var answer = new TrueFalseAnswer
                {
                    Answer = true,
                    Points = 1,
                    Question = new Question
                    {
                        SubcategoryId = subcategoryId,
                        Class = 3,
                        SchoolId = 0,
                        Text = string
                            .Format("Произведението на {0} и {1} е {2}", a, b, a * b),
                        QuestionTypeId = 3
                    }
                };

                trueFalseAnswers.Add(answer);
            }

            for (int i = 0; i < 10; i++)
            {
                int a = r.Next(0, 11);
                int b = r.Next(0, 11);
                int c = r.Next(0, 11);

                var answer = new TrueFalseAnswer
                {
                    Answer = false,
                    Points = 1,
                    Question = new Question
                    {
                        SubcategoryId = subcategoryId,
                        Class = 3,
                        SchoolId = 0,
                        Text = string
                            .Format("Произведението на {0} и {1} е {2}", a, b, a * b + c),
                        QuestionTypeId = 3
                    }
                };

                trueFalseAnswers.Add(answer);
            }

            for (int i = 0; i < 10; i++)
            {                
                var answer = new TrueFalseAnswer
                {
                    Answer = true,
                    Points = 1,
                    Question = new Question
                    {
                        SubcategoryId = subcategoryId,
                        Class = r.Next(2, 13),
                        SchoolId = 0,
                        Text = string.Format("Произволен въпрос от тип да/не {0}", true),
                        QuestionTypeId = 3
                    }
                };

                trueFalseAnswers.Add(answer);
            }

            for (int i = 0; i < 10; i++)
            {
                var answer = new TrueFalseAnswer
                {
                    Answer = false,
                    Points = 1,
                    Question = new Question
                    {
                        SubcategoryId = subcategoryId,
                        Class = r.Next(2, 13),
                        SchoolId = 0,
                        Text = string.Format("Произволен въпрос от тип да/не {0}", false),
                        QuestionTypeId = 3
                    }
                };

                trueFalseAnswers.Add(answer);
            }

            //Generating choice answers questions

            for (int i = 0; i < 20; i++)
            {
                int a = r.Next(0, 51);
                int b = r.Next(0, 51);

                var question = new Question
                {
                    SubcategoryId = subcategoryId,
                    Class = 3,
                    SchoolId = 0,
                    Text = string.Format("Сбора на {0} и {1} е равен на", a, b),
                    QuestionTypeId = 1,
                    ChoiceAnswers = new List<ChoiceAnswer>
                    {
                        new ChoiceAnswer { TextAnswer = a.ToString(), Points = 0},
                        new ChoiceAnswer { TextAnswer = b.ToString(), Points = 0},
                        new ChoiceAnswer { TextAnswer = (a+b).ToString(), Points = 1},
                        new ChoiceAnswer { TextAnswer = (a+b-10).ToString(), Points = 0}
                    }
                };                        

                choiceQuestions.Add(question);
            }

            for (int i = 0; i < 20; i++)
            {
                int a = r.Next(0, 11);
                int b = r.Next(0, 11);
                int c = r.Next(0, 11);

                var question = new Question
                {
                    SubcategoryId = subcategoryId,
                    Class = 3,
                    SchoolId = 0,
                    Text = string.Format("Произведението на {0} и {1} е равно на", a, b),
                    QuestionTypeId = 1,
                    ChoiceAnswers = new List<ChoiceAnswer>
                    {
                        new ChoiceAnswer { TextAnswer = (a*b+c).ToString(), Points = 0},
                        new ChoiceAnswer { TextAnswer = (a*b).ToString(), Points = 1},
                        new ChoiceAnswer { TextAnswer = (a+b).ToString(), Points = 0},
                        new ChoiceAnswer { TextAnswer = (a*b-c).ToString(), Points = 0}
                    }
                };

                choiceQuestions.Add(question);
            }

            for (int i = 0; i < 20; i++)
            { 
                var question = new Question
                {
                    SubcategoryId = subcategoryId,
                    Class = r.Next(2, 13),
                    SchoolId = 0,
                    Text = string
                        .Format("Произволен въпрос с избираеми отговори {0}", i+1),
                    QuestionTypeId = 1,
                    ChoiceAnswers = new List<ChoiceAnswer>
                    {
                        new ChoiceAnswer { TextAnswer = "Грешен отговор 1", Points = 0},
                        new ChoiceAnswer { TextAnswer = "Верен отговор", Points = 1},
                        new ChoiceAnswer { TextAnswer = "Грешен отговор 2", Points = 0},
                        new ChoiceAnswer { TextAnswer = "Грешен отговор 3", Points = 0}
                    }
                };

                choiceQuestions.Add(question);
            }

            data.ExactAnswers.AddRange(exactAnswers);
            data.TrueFalseAnswers.AddRange(trueFalseAnswers);
            data.Questions.AddRange(choiceQuestions);

            data.SaveChanges();
        }


        //Seeding users with roles
        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var adminRole = new IdentityRole { Name = AdministratorRoleName };
                    await roleManager.CreateAsync(adminRole);

                    const string adminEmail = "admin@wequiz.bg";
                    const string adminPassword = "admin123";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FullName = "Администратор",
                        Alias = "Admin",
                        SchoolId = 0
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, adminRole.Name);
                })
                .GetAwaiter()
                .GetResult();
        }

        private static void SeedSchoolAdmin(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            var data = services.GetRequiredService<WeQuizDbContext>();

            var schoolId = data.Schools.First(s => s.SchoolCode == 500102).Id;

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(SchoolAdminRoleName))
                {
                    return;
                }

                var schoolAdminRole = new IdentityRole { Name = SchoolAdminRoleName };
                await roleManager.CreateAsync(schoolAdminRole);

                const string schoolAdminEmail = "sadmin@wequiz.bg";
                const string schoolAdminPassword = "sadmin123";

                var user = new User
                {
                    Email = schoolAdminEmail,
                    UserName = schoolAdminEmail,
                    FullName = "Училищен администратор",
                    Alias = "SchoolAdmin",
                    SchoolId = schoolId
                };

                await userManager.CreateAsync(user, schoolAdminPassword);

                await userManager.AddToRoleAsync(user, schoolAdminRole.Name);
            })
                .GetAwaiter()
                .GetResult();
        }

        private static void SeedTeacher(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            var data = services.GetRequiredService<WeQuizDbContext>();

            var schoolId = data.Schools.First(s => s.SchoolCode == 500102).Id;

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(TeacherRoleName))
                {
                    return;
                }

                var teacherRole = new IdentityRole { Name = TeacherRoleName };
                await roleManager.CreateAsync(teacherRole);

                const string teacherEmail = "teacher@wequiz.bg";
                const string teacherPassword = "teacher123";

                var user = new User
                {
                    Email = teacherEmail,
                    UserName = teacherEmail,
                    FullName = "Учител",
                    Alias = "Teacher",
                    SchoolId = schoolId
                };

                await userManager.CreateAsync(user, teacherPassword);

                await userManager.AddToRoleAsync(user, teacherRole.Name);

                var userId = data.Users.First(u => u.Email == teacherEmail).Id;

                var categoryId = data.Categories.First(c => c.Name == "Общи").Id;

                var newTeacher = new Teacher
                {
                    UserId = userId,
                    SchoolId = schoolId,
                    IsApproved = true
                };

                data.Teachers.Add(newTeacher);
                data.SaveChanges();

                var teacherId = data.Teachers.First(t => t.UserId == userId).Id;

                var teacherCategory = new TeacherCategory
                {
                    TeacherId = teacherId,
                    CategoryId = categoryId,
                    IsApproved = true
                };

                data.TeachersCategories.Add(teacherCategory);
                data.SaveChanges();

            })
                .GetAwaiter()
                .GetResult();
        }

        private static void SeedStudent(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            var data = services.GetRequiredService<WeQuizDbContext>();

            var schoolId = data.Schools.First(s => s.SchoolCode == 500102).Id;

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(StudentRoleName))
                {
                    return;
                }

                var studentRole = new IdentityRole { Name = StudentRoleName };
                await roleManager.CreateAsync(studentRole);

                const string studentEmail = "student@wequiz.bg";
                const string studentPassword = "student123";

                var user = new User
                {
                    Email = studentEmail,
                    UserName = studentEmail,
                    FullName = "Ученик",
                    Alias = "Student",
                    SchoolId = schoolId
                };

                await userManager.CreateAsync(user, studentPassword);

                await userManager.AddToRoleAsync(user, studentRole.Name);

                var userId = data.Users.First(u => u.Email == studentEmail).Id;

                var newStudent = new Student
                {
                    UserId = userId,
                    Class = 10,
                    SchoolId = schoolId,
                    IsApproved = true
                };

                data.Students.Add(newStudent);
                data.SaveChanges();

            })
                .GetAwaiter()
                .GetResult();
        }
    }
}
