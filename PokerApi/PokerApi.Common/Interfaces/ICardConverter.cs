using PokerApi.Common.Models;

namespace PokerApi.Common.Interfaces
{
    public interface ICardConverter
    {
        public CardItem ConvertEnumToItem(CardEnum cardEnum);
        public CardEnum ConvertItemToEnum(CardItem cardItem);
        public CardEnum ConvertIdToEnum(int cardId);
        public CardEnum ConvertSuitAndNumberToEnum(string cardSuitAndNumber);
        public CardEnum ConvertNumberAndSuitToEnum(string cardNumberAndSuit);
    }
}
