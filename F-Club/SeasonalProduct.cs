using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_Club
{
    class SeasonalProduct : Product
    {
        private string _seasonStartDate;
        private string _seasonEndDate;

        public string SeasonStartDate { get { return _seasonStartDate; } set { _seasonStartDate = value; if (value == null) this.Active = true; } }
        public string SeasonEndDate { get { return _seasonEndDate; } set { _seasonEndDate = value; if (value == null) this.Active = true; } }
    }
}
