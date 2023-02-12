using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Persistence
{
    public class QueryFilter
    {
        public List<QueryCondition> Conditions { get; set; }
        public List<OrderByColumn>? OrderByColumns { get; set; }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
    }

    public class QueryCondition
    {
        public string Column { get; set; }
        public string Operator { get; set; } = "=";
        public object Value { get; set; }
    }

    public class OrderByColumn
    {
        public string Column { get; set; }
        public string Direction { get; set; } = "asc";
    }
}
