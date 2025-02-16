using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectricianTasks
{
    // Task-1: 1. Create an interface, which will declare the functionality of the base class including all methods and properties.

    interface IBuilding
    {
        string CustomerName { get; set; }
        string BuildingType { get; }
        int Size { get; set; }
        int LightBulbs { get; set; }
        int Outlets { get; set; }
        void DisplayDetails();
        void TaskWiring(); // Method to be overridden in derived classes
    }

    //Task-2 : 2. Implement a base class as abstract (declaring supplementary tasks as abstract method).
    abstract class Building : IBuilding
    {
        public string CustomerName { get; set; }
        public string BuildingType { get; set; } 
        public int Size { get; set; }
        public int LightBulbs { get; set; }
        public int Outlets { get; set; }
        private string CreditCardNumber { get; set; }

        // Constructor to initialize the building properties
        public Building(string customerName, string buildingType, int size, int lightBulbs, int outlets, string creditCardNumber)
        {
            CustomerName = customerName;
            BuildingType = buildingType;
            Size = size;
            LightBulbs = lightBulbs;
            Outlets = outlets;
            CreditCardNumber = MaskCreditCard(creditCardNumber);
        }

        // Private method to mask the credit card number
        private string MaskCreditCard(string cardNumber)
        {
            return cardNumber.Length == 16 ? $"{cardNumber.Substring(0, 4)} XXXX XXXX {cardNumber.Substring(12, 4)}" : "Invalid Card";
        }

        // Implemented method from the interface
        public void DisplayDetails()
        {
            Console.WriteLine($"{CustomerName} | {BuildingType} | {Size} sq.ft | Bulbs: {LightBulbs} | Outlets: {Outlets} | CC: {CreditCardNumber} ");
        }

        // Abstract method to be overridden by derived classes
        public abstract void TaskWiring();
    }

    // Task-2: 3. Derive all classes from this abstract class, implementing, overloading and overriding methods as needed.Remember that abstract methods are virtual.

    class House : Building
    {
        public House(string customerName, int size, int lightBulbs, int outlets, string creditCardNumber)
            : base(customerName, "House", size, lightBulbs, outlets, creditCardNumber) { }

        // Overriding the abstract method with specific functionality
        public override void TaskWiring()
        {
            Console.WriteLine("Required: Installation of fire alarms in the house.");
        }
    }

    class Barn : Building
    {
        public Barn(string customerName, int size, int lightBulbs, int outlets, string creditCardNumber)
            : base(customerName, "Barn", size, lightBulbs, outlets, creditCardNumber) { }

        // Overriding the abstract method with specific functionality
        public override void TaskWiring()
        {
            Console.WriteLine("Required: Wiring for milking equipment in the barn.");
        }
    }

    class Garage : Building
    {
        public Garage(string customerName, int size, int lightBulbs, int outlets, string creditCardNumber)
            : base(customerName, "Garage", size, lightBulbs, outlets, creditCardNumber) { }

        // Overriding the abstract method with specific functionality
        public override void TaskWiring()
        {
            Console.WriteLine("Required: Installation of automatic doors in the garage.");
        }
    }

    class Arun_A2_Task1
    {
        static void Main(string[] args)
        {
            List<Building> customers = new List<Building>();
            Console.WriteLine("Electrician Task Management - Enter Customer Details");

            while (true)
            {
                try
                {
                    Console.Write("\nEnter Customer Name (or type 'submit' to finish): ");
                    string name = Console.ReadLine();
                    if (name.ToLower() == "submit") break;

                    while (name.Any(c => !char.IsLetter(c) && c != ' '))
                    {
                        Console.Write("Invalid name! Enter again: ");
                        name = Console.ReadLine();
                    }

                    Console.Write("Enter Building Type (House/Barn/Garage): ");
                    string type = Console.ReadLine().ToLower();

                    if (type != "house" && type != "barn" && type != "garage")
                    {
                        Console.WriteLine("Invalid building type. Please enter House, Barn, or Garage.");
                        continue;
                    }

                    int size;
                    Console.Write("Enter Size of Building (1000 - 50000 sq. ft): ");
                    while (!int.TryParse(Console.ReadLine(), out size) || size < 1000 || size > 50000)
                    {
                        Console.Write("Invalid input! Enter Size again: ");
                    }

                    int bulbs;
                    Console.Write("Enter Number of Light Bulbs (max 20): ");
                    while (!int.TryParse(Console.ReadLine(), out bulbs) || bulbs < 0 || bulbs > 20)
                    {
                        Console.Write("Invalid input! Enter Number of Light Bulbs (max 20): ");
                    }

                    int outlets;
                    Console.Write("Enter Number of Outlets (max 50): ");
                    while (!int.TryParse(Console.ReadLine(), out outlets) || outlets < 0 || outlets > 50)
                    {
                        Console.Write("Invalid input! Enter Number of Outlets (max 50): ");
                    }

                    string creditCard;
                    Console.Write("Enter 16-digit Credit Card Number: ");
                    while (true)
                    {
                        creditCard = Console.ReadLine();
                        if (creditCard.Length == 16 && long.TryParse(creditCard, out _)) break;
                        Console.Write("Invalid input! Enter a valid 16-digit Credit Card Number: ");
                    }

                    // Using polymorphism to create the appropriate object
                    Building customer = null;

                    if (type == "house")
                    {
                        customer = new House(name, size, bulbs, outlets, creditCard);
                    }
                    else if (type == "barn")
                    {
                        customer = new Barn(name, size, bulbs, outlets, creditCard);
                    }
                    else if (type == "garage")
                    {
                        customer = new Garage(name, size, bulbs, outlets, creditCard);
                    }

                    if (customer != null) customers.Add(customer);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            // Displaying the customers and their tasks
            Console.WriteLine("\nElectrician Job Schedule:");
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Customer | Type | Size | Bulbs | Outlets | Card info");


            // Iterating over the list of customers to display their details and tasks.
            for (int i = 0; i < customers.Count; i++)
            {
                customers[i].DisplayDetails();
                Console.WriteLine("----------------Tasks to perfom--------------------------");
                Console.WriteLine("Create wiring schemas, Purchase necessary parts.");
                customers[i].TaskWiring(); // Calls overridden method in derived classes
                Console.WriteLine("---------------------------------------------------------");
            }
        }
    }
}
