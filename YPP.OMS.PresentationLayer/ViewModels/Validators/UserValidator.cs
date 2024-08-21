using FluentValidation;
using YPP.MH.DataAccessLayer.Models;

namespace YPP.MH.PresentationLayer.ViewModels.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() { }

    }
}
