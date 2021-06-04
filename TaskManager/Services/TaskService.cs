using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskManagerUI.Models;

namespace TaskManagerUI.Services
{
    public static class TaskService
    {
        public static Apps.Lib.Models.TaskModel GetTaskById(int id)
        {
            WebRequest request = WebRequest.CreateHttp(MainWindow.WebServiceUrl + "api/task/get");

            request.Method = "GET";
            request.ContentType = "application/json";

            using (StreamReader sw = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                string json = sw.ReadToEnd();
                List<Apps.Lib.Models.TaskModel> tasks = JsonConvert.DeserializeObject<List<Apps.Lib.Models.TaskModel>>(json);

                return tasks.FirstOrDefault(x => x.Id == id);
            }
        }
        public static void DeleteById(int id) 
        {
            WebRequest request = WebRequest.CreateHttp(MainWindow.WebServiceUrl + 
                "api/task/delete/" + id.ToString());

            request.Method = "DELETE";
            request.ContentType = "application/json";

            using (StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream())) 
            {
                string json = sr.ReadToEnd();
            }
        }

        public static Apps.Lib.Models.TaskModel UpdateTask(int id, string textQues = null, string comment = null, 
            Apps.Lib.Models.TaskModel oldTask = null) 
        {
            WebRequest request = (HttpWebRequest)WebRequest.CreateHttp(MainWindow.WebServiceUrl + "api/task/update/" + id.ToString());
            request.Method = "PUT";
            request.ContentType = "application/json";

                var newTask = new Apps.Lib.Models.TaskModel
                {
                    TaskText = string.IsNullOrEmpty(textQues) ? oldTask.TaskText : textQues,
                    Comment = string.IsNullOrEmpty(comment) ? oldTask.Comment : comment,
                };
            using (StreamWriter sw = new StreamWriter(request.GetRequestStream())) 
            {
                sw.Write(JsonConvert.SerializeObject(newTask));

            }
            try
            {
                using (StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream()))
                {
                    string json = sr.ReadToEnd();
                }
            } 
            catch (Exception ex) 
            {
                
            }

                return newTask;
        }

        public static void AddCars(Apps.Lib.Models.TaskModel model, BoardModel board) 
        {
            WebRequest request = WebRequest.CreateHttp("https://localhost:5001/api/task/add/" + board.BoardKey);

            request.Method = "POST";
            request.ContentType = "application/json";

            using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.Write(JsonConvert.SerializeObject(model));
            }
            try
            {
                using (StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream()))
                {
                    string json = sr.ReadToEnd();
                }
            } 
            catch (WebException web) 
            {
                
            }
        }

        public async static Task<int> GetLastIndex(int boardId) 
        {
            return await Task.Run(async () => { 
                var list = await BoardService.GetTasks(boardId);
                int cur = 0;

                foreach (var item in list) 
                {
                    if (item.Id > cur) 
                    {
                        cur = item.Id;
                    }
                }
                
                return cur;
            });
        }
    }
}
