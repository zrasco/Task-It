using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskItSite.Data;
using TaskItSite.Models;
using TaskItSite.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.DataProtection;
using System.IO;

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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Set default login page
            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Login");

            services.AddDataProtection()
                            .SetApplicationName("TaskItSite")
                            .PersistKeysToFileSystem(new DirectoryInfo(@".\keys"));

            // Set up image cache
            services.AddSingleton<IImageCache>(new ImageCache());

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = "102863069774-6p2dtscloroulicdul32sg374qnm3hiq.apps.googleusercontent.com";
                googleOptions.ClientSecret = "MY6Ta5Gdm331MVtLLGY-T-fQ";
                googleOptions.Events = new OAuthEvents
                {
                    OnCreatingTicket = context =>
                    {
                        var picture = context.User.Root.SelectToken("image").SelectToken("url").ToString();

                        context.Identity.AddClaim(new Claim("image", picture));

                        return System.Threading.Tasks.Task.FromResult(0);
                    }
                };
            });

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = "511517489202644";
                facebookOptions.AppSecret = "c06100662e20a40f054fd63aa5cec806";
            });

            services.AddAuthentication().AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey = "KEoFgPoKMF4t4VTbkXPGHQOHO";
                twitterOptions.ConsumerSecret = "E6Wy3qvMGwLiNdNK6peYHAZOi5Z26uSaRFpvL3jhi5UokRR9i4";
            });
            

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            // For session-specific storage
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            // For session-specific storage
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
