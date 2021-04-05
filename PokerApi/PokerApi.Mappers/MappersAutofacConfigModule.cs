using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerApi.Mappers
{
    public class MappersAutofacConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembliesWithProfile = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(Profile).IsAssignableFrom(x) && !x.IsInterface && x.IsPublic)
                .Select(x => x.Assembly).Distinct().ToList();

            builder.RegisterAssemblyTypes(assembliesWithProfile.ToArray())
                .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
                .As<Profile>();

            builder.Register(c =>
                new MapperConfiguration(cfg =>
                {
                    var profiles = c.Resolve<IEnumerable<Profile>>();
                    foreach (var profile in profiles)
                    {
                        cfg.AddProfile(profile);
                    }
                }))
                    .AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>()
                .CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }   
    }
}
