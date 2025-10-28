namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
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
            Console.Write("Фамилия (или Enter чтобы пропустить): ");
            string lastName = Console.ReadLine();
            
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

            Console.WriteLine($"Результат: {name}");
        }

        static void Task2_2()
        {
            string firstName = Validator.GetValidatedNamePart("Имя человека", true);
            int height = Validator.GetValidatedHeight();

            var name = new Name(firstName);
            var person = new Person(name, height);

            Console.WriteLine($"Результат: {person}");
        }

        static void Task2_3()
        {
            Person[] people = new Person[3];

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"=== СОЗДАНИЕ ЧЕЛОВЕКА {i + 1} ===");
                
                Console.Write("Введите фамилию: ");
                string lastName = Console.ReadLine();
                
                string firstName = Validator.GetValidatedNamePart("Имя", true);
                
                Console.Write("Введите отчество (или Enter чтобы пропустить): ");
                string middleName = Console.ReadLine();

                int height = Validator.GetValidatedHeight();

                Name name;
                if (string.IsNullOrWhiteSpace(lastName) && string.IsNullOrWhiteSpace(middleName))
                    name = new Name(firstName);
                else if (string.IsNullOrWhiteSpace(middleName))
                    name = new Name(lastName, firstName);
                else
                    name = new Name(lastName, firstName, middleName);

                people[i] = new Person(name, height);
                Console.WriteLine($"Создан: {people[i]}\n");
            }

            Console.WriteLine("=== УСТАНОВКА РОДСТВЕННЫХ СВЯЗЕЙ ===");

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{i + 1}. {people[i]}");
            }

            Console.WriteLine("\nУкажите отца для каждого человека:");

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Кто является отцом для {people[i].PersonName}?");
                int fatherIndex = Validator.GetValidatedInt("Номер отца", 0, 3);
                
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
                Console.WriteLine();
            }
        }

        static void Task3_2()
        {
            var polyline1 = new Polyline();
            
            int pointCount1 = Validator.GetValidatedInt("Количество точек в первой ломаной", 2, 10);
            
            for (int i = 0; i < pointCount1; i++)
            {
                Console.WriteLine($"Точка {i + 1}:");
                double x = Validator.GetValidatedCoordinate("X");
                double y = Validator.GetValidatedCoordinate("Y");
                polyline1.AddPoint(new Point(x, y));
            }

            Console.WriteLine($"Первая ломаная: {polyline1}");
            Console.WriteLine($"Длина: {polyline1.CalculateLength():F2}\n");

            Point firstPoint = polyline1.Points[0];
            Point lastPoint = polyline1.Points[^1];
            
            var polyline2 = new Polyline();
            polyline2.AddPoint(firstPoint);

            int middlePointCount = Validator.GetValidatedInt("Количество средних точек во второй ломаной", 1, 10);
            
            for (int i = 0; i < middlePointCount; i++)
            {
                Console.WriteLine($"Средняя точка {i + 1}:");
                double x = Validator.GetValidatedCoordinate("X");
                double y = Validator.GetValidatedCoordinate("Y");
                polyline2.AddPoint(new Point(x, y));
            }

            polyline2.AddPoint(lastPoint);

            Console.WriteLine($"Вторая ломаная: {polyline2}");
            Console.WriteLine($"Длина: {polyline2.CalculateLength():F2}\n");

            Console.WriteLine($"Начало первой ломаной: {polyline1.Points[0]}");
            Console.WriteLine($"Начало второй ломаной: {polyline2.Points[0]}");

            double shiftX = Validator.GetValidatedShift("Сдвиг по X");
            double shiftY = Validator.GetValidatedShift("Сдвиг по Y");

            polyline1.Points[0].X += shiftX;
            polyline1.Points[0].Y += shiftY;

            Console.WriteLine($"После сдвига:");
            Console.WriteLine($"Начало первой ломаной: {polyline1.Points[0]}");
            Console.WriteLine($"Начало второй ломаной: {polyline2.Points[0]}");

            if (polyline1.Points[0].X == polyline2.Points[0].X && 
                polyline1.Points[0].Y == polyline2.Points[0].Y)
            {
                Console.WriteLine("УСПЕХ: Начало второй ломаной сдвинулось вместе с первой!");
            }

            Console.WriteLine($"Новая длина первой ломаной: {polyline1.CalculateLength():F2}");
            Console.WriteLine($"Новая длина второй ломаной: {polyline2.CalculateLength():F2}");
        }

        static void Task4_9()
        {
            Console.WriteLine("1. Создать пустую ломаную и добавить точки");
            Console.WriteLine("2. Создать ломаную с начальными точками");
            int choice = Validator.GetValidatedInt("Выбор способа", 1, 2);

            Polyline polyline;

            if (choice == 1)
            {
                polyline = new Polyline();
                int addCount = Validator.GetValidatedInt("Количество точек для добавления", 1, 10);

                for (int i = 0; i < addCount; i++)
                {
                    Console.WriteLine($"Точка {i + 1}:");
                    double x = Validator.GetValidatedCoordinate("X");
                    double y = Validator.GetValidatedCoordinate("Y");
                    polyline.AddPoint(new Point(x, y));
                }
            }
            else
            {
                int initialCount = Validator.GetValidatedInt("Количество начальных точек", 2, 10);
                var points = new Point[initialCount];
                
                for (int i = 0; i < initialCount; i++)
                {
                    Console.WriteLine($"Точка {i + 1}:");
                    double x = Validator.GetValidatedCoordinate("X");
                    double y = Validator.GetValidatedCoordinate("Y");
                    points[i] = new Point(x, y);
                }

                polyline = new Polyline(points);
            }

            Console.WriteLine($"Ломаная: {polyline}");
        }

        static void Task5_7()
        {
            var polyline = new Polyline();
            
            int initialCount = Validator.GetValidatedInt("Количество точек в начальной ломаной", 2, 10);

            for (int i = 0; i < initialCount; i++)
            {
                Console.WriteLine($"Точка {i + 1}:");
                double x = Validator.GetValidatedCoordinate("X");
                double y = Validator.GetValidatedCoordinate("Y");
                polyline.AddPoint(new Point(x, y));
            }

            double initialLength = polyline.CalculateLength();
            Console.WriteLine($"Начальная длина: {initialLength:F2}");

            int addCount = Validator.GetValidatedInt("Количество точек для добавления", 0, 10);

            for (int i = 0; i < addCount; i++)
            {
                Console.WriteLine($"Новая точка {i + 1}:");
                double x = Validator.GetValidatedCoordinate("X");
                double y = Validator.GetValidatedCoordinate("Y");
                polyline.AddPoint(new Point(x, y));
            }

            double finalLength = polyline.CalculateLength();
            Console.WriteLine($"Итоговая длина: {finalLength:F2}");
            Console.WriteLine($"Увеличение: {finalLength - initialLength:F2}");
        }
    }
}