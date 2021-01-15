using System;
using System.Collections.Generic;

namespace Example
{
    class Menu
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

        public bool Selected { get; set; }
    }

    class SimpleMenu
    {
        private int _index;
        public SimpleMenu(int index, string name)
        {
            _index = index;
            Name = name;
        }

        public string Name { get; set; }

        public string IndexName {
            get
            {
                return "["+_index+"] : "+Name;
            }
        }

        public string Url
        {
            get
            {
                return "./res/"+Name+".jpg";
            }
        }

        public bool Selected { get; set; }
    }
    

    class MenuTap
    {
        public MenuTap(string name)
        {
            Name = name;
        }

        public string Name {get; set;}
    }

    class MenuGroup : List<SimpleMenu>
    {
        int _index;
        public string GroupName { get; set; }

        public string IndexName {
            get
            {
                return "["+_index+"] : "+GroupName;
            }
        }
        public MenuGroup(int index, string name, List<SimpleMenu> menuList) :base(menuList)
        {
            _index = index;
            GroupName = name;
        }

        public bool Selected { get; set; }
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

        public static List<MenuGroup> CreateDummyMenuGroup(int amount)
        {
            
            string[] beveragePool = {
                "Espresso",
                "Americano",
                "Caffe Latte",
                "Caffe Mocha",
                "Cappuccino",
                "Caramel Macciato",
                "Green Tea",
                "Hot Chocolate",
                "Coke",
                "Zero Coke",
            };

            string[] alcoholPool = {
                "Heineken",
                "Guiness",
                "Stella Artois",
                "Hogaarden",
                "Becks",
                "Estrella Damm",
                "House Red Wine",
                "House White Wine",
                "Champagne",
                "Sangria",
                "Mojito",
                "High Ball",
            };

            string[] dessertPool = {
                "Cheese Cake",
                "Chocolate Cake",
                "Tiramisu",
                "Ice Cream",
                "Waffles",
                "Potato Chips",
                "Pain au Chocolat",
                "Croissant",
            };

            string[] brunchPool = {
                "Salami Sandwich",
                "Egg Bacon Sandwich",
                "Cheese Sandwich",
                "Grilled Chiken Sandwich",
                "B.L.T Sandwich",
                "Club House Sandwich",
                "Macaroni and Cheese",
                "Eggs In Hell",
                "Eggs Benedict",
                "Fish and Chips",
            };

            string[] diningPool = {
                "Spaghetti alla Bolognese",
                "Spaghetti alla Vongole",
                "Fettuccine alla Carbonara",
                "Cacio e Pepe",
                "Aglio e Olie",
                "Risotto alla Pescatore",
                "Risotto all Fungo",
                "Pizza alla Rucola",
                "Pizza Margherita",
                "Pizza Fungo",
                "New york Strip Steak",
                "Filet Mignon Steak",
                "T-Bone Steak",
                "Salmon Steak",
            };

            List<MenuGroup> result = new List<MenuGroup>();
            for (int i = 0; i < amount; i++)
            {
                List<SimpleMenu> bev = new List<SimpleMenu>();
                for (int j = 0; j < beveragePool.Length; j++)
                {
                    bev.Add(new SimpleMenu(j, beveragePool[i]));
                }
                result.Add(new MenuGroup(i*5, "Beverage Menu", bev));

                List<SimpleMenu> alc = new List<SimpleMenu>();
                for (int j = 0; j < alcoholPool.Length; j++)
                {
                    alc.Add(new SimpleMenu(j, alcoholPool[i]));
                }
                result.Add(new MenuGroup(i*5+1, "Alcohol Menu", alc));

                List<SimpleMenu> dessert = new List<SimpleMenu>();
                for (int j = 0; j < dessertPool.Length; j++)
                {
                    dessert.Add(new SimpleMenu(j, dessertPool[i]));
                }
                result.Add(new MenuGroup(i*5+2, "Dessert Menu", dessert));

                List<SimpleMenu> brunch = new List<SimpleMenu>();
                for (int j = 0; j < brunchPool.Length; j++)
                {
                    brunch.Add(new SimpleMenu(j, brunchPool[i]));
                }
                result.Add(new MenuGroup(i*5+3, "Brunch and Snack Menu", brunch));

                List<SimpleMenu> dining = new List<SimpleMenu>();
                for (int j = 0; j < diningPool.Length; j++)
                {
                    dining.Add(new SimpleMenu(i, diningPool[i]));
                }
                result.Add(new MenuGroup(i*5+4, "Dining Menu", dining));
            }

            return result;
        }
    }
}