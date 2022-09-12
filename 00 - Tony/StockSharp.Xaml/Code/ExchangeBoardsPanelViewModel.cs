using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Windows.Input;

internal sealed class ExchangeBoardsPanelViewModel : ViewModelBase
{
    private ICommand _commandAddNewBoard;
    private readonly ExchangeBoardsPanel _boardPanel;
    private ExchangeBoard _board;
    private TimeSpan _expiryTime;
    private TimeZoneInfo _boardTimeZone;
    private WorkingTime _workingTime;
    private bool _isNewBoard;
    private bool _updatingModel;
    private readonly IExchangeInfoProvider _provider;    
    private string _boardCode;

    public ExchangeBoardsPanelViewModel( ExchangeBoardsPanel panel, IExchangeInfoProvider provider )
    {        
        if ( panel == null )
        {
            throw new ArgumentNullException( "panel" );
        }

        _boardPanel = panel;
        IExchangeInfoProvider exchangeInfoProvider = provider;
        if ( exchangeInfoProvider == null )
        {
            throw new ArgumentNullException( "provider" );
        }

        _provider = exchangeInfoProvider;
        IsNewBoard = true;
    }

    public ICommand CommandAddNewBoard
    {
        get
        {
            return _commandAddNewBoard ?? ( _commandAddNewBoard = new DelegateCommand( o => CmdAddNewBoard( _boardPanel.SelectedExchange ), o => true ) );
        }
    }

    private IExchangeInfoProvider Provider
    {
        get
        {
            return _provider;
        }

    }

    private IEnumerable<ExchangeBoard> Boards
    {
        get
        {
            return ( IEnumerable<ExchangeBoard> )_boardPanel.Boards;
        }

    }

    public event Action DataChanged;

    public void SetBoard( string boardCode )
    {
        _updatingModel = true;

        try
        {
            ExchangeBoard newBoard = Boards.FirstOrDefault( b => b.Code == boardCode );

            if ( newBoard != null && ReferenceEquals( newBoard, Board ) )
                return;

            Reset( );

            if ( boardCode.IsEmpty( ) )
                return;

            boardCode = boardCode.ToUpperInvariant( );
            BoardCode = boardCode;
            Board = newBoard;

            IsNewBoard = Board == null;

            if ( !IsNewBoard )
            {
                LoadFromBoard( );
            }

            if ( IsNewBoard )
            {
                _boardPanel.SetSaveError( LocalizedStrings.Str1425, false );
            }
        }
        finally
        {
            _updatingModel = false;
        }
    }

    public void SaveBoard( Exchange exchange )
    {
        if ( _updatingModel || IsNewBoard || Board == null || !IsBoardValid( ) || exchange == null )
        {
            return;
        }

        SetBoardData( Board );
        Board.Exchange = exchange;
        Board.Code = ( BoardCode );
        Provider.Save( Board );
    }

    private void LoadFromBoard( )
    {
        ExpiryTime = Board.ExpiryTime;
        BoardTimeZone = Board.TimeZone;
        WorkingTime = Board.WorkingTime.Clone( );
    }

    private void Reset( )
    {
        Board = ( ExchangeBoard )null;
        BoardCode = ( string )null;
        IsNewBoard = true;
        BoardTimeZone = ( TimeZoneInfo )null;
        WorkingTime = ( WorkingTime )null;
        ExpiryTime = TimeSpan.Zero;
    }

    public ExchangeBoard Board
    {
        get
        {
            return _board;
        }
        private set
        {
            SetField<ExchangeBoard>( ref _board, value, ( Expression<Func<ExchangeBoard>> )( ( ) => Board ), false );
        }
    }

    public string BoardCode
    {
        get
        {
            return _boardCode;
        }
        private set
        {
            _boardCode = value;
        }
    }

    public bool IsNewBoard
    {
        get
        {
            return _isNewBoard;
        }
        set
        {
            SetField<bool>( ref _isNewBoard, value, ( Expression<Func<bool>> )( ( ) => IsNewBoard ), false );
        }
    }

    public TimeSpan ExpiryTime
    {
        get
        {
            return _expiryTime;
        }
        set
        {
            SetField<TimeSpan>( ref _expiryTime, value, ( Expression<Func<TimeSpan>> )( ( ) => ExpiryTime ), true );
        }
    }

    public TimeZoneInfo BoardTimeZone
    {
        get
        {
            return _boardTimeZone;
        }
        set
        {
            SetField<TimeZoneInfo>( ref _boardTimeZone, value, ( Expression<Func<TimeZoneInfo>> )( ( ) => BoardTimeZone ), true );
        }
    }

    public WorkingTime WorkingTime
    {
        get
        {
            return _workingTime;
        }
        set
        {
            SetField<WorkingTime>( ref _workingTime, value, ( Expression<Func<WorkingTime>> )( ( ) => WorkingTime ), true );
        }
    }

    private void CmdAddNewBoard( Exchange exchange )
    {
        if ( ( Equatable<Exchange> )exchange == ( Exchange )null )
        {
            _boardPanel.SetSaveError( LocalizedStrings.Str1426, false );
        }
        else
        {
            if ( !IsNewBoard || !IsBoardValid( ) )
            {
                return;
            }

            Board = CreateBoardFromData( );
            Board.Exchange = exchange;
            IsNewBoard = false;
            _boardPanel.SetSaveError( ( string )null, false );
            Provider.Save( Board );
        }
    }

    private bool IsBoardValid( )
    {
        if ( BoardCode.IsEmpty( ) )
        {
            _boardPanel.SetSaveError( LocalizedStrings.Str1433, false );
            return false;
        }

        if ( BoardTimeZone != null )
        {
            return true;
        }

        _boardPanel.SetSaveError( LocalizedStrings.Str1465, false );

        return false;
    }

    private ExchangeBoard CreateBoardFromData( )
    {
        ExchangeBoard board = new ExchangeBoard( );
        SetBoardData( board );
        return board;
    }

    private void SetBoardData( ExchangeBoard board )
    {
        board.Code        = BoardCode;
        board.ExpiryTime  = ExpiryTime;
        board.WorkingTime = WorkingTime.Clone( );
        board.TimeZone    = BoardTimeZone;
    }

    private void SetField<T>( ref T field, T value, Expression<Func<T>> selectorExpression, bool vmDataChanged )
    {
        bool changed = SetField<T>( ref field, value, selectorExpression );

        if ( !( vmDataChanged & changed ) )
        {
            return;
        }

        OnDataChanged( );
    }

    private void OnDataChanged( )
    {
        if ( !_updatingModel )
        {
            DataChanged?.Invoke( );
        }
    }   
}
