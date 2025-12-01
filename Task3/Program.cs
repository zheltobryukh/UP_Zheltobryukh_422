using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3_SportStore
{
    class SportCustomer
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Product { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public string PaymentMethod { get; set; }

        public SportCustomer(string fullName, int age, string product,
                           string size, decimal price, string paymentMethod)
        {
            FullName = fullName;
            Age = age;
            Product = product;
            Size = size;
            Price = price;
            PaymentMethod = paymentMethod;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"\n--- Покупатель ---");
            Console.WriteLine($"ФИО: {FullName}");
            Console.WriteLine($"Возраст: {Age}");
            Console.WriteLine($"Товар: {Product}");
            Console.WriteLine($"Размер: {Size}");
            Console.WriteLine($"Цена: {Price:C}");
            Console.WriteLine($"Способ оплаты: {PaymentMethod}");
        }
    }

    class Program
    {
        static List<SportCustomer> customers = new List<SportCustomer>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== МАГАЗИН СПОРТИВНОЙ ОДЕЖДЫ ===");
                Console.WriteLine("1. Добавить покупателя");
                Console.WriteLine("2. Показать всех покупателей");
                Console.WriteLine("3. Найти покупателя по возрасту");
                Console.WriteLine("4. Сортировка по стоимости покупки");
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
                        SearchByAge();
                        break;
                    case "4":
                        SortByPrice();
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

            Console.Write("Введите возраст: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Введите выбранный товар (кроссовки/футболка/шорты/куртка): ");
            string product = Console.ReadLine();

            Console.Write("Введите размер: ");
            string size = Console.ReadLine();

            Console.Write("Введите цену товара: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Введите способ оплаты (наличные/карта): ");
            string paymentMethod = Console.ReadLine();

            SportCustomer customer = new SportCustomer(fullName, age, product,
                                                       size, price, paymentMethod);
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

        static void SearchByAge()
        {
            Console.Write("\nВведите возраст для поиска: ");
            int age = int.Parse(Console.ReadLine());

            var foundCustomers = customers.Where(c => c.Age == age).ToList();

            if (foundCustomers.Count > 0)
            {
                Console.WriteLine($"\nНайдено покупателей возраста {age} лет: {foundCustomers.Count}");
                foreach (var customer in foundCustomers)
                {
                    customer.DisplayInfo();
                }
            }
            else
            {
                Console.WriteLine($"Покупатели возраста {age} лет не найдены!");
            }
        }

        static void SortByPrice()
        {
            if (customers.Count == 0)
            {
                Console.WriteLine("\nСписок покупателей пуст.");
                return;
            }

            Console.WriteLine("\n1. По возрастанию цены");
            Console.WriteLine("2. По убыванию цены");
            Console.Write("Выберите тип сортировки: ");
            string choice = Console.ReadLine();

            List<SportCustomer> sorted;

            if (choice == "1")
            {
                sorted = customers.OrderBy(c => c.Price).ToList();
                Console.WriteLine("\n=== Покупатели (сортировка по возрастанию цены) ===");
            }
            else
            {
                sorted = customers.OrderByDescending(c => c.Price).ToList();
                Console.WriteLine("\n=== Покупатели (сортировка по убыванию цены) ===");
            }

            foreach (var customer in sorted)
            {
                customer.DisplayInfo();
            }
        }
    }
}
