using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using SQLite;
using SaveImage.sql;
using SaveImage.Page;


namespace SaveImage
{

    public partial class MainPage : ContentPage
    {
        public string pathName;
        public MainPage()
        {
            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {
            LVProject.ItemsSource = App.Db.GetItems();
            base.OnAppearing();
        }

        private void UpdateList()
        {
            LVProject.ItemsSource = App.Db.GetItems();
        }
        
        private void AddImage_Clicked(object sender, EventArgs e)
        {
            ImageS img = new ImageS();
            img.Name = Name.Text;
            img.Puth = pathName;
            App.Db.SaveItem(img);
            
            UpdateList();
        }
        private async void PhotoGetAsync_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                pathName = photo.FullPath;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }

        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                });

                var newFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);

                Debug.WriteLine($"Путь фото {photo.FullPath}");

                pathName = photo.FullPath;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }

        private async void UpdateView_Refreshing(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            UpdateView.IsRefreshing = false;
            UpdateList();

        }

        private async void LVProject_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ImageS selectedImage = (ImageS)e.SelectedItem;
            SelectedPage imagePage = new SelectedPage();
            imagePage.BindingContext = selectedImage;
            await Navigation.PushAsync(imagePage);
        }



        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            
           
        }

        private void SwipeItem_Clicked(object sender, EventArgs e)
        {
            var project = (sender as SwipeItem).BindingContext as ImageS;
            App.Db.DeleteItem(project.Id);
        }
    }
}
