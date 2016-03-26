﻿using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Owin;
using tupapi.Shared.Enums;
using tupapiService.DataObjects;
using tupapiService.Helpers.DBHelpers;
using tupapiService.Models;

namespace tupapiService
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686 
            config.EnableSystemDiagnosticsTracing();

            new MobileAppConfiguration()
                //  .AddMobileAppHomeController()
                .UseDefaultConfiguration()
                //  .MapLegacyCrossDomainController()
                .ApplyTo(config);

            Mapper.Initialize(cfg =>
            {
                // UserDTO Mapping
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
                // PostDTO Mapping
                cfg.CreateMap<Post, PostDTO>()
                    .ForMember(dst => dst.UserName, map => map.MapFrom(src => src.User.Name))
                    .ForMember(dst => dst.Likes, map => map.MapFrom(src => src.Votes.Count(v => v.Type == VoteType.Up)));
                cfg.CreateMap<PostDTO, Post>();
            });
            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new TupapiInitializer());

            // To prevent Entity Framework from modifying your database schema, use a null database initializer
            //Database.SetInitializer<TupapiContext>(null);

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                // This middleware is intended to be used locally for debugging. By default, HostName will
                // only have a value when running in an App Service application.
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] {ConfigurationManager.AppSettings["ValidAudience"]},
                    ValidIssuers = new[] {ConfigurationManager.AppSettings["ValidIssuer"]},
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }
            app.UseWebApi(config);
            ConfigureSwagger(config);
        }
    }
}