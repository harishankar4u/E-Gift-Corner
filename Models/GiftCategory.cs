using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LG_10_Exercise_01.Models
{
    public class GiftCategory
    {
        public int GiftCategoryID { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Category")]
        public string strCategory { get; set; }
        //Navigational property
        public virtual ICollection<Gift> gifts { get; set; }
    }
}