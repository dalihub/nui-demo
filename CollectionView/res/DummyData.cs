using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tizen.NUI.Binding;

namespace Example
{
    class Menu : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _index;
        private string _name;
        private string _subname;
        private string _price;
        private bool _selected;
        public Menu(int index, string name, string subName, string price)
        {
            _index = index;
            _name = name;
            _subname = subName;
            _price = price;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
                OnPropertyChanged("IndexName");
            }
        }

        public string IndexName {
            get
            {
                return "["+_index+"] : "+Name;
            }
        }

        public string SubName
        {
            get
            {
                return _subname;
            }
            set
            {
                _subname = value;
                OnPropertyChanged("SubName");
                OnPropertyChanged("Url");
            }
        }

        public string Url
        {
            get
            {
                return "./res/imgSmall/"+SubName+".jpg";
            }
        }
    
        public string Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                OnPropertyChanged("Selected");
            }
        }
    }

    class SimpleMenu : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _index;
        private string _name;
        private float _price;
        private bool _selected;
        public SimpleMenu(int index, string name, float price)
        {
            _index = index;
            _name = name;
            _price = price;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
                OnPropertyChanged("IndexName");
                OnPropertyChanged("Url");
            }
        }

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
                return "./res/img/"+Name+".jpg";
            }
        }

        public string Price
        {
            get
            {
                return string.Format("{0:0.00} ", _price)+" EUR";
            }
        }

        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                OnPropertyChanged("Selected");
            }
        }
    }
    

    class MenuTap : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _name;
        public MenuTap(string name)
        {
            _name = name;
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
    }

    class MenuGroup : ObservableCollection<SimpleMenu>
    {
        //public event PropertyChangedEventHandler PropertyChanged;
/*\
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        */
        int _index;
        string _groupName;
        bool _selected;
        public string GroupName
        {
            get
            {
                return _groupName;
            }
            set
            {
                _groupName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("GroupName"));

            }
        }
        public MenuGroup(int index, string name, MenuGroup menuList) :base(menuList)
        {
            _index = index;
            _groupName = name;
        }

        public MenuGroup(int index, string name) :base()
        {
            _index = index;
            _groupName = name;
        }

        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Selected"));
            }
        }
    }

    class DummyData
    {
        public static ObservableCollection<MenuTap> CreateDummyMenuTap(int amount)
        {
            string[] namePool = {
                "Caffe",
                "Tea",
                "Slush",
                "Cake",
                "Food",
            };

            ObservableCollection<MenuTap> result = new ObservableCollection<MenuTap>();
            for (int i = 0; i < amount; i++)
            {
                result.Add(new MenuTap(namePool[i%5]));
            }

            return result;
        }

        public static ObservableCollection<Menu> CreateDummyMenu(int amount)
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

            ObservableCollection<Menu> result = new ObservableCollection<Menu>();
            for (int i = 0; i < amount; i++)
            {
                result.Add(new Menu(i, namePool[i%5], subNamePool[i%5], pricePool[i%5]));
            }

            return result;
        }

        public static ObservableCollection<MenuGroup> CreateDummyMenuGroup(int amount)
        {
            
            (string name, float price)[] beveragePool = {
                ("Espresso", 1.50F),
                ("Americano", 1.50F),
                ("Cafe Latte", 2.00F),
                ("Cafe Mocha", 2.50F), 
                ("Cappuccino", 2.50F ),
                ("Caramel Macchiato", 3.00F),
                ("Green Tea", 2.50F),
                ("Hot Chocolate", 2.00F),
                ("Coke", 2.00F),
            };

            (string name, float price)[] alcoholPool = {
                ("Lager", 2.50F),
                ("Stout", 2.50F),
                ("Wheat Ale", 2.50F),
                ("Pale Ale", 3.00F),
                ("IPA", 3.00F),
                ("House Red Wine", 2.50F),
                ("House White Wine", 2.50F),
                ("Champagne", 3.50F),
                ("Sangria", 3.00F),
                ("Mojito", 3.00F),
                ("High Ball", 3.00F),
            };

            (string name, float price)[] dessertPool = {
                ("Cheese Cake", 3.50F),
                ("Chocolate Cake", 3.50F),
                ("Tiramisu", 4.00F),
                ("Ice Cream", 3.50F),
                ("Waffles", 3.00F),
                ("Potato Chips", 2.50F),
                ("Croissant", 1.50F),
            };

            (string name, float price)[] brunchPool = {
                ("Salami Sandwich", 3.50F),
                ("Egg Bacon Sandwich", 3.50F),
                ("Cheese Sandwich", 3.50F),
                ("Grilled Chiken Sandwich", 4.00F),
                ("B.L.T Sandwich", 4.00F),
                ("Club House Sandwich", 4.00F),
                ("Macaroni and Cheese", 4.00F),
                ("Eggs Benedict", 6.00F),
                ("Fish and Chips", 8.0F),
            };

            ObservableCollection<MenuGroup> result = new ObservableCollection<MenuGroup>();
            for (int i = 0; i < amount; i++)
            {
                MenuGroup bev = new MenuGroup(i*5, "Beverage Menu");
                for (int j = 0; j < beveragePool.Length; j++)
                {
                    bev.Add(new SimpleMenu(j, beveragePool[j].name, beveragePool[j].price));
                }
                result.Add(bev);

                MenuGroup alc = new MenuGroup(i*5+1, "Alcohol Menu");
                for (int j = 0; j < alcoholPool.Length; j++)
                {
                    alc.Add(new SimpleMenu(j, alcoholPool[j].name, alcoholPool[j].price));
                }
                result.Add(alc);

                MenuGroup dessert = new MenuGroup(i*5+2, "Dessert Menu");
                for (int j = 0; j < dessertPool.Length; j++)
                {
                    dessert.Add(new SimpleMenu(j, dessertPool[j].name, dessertPool[j].price));
                }
                result.Add(dessert);

                MenuGroup brunch = new MenuGroup(i*5+3, "Brunch and Snack Menu");
                for (int j = 0; j < brunchPool.Length; j++)
                {
                    brunch.Add(new SimpleMenu(j, brunchPool[j].name, brunchPool[j].price));
                }
                result.Add(brunch);
            }

            return result;
        }
    }
}