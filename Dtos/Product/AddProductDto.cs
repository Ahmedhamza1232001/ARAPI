using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARAPI.Dtos.Product
{
    public class AddProductDto
    {
        public string Name { get; set; } = String.Empty;
        public int Price { get; set; }
        public string Color{get;set;}   =String.Empty;
    }
}