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
using TaskManagerUI.Models;
using TaskManagerUI.Services;

namespace TaskManagerUI
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        public BoardModel _model { get; set; }
        public Apps.Lib.Models.TaskModel taskModel { get; set; } = null;
        public AddTaskWindow(BoardModel model)
        {
            InitializeComponent();
            _model = model;
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => {
                Dispatcher.Invoke(async () => { 
                    Apps.Lib.Models.TaskModel task = new Apps.Lib.Models.TaskModel();

                    if (!string.IsNullOrEmpty(this.txtQuestionName.Text))
                        task.TaskText = this.txtQuestionName.Text;

                    if (!string.IsNullOrEmpty(this.txtCommentName.Text))
                        task.Comment = this.txtCommentName.Text;
                    

                    TaskService.AddCars(task, _model);
                    taskModel = task;
                    taskModel.Id = await TaskService.GetLastIndex(_model.Id);
                    this.Close();
                });
            });
        }
    }
}
