using MediatR;

using Microsoft.AspNetCore.Mvc;
using BackEnd.Application.Common.Models;
using BackEnd.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BackEnd.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected ErrorView _errorView;

    [ApiExplorerSettings(IgnoreApi = true)]
    protected ErrorView HandleError(Exception ex)
    {
        string errorMsg = ex.Message;
        if (ex.InnerException != null)
            errorMsg += "\n" + ex.InnerException.Message;
        _errorView.Message.Add(errorMsg);
        _errorView.ModelStateError = GenModelStateError(ModelState);
        if (ex.GetType().Name == typeof(ValidationException).Name)
        {
            ValidationException vex = (ValidationException)ex;
            _errorView.ModelStateError = GenInsideError(vex);
        }
        throw ex;
        //return _errorView;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected object GenInsideError(ValidationException exception)
    {
        var errorList = exception.Errors
               .ToDictionary(
                   kvp => kvp.Key,
                   kvp => kvp.Value
               );

        return errorList;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected object GenModelStateError(ModelStateDictionary modelState)
    {
        var errorList = modelState.Where(x => x.Value.Errors.Count > 0)
               .ToDictionary(
                   kvp => kvp.Key,
                   kvp => kvp.Value.Errors.Select(x => x.ErrorMessage).ToList()
               );

        return errorList;
    }

    protected SuccessModel Send(bool Success, object Id, string Message = "")
    {
        return new SuccessModel()
        {
            Success = true,
            Id = Id,
            Message = Message
        };
    }
}
