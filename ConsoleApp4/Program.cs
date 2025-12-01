using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task6_TaxiFleet
{
    class TaxiCar
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public string Status { get; set; }
        public string Driver { get; set; }

        public TaxiCar(string brand, string model, int year, int mileage, string driver)
        {
            Brand = brand;
            Model = model;
            Year = year;
            Mileage = mileage;
            Status = "в работе";
            Driver = driver;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"\n--- Автомобиль ---");
            Console.WriteLine($"Марка: {Brand}");
            Console.WriteLine($"Модель: {Model}");
            Console.WriteLine($"Год выпуска: {Year}");
            Console.WriteLine($"Пробег: {Mileage:N0} км");
            Console.WriteLine($"Статус: {Status}");
            Console.WriteLine($"Водитель: {Driver}");
        }

        public void SetStatus(string newStatus)
        {
            if (newStatus == "в работе" || newStatus == "на ремонте")
            {
                Status = newStatus;
                Console.WriteLine($"Статус автомобиля {Brand} {Model} изменён на '{newStatus}'");
            }
            else
            {
                Console.WriteLine("Неверный статус! Используйте: 'в работе' или 'на ремонте'");
            }
        }

        public void UpdateMileage(int additionalKm)
        {
            if (additionalKm > 0)
            {
                Mileage += additionalKm;
                Console.WriteLine($"Пробег обновлён. Добавлено {additionalKm} км. " +
                                $"Текущий пробег: {Mileage:N0} км");
            }
            else
            {
                Console.WriteLine("Пробег должен быть положительным числом!");
            }
        }
    }

    class Program
    {
        static List<TaxiCar> taxiFleet = new List<TaxiCar>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== ТАКСОПАРК ===");
                Console.WriteLine("1. Добавить автомобиль");
                Console.WriteLine("2. Показать все автомобили");
                Console.WriteLine("3. Изменить статус автомобиля");
                Console.WriteLine("4. Обновить пробег");
                Console.WriteLine("5. Поиск по водителю");
                Console.WriteLine("6. Показать автомобили на ремонте");
                Console.WriteLine("7. Статистика таксопарка");
                Console.WriteLine("8. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCar();
                        break;
                    case "2":
                        ShowAllCars();
                        break;
                    case "3":
                        ChangeCarStatus();
                        break;
                    case "4":
                        UpdateCarMileage();
                        break;
                    case "5":
                        SearchByDriver();
                        break;
                    case "6":
                        ShowCarsInRepair();
                        break;
                    case "7":
                        ShowStatistics();
                        break;
                    case "8":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }

        static void AddCar()
        {
            Console.Write("\nВведите марку автомобиля: ");
            string brand = Console.ReadLine();

            Console.Write("Введите модель: ");
            string model = Console.ReadLine();

            Console.Write("Введите год выпуска: ");
            int year = int.Parse(Console.ReadLine());

            Console.Write("Введите текущий пробег (км): ");
            int mileage = int.Parse(Console.ReadLine());

            Console.Write("Введите ФИО водителя: ");
            string driver = Console.ReadLine();

            TaxiCar car = new TaxiCar(brand, model, year, mileage, driver);
            taxiFleet.Add(car);
            Console.WriteLine("Автомобиль успешно добавлен в таксопарк!");
        }

        static void ShowAllCars()
        {
            if (taxiFleet.Count == 0)
            {
                Console.WriteLine("\nТаксопарк пуст.");
                return;
            }

            Console.WriteLine($"\n=== Всего автомобилей: {taxiFleet.Count} ===");
            foreach (var car in taxiFleet)
            {
                car.DisplayInfo();
            }
        }

        static void ChangeCarStatus()
        {
            Console.Write("\nВведите марку автомобиля: ");
            string brand = Console.ReadLine();

            Console.Write("Введите модель автомобиля: ");
            string model = Console.ReadLine();

            TaxiCar car = taxiFleet.Find(c => c.Brand.ToLower() == brand.ToLower() &&
                                             c.Model.ToLower() == model.ToLower());

            if (car != null)
            {
                Console.WriteLine($"Текущий статус: {car.Status}");
                Console.Write("Введите новый статус (в работе/на ремонте): ");
                string newStatus = Console.ReadLine();
                car.SetStatus(newStatus);
            }
            else
            {
                Console.WriteLine("Автомобиль не найден!");
            }
        }

        static void UpdateCarMileage()
        {
            Console.Write("\nВведите марку автомобиля: ");
            string brand = Console.ReadLine();

            Console.Write("Введите модель автомобиля: ");
            string model = Console.ReadLine();

            TaxiCar car = taxiFleet.Find(c => c.Brand.ToLower() == brand.ToLower() &&
                                             c.Model.ToLower() == model.ToLower());

            if (car != null)
            {
                Console.WriteLine($"Текущий пробег: {car.Mileage:N0} км");
                Console.Write("Введите количество добавляемых километров: ");
                int additionalKm = int.Parse(Console.ReadLine());
                car.UpdateMileage(additionalKm);
            }
            else
            {
                Console.WriteLine("Автомобиль не найден!");
            }
        }

        static void SearchByDriver()
        {
            Console.Write("\nВведите ФИО водителя: ");
            string driver = Console.ReadLine();

            var foundCars = taxiFleet.Where(c => c.Driver.ToLower()
                                                .Contains(driver.ToLower())).ToList();

            if (foundCars.Count > 0)
            {
                Console.WriteLine($"\nНайдено автомобилей: {foundCars.Count}");
                foreach (var car in foundCars)
                {
                    car.DisplayInfo();
                }
            }
            else
            {
                Console.WriteLine("Автомобили этого водителя не найдены!");
            }
        }

        static void ShowCarsInRepair()
        {
            var carsInRepair = taxiFleet.Where(c => c.Status == "на ремонте").ToList();

            if (carsInRepair.Count > 0)
            {
                Console.WriteLine($"\n🔧 Автомобили на ремонте: {carsInRepair.Count}");
                foreach (var car in carsInRepair)
                {
                    car.DisplayInfo();
                }
            }
            else
            {
                Console.WriteLine("\n✓ Все автомобили в работе!");
            }
        }

        static void ShowStatistics()
        {
            if (taxiFleet.Count == 0)
            {
                Console.WriteLine("\nНет данных для статистики.");
                return;
            }

            int working = taxiFleet.Count(c => c.Status == "в работе");
            int inRepair = taxiFleet.Count(c => c.Status == "на ремонте");
            double averageMileage = taxiFleet.Average(c => c.Mileage);
            int totalMileage = taxiFleet.Sum(c => c.Mileage);
            var oldestCar = taxiFleet.OrderBy(c => c.Year).First();
            var newestCar = taxiFleet.OrderByDescending(c => c.Year).First();

            Console.WriteLine("\n=== СТАТИСТИКА ТАКСОПАРКА ===");
            Console.WriteLine($"Всего автомобилей: {taxiFleet.Count}");
            Console.WriteLine($"В работе: {working}");
            Console.WriteLine($"На ремонте: {inRepair}");
            Console.WriteLine($"Средний пробег: {averageMileage:N0} км");
            Console.WriteLine($"Общий пробег парка: {totalMileage:N0} км");
            Console.WriteLine($"Самый старый автомобиль: {oldestCar.Brand} {oldestCar.Model} ({oldestCar.Year})");
            Console.WriteLine($"Самый новый автомобиль: {newestCar.Brand} {newestCar.Model} ({newestCar.Year})");

            var brandGroups = taxiFleet.GroupBy(c => c.Brand);
            Console.WriteLine("\nПо маркам:");
            foreach (var group in brandGroups)
            {
                Console.WriteLine($"  {group.Key}: {group.Count()} шт.");
            }
        }
    }
}
