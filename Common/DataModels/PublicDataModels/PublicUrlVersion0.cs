﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.DataModels.PublicDataModels
{
    public class PublicUrlVersion0
    {
        [Required]
        [Url]
        public string Value { get; set; }
        public string Id { get; set; }

        public static PublicUrlVersion0 FromInternal(StoredUrl internalVal)
        {
            return new PublicUrlVersion0()
            {
                Value = internalVal.Url,
                Id = internalVal.Id,
            };

        }
    }
}
