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

        public BaseEntity(Id? id)
        {
            _id = id;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
        public void SetUpdatedAt(DateTime updatedAt)
        {
            UpdatedAt = updatedAt;
        }
    }
}
