using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaXT.Interfaces
{
    public interface IMenuToolService
    {
        /// <summary>
        /// 是否显示搜索
        /// </summary>
        public bool ShowSearch { get; set; }
       

        public bool ShowAudio { get; set; }
       
        /// <summary>
        /// 点击搜索
        /// </summary>
        Task SearchClick();
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> Login(string username, string password);
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        Task Initial();
        /// <summary>
        /// 获取主要标题
        /// </summary>
        /// <returns></returns>
        string GetMainTitle();
        
    }
}
