using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN88.TotalBets.Shared.Models
{
    public class CustInfo
    {
        public CustInfo(
            int custId,
            string custName,
            byte custLevelId)
        {
            CustId = custId;
            CustName = custName;
            CustLevelId = custLevelId;
        }

        public int CustId { get; }

        public string CustName { get; }

        public byte CustLevelId { get; }
    }
}
