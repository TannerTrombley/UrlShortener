using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DataModels
{
    public class StoredUrl
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public DateTime LastAccessed { get; set; }
    }
}
