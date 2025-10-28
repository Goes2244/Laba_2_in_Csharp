namespace lab2
{
    public class Person : Validator
    {
        public Name PersonName { get; set; }
        public int Height { get; set; }
        public Person Father { get; set; }

        public Person(Name name, int height)
        {
            ValidateNotNull(name, "Имя")
                .ValidateHeight(height);
            ThrowIfInvalid();

            PersonName = name;
            Height = height;
        }

        public Person(Name name, int height, Person father) : this(name, height)
        {
            ValidateNotNull(father, "Отец");
            ThrowIfInvalid();

            Father = father;
            ApplyInheritanceRules();
        }

        public void ApplyInheritanceRules()
        {
            if (Father?.PersonName != null)
            {
                if (string.IsNullOrEmpty(PersonName.LastName) && !string.IsNullOrEmpty(Father.PersonName.LastName))
                {
                    PersonName.LastName = Father.PersonName.LastName;
                }

                if (string.IsNullOrEmpty(PersonName.MiddleName) && !string.IsNullOrEmpty(Father.PersonName.FirstName))
                {
                    PersonName.MiddleName = Father.PersonName.FirstName + "ович";
                }
            }
        }

        public override string ToString()
        {
            return $"{PersonName}, рост: {Height} см";
        }
    }
}