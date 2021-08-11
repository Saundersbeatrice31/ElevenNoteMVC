using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class CategoryListItem
    {
        [Required]
        public int CategoryID { get; set; }
        public string Name { get; set; }
    }
}
