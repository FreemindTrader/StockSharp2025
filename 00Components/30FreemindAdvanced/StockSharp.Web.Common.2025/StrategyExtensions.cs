// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.StrategyExtensions
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E38A38B-3071-40E9-9B31-80D08347A76B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Common.dll

using Ecng.Common;
using Ecng.IO;
using Ecng.Serialization;
using Newtonsoft.Json;
using StockSharp.Messages;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Common
{
    public static class StrategyExtensions
    {
        private static readonly JsonSerializer<StrategyBacktestReport> _backtestReportSerializer = StrategyExtensions.CreateSerializer<StrategyBacktestReport>();
        private static readonly JsonSerializer<StrategyOptimizationReport> _optimizationSerializer = StrategyExtensions.CreateSerializer<StrategyOptimizationReport>();

        public static bool IsStrategyContent( this ProductContentTypes2 type )
        {
            bool flag;
            switch ( type )
            {
                case ProductContentTypes2.SourceCode:
                case ProductContentTypes2.CompiledAssembly:
                case ProductContentTypes2.Schema:
                flag = true;
                break;
                default:
                flag = false;
                break;
            }
            return flag;
        }

        public static StrategyExecutionModes GetExecMode( this Strategy strategy )
        {
            if ( strategy == null )
                throw new ArgumentNullException( nameof( strategy ) );
            if ( strategy.Type == null )
                return StrategyExecutionModes.None;
            if ( !strategy.BacktestFrom.HasValue && !strategy.BacktestTo.HasValue )
                return StrategyExecutionModes.Live;
            return strategy.Optimization != null ? StrategyExecutionModes.Optimization : StrategyExecutionModes.Backtest;
        }

        public static string ToStringId( this InstrumentInfo instrument )
        {
            if ( instrument == null )
                throw new ArgumentNullException( nameof( instrument ) );
            return ( instrument.Code + "@" + instrument.Board ).ToUpperInvariant();
        }

        public static SecurityId ToSecurityId( this InstrumentInfo instrument )
        {
            if ( instrument == null )
                throw new ArgumentNullException( nameof( instrument ) );
            return new SecurityId()
            {
                SecurityCode = instrument.Code.ToUpperInvariant(),
                BoardCode = instrument.Board.ToUpperInvariant()
            };
        }

        public static InstrumentInfo ToInstrumentInfo( this string stringId )
        {
            SecurityId securityId = stringId.ToSecurityId((SecurityIdGenerator) null);
            return new InstrumentInfo()
            {
                Code = securityId.SecurityCode,
                Board = securityId.BoardCode
            };
        }

        private static string CreateFileName( string fileNameNoExt, StrategyReportTypes type )
        {
            return StringHelper.ThrowIfEmpty( fileNameNoExt, nameof( fileNameNoExt ) ) + string.Format( ".{0}", ( object ) type ).ToLowerInvariant();
        }

        private static StrategyReportTypes GetReportType( string fileName )
        {
            return ( StrategyReportTypes ) Converter.To<StrategyReportTypes>( ( object ) Path.GetExtension( fileName ).Substring( 1 ) );
        }

        private static JsonSerializer<T> CreateSerializer<T>()
        {
            JsonSerializer<T> jsonSerializer = new JsonSerializer<T>();
            jsonSerializer.NullValueHandling = NullValueHandling.Ignore;
            jsonSerializer.Indent = true;
            return jsonSerializer;
        }

        public static ValueTask<StockSharp.Web.DomainModel.File> GenerateFile(
          this StrategyBacktestReport report,
          StrategyReportTypes type,
          string fileNameNoExt = "report",
          CancellationToken cancellationToken = default( CancellationToken ) )
        {
            return StrategyExtensions.GenerateFile<StrategyBacktestReport>( report, StrategyExtensions._backtestReportSerializer, type, fileNameNoExt, cancellationToken );
        }

        public static ValueTask<string> GenerateFile(
          this StrategyBacktestReport report,
          StrategyReportTypes type,
          Stream stream,
          string fileNameNoExt = "report",
          CancellationToken cancellationToken = default( CancellationToken ) )
        {
            return StrategyExtensions.GenerateFile<StrategyBacktestReport>( report, StrategyExtensions._backtestReportSerializer, type, stream, fileNameNoExt, cancellationToken );
        }

        public static async ValueTask<StrategyBacktestReport> ToBacktestReport(
          this StockSharp.Web.DomainModel.File file,
          CancellationToken cancellationToken )
        {
            StrategyBacktestReport report = await StrategyExtensions.ToReport<StrategyBacktestReport>(StrategyExtensions._backtestReportSerializer, file, cancellationToken);
            Dictionary<long, StrategyOrder> dictionary = new Dictionary<long, StrategyOrder>();
            if ( report.Orders != null )
            {
                foreach ( StrategyOrder order in report.Orders )
                {
                    long result;
                    if ( long.TryParse( order.UserId, out result ) && !dictionary.ContainsKey( result ) )
                        dictionary.Add( result, order );
                }
            }
            if ( report.Trades != null )
            {
                foreach ( StrategyTrade trade in report.Trades )
                {
                    long result;
                    StrategyOrder strategyOrder;
                    if ( trade.Order != null && long.TryParse( trade.Order.UserId, out result ) && dictionary.TryGetValue( result, out strategyOrder ) )
                        trade.Order = strategyOrder;
                }
            }
            return report;
        }

        public static ValueTask<StockSharp.Web.DomainModel.File> GenerateFile(
          this StrategyOptimizationReport report,
          StrategyReportTypes type,
          string fileNameNoExt = "report",
          CancellationToken cancellationToken = default( CancellationToken ) )
        {
            return StrategyExtensions.GenerateFile<StrategyOptimizationReport>( report, StrategyExtensions._optimizationSerializer, type, fileNameNoExt, cancellationToken );
        }

        public static ValueTask<string> GenerateFile(
          this StrategyOptimizationReport report,
          StrategyReportTypes type,
          Stream stream,
          string fileNameNoExt = "report",
          CancellationToken cancellationToken = default( CancellationToken ) )
        {
            return StrategyExtensions.GenerateFile<StrategyOptimizationReport>( report, StrategyExtensions._optimizationSerializer, type, stream, fileNameNoExt, cancellationToken );
        }

        public static ValueTask<StrategyOptimizationReport> ToOptimizationReport(
          this StockSharp.Web.DomainModel.File file,
          CancellationToken cancellationToken )
        {
            return StrategyExtensions.ToReport<StrategyOptimizationReport>( StrategyExtensions._optimizationSerializer, file, cancellationToken );
        }

        private static async ValueTask<TReport> ToReport<TReport>(
          JsonSerializer<TReport> serializer,
          StockSharp.Web.DomainModel.File file,
          CancellationToken cancellationToken )
        {
            if ( serializer == null )
                throw new ArgumentNullException( nameof( serializer ) );
            if ( file == null )
                throw new ArgumentNullException( nameof( file ) );
            StrategyReportTypes reportType = StrategyExtensions.GetReportType(file.Name);
            Stream stream = (Stream) Converter.To<Stream>( file.Body);
            if ( reportType != StrategyReportTypes.Zip )
            {
                if ( reportType != StrategyReportTypes.Json )
                    throw new ArgumentOutOfRangeException( "type", reportType.ToString() );
                return await ( ( Serializer<TReport> ) serializer ).DeserializeAsync( stream, cancellationToken );
            }
            using ( IEnumerator<ValueTuple<string, Stream>> enumerator = CompressionHelper.Unzip( stream, true, ( Func<string, bool> ) null ).GetEnumerator() )
            {
                if ( enumerator.MoveNext() )
                    return await ( ( Serializer<TReport> ) serializer ).DeserializeAsync( enumerator.Current.Item2, cancellationToken );
            }
            throw new ArgumentException( "Archive is empty.", nameof( file ) );
        }

        private static async ValueTask<StockSharp.Web.DomainModel.File> GenerateFile<TReport>( TReport report, JsonSerializer<TReport> serializer, StrategyReportTypes type, string fileNameNoExt, CancellationToken cancellationToken )
        {
            MemoryStream body;
            StockSharp.Web.DomainModel.File file1;
            body = new MemoryStream();
            var result = await StrategyExtensions.GenerateFile<TReport>(report, serializer, type, (Stream) body, fileNameNoExt, cancellationToken);


            file1 = new StockSharp.Web.DomainModel.File()
            {
                Name = result,
                Body = ( byte [ ] ) Converter.To<byte [ ]>( ( object ) body )
            };

            return file1;
        }

        private static async ValueTask<string> GenerateFile<TReport>(
          TReport report,
          JsonSerializer<TReport> serializer,
          StrategyReportTypes type,
          Stream stream,
          string fileNameNoExt,
          CancellationToken cancellationToken )
        {
            if ( ( object ) ( TReport ) report == null )
                throw new ArgumentNullException( nameof( report ) );
            if ( serializer == null )
                throw new ArgumentNullException( nameof( serializer ) );
            switch ( type )
            {
                case StrategyReportTypes.Zip:
                using ( ZipArchive zip = new ZipArchive( stream, ZipArchiveMode.Create, true ) )
                {
                    using ( Stream entryStream = zip.CreateEntry( StrategyExtensions.CreateFileName( fileNameNoExt, StrategyReportTypes.Json ), CompressionLevel.Optimal ).Open() )
                        await ( ( Serializer<TReport> ) serializer ).SerializeAsync( report, entryStream, cancellationToken );
                }
                break;
                case StrategyReportTypes.Json:
                await ( ( Serializer<TReport> ) serializer ).SerializeAsync( report, stream, cancellationToken );
                break;
                default:
                throw new ArgumentOutOfRangeException( nameof( type ), type.ToString() );
            }
            return StrategyExtensions.CreateFileName( fileNameNoExt, type );
        }

        public static Decimal? TryGetLastValue( this StrategyPosition position )
        {
            StrategyPositionChange[] changes = ((StrategyPosition) TypeHelper.CheckOnNull<StrategyPosition>( position, nameof (position))).Changes;
            if ( changes == null )
                return new Decimal?();
            return ( ( IEnumerable<StrategyPositionChange> ) changes ).LastOrDefault<StrategyPositionChange>()?.Value;
        }

        public static bool HasEntityInfo( this CommandResponse respose )
        {
            if ( !respose.EntityId.HasValue )
                return !StringHelper.IsEmpty( respose.UserId );
            return true;
        }
    }
}
