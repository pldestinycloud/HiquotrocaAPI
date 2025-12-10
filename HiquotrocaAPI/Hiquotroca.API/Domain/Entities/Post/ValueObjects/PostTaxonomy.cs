namespace Hiquotroca.API.Domain.Entities.Posts.ValueObjects;

public class PostTaxonomy
{
    public long ActionTypeId { get; private set; }
    public long CategoryId { get; private set; }
    public long SubCategoryId { get; private set; }

    public PostTaxonomy(long actionTypeId, long categoryId, long subCategoryId)
    {
        ActionTypeId = actionTypeId;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
    }
}
