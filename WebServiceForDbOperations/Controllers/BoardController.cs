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
    public class BoardController : ControllerBase
    {
        private DataContext _context { get; set; }
        public BoardController(DataContext context)
        {
            _context = context;
        }
        [Route("add"), HttpPost]
        public async Task<IActionResult> AddBoard([FromBody] BoardModel board) 
        {
            return await Task.Run(() => { 
                this._context.Boards.Add(new AppBoard { 
                    BoardKey = board.BoardKey,
                    UrlImage = board.UrlImage
                });
                this._context.SaveChanges();

                return Ok(JsonConvert.SerializeObject("Додано!"));
            });
        }
        [HttpPut, Route("update")]
        public async Task<IActionResult> UpdateBoard([FromBody] BoardModel board)
        {
            return await Task.Run(() =>
            {
                IActionResult res = BadRequest(JsonConvert.SerializeObject("Не існує!"));
                if (!string.IsNullOrEmpty(board.UrlImage))
                {
                    var appboard = _context.Boards.FirstOrDefault(obj => obj.BoardKey == board.BoardKey);
                    if (appboard != null) 
                    {
                        appboard.UrlImage = board.UrlImage;
                        this._context.SaveChanges();
                        res =  Ok(JsonConvert.SerializeObject("Редаговано!"));
                    }

                }

                return res;
            });


        }
        [Route("get"), HttpGet]
        public async Task<IActionResult> GetBoards() 
        {
            return await Task.Run(() => { 
                return Ok(JsonConvert.SerializeObject(_context.Boards.ToList()));
            });
        }
        [Route("remove"), HttpDelete]
        public async Task<IActionResult> RemoveBoard([FromBody] BoardModel model)
        {
            return await Task.Run(() =>
            {
                IActionResult res = BadRequest(JsonConvert.SerializeObject("Не існує!"));
                var board = _context.Boards.FirstOrDefault(b => b.BoardKey == model.BoardKey);

                if (board != null)
                {
                    this._context.Boards.Remove(board);
                    this._context.SaveChanges();
                    res = Ok(JsonConvert.SerializeObject("Видалено!"));
                }
                return res;
            });

        }
    }
}
