using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceForDbOperations.Entities
{
    [Table("tblBoards")]
    public class AppBoard
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string BoardKey { get; set; }
        [Required, StringLength(255)]
        public string UrlImage { get; set; }
        public virtual ICollection<AppTask> Tasks { get; set; }
    }
}
