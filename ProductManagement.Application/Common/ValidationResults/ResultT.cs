using System.Net;

namespace ProductManagement.Application.Common.ValidationResults;

public class Result<T>: Result
{
    private readonly T _value;
    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");


    protected internal Result(T value, bool isSuccess, Error error, HttpStatusCode? statusCode = null) 
        : base(isSuccess, error, statusCode)
    {
        _value = value;
    }

    public static Result<T> Success(T value) => new(value,true, null,HttpStatusCode.OK);

    public static Result<T> NotFound(Error error) => new( default,false, error, HttpStatusCode.NotFound);

    public static Result<T> Unauthorized(Error error) => new( default,false, error, HttpStatusCode.Unauthorized);

    public new static Result<T> Failure(Error error) => new( default,false, error, HttpStatusCode.BadRequest);
    
    public static implicit operator Result<T>(T value) => Create(value);

}