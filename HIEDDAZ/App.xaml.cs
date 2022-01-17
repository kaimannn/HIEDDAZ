using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;

namespace HIEDDAZ
{
    public partial class App : Application
    {
        private readonly IServiceProvider sp = null;

        public App()
        {
            InitializeComponent();

            sp = RegisterServices();

            MainPage = new NavigationPage(sp.GetRequiredService<UI.MainPage>());
        }

        private static IServiceProvider RegisterServices()
        {
            return new ServiceCollection()
                .AddSingleton<Data.IPlantsContext, Data.PlantsContext>()

                .AddTransient<UI.MainPage>()
                .AddTransient<UI.CreateEditPlantPage>()
                .AddTransient<UI.PlantItemView>()

                .BuildServiceProvider();
        }
    }
}
