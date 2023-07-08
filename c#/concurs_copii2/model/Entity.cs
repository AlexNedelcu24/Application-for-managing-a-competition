using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Entity<T>
    {
        [Browsable(false)]
        private T id;
        public T ID { get; set; }
        /*public Entity(T iD)
        {
            ID = iD;
        }
        public Entity() {}*/
        
    }

}
