using HIEDDAZ.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace HIEDDAZ.UI
{
    public partial class MainPage : ContentPage
    {
        private bool isFirst = true;

        private readonly IPlantsContext context = null;
        private readonly IServiceProvider sp = null;

        public ObservableCollection<Plant> Plants { get; set; } = new ObservableCollection<Plant>();

        public MainPage(IPlantsContext context, IServiceProvider sp)
        {
            InitializeComponent();

            BindingContext = this;

            this.context = context;
            this.sp = sp;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Update list based in context
            if (isFirst)
            {
                isFirst = false;
                var plants = await context.GetContextAsync();

                foreach (var plant in plants)
                    Plants.Add(plant);
            }
            
        }

        private void OnCreateClicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(sp.GetRequiredService<CreateEditPlantPage>());
        }
    }
}
