using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Infrastructure.Utils
{
    public class Helper
    {
        public static string GetSqlCondition(string conditionClause, string conditionOperator)
        {
            if (string.IsNullOrWhiteSpace(conditionClause))
            {
                return " WHERE ";
            }
            else if (conditionOperator.ToUpper() == "AND")
            {
                return " AND ";
            }
            else if (conditionOperator.ToUpper() == "OR")
            {
                return " OR ";
            }
            else
            {
                return "";
            }
        }
    }
}
