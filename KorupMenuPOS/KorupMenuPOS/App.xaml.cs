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
        public App()
        {
            InitializeComponent();

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
