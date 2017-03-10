﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PipServices.Dummy.Build;
using PipServices.Dummy.Logic;

namespace PipServices.Dummy
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddSingleton<IDummyController, DummyController>(provider => DummyFactory.Сontroller);
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
        {
            applicationLifetime.ApplicationStopping.Register(OnShutdown);

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            loggerFactory.AddDebug();

            app.UseMvc();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("default", "{controller=DummyWebApi}");
            //});

            app.UseStatusCodePages();//context => context.HttpContext.Response.);

            //app.UseMvc(routes =>
            //{
            //    routes.DefaultHandler = new MvcAttributeRouteHandler();//MapRoute()
            //});

            //var serverAddressesFeature = app.ServerFeatures.Get<IServerAddressesFeature>();

            //app.UseStaticFiles();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync($"Hosted by ME\r\n\r\n"); //{Program.Server}

            //    if (serverAddressesFeature != null)
            //    {
            //        await context.Response.WriteAsync($"Listening on the following addresses: {string.Join(", ", serverAddressesFeature.Addresses)}\r\n");
            //    }

            //    await context.Response.WriteAsync($"Request URL: {context.Request.GetDisplayUrl()}");
            //});
        }

        private void OnShutdown()
        {
            
        }
    }
}
