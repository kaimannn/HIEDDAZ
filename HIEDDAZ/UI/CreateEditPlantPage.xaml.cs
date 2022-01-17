using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using HIEDDAZ.Data;
using System;

namespace HIEDDAZ.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateEditPlantPage : ContentPage
    {
        public new int Id { get; set; }
        public string Name { get; set; }
        public DateTime WateredOn { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }

        private readonly IPlantsContext context = null;
        private readonly IServiceProvider sp = null;

        public CreateEditPlantPage(IPlantsContext context, IServiceProvider sp)
        {
            InitializeComponent();

            BindingContext = this;

            this.context = context;
            this.sp = sp;
        }

        private async void OnSaveClicked(object sender, EventArgs args)
        {
            if (Id == 0)
            {
                context.Plants.Add(new Plant
                {
                    Id = context.Plants.Any() ? context.Plants.Max(p => p.Id) + 1 : 1,
                    Name = Name,
                    WateredOn = WateredOn,
                    WaterFrequencyInHours = Days * 24 + Hours,
                });
            }
            else
            {
                var plant = context.Plants.Where(p => p.Id == Id).FirstOrDefault();
                if (plant != null)
                {
                    plant.Name = Name;
                    plant.WateredOn = WateredOn;
                    plant.WaterFrequencyInHours = Days * 24 + Hours;
                }
            }

            await context.SaveChangesAsync();
            await Navigation.PushAsync(sp.GetRequiredService<MainPage>());
        }
    }
}