using System.Text.Json.Serialization;

namespace Entities.Models;
public class BaseResponse<T>
{
    public bool Success { get; set; } = false;
    public string Message { get; set; }
    public T Data { get; set; }
    public string Code { get; set; }

    [JsonConstructor]
    public BaseResponse(T data, bool success)
    {
        Data = data;
        Success = success;
    }
    public BaseResponse(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public BaseResponse(T data, bool success, string message) : this(data, success)
    {
        Message = message;
    }
    public BaseResponse(T data, bool success, string message, string code) : this(data, success, message)
    {
        Code = code;
    }
}


public class BaseResponse
{
    public bool Success { get; set; } = false;
    public string Message { get; set; }

    [JsonConstructor]
    public BaseResponse(bool success)
    {
        Success = success;
    }

    public BaseResponse(bool success, string message) : this(success)
    {
        Message = message;
    }
}