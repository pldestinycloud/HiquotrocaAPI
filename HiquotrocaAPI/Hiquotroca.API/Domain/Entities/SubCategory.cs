using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hiquotroca.API.Domain.Common;
using Hiquotroca.API.Domain.Entities.Posts;

namespace Hiquotroca.API.Domain.Entities
{
    public class SubCategory : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public long CategoryId { get; private set; }

        public SubCategory(string name, long categoryId)
        {
            Name = name;
            CategoryId = categoryId;
        }
    }
}
