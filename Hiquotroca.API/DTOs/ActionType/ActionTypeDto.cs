namespace Hiquotroca.API.DTOs.ActionType
{
    public class ActionTypeDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
    public class ActionTypeCreateRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
    public class ActionTypeUpdateRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }

}
