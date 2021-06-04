using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServiceForDbOperations.Entities;
using Apps.Lib.Models;

namespace WebServiceForDbOperations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private DataContext _context { get; set; }
        public TaskController(DataContext context)
        {
            _context = context;
        }

        [HttpGet, Route("get")]
        public async Task<IActionResult> GetData() 
        {
            return await Task.Run(() => { return Ok(JsonConvert.SerializeObject(_context.Tasks.ToList())); });
        }

        [HttpPost, Route("add/{boardCode}")]
        public async Task<IActionResult> AddTask(string boardCode, [FromBody] TaskModel model) 
        {
            return await Task.Run(() => {
                IActionResult res = BadRequest(JsonConvert.SerializeObject("Не існує!"));
                int? boardId = _context.Boards.FirstOrDefault(x => x.BoardKey == boardCode)?.Id;
                if (boardId.HasValue) 
                {
                    this._context.Tasks.Add(new AppTask { 
                        BoardId = boardId.Value,
                        TaskText = model.TaskText,
                        Comment = model.Comment
                    });
                    this._context.SaveChanges();
                    res = Ok(JsonConvert.SerializeObject("Додано!"));
                }

                return res;
            });
        }

        [HttpPut, Route("update/{taskId}")]
        public async Task<IActionResult> UpdateTask(int taskId, [FromBody] TaskModel model) 
        {
            return await Task.Run(() => {
                IActionResult res = BadRequest(JsonConvert.SerializeObject("Не існує!"));
                var task = _context.Tasks.FirstOrDefault(x => x.Id == taskId);
                if (task != null) 
                {
                    task.TaskText = model.TaskText;
                    task.Comment = model.Comment;
                    this._context.SaveChanges();
                    res = Ok(JsonConvert.SerializeObject("Редаговано!"));
                }
                return res;
            });
        }

        [HttpDelete("delete/{taskId}")]
        public async Task<IActionResult> RemoveTask(int taskId) 
        {
            return await Task.Run(() => {
                IActionResult res = BadRequest(JsonConvert.SerializeObject("Не існує!"));
                var task = this._context.Tasks.FirstOrDefault(x => x.Id == taskId);
                if (task != null) 
                {
                    this._context.Tasks.Remove(task);
                    this._context.SaveChanges();
                    res =  Ok(JsonConvert.SerializeObject("Видалено!"));
                }

                return res;
            });
        }
    }
}
