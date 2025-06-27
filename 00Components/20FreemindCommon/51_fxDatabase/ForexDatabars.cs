namespace fx.Database
{
    using System;

//#if ( NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP3_0 || NETCOREAPP3_1 )
//using Microsoft.EntityFrameworkCore;
//#else
    using System.Data.Entity;
    using System.Data.Common;

//#endif


    using System.IO;
    //using StockSharp.Hydra.Core;
    using System.Linq;
    using Ecng.Data;

    public partial class ForexDatabars : DbContext
    {
        static ForexDatabars( )
        {
            Database.SetInitializer<ForexDatabars>( null );
        }

        public ForexDatabars() : base( CreateConnection(), true) 
        {

        }
        public ForexDatabars(string connectionString) : base( connectionString ) 
        {

        }

        public ForexDatabars( DbConnection connection ) : base( connection, true ) { }
        

        public void DisableAutoDetectChanges()
        {
            Configuration.AutoDetectChangesEnabled = false;
        }

        public void EnableAutoDetectChanges( )
        {
            Configuration.AutoDetectChangesEnabled = true;
        }

        
        public virtual DbSet< DbAccounts >            ACCOUNTS         { get; set; }

        public virtual DbSet< DbSymbolsInfo >         SYMBOLSINFO      { get; set; }

        public virtual DbSet< DbClosedTrade >         CLOSEDTRADES     { get; set; }

        public virtual DbSet< DetailedOrderDB >       ORDERS           { get; set; }

        public virtual DbSet< DbElliottWave >         ELLIOTTWAVE      { get; set; }

        public virtual DbSet< DbBaZi >                BAZI             { get; set; }

        public virtual DbSet< DbSmartWaveCycles >     SMARTWAVECYCLES  { get; set; }

        public virtual DbSet< DBSubscribedSymbols >   SUBSCRIBEDSYMBOL { get; set; }

        public virtual DbSet<DbSafetyNetPips>         SAFETYNETPIPS    { get; set; }

        public virtual DbSet<DbLockedPositions> LOCKEDPOSITIONS { get; set; }


//#if ( NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP3_0 || NETCOREAPP3_1 )

//        protected override void OnModelCreating( Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder )
//        {
//            modelBuilder.Entity<DbSymbolsInfo>( ).ToTable( "SymbolsInfo" );
//            modelBuilder.Entity<DbSymbolsInfo>( ).Ignore( s => s.StartDate );

//            modelBuilder.Entity<DbSmartWaveCycles>( ).ToTable( "SmartWaveCycles" );
//            modelBuilder.Entity<DbSmartWaveCycles>( ).Ignore( s => s.StartDate );
//            modelBuilder.Entity<DbSmartWaveCycles>( ).Property( m => m.WaveCycle2 ).IsOptional( );
//            modelBuilder.Entity<DbSmartWaveCycles>( ).Property( m => m.WaveCycle3 ).IsOptional( );
//            modelBuilder.Entity<DbSmartWaveCycles>( ).Property( m => m.WaveCycle4 ).IsOptional( );

//            modelBuilder.Entity<DbClosedTrade>( ).ToTable( "ClosedTrades" );
//            modelBuilder.Entity<DbClosedTrade>( ).Ignore( s => s.StartDate );

//            modelBuilder.Entity<DetailedOrderDB>( ).ToTable( "Orders" );
//            modelBuilder.Entity<DetailedOrderDB>( ).Ignore( s => s.StartDate );
//            modelBuilder.Entity<DetailedOrderDB>( ).Ignore( s => s.WorkingIndicator );
//            modelBuilder.Entity<DetailedOrderDB>( ).Ignore( s => s.ExpireDateDT );
//            modelBuilder.Entity<DetailedOrderDB>( ).Ignore( s => s.StatusTimeDT );
//            modelBuilder.Entity<DetailedOrderDB>( ).Ignore( s => s.MainLoginName );

//            modelBuilder.Entity<DbElliottWave>( ).ToTable( "ElliottWaves" );
//            modelBuilder.Entity<DbElliottWave>( ).Ignore( s => s.BeginTimeUTC );            
//            modelBuilder.Entity<DbElliottWave>( ).Ignore( s => s.ElliottWave );
//            modelBuilder.Entity<DbElliottWave>( ).Ignore( s => s.AlternativeWave );            
//            modelBuilder.Entity<DbElliottWave>( ).Ignore( s => s.OwningBar );

//            modelBuilder.Entity<DbAccounts>( ).ToTable( "Accounts" );
//            modelBuilder.Entity<DbAccounts>( ).Ignore( s => s.StartDate );
//            modelBuilder.Entity<DbAccounts>( ).Ignore( s => s.Status );
//            modelBuilder.Entity<DbAccounts>( ).Ignore( s => s.IsStarted );

//            modelBuilder.Entity<DbBaZi>( ).ToTable( "BaZi" );
//            modelBuilder.Entity<DbBaZi>( ).Ignore( s => s.StartDate );

//            modelBuilder.Entity<DBSubscribedSymbols>( ).ToTable( "SubscribedSymbols" );
//            modelBuilder.Entity<DBSubscribedSymbols>( ).Ignore( s => s.StartDate );

//            modelBuilder.Entity<DbSafetyNetPips>( ).ToTable( "SafetyNetPips" );
//            modelBuilder.Entity<DbSafetyNetPips>( ).Ignore( s => s.StartDate );

//            modelBuilder.Entity<DbForexNews>( ).ToTable( "NewsEvents" );
//            modelBuilder.Entity<DbForexNews>( ).Property( e => e.StartDate ).HasColumnName( "EventTime" );
//            modelBuilder.Entity<DbForexNews>( ).Ignore( s => s.EventTimeUTC );
//            modelBuilder.Entity<DbForexNews>( ).Ignore( s => s.HasTime );

//            modelBuilder.Entity<DbLockedPositions>( ).ToTable( "LockedPositions" );
//            modelBuilder.Entity<DbLockedPositions>( ).Ignore( s => s.StartDate );
//       }
//#else



        protected override void OnModelCreating( DbModelBuilder modelBuilder )
        {
            modelBuilder.Entity<DbSymbolsInfo>( ).ToTable( "SymbolsInfo" );
            modelBuilder.Entity<DbSymbolsInfo>( ).Ignore( s => s.StartDate );

            modelBuilder.Entity<DbSmartWaveCycles>( ).ToTable( "SmartWaveCycles" );
            modelBuilder.Entity<DbSmartWaveCycles>( ).Ignore( s => s.StartDate );
            modelBuilder.Entity<DbSmartWaveCycles>( ).Property( m => m.WaveCycle2 ).IsOptional( );
            modelBuilder.Entity<DbSmartWaveCycles>( ).Property( m => m.WaveCycle3 ).IsOptional( );
            modelBuilder.Entity<DbSmartWaveCycles>( ).Property( m => m.WaveCycle4 ).IsOptional( );

            modelBuilder.Entity<DbClosedTrade>( ).ToTable( "ClosedTrades" );
            modelBuilder.Entity<DbClosedTrade>( ).Ignore( s => s.StartDate );

            modelBuilder.Entity<DetailedOrderDB>( ).ToTable( "Orders" );
            modelBuilder.Entity<DetailedOrderDB>( ).Ignore( s => s.StartDate );
            modelBuilder.Entity<DetailedOrderDB>( ).Ignore( s => s.WorkingIndicator );
            modelBuilder.Entity<DetailedOrderDB>( ).Ignore( s => s.ExpireDateDT );
            modelBuilder.Entity<DetailedOrderDB>( ).Ignore( s => s.StatusTimeDT );
            modelBuilder.Entity<DetailedOrderDB>( ).Ignore( s => s.MainLoginName );

            modelBuilder.Entity<DbElliottWave>( ).ToTable( "ElliottWaves" );
            modelBuilder.Entity<DbElliottWave>( ).Ignore( s => s.BeginTimeUTC );            
            

            modelBuilder.Entity<DbAccounts>( ).ToTable( "Accounts" );
            modelBuilder.Entity<DbAccounts>( ).Ignore( s => s.StartDate );
            modelBuilder.Entity<DbAccounts>( ).Ignore( s => s.Status );
            modelBuilder.Entity<DbAccounts>( ).Ignore( s => s.IsStarted );

            modelBuilder.Entity<DbBaZi>( ).ToTable( "BaZi" );
            modelBuilder.Entity<DbBaZi>( ).Ignore( s => s.StartDate );

            modelBuilder.Entity<DBSubscribedSymbols>( ).ToTable( "SubscribedSymbols" );
            modelBuilder.Entity<DBSubscribedSymbols>( ).Ignore( s => s.StartDate );

            modelBuilder.Entity<DbSafetyNetPips>( ).ToTable( "SafetyNetPips" );
            modelBuilder.Entity<DbSafetyNetPips>( ).Ignore( s => s.StartDate );

            modelBuilder.Entity<DbForexNews>( ).ToTable( "ForexNews" );
            modelBuilder.Entity<DbForexNews>( ).Ignore( e => e.StartDate );
            modelBuilder.Entity<DbForexNews>( ).Ignore( s => s.NewsTimeUTC);
            
            modelBuilder.Entity<DbLockedPositions>( ).ToTable( "LockedPositions" );
            modelBuilder.Entity<DbLockedPositions>( ).Ignore( s => s.StartDate );
        }
//#endif
        //static string filePath;

        static DbConnection CreateConnection( )
        {            


            DbProviderFactories.RegisterFactory( "System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance );


            var connection   = DbProviderFactories.GetFactory( "System.Data.SqlClient" ).CreateConnection( );

            var connectionString = string.Empty;

            connectionString = "server=tcp:localhost;Integrated Security=False;database=FreemindDB;User ID=sa;Password=QqGg20061203";
            
                /*
                 * Integrated Security = False : User ID and Password are specified in the connection. 
                 * Integrated Security = true : the current Windows account credentials are used for authentication.
                 * Integrated Security = SSPI : this is equivalant to true.
                 * 
                 * We can avoid the username and password attributes from the connection string and use the Integrated Security
                 * 
                 */
                
            
            

            connection.ConnectionString = connectionString;
            return connection;
        }
    }
}
