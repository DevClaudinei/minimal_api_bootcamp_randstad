using System;

namespace minimal_api.DomainServices.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string errorMessage) : base(errorMessage) { }
}