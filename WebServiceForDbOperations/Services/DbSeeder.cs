using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebServiceForDbOperations.Entities;

namespace WebServiceForDbOperations.Services
{
    public static class DbSeeder
    {
        public static void SeedAll(this IApplicationBuilder builder) 
        {
            using (var scope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope()) 
            {
                DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();
                SeedTestBoard(context);
            }
        }

        public static void SeedTestBoard(DataContext context) 
        {
            if (!context.Boards.Any()) 
            {
                var board = new AppBoard 
                { 
                    BoardKey = Path.GetRandomFileName().ToString(),
                    UrlImage = "https://drive.google.com/file/d/18qt1PFCdam_f7IekJ0Nh1QlVCz64JBIB/view?usp=sharing"
                };
                var tasks = new List<AppTask> { 
                    new AppTask
                    {
                        Board = board,
                        TaskText = "Прибрати в кімнаті",
                        Comment = "Попилососити і протерти пилюку",
                    },
                    new AppTask
                    {
                        Board = board,
                        TaskText = "Піти в спортзал",
                        Comment = "Взяти переодягання",
                    },
                };

                context.Boards.Add(board);
                context.Tasks.AddRange(tasks);
                context.SaveChanges();
            }
        }
    }
}
