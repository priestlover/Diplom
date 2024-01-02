namespace Diplom.Models.Authorization
{
    public enum StatusCode
    {
        UserNotFound = 0,
        UserAlreadyExists = 1,

        GameNotFound = 10,

        OrderNotFound = 20,

        OK = 200,
        InternalServerError = 500
    }
}
