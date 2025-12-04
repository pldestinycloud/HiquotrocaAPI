using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hiquotroca.API.Domain.Common;

namespace Hiquotroca.API.Domain.Entities
{
    public class PostImage : BaseEntity
    {
        public long Id { get; private set; } 
        public bool IsPrimary { get; set; } = false;
        public string Url { get; set; } = string.Empty;

        public PostImage() { }

        public PostImage(string url, bool isPrimary = false)
        {
            this.IsPrimary = isPrimary;
            Url = url;
        }
    }
}

