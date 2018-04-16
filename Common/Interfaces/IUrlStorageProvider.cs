﻿using Common.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IUrlStorageProvider
    {
        Task<StoredUrl> GetUrlByIdAsync(string _id);
        Task<StoredUrl> PostUrlAsync(StoredUrl value);
        Task<StoredUrl> PutUrlAsync(StoredUrl value);
        Task<IEnumerable<StoredUrl>> ListUrlsAsync();
        Task DeleteUrlAsync();
    }
}
