using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
using TaskManagerUI.Services;

namespace TaskManagerUI
{
    /// <summary>
    /// Interaction logic for BoardWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        public BoardModel model { get; set; }
        public BoardWindow(BoardModel model)
        {
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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.DataContext = model;
            imgBrush.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(),
                "Images", "background.jpg")));
            txtCode.Content = "Код: " + model.BoardKey;

            //listBoxTasks.ItemsSource = model.tasks;
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
            if (model.tasks.Count == 0) 
            {
                BoardService.DeleteBoard(model.BoardKey);   
            }
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
                    while (!this.model.tasks.Remove(this.model.tasks.FirstOrDefault(x => x.Id == index))) { }
                    TaskService.DeleteById(index);
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow window = new AddTaskWindow(model);
            
            window.ShowDialog();
            model.tasks.Add(window.taskModel);
        }
    }
}
