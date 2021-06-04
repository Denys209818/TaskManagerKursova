using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Lib.Models
{
    public class BoardModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле BoardKey не може бути пустим!"), StringLength(255)]
        public string BoardKey { get; set; }
        [Required(ErrorMessage = "Поле UrlImage не може бути пустим!"), StringLength(255)]
        public string UrlImage { get; set; }


    }
}
