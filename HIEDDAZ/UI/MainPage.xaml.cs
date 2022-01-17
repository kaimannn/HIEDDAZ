using HIEDDAZ.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;

namespace HIEDDAZ.UI
{
    public partial class MainPage : ContentPage
    {
        private readonly IPlantsContext context = null;
        private readonly IServiceProvider sp = null;

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
            var plants = await context.GetContextAsync();
        }

        private void OnCreateClicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(sp.GetRequiredService<CreateEditPlantPage>());
        }
    }
}
