using Microsoft.EntityFrameworkCore;
using WebApplicationCoreLogin.Models;

namespace WebApplicationCoreLogin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /*MVC projesi olmas� i�in servis.add k�sm�n� ekl�yoruz eklenmi� tikledi�imiz i�in addrazor k�sm�n� daha sonradan �nd�rd�g�m�z� eklemek �c�n ekled�k.*/           
            
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            builder.Services.AddDbContext<DatabaseContext>(o => { o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}