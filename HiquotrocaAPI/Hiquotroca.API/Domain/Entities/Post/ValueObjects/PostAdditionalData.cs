namespace Hiquotroca.API.Domain.Entities.Post.ValueObjects;

public class PostAdditionalData
{
    public string? Elements { get; private set; }
    public string? Caracteristics { get; private set; }
    public int? Duration { get; private set; }
    public int ViewCounter { get; private set; } = 0;
    public bool IsFeatured { get; private set; }
    public PostAdditionalData(string? elements, string? caracteristics, int? duration, bool isFeatured = false)
    {
        Elements = elements;
        Caracteristics = caracteristics;
        Duration = duration;
        IsFeatured = isFeatured;
    }

    public void IncrementViewCounter(int viewsNumber = 1)
    {
       ViewCounter += viewsNumber;
    }
}
