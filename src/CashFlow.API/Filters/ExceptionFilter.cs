﻿using CashFlow.Communication.Responses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is CashFlowException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknowError(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var cashFlowException = (CashFlowException)context.Exception; // ou as CashFlowException, para uso de 
        var errorResponse = new ResponseErrorsJson(cashFlowException.GetErrors());


        context.HttpContext.Response.StatusCode = cashFlowException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
        //if (context.Exception is ErrorOnValidationException errorOnValidationException)
        //{
        //    var errorResponse = new ResponseErrorsJson(errorOnValidationException.Errors);

        //    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        //    context.Result = new BadRequestObjectResult(errorResponse);

        //}
        //else if(context.Exception is NotFoundException notFoundException)
        //{
        //    var errorResponse = new ResponseErrorsJson(notFoundException.Message);
        //    context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        //    context.Result = new NotFoundObjectResult(errorResponse);
        //} 
        //else
        //{
        //    var errorResponse = context.Exception.Message;

        //    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        //    context.Result = new BadRequestObjectResult(errorResponse);
        //}
    }
    private void ThrowUnknowError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorsJson(ResourceErrorMessages.UNKOWN_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }


}