using System;
using System.Collections.Generic;
using System.Text;

namespace Task1_TouristAgency
{
    class TouristUser
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Destination { get; set; }
        public DateTime TravelDate { get; set; }
        public int NumberOfPeople { get; set; }
        public decimal TotalCost { get; set; }
        public bool IsPaid { get; set; }

        public TouristUser(string fullName, string phoneNumber, string destination,
                          DateTime travelDate, int numberOfPeople, decimal totalCost)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Destination = destination;
            TravelDate = travelDate;
            NumberOfPeople = numberOfPeople;
            TotalCost = totalCost;
            IsPaid = false;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"\n--- Информация о клиенте ---");
            Console.WriteLine($"ФИО: {FullName}");
            Console.WriteLine($"Телефон: {PhoneNumber}");
            Console.WriteLine($"Направление: {Destination}");
            Console.WriteLine($"Дата поездки: {TravelDate.ToShortDateString()}");
            Console.WriteLine($"Количество человек: {NumberOfPeople}");
            Console.WriteLine($"Стоимость: {TotalCost:C}");
            Console.WriteLine($"Статус оплаты: {(IsPaid ? "Оплачено" : "Не оплачено")}");
        }

        public void MakePayment()
        {
            IsPaid = true;
            Console.WriteLine($"Тур для {FullName} успешно оплачен!");
        }
    }

    class Program
    {
        static List<TouristUser> tourists = new List<TouristUser>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== ТУРИСТИЧЕСКОЕ АГЕНТСТВО ===");
                Console.WriteLine("1. Добавить нового клиента");
                Console.WriteLine("2. Показать всех клиентов");
                Console.WriteLine("3. Оплатить тур");
                Console.WriteLine("4. Найти клиента по телефону");
                Console.WriteLine("5. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTourist();
                        break;
                    case "2":
                        ShowAllTourists();
                        break;
                    case "3":
                        PayForTour();
                        break;
                    case "4":
                        SearchByPhone();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }

        static void AddTourist()
        {
            Console.Write("\nВведите ФИО: ");
            string fullName = Console.ReadLine();

            Console.Write("Введите телефон: ");
            string phone = Console.ReadLine();

            Console.Write("Введите направление: ");
            string destination = Console.ReadLine();

            Console.Write("Введите дату поездки (дд.мм.гггг): ");
            DateTime travelDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Введите количество человек: ");
            int people = int.Parse(Console.ReadLine());

            Console.Write("Введите стоимость тура: ");
            decimal cost = decimal.Parse(Console.ReadLine());

            TouristUser tourist = new TouristUser(fullName, phone, destination,
                                                  travelDate, people, cost);
            tourists.Add(tourist);
            Console.WriteLine("Клиент успешно добавлен!");
        }

        static void ShowAllTourists()
        {
            if (tourists.Count == 0)
            {
                Console.WriteLine("\nСписок клиентов пуст.");
                return;
            }

            Console.WriteLine($"\n=== Всего клиентов: {tourists.Count} ===");
            foreach (var tourist in tourists)
            {
                tourist.DisplayInfo();
            }
        }

        static void PayForTour()
        {
            Console.Write("\nВведите телефон клиента: ");
            string phone = Console.ReadLine();

            TouristUser tourist = tourists.Find(t => t.PhoneNumber == phone);
            if (tourist != null)
            {
                tourist.MakePayment();
            }
            else
            {
                Console.WriteLine("Клиент не найден!");
            }
        }

        static void SearchByPhone()
        {
            Console.Write("\nВведите телефон для поиска: ");
            string phone = Console.ReadLine();

            TouristUser tourist = tourists.Find(t => t.PhoneNumber == phone);
            if (tourist != null)
            {
                tourist.DisplayInfo();
            }
            else
            {
                Console.WriteLine("Клиент не найден!");
            }
        }
    }
}
