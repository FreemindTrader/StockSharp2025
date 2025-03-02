
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Reflection;
using StockSharp.Algo;
using StockSharp.Algo.Export;
using StockSharp.Algo.Indicators;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Hydra.Core
{
    /// <summary>Вспомогательный класс.</summary>
    public static class Extensions
    {
        /// <summary>
        /// Проверить если указанный инструмент является <see cref="P:StockSharp.Algo.TraderHelper.AllSecurity" />.
        /// </summary>
        /// <param name="security">Инструмент.</param>
        /// <returns><see langword="true" />, если указанный инструмент является <see cref="P:StockSharp.Algo.TraderHelper.AllSecurity" />, иначе, <see langword="false" />.</returns>
        public static bool IsAllSecurity( this HydraTaskSecurity security )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            return security.Security.IsAllSecurity();
        }

        /// <summary>Получить инструмент "Все инструменты" для задачи.</summary>
        /// <param name="task">Задача.</param>
        /// <returns>Инструмент "Все инструменты".</returns>
        public static HydraTaskSecurity GetAllSecurity( this IHydraTask task )
        {
            if ( task == null )
                throw new ArgumentNullException( nameof( task ) );
            return task.Securities.FirstOrDefault( s => s.IsAllSecurity() );
        }

        /// <summary>
        /// Преобразовать <see cref="T:StockSharp.BusinessEntities.Security" /> в <see cref="T:StockSharp.Hydra.Core.HydraTaskSecurity" />.
        /// </summary>
        /// <param name="task">Задача.</param>
        /// <param name="securities">Исходные инструменты.</param>
        /// <returns>Сконвертированные инструменты.</returns>
        public static IEnumerable<HydraTaskSecurity> ToHydraSecurities(
          this IHydraTask task,
          IEnumerable<Security> securities )
        {
            if ( task == null )
                throw new ArgumentNullException( nameof( task ) );
            if ( securities == null )
                throw new ArgumentNullException( nameof( securities ) );
            HydraTaskSecurity allSec = task.GetAllSecurity();
            Dictionary<Security, HydraTaskSecurity> secMap = task.Securities.ToDictionary( s => s.Security, s => s );
            return securities.Where( s => s != allSec.Security ).Select( s =>
            {
                HydraTaskSecurity hydraTaskSecurity;
                if ( !secMap.TryGetValue( s, out hydraTaskSecurity ) )
                {
                    hydraTaskSecurity = new HydraTaskSecurity()
                    {
                        Security = s,
                        Task = task
                    };
                    foreach ( DataType dataType in allSec.GetDataTypes() )
                        hydraTaskSecurity.AddDataType( dataType );
                }
                return hydraTaskSecurity;
            } );
        }

        /// <summary>Получить отображаемое имя для задачи.</summary>
        /// <param name="task">Задача.</param>
        /// <returns>Отображаемое имя.</returns>
        public static string GetDisplayName( this IHydraTask task )
        {
            if ( task == null )
                throw new ArgumentNullException( nameof( task ) );
            return task.GetType().GetTaskDisplayName();
        }

        /// <summary>Получить описание задачи.</summary>
        /// <param name="task">Задача.</param>
        /// <returns>Описание задачи.</returns>
        public static string GetDescription( this IHydraTask task )
        {
            if ( task == null )
                throw new ArgumentNullException( nameof( task ) );
            return task.GetType().GetTaskDescription();
        }

        private static string GetFileName( string fileNamePrefix, DataType dataType )
        {
            string str = fileNamePrefix;
            if ( str.IsEmpty() )
            {
                str = dataType.DataTypeToFileName();
                if ( str.IsEmpty() )
                {
                    if ( dataType == DataType.Securities )
                    {
                        str = "securities";
                    }
                    else
                    {
                        if ( !( dataType.MessageType == typeof( IndicatorValue ) ) )
                            throw new ArgumentOutOfRangeException( nameof( dataType ), dataType, LocalizedStrings.Str1219 );
                        str = "indicator_" + ( ( IndicatorType )dataType.Arg )?.Name;
                    }
                }
            }
            return str;
        }

        private static string GetExtension( ExportTypes type, StorageFormats format )
        {
            switch ( type )
            {
                case ExportTypes.Excel:
                    return ".xlsx";
                case ExportTypes.Xml:
                    return ".xml";
                case ExportTypes.Txt:
                    return ".csv";
                case ExportTypes.Sql:
                case ExportTypes.SaveBuild:
                    return string.Empty;
                case ExportTypes.Json:
                    return ".json";
                case ExportTypes.StockSharp:
                    return format != StorageFormats.Binary ? ".csv" : ".bin";
                default:
                    throw new ArgumentOutOfRangeException( nameof( type ), type, LocalizedStrings.Str1219 );
            }
        }

        /// <summary>Получить формат файла.</summary>
        /// <param name="fileNamePrefix">Начало имени файла.</param>
        /// <param name="dataType">Тип маркет-данных.</param>
        /// <param name="from">Дата начала.</param>
        /// <param name="to">Дата окончания.</param>
        /// <param name="type">Тип экспорта.</param>
        /// <param name="format">Тип формата.</param>
        /// <returns>Произвольный формат файла.</returns>
        public static string GetFileFormat(
          string fileNamePrefix,
          DataType dataType,
          DateTime? from,
          DateTime? to,
          ExportTypes type,
          StorageFormats format )
        {
            return GetFileName( fileNamePrefix, dataType ) + "_{Security.Id}_{From:yyyy_MM_dd}_{To:yyyy_MM_dd}" + GetExtension( type, format );
        }

        /// <summary>Сгенерировать имя эспортируемого файла.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="fileNamePrefix">Начало имени файла.</param>
        /// <param name="fileFormat">Произвольный формат файла.</param>
        /// <param name="dataType">Тип маркет-данных.</param>
        /// <param name="from">Дата начала.</param>
        /// <param name="to">Дата окончания.</param>
        /// <param name="type">Тип экспорта.</param>
        /// <param name="format">Тип формата.</param>
        /// <returns>Имя экспортируемого файла.</returns>
        public static string GetFileName(
          this Security security,
          string fileNamePrefix,
          string fileFormat,
          DataType dataType,
          DateTime? from,
          DateTime? to,
          ExportTypes type,
          StorageFormats format )
        {
            if ( fileFormat.IsEmpty() )
            {
                string str = GetFileName( fileNamePrefix, dataType );
                if ( security != null )
                    str = str + "_" + security.Id.SecurityIdToFolderName();
                if ( from.HasValue )
                    str += string.Format( "_{0:yyyy_MM_dd}", from );
                if ( to.HasValue )
                    str += string.Format( "_{0:yyyy_MM_dd}", to );
                return str + GetExtension( type, format );
            }
            Security security1 = security.Clone();
            security1.Id = security1.Id.SecurityIdToFolderName();
            if ( !security1.Code.IsEmpty() )
                security1.Code = security1.Code.SecurityIdToFolderName();
            return fileFormat.PutEx( new
            {
                Security = security1,
                From = from,
                To = to
            } );
        }

        /// <summary>Проверить, является ли дата торгуемой.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="date">Передаваемая дата, которую необходимо проверить.</param>
        /// <returns><see langword="true" />, если торгуемая дата, иначе, неторгуемая.</returns>
        public static bool IsTradeDate( this HydraTaskSecurity security, DateTime date )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            return security.Security.Board.IsTradeDate( date.ApplyTimeZone( security.Security.Board.TimeZone ), true );
        }

        /// <summary>Принадлежит ли задача категории.</summary>
        /// <param name="task">Задача.</param>
        /// <param name="category">Категория.</param>
        /// <returns>Принадлежит ли задача категории.</returns>
        public static bool IsCategoryOf( this IHydraTask task, MessageAdapterCategories category )
        {
            if ( task == null )
                throw new ArgumentNullException( nameof( task ) );
            return task.GetType().IsCategoryOf( category );
        }

        /// <summary>Получить тип адаптера сообщений.</summary>
        /// <param name="taskType">Задача.</param>
        /// <returns>Тип адаптера сообщений.</returns>
        public static Type GetAdapterType( this Type taskType )
        {
            if ( taskType == null )
                throw new ArgumentNullException( nameof( taskType ) );
            Type genericType = ReflectionHelper.GetGenericType( taskType, typeof( ConnectorHydraTask<> ) );
            if ( ( object )genericType == null )
                return null;
            return genericType.GetGenericArguments().First();
        }

        /// <summary>Получить тип для рефлекции мета-информации.</summary>
        /// <param name="taskType">Задача.</param>
        /// <returns>Тип.</returns>
        public static Type GetReflectTaskType( this Type taskType )
        {
            if ( taskType == null )
                throw new ArgumentNullException( nameof( taskType ) );
            Type adapterType = taskType.GetAdapterType();
            if ( ( object )adapterType != null )
                return adapterType;
            return taskType;
        }

        /// <summary>Получить категории задачи.</summary>
        /// <param name="taskType">Задача.</param>
        /// <returns>Категории.</returns>
        public static MessageAdapterCategories? GetCategories( this Type taskType )
        {
            return taskType.GetReflectTaskType().GetAttribute<MessageAdapterCategoryAttribute>( true )?.Categories;
        }

        /// <summary>Принадлежит ли задача категории.</summary>
        /// <param name="taskType">Задача.</param>
        /// <param name="category">Категория.</param>
        /// <returns>Принадлежит ли задача категории.</returns>
        public static bool IsCategoryOf( this Type taskType, MessageAdapterCategories category )
        {
            MessageAdapterCategories? categories = taskType.GetCategories();
            ref MessageAdapterCategories? local = ref categories;
            if ( !local.HasValue )
                return false;
            return local.GetValueOrDefault().HasFlag( category );
        }

        /// <summary>Получить инонку задачи.</summary>
        /// <param name="taskType">Задача.</param>
        /// <returns>Иконка задачи.</returns>
        public static Uri GetIcon( this Type taskType )
        {
            return taskType.GetReflectTaskType().GetIconUrl();
        }

        /// <summary>Получить отображаемое имя для задачи.</summary>
        /// <param name="taskType">Задача.</param>
        /// <returns>Отображаемое имя.</returns>
        public static string GetTaskDisplayName( this Type taskType )
        {
            return taskType.GetReflectTaskType().GetDisplayName( null );
        }

        /// <summary>Получить описание задачи.</summary>
        /// <param name="taskType">Задача.</param>
        /// <returns>Описание задачи.</returns>
        public static string GetTaskDescription( this Type taskType )
        {
            return taskType.GetReflectTaskType().GetDescription( null );
        }

        /// <summary>
        /// Создать инструмент, ассоциированный с <see cref="T:StockSharp.Hydra.Core.IHydraTask" />.
        /// </summary>
        /// <param name="task">Задача.</param>
        /// <param name="security">Инструмент.</param>
        /// <returns>Инструмент, ассоциированный с <see cref="T:StockSharp.Hydra.Core.IHydraTask" />.</returns>
        public static HydraTaskSecurity ToTaskSecurity(
          this IHydraTask task,
          Security security )
        {
            if ( task == null )
                throw new ArgumentNullException( nameof( task ) );
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            return new HydraTaskSecurity()
            {
                Task = task,
                Security = security
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registry"></param>
        public static void WaitSecuritiesFlush( this IEntityRegistry registry )
        {
            registry.Securities.WaitFlush();
        }

        /// <summary>Получить подходящий txt шаблон.</summary>
        /// <param name="registry">Реестр txt шаблонов.</param>
        /// <param name="dataType">Информация о типе данных.</param>
        /// <param name="isEmptySecurity">Пустой ли инструмент.</param>
        /// <returns>Шаблон.</returns>
        public static string GetTemplate(
          this TemplateTxtRegistry registry,
          DataType dataType,
          bool isEmptySecurity )
        {
            if ( registry == null )
                throw new ArgumentNullException( nameof( registry ) );
            if ( dataType == null )
                throw new ArgumentNullException( nameof( dataType ) );
            if ( dataType == DataType.Securities )
                return registry.TemplateTxtSecurity;
            if ( dataType == DataType.News )
                return registry.TemplateTxtNews;
            if ( dataType == DataType.Board )
                return registry.TemplateTxtBoard;
            if ( dataType == DataType.BoardState )
                return registry.TemplateTxtBoardState;
            if ( dataType.IsCandles )
                return registry.TemplateTxtCandle;
            if ( dataType == DataType.Level1 )
            {
                if ( isEmptySecurity )
                    return registry.TemplateTxtOptions;
                return registry.TemplateTxtLevel1;
            }
            if ( dataType == DataType.MarketDepth )
                return registry.TemplateTxtDepth;
            if ( dataType == DataType.Ticks )
                return registry.TemplateTxtTick;
            if ( dataType == DataType.Transactions )
                return registry.TemplateTxtTransaction;
            if ( dataType == DataType.OrderLog )
                return registry.TemplateTxtOrderLog;
            if ( dataType.MessageType == typeof( IndicatorValue ) )
                return registry.TemplateTxtIndicator;
            throw new ArgumentOutOfRangeException( nameof( dataType ), dataType, LocalizedStrings.Str721 );
        }

        /// <summary>Установить новый txt шаблон.</summary>
        /// <param name="registry">Реестр txt шаблонов.</param>
        /// <param name="dataType">Информация о типе данных.</param>
        /// <param name="isEmptySecurity">Пустой ли инструмент.</param>
        /// <param name="txtTemplate">Шаблон.</param>
        public static void SetTemplate(
          this TemplateTxtRegistry registry,
          DataType dataType,
          bool isEmptySecurity,
          string txtTemplate )
        {
            if ( registry == null )
                throw new ArgumentNullException( nameof( registry ) );
            if ( dataType == null )
                throw new ArgumentNullException( nameof( dataType ) );
            if ( txtTemplate.IsEmpty() )
                throw new ArgumentNullException( nameof( txtTemplate ) );
            if ( dataType == DataType.Securities )
                registry.TemplateTxtSecurity = txtTemplate;
            else if ( dataType == DataType.News )
                registry.TemplateTxtNews = txtTemplate;
            else if ( dataType == DataType.Board )
                registry.TemplateTxtBoard = txtTemplate;
            else if ( dataType == DataType.BoardState )
                registry.TemplateTxtBoardState = txtTemplate;
            else if ( dataType.IsCandles )
                registry.TemplateTxtCandle = txtTemplate;
            else if ( dataType == DataType.Level1 )
            {
                if ( !isEmptySecurity )
                    registry.TemplateTxtLevel1 = txtTemplate;
                else
                    registry.TemplateTxtOptions = txtTemplate;
            }
            else if ( dataType == DataType.MarketDepth )
                registry.TemplateTxtDepth = txtTemplate;
            else if ( dataType == DataType.Ticks )
                registry.TemplateTxtTick = txtTemplate;
            else if ( dataType == DataType.Transactions )
                registry.TemplateTxtTransaction = txtTemplate;
            else if ( dataType == DataType.OrderLog )
            {
                registry.TemplateTxtOrderLog = txtTemplate;
            }
            else
            {
                if ( !( dataType.MessageType == typeof( IndicatorValue ) ) )
                    throw new ArgumentOutOfRangeException( nameof( dataType ), dataType, LocalizedStrings.Str721 );
                registry.TemplateTxtIndicator = txtTemplate;
            }
        }

        /// <summary>Сохранить настройки источника.</summary>
        /// <param name="task">Задача.</param>
        public static void SaveSettings( this IHydraTask task )
        {
            if ( task == null )
                throw new ArgumentNullException( nameof( task ) );
            HydraTaskManager.Instance.Save( task );
        }

        /// <summary>Сохранить изменения по инструменту.</summary>
        /// <param name="task">Задача.</param>
        /// <param name="security">Инструмент.</param>
        public static void UpdateSecurity( this IHydraTask task, HydraTaskSecurity security )
        {
            if ( task == null )
                throw new ArgumentNullException( nameof( task ) );
            HydraTaskManager.Instance.Save( task, security );
        }

        /// <summary>
        /// Получить даты, для которых необходимо запускать закачку.
        /// </summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="dataType">Тип маркет-данных.</param>
        /// <param name="startDate">Общая дата начала источника.</param>
        /// <param name="daysOffset">Временной отступ в днях.</param>
        /// <param name="interval">Интервал.</param>
        /// <returns>Даты.</returns>
        public static IEnumerable<DateTime> GetDates(
          this HydraTaskSecurity security,
          DataType dataType,
          DateTime startDate,
          int daysOffset,
          TimeSpan? interval = null )
        {
            if ( dataType == null )
                throw new ArgumentNullException( nameof( dataType ) );
            HydraTaskSecurity.DateTypeInfo dateTypeInfo = security != null ? security.InfoDict.TryGetValue( dataType ) : null;
            DateTime? nullable = dateTypeInfo?.BeginDate;
            DateTime from = nullable ?? startDate;
            nullable = dateTypeInfo?.EndDate;
            DateTime to = nullable ?? DateTime.Today - TimeSpan.FromDays( daysOffset );
            TimeSpan interval1 = interval ?? TimeSpan.FromDays( 1.0 );
            return from.Range( to, interval1 );
        }

        /// <summary>Установить дата начала.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="dataType">Тип маркет-данных.</param>
        /// <param name="beginDate">Дата начала.</param>
        public static void SetBeginDate(
          this HydraTaskSecurity security,
          DataType dataType,
          DateTime? beginDate )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            security.InfoDict.SafeAdd( dataType ).BeginDate = beginDate;
        }

        /// <summary>Получить дату начала.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="dataType">Тип маркет-данных.</param>
        /// <returns>Дата начала.</returns>
        public static DateTime? GetBeginDate( this HydraTaskSecurity security, DataType dataType )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            return security.InfoDict.TryGetValue( dataType )?.BeginDate;
        }

        /// <summary>Получить дату окончания.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="dataType">Тип маркет-данных.</param>
        /// <returns>Дата окончания.</returns>
        public static DateTime? GetEndDate( this HydraTaskSecurity security, DataType dataType )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            return security.InfoDict.TryGetValue( dataType )?.EndDate;
        }

        /// <summary>Установить дата окончания.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="dataType">Тип маркет-данных.</param>
        /// <param name="endDate">Дата окончания.</param>
        public static void SetEndDate(
          this HydraTaskSecurity security,
          DataType dataType,
          DateTime? endDate )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            security.InfoDict.SafeAdd( dataType ).EndDate = endDate;
        }

        /// <summary>Получить источник данных построения свечей.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="dataType">Тип маркет-данных.</param>
        /// <returns>Источник данных построения свечей.</returns>
        public static Level1Fields? GetCandlesBuildFrom(
          this HydraTaskSecurity security,
          DataType dataType )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            return security.InfoDict.TryGetValue( dataType )?.CandlesBuildFrom;
        }

        /// <summary>Установить источник данных построения свечей.</summary>
        /// <param name="security">Инструмент.</param>
        /// <param name="dataType">Тип маркет-данных.</param>
        /// <param name="candlesBuildFrom">Источник данных построения свечей.</param>
        public static void SetCandlesBuildFrom(
          this HydraTaskSecurity security,
          DataType dataType,
          Level1Fields? candlesBuildFrom )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            security.InfoDict.SafeAdd( dataType ).CandlesBuildFrom = candlesBuildFrom;
        }

        /// <summary>Get max depth.</summary>
        public static int? GetMaxDepth( this HydraTaskSecurity security, DataType dataType )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            return security.InfoDict.TryGetValue( dataType )?.MaxDepth;
        }

        /// <summary>Get vol profile.</summary>
        public static bool? GetVolumeProfile( this HydraTaskSecurity security, DataType dataType )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            return security.InfoDict.TryGetValue( dataType )?.VolumeProfile;
        }

        /// <summary>Set max depth.</summary>
        public static void SetMaxDepth(
          this HydraTaskSecurity security,
          DataType dataType,
          int? maxDepth )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            security.InfoDict.SafeAdd( dataType ).MaxDepth = maxDepth;
        }

        /// <summary>Set vol profile.</summary>
        public static void SetVolumeProfile(
          this HydraTaskSecurity security,
          DataType dataType,
          bool? volumeProfile )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            security.InfoDict.SafeAdd( dataType ).VolumeProfile = volumeProfile;
        }

        /// <summary>Получить дату последней свечи.</summary>
        /// <param name="candles">Свечи.</param>
        /// <returns>Дата.</returns>
        public static DateTime GetLastDate( this IEnumerable<CandleMessage> candles )
        {
            DateTime dateTime = candles.Last().OpenTime.UtcDateTime;
            dateTime = dateTime.Date;
            return dateTime.AddDays( 1.0 );
        }

        /// <summary>Тайм-фреймы.</summary>
        public static IEnumerable<DataType> GeneratedTimeFrames { get; } = new DataType[12] { DataType.TimeFrame( TimeSpan.FromTicks( 1L ) ), DataType.TimeFrame( TimeSpan.FromSeconds( 1.0 ) ), DataType.TimeFrame( TimeSpan.FromMinutes( 1.0 ) ), DataType.TimeFrame( TimeSpan.FromMinutes( 5.0 ) ), DataType.TimeFrame( TimeSpan.FromMinutes( 10.0 ) ), DataType.TimeFrame( TimeSpan.FromMinutes( 15.0 ) ), DataType.TimeFrame( TimeSpan.FromMinutes( 30.0 ) ), DataType.TimeFrame( TimeSpan.FromHours( 1.0 ) ), DataType.TimeFrame( TimeSpan.FromDays( 1.0 ) ), DataType.TimeFrame( TimeSpan.FromDays( 7.0 ) ), DataType.TimeFrame( TimeSpan.FromTicks( 25920000000000L ) ), DataType.TimeFrame( TimeSpan.FromTicks( 315360000000000L ) ) };
    }
}
