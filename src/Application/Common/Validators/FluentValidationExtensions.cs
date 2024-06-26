using FluentValidation;
using Microsoft.EntityFrameworkCore;
using BackEnd.Application.Common.Interfaces;
using BackEnd.Domain.Enums;

namespace BackEnd.Application.Common.Validators
{
    public static class FluentValidationExtensions
    {

        public static IRuleBuilderOptions<T, int> ValidateId<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
                .NotNull()
                .NotEmpty()
                .NotEqual(0)
                .WithMessage($"Incorrect {{PropertyName}} selected.");
        }

        public static IRuleBuilderOptions<T, string> MaxDescriptionLength<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.MaximumLength(550).WithMessage($"{{PropertyName}} cannot exceed 550 characters.");
        }

        public static IRuleBuilderOptions<T, string> MaxTitleLength<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.MaximumLength(70).WithMessage($"{{PropertyName}} cannot exceed 75 characters.");
        }

        public static IRuleBuilderOptions<T, int> EmployeeExist<T>(this IRuleBuilder<T, int> ruleBuilder, IApplicationDbContext _context)
        {
            return ruleBuilder.MustAsync(async (value, token) => await EmployeeExist(_context, value))
                              .WithMessage("User not found");
        }

        private static async Task<bool> EmployeeExist(IApplicationDbContext _context, int UserId)
        {
            return await _context.Employees.Where(x => x.Id == UserId).AnyAsync();
        }

    }


}