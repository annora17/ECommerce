using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Domain.Entities.Base
{
    public abstract class BaseEntities : BaseEntities<int> { }
    public abstract class BaseGuidEntities : BaseEntities<string>
    {
        protected BaseGuidEntities()
        {
            ID = Guid.NewGuid().ToString();
        }
    }
    public abstract class AuditEntities : BaseEntities
    {
        public DateTime CreatedTime { get; private set; }
        public DateTime ModifiedTime { get; private set; }

        protected AuditEntities()
        {
            var currentTime = DateTime.Now;
            CreatedTime = currentTime;
            ModifiedTime = currentTime;
        }
        public override bool Update<T>(ref T output, T input)
        {
            var result = base.Update(ref output, input);

            if (result)
                ModifiedTime = DateTime.Now;

            return result;
        }
    }
    public abstract class AuditGuidEntities : BaseGuidEntities
    {
        public DateTime CreatedTime { get; private set; }
        public DateTime ModifiedTime { get; protected set; }

        protected AuditGuidEntities()
        {
            var currentTime = DateTime.Now;
            CreatedTime = currentTime;
            ModifiedTime = currentTime;
        }

        public override bool Update<T>(ref T output, T input)
        {
            var result = base.Update(ref output, input);

            if (result)
                ModifiedTime = DateTime.Now;

            return result;
        }
    }
    public abstract class BaseEntities<TId> : IBaseEntity where TId : IComparable
    {

        private bool isUpdated = false;
        public bool IsUpdated { get => isUpdated; }
        public TId ID { get; protected set; }
        public virtual void IsUpdatedChanged()
        {
            if (IsUpdated)
                return;
            isUpdated = true;
        }
        public virtual void IsUpdatedComplete()
        {
            if (!IsUpdated)
                return;
            isUpdated = false;
        }
        public virtual bool Update<T>(ref T output, T input) where T : class, IComparable
        {
            if (!output.Equals(input))
            {
                output = input;
                IsUpdatedChanged();
                return true;
            }

            return false;
        }
    }
}
