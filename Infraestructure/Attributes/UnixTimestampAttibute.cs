using System;
using System.ComponentModel.DataAnnotations;

public class UnixTimestampAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is null) return ValidationResult.Success!;

        if (value is long)
        {
            try
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((long)value);
                return ValidationResult.Success!;
            }
            catch (ArgumentOutOfRangeException)
            {
                return new ValidationResult("El timestamp de Unix está fuera de rango.");
            }
        }

        return new ValidationResult("El valor proporcionado no es un timestamp de Unix válido.");
    }
}
