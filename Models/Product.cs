using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Color{get;set;}   =string.Empty;

    }
}