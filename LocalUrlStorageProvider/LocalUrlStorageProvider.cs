using Common.DataModels;
using Common.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Common.Exceptions.ApiExceptions;

namespace LocalUrlStorageProvider
{
    public class LocalUrlStorageProvider : IUrlStorageProvider
    {
        private static Dictionary<string, StoredUrl> InMemoryStore;
        private IIdGenerator IdGenerator;

        private const int IdCreationRetryCount = 5;


        public LocalUrlStorageProvider(LocalUrlStoreSettings settings, IIdGenerator idGenerator)
        {
            InMemoryStore = settings.Store;
            IdGenerator = idGenerator;
        }

        public Task DeleteUrlAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<StoredUrl> GetUrlByIdAsync(string _id)
        {
            if (InMemoryStore == null)
            {
                throw new Exception("Internal Memory invalid state");
            }

            StoredUrl url;
            if (!InMemoryStore.TryGetValue(_id, out url))
            {
                throw new ApiNotFoundException($"Stored Url with id: {_id} is not found");
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

        public async Task<StoredUrl> PostUrlAsync(string value)
        {
            if (InMemoryStore == null)
            {
                throw new Exception("Internal Memory invalid state");
            }

            var retryCount = 0;
            string id;
            bool unique = false;
            do
            {
                id = IdGenerator.GenerateId();
                if (!InMemoryStore.ContainsKey(id))
                {
                    unique = true;
                    break;
                }
                retryCount++;
            } while (retryCount < IdCreationRetryCount);

            if (!unique)
            {
                throw new Exception("unable to generate unique ID. all of them are used up :/");
            }

            var internalUrl = new StoredUrl()
            {
                Url = value,
                Id = id,
                LastAccessed = DateTime.Now
            };

            InMemoryStore[id] = internalUrl;
            return internalUrl;
        }
    }
}
