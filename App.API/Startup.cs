using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.API.Options;
using App.Constants;
using App.DatabaseContext;
using App.DatabaseContext.Models.Master;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace App.API
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
            // ========== Options ==========
            //
            var jwtAuthenticationConfigSection = Configuration.GetSection("JwtAuthenticationOptions");
            var jwtAuthenticationOptions = jwtAuthenticationConfigSection.Get<JwtAuthenticationOptions>();
            services.Configure<JwtAuthenticationOptions>(jwtAuthenticationConfigSection);

            services.Configure<IdentityOptions>(Configuration.GetSection("IdentityOptions"));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //
            // ========== Options ==========

            // ========== DbContext ==========
            //
            // Injecting MasterDbContext
            services.AddDbContext<MasterDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString(DbContextConstants.MASTER_CONNECTION_STRING)));

            // ***Convention Approach: injecting DbContext into DI
            //services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql("Host=localhost;Database=AppPrototype_Master;Username=postgres;Password=password"));
            // ***Innovative Approach: injecting DbContextFactory instead, and "DatabaseName" will be passed on to initialize DbContext in a controller's constructor
            services.AddTransient<ApplicationDbContextFactory>();
            //
            // ========== DbContext ==========

            // ========== Authentication ==========
            //
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<MasterDbContext>()
                .AddDefaultTokenProviders();

            // JWT Authentication Configuration
            var securityKey = Encoding.UTF8.GetBytes(jwtAuthenticationOptions.SecurityKey);
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(securityKey),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero

                };
            });
            //
            // ========== Authentication ==========

            // ========== Application Services ==========
            //
            //services.AddTransient<IEmailSender, AuthMessageSender>();
            //services.AddTransient<ISmsSender, AuthMessageSender>();
            //
            // ========== Application Services ==========

            services.AddCors();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var jwtAuthenticationOptions = Configuration.GetSection("JwtAuthenticationOptions").Get<JwtAuthenticationOptions>();

            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                //app.UseBrowserLink();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts(); // HTTP Strict Transport Security (HSTS)

                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            
            app.UseCors(builder => {
                builder.WithOrigins(jwtAuthenticationOptions.ClientUrl)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}");
            });
        }
    }
}
