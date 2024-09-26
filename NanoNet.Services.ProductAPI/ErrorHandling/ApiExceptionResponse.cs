﻿namespace NanoNet.Services.ProductAPI.ErrorHandling;
public class ApiExceptionResponse(int statusCode, string? message = null, string? errors = null) : Error(statusCode, message)
{
    public string? Errors { get; set; } = errors;
}