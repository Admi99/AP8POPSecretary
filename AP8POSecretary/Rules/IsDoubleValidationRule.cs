﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace AP8POSecretary.Rules
{
    public class IsDoubleValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString()) && !double.TryParse(value as String, out _)
               ? new ValidationResult(false, "Field is not number or is empty")
               : ValidationResult.ValidResult;
        }
    }
}
