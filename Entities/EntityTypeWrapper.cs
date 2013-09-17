using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBitBros.Entities
{
    public class EntityTypeWrapper
    {
        private Type type;

        public EntityTypeWrapper(Type t)
        {
            this.type = t;
        }

        public DynamicEntity Get()
        {
            return (DynamicEntity)type.GetConstructor(new Type[] { }).Invoke(new Object[] { });
        }

        public bool IsSet()
        {
            return type != null;
        }
    }
}
