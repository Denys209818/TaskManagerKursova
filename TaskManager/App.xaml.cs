using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TaskManagerUI.Models;

namespace TaskManagerUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public interface IConfigurationService
    {
        public IConfiguration Configuration { get; set; }
    }
    public partial class App : Application, IConfigurationService
    {
        public IServiceProvider Provider { get; set; }
        public IConfiguration Configuration { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            var services = new ServiceCollection();
            services.AddTransient(typeof(MainWindow));
            services.AddTransient(typeof(BoardWindow));
            Provider = services.BuildServiceProvider();

            
            
            MainWindow window = Provider.GetRequiredService<MainWindow>();
            window.Show();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            
        }
    }
}
