using HIEDDAZ.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HIEDDAZ.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateEditPlantPage : ContentPage
    {
        public new int Id { get; set; }
        public string Name { get; set; }
        public DateTime WateredOn { get; set; }

        private readonly IPlantsContext context = null;
        private readonly IServiceProvider sp = null;

        public CreateEditPlantPage(IPlantsContext context, IServiceProvider sp)
        {
            InitializeComponent();

            if (IsNewPlant())
                SetNewPlantDefaultValues();

            BindingContext = this;

            this.context = context;
            this.sp = sp;
        }

        private void SetNewPlantDefaultValues()
        {
            WateredOn = DateTime.Now;
            pickerDays.SelectedIndex = 0;
            pickerHours.SelectedIndex = 0;
        }

        private async void OnSaveClicked(object sender, EventArgs args)
        {
            if (!IsFormOK())
                return;

            if (IsNewPlant())
            {
                context.Plants.Add(new Plant
                {
                    Id = context.Plants.Any() ? context.Plants.Max(p => p.Id) + 1 : 1,
                    Name = Name,
                    WateredOn = WateredOn,
                    WaterFrequencyInHours = int.Parse(pickerDays.SelectedItem.ToString()) * 24 + int.Parse(pickerHours.SelectedItem.ToString())
                });
            }
            else
            {
                var plant = context.Plants.Where(p => p.Id == Id).FirstOrDefault();
                if (plant != null)
                {
                    plant.Name = Name;
                    plant.WateredOn = WateredOn;
                    plant.WaterFrequencyInHours = int.Parse(pickerDays.SelectedItem.ToString()) * 24 + int.Parse(pickerHours.SelectedItem.ToString());
                }
            }

            await context.SaveChangesAsync();
            await Navigation.PushAsync(sp.GetRequiredService<MainPage>());
        }

        private bool IsFormOK()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                DisplayAlert("Error", "Plant Name cannot be empty", "Cancel");
                return false;
            }

            if (pickerDays.SelectedIndex == 0 && pickerHours.SelectedIndex == 0)
            {
                DisplayAlert("Error", "Plant Water frequency cannot be 0:0", "Cancel");
                return false;
            }

            return true;
        }

        public bool IsNewPlant() => Id == 0;
    }
}