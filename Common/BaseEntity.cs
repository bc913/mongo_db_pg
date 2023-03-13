using System;

namespace Shine.Backend.Common
{
    public abstract class BaseEntity<IdType>
    {
        protected BaseEntity() { Id = default(IdType); }
        protected BaseEntity(IdType id)
        {
            if(object.Equals(id, default(IdType)))
                throw new ArgumentException("The id can not be default value");
            
            Id = id;
        }

        public IdType Id { get; private set; }
    }
}