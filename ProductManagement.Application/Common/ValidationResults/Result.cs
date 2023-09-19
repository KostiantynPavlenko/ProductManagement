using System.Net;

namespace ProductManagement.Application.Common.ValidationResults;


public class Result
{
    protected Result(bool isSuccess, Error error, HttpStatusCode? statusCode = null)
    {
        IsSuccess = isSuccess;
        Error = error;
        StatusCode = statusCode;
    }
    
    public HttpStatusCode? StatusCode { get; }
    protected bool IsSuccess { get;}
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public static Result Success() => new(true, Error.None, HttpStatusCode.OK);
    private static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
    
    public static Result UnprocessableEntity(Error error) => new(false, error, HttpStatusCode.UnprocessableEntity);
    public static Result Failure(Error error) => new(false, error, HttpStatusCode.BadRequest);
    private static Result<TValue> Failure<TValue>(Error error) => new(default,false, error);
    protected static Result<TValue> Create<TValue>(TValue value) => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
}