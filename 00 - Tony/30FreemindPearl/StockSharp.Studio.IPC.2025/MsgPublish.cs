// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.IPC.MsgPublish
// Assembly: StockSharp.Studio.IPC, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65FF0FD2-B114-4B6B-959A-42B33214A877
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.IPC.dll

using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace StockSharp.Studio.IPC
{
    /// <summary>Message from app to publish content.</summary>
    public class MsgPublish : StudioMessage
    {
        /// <summary>
        /// <see cref="T:StockSharp.Web.DomainModel.ProductGroup" />.
        /// </summary>
        public long [ ] Groups { get; set; }

        /// <summary>User id of content.</summary>
        public string UserId { get; set; }

        /// <summary>Name.</summary>
        public string Name { get; set; }

        /// <summary>Description.</summary>
        public string Description { get; set; }

        /// <summary>Content type.</summary>
        public ProductContentTypes2 ContentType { get; set; }

        /// <summary>Content path.</summary>
        public string ContentPath { get; set; }

        /// <summary>Icon identifier.</summary>
        public long IconId { get; set; }

        /// <inheritdoc />
        public override void Load( SettingsStorage ss )
        {
            base.Load( ss );
            this.Groups = ( long [ ] ) ss.GetValue<long [ ]>( "Groups",  null );
            this.UserId = ( string ) ss.GetValue<string>( "UserId",  null );
            this.Name = ( string ) ss.GetValue<string>( "Name",  null );
            this.Description = ( string ) ss.GetValue<string>( "Description",  null );
            this.IconId = ( long ) ss.GetValue<long>( "IconId",  0L );
            this.ContentType = ( ProductContentTypes2 ) ss.GetValue<ProductContentTypes2>( "ContentType",  0L );
            this.ContentPath = ( string ) ss.GetValue<string>( "ContentPath",  null );
        }

        /// <inheritdoc />
        public override void Save( SettingsStorage ss )
        {
            base.Save( ss );
            ss.Set<long [ ]>( "Groups",  this.Groups ).Set<string>( "UserId",  this.UserId ).Set<string>( "Name",  this.Name ).Set<string>( "Description",  this.Description ).Set<long>( "IconId",  this.IconId ).Set<ProductContentTypes2>( "ContentType",  this.ContentType ).Set<string>( "ContentPath",  this.ContentPath );
        }

        /// <inheritdoc />
        public override string ToString()
        {
            string str1 = base.ToString();
            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 5);
            interpolatedStringHandler.AppendLiteral( ", name='" );
            interpolatedStringHandler.AppendFormatted( this.Name );
            interpolatedStringHandler.AppendLiteral( "', icon=" );
            interpolatedStringHandler.AppendFormatted<long>( this.IconId );
            interpolatedStringHandler.AppendLiteral( ", type=" );
            interpolatedStringHandler.AppendFormatted<ProductContentTypes2>( this.ContentType );
            interpolatedStringHandler.AppendLiteral( ", path='" );
            interpolatedStringHandler.AppendFormatted( this.ContentPath );
            interpolatedStringHandler.AppendLiteral( "', Groups=(" );
            ref DefaultInterpolatedStringHandler local = ref interpolatedStringHandler;
            long[] groups = this.Groups;
            string str2 = groups != null ? StringHelper.JoinCommaSpace(((IEnumerable<long>) groups).Select<long, string>((Func<long, string>) (u => u.ToString()))) : (string) null;
            local.AppendFormatted( str2 );
            interpolatedStringHandler.AppendLiteral( ")" );
            string stringAndClear = interpolatedStringHandler.ToStringAndClear();
            return str1 + stringAndClear;
        }
    }
}
