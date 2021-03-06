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
using UrlShortener.Controllers.Common;
using Common.DataModels.PublicDataModels.ApiResponses;

namespace UrlShortener.Controllers
{
    [Produces("application/json")]
    [Route("api/StoredUrls/v0")]
    [ApiValidationFilter]
    [ApiExceptionFilter]
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
        public async Task<IActionResult> Get()
        {
            var urls = await UrlStore.ListUrlsAsync();
            var publicUrls = urls.Select(url => PublicUrlVersion0.FromInternal(url));
            return Ok(new ApiOkResponse(publicUrls));
        }

        // GET: api/StoredUrls/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(string id)
        {
            StoredUrl stored;

            try
            {
                stored = await UrlStore.GetUrlByIdAsync(id);
            }
            catch (ArgumentException ex)
            {
                return new ObjectResult(new ApiResponse(404, $"Url with id: {id} not found"));
            }
      
            return Ok(new ApiOkResponse(PublicUrlVersion0.FromInternal(stored)));
        }
        
        // POST: api/StoredUrl
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PublicUrlVersion0 value)
        {
            var stored = await UrlStore.PostUrlAsync(value.Value);
            return Ok(new ApiOkResponse(PublicUrlVersion0.FromInternal(stored)));
        }
       
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
