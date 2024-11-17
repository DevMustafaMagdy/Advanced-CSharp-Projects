
using System.ComponentModel.Design;

namespace ShoppingSystemProject
{
    internal class Program
    {
        static List<string> cartItems = [];
        static readonly Dictionary<string, double> stockItems = new()
        {
            { "Camera", 2000 },
            {"Laptop", 20_000 },
            {"IPhone 15", 50_000 },
            {"Samsung s23", 48_0000 }
        };
        static Stack<string> actionsStack = new(); // save your actions in cart items [add, remove]

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nWelcome to the Shopping System");
                Console.WriteLine("==============================");
                Console.WriteLine("1. Add item to cart");
                Console.WriteLine("2. View cart item");
                Console.WriteLine("3. Remove item from cart");
                Console.WriteLine("4. Checkout");
                Console.WriteLine("5. Undo Action");
                Console.WriteLine("6. Exit");

                Console.Write("Please choose your choice number: ");
                if (int.TryParse(Console.ReadLine(), out int choiceNumber))
                {
                    switch (choiceNumber)
                    {
                        case 1:
                            AddItem();
                            break;
                        case 2:
                            ViewCart();
                            break;
                        case 3:
                            RemoveItem();
                            break;
                        case 4:
                            Checkout();
                            break;
                        case 5:
                            UndoAction();
                            break;
                        case 6:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid Choice");
                            break;
                    }
                }
                else
                    Console.WriteLine("Invalid Choice, Please Try Enter Number (1 -> 6)");
            }
        }
        private static void AddItem()
        {
            Console.WriteLine("\nAvailable items in stock");
            Console.WriteLine("========================");
            foreach (var item in stockItems)
                Console.WriteLine($"{item.Key}, {item.Value}");

            Console.Write("Enter your item: ");
            string itemToAdd = Console.ReadLine();

            if (stockItems.ContainsKey(itemToAdd))
            {
                cartItems.Add(itemToAdd);
                actionsStack.Push(itemToAdd);
                Console.WriteLine($"Item {itemToAdd} is added successfully");
            }
            else
                Console.WriteLine($"Item {itemToAdd} is out of stock or unavailable");
        }

        private static void ViewCart()
        {
            Console.WriteLine("\nYour cart items are: ");
            Console.WriteLine("====================");

            if (cartItems.Any())
            {
                foreach (var item in GetCartPrices())
                    Console.WriteLine($"Item: {item.Item1}, Price: {item.Item2}");
            }
            else
                Console.WriteLine("Your cart items is empty");
        }

        private static IEnumerable<Tuple<string, double>> GetCartPrices()
        {
            var cartPrices = new List<Tuple<string, double>>();

            foreach (var item in cartItems)
            {
                var itemPrice = new Tuple<string, double>(item, stockItems[item]);
                cartPrices.Add(itemPrice);
            }

            return cartPrices;
        }
        private static void RemoveItem()
        {
            ViewCart();

            if (cartItems.Any())
            {
                Console.Write("Enter your item to remove: ");
                string itemToRemove = Console.ReadLine();
                if (cartItems.Contains(itemToRemove))
                {
                    cartItems.Remove(itemToRemove);
                    actionsStack.Push(itemToRemove);
                    Console.WriteLine($"Item {itemToRemove} is removed successfully");
                }
                else
                    Console.WriteLine($"Item {itemToRemove} is not found in your cart to remove");
            }
        }

        private static void Checkout()
        {
            Console.WriteLine("\nCheckout");
            Console.WriteLine("==========");
            if (cartItems.Any())
            {
                double totalPrice = 0;
                foreach (var item in GetCartPrices())
                {
                    totalPrice += item.Item2;
                    Console.WriteLine($"Item -> {item.Item1}, Price -> {item.Item2}");
                }
                Console.WriteLine($"Total Price = {totalPrice}");
                Console.WriteLine("Please Proceed to Payment, Thank you for shopping with us");
                cartItems.Clear();
                actionsStack.Clear();
            }
            else
                Console.WriteLine("Your car is empty");

        }
        private static void UndoAction()
        {
            if (actionsStack.Count > 0)
            {
                string lastAction = actionsStack.Pop();
                if (cartItems.Contains(lastAction)) 
                {
                    cartItems.Remove(lastAction);
                    Console.WriteLine($"Undo is done successfully and the item {lastAction} is removed");
                }
                else
                {
                    cartItems.Add(lastAction);
                    Console.WriteLine($"Undo is done successfully and the item {lastAction} is Added");
                }
            }
            else
            {
                Console.WriteLine("Your actions is empty");
            }
        }
    }
}
