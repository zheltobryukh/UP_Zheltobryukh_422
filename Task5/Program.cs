using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task5_AnimalShelter
{
    class ShelterAnimal
    {
        public string Nickname { get; set; }
        public string AnimalType { get; set; }
        public int Age { get; set; }
        public bool HasVaccinations { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Status { get; set; }

        public ShelterAnimal(string nickname, string animalType, int age,
                            bool hasVaccinations, DateTime arrivalDate)
        {
            Nickname = nickname;
            AnimalType = animalType;
            Age = age;
            HasVaccinations = hasVaccinations;
            ArrivalDate = arrivalDate;
            Status = "в приюте";
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"\n--- Животное ---");
            Console.WriteLine($"Кличка: {Nickname}");
            Console.WriteLine($"Вид: {AnimalType}");
            Console.WriteLine($"Возраст: {Age} лет");
            Console.WriteLine($"Прививки: {(HasVaccinations ? "Есть" : "Нет")}");
            Console.WriteLine($"Дата поступления: {ArrivalDate.ToShortDateString()}");
            Console.WriteLine($"Статус: {Status}");
            Console.WriteLine($"Дней в приюте: {(DateTime.Now - ArrivalDate).Days}");
        }

        public void AdoptAnimal()
        {
            Status = "забрали домой";
            Console.WriteLine($"🏠 {Nickname} нашёл(а) новый дом!");
        }
    }

    class Program
    {
        static List<ShelterAnimal> animals = new List<ShelterAnimal>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== ПРИЮТ ДЛЯ БЕЗДОМНЫХ ЖИВОТНЫХ ===");
                Console.WriteLine("1. Добавить новое животное");
                Console.WriteLine("2. Показать всех животных");
                Console.WriteLine("3. Фильтрация животных без прививок");
                Console.WriteLine("4. Поиск по кличке");
                Console.WriteLine("5. Изменить статус (забрали домой)");
                Console.WriteLine("6. Статистика приюта");
                Console.WriteLine("7. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddAnimal();
                        break;
                    case "2":
                        ShowAllAnimals();
                        break;
                    case "3":
                        ShowAnimalsWithoutVaccinations();
                        break;
                    case "4":
                        SearchByNickname();
                        break;
                    case "5":
                        AdoptAnimal();
                        break;
                    case "6":
                        ShowStatistics();
                        break;
                    case "7":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }

        static void AddAnimal()
        {
            Console.Write("\nВведите кличку: ");
            string nickname = Console.ReadLine();

            Console.Write("Введите вид животного (кот/собака/кролик): ");
            string animalType = Console.ReadLine();

            Console.Write("Введите возраст (лет): ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Есть ли прививки? (да/нет): ");
            bool hasVaccinations = Console.ReadLine().ToLower() == "да";

            Console.Write("Введите дату поступления (дд.мм.гггг): ");
            DateTime arrivalDate = DateTime.Parse(Console.ReadLine());

            ShelterAnimal animal = new ShelterAnimal(nickname, animalType, age,
                                                    hasVaccinations, arrivalDate);
            animals.Add(animal);
            Console.WriteLine("Животное успешно добавлено в приют!");
        }

        static void ShowAllAnimals()
        {
            if (animals.Count == 0)
            {
                Console.WriteLine("\nВ приюте нет животных.");
                return;
            }

            Console.WriteLine($"\n=== Всего животных: {animals.Count} ===");
            foreach (var animal in animals)
            {
                animal.DisplayInfo();
            }
        }

        static void ShowAnimalsWithoutVaccinations()
        {
            var unvaccinatedAnimals = animals.Where(a => !a.HasVaccinations &&
                                                        a.Status == "в приюте").ToList();

            if (unvaccinatedAnimals.Count > 0)
            {
                Console.WriteLine($"\n⚠ Животные без прививок: {unvaccinatedAnimals.Count}");
                foreach (var animal in unvaccinatedAnimals)
                {
                    animal.DisplayInfo();
                }
            }
            else
            {
                Console.WriteLine("\n✓ Все животные в приюте привиты!");
            }
        }

        static void SearchByNickname()
        {
            Console.Write("\nВведите кличку для поиска: ");
            string nickname = Console.ReadLine();

            var foundAnimals = animals.Where(a => a.Nickname.ToLower()
                                                  .Contains(nickname.ToLower())).ToList();

            if (foundAnimals.Count > 0)
            {
                Console.WriteLine($"\nНайдено животных: {foundAnimals.Count}");
                foreach (var animal in foundAnimals)
                {
                    animal.DisplayInfo();
                }
            }
            else
            {
                Console.WriteLine("Животные не найдены!");
            }
        }

        static void AdoptAnimal()
        {
            Console.Write("\nВведите кличку животного: ");
            string nickname = Console.ReadLine();

            ShelterAnimal animal = animals.Find(a => a.Nickname.ToLower() ==
                                                     nickname.ToLower() &&
                                                     a.Status == "в приюте");
            if (animal != null)
            {
                animal.AdoptAnimal();
            }
            else
            {
                Console.WriteLine("Животное не найдено в приюте или уже забрали!");
            }
        }

        static void ShowStatistics()
        {
            if (animals.Count == 0)
            {
                Console.WriteLine("\nНет данных для статистики.");
                return;
            }

            int inShelter = animals.Count(a => a.Status == "в приюте");
            int adopted = animals.Count(a => a.Status == "забрали домой");
            int withVaccinations = animals.Count(a => a.HasVaccinations &&
                                                     a.Status == "в приюте");
            int withoutVaccinations = animals.Count(a => !a.HasVaccinations &&
                                                        a.Status == "в приюте");

            Console.WriteLine("\n=== СТАТИСТИКА ПРИЮТА ===");
            Console.WriteLine($"Всего животных зарегистрировано: {animals.Count}");
            Console.WriteLine($"Сейчас в приюте: {inShelter}");
            Console.WriteLine($"Забрали домой: {adopted}");
            Console.WriteLine($"С прививками (в приюте): {withVaccinations}");
            Console.WriteLine($"Без прививок (в приюте): {withoutVaccinations}");

            var typeGroups = animals.Where(a => a.Status == "в приюте")
                                   .GroupBy(a => a.AnimalType);
            Console.WriteLine("\nПо видам:");
            foreach (var group in typeGroups)
            {
                Console.WriteLine($"  {group.Key}: {group.Count()} шт.");
            }
        }
    }
}
