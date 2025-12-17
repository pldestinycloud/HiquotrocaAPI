namespace Hiquotroca.API.Application.Wrappers;

public enum ErrorCode : short
{
    ModelStateNotValid = 400,
    FieldDataInvalid = 422,
    NotFound = 404,
    AccessDenied = 403,
    ErrorInIdentity = 401,
    Exception = 500,
    InvalidAction = 409
}
