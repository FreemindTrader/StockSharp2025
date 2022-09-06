using SciChart.Data.Model;
using System;
using System.Collections;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Charting.ATony
{
    interface ISciReadOnlyList<T> : ISciList<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
    }
}
