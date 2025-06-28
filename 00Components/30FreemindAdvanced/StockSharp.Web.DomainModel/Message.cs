// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Message
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Common;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class Message : BaseEntity, IClientEntity, ITopicEntity, INameEntity, IDescriptionEntity
{
    private string _text;

    public string Text
    {
        get => this._text;
        set
        {
            this._text = value;
            if (!StringHelper.IsEmpty(this.Body))
                return;
            this.Body = value;
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

    [Obsolete("Use Text property.")]
    public string Body { get; set; }

    public string GetText() => StringHelper.IsEmpty(this.Text, this.Body);

    public BaseEntitySet<File> Attachments { get; set; }

    public BaseEntitySet<MessageHistory> History { get; set; }

    public BaseEntitySet<MessagePatch> Patches { get; set; }

    public BaseEntitySet<MessageVote> Votes { get; set; }

    string INameEntity.Name
    {
        get
        {
            string text = this.Text;
            return text == null ? (string)null : StringHelper.Truncate(text, 50);
        }
        set => this.Text = value;
    }

    string IDescriptionEntity.Description
    {
        get => this.Text;
        set => this.Text = value;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Text = storage.GetValue<string>("Text", (string)null);
        this.Parent = storage.GetValue<Message>("Parent", (Message)null);
        this.Topic = storage.GetValue<Topic>("Topic", (Topic)null);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.UrlRelative = storage.GetValue<string>("UrlRelative", (string)null);
        this.PageId = storage.GetValue<string>("PageId", (string)null);
        this.AllowHtml = storage.GetValue<bool?>("AllowHtml", new bool?());
        this.AllowEdit = storage.GetValue<bool?>("AllowEdit", new bool?());
        this.AllowDelete = storage.GetValue<bool?>("AllowDelete", new bool?());
        this.AllowNewTopic = storage.GetValue<bool?>("AllowNewTopic", new bool?());
        this.AllowSelectExecutor = storage.GetValue<bool?>("AllowSelectExecutor", new bool?());
        this.IsSelectedAsExecutor = storage.GetValue<bool?>("IsSelectedAsExecutor", new bool?());
        this.AllowThank = storage.GetValue<bool?>("AllowThank", new bool?());
        this.Html = storage.GetValue<string>("Html", (string)null);
        this.PageNum = storage.GetValue<int?>("PageNum", new int?());
        this.IsVideo = storage.GetValue<bool?>("IsVideo", new bool?());
        this.Body = storage.GetValue<string>("Body", (string)null);
        this.Attachments = storage.GetValue<BaseEntitySet<File>>("Attachments", (BaseEntitySet<File>)null);
        this.History = storage.GetValue<BaseEntitySet<MessageHistory>>("History", (BaseEntitySet<MessageHistory>)null);
        this.Patches = storage.GetValue<BaseEntitySet<MessagePatch>>("Patches", (BaseEntitySet<MessagePatch>)null);
        this.Votes = storage.GetValue<BaseEntitySet<MessageVote>>("Votes", (BaseEntitySet<MessageVote>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Text", this.Text).Set<Message>("Parent", this.Parent).Set<Topic>("Topic", this.Topic).Set<Client>("Client", this.Client).Set<string>("UrlRelative", this.UrlRelative).Set<string>("PageId", this.PageId).Set<bool?>("AllowHtml", this.AllowHtml).Set<bool?>("AllowEdit", this.AllowEdit).Set<bool?>("AllowDelete", this.AllowDelete).Set<bool?>("AllowNewTopic", this.AllowNewTopic).Set<bool?>("AllowSelectExecutor", this.AllowSelectExecutor).Set<bool?>("IsSelectedAsExecutor", this.IsSelectedAsExecutor).Set<bool?>("AllowThank", this.AllowThank).Set<string>("Html", this.Html).Set<int?>("PageNum", this.PageNum).Set<bool?>("IsVideo", this.IsVideo).Set<string>("Body", this.Body).Set<BaseEntitySet<File>>("Attachments", this.Attachments).Set<BaseEntitySet<MessageHistory>>("History", this.History).Set<BaseEntitySet<MessagePatch>>("Patches", this.Patches).Set<BaseEntitySet<MessageVote>>("Votes", this.Votes);
    }
}
