using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace services
{
    public class Exceptions : Exception
    {
        public Exceptions():base() { }

        public Exceptions(String msg) : base(msg) { }

        public Exceptions(String msg, Exception ex) : base(msg, ex) { }

    }
}
