using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using tresenraya.Models;
using tresenraya.ViewModels;
using tresenraya.Views;

namespace tresenraya
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartUp(object sender, StartupEventArgs e)
        {
            TresEnRaya model = new TresEnRaya();
            TresEnRayaViewModel viewModel = new TresEnRayaViewModel(model);
            TresEnRayaView view = new TresEnRayaView();

            view.DataContext = viewModel;
            view.Show();
        }
    }
}
