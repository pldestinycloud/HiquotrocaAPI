namespace Hiquotroca.API.Domain.Entities.Posts.ValueObjects;

public class PostTaxonomy
{
    public long ActionTypeId { get; private set; }
    public long CategoryId { get; private set; }
    public long SubcategoryId { get; private set; }

    public PostTaxonomy(long actionTypeId, long categoryId, long subcategoryId)
    {
        ActionTypeId = actionTypeId;
        CategoryId = categoryId;
        SubcategoryId = subcategoryId;
    }
}
