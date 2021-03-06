﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFTestProject.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace EFTestProject
{
    public class Startup
    {
	    public Startup(IHostingEnvironment env)
	    {
		    var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

		    builder.AddEnvironmentVariables();
			
		    Configuration = builder.Build();
	    }

		public IConfigurationRoot Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
        {
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), o => o.UseRowNumberForPaging()));

	        services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

	        app.UseMvc(routes =>
	        {
		        routes.MapRoute(
			        name: "default",
			        template: "{controller=Default}/{action=Index}");
	        });
        }
    }
}
