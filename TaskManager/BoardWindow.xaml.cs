using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
using System.Windows.Shapes;
using TaskManagerUI.Models;
using App.Lib.Services;
using TaskManagerUI.Services;
using Newtonsoft.Json;
using System.Diagnostics;

namespace TaskManagerUI
{
    /// <summary>
    /// Interaction logic for BoardWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        public Apps.Lib.Models.CloseModel CloseModel { get; set; }
        public Socket socket { get; set; }
        public int port { get; set; }
        public BoardModel model { get; set; }
        public BoardWindow(BoardModel model)
        {
            port = FreePort.GetFreePort();
            this.model = model;
            
            var el = (BoardModel)
                (App.Current.Resources["taskData"] as ObjectDataProvider).Data;

            el.tasks = model.tasks;
            if (el.tasks == null) 
            {
                el.tasks = new ObservableCollection<Apps.Lib.Models.TaskModel>();
            }
            el.BoardKey = model.BoardKey;
            

            (App.Current.Resources["dataTasks"] as ObjectDataProvider).Refresh();
            InitializeComponent();

            //string hostName = System.Net.Dns.GetHostName();

            //var ipAdress = Dns.GetHostEntry(hostName).AddressList;

            //foreach (var item in ipAdress) 
            //{
            //    Debug.WriteLine(item.ToString());
            //}


            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            //TcpClient client = new TcpClient(endPoint);
            //client.Connect(IPAddress.Parse("91.238.103.109"), 1245);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //socket.Bind(endPoint);
            socket.Connect(new IPEndPoint(IPAddress.Parse("91.238.103.109"), 1245));
            socket.Send(Encoding.UTF8.GetBytes(model.Id.ToString()));
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.DataContext = model;
            imgBrush.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(),
                "Images", "background.jpg")));
            txtCode.Content = "Код: " + model.BoardKey;

            //listBoxTasks.ItemsSource = model.tasks;

            Task.Run(() => SocketServer());
        }
        private void btnChangeBack_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.png)|*.jpg;*.png;";

            dialog.ShowDialog();

            if (File.Exists(dialog.FileName)) 
            {
                imgBrush.ImageSource = new BitmapImage(new Uri(dialog.FileName));
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
           
        }
        private void ButtonItem_Click(object sender, RoutedEventArgs e) 
        {
            int index = (int)((sender as Button).Content as GroupBox).Tag;

            Apps.Lib.Models.TaskModel task = TaskService.GetTaskById(index);

            UpdateTaskWindow window = new UpdateTaskWindow(task);
            window.ShowDialog();

            var newTask = window._task;
            newTask.Id = task.Id;
            newTask.BoardId = task.BoardId;
            while (!model.tasks.Remove(model.tasks.FirstOrDefault(x => x.Id == index))) { }
            model.tasks.Add(newTask);

            newTask.mode = Apps.Lib.Models.TaskMode.Updating;

            SocketClient(newTask);
        }
        private void CheckBoxIsChecked(object sender, RoutedEventArgs e) 
        {
            CheckBox box = sender as CheckBox;
            if (box.IsChecked.HasValue && box.IsChecked.Value == true)
            {
                int index = (int)box.Tag;

                if (MessageBox.Show("Видалити із списку?", "Виконано!", MessageBoxButton.YesNo) 
                == MessageBoxResult.Yes)
                {
                    var newTask = this.model.tasks.FirstOrDefault(x => x.Id == index);
                    while (!this.model.tasks.Remove(this.model.tasks.FirstOrDefault(x => x.Id == index))) { }
                    TaskService.DeleteById(index);
                    newTask.mode = Apps.Lib.Models.TaskMode.Deleting;
                    SocketClient(newTask);
                }
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow window = new AddTaskWindow(model);
            
            window.ShowDialog();
            if (window.taskModel != null) 
            {
                model.tasks.Add(window.taskModel);
                window.taskModel.mode = Apps.Lib.Models.TaskMode.Adding;
                SocketClient(window.taskModel);
            }
        }

        /// Sockets
        private void SocketClient(Apps.Lib.Models.TaskModel task) 
        {
            task.BoardId = model.Id;
            socket.Send(Encoding.UTF8.GetBytes(Base64Converter.ConvertToBase64(task)));
        }
        private void SocketServer() 
        {
            while (true)
            {
                byte[] buffer = new byte[255];
                int countBytes = 0;
                StringBuilder builder = new StringBuilder();

                do
                {
                    countBytes = socket.Receive(buffer);
                    builder.Append(Encoding.UTF8.GetString(buffer));
                }
                while (socket.Available > 0);

                Apps.Lib.Models.TaskModel newTask = Base64Converter.ConvertToTask(builder.ToString());

                switch (newTask.mode) 
                {
                    case Apps.Lib.Models.TaskMode.Adding: 
                        {
                            Dispatcher.Invoke(() => {
                                model.tasks.Add(newTask);
                            });
                            break; 
                        }
                    case Apps.Lib.Models.TaskMode.Updating:
                        {
                            Dispatcher.Invoke(() => {
                                var el = model.tasks.FirstOrDefault(x => x.Id == newTask.Id);
                                while (!model.tasks.Remove(el)) { }
                                el.TaskText = newTask.TaskText;
                                el.Comment = newTask.Comment;

                                model.tasks.Add(el);
                            });
                            break;
                        }
                    case Apps.Lib.Models.TaskMode.Deleting:
                        {
                            Dispatcher.Invoke(() => {
                                while (!this.model.tasks.Remove(this.model.tasks
                                    .FirstOrDefault(x => x.Id == newTask.Id))) { }
                            });
                            break;
                        }
                }
                  
            }
        }
    }
}
