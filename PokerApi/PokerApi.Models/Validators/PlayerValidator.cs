using FluentValidation;

namespace PokerApi.Models.Validators
{
    public class PlayerValidator : AbstractValidator<Player>
    {
        public PlayerValidator()
        {
            RuleFor(p => p.UserId).NotEqual("string");
            RuleFor(p => p.UserName).NotEqual("string");
            RuleFor(p => p.UserName).NotEmpty();
            RuleFor(p => p.UserName).NotNull();
        }
    }
}
