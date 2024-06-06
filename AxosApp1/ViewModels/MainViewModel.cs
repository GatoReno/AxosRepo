

using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using AxosApp1.Abstractions;
using AxosApp1.Model;
using AxosApp1.Models;

namespace AxosApp1.ViewModels
{
    public class MainViewModel : BindableObject //implementens INotifyPropertyChanged
    {
        #region props
                string _bundleNameApp;
                ImageSource _selectedImage,_imageSource;
                bool _isBusy;
                private IAppInfoService _appInfoService = DependencyService.Get<IAppInfoService>(DependencyFetchTarget.GlobalInstance);
                public string BundleNameApp { get => _bundleNameApp; set { _bundleNameApp = value; OnPropertyChanged(); } }
                public bool IsBusy { get => _isBusy; set { _isBusy = value; OnPropertyChanged(); } }
                public ImageSource SelectedImage
                {
                    get => _selectedImage;
                    set
                    {
                        _selectedImage = value;
                        OnPropertyChanged();
                    }
                }

                private string _imageUrl;
                public string ImageUrl
                {
                    get => _imageUrl;
                    set
                    {
                        _imageUrl = value;
                        OnPropertyChanged();
                    }
                }

            
                private ObservableCollection<CatModel> _cats;
                public ObservableCollection<CatModel> CatCollection
                {
                    get => _cats;
                    set
                    {
                        _cats = value;
                        OnPropertyChanged();
                    }
                }
        #endregion
        #region commands
         public ICommand SelectImageCommand { get; }
        public ICommand SetImageFromUrlCommand { get; }
        public ICommand LoadCountriesCommand { get; }
        #endregion

        public MainViewModel()
        {
            SelectImageCommand = new Command(async () => await SelectImageAsync());
            SetImageFromUrlCommand = new Command(SetImageFromUrl);
            ImageUrl = "https://random.dog/af70ad75-24af-4518-bf03-fec4a997004c.jpg";
            LoadCountriesCommand = new Command(async () => await LoadCountriesAsync());
            IsBusy = true;
            SetImageFromUrl();
            GetAppInfo(); 
            LoadCountriesCommand.Execute(null);
            CatCollection = new ObservableCollection<CatModel>();
            
        }
 private async Task LoadCountriesAsync()
        {
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync("https://api.thecatapi.com/v1/images/search?limit=10");
                var catslist = JsonSerializer.Deserialize<List<CatModel>>(response);
 
                CatCollection.Clear();
                foreach (var cat in catslist)
                {
                    CatCollection.Add(cat);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Unable to load countries: " + ex.Message, "OK");
            }
            IsBusy = false;
        }
        private async void GetAppInfo()
        {
           BundleNameApp = await _appInfoService.GetBundleID();
        }
        private async Task SelectImageAsync()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "pick an image",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    SelectedImage = ImageSource.FromStream(() => stream);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during file picking
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void SetImageFromUrl()
        {
            if (!string.IsNullOrEmpty(ImageUrl))
            {
                SelectedImage = ImageSource.FromUri(new Uri(ImageUrl));
            }
        }

    }
}