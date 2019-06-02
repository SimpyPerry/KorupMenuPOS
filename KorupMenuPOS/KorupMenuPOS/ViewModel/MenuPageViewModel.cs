using KorupMenuPOS.Model;
using KorupMenuPOS.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KorupMenuPOS.ViewModel
{
    public class MenuPageViewModel : INotifyPropertyChanged
    {  
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Categories> MenuEmner { get; set; }
        public List<Product> Products { get; set; }
        private Product _chosenProduct { get; set; }
        public Product ChosenProduct { get; set; }
        //{
        //    get { return _chosenProduct; }
        //    set
        //    {
        //        if(_chosenProduct != value)
        //        { 
        //            _chosenProduct = value;
        //            //Her sætter jeg den metode der skal køre hver gang et produkt vælges
        //            AddToOrder(ChosenProduct);
        //        }
        //    }
        //}
        private Categories _chosenCategory { get; set; }
        public Categories ChosenCategory
        {
            get { return _chosenCategory; }
            set
            {
                if(_chosenCategory != value)
                {
                    _chosenCategory = value;
                    GetProductsToList(_chosenCategory);
                    OnPropertyChanged(nameof(ChosenCategory));
                }
            }
        }
            
        //Lister skal være observableCollection da PropertyChanged ikke reagere på lister der har tilføjet et element
        //Kun når en list går fra null til have et element eller fra til null
        public ObservableCollection<OrderItem> OrderList { get; set; }
        public double TotalePrice { get; set; }

        public MenuPageViewModel()
        {
            
            GetMenuData();
            

            addProductToOrderList = new Command<Product>(AddToOrder);
            removeProductFromOrderListCommand = new Command<Product>(RemoveFromOrder);
            RefreshMenuCommand = new Command(Refresh);
            GetProductsForCategoryCommand = new Command<Categories>(GetProductsToList);
            SaveOrderCommand = new Command(SendOrder);
        }

        public ICommand addProductToOrderList { private set; get; }
        public ICommand removeProductFromOrderListCommand { get; private set; }
        public ICommand RefreshMenuCommand { get; private set; }
        public ICommand GetProductsForCategoryCommand { get; private set; }
        public ICommand SendOrderCommand { get; private set; }
        public ICommand SaveOrderCommand { get; private set; }

        private async void Refresh()
        {
            
            await App.MDatabase.RefreshDatabaseDataAsync();
            await App.PDatabase.CreateOrUpdateProductDB();

            GetMenuData();
            
        }

        

        private async void GetMenuData()
        {
            //MenuEmner = await App.Restmanager.GetMenuData();

            MenuEmner = await App.MDatabase.GetMenuData();

            OnPropertyChanged(nameof(MenuEmner));
        }

        private async void GetProductsToList(Categories cate)
        {

              Products = new List<Product>();
            

              Products = await App.PDatabase.GetProductsForCategory(cate);

            
                //cate.Products;

            if(Products == null)
            {
                List<Product> listEmpty = new List<Product>();

                Product product = new Product
                {
                    Description = "Listen er tom",
                    ProductName = "Tom",
                    ProductId = 1,
                    ProductCode = null,
                    Barcode = null,
                    ProductImageUrl = null,
                    UnitOfMeasureId = 2,
                    BuyingPrice = 49.95,
                    SellingPrice = 74.95,
                    BranchId = 1,
                    CurrencyId = 1
                    
                };

                Product product2 = new Product
                {
                    Description = "Listen er tom2",
                    ProductName = "Tom2",
                    ProductId = 2,
                    ProductCode = null,
                    Barcode = null,
                    ProductImageUrl = null,
                    UnitOfMeasureId = 3,
                    BuyingPrice = 69.95,
                    SellingPrice = 104.95,
                    BranchId = 2,
                    CurrencyId = 1
                };

                listEmpty.Add(product);
                listEmpty.Add(product2);

                Products = listEmpty;
            }

            

            OnPropertyChanged(nameof(Products));
        }

        ObservableCollection<Product> p = new ObservableCollection<Product>();
        ObservableCollection<OrderItem> noDubs = new ObservableCollection<OrderItem>();

        public void AddToOrder(Product addedProduct)
        {

            p.Add(addedProduct);

            List<Product> addedProductList = new List<Product>();

            addedProductList = p.Where(x => x.ProductId == addedProduct.ProductId).ToList();

            int number = 0;

            foreach(Product product in addedProductList)
            {
                number++;
            }


            var item = noDubs.Where(x => x.ProductId == addedProduct.ProductId).FirstOrDefault();

            if(item == null)
            {
                OrderItem o = new OrderItem()
                {
                    ProductId = addedProduct.ProductId,
                    Price = addedProduct.SellingPrice,
                    ProdcutName = addedProduct.ProductName,
                    Amount = 1

                };

                noDubs.Add(o);
            }
            else
            {
                noDubs.Where(x => x.ProductId == addedProduct.ProductId).FirstOrDefault().Amount = number;
            }
       

            double price = addedProduct.SellingPrice;

            OrderList = noDubs;

            TotalePrice = TotalePrice + addedProduct.SellingPrice;

            
            OnPropertyChanged(nameof(OrderList));
            OnPropertyChanged(nameof(TotalePrice));
        }

        public void RemoveFromOrder(Product removedProduct)
        {
            double subtractedPrice = removedProduct.SellingPrice;

            

            if (p.Any(x => x.ProductId == removedProduct.ProductId))
            {
                TotalePrice = TotalePrice - subtractedPrice;

                //Finder index af et product i p listen der matcher productname af removedproduct
                var index = p.IndexOf(p.Where(x => x.ProductName == removedProduct.ProductName).FirstOrDefault());

                p.RemoveAt(index);
                



                List<Product> removedProductList = new List<Product>();
                removedProductList = p.Where(x => x.ProductId == removedProduct.ProductId).ToList();

                var item = noDubs.Where(x => x.ProductId == removedProduct.ProductId).FirstOrDefault();
                
                if (!removedProductList.Any())
                {
                    item.Amount = 0;
                    noDubs.Remove(item);
                }
                else
                {
                    item.Amount = removedProductList.Count();
                }

                    



            }else if(!p.Any())
            {
                TotalePrice = 0;
            }
            


            //if (p.Contains(removedProduct))
            //{


            //    //if (p.Where(x => x == removedProduct).Count() == 1)
            //    //{
            //    //    for(int i = noDubs.Count() - 1; i >= 0; i--)
            //    //    {
            //    //        if(noDubs[i].ProductId == removedProduct.ProductId)
            //    //        {
            //    //            noDubs[i].Amount = 0;
            //    //            noDubs.RemoveAt(i);

            //    //        }
            //    //    }


            //    //        //Where(x => x.ProdcutName == removedProduct.ProductName);
            //    //}


            //}

            OrderList = noDubs;

            OnPropertyChanged(nameof(OrderList));
            OnPropertyChanged(nameof(TotalePrice));
        }

        public string OrderSendtResult { get; set; }

        public void SendOrder()
        {
            if(noDubs != null)
            {

                App.ItemManager.AddListToService(noDubs);

               //OrderSendtResult = await App.Restmanager.SendeOrderToPOS(noDubs.ToList());

               // OnPropertyChanged(nameof(OrderSendtResult));
            }

        }

        

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        //private MenuPageViewModel _model;

        //public MenuPageViewModel model
        //{
        //    get
        //    {
        //        return _model;
        //    }
        //    set
        //    {
        //        _model = value;
        //        OnPropertyChanged(nameof(model));

        //        this.model2 = new LoggedInViewModel(value);
        //    }
        //}




        //private LoggedInViewModel _model2;
        //public LoggedInViewModel model2
        //{
        //    get
        //    {
        //        return _model2;
        //    }
        //    set
        //    {
        //        if(_model2 != value)
        //        {
        //            _model2 = value;
        //            OnPropertyChanged(nameof(model2));
        //        }
        //    }
        //}
    }
}

