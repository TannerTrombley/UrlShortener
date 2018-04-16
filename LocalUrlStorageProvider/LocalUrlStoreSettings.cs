using Common.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalUrlStorageProvider
{
    public class LocalUrlStoreSettings
    {
        public Dictionary<string, StoredUrl> Store { get; set; }
    }
}
