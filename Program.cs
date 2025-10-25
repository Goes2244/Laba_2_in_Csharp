namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЛАБОРАТОРНАЯ РАБОТА №2 ===");
            Console.WriteLine();

            try
            {
                // Ручной ввод
                ManualInputDemo();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Критическая ошибка: {ex.Message}");
            }

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static void ManualInputDemo()
        {
            Console.WriteLine("=== РУЧНОЙ ВВОД ДАННЫХ ===");
            
            try
            {
                while (true)
                {
                    Console.WriteLine("\n=== ВЫБЕРИТЕ ДЕЙСТВИЕ ===");
                    Console.WriteLine("1 - Создать имя");
                    Console.WriteLine("2 - Создать человека");
                    Console.WriteLine("3 - Создать ломаную линию");
                    Console.WriteLine("4 - Создать семейные связи");
                    Console.WriteLine("5 - Выход");
                    Console.Write("Ваш выбор: ");

                    string choice = Console.ReadLine();
                    
                    switch (choice)
                    {
                        case "1":
                            CreateNameManually();
                            break;
                        case "2":
                            CreatePersonManually();
                            break;
                        case "3":
                            CreateAndManagePolyline();
                            break;
                        case "4":
                            CreateFamilyRelationsManually();
                            break;
                        case "5":
                            Console.WriteLine("Выход из программы...");
                        {
                            return;
                        }
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }
            }
            catch (ValidationException ex)
            {
                Console.WriteLine($"Ошибка валидации: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void CreateNameManually()
        {
            Console.WriteLine("\n--- Создание имени ---");
            
            Console.Write("Фамилия (необязательно): ");
            string lastName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lastName)) lastName = null;

            Console.Write("Имя (обязательно): ");
            string firstName = Console.ReadLine();

            Console.Write("Отчество (необязательно): ");
            string middleName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(middleName)) middleName = null;

            Name name = new Name(lastName, firstName, middleName);
            Console.WriteLine($"✅ Создано имя: {name}");
        }

        static void CreatePersonManually()
        {
            Console.WriteLine("\n--- Создание человека ---");
            
            Console.WriteLine("Введите данные для имени:");
            Console.Write("Фамилия (необязательно): ");
            string lastName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lastName)) lastName = null;

            Console.Write("Имя (обязательно): ");
            string firstName = Console.ReadLine();

            Console.Write("Отчество (необязательно): ");
            string middleName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(middleName)) middleName = null;

            Name name = new Name(lastName, firstName, middleName);

            Console.Write("Рост (см, от 30 до 300): ");
            double height = GetValidatedDoubleInput(30, 300);

            Console.Write("Пол (мужской/женский): ");
            string gender = GetValidatedGenderInput();

            Person person = new Person(name, height, gender);
            Console.WriteLine($"✅ Создан человек: {person}");
        }

        static void CreateAndManagePolyline()
        {
            Console.WriteLine("\n--- Создание и управление ломаной линией ---");
            
            Polyline polyline = CreatePolylineManually();
            Console.WriteLine($"✅ Создана ломаная: {polyline}");
            Console.WriteLine($"📏 Длина ломаной: {polyline.GetLength():F2}");

            // Меню управления ломаной
            while (true)
            {
                Console.WriteLine("\n--- УПРАВЛЕНИЕ ЛОМАНОЙ ---");
                Console.WriteLine("1 - Добавить точки");
                Console.WriteLine("2 - Сдвинуть ломаную");
                Console.WriteLine("3 - Показать информацию");
                Console.WriteLine("4 - Вернуться в главное меню");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        AddPointsToPolylineManually(polyline);
                        Console.WriteLine($"✅ Ломаная после добавления: {polyline}");
                        Console.WriteLine($"📏 Новая длина: {polyline.GetLength():F2}");
                        break;
                    case "2":
                        ShiftPolylineManually(polyline);
                        Console.WriteLine($"✅ Ломаная после сдвига: {polyline}");
                        Console.WriteLine($"📏 Длина после сдвига: {polyline.GetLength():F2}");
                        break;
                    case "3":
                        Console.WriteLine($"📐 Ломаная: {polyline}");
                        Console.WriteLine($"📏 Длина: {polyline.GetLength():F2}");
                        Console.WriteLine($"🔢 Количество точек: {polyline.Points.Count}");
                        break;
                    case "4":
                    {
                        return;
                    }
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static Polyline CreatePolylineManually()
        {
            Console.Write("Введите количество точек для ломаной (минимум 2): ");
            int pointCount = GetValidatedIntInput(2, 10);

            List<Point> points = new List<Point>();
            for (int i = 0; i < pointCount; i++)
            {
                Console.WriteLine($"Точка {i + 1}:");
                Console.Write("  X = ");
                double x = GetValidatedDoubleInput(-1000, 1000);
                Console.Write("  Y = ");
                double y = GetValidatedDoubleInput(-1000, 1000);
                
                points.Add(new Point(x, y));
            }

            {
                return new Polyline(points);
            }
        }

        static void AddPointsToPolylineManually(Polyline polyline)
        {
            Console.Write("Сколько точек хотите добавить? ");
            int addCount = GetValidatedIntInput(1, 5);

            List<Point> newPoints = new List<Point>();
            for (int i = 0; i < addCount; i++)
            {
                Console.WriteLine($"Добавляемая точка {i + 1}:");
                Console.Write("  X = ");
                double x = GetValidatedDoubleInput(-1000, 1000);
                Console.Write("  Y = ");
                double y = GetValidatedDoubleInput(-1000, 1000);
                
                newPoints.Add(new Point(x, y));
            }

            polyline.AddPoints(newPoints.ToArray());
            Console.WriteLine($"✅ Добавлено {addCount} точек");
        }

        static void ShiftPolylineManually(Polyline polyline)
        {
            Console.Write("Введите сдвиг по X: ");
            double deltaX = GetValidatedDoubleInput(-100, 100);

            Console.Write("Введите сдвиг по Y: ");
            double deltaY = GetValidatedDoubleInput(-100, 100);

            polyline.Shift(deltaX, deltaY);
            Console.WriteLine($"✅ Ломаная сдвинута на ({deltaX}, {deltaY})");
        }

        static void CreateFamilyRelationsManually()
        {
            Console.WriteLine("\n--- Создание семейных связей ---");
            Console.WriteLine("Создадим отца и ребенка с автоматическим наследованием фамилии и отчества:");

            Console.WriteLine("\n--- Данные отца ---");
            Console.Write("Фамилия отца: ");
            string fatherLastName = Console.ReadLine();

            Console.Write("Имя отца: ");
            string fatherFirstName = Console.ReadLine();

            Console.Write("Отчество отца (необязательно): ");
            string fatherMiddleName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(fatherMiddleName)) fatherMiddleName = null;

            Console.Write("Рост отца: ");
            double fatherHeight = GetValidatedDoubleInput(30, 300);

            Name fatherName = new Name(fatherLastName, fatherFirstName, fatherMiddleName);
            Person father = new Person(fatherName, fatherHeight, "мужской");

            Console.WriteLine("\n--- Данные ребенка ---");
            Console.WriteLine("Для демонстрации наследования у ребенка не указывайте фамилию и отчество");
            
            Console.Write("Имя ребенка: ");
            string childFirstName = Console.ReadLine();
            
            Console.Write("Рост ребенка: ");
            double childHeight = GetValidatedDoubleInput(30, 300);

            Console.Write("Пол ребенка (мужской/женский): ");
            string childGender = GetValidatedGenderInput();

            Name childName = new Name(null, childFirstName, null);
            Person child = new Person(childName, childHeight, childGender);
            child.Father = father;

            Console.WriteLine($"\n📋 РЕЗУЛЬТАТ:");
            Console.WriteLine($"👨 Отец: {father}");
            Console.WriteLine($"👶 Ребенок: {child}");
            Console.WriteLine($"🔗 Связь: автоматически унаследованы фамилия '{child.PersonName.LastName}' и отчество '{child.PersonName.MiddleName}'");
        }

        static int GetValidatedIntInput(int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                try
                {
                    if (int.TryParse(Console.ReadLine(), out int result))
                    {
                        var validator = new Validator()
                            .ValidateInt(result, "Ввод", min, max, required: true);

                        if (validator.IsValid)
                        {
                            return result;
                        }
                        else
                            Console.Write($"{string.Join("; ", validator.Errors)}. Попробуйте снова: ");
                    }
                    else
                    {
                        Console.Write("Введите корректное целое число: ");
                    }
                }
                catch (Exception ex)
                {
                    Console.Write($"Ошибка: {ex.Message}. Попробуйте снова: ");
                }
            }
        }

        static double GetValidatedDoubleInput(double min = -1000, double max = 1000)
        {
            while (true)
            {
                try
                {
                    if (double.TryParse(Console.ReadLine(), out double result))
                    {
                        var validator = new Validator()
                            .ValidateDouble(result, "Ввод", min, max, required: true);

                        if (validator.IsValid)
                        {
                            return result;
                        }
                        else
                            Console.Write($"{string.Join("; ", validator.Errors)}. Попробуйте снова: ");
                    }
                    else
                    {
                        Console.Write("Введите корректное число: ");
                    }
                }
                catch (Exception ex)
                {
                    Console.Write($"Ошибка: {ex.Message}. Попробуйте снова: ");
                }
            }
        }

        static string GetValidatedGenderInput()
        {
            while (true)
            {
                string input = Console.ReadLine()?.ToLower().Trim();

                if (input == "мужской" || input == "м")
                {
                    return "мужской";
                }

                if (input == "женский" || input == "ж")
                {
                    return "женский";
                }
                else
                    Console.Write("Введите 'мужской' или 'женский': ");
            }
        }
    }
}