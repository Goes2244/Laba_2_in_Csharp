namespace lab2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Validator
    {
        private readonly List<string> _errors;

        public Validator()
        {
            _errors = new List<string>();
        }

        public List<string> Errors => _errors;
        public bool IsValid => !_errors.Any();

        public void ClearErrors()
        {
            _errors.Clear();
        }

        public void AddError(string errorMessage)
        {
            _errors.Add(errorMessage);
        }

        public Validator ValidateString(string value, string fieldName, bool required = false, 
            int? minLength = null, int? maxLength = null, string pattern = null)
        {
            if (required && string.IsNullOrWhiteSpace(value))
            {
                _errors.Add($"{fieldName} является обязательным полем");
                return this;
            }

            if (!required && string.IsNullOrWhiteSpace(value))
            {
                return this;
            }

            if (minLength.HasValue && value.Length < minLength.Value)
                _errors.Add($"{fieldName} должен содержать минимум {minLength} символов");

            if (maxLength.HasValue && value.Length > maxLength.Value)
                _errors.Add($"{fieldName} должен содержать максимум {maxLength} символов");

            if (!string.IsNullOrEmpty(pattern) && !Regex.IsMatch(value, pattern))
                _errors.Add($"{fieldName} имеет неверный формат");

            return this;
        }
        
        public Validator ValidateNumber<T>(T value, string fieldName, 
            T? minValue = null, T? maxValue = null, bool required = false) where T : struct, IComparable
        {
            if (required && value.Equals(default(T)))
            {
                _errors.Add($"{fieldName} является обязательным полем");
                return this;
            }

            if (minValue.HasValue && value.CompareTo(minValue.Value) < 0)
                _errors.Add($"{fieldName} должен быть не меньше {minValue}");

            if (maxValue.HasValue && value.CompareTo(maxValue.Value) > 0)
                _errors.Add($"{fieldName} должен быть не больше {maxValue}");

            return this;
        }

        public Validator ValidateInt(int value, string fieldName, 
            int? minValue = null, int? maxValue = null, bool required = false)
        {
            return ValidateNumber(value, fieldName, minValue, maxValue, required);
        }

        public Validator ValidateDouble(double value, string fieldName, 
            double? minValue = null, double? maxValue = null, bool required = false)
        {
            return ValidateNumber(value, fieldName, minValue, maxValue, required);
        }

        public Validator ValidateDateTime(DateTime value, string fieldName, 
            DateTime? minDate = null, DateTime? maxDate = null, bool required = false)
        {
            if (required && value == default(DateTime))
            {
                _errors.Add($"{fieldName} является обязательным полем");
                return this;
            }

            if (minDate.HasValue && value < minDate.Value)
                _errors.Add($"{fieldName} не может быть раньше {minDate.Value:dd.MM.yyyy}");

            if (maxDate.HasValue && value > maxDate.Value)
                _errors.Add($"{fieldName} не может быть позже {maxDate.Value:dd.MM.yyyy}");

            return this;
        }

        public Validator ValidateBool(bool value, string fieldName, bool requiredValue)
        {
            if (value != requiredValue)
                _errors.Add($"{fieldName} должен быть {requiredValue}");

            return this;
        }

        public Validator ValidateRegex(string value, string fieldName, string pattern, bool required = false)
        {
            if (required && string.IsNullOrWhiteSpace(value))
            {
                _errors.Add($"{fieldName} является обязательным полем");
                return this;
            }

            if (!required && string.IsNullOrWhiteSpace(value))
            {
                return this;
            }

            if (!Regex.IsMatch(value, pattern))
                _errors.Add($"{fieldName} имеет неверный формат");

            return this;
        }

        public Validator ValidateIn<T>(T value, string fieldName, IEnumerable<T> allowedValues, bool required = false)
        {
            if (required && value == null)
            {
                _errors.Add($"{fieldName} является обязательным полем");
                return this;
            }

            if (!required && value == null)
            {
                return this;
            }

            if (!allowedValues.Contains(value))
                _errors.Add($"{fieldName} содержит недопустимое значение");

            return this;
        }

        public Validator ValidateCoordinate(double value, string coordinateName)
        {
            return ValidateDouble(value, coordinateName, minValue: -1000, maxValue: 1000, required: true);
        }

        public Validator ValidateNamePart(string value, string fieldName, bool required = false)
        {
            if (!required && string.IsNullOrEmpty(value))
            {
                return this;
            }

            return ValidateString(value, fieldName, required: required, minLength: 2, maxLength: 50, 
                            pattern: @"^[A-Za-zА-Яа-яЁё\- ]+$");
        }

        public Validator ValidateHeight(double height)
        {
            return ValidateDouble(height, "Рост", minValue: 30, maxValue: 300, required: true);
        }

        public Validator ValidateGender(string gender)
        {
            var allowedGenders = new List<string> { "мужской", "женский" };
            return ValidateIn(gender, "Пол", allowedGenders, required: true);
        }

        public Validator ValidateShift(double deltaX, double deltaY)
        {
            return ValidateDouble(deltaX, "Сдвиг по X", minValue: -100, maxValue: 100)
                  .ValidateDouble(deltaY, "Сдвиг по Y", minValue: -100, maxValue: 100);
        }

        public Validator ValidatePointsArray(Point[] points, string fieldName = "Точки ломаной")
        {
            if (points == null || points.Length == 0)
            {
                _errors.Add($"{fieldName} не может быть пустым");
                return this;
            }

            if (points.Length < 2)
            {
                _errors.Add($"{fieldName} должен содержать минимум 2 точки");
            }

            for (int i = 0; i < points.Length; i++)
            {
                if (points[i] == null)
                {
                    _errors.Add($"{fieldName}: точка {i + 1} не может быть null");
                }
            }

            return this;
        }

        public Validator ValidateNotNull<T>(T obj, string fieldName) where T : class
        {
            if (obj == null)
            {
                _errors.Add($"{fieldName} не может быть null");
            }

            return this;
        }

        public Validator ValidateCollection<T>(IEnumerable<T> collection, string fieldName, bool requireNotEmpty = false)
        {
            if (collection == null)
            {
                _errors.Add($"{fieldName} не может быть null");
                return this;
            }

            if (requireNotEmpty && !collection.Any())
            {
                _errors.Add($"{fieldName} не может быть пустым");
            }

            return this;
        }
        
        public ValidationResult GetResult()
        {
            return new ValidationResult(IsValid, _errors.ToList());
        }

        public void ThrowIfInvalid()
        {
            if (!IsValid)
            {
                throw new ValidationException(string.Join("; ", _errors));
            }
        }

        public static void Validate(Action<Validator> validationAction)
        {
            var validator = new Validator();
            validationAction(validator);
            validator.ThrowIfInvalid();
        }

        // Статические методы для ввода данных
        public static int GetValidatedInt(string fieldName, int minValue = 1, int maxValue = 100, bool required = true)
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    Validate(v => v.ValidateInt(int.Parse(input), fieldName, minValue, maxValue, required));
                    return int.Parse(input);
                }
                catch (FormatException)
                {
                    Console.Write($"Ошибка: введите целое число для {fieldName}. Введите снова: ");
                }
                catch (ValidationException ex)
                {
                    Console.Write($"{ex.Message}. Введите снова: ");
                }
            }
        }

        public static double GetValidatedDouble(string fieldName, double minValue = -1000, double maxValue = 1000)
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    Validate(v => v.ValidateDouble(double.Parse(input), fieldName, minValue, maxValue, true));
                    return double.Parse(input);
                }
                catch (ValidationException ex)
                {
                    Console.Write($"{ex.Message}. Введите снова: ");
                }
            }
        }

        public static string GetValidatedString(string fieldName, int minLength = 1, int maxLength = 100, bool required = true)
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    Validate(v => v.ValidateString(input, fieldName, required, minLength, maxLength));
                    return input;
                }
                catch (ValidationException ex)
                {
                    Console.Write($"{ex.Message}. Введите снова: ");
                }
            }
        }

        public static string GetValidatedNamePart(string fieldName, bool required = true)
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    Validate(v => v.ValidateNamePart(input, fieldName, required));
                    return input;
                }
                catch (ValidationException ex)
                {
                    Console.Write($"{ex.Message}. Введите снова: ");
                }
            }
        }

        public static int GetValidatedHeight()
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    double height = double.Parse(input);
                    Validate(v => v.ValidateHeight(height));
                    return (int)height;
                }
                catch (FormatException)
                {
                    Console.Write("Ошибка: введите число для роста. Введите снова: ");
                }
                catch (ValidationException ex)
                {
                    Console.Write($"{ex.Message}. Введите снова: ");
                }
            }
        }

        public static double GetValidatedCoordinate(string coordinateName)
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    double coordinate = double.Parse(input);
                    Validate(v => v.ValidateCoordinate(coordinate, coordinateName));
                    return coordinate;
                }
                catch (ValidationException ex)
                {
                    Console.Write($"{ex.Message}. Введите снова: ");
                }
            }
        }

        public static double GetValidatedShift(string coordinateName)
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    double shift = double.Parse(input);
                    Validate(v => v.ValidateDouble(shift, coordinateName, -100, 100));
                    return shift;
                }
                catch (ValidationException ex)
                {
                    Console.Write($"{ex.Message}. Введите снова: ");
                }
            }
        }
    }

    public class ValidationResult
    {
        public bool IsValid { get; }
        public List<string> Errors { get; }

        public ValidationResult(bool isValid, List<string> errors)
        {
            IsValid = isValid;
            Errors = errors;
        }
    }

    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
    
}
