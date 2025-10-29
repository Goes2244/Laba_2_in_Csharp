using System.Collections.Generic;

namespace lab2
{
    public class Name
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public Name(string firstName)
        {
            Validator.Validate(v => v.ValidateNamePart(firstName, "Имя", true));
            FirstName = firstName;
        }

        public Name(string lastName, string firstName) : this(firstName)
        {
            Validator.Validate(v => v.ValidateNamePart(lastName, "Фамилия", true));
            LastName = lastName;
        }

        public Name(string lastName, string firstName, string middleName) : this(lastName, firstName)
        {
            if (!string.IsNullOrEmpty(middleName))
            {
                Validator.Validate(v => v.ValidateNamePart(middleName, "Отчество", false));
                MiddleName = middleName;
            }
        }

        public override string ToString()
        {
            var parts = new List<string>();
            if (!string.IsNullOrEmpty(LastName)) parts.Add(LastName);
            if (!string.IsNullOrEmpty(FirstName)) parts.Add(FirstName);
            if (!string.IsNullOrEmpty(MiddleName)) parts.Add(MiddleName);
            return string.Join(" ", parts);
        }
    }
}
