using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Lib.Models
{
    public enum TaskMode 
    {
        Adding,
        Updating,
        Deleting
    };
    public class TaskModel
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        [Required(ErrorMessage = "Поле TaskText не може бути пустим!"), StringLength(255)]
        public string TaskText { get; set; }
        [Required(ErrorMessage = "Поле Comment не може бути пустим!"), StringLength(4000)]
        public string Comment { get; set; }


        public TaskMode mode { get; set; }
    }
}
