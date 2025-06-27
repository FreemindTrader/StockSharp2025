// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.BaseEntity
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public abstract class BaseEntity : IEquatable<BaseEntity>, IPersistable
    {
        public long Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public bool Deleted { get; set; }

        public Client CreatedBy { get; set; }

        public DateTime CreationDateLocal
        {
            get
            {
                return this.CreationDate.ToLocalTime();
            }
        }

        public DateTime ModificationDateLocal
        {
            get
            {
                return this.ModificationDate.ToLocalTime();
            }
        }

        public string IP { get; set; }

        public override string ToString()
        {
            return string.Format( "{{x:Type={0}, Id={1}}}", ( object ) this.GetType(), ( object ) this.Id );
        }

        public override bool Equals( object obj )
        {
            BaseEntity other = obj as BaseEntity;
            if ( other != null )
                return this.Equals( other );
            return false;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public bool Equals( BaseEntity other )
        {
            if ( this.GetType() == other.GetType() )
                return this.Id == other.Id;
            return false;
        }

        public virtual void Load( SettingsStorage storage )
        {
            this.Id = ( long ) storage.GetValue<long>( "Id", 0L );
            this.CreationDate = ( DateTime ) storage.GetValue<DateTime>( "CreationDate", new DateTime() );
            this.ModificationDate = ( DateTime ) storage.GetValue<DateTime>( "ModificationDate", new DateTime() );
            this.Deleted = ( bool ) storage.GetValue<bool>( "Deleted", false );
            this.CreatedBy = ( Client ) storage.GetValue<Client>( "CreatedBy", null );
            this.IP = ( string ) storage.GetValue<string>( "IP", null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<long>( "Id", this.Id ).Set<DateTime>( "CreationDate", this.CreationDate ).Set<DateTime>( "ModificationDate", this.ModificationDate ).Set<bool>( "Deleted", ( this.Deleted ) ).Set<Client>( "CreatedBy", this.CreatedBy ).Set<string>( "IP", this.IP );
        }
    }
}
