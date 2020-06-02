using System;
using System.Collections.Generic;

namespace Example
{
    class Menu
    {
        public Menu(string name, string subName, string price)
        {
            Name = name;
            SubName = subName;
            Price = price;
        }

        public string Name { get; set; }
        public string SubName { get; set; }
        public string Price { get; set; }
    }

    class DummyData
    {
        public static List<object> CreateDummyData(int amount)
        {
            string[] namePool = {
                "Café exprés",
                "Americano",
                "Café latte",
                "Café Mocha",
                "Té verde",
            };
            
            string[] subNamePool = {
                "Espresso",
                "Americano",
                "Cafe Latte",
                "Cafe Mocha",
                "Green Tea",
            };

            string[] pricePool = {
                "2.0 USD",
                "2.0 USD",
                "3.0 USD",
                "3.5 USD",
                "3.0 USD"
            };

            List<object> result = new List<object>();
            for (int i = 0; i < amount; i++)
            {
                result.Add(new Menu(namePool[i%5], subNamePool[i%5], pricePool[i%5]));
            }

            return result;
        }
    }
}