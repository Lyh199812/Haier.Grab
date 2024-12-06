using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.Models.Recipe
{
    /// <summary>
    /// 配方详细信息
    /// </summary>
    public class RecipeInfo
    {
        /// <summary>
        /// 配方名称
        /// </summary>
        public string RecipeName { get; set; }
        /// <summary>
        /// 配方参数
        /// </summary>
        public List<RecipeParam> RecipeParams { get; set; } = new List<RecipeParam>();
    }
}
