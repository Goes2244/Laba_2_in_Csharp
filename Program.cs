using System;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЛАБОРАТОРНАЯ РАБОТА №2 - ВАРИАНТ 10 ===\n");

            while (true)
            {
                ShowMenu();
                int choice = Validator.GetValidatedInt("Выбор меню", 0, 6);
                
                if (choice == 0) break;
                
                ExecuteTask(choice);
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
            
            Console.WriteLine("Программа завершена. До свидания!");
        }

        static void ShowMenu()
        {
            Console.WriteLine("=== ГЛАВНОЕ МЕНЮ ===");
            Console.WriteLine("1. Задание 1.3 - Имена");
            Console.WriteLine("2. Задание 2.2 - Человек с именем");
            Console.WriteLine("3. Задание 2.3 - Человек с родителем");
            Console.WriteLine("4. Задание 3.2 - Ломаная");
            Console.WriteLine("5. Задание 4.9 - Ломаная (расширенная)");
            Console.WriteLine("6. Задание 5.7 - Длина Ломаной");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите задание: ");
        }

        static void ExecuteTask(int taskNumber)
        {
            Console.WriteLine($"=== ВЫПОЛНЕНИЕ ЗАДАНИЯ {taskNumber} ===\n");

            switch (taskNumber)
            {
                case 1: Task1_3(); break;
                case 2: Task2_2(); break;
                case 3: Task2_3(); break;
                case 4: Task3_2(); break;
                case 5: Task4_9(); break;
                case 6: Task5_7(); break;
            }
        }

        static void Task1_3()
        {
            Console.WriteLine("ЗАДАНИЕ 1.3 - СОЗДАНИЕ ИМЕН\n");
            Console.WriteLine("Создайте имя человека. Можно ввести фамилию, имя и отчество.");
            Console.WriteLine("Если какие-то части имени отсутствуют - просто нажмите Enter.\n");

            Console.Write("Фамилия (или Enter чтобы пропустить): ");
            string lastName = Console.ReadLine();
            
            Console.Write("Имя: ");
            string firstName = Validator.GetValidatedNamePart("Имя", true);
            
            Console.Write("Отчество (или Enter чтобы пропустить): ");
            string middleName = Console.ReadLine();

            Name name;
            if (string.IsNullOrWhiteSpace(lastName) && string.IsNullOrWhiteSpace(middleName))
                name = new Name(firstName);
            else if (string.IsNullOrWhiteSpace(middleName))
                name = new Name(lastName, firstName);
            else
                name = new Name(lastName, firstName, middleName);

            Console.WriteLine($"\nРезультат: {name}");
        }

        static void Task2_2()
        {
            Console.WriteLine("ЗАДАНИЕ 2.2 - ЧЕЛОВЕК С ИМЕНЕМ\n");
            Console.WriteLine("Создайте человека, указав фамилию, имя, отчество и рост.\n");
            Console.WriteLine("Если какие-то части имени отсутствуют - просто нажмите Enter.\n");

            Console.Write("Фамилия (или Enter чтобы пропустить): ");
            string lastName = Console.ReadLine();
            
            Console.Write("Имя: ");
            string firstName = Validator.GetValidatedNamePart("Имя", true);
            
            Console.Write("Отчество (или Enter чтобы пропустить): ");
            string middleName = Console.ReadLine();

            Console.Write("Рост (см): ");
            int height = Validator.GetValidatedHeight();

            Name name;
            if (string.IsNullOrWhiteSpace(lastName) && string.IsNullOrWhiteSpace(middleName))
                name = new Name(firstName);
            else if (string.IsNullOrWhiteSpace(middleName))
                name = new Name(lastName, firstName);
            else
                name = new Name(lastName, firstName, middleName);

            var person = new Person(name, height);

            Console.WriteLine($"\nРезультат: {person}");
        }

        static void Task2_3()
        {
            Console.WriteLine("ЗАДАНИЕ 2.3 - СОЗДАНИЕ СЕМЬИ С НАСЛЕДОВАНИЕМ\n");
            Console.WriteLine("Создадим семью из трех человек и установим родственные связи.\n");

            Person[] people = new Person[3];

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"=== СОЗДАНИЕ ЧЕЛОВЕКА {i + 1} ===");
                
                string lastName;
                while (true)
                {
                    Console.Write("Введите фамилию: ");
                    lastName = Console.ReadLine();
                    if (string.IsNullOrEmpty(lastName)) break;
                    
                    try
                    {
                        Validator.Validate(v => v.ValidateNamePart(lastName, "Фамилия", false));
                        break;
                    }
                    catch (ValidationException ex)
                    {
                        Console.WriteLine($"{ex.Message}. Введите снова.");
                    }
                }
                
                Console.Write("Введите имя: ");
                string firstName = Validator.GetValidatedNamePart("Имя", true);
                
                string middleName;
                while (true)
                {
                    Console.Write("Введите отчество (или Enter чтобы пропустить): ");
                    middleName = Console.ReadLine();
                    if (string.IsNullOrEmpty(middleName)) break;
                    
                    try
                    {
                        Validator.Validate(v => v.ValidateNamePart(middleName, "Отчество", false));
                        break;
                    }
                    catch (ValidationException ex)
                    {
                        Console.WriteLine($"{ex.Message}. Введите снова.");
                    }
                }

                Console.Write("Введите рост (см): ");
                int height = Validator.GetValidatedHeight();

                Name name;
                try
                {
                    if (string.IsNullOrWhiteSpace(lastName) && string.IsNullOrWhiteSpace(middleName))
                        name = new Name(firstName);
                    else if (string.IsNullOrWhiteSpace(middleName))
                        name = new Name(lastName, firstName);
                    else
                        name = new Name(lastName, firstName, middleName);

                    people[i] = new Person(name, height);
                    Console.WriteLine($"Создан: {people[i]}\n");
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine($"Ошибка при создании человека: {ex.Message}");
                    i--; // Повторить создание этого человека
                    Console.WriteLine();
                }
            }

            Console.WriteLine("=== УСТАНОВКА РОДСТВЕННЫХ СВЯЗЕЙ ===");
            Console.WriteLine("Теперь укажите, кто является отцом для кого:\n");

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{i + 1}. {people[i]}");
            }

            Console.WriteLine("\nУкажите отца для каждого человека (введите номер от 1 до 3):");
            Console.WriteLine("Если отец не указан - введите 0\n");

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Кто является отцом для {people[i].PersonName}?");
                Console.Write("Номер отца: ");
                int fatherIndex = Validator.GetValidatedInt("Номер отца", 0, 3, false);
                
                if (fatherIndex >= 1 && fatherIndex <= 3)
                {
                    if (fatherIndex - 1 != i)
                    {
                        people[i].Father = people[fatherIndex - 1];
                        people[i].ApplyInheritanceRules();
                        Console.WriteLine($"Установлен отец: {people[fatherIndex - 1].PersonName}");
                    }
                    else
                    {
                        Console.WriteLine("Человек не может быть отцом самому себе!");
                    }
                }
                else
                {
                    Console.WriteLine("Отец не указан");
                }
                Console.WriteLine();
            }

            Console.WriteLine("=== РЕЗУЛЬТАТ ===");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{i + 1}. {people[i]}");
                if (people[i].Father != null)
                {
                    Console.WriteLine($"   Отец: {people[i].Father.PersonName}");
                }
                else
                {
                    Console.WriteLine($"   Отец: не указан");
                }
                Console.WriteLine();
            }
        }

        static void Task3_2()
        {
            Console.WriteLine("ЗАДАНИЕ 3.2 - ЛОМАНАЯ ЛИНИЯ\n");
            Console.WriteLine("Необходимо выполнить следующие задачи:");
            Console.WriteLine("1. Создать первую ломаную");
            Console.WriteLine("2. Создать вторую ломаную с совпадающими первой и последней точками");
            Console.WriteLine("3. Сдвинуть начало первой ломаной так, чтобы сдвинулось начало второй\n");

            Console.WriteLine("=== СОЗДАНИЕ ПЕРВОЙ ЛОМАНОЙ ===");
            var polyline1 = new Polyline();
            
            Console.Write("Сколько точек будет в первой ломаной? ");
            int pointCount1 = Validator.GetValidatedInt("Количество точек в первой ломаной", 2, 10);
            
            for (int i = 0; i < pointCount1; i++)
            {
                Console.WriteLine($"\nТочка {i + 1}:");
                Console.Write("X = ");
                double x = Validator.GetValidatedCoordinate("X");
                Console.Write("Y = ");
                double y = Validator.GetValidatedCoordinate("Y");
                polyline1.AddPoint(new Point(x, y));
            }

            Console.WriteLine($"\nПервая ломаная: {polyline1}");
            Console.WriteLine($"Длина: {polyline1.CalculateLength():F2}\n");

            Console.WriteLine("=== СОЗДАНИЕ ВТОРОЙ ЛОМАНОЙ ===");
            Point firstPoint = polyline1.Points[0];
            Point lastPoint = polyline1.Points[^1];
            
            var polyline2 = new Polyline();
            polyline2.AddPoint(firstPoint);

            Console.Write("Сколько средних точек будет во второй ломаной? ");
            int middlePointCount = Validator.GetValidatedInt("Количество средних точек во второй ломаной", 1, 10);
            
            for (int i = 0; i < middlePointCount; i++)
            {
                Console.WriteLine($"\nСредняя точка {i + 1}:");
                Console.Write("X = ");
                double x = Validator.GetValidatedCoordinate("X");
                Console.Write("Y = ");
                double y = Validator.GetValidatedCoordinate("Y");
                polyline2.AddPoint(new Point(x, y));
            }

            polyline2.AddPoint(lastPoint);

            Console.WriteLine($"\nВторая ломаная: {polyline2}");
            Console.WriteLine($"Длина: {polyline2.CalculateLength():F2}\n");

            Console.WriteLine("=== СДВИГ НАЧАЛА ПЕРВОЙ ЛОМАНОЙ ===");
            Console.WriteLine($"Текущее начало первой ломаной: {polyline1.Points[0]}");
            Console.WriteLine($"Текущее начало второй ломаной: {polyline2.Points[0]}");

            Console.Write("Введите сдвиг по X: ");
            double shiftX = Validator.GetValidatedShift("Сдвиг по X");
            Console.Write("Введите сдвиг по Y: ");
            double shiftY = Validator.GetValidatedShift("Сдвиг по Y");

            polyline1.Points[0].X += shiftX;
            polyline1.Points[0].Y += shiftY;

            Console.WriteLine($"\nПосле сдвига:");
            Console.WriteLine($"Начало первой ломаной: {polyline1.Points[0]}");
            Console.WriteLine($"Начало второй ломаной: {polyline2.Points[0]}");

            if (polyline1.Points[0].X == polyline2.Points[0].X && 
                polyline1.Points[0].Y == polyline2.Points[0].Y)
            {
                Console.WriteLine("\n✓ УСПЕХ: Начало второй ломаной сдвинулось вместе с первой!");
            }
            else
            {
                Console.WriteLine("\n✗ ОШИБКА: Начала ломаных не совпадают!");
            }

            Console.WriteLine($"\nНовая длина первой ломаной: {polyline1.CalculateLength():F2}");
            Console.WriteLine($"Новая длина второй ломаной: {polyline2.CalculateLength():F2}");
        }

        static void Task4_9()
        {
            Console.WriteLine("ЗАДАНИЕ 4.9 - РАСШИРЕННЫЕ ВОЗМОЖНОСТИ ЛОМАНОЙ\n");
            Console.WriteLine("Демонстрация разных способов создания ломаной линии.\n");

            Console.WriteLine("Выберите способ создания ломаной:");
            Console.WriteLine("1. Создать пустую ломаную и добавить точки");
            Console.WriteLine("2. Создать ломаную с начальными точками");
            Console.Write("Выберите: ");
            int choice = Validator.GetValidatedInt("Выбор способа", 1, 2);

            Polyline polyline;

            if (choice == 1)
            {
                polyline = new Polyline();
                Console.WriteLine("\nСоздана пустая ломаная.");
                
                Console.Write("Сколько точек добавить? ");
                int addCount = Validator.GetValidatedInt("Количество точек для добавления", 1, 10);

                for (int i = 0; i < addCount; i++)
                {
                    Console.WriteLine($"\nТочка {i + 1}:");
                    Console.Write("X: ");
                    double x = Validator.GetValidatedCoordinate("X");
                    Console.Write("Y: ");
                    double y = Validator.GetValidatedCoordinate("Y");
                    polyline.AddPoint(new Point(x, y));
                }
            }
            else
            {
                Console.Write("\nСколько начальных точек? ");
                int initialCount = Validator.GetValidatedInt("Количество начальных точек", 2, 10);

                var points = new Point[initialCount];
                for (int i = 0; i < initialCount; i++)
                {
                    Console.WriteLine($"\nТочка {i + 1}:");
                    Console.Write("X: ");
                    double x = Validator.GetValidatedCoordinate("X");
                    Console.Write("Y: ");
                    double y = Validator.GetValidatedCoordinate("Y");
                    points[i] = new Point(x, y);
                }

                polyline = new Polyline(points);
            }

            Console.WriteLine($"\nЛоманая: {polyline}");
        }

        static void Task5_7()
        {
            Console.WriteLine("ЗАДАНИЕ 5.7 - РАСЧЕТ ДЛИНЫ ЛОМАНОЙ ЛИНИИ\n");
            Console.WriteLine("Создадим ломаную и рассчитаем её длину, затем добавим точки и пересчитаем.\n");

            var polyline = new Polyline();
            
            Console.WriteLine("Создание начальной ломаной:");
            Console.Write("Сколько точек в начальной ломаной? ");
            int initialCount = Validator.GetValidatedInt("Количество точек в начальной ломаной", 2, 10);

            for (int i = 0; i < initialCount; i++)
            {
                Console.WriteLine($"\nТочка {i + 1}:");
                Console.Write("X: ");
                double x = Validator.GetValidatedCoordinate("X");
                Console.Write("Y: ");
                double y = Validator.GetValidatedCoordinate("Y");
                polyline.AddPoint(new Point(x, y));
            }

            double initialLength = polyline.CalculateLength();
            Console.WriteLine($"\nНачальная длина: {initialLength:F2}");

            Console.WriteLine($"\nДобавление новых точек:");
            Console.Write("Сколько точек добавить? ");
            int addCount = Validator.GetValidatedInt("Количество точек для добавления", 0, 10);

            for (int i = 0; i < addCount; i++)
            {
                Console.WriteLine($"\nНовая точка {i + 1}:");
                Console.Write("X: ");
                double x = Validator.GetValidatedCoordinate("X");
                Console.Write("Y: ");
                double y = Validator.GetValidatedCoordinate("Y");
                polyline.AddPoint(new Point(x, y));
                Console.WriteLine($"Точка добавлена. Текущее количество точек: {polyline.Points.Count}");
            }

            double finalLength = polyline.CalculateLength();
            Console.WriteLine($"\nИтоговая длина: {finalLength:F2}");
            Console.WriteLine($"Увеличение длины: {finalLength - initialLength:F2}");
        }
    }
}
