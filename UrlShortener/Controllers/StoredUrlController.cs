﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DataModels;
using Common.DataModels.PublicDataModels;
using Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UrlShortener.Configurations;
using shortid;

namespace UrlShortener.Controllers
{
    [Produces("application/json")]
    [Route("api/StoredUrls/v0")]
    public class StoredUrlController : Controller
    {
        private IUrlStorageProvider UrlStore;
        private IOptions<ApplicationConfig> Settings;

        public StoredUrlController(IUrlStorageProvider _urlStore, IOptions<ApplicationConfig> _settings)
        {
            UrlStore = _urlStore;
            Settings = _settings;

        }
        // GET: api/StoredUrls
        [HttpGet]
        public async Task<IEnumerable<PublicUrlVersion0>> Get()
        {
            var urls = await UrlStore.ListUrlsAsync();
            var publicUrls = urls.Select(url => PublicUrlVersion0.FromInternal(url));
            return publicUrls;
        }

        // GET: api/StoredUrls/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<PublicUrlVersion0> Get(string id)
        {
            var stored = await UrlStore.GetUrlByIdAsync(id);
            return PublicUrlVersion0.FromInternal(stored);
        }
        
        // POST: api/StoredUrl
        [HttpPost]
        public async Task<PublicUrlVersion0> Post([FromBody]PublicUrlVersion0 value)
        {
            var internalUrl = new StoredUrl()
            {
                Url = value.Value,
                Id = ShortId.Generate(),
                LastAccessed = DateTime.Now
            };

            await UrlStore.PostUrlAsync(internalUrl);
            return PublicUrlVersion0.FromInternal(internalUrl);
        }
        
        // PUT: api/StoredUrl/v0/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}