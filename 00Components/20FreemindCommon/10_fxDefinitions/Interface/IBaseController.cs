using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public interface IBaseController : IDisposable
    {
        bool StartWork( bool isMainDataSource, bool weekendMode );

        void StopWork( );

        string MainLoginName { get; set; }

        bool InitializeTimeZone( );

        bool Wait( int second );
    }
}
