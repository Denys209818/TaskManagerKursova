using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskManagerUI.Models;
using TaskManagerUI.Services;

namespace TaskManagerUI
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string WebServiceUrl { get; set; }
        private IConfiguration configuration { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var config = App.Current as IConfigurationService;
            configuration = config.Configuration;

            WebServiceUrl = configuration.GetSection("ServerUrlDev").Value;

        }

        private async void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            Apps.Lib.Models.BoardModel isValid = await IsValidCode();
            if (isValid != null)
            {
                List<Apps.Lib.Models.TaskModel> tasks = await GetTasks(isValid.Id);
                BoardModel mod = new BoardModel
                {
                    tasks = new
                    System.Collections.ObjectModel.ObservableCollection<Apps.Lib.Models.TaskModel>
                    (tasks),
                    BoardKey = isValid.BoardKey,
                    Id = isValid.Id

                };

                this.Visibility = Visibility.Hidden;

                BoardWindow window = new BoardWindow(mod);

                window.ShowDialog();

                this.Visibility = Visibility.Visible;
            }
            else 
            {
                MessageBox.Show("Не знайдено!");
            }
        }

        private async Task<Apps.Lib.Models.BoardModel> IsValidCode() 
        {
            return await Task.Run(async () => {
                return await Dispatcher.Invoke(async () => {
                    Apps.Lib.Models.BoardModel isCorect = null;
                    string text = this.txtCode.Text;

                    isCorect = await BoardService.GetBoard(text);
                    return isCorect;
                });
            });
        }

        private async Task<List<Apps.Lib.Models.TaskModel>> GetTasks(int id) 
        {
            return await Task.Run(async () => {
                return await BoardService.GetTasks(id);
            });
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            string code = System.IO.Path.GetRandomFileName().ToString();

            BoardService.CreateBoard(code);

            BoardWindow window = new BoardWindow(new BoardModel
            {
                BoardKey = code,
                tasks = new System.Collections.ObjectModel.ObservableCollection<Apps.Lib.Models.TaskModel>()
            });

            window.ShowDialog();

            this.Visibility = Visibility.Visible;
        }
    }
}
