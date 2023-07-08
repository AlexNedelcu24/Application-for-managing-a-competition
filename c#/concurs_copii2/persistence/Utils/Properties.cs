using System.Collections.Generic;

namespace persistence.Utils
{
    public class Properties
    {
        private readonly IDictionary<string, string> _props;
    
        public Properties()
        {
            _props = new SortedList<string, string>();
        }
    
        public string this[string key]
        {
            get => _props[key];
            set => _props[key] = value;
        }
    }
}
