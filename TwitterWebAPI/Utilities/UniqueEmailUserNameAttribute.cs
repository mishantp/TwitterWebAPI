using System.ComponentModel.DataAnnotations;
using TwitterWebAPI.Data;

namespace TwitterWebAPI.Utilities
{
    public class UniqueEmailUserNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var _appDbcontext = (AppDbContext)validationContext.GetService(typeof(AppDbContext));
            var entity = _appDbcontext?.Users.SingleOrDefault(u => u.Email.ToLower() == value.ToString().ToLower() 
                                                            || u.LoginId.ToLower() == value.ToString().ToLower());

            if (entity != null)
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }
            return ValidationResult.Success;
        }
        public string GetErrorMessage(string uniqueVal)
        {
            return $"{uniqueVal} is already in use. Kindly try with diffrent one.";
        }
    }
}
