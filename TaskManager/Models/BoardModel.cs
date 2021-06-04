using Apps.Lib.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TaskManagerUI.Models
{
    public class BoardModel
    {
        public int Id { get; set; }
        public ObservableCollection<TaskModel> tasks { get; set; }
        public string BoardKey { get; set; }
        public ObservableCollection<TaskModel> GetTasks() 
        {
            return this.tasks;
        }
    }
}
