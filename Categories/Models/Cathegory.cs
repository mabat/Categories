using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Categories.Models
{
    public class Cathegory
    {
        public int CathegoryID { get; set; }
        public string Name { get; set; }

        public int? ParentID{ get; set; }
        public List <Cathegory> Children { get; set; }
    }
}