﻿namespace ProductManagement.Application.Products.Commands
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
