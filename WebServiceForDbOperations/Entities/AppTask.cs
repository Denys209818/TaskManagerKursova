using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceForDbOperations.Entities
{
    [Table("tblTasks")]
    public class AppTask
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string TaskText { get; set; }
        [Required, StringLength(4000)]
        public string Comment { get; set; }
        [Required, ForeignKey("Board.Id")]
        public int BoardId { get; set; }
        public virtual AppBoard Board { get; set; }
    }
}
