// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Message
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Common;
using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class Message : BaseEntity, IClientEntity, ITopicEntity, INameEntity, IDescriptionEntity
    {
        private string _text;

        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
                if ( !StringHelper.IsEmpty( this.Text ) )
                    return;
                this.Text = value;
            }
        }

        public Message Parent { get; set; }

        public Topic Topic { get; set; }

        public Client Client { get; set; }

        public string UrlRelative { get; set; }

        public string PageId { get; set; }

        public bool? AllowHtml { get; set; }

        public bool? AllowEdit { get; set; }

        public bool? AllowDelete { get; set; }

        public bool? AllowNewTopic { get; set; }

        public bool? AllowSelectExecutor { get; set; }

        public bool? IsSelectedAsExecutor { get; set; }

        public bool? AllowThank { get; set; }

        public string Html { get; set; }

        public int? PageNum { get; set; }

        public bool? IsVideo { get; set; }

        [Obsolete( "Use Text property." )]
        public string Body { get; set; }

        public string GetText()
        {
            return StringHelper.IsEmpty( this.Text, this.Text );
        }

        public BaseEntitySet<File> Attachments { get; set; }

        public BaseEntitySet<MessageHistory> History { get; set; }

        public BaseEntitySet<MessagePatch> Patches { get; set; }

        public BaseEntitySet<MessageVote> Votes { get; set; }

        string INameEntity.Name
        {
            get
            {
                string text = this.Text;
                if ( text == null )
                    return ( string ) null;
                return StringHelper.Truncate( text, 50 );
            }
            set
            {
                this.Text = value;
            }
        }

        string IDescriptionEntity.Description
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Text = ( string ) storage.GetValue<string>( "Text", null );
            this.Parent = ( Message ) storage.GetValue<Message>( "Parent", null );
            this.Topic = ( Topic ) storage.GetValue<Topic>( "Topic", null );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.UrlRelative = ( string ) storage.GetValue<string>( "UrlRelative", null );
            this.PageId = ( string ) storage.GetValue<string>( "PageId", null );
            this.AllowHtml = ( bool? ) storage.GetValue<bool?>( "AllowHtml", new bool?() );
            this.AllowEdit = ( bool? ) storage.GetValue<bool?>( "AllowEdit", new bool?() );
            this.AllowDelete = ( bool? ) storage.GetValue<bool?>( "AllowDelete", new bool?() );
            this.AllowNewTopic = ( bool? ) storage.GetValue<bool?>( "AllowNewTopic", new bool?() );
            this.AllowSelectExecutor = ( bool? ) storage.GetValue<bool?>( "AllowSelectExecutor", new bool?() );
            this.IsSelectedAsExecutor = ( bool? ) storage.GetValue<bool?>( "IsSelectedAsExecutor", new bool?() );
            this.AllowThank = ( bool? ) storage.GetValue<bool?>( "AllowThank", new bool?() );
            this.Html = ( string ) storage.GetValue<string>( "Html", null );
            this.PageNum = ( int? ) storage.GetValue<int?>( "PageNum", new int?() );
            this.IsVideo = ( bool? ) storage.GetValue<bool?>( "IsVideo", new bool?() );
            this.Body = ( string ) storage.GetValue<string>( "Body", null );
            this.Attachments = ( BaseEntitySet<File> ) storage.GetValue<BaseEntitySet<File>>( "Attachments", null );
            this.History = ( BaseEntitySet<MessageHistory> ) storage.GetValue<BaseEntitySet<MessageHistory>>( "History", null );
            this.Patches = ( BaseEntitySet<MessagePatch> ) storage.GetValue<BaseEntitySet<MessagePatch>>( "Patches", null );
            this.Votes = ( BaseEntitySet<MessageVote> ) storage.GetValue<BaseEntitySet<MessageVote>>( "Votes", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "Text", this.Text ).Set<Message>( "Parent", this.Parent ).Set<Topic>( "Topic", this.Topic ).Set<Client>( "Client", this.Client ).Set<string>( "UrlRelative", this.UrlRelative ).Set<string>( "PageId", this.PageId ).Set<bool?>( "AllowHtml", this.AllowHtml ).Set<bool?>( "AllowEdit", this.AllowEdit ).Set<bool?>( "AllowDelete", this.AllowDelete ).Set<bool?>( "AllowNewTopic", this.AllowNewTopic ).Set<bool?>( "AllowSelectExecutor", this.AllowSelectExecutor ).Set<bool?>( "IsSelectedAsExecutor", this.IsSelectedAsExecutor ).Set<bool?>( "AllowThank", this.AllowThank ).Set<string>( "Html", this.Html ).Set<int?>( "PageNum", this.PageNum ).Set<bool?>( "IsVideo", this.IsVideo ).Set<string>( "Body", this.Body ).Set<BaseEntitySet<File>>( "Attachments", this.Attachments ).Set<BaseEntitySet<MessageHistory>>( "History", this.History ).Set<BaseEntitySet<MessagePatch>>( "Patches", this.Patches ).Set<BaseEntitySet<MessageVote>>( "Votes", this.Votes );
        }
    }
}
