namespace lab2
{
    public class Name
    {
        private string _lastName;
        private string _firstName;
        private string _middleName;

        public string LastName 
        { 
            get => _lastName; 
            set
            {
                Validator.Validate(v => v.ValidateNamePart(value, "Фамилия"));
                _lastName = value;
            }
        }

        public string FirstName 
        { 
            get => _firstName; 
            set
            {
                Validator.Validate(v => v.ValidateNamePart(value, "Имя", required: true));
                _firstName = value;
            }
        }

        public string MiddleName 
        { 
            get => _middleName; 
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Validator.Validate(v => v.ValidateNamePart(value, "Отчество"));
                }
                _middleName = value;
            }
        }

        public Name() { }

        public Name(string firstName) : this(null, firstName, null) { }

        public Name(string lastName, string firstName) : this(lastName, firstName, null) { }

        public Name(string lastName, string firstName, string middleName)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }

        public override string ToString()
        {
            var parts = new List<string>();
            
            if (!string.IsNullOrEmpty(LastName))
                parts.Add(LastName);
            if (!string.IsNullOrEmpty(FirstName))
                parts.Add(FirstName);
            if (!string.IsNullOrEmpty(MiddleName))
                parts.Add(MiddleName);

            {
                return string.Join(" ", parts);
            }
        }
    }
}