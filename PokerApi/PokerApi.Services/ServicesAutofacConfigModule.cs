using Autofac;
using Microsoft.Extensions.Configuration;
using PokerApi.Common;
using PokerApi.Common.Interfaces;
using PokerApi.Models;
using PokerApi.RankAssignment.Abstractions;
using PokerApi.RankAssignment.Simple;
using PokerApi.Services.Interfaces;
using Serilog;
using System.IO;

namespace PokerApi.Services
{
    public class ServicesAutofacConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(Log.Logger)
                .As<ILogger>()
                .SingleInstance();

            builder.RegisterType<RankAssignmentService>().As<IRankAssignmentService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CardConverter>().As<ICardConverter>()
                .SingleInstance();

            builder.RegisterType<SimpleCardSorter>().As<ICardSorter>()
                .SingleInstance();

            builder.Register<IConfiguration>(ctx =>
            {
                var resolver = ctx.Resolve<IComponentContext>();
                var logger = resolver.Resolve<ILogger>();
                logger.Information("Started configuration builder");
                var configBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                    .AddJsonFile($"appsettings.development.json", optional: true, reloadOnChange: false)
                    .AddJsonFile($"appsettings.test.json", optional: true, reloadOnChange: false)
                    .AddJsonFile($"appsettings.production.json", optional: true, reloadOnChange: false)
                    .AddEnvironmentVariables();
                var configuraion = configBuilder.Build();
                logger.Information("Finished configuration builder");
                return configuraion;
            })
                .SingleInstance();

            builder.Register(ctx =>
            {
                var resolver = ctx.Resolve<IComponentContext>();
                var logger = resolver.Resolve<ILogger>();
                var configuration = resolver.Resolve<IConfiguration>();
                logger.Information("Started configuring Poker Settings");
                var settings = GetPokerSettings(configuration, logger);
                logger.Information("Finished configuring Poker Settings");
                return settings;
            })
                .As<Settings>()
                .SingleInstance();

            builder.Register(ctx =>
            {
                var resolver = ctx.Resolve<IComponentContext>();
                var logger = resolver.Resolve<ILogger>();
                var settings = resolver.Resolve<Settings>();
                logger.Information("Started configuring Poker Hand Recognizer");
                var pokerHandRecognizer = new PokerHandRecognizer(settings.EnableAceLowFiveHigh);
                logger.Warning("Finished configuring Poker Hand Recognizer with EnableAceLowFiveHigh:{EnableAceLowFiveHigh}", settings.EnableAceLowFiveHigh);
                return pokerHandRecognizer;
            })
                .As<IPokerHandRecognizer>()
                .SingleInstance();

            builder.Register(ctx =>
            {
                var resolver = ctx.Resolve<IComponentContext>();
                var logger = resolver.Resolve<ILogger>();
                var settings = resolver.Resolve<Settings>();
                logger.Information("Started configuring Simple Assigner");
                var assigner = new Assigner(settings.EnableRankGaps);
                logger.Warning("Finished configuring Simple Assigner with EnableRankGaps:{EnableRankGaps}", settings.EnableRankGaps);
                return assigner;
            })
                .As<IRankAssigner>()
                .SingleInstance();
        }

        private Settings GetPokerSettings(IConfiguration configuration, ILogger logger)
        {
            var enableAceLow = Constants.APPSETTINGS_POKER_SETTINGS_KEY_ENABLE_ACE_LOW_DEFAULT;
            var enableRankGaps = Constants.APPSETTINGS_POKER_SETTINGS_KEY_ENABLE_RANK_GAPS_DEFAULT;
            var configSection = configuration.GetSection(Constants.APPSETTINGS_POKER_SETTINGS_KEY_ENABLE_ACE_LOW_FIVE_HIGH);
            if (configSection != null)
            {
                if (!bool.TryParse(configSection.Value, out enableAceLow))
                    logger.Warning("Cannot parse Poker Settings value for Key:{Key}", Constants.APPSETTINGS_POKER_SETTINGS_KEY_ENABLE_ACE_LOW_FIVE_HIGH);
            }
            else
            {
                logger.Warning("Cannot read Poker Settings Key:{Key}", Constants.APPSETTINGS_POKER_SETTINGS_KEY_ENABLE_ACE_LOW_FIVE_HIGH);
            }
            configSection = configuration.GetSection(Constants.APPSETTINGS_POKER_SETTINGS_KEY_ENABLE_RANK_GAPS);
            if (configSection != null)
            {
                if (!bool.TryParse(configSection.Value, out enableRankGaps))
                    logger.Warning("Cannot parse Poker Settings value for Key:{Key}", Constants.APPSETTINGS_POKER_SETTINGS_KEY_ENABLE_RANK_GAPS);
            }
            else
            {
                logger.Warning("Cannot read Poker Settings Key:{Key}", Constants.APPSETTINGS_POKER_SETTINGS_KEY_ENABLE_RANK_GAPS);
            }
            return new Settings { EnableAceLowFiveHigh = enableAceLow, EnableRankGaps = enableRankGaps };
        }
    }
}
