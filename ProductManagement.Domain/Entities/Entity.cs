using System;

namespace ProductManagement.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? UpdateDate { get; private set; }

        private protected Entity() 
        {
            CreationDate = DateTime.Now;
        }

        private protected void SetUpdateDate()
            => UpdateDate = DateTime.UtcNow;
    }
}
