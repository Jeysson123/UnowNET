using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.Dtos;
using Models.Entities;
using Services.Interfaces;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class TaskToDoService : ITaskToDoService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TaskToDoService> _logger;

        public TaskToDoService(ApplicationDbContext context, ILogger<TaskToDoService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<TaskToDo>> GetAll()
        {
            try
            {
                var listTasks = await _context.Task.ToListAsync();

                if(listTasks.Count == 0)
                {
                    HttpUtils.httpStatusDto = new HttpStatusDto { Code = HttpUtils.CODE_NO_CONTENT, Message = HttpUtils.HTTPS_STATUS[HttpUtils.CODE_OK].Replace("{0}", "List Tasks Empty.") };
                   
                    return Enumerable.Empty<TaskToDo>().ToList();

                } else
                {
                    HttpUtils.httpStatusDto = new HttpStatusDto { Code = HttpUtils.CODE_OK, Message = HttpUtils.HTTPS_STATUS[HttpUtils.CODE_OK].Replace("{0}", "List Tasks Found.") };
                }

                return listTasks;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching task");
                HttpUtils.httpStatusDto = new HttpStatusDto { Code = HttpUtils.CODE_INTERNAL_ERROR, Message = HttpUtils.HTTPS_STATUS[HttpUtils.CODE_OK].Replace("{0}", ex.Message) };
                return Enumerable.Empty<TaskToDo>().ToList();
            }
        }

        public async Task<TaskToDo> GetBy(object id)
        {
            try
            {
                var taskFound = await _context.Task.FirstOrDefaultAsync(tsk => tsk.Id == (int) id);

                if (taskFound == null)
                {
                    HttpUtils.httpStatusDto = new HttpStatusDto { Code = HttpUtils.CODE_NO_CONTENT, Message = HttpUtils.HTTPS_STATUS[HttpUtils.CODE_OK].Replace("{0}", "Task not Found.") };

                    return new TaskToDo();

                }
                else
                {
                    HttpUtils.httpStatusDto = new HttpStatusDto { Code = HttpUtils.CODE_OK, Message = HttpUtils.HTTPS_STATUS[HttpUtils.CODE_OK].Replace("{0}", "Task Found.") };
                }

                return taskFound;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting task");
                HttpUtils.httpStatusDto = new HttpStatusDto { Code = HttpUtils.CODE_INTERNAL_ERROR, Message = HttpUtils.HTTPS_STATUS[HttpUtils.CODE_OK].Replace("{0}", ex.Message) };
                return new TaskToDo();
            }
        }

        public async Task<TaskToDo> Create(TaskToDo task)
        {
            try
            {
                _context.Task.Add(task);

                await _context.SaveChangesAsync();

                HttpUtils.httpStatusDto = new HttpStatusDto { Code = HttpUtils.CODE_OK, Message = HttpUtils.HTTPS_STATUS[HttpUtils.CODE_OK].Replace("{0}", "Task Inserted.") };

                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding task");
                HttpUtils.httpStatusDto = new HttpStatusDto { Code = HttpUtils.CODE_INTERNAL_ERROR, Message = HttpUtils.HTTPS_STATUS[HttpUtils.CODE_OK].Replace("{0}", ex.Message) };
                return new TaskToDo();
            }
        }

        public async Task<TaskToDo> Update(TaskToDo task)
        {
            try
            {
                _context.Task.Update(task);

                await _context.SaveChangesAsync();

                HttpUtils.httpStatusDto = new HttpStatusDto { Code = HttpUtils.CODE_OK, Message = HttpUtils.HTTPS_STATUS[HttpUtils.CODE_OK].Replace("{0}", "Task Updated.") };

                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task");
                HttpUtils.httpStatusDto = new HttpStatusDto { Code = HttpUtils.CODE_INTERNAL_ERROR, Message = HttpUtils.HTTPS_STATUS[HttpUtils.CODE_OK].Replace("{0}", ex.Message) };
                return new TaskToDo();
            }
        }

        public async Task<TaskToDo> Delete(object id)
        {
            try
            {
                var taskToDelete = await _context.Task.FindAsync(id);

                if (taskToDelete == null)
                {
                    HttpUtils.httpStatusDto = new HttpStatusDto { Code = HttpUtils.CODE_NO_CONTENT, Message = HttpUtils.HTTPS_STATUS[HttpUtils.CODE_OK].Replace("{0}", "Task not Found.") };

                    return new TaskToDo();

                } else
                {
                    HttpUtils.httpStatusDto = new HttpStatusDto { Code = HttpUtils.CODE_OK, Message = HttpUtils.HTTPS_STATUS[HttpUtils.CODE_OK].Replace("{0}", "Task Deleted.") };
                  
                    _context.Task.Remove(taskToDelete);

                    await _context.SaveChangesAsync();
                }

                return taskToDelete;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task");
                HttpUtils.httpStatusDto = new HttpStatusDto { Code = HttpUtils.CODE_INTERNAL_ERROR, Message = HttpUtils.HTTPS_STATUS[HttpUtils.CODE_OK].Replace("{0}", ex.Message) };
                return new TaskToDo();
            }
        }
    }
}
