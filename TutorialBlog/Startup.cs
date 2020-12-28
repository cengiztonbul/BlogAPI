using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TutorialBlog.Models;

namespace TutorialBlog
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
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", builder =>
				{
					builder
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
				});
			});

			services.Configure<MvcOptions>(options =>
			{
				options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAll"));
			});

			services.Configure<BlogDatabaseSettings>(
			Configuration.GetSection(nameof(BlogDatabaseSettings)));
			services.Configure<UserDatabaseSettings>(
			Configuration.GetSection(nameof(UserDatabaseSettings)));

			var key = Encoding.ASCII.GetBytes("classifiedclassifiedclassifiedclassified");;

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});
			services.AddAuthorization();
			services.AddScoped<DAL.Abstract.IBlogDAL, DAL.Concerete.BlogDAL>();
			services.AddScoped<DAL.Abstract.IUserDAL, DAL.Concerete.UserDAL>();
			services.AddScoped<Business.Abstract.IBlogService, Business.Concrete.BlogManager>();
			services.AddScoped<Business.Abstract.IUserService, Business.Concrete.UserManager>();

			services.AddSingleton<BlogDatabaseSettings>(sp =>
				sp.GetRequiredService<IOptions<BlogDatabaseSettings>>().Value);
			services.AddSingleton<UserDatabaseSettings>(sp =>
				sp.GetRequiredService<IOptions<UserDatabaseSettings>>().Value);

			services.AddSingleton<Business.Concrete.BlogManager>();
			services.AddSingleton<Business.Concrete.UserManager>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseAuthentication();

			app.UseMvc();
		}
	}
}
