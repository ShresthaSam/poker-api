using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace PokerApi.Models.Validators
{
    public class PokerHandDtoListValidator : AbstractValidator<List<PokerHandDto>>
    {
        public PokerHandDtoListValidator()
        {
            RuleFor(p => p.Count).InclusiveBetween(2, 9);

            RuleFor(p => p[0].Card1).Must((list, c1) =>
            {
                var flattened = new List<string>();
                foreach (var pokerHandDto in list)
                {
                    flattened.Add(pokerHandDto.Card1);
                    flattened.Add(pokerHandDto.Card2);
                    flattened.Add(pokerHandDto.Card3);
                    flattened.Add(pokerHandDto.Card4);
                    flattened.Add(pokerHandDto.Card5);
                };
                var dups = flattened.GroupBy(i => i).Where(g => g.Count() > 1).Select(g => g.Key);
                return dups.Count() == 0;
            })
                .WithMessage(Constants.CARD_MUST_BE_UNIQUE_ACROSS_PLAYERS_MESSAGE);

            RuleFor(p => p).Must((list) =>
            {
                var flattened = new List<string>();
                foreach (var pokerHandDto in list)
                {
                    if (!string.IsNullOrEmpty(pokerHandDto.Player.UserId))
                        flattened.Add(pokerHandDto.Player.UserId);
                };
                if (flattened.Count > 0)
                {
                    var dups = flattened.GroupBy(i => i).Where(g => g.Count() > 1).Select(g => g.Key);
                    return dups.Count() == 0;
                }
                return true;
            })
                .WithMessage(Constants.PLAYER_USERID_MUST_BE_UNIQUE_MESSAGE);
        }
    }
}
