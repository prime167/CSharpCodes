using System;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}