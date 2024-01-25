using AvaloniaXT.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XT.Common.Converters;
using XT.Common.Extensions;
using XT.Common.Interfaces;
using XT.Common.Models.Server;
using XT.Common.Services;
using XTExternalPage.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XTExternalPage.Apis
{
    public class EcsTagApi : BaseApiService
    {
        private IApiConfig _config;
        public EcsTagApi(IHttpClientFactory httpClientFactory, IApiConfig apiConfig) : base(httpClientFactory, apiConfig)
        {
            _config = apiConfig;
        }
        /// <summary>
        /// 获取tag点位
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public async Task<EcsMainView> GetTags(string viewName)
        {
            //var options = UrlUtilities.GetJsonOptions();
            //options.TypeInfoResolver = ApiJsonContext.Default;
            //var result = JsonSerializer.Deserialize<AdminCodeResult<EcsMainView>>(data, options);
            return new EcsMainView
            {
                ColNum = 40,
                RowHeight = 20,
                Tags = new List<ViewTag>
                {
                    new ViewTag
                    {
                        Code="113",
                        Condition="xx",
                        X=1,
                        Y=1,
                        H=2,
                        W=2,
                        Type=EquipTypeEnum.Other
                    },
                     new ViewTag
                    {
                        Code="115",
                        Condition="yy",
                        X=4,
                        Y=1,
                        H=2,
                        W=2,
                        Type=EquipTypeEnum.Other
                    },
                }
            };
        }


        /// <summary>
        /// 转换点位
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public List<UnitTagModel> ChangeToUnits(EcsMainView view)
        {
          
           
            List<UnitTagModel> units = new List<UnitTagModel>();
            foreach (var tag in view.Tags)
            {

                units.Add(new UnitTagModel
                {
                    Id = tag.Code,
                    Name = tag.Code.Substring(tag.Code.Length - 2, 2),
                    Height = tag.H * 20,
                    Width = tag.W * 20,
                    Top = tag.Y * 20,
                    Left = tag.X * 20,
                    Type = tag.Type,
                    IsCondition = tag.IsCondition,
                    Condition = tag.Condition
                });
            }

         
            return units;
        }
        /// <summary>
        /// 获取view
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>

        public UnitTagModel GetView(EcsMainView view)
        {
            UnitTagModel tag = new UnitTagModel();
            var maxY = view.Tags.Max(x => x.Y);
            var maxH = view.Tags.Max(x => x.H);
            tag.Height = (maxY + maxH) * 20;
            tag.Width = view.ColNum * 20;

            return tag;
        }
    }
}
