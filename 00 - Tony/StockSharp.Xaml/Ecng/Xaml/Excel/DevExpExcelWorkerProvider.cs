// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Excel.DevExpExcelWorkerProvider
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Export.Xl;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Ecng.Xaml.Excel
{
    /// <summary>
    /// Implementation of the <see cref="T:Ecng.Interop.IExcelWorkerProvider" /> works with DevExpress excel processors.
    /// </summary>
    public class DevExpExcelWorkerProvider : IExcelWorkerProvider
    {
        IExcelWorker IExcelWorkerProvider.CreateNew(
          Stream _param1,
          bool _param2 )
        {
            return ( IExcelWorker )new DevExpExcelWorkerProvider.DevExpExcelWorker( _param1 );
        }

        IExcelWorker IExcelWorkerProvider.OpenExist(
          Stream _param1 )
        {
            return ( IExcelWorker )new DevExpExcelWorkerProvider.DevExpExcelWorker( _param1 );
        }

        private sealed class DevExpExcelWorker : IExcelWorker, IDisposable
        {

            private readonly IXlExporter _IXlExporter = XlExport.CreateExporter( ( XlDocumentFormat )0 );

            private readonly List<DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable> _myList = new List<DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable>();

            private readonly IXlDocument _IXlDocument;

            private DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable _devExpExcel;

            public DevExpExcelWorker( Stream _param1 )
            {
                this._IXlDocument = this._IXlExporter.CreateDocument( _param1 );
            }

            void IDisposable.Dispose()
            {
                this._myList.ForEach( DevExpExcelWorkerProvider.DevExpExcelWorker.Lamdba0003._myAction213 ?? ( DevExpExcelWorkerProvider.DevExpExcelWorker.Lamdba0003._myAction213 = new Action<DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable>( DevExpExcelWorkerProvider.DevExpExcelWorker.Lamdba0003._this.Lamdba0003M01 ) ) );
                this._myList.Clear();
                ( ( IDisposable )this._IXlDocument ).Dispose();
            }

            IExcelWorker IExcelWorker.SetCell<T>(
              int _param1,
              int _param2,
              T _param3 )
            {
                this._devExpExcel.SafeAdd023<T>( _param1, _param2, _param3 );
                return ( IExcelWorker )this;
            }

            T IExcelWorker.GetCell<T>(
              int _param1,
              int _param2 )
            {
                return this._devExpExcel.GetCellT<T>( _param1, _param2 );
            }

            IExcelWorker IExcelWorker.SetStyle(
              int _param1,
              Type _param2 )
            {
                this._devExpExcel._sortDictionary234[_param1] = new RefPair<Type, string>( _param2, ( string )null );
                return ( IExcelWorker )this;
            }

            IExcelWorker IExcelWorker.SetStyle(
              int _param1,
              string _param2 )
            {
                this._devExpExcel._sortDictionary234[_param1] = new RefPair<Type, string>( ( Type )null, _param2 );
                return ( IExcelWorker )this;
            }

            IExcelWorker IExcelWorker.SetConditionalFormatting(
              int _param1,
              ComparisonOperator _param2,
              string _param3,
              string _param4,
              string _param5 )
            {
                return ( IExcelWorker )this;
            }

            IExcelWorker IExcelWorker.RenameSheet(
              string _param1 )
            {
                this._devExpExcel.Name = _param1;
                return ( IExcelWorker )this;
            }

            IExcelWorker IExcelWorker.AddSheet()
            {
                this._devExpExcel = new DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable( this );
                this._myList.Add( this._devExpExcel );
                return ( IExcelWorker )this;
            }

            bool IExcelWorker.ContainsSheet( string _param1 )
            {
                return this._myList.Any<DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable>( new Func<DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable, bool>( new DevExpExcelWorkerProvider.DevExpExcelWorker.SealClass337()
                {
                    _myString123 = _param1
                }.SealClass337M01 ) );
            }

            IExcelWorker IExcelWorker.SwitchSheet(
              string _param1 )
            {
                this._devExpExcel = this._myList.First<DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable>( new Func<DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable, bool>( new DevExpExcelWorkerProvider.DevExpExcelWorker.SealClass335()
                {
                    _myString123 = _param1
                }.SealClass335F01 ) );
                return ( IExcelWorker )this;
            }

            int IExcelWorker.GetColumnsCount()
            {
                return this._devExpExcel._sortDictionary234.Count;
            }

            int IExcelWorker.GetRowsCount()
            {
                return this._devExpExcel._someHashD.Count;
            }

            [Serializable]
            private sealed class Lamdba0003
            {
                public static readonly DevExpExcelWorkerProvider.DevExpExcelWorker.Lamdba0003 _this = new DevExpExcelWorkerProvider.DevExpExcelWorker.Lamdba0003();
                public static Action<DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable> _myAction213;

                internal void Lamdba0003M01(
                  DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable _param1 )
                {
                    _param1.Dispose();
                }
            }

            private sealed class SealClass337
            {
                public string _myString123;

                internal bool SealClass337M01(
                  DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable _param1 )
                {
                    return StringHelper.EqualsIgnoreCase( _param1.Name, this._myString123 );
                }
            }

            private sealed class DexExpExcelDisposable : IDisposable
            {

                private readonly Dictionary<int, SortedDictionary<int, object>> _dictionary = new Dictionary<int, SortedDictionary<int, object>>();

                public readonly SortedDictionary<int, RefPair<Type, string>> _sortDictionary234 = new SortedDictionary<int, RefPair<Type, string>>();

                public readonly HashSet<int> _someHashD = new HashSet<int>();

                private readonly DevExpExcelWorkerProvider.DevExpExcelWorker _devExpExcelWorker;

                private string _name;

                public DexExpExcelDisposable( DevExpExcelWorkerProvider.DevExpExcelWorker worker )
                {

                    if ( worker == null )
                        throw new ArgumentNullException( nameof( worker ) );
                    this._devExpExcelWorker = worker;
                }

                public string Name
                {
                    get
                    {
                        return this._name;
                    }
                    set
                    {
                        this._name = value;
                    }
                }

                public void SafeAdd023<T>( int _param1, int _param2, T _param3 )
                {
                    ( ( IDictionary<int, RefPair<Type, string>> )this._sortDictionary234 ).TryAdd<int, RefPair<Type, string>>( _param1, new RefPair<Type, string>() );
                    this._someHashD.Add( _param2 );
                    ( ( SortedDictionary<int, object> )CollectionHelper.SafeAdd<int, SortedDictionary<int, object>>( this._dictionary, _param2, ( DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable.SealClass334<T>._myFuction096 ?? ( DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable.SealClass334<T>._myFuction096 = new Func<int, SortedDictionary<int, object>>( DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable.SealClass334<T>._this.SealClass334M01 ) ) ) ) )[_param1] = ( object )_param3;
                }

                public T GetCellT<T>( int _param1, int _param2 )
                {
                    return ( T )CollectionHelper.TryGetValue<int, object>( CollectionHelper.SafeAdd<int, SortedDictionary<int, object>>( this._dictionary, _param2, ( DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable.SealClass333<T>.SealClass333F01 ?? ( DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable.SealClass333<T>.SealClass333F01 = new Func<int, SortedDictionary<int, object>>( DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable.SealClass333<T>._this.SealClass333M01 ) ) ) ), _param1 );
                }

                public void Dispose()
                {
                    using ( IXlSheet sheet = this._devExpExcelWorker._IXlDocument.CreateSheet() )
                    {
                        if ( !StringHelper.IsEmpty( this.Name ) )
                            sheet.Name = ( this.Name );
                        using ( SortedDictionary<int, RefPair<Type, string>>.Enumerator enumerator = this._sortDictionary234.GetEnumerator() )
                        {
                            while ( enumerator.MoveNext() )
                            {
                                KeyValuePair<int, RefPair<Type, string>> current = enumerator.Current;
                                using ( IXlColumn column = sheet.CreateColumn( current.Key ) )
                                {
                                    if ( !( current.Value.First != ( Type )null ) )
                                    {
                                        if ( !StringHelper.IsEmpty( current.Value.Second ) )
                                        {
                                            IXlColumn ixlColumn = column;
                                            XlCellFormatting xlCellFormatting = new XlCellFormatting();
                                            ( ( XlFormatting )xlCellFormatting ).IsDateTimeFormatString = ( true );
                                            ( ( XlFormatting )xlCellFormatting ).NetFormatString = ( current.Value.Second );
                                            ixlColumn.Formatting = ( xlCellFormatting );
                                        }
                                    }
                                }
                            }
                        }
                        foreach ( int key in ( IEnumerable<int> )CollectionHelper.OrderBy<int>( this._someHashD ) )
                        {
                            using ( IXlRow row = sheet.CreateRow( key ) )
                            {
                                SortedDictionary<int, object> sortedDictionary;
                                if ( this._dictionary.TryGetValue( key, out sortedDictionary ) )
                                {
                                    foreach ( KeyValuePair<int, object> keyValuePair in sortedDictionary )
                                    {
                                        using ( IXlCell cell = row.CreateCell( keyValuePair.Key ) )
                                        {
                                            if ( keyValuePair.Value != null )
                                            {
                                                object obj1 = keyValuePair.Value;
                                                XlVariantValue xlVariantValue1;
                                                if ( obj1 is bool )
                                                {
                                                    bool flag = ( bool )obj1;
                                                    XlVariantValue xlVariantValue2 = ( XlVariantValue )null;
                                                    xlVariantValue2.BooleanValue = ( flag );
                                                    xlVariantValue1 = xlVariantValue2;
                                                }
                                                else
                                                {
                                                    object obj2 = keyValuePair.Value;
                                                    if ( obj2 is DateTime )
                                                    {
                                                        DateTime dateTime = ( DateTime )obj2;
                                                        XlVariantValue xlVariantValue2 = ( XlVariantValue )null;
                                                        xlVariantValue2.DateTimeValue = ( dateTime );
                                                        xlVariantValue1 = xlVariantValue2;
                                                    }
                                                    else
                                                    {
                                                        object obj3 = keyValuePair.Value;
                                                        if ( obj3 is DateTimeOffset )
                                                        {
                                                            DateTimeOffset dateTimeOffset = ( DateTimeOffset )obj3;
                                                            XlVariantValue xlVariantValue2 = ( XlVariantValue )null;
                                                            xlVariantValue2.DateTimeValue = ( dateTimeOffset.DateTime );
                                                            xlVariantValue1 = xlVariantValue2;
                                                        }
                                                        else
                                                        {
                                                            string str = keyValuePair.Value as string;
                                                            if ( str != null )
                                                            {
                                                                XlVariantValue xlVariantValue2 = ( XlVariantValue )null;
                                                                xlVariantValue2.TextValue = ( str );
                                                                xlVariantValue1 = xlVariantValue2;
                                                            }
                                                            else
                                                            {
                                                                if ( !TypeHelper.IsNumeric( keyValuePair.Value.GetType() ) )
                                                                    throw new ArgumentOutOfRangeException( keyValuePair.Value?.ToString() );
                                                                XlVariantValue xlVariantValue2 = ( XlVariantValue )null;
                                                                xlVariantValue2.NumericValue = ( ( double )Converter.To<double>( keyValuePair.Value ) );
                                                                xlVariantValue1 = xlVariantValue2;
                                                            }
                                                        }
                                                    }
                                                }
                                                cell.Value = ( xlVariantValue1 );
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    this._sortDictionary234.Clear();
                    this._someHashD.Clear();
                    this._dictionary.Clear();
                }

                [Serializable]
                private sealed class SealClass333<T>
                {
                    public static readonly DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable.SealClass333<T> _this = new DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable.SealClass333<T>();
                    public static Func<int, SortedDictionary<int, object>> SealClass333F01;

                    internal SortedDictionary<int, object> SealClass333M01(
                      int _param1 )
                    {
                        return new SortedDictionary<int, object>();
                    }
                }

                [Serializable]
                private sealed class SealClass334<T>
                {
                    public static readonly DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable.SealClass334<T> _this = new DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable.SealClass334<T>();
                    public static Func<int, SortedDictionary<int, object>> _myFuction096;

                    internal SortedDictionary<int, object> SealClass334M01(
                      int _param1 )
                    {
                        return new SortedDictionary<int, object>();
                    }
                }
            }

            private sealed class SealClass335
            {
                public string _myString123;

                internal bool SealClass335F01(
                  DevExpExcelWorkerProvider.DevExpExcelWorker.DexExpExcelDisposable _param1 )
                {
                    return StringHelper.EqualsIgnoreCase( _param1.Name, this._myString123 );
                }
            }
        }
    }
}
