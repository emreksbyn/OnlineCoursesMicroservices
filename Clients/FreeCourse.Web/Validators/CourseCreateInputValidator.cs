﻿using FluentValidation;
using FreeCourse.Web.Models.CatalogServiceModels;

namespace FreeCourse.Web.Validators
{
    public class CourseCreateInputValidator : AbstractValidator<CourseCreateInput>
    {
        public CourseCreateInputValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(c => c.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(c => c.Feature.Duration).InclusiveBetween(1,int.MaxValue).WithMessage("Description is required.");
            
            RuleFor(c => c.Price).NotEmpty().WithMessage("Price is required.")
                                    // $$$$.$$
                                 .ScalePrecision(2,6).WithMessage("Wrong currency format.");

            RuleFor(c => c.CategoryId).NotEmpty().WithMessage("Select the category.");
        }
    }
}