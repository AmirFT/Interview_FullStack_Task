using Microsoft.AspNetCore.Mvc;
using BackEnd.Application.Common.Models;
using BackEnd.Application.TodoItems.Commands.CreateTodoItem;
using BackEnd.Application.TodoItems.Commands.UpdateTodoItem;
using BackEnd.Application.TodoItems.Commands.DeleteTodoItem;
using System.Data;
using BackEnd.Application.TodoItems.Commands.UpdateTodoItemDetail;
using BackEnd.Application.TodoItems.Queries.Get;
using BackEnd.Application.TodoItems.Queries.List;
using Microsoft.AspNetCore.Authorization;
using BackEnd.Application.Employees.Queries.List;

namespace BackEnd.WebUI.Controllers;

[AllowAnonymous]
public class EmployeeController : ApiControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<EmployeeDTO>>> List([FromQuery] GetEmployeesQuery query)
    {
        try
        {
            return await Mediator.Send(query);
        }
        catch (Exception ex)
        {
            return BadRequest(HandleError(ex));
        }
    }
}
