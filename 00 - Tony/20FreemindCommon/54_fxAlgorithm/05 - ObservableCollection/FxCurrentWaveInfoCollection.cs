using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Ecng.Collections;
using Ecng.Xaml;
using fx.Collections;
using fx.Definitions;

namespace fx.Algorithm
{
    /// <summary>
    ///
    /// 
    /// </summary>
    public class FxCurrentWaveInfoBindingList : ThreadSafeObservableCollection< FxCurrentWaveInfo >
    {
        public FxCurrentWaveInfoBindingList( IListEx< FxCurrentWaveInfo > items ) : base( items )
        {
        }
    }
}

