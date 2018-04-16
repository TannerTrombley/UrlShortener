using Common.DataModels;
using Common.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace LocalUrlStorageProvider
{
    public class LocalUrlStorageProvider : IUrlStorageProvider
    {
        private static Dictionary<string, StoredUrl> InMemoryStore;

        public LocalUrlStorageProvider(LocalUrlStoreSettings settings)
        {
            InMemoryStore = settings.Store;
        }

        public Task DeleteUrlAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<StoredUrl> GetUrlByIdAsync(string _id)
        {
            StoredUrl url;
            if (!InMemoryStore.TryGetValue(_id, out url))
            {
                throw new ArgumentException($"Stored Url with id: {_id} is not found");
            }
            return url;
        }

        public async Task<IEnumerable<StoredUrl>> ListUrlsAsync()
        {
            if (InMemoryStore == null)
            {
                throw new Exception("Internal Memory invalid state");
            }

            IEnumerable<StoredUrl> result = InMemoryStore.Values.ToList();
            return result;
        }

        public async Task<StoredUrl> PutUrlAsync(StoredUrl value)
        {
            throw new NotImplementedException();
        }

        public async Task<StoredUrl> PostUrlAsync(StoredUrl value)
        {
            if (InMemoryStore.ContainsKey(value.Id))
            {
                throw new ArgumentException($"Url with id: {value.Id} already exists");
            }
            InMemoryStore[value.Id] = value;
            return value;
        }
    }
}
