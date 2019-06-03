using KorupMenuPOS.Data;
using KorupMenuPOS.View;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace KorupMenuPOS
{
    public partial class App : Application
    {
        static LeaderBoardDB database;
        static MenuDatabase menuDatabase;
        static ProductDatabase productDatabase;
        public static RestServicesManager Restmanager { get; private set; }
        public static OrderItemServiceManager ItemManager { get; private set; }
        
        //Denne returnere den lokale file path til at gemme i databasen
        public static LeaderBoardDB Database
        {
            get
            {
                if(database == null)
                {
                  database = new LeaderBoardDB (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LeaderBoard.db3"));
                }
                return database;
            }
        }
        public static MenuDatabase MDatabase
        {
            get
            {
                if(menuDatabase == null)
                {
                    menuDatabase = new MenuDatabase (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Menu.db3"));
                }
                return menuDatabase;
            }
        }

        public static ProductDatabase PDatabase
        {
            get
            {
                if(productDatabase == null)
                {
                    productDatabase = new ProductDatabase (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Products.db3"));
                }
                return productDatabase;
            }
        }
        public App()
        {
            InitializeComponent();
            Restmanager = new RestServicesManager(new Restservice());
            ItemManager = new OrderItemServiceManager(new OrderItemService());
            MainPage = new NavigationPage(new MenuPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
