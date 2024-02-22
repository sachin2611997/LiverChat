using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverChat.Models
{
  public  interface ICharts
    {
        void ProductWiseSales(out string CountList);
    }
}
