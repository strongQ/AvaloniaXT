using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTExternalPage.Models
{
    public class EcsMainView
    {
        

        public int RowHeight { get; set; }

        public int ColNum { get; set; }
        /// <summary>
        /// 点位数据
        /// </summary>
        public List<ViewTag> Tags { get; set; }
    }
}
