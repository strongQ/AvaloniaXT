using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XT.Common.Models.Server;
using XTExternalPage.Models;

namespace XTExternalPage.Apis
{
   
    [JsonSerializable(typeof(AdminCodeResult<EcsMainView>))]
    public partial class ApiJsonContext: JsonSerializerContext
    {
    }
}
