using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cache_proxy.models
{
    public class tbl_request
    {
        public Int64 Id { get; set; }

        [Index]
        public string url { get; set; }
        public string method { get; set; }

        

        [Index]
        public string requestContent { get; set; }

        public string response { get; set; }

        public DateTime time { get; set; }

    }
}
