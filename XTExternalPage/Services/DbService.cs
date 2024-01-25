using AvaloniaXT.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTExternalPage.Models;

namespace XTExternalPage.Services
{
    public class DbService
    {
        
        public DbService()
        {
           
        }

     //   private IFreeSql GetDb()
     //   {
     //       Enum.GetValues(typeof(EquipTypeEnum));
           
     //       var db = new FreeSql.FreeSqlBuilder()
     //.UseConnectionString(FreeSql.DataType.PostgreSQL, $"Host=localhost; Port=5432; Database=postgres; Username=postgres; Password=123456")
     //.UseNoneCommandParameter(true)
     ////.UseMonitorCommand(cmd => Console.WriteLine(cmd.CommandText + "\r\n"))
     //.Build();
     //       return db;
     //   }
      
     //   public async Task<List<ViewTag>> GetViewTags(string name)
     //   {
     //       using (var db = GetDb())
     //       {
     //           var viewId = await db.Ado.QuerySingleAsync<long>("select id from net_mainview_iot where name=@Name order by orderno", new { Name = name });
     //           var tags = await db.Ado.QueryAsync<ViewTag>("select * from net_viewtag_iot where viewid=@Id", new { Id = viewId });
     //           return tags;
     //       }
                
          
     //   }
    }
}
