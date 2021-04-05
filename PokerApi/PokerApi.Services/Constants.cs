namespace PokerApi.Services
{
    public class Constants
    {
        public const string OPEN_API_CONTACT_NAME = "Poker Inc. API Team";
        public const string OPEN_API_CONTACT_EMAIL = "api-team@poker-inc.org";
        public const string OPEN_API_SWAGGER_DOC_DESCRIPTION = "Poker API that processes multiple poker hands and returns aggregates such as rankings and so on.";
        public const string OPEN_API_SWAGGER_JSON_URL = "/swagger/v1/swagger.json";

        public const string APPSETTINGS_POKER_SETTINGS_KEY_ENABLE_ACE_LOW_FIVE_HIGH = "PokerSettings:EnableAceLowFiveHigh";
        public const bool APPSETTINGS_POKER_SETTINGS_KEY_ENABLE_ACE_LOW_DEFAULT = true;
        public const string APPSETTINGS_POKER_SETTINGS_KEY_ENABLE_RANK_GAPS = "PokerSettings:EnableRankGaps";
        public const bool APPSETTINGS_POKER_SETTINGS_KEY_ENABLE_RANK_GAPS_DEFAULT = true;
    }
}
