using System.Collections.Generic;

namespace Wimc.Domain.Models.CostManagement
{
    public class ResultProperties
    {
        public IList<ResultColumn> Columns { get; set;  }
        public IList<IList<string>> Rows { get; set; }
    }
}