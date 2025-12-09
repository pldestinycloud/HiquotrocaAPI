using System.ComponentModel.DataAnnotations.Schema;
using Hiquotroca.API.Domain.Common;
using Hiquotroca.API.Domain.Entities.Post;

namespace Hiquotroca.API.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }
        public string? IconPath { get; private set; }
        public List<SubCategory> SubCategories { get; private set; }

        public Category(string name, string? iconPath)
        {
            Name = name;
            IconPath = iconPath;
            SubCategories = new List<SubCategory>();
        }
    }
}
