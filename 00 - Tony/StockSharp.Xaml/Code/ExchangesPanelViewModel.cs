using DevExpress.Mvvm;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Windows.Input;

internal sealed class ExchangesPanelViewModel : ViewModelBase
{
    public event Action DataChanged;

    private ICommand                       _commandAddNewExchange;
    private readonly IExchangeInfoProvider _provider;
    private readonly ExchangesPanel        _exchangesPanel;
    private string                         _exchangeName;
    private Exchange                       _exchange;
    private string                         _exchangeRusName;
    private string                         _exchangeEngName;
    private string                         _saveErrorMessage;
    private CountryCodes?                  _countryCode;

    private bool _isNew;

    private bool _updatingModel;




    public ExchangesPanelViewModel( ExchangesPanel viewPanel, IExchangeInfoProvider provider )
    {
        if ( viewPanel == null )
        {
            throw new ArgumentNullException( "panel" );
        }

        _exchangesPanel = viewPanel;

        if ( provider == null )
        {
            throw new ArgumentNullException( "provider" );
        }

        _provider = provider;
        _isNew = true;
    }

    public ICommand CommandAddNewExchange
    {
        get
        {
            return _commandAddNewExchange ?? ( _commandAddNewExchange = new DelegateCommand( o => CmdAddNewExchange(), o => true ) );
        }
    }

    //https://www.reddit.com/r/csharp/comments/445eb0/can_i_get_a_reality_check_on_how_this_event_safe/
    public void ThreadedSafeAddEventHandle( Action newAction )
    {
        Action action = DataChanged;
        Action comparand;
        do
        {
            comparand = action;
            action = Interlocked.CompareExchange<Action>( ref DataChanged, ( Action ) Delegate.Combine( comparand, newAction ) , comparand );
        }
        while ( action != comparand );
    }

    public void ThreadedSafeRemoveEventHandle( Action newAction )
    {
        Action action = DataChanged;
        Action comparand;
        do
        {
            comparand = action;
            action = Interlocked.CompareExchange<Action>( ref DataChanged, comparand - newAction, comparand );
        }
        while ( action != comparand );
    }

    private IExchangeInfoProvider Provider =>  _provider;
    
    private IEnumerable<Exchange> Exchanges => _exchangesPanel.Exchanges;
    

    public void SetExchange( string exchangeName )
    {
        if ( exchangeName.IsEmpty() )
        {
            Reset();
        }
        else
        {
            exchangeName = exchangeName.ToUpperInvariant();
            ExchangeName = exchangeName;

            Exchange = Exchanges.FirstOrDefault<Exchange>( e => e.Name == exchangeName );
            if ( Exchange != null )
            {
                LoadFromExchange();
            }
            else
            {
                Reset();
                ExchangeName = exchangeName;
            }
        }
        IsNew = Exchange == null;
        SaveErrorMessage = IsNew ? LocalizedStrings.Str1460 : ( string ) null;
    }

    private void LoadFromExchange( )
    {
        _updatingModel = true;
        try
        {
            ExchangeRusName = Exchange.RusName;
            ExchangeEngName = Exchange.EngName;
            CountryCode = Exchange.CountryCode;
        }
        finally
        {
            _updatingModel = false;
        }
    }

    private void Reset( )
    {
        _updatingModel = true;
        try
        {
            Exchange = ( Exchange ) null;
            ExchangeName = ExchangeRusName = ExchangeEngName = string.Empty;
            CountryCode = null;
            IsNew = true;
        }
        finally
        {
            _updatingModel = false;
        }
    }

    public Exchange Exchange
    {
        get
        {
            return _exchange;
        }
        private set
        {
            SetField<Exchange>( ref _exchange, value, ( ) => Exchange, false );
        }
    }

    public string ExchangeName
    {
        get
        {
            return _exchangeName;
        }
        private set
        {
            _exchangeName = value;
        }
    }

    public bool IsNew
    {
        get
        {
            return _isNew;
        }
        private set
        {
            SetField<bool>( ref _isNew, value, ( ) => IsNew, false );
        }
    }

    public string ExchangeRusName
    {
        get
        {
            return _exchangeRusName;
        }
        set
        {
            SetField<string>( ref _exchangeRusName, value, ( ) => ExchangeRusName, true );
        }
    }

    public string ExchangeEngName
    {
        get
        {
            return _exchangeEngName;
        }
        set
        {
            SetField<string>( ref _exchangeEngName, value, ( ) => ExchangeEngName, true );
        }
    }

    public CountryCodes? CountryCode
    {
        get
        {
            return _countryCode;
        }
        set
        {
            SetField<CountryCodes?>( ref _countryCode, value, ( ) => CountryCode, true );
        }
    }

    public string SaveErrorMessage
    {
        get
        {
            return _saveErrorMessage;
        }
        set
        {
            SetField<string>( ref _saveErrorMessage, value, ( Expression<Func<string>> ) ( ( ) => SaveErrorMessage ), false );
        }
    }

    

    private bool IsExchangeDataValid( )
    {
        if ( ExchangeName.IsEmpty() )
        {
            SaveErrorMessage = LocalizedStrings.Str1461;
        }
        else if ( ExchangeRusName.IsEmpty() )
        {
            SaveErrorMessage = LocalizedStrings.Str1462;
        }
        else if ( ExchangeEngName.IsEmpty() )
        {
            SaveErrorMessage = LocalizedStrings.Str1463;
        }
        else if ( CountryCode == null )
        {
            SaveErrorMessage = LocalizedStrings.Str1464;
        }
        else
        {
            return true;
        }

        return false;
    }

    public void SaveExchange( )
    {
        if ( IsNew || Exchange == null || !IsExchangeDataValid() )
        {
            return;
        }

        Exchange exchange = CreateExchangeFromData();
        exchange.Name = ( Exchange.Name );
        Provider.Save( exchange );
    }

    private void CmdAddNewExchange( )
    {
        if ( !IsNew || !IsExchangeDataValid() )
        {
            return;
        }

        Exchange = CreateExchangeFromData();
        IsNew = false;
        SaveErrorMessage = null;
        Provider.Save( Exchange );
    }

    private Exchange CreateExchangeFromData( )
    {
        Exchange exchange = new Exchange();
        exchange.Name = ( ExchangeName );
        exchange.RusName = ( ExchangeRusName );
        exchange.EngName = ( ExchangeEngName );
        exchange.CountryCode = ( CountryCode );
        return exchange;
    }

    private void SetField<T>( ref T field, T value, Expression<Func<T>> selectorExpression, bool vmDataChanged = true )
    {
        bool changed = base.SetField<T>(ref field, value, selectorExpression);

        if ( !_updatingModel & vmDataChanged & changed )
        {
            DataChanged?.Invoke( );
        }        
    }

    
}
