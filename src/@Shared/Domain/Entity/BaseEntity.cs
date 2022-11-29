using _Shared.Domain.Interface;
using _Shared.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Shared.Domain.Entity
{
    public class BaseEntity
    {
        public Id _id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public BaseEntity()
        {
        }

        public BaseEntity(Id? id)
        {
            _id = id ?? new Id();
            CreatedAt =  DateTime.Now;
            UpdatedAt =  DateTime.Now;
        }

        public BaseEntity(Id? id, DateTime? createdAt, DateTime? updatedAt)
        {
            _id = id ?? new Id();
            CreatedAt = createdAt ?? DateTime.Now;
            UpdatedAt = updatedAt ?? DateTime.Now;
        }
        public void SetUpdatedAt(DateTime updatedAt)
        {
            UpdatedAt = updatedAt;
        }
    }
}
