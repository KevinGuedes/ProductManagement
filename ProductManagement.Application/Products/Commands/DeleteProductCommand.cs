using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Products.Commands
{
    public class DeleteProductCommand
    {
        public int ProductId { get; set; }

        public DeleteProductCommand(int productId)
        {
            ProductId = productId;
        }
    }
}
