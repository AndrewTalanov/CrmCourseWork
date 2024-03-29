﻿using System.Collections.Generic;

namespace CrmBL.Model
{
    public class Seller
    {
        public int SellerID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Check> Checks { get; set; }  

        public override string ToString()
        {
            return Name;
        }
    }
}
