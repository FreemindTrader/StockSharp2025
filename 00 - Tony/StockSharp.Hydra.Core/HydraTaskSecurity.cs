// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Core.HydraTaskSecurity
// Assembly: StockSharp.Hydra.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BF4FBD4E-7629-47D5-B0AC-6D48C0A60551
// Assembly location: T:\00-StockSharp\Data\StockSharp.Hydra.Core.dll

using Ecng.Collections;
using Ecng.ComponentModel;
using Newtonsoft.Json;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Hydra.Core
{
    /// <summary>
    /// Инструмент, ассоциированный с <see cref="T:StockSharp.Hydra.Core.IHydraTask" />.
    /// </summary>
    public class HydraTaskSecurity : NotifiableObject
    {
        /// <summary>
        /// Хэш-коллекция для быстрой проверки <see cref="P:StockSharp.Hydra.Core.HydraTaskSecurity.DataTypes" />.
        /// </summary>
        [Obsolete]
        public readonly CachedSynchronizedSet<DataType> DataTypesSet = new CachedSynchronizedSet<DataType>();
        private TypeInfo _tradeInfo = new TypeInfo();
        private TypeInfo _depthInfo = new TypeInfo();
        private TypeInfo _orderLogInfo = new TypeInfo();
        private TypeInfo _level1Info = new TypeInfo();
        private TypeInfo _candleInfo = new TypeInfo();
        private TypeInfo _transactionInfo = new TypeInfo();
        private TypeInfo _positionChangeInfo = new TypeInfo();
        private TypeInfo _newsInfo = new TypeInfo();

        /// <summary>Уникальный идентификатор инструмента.</summary>
        [Obsolete]
        public long Id { get; set; }

        /// <summary>Задача.</summary>
        public IHydraTask Task { get; set; }

        /// <summary>Биржевой инструмент.</summary>
        public Security Security { get; set; }

        /// <summary>
        /// Типы данных, которые нужно получать для данного инструмента.
        /// </summary>
        [Obsolete]
        public DataType[ ] DataTypes
        {
            get
            {
                return DataTypesSet.Cache;
            }
            set
            {
                if ( value == null )
                    throw new ArgumentNullException( nameof( value ) );
                if ( value.Any( t => t.MessageType == null ) )
                    throw new ArgumentException( nameof( value ) );
                DataTypesSet.Clear();
                DataTypesSet.AddRange( value );
            }
        }

        /// <summary>Источник данных построения свечей.</summary>
        [Obsolete]
        public Level1Fields? CandlesBuildFrom
        {
            get
            {
                return new Level1Fields?();
            }
            set
            {
            }
        }

        /// <summary>Информация о сделках.</summary>
        [Obsolete]
        public TypeInfo TradeInfo
        {
            get
            {
                return _tradeInfo;
            }
            set
            {
                TypeInfo typeInfo = value;
                if ( typeInfo == null )
                    throw new ArgumentNullException( nameof( value ) );
                _tradeInfo = typeInfo;
            }
        }

        /// <summary>Информация о стаканах.</summary>
        [Obsolete]
        public TypeInfo DepthInfo
        {
            get
            {
                return _depthInfo;
            }
            set
            {
                TypeInfo typeInfo = value;
                if ( typeInfo == null )
                    throw new ArgumentNullException( nameof( value ) );
                _depthInfo = typeInfo;
            }
        }

        /// <summary>Информация о логе заявок.</summary>
        [Obsolete]
        public TypeInfo OrderLogInfo
        {
            get
            {
                return _orderLogInfo;
            }
            set
            {
                TypeInfo typeInfo = value;
                if ( typeInfo == null )
                    throw new ArgumentNullException( nameof( value ) );
                _orderLogInfo = typeInfo;
            }
        }

        /// <summary>Информация о Level1.</summary>
        [Obsolete]
        public TypeInfo Level1Info
        {
            get
            {
                return _level1Info;
            }
            set
            {
                TypeInfo typeInfo = value;
                if ( typeInfo == null )
                    throw new ArgumentNullException( nameof( value ) );
                _level1Info = typeInfo;
            }
        }

        /// <summary>Информация о свечах.</summary>
        [Obsolete]
        public TypeInfo CandleInfo
        {
            get
            {
                return _candleInfo;
            }
            set
            {
                TypeInfo typeInfo = value;
                if ( typeInfo == null )
                    throw new ArgumentNullException( nameof( value ) );
                _candleInfo = typeInfo;
            }
        }

        /// <summary>Информация о логе собственных транзакций.</summary>
        [Obsolete]
        public TypeInfo TransactionInfo
        {
            get
            {
                return _transactionInfo;
            }
            set
            {
                TypeInfo typeInfo = value;
                if ( typeInfo == null )
                    throw new ArgumentNullException( nameof( value ) );
                _transactionInfo = typeInfo;
            }
        }

        /// <summary>Информация об изменениях позиций.</summary>
        [Obsolete]
        public TypeInfo PositionChangeInfo
        {
            get
            {
                return _positionChangeInfo;
            }
            set
            {
                TypeInfo typeInfo = value;
                if ( typeInfo == null )
                    throw new ArgumentNullException( nameof( value ) );
                _positionChangeInfo = typeInfo;
            }
        }

        /// <summary>Информация о новостях.</summary>
        [Obsolete]
        public TypeInfo NewsInfo
        {
            get
            {
                return _newsInfo;
            }
            set
            {
                TypeInfo typeInfo = value;
                if ( typeInfo == null )
                    throw new ArgumentNullException( nameof( value ) );
                _newsInfo = typeInfo;
            }
        }

        /// <summary>Информация по типу данных.</summary>
        public SynchronizedDictionary<DataType, DateTypeInfo> InfoDict { get; set; } = new SynchronizedDictionary<DataType, DateTypeInfo>();

        /// <summary>
        /// </summary>
        public bool Enabled( DataType dataType )
        {
            DateTypeInfo dateTypeInfo;
            if ( InfoDict.TryGetValue( dataType, out dateTypeInfo ) )
                return !dateTypeInfo.Disabled;
            return false;
        }

        /// <summary>
        /// </summary>
        public IEnumerable<DataType> GetDataTypes()
        {
            return InfoDict.SyncGet( d => d.Where( p => !p.Value.Disabled ).Select( p => p.Key ).ToArray() );
        }

        /// <summary>
        /// </summary>
        public DateTypeInfo AddDataType( DataType dataType )
        {
            DateTypeInfo dateTypeInfo = InfoDict.SafeAdd( dataType );
            dateTypeInfo.Disabled = false;
            return dateTypeInfo;
        }

        /// <summary>
        /// </summary>
        public void RemoveDataType( DataType dataType )
        {
            DateTypeInfo dateTypeInfo;
            if ( !InfoDict.TryGetValue( dataType, out dateTypeInfo ) )
                return;
            dateTypeInfo.Disabled = true;
        }

        /// <summary>Получить строковое представление.</summary>
        /// <returns>Строковое представление.</returns>
        public override string ToString()
        {
            return Security?.Id;
        }

        /// <summary>Информация по типу данных.</summary>
        public class TypeInfo : NotifiableObject
        {
            private long _count;
            private DateTime? _lastTime;

            /// <summary>
            /// </summary>
            [JsonProperty( "disabled" )]
            public bool Disabled { get; set; }

            /// <summary>Обработанное количество данных.</summary>
            [JsonProperty( "count" )]
            public long Count
            {
                get
                {
                    return _count;
                }
                set
                {
                    _count = value;
                    NotifyPropertyChanged( nameof( Count ) );
                }
            }

            /// <summary>Временная метка последних обработанных данных.</summary>
            [JsonProperty( "lastTime" )]
            public DateTime? LastTime
            {
                get
                {
                    return _lastTime;
                }
                set
                {
                    _lastTime = value;
                    NotifyPropertyChanged( nameof( LastTime ) );
                }
            }
        }

        /// <summary>Информация по типу данных.</summary>
        public class DateTypeInfo : TypeInfo
        {
            private DateTime? _beginDate;
            private DateTime? _endDate;
            private Level1Fields? _candlesBuildFrom;
            private int? _maxDepth;
            private bool? _volumeProfile;

            /// <summary>Дата начала загрузки данных.</summary>
            [JsonProperty( "beginDate" )]
            public DateTime? BeginDate
            {
                get
                {
                    return _beginDate;
                }
                set
                {
                    _beginDate = value;
                    NotifyChanged( nameof( BeginDate ) );
                }
            }

            /// <summary>Дата окончания загрузки данных.</summary>
            [JsonProperty( "endDate" )]
            public DateTime? EndDate
            {
                get
                {
                    return _endDate;
                }
                set
                {
                    _endDate = value;
                    NotifyChanged( nameof( EndDate ) );
                }
            }

            /// <summary>Источник данных построения свечей.</summary>
            [JsonProperty( "candlesBuildFrom" )]
            public Level1Fields? CandlesBuildFrom
            {
                get
                {
                    return _candlesBuildFrom;
                }
                set
                {
                    _candlesBuildFrom = value;
                    NotifyChanged( nameof( CandlesBuildFrom ) );
                }
            }

            /// <summary>Max depth.</summary>
            [JsonProperty( "maxDepth" )]
            public int? MaxDepth
            {
                get
                {
                    return _maxDepth;
                }
                set
                {
                    _maxDepth = value;
                    NotifyChanged( nameof( MaxDepth ) );
                }
            }

            /// <summary>Volume profile.</summary>
            [JsonProperty( "volProfile" )]
            public bool? VolumeProfile
            {
                get
                {
                    return _volumeProfile;
                }
                set
                {
                    _volumeProfile = value;
                    NotifyChanged( nameof( VolumeProfile ) );
                }
            }

            /// <summary>
            /// </summary>
            public DateTypeInfo Clone()
            {
                DateTypeInfo dateTypeInfo = new DateTypeInfo();
                dateTypeInfo.BeginDate = BeginDate;
                dateTypeInfo.EndDate = EndDate;
                dateTypeInfo.CandlesBuildFrom = CandlesBuildFrom;
                dateTypeInfo.MaxDepth = MaxDepth;
                dateTypeInfo.Count = Count;
                dateTypeInfo.LastTime = LastTime;
                dateTypeInfo.VolumeProfile = VolumeProfile;
                return dateTypeInfo;
            }
        }
    }
}
