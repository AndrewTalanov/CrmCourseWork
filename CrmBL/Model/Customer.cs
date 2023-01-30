using System.Collections.Generic;

namespace CrmBL.Model
{
    internal class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Check> Checks { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
