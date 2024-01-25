using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XTExternalPage.Models
{
    public class ViewTag
    {
        /// <summary>
        /// x轴
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// y轴
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public int W { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public int H { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [JsonPropertyName("id")]
        public string Code { get; set; }
        /// <summary>
        /// 类型 1站台 2堆垛机 
        /// </summary>
        public EquipTypeEnum Type { get; set; }

        /// <summary>
        /// int条件
        /// </summary>
        public int IsCondition { get; set; }

        /// <summary>
        /// string 条件
        /// </summary>
        public string Condition { get; set; }
        /// <summary>
        /// 命名
        /// </summary>
        public string Name { get; set; }
    }
}
