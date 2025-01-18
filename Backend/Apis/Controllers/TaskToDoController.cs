using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Models.Entities;
using Services.Services;
using Services.Utils;

namespace Apis.Controllers
{
    [Route("api/task")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TaskToDoController : ControllerBase
    {
        private readonly TaskToDoService _taskToDoService;
        private readonly IValidator<TaskToDo> _validator;

        public TaskToDoController(TaskToDoService TaskToDoService, IValidator<TaskToDo> validator)
        {
            _taskToDoService = TaskToDoService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<WebResponseDto<List<TaskToDo>>>> GetTasks(string description, bool? completed)
        {

            List<TaskToDo> tasks = await _taskToDoService.GetAll();

            if (completed.HasValue)
            {
                tasks = tasks.FindAll(task => task.Completed == completed);
            }

            if (!string.IsNullOrEmpty(description))
            {
                tasks = tasks.FindAll(task =>
                    task.Description.ToLower().StartsWith(description.ToLower()) ||
                    task.Description.ToLower().Contains(description.ToLower()));
            }

            return Ok(new WebResponseDto<List<TaskToDo>>
            {
                Code = HttpUtils.httpStatusDto.Code,
                Message = HttpUtils.httpStatusDto.Message,
                Body = tasks
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WebResponseDto<TaskToDo>>> GetTask(int id)
        {
            var taskFound = await _taskToDoService.GetBy(id);

            return Ok(new WebResponseDto<TaskToDo>
            {
                Code = HttpUtils.httpStatusDto.Code,
                Message = HttpUtils.httpStatusDto.Message,
                Body = taskFound
            });
        }

        [HttpPost]
        public async Task<ActionResult<WebResponseDto<TaskToDo>>> AddTask([FromBody] TaskToDo task)
        {
            var validationResult = await _validator.ValidateAsync(task);

            if (!validationResult.IsValid)
            {
                return BadRequest(new WebResponseDto<TaskToDo>
                {
                    Code = HttpUtils.CODE_BAD_REQUEST,
                    Message = HttpUtils.HTTPS_STATUS[HttpUtils.CODE_BAD_REQUEST].Replace("{0}", validationResult.Errors[0].ToString()),
                    Body = new TaskToDo()
                });
            }

            var taskInserted = await _taskToDoService.Create(task);

            return Ok(new WebResponseDto<TaskToDo>
            {
                Code = HttpUtils.httpStatusDto.Code,
                Message = HttpUtils.httpStatusDto.Message,
                Body = taskInserted
            });
        }

        [HttpPut]
        public async Task<ActionResult<WebResponseDto<TaskToDo>>> UpdateTask([FromBody] TaskToDo task)
        {
            var validationResult = await _validator.ValidateAsync(task);

            if (!validationResult.IsValid)
            {
                return BadRequest(new WebResponseDto<TaskToDo>
                {
                    Code = HttpUtils.CODE_BAD_REQUEST,
                    Message = HttpUtils.HTTPS_STATUS[HttpUtils.CODE_BAD_REQUEST].Replace("{0}", validationResult.Errors[0].ToString()),
                    Body = new TaskToDo()
                });
            }

            var taskUpdated = await _taskToDoService.Update(task);

            return Ok(new WebResponseDto<TaskToDo>
            {
                Code = HttpUtils.httpStatusDto.Code,
                Message = HttpUtils.httpStatusDto.Message,
                Body = taskUpdated
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<WebResponseDto<TaskToDo>>> DeleteTask(int id)
        {
            var taskDeleted = await _taskToDoService.Delete(id);

            return Ok(new WebResponseDto<TaskToDo>
            {
                Code = HttpUtils.httpStatusDto.Code,
                Message = HttpUtils.httpStatusDto.Message,
                Body = taskDeleted
            });
        }
    }
}
