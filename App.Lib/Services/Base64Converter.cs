using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Lib.Services
{
    public static class Base64Converter
    {
        public static string ConvertToBase64(Apps.Lib.Models.TaskModel task) 
        {
           return Convert.ToBase64String(Encoding.UTF8.GetBytes(
               JsonConvert.SerializeObject(task)));
        }

        public static Apps.Lib.Models.TaskModel ConvertToTask(string base64) 
        {
            string trimedString = base64.Trim('\0');
            return JsonConvert.DeserializeObject<Apps.Lib.Models.TaskModel>(
                Encoding.UTF8.GetString(Convert.FromBase64String(trimedString)));
        }
    }
}
