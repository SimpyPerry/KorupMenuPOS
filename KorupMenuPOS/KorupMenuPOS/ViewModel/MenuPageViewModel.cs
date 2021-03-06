﻿using KorupMenuPOS.Model;
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
        public List<Categories> MenuCategories { get; set; }
        public List<Product> MenuProducts { get; set; }
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

            MessagingCenter.Subscribe<OrderViewModel>(this, "update", (sender) => {
                UpdatePrice();
            });

            addProductToOrderList = new Command<Product>(AddToOrder);
            removeProductFromOrderListCommand = new Command<Product>(RemoveFromOrder);
            RefreshMenuCommand = new Command(Refresh);
            GetProductsForCategoryCommand = new Command<Categories>(GetProductsToList);
            ShowProductInfoCommand = new Command<Product>(ShowProductInfo);
        }

        public ICommand addProductToOrderList { private set; get; }
        public ICommand removeProductFromOrderListCommand { get; private set; }
        public ICommand RefreshMenuCommand { get; private set; }
        public ICommand GetProductsForCategoryCommand { get; private set; }
        public ICommand SendOrderCommand { get; private set; }
        public ICommand SaveOrderCommand { get; private set; }
        public ICommand ShowProductInfoCommand { get; private set; }

        private async void Refresh()
        {
            
            await App.MDatabase.RefreshDatabaseDataAsync();
            await App.PDatabase.CreateOrUpdateProductDB();
            
            GetMenuData();
            
        }

        private void UpdatePrice()
        {
            TotalePrice = App.ItemManager.GetTotalePrice();
            OnPropertyChanged(nameof(TotalePrice));
        }

        private void ShowProductInfo(Product product)
        {
            string message = $"Beskrivelse: {product.Description} {Environment.NewLine}Pris: {product.Price} {Environment.NewLine}";

            App.Current.MainPage.DisplayAlert(product.Name,message, "Luk");
        }

        private async void GetMenuData()
        {
            //MenuEmner = await App.Restmanager.GetMenuData();

            MenuCategories = await App.MDatabase.GetMenuData();

            OnPropertyChanged(nameof(MenuCategories));
        }

        private async void GetProductsToList(Categories cate)
        {

              MenuProducts = new List<Product>();
            

              MenuProducts = await App.PDatabase.GetProductsForCategory(cate);

                //cate.Products;

            if(MenuProducts == null)
            {
                List<Product> listEmpty = new List<Product>();

                Product product = new Product
                {
                    Description = "Listen er tom",
                    Name = "Tom"
                    

                    
                };

                Product product2 = new Product
                {
                    Description = "Listen er tom2",
                    Name = "Tom2"
              
                };

                listEmpty.Add(product);
                listEmpty.Add(product2);

                MenuProducts = listEmpty;
            }

            

            OnPropertyChanged(nameof(MenuProducts));
        }

        //ObservableCollection<Product> p = new ObservableCollection<Product>();
        //ObservableCollection<OrderItem> noDubs = new ObservableCollection<OrderItem>();

        public void AddToOrder(Product addedProduct)
        {

            //p.Add(addedProduct);
            //List<Product> addedProductList = new List<Product>();
            //addedProductList = p.Where(x => x.ProductId == addedProduct.ProductId).ToList();
            //int number = 0;
            //foreach(Product product in addedProductList)
            //{
            //    number++;
            //}
            //var item = noDubs.Where(x => x.ProductId == addedProduct.ProductId).FirstOrDefault();
            //if(item == null)
            //{
            //    OrderItem o = new OrderItem()
            //    {
            //        ProductId = addedProduct.ProductId,
            //        Price = addedProduct.SellingPrice,
            //        ProdcutName = addedProduct.ProductName,
            //        Amount = 1

            //    };
            //    noDubs.Add(o);
            //}
            //else
            //{
            //    noDubs.Where(x => x.ProductId == addedProduct.ProductId).FirstOrDefault().Amount = number;
            //}

            App.ItemManager.AddOneToOrder(addedProduct);

            OrderList = App.ItemManager.GetOrderItems();

            TotalePrice = App.ItemManager.GetTotalePrice();
            
            OnPropertyChanged(nameof(TotalePrice));
            OnPropertyChanged(nameof(OrderList));
        }

        

        public void RemoveFromOrder(Product removedProduct)
        {

            //if (p.Any(x => x.ProductId == removedProduct.ProductId))
            //{
            //    TotalePrice = TotalePrice - subtractedPrice;
            //    //Finder index af et product i p listen der matcher productname af removedproduct
            //    var index = p.IndexOf(p.Where(x => x.ProductName == removedProduct.ProductName).FirstOrDefault());
            //    p.RemoveAt(index);
            //    List<Product> removedProductList = new List<Product>();
            //    removedProductList = p.Where(x => x.ProductId == removedProduct.ProductId).ToList();
            //    var item = noDubs.Where(x => x.ProductId == removedProduct.ProductId).FirstOrDefault();
            //    if (!removedProductList.Any())
            //    {
            //        item.Amount = 0;
            //        noDubs.Remove(item);
            //    }
            //    else
            //    {
            //        item.Amount = removedProductList.Count();
            //    }
            //}else if(!p.Any())
            //{
            //    TotalePrice = 0;
            //}

            App.ItemManager.RemoveOneItemWithProduct(removedProduct);

            TotalePrice = App.ItemManager.GetTotalePrice();

            OrderList = App.ItemManager.GetOrderItems();

            OnPropertyChanged(nameof(TotalePrice));
            OnPropertyChanged(nameof(OrderList));
        }


        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

     
    }
}

