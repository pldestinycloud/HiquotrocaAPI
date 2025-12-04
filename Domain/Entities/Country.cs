using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hiquotroca.API.Domain.Common;
using Hiquotroca.API.Domain.Entities.Posts;
using Hiquotroca.API.Domain.Entities.Users;

namespace Hiquotroca.API.Domain.Entities
{
    public class Country : BaseEntity
    {
        public long Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? IsoCode { get; private set; }

        public Country(string name, string? isoCode)
        {
            Name = name;
            IsoCode = isoCode;
        }
    }
}
