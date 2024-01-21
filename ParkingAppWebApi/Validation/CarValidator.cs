using FluentValidation;
using ParkingAppWebApi.Models;
using System.Text.RegularExpressions;

namespace ParkingAppWebApi.Validation;

public class CarValidator : AbstractValidator<Car>
{
    public CarValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Please specify a name");
        RuleFor(x => x.Name).Length(2, 15);

        RuleFor(x => x.PlateNumber).NotEmpty().WithMessage("Please specify a plate number");
        RuleFor(x => x.PlateNumber).Length(5, 10);
        RuleFor(x => x.PlateNumber).Must(BeAValidPlateNumber).WithMessage("please a specify a valid plate number");
    }

    private bool BeAValidPlateNumber(string licensePlate)
    {
        const string pattern = @"^[A-Z]{2}\d{3}[A-Z]{2}$";
        var regex = new Regex(pattern);

        return regex.IsMatch(licensePlate);
    }

}