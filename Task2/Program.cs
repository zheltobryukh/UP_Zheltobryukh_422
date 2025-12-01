using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task2_JewelryStore
{
    class JewelryCustomer
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string JewelryType { get; set; }
        public string Material { get; set; }
        public decimal Price { get; set; }
        public int DiscountPercent { get; set; }

        public JewelryCustomer(string fullName, string phoneNumber, string jewelryType,
                              string material, decimal price, int discountPercent)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            JewelryType = jewelryType;
            Material = material;
            Price = price;
            DiscountPercent = discountPercent;
        }

        public decimal CalculateFinalPrice()
        {
            return Price * (1 - DiscountPercent / 100m);
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"\n--- Покупатель ---");
            Console.WriteLine($"ФИО: {FullName}");
            Console.WriteLine($"Телефон: {PhoneNumber}");
            Console.WriteLine($"Тип украшения: {JewelryType}");
            Console.WriteLine($"Материал: {Material}");
            Console.WriteLine($"Цена: {Price:C}");
            Console.WriteLine($"Скидка: {DiscountPercent}%");
            Console.WriteLine($"Итоговая стоимость: {CalculateFinalPrice():C}");
        }
    }

    class Program
    {
        static List<JewelryCustomer> customers = new List<JewelryCustomer>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== ЮВЕЛИРНЫЙ МАГАЗИН ===");
                Console.WriteLine("1. Добавить нового покупателя");
                Console.WriteLine("2. Показать всех покупателей");
                Console.WriteLine("3. Найти покупателя по номеру телефона");
                Console.WriteLine("4. Показать общую прибыль магазина");
                Console.WriteLine("5. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCustomer();
                        break;
                    case "2":
                        ShowAllCustomers();
                        break;
                    case "3":
                        SearchByPhone();
                        break;
                    case "4":
                        CalculateTotalProfit();
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

        static void AddCustomer()
        {
            Console.Write("\nВведите ФИО: ");
            string fullName = Console.ReadLine();

            Console.Write("Введите телефон: ");
            string phone = Console.ReadLine();

            Console.Write("Введите тип украшения (кольцо/браслет/серьги/цепочка): ");
            string jewelryType = Console.ReadLine();

            Console.Write("Введите материал (золото/серебро/платина): ");
            string material = Console.ReadLine();

            Console.Write("Введите цену: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Введите скидку (%): ");
            int discount = int.Parse(Console.ReadLine());

            JewelryCustomer customer = new JewelryCustomer(fullName, phone, jewelryType,
                                                          material, price, discount);
            customers.Add(customer);
            Console.WriteLine("Покупатель успешно добавлен!");
        }

        static void ShowAllCustomers()
        {
            if (customers.Count == 0)
            {
                Console.WriteLine("\nСписок покупателей пуст.");
                return;
            }

            Console.WriteLine($"\n=== Всего покупателей: {customers.Count} ===");
            foreach (var customer in customers)
            {
                customer.DisplayInfo();
            }
        }

        static void SearchByPhone()
        {
            Console.Write("\nВведите номер телефона: ");
            string phone = Console.ReadLine();

            var foundCustomers = customers.Where(c => c.PhoneNumber.Contains(phone)).ToList();

            if (foundCustomers.Count > 0)
            {
                Console.WriteLine($"\nНайдено покупателей: {foundCustomers.Count}");
                foreach (var customer in foundCustomers)
                {
                    customer.DisplayInfo();
                }
            }
            else
            {
                Console.WriteLine("Покупатели не найдены!");
            }
        }

        static void CalculateTotalProfit()
        {
            if (customers.Count == 0)
            {
                Console.WriteLine("\nНет данных для расчёта.");
                return;
            }

            decimal totalProfit = customers.Sum(c => c.CalculateFinalPrice());
            decimal totalDiscount = customers.Sum(c => c.Price - c.CalculateFinalPrice());

            Console.WriteLine($"\n=== СТАТИСТИКА МАГАЗИНА ===");
            Console.WriteLine($"Количество покупателей: {customers.Count}");
            Console.WriteLine($"Общая прибыль: {totalProfit:C}");
            Console.WriteLine($"Общая сумма скидок: {totalDiscount:C}");
        }
    }
}
