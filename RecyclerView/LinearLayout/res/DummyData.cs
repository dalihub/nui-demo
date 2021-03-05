using System;
using System.Collections.Generic;

namespace Example
{
    class MenuLinearLayout
    {
        public MenuLinearLayout(string name, string subName, string price)
        {
            Name = name;
            SubName = subName;
            Price = price;
        }

        public string Name { get; set; }
        public string SubName { get; set; }
        public string Price { get; set; }
    }

    class MenuTap
    {
        public MenuTap(string name)
        {
            Name = name;
        }

        public string Name {get; set;}
    }

    class DummyDataLinearLayout
    {
        public static List<object> CreateDummyMenuTap(int amount)
        {
            string[] namePool = {
                "Caffe",
                "Tea",
                "Slush",
                "Cake",
                "Food",
            };

            List<object> result = new List<object>();
            for (int i = 0; i < amount; i++)
            {
                result.Add(new MenuTap(namePool[i%5]));
            }

            return result;
        }

        public static List<object> CreateDummyMenu(int amount)
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
                result.Add(new MenuLinearLayout(namePool[i%5], subNamePool[i%5], pricePool[i%5]));
            }

            return result;
        }
    }
}