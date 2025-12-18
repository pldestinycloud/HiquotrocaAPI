namespace Hiquotroca.API.Application.Wrappers;

public class Error(ErrorCode errorcode, string description = null)
{
    public ErrorCode ErrorCode { get; set; } = errorcode;
    public string Description { get; set; } = description;
}
