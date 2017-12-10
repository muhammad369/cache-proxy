using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cache_proxy
{
    public class LogItem
    {
        public string url { get; set; }

        public string method { get; set; }


        public bool fromCache { get; set; }

        public DateTime time { get; set; }
    }
}
