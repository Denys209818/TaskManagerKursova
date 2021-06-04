using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskManagerUI.Services;

namespace TaskManagerUI
{
    /// <summary>
    /// Interaction logic for UpdateTaskWindow.xaml
    /// </summary>
    public partial class UpdateTaskWindow : Window
    {
        public Apps.Lib.Models.TaskModel _task { get; set; }
        public UpdateTaskWindow(Apps.Lib.Models.TaskModel task)
        {
            InitializeComponent();
            _task = task;
        }
        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => {
                Dispatcher.Invoke(() => { 
                    var newTask = 
                    TaskService.UpdateTask(_task.Id, this.txtQuestionName?.Text, 
                    this.txtCommentName?.Text, _task);

                    _task = newTask;
                });
            });

            this.Close();
        }
    }
}
