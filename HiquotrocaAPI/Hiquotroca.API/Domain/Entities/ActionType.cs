using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hiquotroca.API.Domain.Common;
using Hiquotroca.API.Domain.Entities.Posts;

namespace Hiquotroca.API.Domain.Entities
{
    public class ActionType : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }

        public ActionType(string name, string? description)
        {
            Name = name;
            Description = description;
        }
    }
}

