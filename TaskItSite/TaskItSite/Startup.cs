using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskItSite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using TaskItSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace TaskItSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TaskItContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<TaskItContext>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                        .AddCookie(o => { o.LoginPath = new PathString("/Account/Login");
                            /*o.Events.OnRedirectToLogin = (context) => {
                                context.Response.StatusCode = 401;
                                return System.Threading.Tasks.Task.CompletedTask;
                            }; */
                        })
                        .AddGoogle(o =>
                        {
                            o.ClientId = "102863069774-6p2dtscloroulicdul32sg374qnm3hiq.apps.googleusercontent.com";
                            o.ClientSecret = "MY6Ta5Gdm331MVtLLGY-T-fQ";
                        });

            /*
            services.AddAuthentication("CookieAuthenticationScheme")
                .AddCookie(options => {
                options.Events.OnRedirectToLogin = (context) =>
                {
                    context.Response.StatusCode = 401;
                    return System.Threading.Tasks.Task.CompletedTask;
                };
            });
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
