using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace PokerApi.Models.Validators
{
    public class PokerHandDtoValidator : AbstractValidator<PokerHandDto>
    {
        public PokerHandDtoValidator()
        {
            RuleFor(p => p.Card1.Length).InclusiveBetween(2, 3);
            RuleFor(p => p.Card2.Length).InclusiveBetween(2, 3);
            RuleFor(p => p.Card3.Length).InclusiveBetween(2, 3);
            RuleFor(p => p.Card4.Length).InclusiveBetween(2, 3);
            RuleFor(p => p.Card5.Length).InclusiveBetween(2, 3);
            RuleFor(m => m.Card1).Must((hand, c1) =>
            {
                var list = new List<string> { c1, hand.Card2, hand.Card3, hand.Card4, hand.Card5};
                var dups = list.GroupBy(i => i).Where(g => g.Count() > 1).Select(g => g.Key);
                return dups.Count() == 0;
            })
                .WithMessage(Constants.CARD_MUST_BE_UNIQUE_MESSAGE);

            RuleFor(p => p.Card1).Must(c =>
            {
                return ValidateCardNumberAndSuit(c);
            })
                .WithMessage(Constants.CARD_INVALID_MESSAGE);

            RuleFor(p => p.Card2).Must(c =>
            {
                return ValidateCardNumberAndSuit(c);
            })
                .WithMessage(Constants.CARD_INVALID_MESSAGE);

            RuleFor(p => p.Card3).Must(c =>
            {
                return ValidateCardNumberAndSuit(c);
            })
                .WithMessage(Constants.CARD_INVALID_MESSAGE);

            RuleFor(p => p.Card4).Must(c =>
            {
                return ValidateCardNumberAndSuit(c);
            })
                .WithMessage(Constants.CARD_INVALID_MESSAGE);

            RuleFor(p => p.Card5).Must(c =>
            {
                return ValidateCardNumberAndSuit(c);
            })
                .WithMessage(Constants.CARD_INVALID_MESSAGE);
        }

        /// <summary>
        /// A cardNumberAndSuit parameter must be in "XY" format where "X" is a number 2 thru 10, J, Q, K or A
        /// and "Y" is a suit of H, S, C or D. Examples of valid cardNumberAndSuit are 2H, AD, 10C etc.
        /// </summary>
        /// <param name="cardNumberAndSuit">Must be in "XY" format described above</param>
        /// <returns></returns>
        private bool ValidateCardNumberAndSuit(string cardNumberAndSuit)
        {
            var validated = true;
            var suit = cardNumberAndSuit.Substring(cardNumberAndSuit.Length - 1, 1);
            var number = cardNumberAndSuit.Substring(0, cardNumberAndSuit.Length - 1);
            if (suit != "D" && suit != "H" && suit != "C" && suit != "S")
                validated = false;
            if (validated)
            {
                if (number != "2" && number != "3" && number != "4" && number != "5" && number != "6" && number != "7"
                    && number != "8" && number != "9" && number != "10" && number != "J" && number != "Q"
                    && number != "K" && number != "A")
                        validated = false;
            }
            return validated;
        }
    }
}
