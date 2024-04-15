using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LG_10_Exercise_01.Models
{
    public class Gift
    {
        public long ID { get; set; }
        public string Gift_Name { get; set; }

        public string Details { get; set; }
        public int Price { get; set; }

        public virtual GiftCategory giftcategory { get; set; }

        public int GiftCategoryID { get; set; }
    }
}