//using StockSharp.Algo.Storages;
//using StockSharp.BusinessEntities;
//using StockSharp.Community;
//using System;
//using System.IO;
//using System.ServiceModel;

//namespace StockSharp.Algo.History.Hydra
//{
//    [ServiceContract(Namespace = "https://stocksharp.com/hydraserver")]
//  public interface IRemoteStorage : IAuthenticationService
//  {
//        [OperationContract]
//        string[] LookupSecurityIds( Guid sessionId, Security criteria );

//        [OperationContract]
//        string[] LookupExchanges( Guid sessionId, Exchange criteria );

//        [OperationContract]
//        string[] LookupExchangeBoards( Guid sessionId, ExchangeBoard criteria );

//        [OperationContract]
//        Security[] GetSecurities( Guid sessionId, string[] securityIds );

//        [OperationContract]
//        Exchange[] GetExchanges( Guid sessionId, string[] codes );

//        [OperationContract]
//        ExchangeBoard[] GetExchangeBoards( Guid sessionId, string[] codes );

//        [OperationContract]
//        void SaveSecurities( Guid sessionId, Security[] securities );

//        [OperationContract]
//        void SaveExchanges( Guid sessionId, Exchange[] exchanges );

//        [OperationContract]
//        void SaveExchangeBoards( Guid sessionId, ExchangeBoard[] boards );

//        [OperationContract]
//        void DeleteSecurities( Guid sessionId, string[] securityIds );

//        [OperationContract]
//        void DeleteExchanges( Guid sessionId, string[] codes );

//        [OperationContract]
//        void DeleteExchangeBoards( Guid sessionId, string[] codes );

//        [OperationContract]
//        string[] GetSecurityExtendedStorages( Guid sessionId );

//        [OperationContract]
//        Tuple< string, string >[] GetSecurityExtendedFields( Guid sessionId, string storageName );

//        [OperationContract]
//        string[] GetExtendedInfoSecurities( Guid sessionId, string storageName );

//        [OperationContract]
//        string[] GetSecurityExtendedInfo( Guid sessionId, string storageName, string securityId );

//        [OperationContract]
//        Tuple< string, string[] >[] GetAllExtendedInfo( Guid sessionId, string storageName );

//        [OperationContract]
//        void CreateSecurityExtendedFields(
//      Guid sessionId,
//      string storageName,
//      Tuple< string, string >[] fields );

//        [OperationContract]
//        void DeleteSecurityExtendedFields( Guid sessionId, string storageName );

//        [OperationContract]
//        void AddSecurityExtendedInfo(
//      Guid sessionId,
//      string storageName,
//      string securityId,
//      string[] fieldValues );

//        [OperationContract]
//        void DeleteSecurityExtendedInfo( Guid sessionId, string storageName, string securityId );

//        [OperationContract]
//        Tuple< string, string[], UserPermissions >[] GetUsers( Guid sessionId );

//        [OperationContract]
//        void SaveUser(
//      Guid sessionId,
//      string login,
//      string password,
//      string[] ipAddresses,
//      UserPermissions permissions );

//        [OperationContract]
//        void DeleteUser( Guid sessionId, string login );

//        [OperationContract]
//        void Restart( Guid sessionId );

//        [OperationContract]
//        bool StartDownloading( Guid sessionId );

//        [OperationContract]
//        void StopDownloading( Guid sessionId );

//        [OperationContract]
//        DateTime[] GetDates(
//      Guid sessionId,
//      string securityId,
//      string dataType,
//      string arg,
//      StorageFormats format );

//        [OperationContract]
//        string[] GetAvailableSecurities( Guid sessionId );

//        [OperationContract]
//        Tuple< string, object >[] GetAvailableDataTypes(
//      Guid sessionId,
//      string securityId,
//      StorageFormats format );

//        [OperationContract]
//        void Save(
//      Guid sessionId,
//      string securityId,
//      string dataType,
//      string arg,
//      DateTime date,
//      StorageFormats format,
//      byte[] data );

//        [OperationContract]
//        void Delete(
//      Guid sessionId,
//      string securityId,
//      string dataType,
//      string arg,
//      DateTime date,
//      StorageFormats format );

//        [OperationContract]
//        Stream LoadStream(
//      Guid sessionId,
//      string securityId,
//      string dataType,
//      string arg,
//      DateTime date,
//      StorageFormats format );
//  }
//}
