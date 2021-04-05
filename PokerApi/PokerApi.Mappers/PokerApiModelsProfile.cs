using AutoMapper;
using PokerApi.Common.Models;
using PokerApi.Models;

namespace PokerApi.Mappers
{
    public class PokerApiModelsProfile : Profile
    {
        public PokerApiModelsProfile()
        {
            CreateMap<Player, Repositories.Entities.Player>()
                .ReverseMap();
            CreateMap<CardEnum, Repositories.Entities.Card>()
                .ForMember(d => d.Suit, o => o.MapFrom(s => s.ToString().Substring(0, 1)))
                .ForMember(d => d.Number, o => o.MapFrom(s => s.ToString().Substring(1, s.ToString().Length - 1)))
                .ForMember(d => d.CardId, o => o.MapFrom(s => (int)s));
            CreateMap<Repositories.Entities.Card, CardEnum>()
                .ConvertUsing<CardToCardEnumConverter>();
            CreateMap<PokerHandDomainModel, Repositories.Entities.Hand>()
                .ForMember(d => d.CardId1, o => o.MapFrom(s => (int)s.Card1))
                .ForMember(d => d.CardId2, o => o.MapFrom(s => (int)s.Card2))
                .ForMember(d => d.CardId3, o => o.MapFrom(s => (int)s.Card3))
                .ForMember(d => d.CardId4, o => o.MapFrom(s => (int)s.Card4))
                .ForMember(d => d.CardId5, o => o.MapFrom(s => (int)s.Card5))
                .ForMember(d => d.HandId, o => o.MapFrom(s => s.PokerHandId))
                .ForMember(d => d.PlayerUserId, o => o.MapFrom(s => s.Player.UserId));
            CreateMap<Repositories.Entities.Hand, PokerHandDomainModel>()
                .ConvertUsing<PokerHandConverter>();
            CreateMap<CardItem, Repositories.Entities.Card>()
                .ForMember(d => d.Suit, o => o.MapFrom(s => s.Suit))
                .ForMember(d => d.Number, o => o.MapFrom(s => s.Number))
                .ForMember(d => d.CardId, o => o.MapFrom(s => s.Id))
                .ReverseMap();
        }
    }
}
