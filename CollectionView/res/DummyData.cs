using System;
using System.Collections.Generic;

namespace Example
{
    class Menu : List<object>
    {
        private int _index;
        public Menu(int index, string name, string subName, string price)
        {
            _index = index;
            Name = name;
            SubName = subName;
            Price = price;
        }

        public string Name { get; set; }

        public string IndexName {
            get
            {
                return "["+_index+"] : "+Name;
            }
        }
        public string SubName { get; set; }

        public string SubNameUrl
        {
            get
            {
                return "./res/"+SubName+".jpg";
            }
        }
    
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

    class DummyData
    {
        public static List<MenuTap> CreateDummyMenuTap(int amount)
        {
            string[] namePool = {
                "Caffe",
                "Tea",
                "Slush",
                "Cake",
                "Food",
            };

            List<MenuTap> result = new List<MenuTap>();
            for (int i = 0; i < amount; i++)
            {
                result.Add(new MenuTap(namePool[i%5]));
            }

            return result;
        }

        public static List<Menu> CreateDummyMenu(int amount)
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

            List<Menu> result = new List<Menu>();
            for (int i = 0; i < amount; i++)
            {
                result.Add(new Menu(i, namePool[i%5], subNamePool[i%5], pricePool[i%5]));
            }

            return result;
        }
    }
}