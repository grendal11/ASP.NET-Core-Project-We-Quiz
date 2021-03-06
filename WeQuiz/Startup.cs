namespace WeQuiz
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using WeQuiz.Data;
    using WeQuiz.Data.Models;
    using WeQuiz.Infrastructure;
    using WeQuiz.Services.Categories;
    using WeQuiz.Services.Questions;
    using WeQuiz.Services.Schools;
    using WeQuiz.Services.Statistics;
    using WeQuiz.Services.Users;

    public class Startup
    {
        public Startup(IConfiguration configuration) 
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<WeQuizDbContext>(options => options
                    .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<User>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<WeQuizDbContext>();

            services.AddControllersWithViews(options => 
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });           

            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<ISchoolsService, SchoolsService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IQuestionsService, QuestionsService>();
        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();               
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
                       
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultAreaRoute();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
            
        }
    }
}
