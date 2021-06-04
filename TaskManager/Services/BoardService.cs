using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaskManagerUI.Services
{
    public static class BoardService
    {
        public static bool DEBUG_PROGRAM = false;
        public static void ShowWebException(WebException web)
        {
            WebResponse response = (HttpWebResponse)web.Response;
            using (StreamReader r = new StreamReader(response.GetResponseStream()))
            {
                string json = r.ReadToEnd();
                MessageBox.Show(json);
            }
        }
        public static void CreateBoard(string code)
        {
            RequestBody(MainWindow.WebServiceUrl +
                "api/board/add", "POST", "application/json", code);
        }
        public static void DeleteBoard(string code) 
        {
            RequestBody(MainWindow.WebServiceUrl +
                "api/board/remove", "DELETE", "application/json", code);
        }

        public async static Task<Apps.Lib.Models.BoardModel> GetBoard(string code) 
        {
            return await Task.Run(() => { 
            Apps.Lib.Models.BoardModel board = new Apps.Lib.Models.BoardModel();
            WebRequest request = WebRequest.CreateHttp(MainWindow.WebServiceUrl +
                            "api/board/get");

            request.Method = "GET";
            request.ContentType = "application/json";
            try
            {
                using (StreamReader sr = new StreamReader(((HttpWebResponse)request.GetResponse())
                    .GetResponseStream()))
                {
                    string json = sr.ReadToEnd();
                    List<Apps.Lib.Models.BoardModel> boards =
                    JsonConvert.DeserializeObject<List<Apps.Lib.Models.BoardModel>>(json);
                        board = boards.FirstOrDefault(x => x.BoardKey == code);
                }
            }
            catch (WebException web)
            {
                WebResponse response = (HttpWebResponse)web.Response;
                using (StreamReader r = new StreamReader(response.GetResponseStream()))
                {
                    string json = r.ReadToEnd();
                    MessageBox.Show(json);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return board;
            });
        }

        public async static Task<List<Apps.Lib.Models.TaskModel>> GetTasks(int id) 
        {
            return await Task.Run(() => {
                List<Apps.Lib.Models.TaskModel> tasks = new List<Apps.Lib.Models.TaskModel>();
                WebRequest request = (HttpWebRequest)WebRequest.CreateHttp(MainWindow.WebServiceUrl +
                        "api/task/get");

                request.Method = "GET";
                request.ContentType = "application/json";

                try
                {
                    using (StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream()))
                    {
                        string json = sr.ReadToEnd();
                        tasks = JsonConvert.DeserializeObject<List<Apps.Lib.Models.TaskModel>>(json);
                    }
                }
                catch (WebException web)
                {
                    BoardService.ShowWebException(web);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                var retTasks = tasks.Where(x => x.BoardId == id).ToList();
                return retTasks;
            });
        }

        private static void RequestBody(string path, string method, string contentType,  string code) 
        {
            WebRequest request = WebRequest.CreateHttp(path);

            request.Method = method;
            request.ContentType = contentType;

            using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.Write(JsonConvert.SerializeObject(new
                {
                    BoardKey = code,
                    UrlImage = @"\Images\background.jpg"
                }));
            }
            try
            {
                using (StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream()))
                {

                    string json = sr.ReadToEnd();
                    if (DEBUG_PROGRAM)
                    {
                        MessageBox.Show(JsonConvert.DeserializeObject<string>(json));
                    }

                }
            }
            catch (WebException web)
            {
                ShowWebException(web);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
