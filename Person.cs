namespace lab2
{
    public class Person
    {
        private double _height;
        private string _gender;

        public Name PersonName { get; set; }
        
        public double Height 
        { 
            get => _height; 
            set
            {
                Validator.Validate(v => v.ValidateHeight(value));
                _height = value;
            }
        }
        
        public Person Father { get; set; }
        
        public string Gender 
        { 
            get => _gender; 
            set
            {
                Validator.Validate(v => v.ValidateGender(value));
                _gender = value;
            }
        }

        public Person() : this(new Name(), 0) { }

        public Person(Name name, double height)
        {
            Validator.Validate(v => v.ValidateNotNull(name, "Имя"));
            PersonName = name;
            Height = height;
            Gender = "мужской";
        }

        public Person(Name name, double height, string gender) : this(name, height)
        {
            Gender = gender;
        }

        private string GetPatronymicSuffix(string fatherName)
        {
            if (string.IsNullOrEmpty(fatherName))
            {
                return "ович";
            }

            char lastChar = fatherName[fatherName.Length - 1];
            
            if (Gender?.ToLower() == "женский")
            {
                return lastChar == 'а' || lastChar == 'я' ? "овна" : "овична";
            }
            else
            {
                return lastChar == 'а' || lastChar == 'я' ? "ович" : "ович";
            }
        }

        public override string ToString()
        {
            if (Father != null)
            {
                if (string.IsNullOrEmpty(PersonName.LastName) && !string.IsNullOrEmpty(Father.PersonName.LastName))
                {
                    PersonName.LastName = Father.PersonName.LastName;
                }

                if (string.IsNullOrEmpty(PersonName.MiddleName) && !string.IsNullOrEmpty(Father.PersonName.FirstName))
                {
                    string suffix = GetPatronymicSuffix(Father.PersonName.FirstName);
                    PersonName.MiddleName = Father.PersonName.FirstName + suffix;
                }
            }

            {
                return $"{PersonName}, рост: {Height} см, пол: {Gender}";
            }
        }
    }
}