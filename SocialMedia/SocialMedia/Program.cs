using API.Domain.Entites;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Presistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddDbContext<SocialMediaContext>
            (options => options.UseSqlServer(builder.Configuration.GetConnectionString("constr")));

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                // Password settings (optional)
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<SocialMediaContext>()
              .AddDefaultTokenProviders();

            builder.Services.AddScoped<IUserService, UserService>();





            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
