// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Core.HydraStorage
// Assembly: StockSharp.Hydra.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BF4FBD4E-7629-47D5-B0AC-6D48C0A60551
// Assembly location: T:\00-StockSharp\Data\StockSharp.Hydra.Core.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Newtonsoft.Json;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Configuration;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace StockSharp.Hydra.Core
{
    /// <summary>Hydra tasks settings storage.</summary>
    public class HydraStorage
    {
        private readonly Dictionary<Guid, HydraStorage.HydraSecurityStorage> _securityStorages = new Dictionary<Guid, HydraStorage.HydraSecurityStorage>();
        private readonly string _path;
        private DelayAction _delayAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Hydra.Core.HydraStorage" />.
        /// </summary>
        /// <param name="path">Path to storage.</param>
        public HydraStorage( string path )
        {
            if ( path.IsEmpty() )
                throw new ArgumentNullException( nameof( path ) );
            this._path = path;
            this._delayAction = new DelayAction( ( Action<Exception> )( ex => ex.LogError( ( string )null ) ) );
        }

        /// <summary>The time delayed action.</summary>
        public DelayAction DelayAction
        {
            get
            {
                return this._delayAction;
            }
            set
            {
                DelayAction delayAction = value;
                if ( delayAction == null )
                    throw new ArgumentNullException( nameof( value ) );
                this._delayAction = delayAction;
            }
        }

        /// <summary>Initialize the storage.</summary>
        public void Init()
        {
            Directory.CreateDirectory( this._path );
        }

        /// <summary>Reset all settings.</summary>
        public void Reset()
        {
            Directory.Delete( this._path, true );
        }

        /// <summary>Load all tasks.</summary>
        /// <param name="logs">Logs.</param>
        /// <returns>Tasks.</returns>
        public IDictionary<HydraTaskInfo, IEnumerable<HydraTaskSecurity>> Load(
          ILogReceiver logs )
        {
            Dictionary<HydraTaskInfo, IEnumerable<HydraTaskSecurity>> retVal = new Dictionary<HydraTaskInfo, IEnumerable<HydraTaskSecurity>>();
            Do.Invariant( ( Action )( () =>
            {
                foreach ( string enumerateConfig in this._path.EnumerateConfigs( "task_*" ) )
                {
                    try
                    {
                        SettingsStorage settingsStorage = enumerateConfig.Deserialize<SettingsStorage>();
                        if ( settingsStorage != null )
                        {
                            Guid guid = settingsStorage.GetValue<Guid>( "Id", new Guid() );
                            string taskType = settingsStorage.GetValue<string>( "TaskType", ( string )null );
                            HydraTaskInfo key = new HydraTaskInfo( guid, taskType, new SettingsStorage() );
                            foreach ( KeyValuePair<string, object> keyValuePair in ( SynchronizedDictionary<string, object> )( settingsStorage.GetValue<SettingsStorage>( "Settings", ( SettingsStorage )null ) ?? settingsStorage.GetValue<SettingsStorage>( "ExtensionInfo", ( SettingsStorage )null ) ) )
                            {
                                if ( !key.Settings.TryAdd<KeyValuePair<string, object>>( keyValuePair ) )
                                    logs.AddErrorLog( string.Format( "For task {0} ({1}) value '{2}' already added.", ( object )guid, ( object )taskType, ( object )keyValuePair.Key ) );
                            }
                            HydraStorage.HydraSecurityStorage hydraSecurityStorage = new HydraStorage.HydraSecurityStorage( this._path, guid );
                            IEnumerable<HydraTaskSecurity> hydraTaskSecurities = hydraSecurityStorage.Load( logs );
                            if ( this._securityStorages.ContainsKey( guid ) )
                                logs.AddErrorLog( string.Format( "Task {0} already added.", ( object )guid ) );
                            else
                                this._securityStorages.Add( guid, hydraSecurityStorage );
                            retVal.Add( key, hydraTaskSecurities );
                        }
                    }
                    catch ( Exception ex )
                    {
                        logs.AddErrorLog( ex );
                    }
                }
            } ) );
            return ( IDictionary<HydraTaskInfo, IEnumerable<HydraTaskSecurity>> )retVal;
        }

        /// <summary>Update task settings.</summary>
        /// <param name="info">Task settings.</param>
        public void Save( HydraTaskInfo info )
        {
            if ( info == null )
                throw new ArgumentNullException( nameof( info ) );
            this.DelayAction.DefaultGroup.Add( ( Action )( () => Do.Invariant( ( Action )( () =>
            {
                new SettingsStorage()
              {
          {
            "Id",
            (object) info.Id
          },
          {
            "TaskType",
            (object) info.TaskType
          },
          {
            "Settings",
            (object) info.Settings
          }
              }.Serialize<SettingsStorage>( this.GetFileName( info.Id ) );
                this._securityStorages.SafeAdd<Guid, HydraStorage.HydraSecurityStorage>( info.Id, ( Func<Guid, HydraStorage.HydraSecurityStorage> )( key => new HydraStorage.HydraSecurityStorage( this._path, key ) ) );
            } ) ) ), ( Action<Exception> )null, true, true );
        }

        /// <summary>Delete task settings.</summary>
        /// <param name="taskId">Task id.</param>
        public void Delete( Guid taskId )
        {
            this.DelayAction.DefaultGroup.Add( ( Action )( () => Do.Invariant( ( Action )( () =>
            {
                File.Delete( this.GetFileName( taskId ) );
                this._securityStorages[taskId].Clear();
                this._securityStorages.Remove( taskId );
            } ) ) ), ( Action<Exception> )null, true, true );
        }

        /// <summary>Add securities into the task.</summary>
        /// <param name="taskId">Task id.</param>
        /// <param name="securities">Securities.</param>
        public void Add( Guid taskId, IEnumerable<HydraTaskSecurity> securities )
        {
            if ( securities == null )
                throw new ArgumentNullException( nameof( securities ) );
            this.DelayAction.DefaultGroup.Add( ( Action )( () => Do.Invariant( ( Action )( () => this._securityStorages[taskId].Add( securities ) ) ) ), ( Action<Exception> )null, true, true );
        }

        /// <summary>Update securities info.</summary>
        /// <param name="taskId">Task id.</param>
        /// <param name="securities">Securities.</param>
        public void Update( Guid taskId, IEnumerable<HydraTaskSecurity> securities )
        {
            if ( securities == null )
                throw new ArgumentNullException( nameof( securities ) );
            this.DelayAction.DefaultGroup.Add( ( Action )( () => Do.Invariant( ( Action )( () => this._securityStorages[taskId].Update( securities ) ) ) ), ( Action<Exception> )null, true, true );
        }

        /// <summary>Delete securities from the task.</summary>
        /// <param name="taskId">Task id.</param>
        /// <param name="securities">Securities.</param>
        public void Delete( Guid taskId, IEnumerable<HydraTaskSecurity> securities )
        {
            if ( securities == null )
                throw new ArgumentNullException( nameof( securities ) );
            this.DelayAction.DefaultGroup.Add( ( Action )( () => Do.Invariant( ( Action )( () => this._securityStorages[taskId].Delete( securities ) ) ) ), ( Action<Exception> )null, true, true );
        }

        /// <summary>Delete all securities from the task.</summary>
        /// <param name="taskId">Task id.</param>
        public void DeleteAll( Guid taskId )
        {
            this.DelayAction.DefaultGroup.Add( ( Action )( () => Do.Invariant( ( Action )( () => this._securityStorages[taskId].Clear() ) ) ), ( Action<Exception> )null, true, true );
        }

        private string GetFileName( Guid taskId )
        {
            return Path.Combine( this._path, string.Format( "task_{0}{1}", ( object )taskId, ( object )".json" ) );
        }

        private class HydraSecurityStorage
        {
            private readonly string _path;
            private readonly Guid _taskId;

            public HydraSecurityStorage( string path, Guid taskId )
            {
                if ( path == null )
                    throw new ArgumentNullException( nameof( path ) );
                if ( taskId == new Guid() )
                    throw new ArgumentNullException( nameof( taskId ) );
                this._taskId = taskId;
                this._path = Path.Combine( path, string.Format( "task_{0}_securities", ( object )taskId ) );
                Directory.CreateDirectory( this._path );
            }

            public IEnumerable<HydraTaskSecurity> Load( ILogReceiver logs )
            {
                if ( logs == null )
                    throw new ArgumentNullException( nameof( logs ) );
                List<HydraTaskSecurity> hydraTaskSecurityList = new List<HydraTaskSecurity>();
                try
                {
                    string str1 = this._path + ".csv";
                    if ( File.Exists( str1 ) )
                    {
                        hydraTaskSecurityList.AddRange( HydraStorage.HydraSecurityStorage.HydraSecurityStorageOld.Load( str1, this._taskId, logs ) );
                        this.Add( ( IEnumerable<HydraTaskSecurity> )hydraTaskSecurityList );
                        str1.MoveToBackup( str1 + string.Format( ".migrated_{0:yyyy_MM_dd_HH_mm}", ( object )DateTime.Now ) );
                    }
                    else
                    {
                        foreach ( string str2 in Directory.EnumerateDirectories( this._path ).SelectMany<string, string>( ( Func<string, IEnumerable<string>> )( dir => Directory.EnumerateFiles( dir, "*.json" ) ) ) )
                        {
                            string str3 = File.ReadAllText( str2 );
                            if ( !str3.IsEmpty() )
                            {
                                Security security = ServicesRegistry.SecurityProvider.LookupById( Path.GetFileNameWithoutExtension( str2 ).FolderNameToSecurityId() );
                                if ( security != null )
                                {
                                    try
                                    {
                                        IDictionary<string, HydraTaskSecurity.DateTypeInfo> dictionary = str3.DeserializeObject<IDictionary<string, HydraTaskSecurity.DateTypeInfo>>();
                                        HydraTaskSecurity hydraTaskSecurity = new HydraTaskSecurity() { Security = security };
                                        foreach ( KeyValuePair<string, HydraTaskSecurity.DateTypeInfo> keyValuePair in ( IEnumerable<KeyValuePair<string, HydraTaskSecurity.DateTypeInfo>> )dictionary )
                                            hydraTaskSecurity.InfoDict.Add( HydraStorage.HydraSecurityStorage.ToDataType( keyValuePair.Key ), keyValuePair.Value );
                                        hydraTaskSecurityList.Add( hydraTaskSecurity );
                                    }
                                    catch ( Exception ex )
                                    {
                                        logs.AddErrorLog( ( Exception )new InvalidOperationException( LocalizedStrings.Str2929Params.Put( ( object )str2 ), ex ) );
                                        str2.MoveToBackup( str2 + string.Format( ".corrupted_{0:yyyy_MM_dd_HH_mm}", ( object )DateTime.Now ) );
                                    }
                                }
                            }
                        }
                    }
                }
                catch ( Exception ex )
                {
                    logs.AddErrorLog( ( Exception )new InvalidOperationException( LocalizedStrings.Str2929Params.Put( ( object )this._path ), ex ) );
                }
                return ( IEnumerable<HydraTaskSecurity> )hydraTaskSecurityList.ToArray();
            }

            private static DataType ToDataType( string str )
            {
                return LocalMarketDataDrive.GetDataType( str ) ?? DataType.Create( str.Replace( "_", ", " ).To<Type>(), ( object )null );
            }

            private static string ToString( DataType dataType )
            {
                return LocalMarketDataDrive.GetFileName( dataType, new StorageFormats?(), false ) ?? dataType.MessageType.GetTypeName( false ).Replace( ", ", "_" );
            }

            public void Add( IEnumerable<HydraTaskSecurity> securities )
            {
                this.Update( securities );
            }

            public void Update( IEnumerable<HydraTaskSecurity> securities )
            {
                if ( securities == null )
                    throw new ArgumentNullException( nameof( securities ) );
                foreach ( HydraTaskSecurity security in securities )
                {
                    Dictionary<string, HydraTaskSecurity.DateTypeInfo> dictionary = security.InfoDict.ToDictionary<KeyValuePair<DataType, HydraTaskSecurity.DateTypeInfo>, string, HydraTaskSecurity.DateTypeInfo>( ( Func<KeyValuePair<DataType, HydraTaskSecurity.DateTypeInfo>, string> )( p => HydraStorage.HydraSecurityStorage.ToString( p.Key ) ), ( Func<KeyValuePair<DataType, HydraTaskSecurity.DateTypeInfo>, HydraTaskSecurity.DateTypeInfo> )( p => p.Value ) );
                    string path = this.GetPath( security );
                    Directory.CreateDirectory( Path.GetDirectoryName( path ) );
                    File.WriteAllText( path, JsonConvert.SerializeObject( ( object )dictionary, Formatting.Indented ) );
                }
            }

            public void Delete( IEnumerable<HydraTaskSecurity> securities )
            {
                foreach ( HydraTaskSecurity security in securities )
                    File.Delete( this.GetPath( security ) );
            }

            private string GetPath( HydraTaskSecurity security )
            {
                if ( security == null )
                    throw new ArgumentNullException( nameof( security ) );
                string folderName = security.Security.Id.SecurityIdToFolderName();
                return Path.Combine( this._path, folderName.Substring( 0, 1 ), folderName + ".json" );
            }

            public void Clear()
            {
                Directory.Delete( this._path, true );
            }

            [Obsolete]
            private static class HydraSecurityStorageOld
            {
                public static IEnumerable<HydraTaskSecurity> Load(
                  string fileName,
                  Guid taskId,
                  ILogReceiver logs )
                {
                    if ( fileName.IsEmpty() )
                        throw new ArgumentNullException( nameof( fileName ) );
                    if ( taskId == new Guid() )
                        throw new ArgumentNullException( nameof( taskId ) );
                    if ( logs == null )
                        throw new ArgumentNullException( nameof( logs ) );
                    byte[ ] numArray = new byte[51200];
                    List<HydraTaskSecurity> hydraTaskSecurityList = new List<HydraTaskSecurity>();
                    try
                    {
                        using ( FileStream fileStream = File.OpenRead( fileName ) )
                        {
                            while ( fileStream.Position < fileStream.Length - 1L )
                            {
                                long position = fileStream.Position;
                                int num1 = fileStream.Read( numArray, 0, numArray.Length );
                                if ( num1 != numArray.Length )
                                    throw new InvalidOperationException( string.Format( "read={0}, pos={1}, len={2}", ( object )num1, ( object )fileStream.Position, ( object )fileStream.Length ) );
                                string[ ] parts = numArray.UTF8().Remove( "\0", false ).Split( ';' );
                                int index1 = 0;
                                Security security = ServicesRegistry.SecurityProvider.LookupById( parts[index1++] );
                                if ( security != null )
                                {
                                    HydraTaskSecurity hydraTaskSecurity = new HydraTaskSecurity() { Security = security, CandleInfo = Load( parts, ref index1 ), TradeInfo = Load( parts, ref index1 ), Level1Info = Load( parts, ref index1 ), DepthInfo = Load( parts, ref index1 ), OrderLogInfo = Load( parts, ref index1 ), PositionChangeInfo = Load( parts, ref index1 ), TransactionInfo = Load( parts, ref index1 ), CandlesBuildFrom = parts[index1++].To<Level1Fields?>(), DataTypes = LoadDataTypes( parts[index1++] ) };
                                    foreach ( string str in parts[index1++].SplitByComma( false ) )
                                    {
                                        char[ ] chArray = new char[1] { '=' };
                                        string[ ] strArray1 = str.Split( chArray );
                                        int num2 = 0;
                                        string[ ] strArray2 = strArray1;
                                        int index2 = num2;
                                        int num3 = index2 + 1;
                                        DataType dataType = ToDataType( strArray2[index2] );
                                        HydraTaskSecurity.DateTypeInfo dateTypeInfo1 = new HydraTaskSecurity.DateTypeInfo();
                                        string[ ] strArray3 = strArray1;
                                        int index3 = num3;
                                        int num4 = index3 + 1;
                                        dateTypeInfo1.LastTime = ToDateTime( strArray3[index3] );
                                        string[ ] strArray4 = strArray1;
                                        int index4 = num4;
                                        int num5 = index4 + 1;
                                        dateTypeInfo1.Count = strArray4[index4].To<long>();
                                        string[ ] strArray5 = strArray1;
                                        int index5 = num5;
                                        int num6 = index5 + 1;
                                        dateTypeInfo1.BeginDate = ToDateTime( strArray5[index5] );
                                        string[ ] strArray6 = strArray1;
                                        int index6 = num6;
                                        int index7 = index6 + 1;
                                        dateTypeInfo1.EndDate = ToDateTime( strArray6[index6] );
                                        HydraTaskSecurity.DateTypeInfo dateTypeInfo2 = dateTypeInfo1;
                                        if ( index7 < strArray1.Length )
                                        {
                                            HydraTaskSecurity.DateTypeInfo dateTypeInfo3 = dateTypeInfo2;
                                            int? nullable1 = strArray1[index7].To<int?>();
                                            Level1Fields? nullable2 = nullable1.HasValue ? new Level1Fields?( ( Level1Fields )nullable1.GetValueOrDefault() ) : new Level1Fields?();
                                            dateTypeInfo3.CandlesBuildFrom = nullable2;
                                        }
                                        hydraTaskSecurity.InfoDict.Add( dataType, dateTypeInfo2 );
                                    }
                                    if ( parts.Length > index1 )
                                        hydraTaskSecurity.NewsInfo = Load( parts, ref index1 );
                                    hydraTaskSecurityList.Add( hydraTaskSecurity );
                                }
                            }
                        }
                    }
                    catch ( Exception ex )
                    {
                        logs.AddErrorLog( ( Exception )new InvalidOperationException( LocalizedStrings.Str2929Params.Put( ( object )fileName ), ex ) );
                        hydraTaskSecurityList.Clear();
                        fileName.MoveToBackup( fileName + string.Format( ".corrupted_{0:yyyy_MM_dd_HH_mm}", ( object )DateTime.Now ) );
                    }
                    return ( IEnumerable<HydraTaskSecurity> )hydraTaskSecurityList.ToArray();

                    DateTime? ToDateTime( string str )
                    {
                        if ( !str.IsEmpty() )
                            return new DateTime?( str.Length > 8 ? str.ToDateTime( "yyyyMMddHHmmss", ( CultureInfo )null ) : str.ToDateTime( "yyyyMMdd", ( CultureInfo )null ) );
                        return new DateTime?();
                    }

                    HydraTaskSecurity.TypeInfo Load( string[ ] parts, ref int index )
                    {
                        return new HydraTaskSecurity.TypeInfo() { LastTime = ToDateTime( parts[index++] ), Count = parts[index++].To<long>() };
                    }

                    DataType ToDataType( string str )
                    {
                        return LocalMarketDataDrive.GetDataType( str ) ?? DataType.Create( str.Replace( "_", ", " ).To<Type>(), ( object )null );
                    }

                    DataType[ ] LoadDataTypes( string str )
                    {
                        return ( ( IEnumerable<string> )str.SplitByComma( false ) ).Select<string, DataType>( new Func<string, DataType>( ToDataType ) ).ToArray<DataType>();
                    }
                }
            }
        }
    }
}
