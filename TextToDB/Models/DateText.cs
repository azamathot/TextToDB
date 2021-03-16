using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TextToDB.Models
{
    public class DateText
    {
        public int Id { get; set; }
        [Column(TypeName = "Date")] 
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
        [Required(ErrorMessage = "Сообщение не может быть пустым")]
        public string Text { get; set; }
    }
}
