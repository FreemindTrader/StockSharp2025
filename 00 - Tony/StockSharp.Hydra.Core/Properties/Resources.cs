// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Core.Properties.Resources
// Assembly: StockSharp.Hydra.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BF4FBD4E-7629-47D5-B0AC-6D48C0A60551
// Assembly location: T:\00-StockSharp\Data\StockSharp.Hydra.Core.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace StockSharp.Hydra.Core.Properties
{
  /// <summary>
  ///   A strongly-typed resource class, for looking up localized strings, etc.
  /// </summary>
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    /// <summary>
    ///   Returns the cached ResourceManager instance used by this class.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (StockSharp.Hydra.Core.Properties.Resources.resourceMan == null)
          StockSharp.Hydra.Core.Properties.Resources.resourceMan = new ResourceManager("StockSharp.Hydra.Core.Properties.Resources", typeof (StockSharp.Hydra.Core.Properties.Resources).Assembly);
        return StockSharp.Hydra.Core.Properties.Resources.resourceMan;
      }
    }

    /// <summary>
    ///   Overrides the current thread's CurrentUICulture property for all
    ///   resource lookups using this strongly typed resource class.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return StockSharp.Hydra.Core.Properties.Resources.resourceCulture;
      }
      set
      {
        StockSharp.Hydra.Core.Properties.Resources.resourceCulture = value;
      }
    }

    /// <summary>
    ///    Looks up a localized string similar to ABORT
    /// ACTION
    /// ADD
    /// AFTER
    /// ALL
    /// ALTER
    /// ANALYZE
    /// AND
    /// AS
    /// ASC
    /// ATTACH
    /// AUTOINCREMENT
    /// BEFORE
    /// BEGIN
    /// BETWEEN
    /// BY
    /// CASCADE
    /// CASE
    /// CAST
    /// CHECK
    /// COLLATE
    /// COLUMN
    /// COMMIT
    /// CONFLICT
    /// CONSTRAINT
    /// CREATE
    /// CROSS
    /// CURRENT_DATE
    /// CURRENT_TIME
    /// CURRENT_TIMESTAMP
    /// DATABASE
    /// DEFAULT
    /// DEFERRABLE
    /// DEFERRED
    /// DELETE
    /// DESC
    /// DETACH
    /// DISTINCT
    /// DROP
    /// EACH
    /// ELSE
    /// END
    /// ESCAPE
    /// EXCEPT
    /// EXCLUSIVE
    /// EXISTS
    /// EXPLAIN
    /// FAIL
    /// FOR
    /// FOREIGN
    /// FROM
    /// FULL
    /// GLOB
    /// GROUP
    /// HAVING
    /// IF
    /// IGNORE
    /// IMMEDIATE
    /// IN
    /// INDEX
    /// INDEXED
    /// INITIALLY
    /// INNER
    /// INSERT
    ///  [rest of string was truncated]";.
    ///  </summary>
    internal static string SQLiteReservedWords
    {
      get
      {
        return StockSharp.Hydra.Core.Properties.Resources.ResourceManager.GetString(nameof (SQLiteReservedWords), StockSharp.Hydra.Core.Properties.Resources.resourceCulture);
      }
    }

    /// <summary>
    ///    Looks up a localized string similar to add
    /// except
    /// percent
    /// all
    /// exec
    /// plan
    /// alter
    /// execute
    /// precision
    /// and
    /// exists
    /// primary
    /// any
    /// exit
    /// print
    /// as
    /// fetch
    /// proc
    /// asc
    /// file
    /// procedure
    /// authorization
    /// fillfactor
    /// public
    /// backup
    /// for
    /// raiserror
    /// begin
    /// foreign
    /// read
    /// between
    /// freetext
    /// readtext
    /// break
    /// freetexttable
    /// reconfigure
    /// browse
    /// from
    /// references
    /// bulk
    /// full
    /// replication
    /// by
    /// function
    /// restore
    /// cascade
    /// goto
    /// restrict
    /// case
    /// grant
    /// return
    /// check
    /// group
    /// revoke
    /// checkpoint
    /// having
    /// right
    /// close
    /// holdlock
    /// rollback
    /// clustered
    /// identity
    /// rowco [rest of string was truncated]";.
    ///  </summary>
    internal static string SqlServerReservedWords
    {
      get
      {
        return StockSharp.Hydra.Core.Properties.Resources.ResourceManager.GetString(nameof (SqlServerReservedWords), StockSharp.Hydra.Core.Properties.Resources.resourceCulture);
      }
    }
  }
}
