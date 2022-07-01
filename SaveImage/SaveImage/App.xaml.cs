using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SaveImage.sql;
using SQLite;
using System.IO;

namespace SaveImage
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "SaveImage.db";
        internal static ImageBD db;
        internal static ImageBD Db
        {
            get
            {
                if (db == null)
                {
                    db = new ImageBD(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return db;
            }
        }
        public App()
        {
            InitializeComponent();

           MainPage = MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
