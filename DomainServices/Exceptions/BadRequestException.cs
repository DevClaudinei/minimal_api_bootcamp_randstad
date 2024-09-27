using System;

namespace minimal_api.DomainServices.Exceptions;

public class BadRequestException : Exception
{
	public BadRequestException(string errorMessage) : base(errorMessage) { }
}