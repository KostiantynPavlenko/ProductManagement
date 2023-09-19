using System.Runtime.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace ProductManagement.Application.Common.Exceptions;

public class PipelineValidationException : ValidationException
{
    public PipelineValidationException(string message) : base(message)
    {
        
    }

    public PipelineValidationException(string message, IEnumerable<ValidationFailure> errors) : base(message, errors)
    {
    }

    public PipelineValidationException(string message, IEnumerable<ValidationFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
    {
    }

    public PipelineValidationException(IEnumerable<ValidationFailure> errors) : base(errors)
    {
    }

    public PipelineValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}