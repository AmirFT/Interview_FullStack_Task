using Microsoft.AspNetCore.Mvc;
using BackEnd.Application.Common.Models;
using BackEnd.Application.TodoItems.Commands.CreateTodoItem;
using BackEnd.Application.TodoItems.Commands.UpdateTodoItem;
using BackEnd.Application.TodoItems.Commands.DeleteTodoItem;
using System.Data;
using BackEnd.Application.TodoItems.Commands.UpdateTodoItemDetail;
using BackEnd.Application.TodoItems.Queries.Get;
using BackEnd.Application.TodoItems.Queries.List;

namespace BackEnd.WebUI.Controllers;

public class TaskController : ApiControllerBase
{
    
        [HttpPost]
        public async Task<ActionResult<SuccessModel>> Create([FromBody] CreateTodoItemCommand command)
        {
            try
            {
                var a = await Mediator.Send(command);
                return Send(true, a, "Task Created Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(HandleError(ex));
            }
        }

        [HttpPut]
        public async Task<ActionResult<SuccessModel>> Update([FromBody] UpdateTodoItemCommand command)
        {
            try
            {
                var a = await Mediator.Send(command);
                return Send(true, a, "Task Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(HandleError(ex));
            }
        }

        [HttpPut]
        public async Task<ActionResult<SuccessModel>> UpdateStatus([FromBody] UpdateStatusCommand command)
        {
            try
            {
                var a = await Mediator.Send(command);
                return Send(true, a, "Task Status Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(HandleError(ex));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SuccessModel>> Delete([FromRoute] DeleteTodoItemCommand command)
        {
            try
            {
                var a = await Mediator.Send(command);
                return Send(true, a, "Task Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(HandleError(ex));
            }
        }
        [HttpGet]
        public async Task<ActionResult<TodoItemDto>> Get([FromQuery] int id)
        {
            try
            {
                var result = await Mediator.Send(new GetTodoItemQuery { Id = id });
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(HandleError(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<TodoItemsDto>>> List([FromBody] GetTodoItemsQuery query)
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
