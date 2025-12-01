using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task4_BuildingStore
{
    class BuildingItem
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal PricePerUnit { get; set; }
        public int QuantityInStock { get; set; }
        public int MinimumStock { get; set; }

        public BuildingItem(string name, string category, decimal pricePerUnit,
                           int quantityInStock, int minimumStock)
        {
            Name = name;
            Category = category;
            PricePerUnit = pricePerUnit;
            QuantityInStock = quantityInStock;
            MinimumStock = minimumStock;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"\n--- Товар ---");
            Console.WriteLine($"Название: {Name}");
            Console.WriteLine($"Категория: {Category}");
            Console.WriteLine($"Цена за единицу: {PricePerUnit:C}");
            Console.WriteLine($"Количество на складе: {QuantityInStock} шт.");
            Console.WriteLine($"Минимальный остаток: {MinimumStock} шт.");
            CheckStock();
        }

        public bool SellItem(int quantity)
        {
            if (quantity > QuantityInStock)
            {
                Console.WriteLine($"Недостаточно товара на складе! Доступно: {QuantityInStock} шт.");
                return false;
            }

            QuantityInStock -= quantity;
            Console.WriteLine($"Продано {quantity} шт. товара '{Name}'");
            Console.WriteLine($"Сумма продажи: {(PricePerUnit * quantity):C}");
            CheckStock();
            return true;
        }

        public void CheckStock()
        {
            if (QuantityInStock < MinimumStock)
            {
                Console.WriteLine($"⚠ ВНИМАНИЕ! Товар '{Name}' заканчивается!");
                Console.WriteLine($"Осталось: {QuantityInStock} шт., требуется минимум: {MinimumStock} шт.");
            }
        }
    }

    class Program
    {
        static List<BuildingItem> items = new List<BuildingItem>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== СТРОИТЕЛЬНЫЙ МАГАЗИН ===");
                Console.WriteLine("1. Добавить товар");
                Console.WriteLine("2. Удалить товар");
                Console.WriteLine("3. Просмотр всех товаров");
                Console.WriteLine("4. Поиск по названию");
                Console.WriteLine("5. Продать товар");
                Console.WriteLine("6. Проверить критические остатки");
                Console.WriteLine("7. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddItem();
                        break;
                    case "2":
                        RemoveItem();
                        break;
                    case "3":
                        ShowAllItems();
                        break;
                    case "4":
                        SearchByName();
                        break;
                    case "5":
                        SellItem();
                        break;
                    case "6":
                        CheckCriticalStock();
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

        static void AddItem()
        {
            Console.Write("\nВведите название товара: ");
            string name = Console.ReadLine();

            Console.Write("Введите категорию (инструмент/отделка/сантехника/электрика): ");
            string category = Console.ReadLine();

            Console.Write("Введите цену за единицу: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Введите количество на складе: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.Write("Введите минимальный остаток: ");
            int minStock = int.Parse(Console.ReadLine());

            BuildingItem item = new BuildingItem(name, category, price, quantity, minStock);
            items.Add(item);
            Console.WriteLine("Товар успешно добавлен!");
        }

        static void RemoveItem()
        {
            Console.Write("\nВведите название товара для удаления: ");
            string name = Console.ReadLine();

            BuildingItem item = items.Find(i => i.Name.ToLower() == name.ToLower());
            if (item != null)
            {
                items.Remove(item);
                Console.WriteLine($"Товар '{name}' успешно удалён!");
            }
            else
            {
                Console.WriteLine("Товар не найден!");
            }
        }

        static void ShowAllItems()
        {
            if (items.Count == 0)
            {
                Console.WriteLine("\nСклад пуст.");
                return;
            }

            Console.WriteLine($"\n=== Всего товаров: {items.Count} ===");
            foreach (var item in items)
            {
                item.DisplayInfo();
            }
        }

        static void SearchByName()
        {
            Console.Write("\nВведите название товара: ");
            string name = Console.ReadLine();

            var foundItems = items.Where(i => i.Name.ToLower().Contains(name.ToLower())).ToList();

            if (foundItems.Count > 0)
            {
                Console.WriteLine($"\nНайдено товаров: {foundItems.Count}");
                foreach (var item in foundItems)
                {
                    item.DisplayInfo();
                }
            }
            else
            {
                Console.WriteLine("Товары не найдены!");
            }
        }

        static void SellItem()
        {
            Console.Write("\nВведите название товара: ");
            string name = Console.ReadLine();

            BuildingItem item = items.Find(i => i.Name.ToLower() == name.ToLower());
            if (item != null)
            {
                Console.Write($"Введите количество для продажи (доступно {item.QuantityInStock} шт.): ");
                int quantity = int.Parse(Console.ReadLine());
                item.SellItem(quantity);
            }
            else
            {
                Console.WriteLine("Товар не найден!");
            }
        }

        static void CheckCriticalStock()
        {
            var criticalItems = items.Where(i => i.QuantityInStock < i.MinimumStock).ToList();

            if (criticalItems.Count > 0)
            {
                Console.WriteLine($"\n⚠ Товары с критическими остатками: {criticalItems.Count}");
                foreach (var item in criticalItems)
                {
                    item.DisplayInfo();
                }
            }
            else
            {
                Console.WriteLine("\n✓ Все товары в достаточном количестве!");
            }
        }
    }
}
