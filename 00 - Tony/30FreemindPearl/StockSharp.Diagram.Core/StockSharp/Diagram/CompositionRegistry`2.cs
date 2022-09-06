// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.CompositionRegistry`2
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Security;
using Ecng.Serialization;
using StockSharp.Configuration;
using StockSharp.Localization;
using StockSharp.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security;

namespace StockSharp.Diagram
{
    /// <summary>
    /// Default <see cref="T:StockSharp.Diagram.ICompositionRegistry" /> implementation.
    /// </summary>
    /// <typeparam name="TNode">Node type.</typeparam>
    /// <typeparam name="TLink">Link type.</typeparam>
    public class CompositionRegistry<TNode, TLink> : ICompositionRegistry where TNode : ICompositionModelNode, new() where TLink : ICompositionModelLink, new()
    {

        private static readonly Version _version = new Version( 1, 0 );

        private static readonly SecureString _password = nameof( -1260198100 ).Secure();

        private static readonly byte[ ] _byteArray = nameof( -1260198093 ).ASCII();

        private readonly SynchronizedDictionary<Guid, List<ValueTuple<TNode, SettingsStorage>>> _guidDictionary = new SynchronizedDictionary<Guid, List<ValueTuple<TNode, SettingsStorage>>>();

        private readonly Func<ICompositionModelBehavior<TNode, TLink>> _createBehaviorFunc;

        private readonly SynchronizedSet<DiagramElement> _diagramElements;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.CompositionRegistry`2" />.
        /// </summary>
        /// <param name="createBehavior">
        ///   <see cref="T:StockSharp.Diagram.ICompositionModelBehavior`2" />
        /// </param>
        public CompositionRegistry(
          Func<ICompositionModelBehavior<TNode, TLink>> createBehavior )
        {
            this._createBehaviorFunc = createBehavior;
            this._diagramElements = new SynchronizedSet<DiagramElement>();
            this._diagramElements.Added += new Action<DiagramElement>( this.OnDiagramElementsAdded );
        }

        /// <inheritdoc />
        public INotifyList<DiagramElement> DiagramElements
        {
            get
            {
                return ( INotifyList<DiagramElement> )this._diagramElements;
            }
        }

        private CompositionModel<TNode, TLink> CreateNewBehavior()
        {
            return new CompositionModel<TNode, TLink>( this._createBehaviorFunc() );
        }

        /// <inheritdoc />
        public CompositionDiagramElement CreateComposition()
        {
            return new CompositionDiagramElement( ( ICompositionModel )this.CreateNewBehavior() );
        }

        /// <inheritdoc />
        public SettingsStorage Serialize(
          CompositionDiagramElement element,
          SchemeTypes? schemeType,
          SecureString password = null )
        {
            if ( element == null )
                throw new ArgumentNullException( "element == null" );
            schemeType.GetValueOrDefault();
            if ( !schemeType.HasValue )
                schemeType = new SchemeTypes?( element.Type );
            SettingsStorage settings = this._settingStorage( element, schemeType.Value );
            SettingsStorage settingsStorage = new SettingsStorage().Set<string>( nameof( -1260198127 ), ( ( object )CompositionRegistry<TNode, TLink>._version ).ToString() ).Set<SchemeTypes?>( nameof( -1260198464 ), schemeType );

            if ( schemeType.HasValue && schemeType.GetValueOrDefault() == SchemeTypes.Encrypted )
            {
                PasswordType passwordType = password == null ? PasswordType.Predefined : PasswordType.User;
                settingsStorage.SetValue<PasswordType>( nameof( -1260197917 ), passwordType );
                if ( passwordType == PasswordType.UserPredefined )
                    settingsStorage.SetValue<SecureString>( nameof( -1260197938 ), password );
                string str = settings.SerializeInvariant().Encrypt( ( passwordType == PasswordType.Predefined ? CompositionRegistry<TNode, TLink>._password : password ).UnSecure(), CompositionRegistry<TNode, TLink>._byteArray, CompositionRegistry<TNode, TLink>._byteArray ).Base64();
                settingsStorage.SetValue<string>( nameof( -1260197951 ), str );
            }
            else
                settingsStorage.SetValue<SettingsStorage>( nameof( -1260197951 ), settings );
            return settingsStorage;
        }

        /// <inheritdoc />
        public void Deserialize( CompositionDiagramElement element, SettingsStorage container, Func<SecureString> getPassword )
        {
            if ( container == null )
                throw new ArgumentNullException( nameof( -1260197934 ) );
            Version version = container.GetValue<string>( nameof( -1260198127 ), nameof( -1260197975 ) ).To<Version>();
            if ( version > CompositionRegistry<TNode, TLink>._version )
                throw new InvalidOperationException( LocalizedStrings.UnsupportedSchemeVersionParams.Put( new object[1] { ( object )version } ) );
            SchemeTypes schemeTypes = container.GetValue<SchemeTypes>( nameof( -1260198464 ), SchemeTypes.Regular );
            SettingsStorage settingsStorage;
            switch ( schemeTypes )
            {
                case SchemeTypes.Regular:
                case SchemeTypes.Independent:
                    settingsStorage = container.GetValue<SettingsStorage>( nameof( -1260197951 ), ( SettingsStorage )null );
                    break;
                case SchemeTypes.Encrypted:
                    string str = container.GetValue<string>( nameof( -1260197951 ), ( string )null );
                    PasswordType passwordType = container.GetValue<PasswordType>( nameof( -1260197917 ), PasswordType.Predefined );
                    SecureString secureString;
                    switch ( passwordType )
                    {
                        case PasswordType.Predefined:
                            secureString = CompositionRegistry<TNode, TLink>._password;
                            break;
                        case PasswordType.UserPredefined:
                            secureString = container.GetValue<SecureString>( nameof( -1260197938 ), ( SecureString )null );
                            break;
                        case PasswordType.User:
                            secureString = getPassword != null ? getPassword() : ( SecureString )null;
                            if ( secureString.IsEmpty() )
                                return;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException( nameof( -1260197953 ), ( object )passwordType, LocalizedStrings.Str1219 );
                    }
                    settingsStorage = str.Base64().Decrypt( secureString.UnSecure(), CompositionRegistry<TNode, TLink>._byteArray, CompositionRegistry<TNode, TLink>._byteArray ).DeserializeInvariant();
                    break;
                default:
                    throw new ArgumentOutOfRangeException( nameof( -1260198006 ), ( object )schemeTypes, LocalizedStrings.Str1219 );
            }
            this.\u0023\u003DzxekB70g\u003D(element, settingsStorage);
        }

        /// <inheritdoc />
        public CompositionDiagramElement Deserialize(
          SettingsStorage storage,
          Func<SecureString> getPassword )
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( -1260198015 ) );
            CompositionDiagramElement composition = this.CreateComposition();
            this.Deserialize( composition, storage, getPassword );
            return composition;
        }

        private void \u0023\u003DzxekB70g\u003D(
          CompositionDiagramElement _param1,
          SettingsStorage _param2)
    {
      if (_param1 == null)
        throw new ArgumentNullException( nameof(-1260197996));
      if (_param2 == null)
        throw new ArgumentNullException( nameof(-1260198293));
      _param1.SchemaVersion = _param2.GetValue<int>(nameof(-1260198274), _param1.SchemaVersion);
      _param1.Category = _param2.GetValue<string>(nameof(-1260198623), string.Empty);
      _param1.Description = _param2.GetValue<string>(nameof(-1260198608), string.Empty);
      _param1.DocUrl = _param2.GetValue<string>(nameof(-1260198326), string.Empty);
      _param1.Model = this.\u0023\u003DzJeCezA8\u003D(_param2.GetValue<SettingsStorage>(nameof(-1260198305), (SettingsStorage) null));
      _param1.Load(_param2.GetValue<SettingsStorage>(nameof(-1260198317), (SettingsStorage) null));
    }

    private SettingsStorage _settingStorage(
      CompositionDiagramElement _param1,
      SchemeTypes _param2 )
    {
        if ( _param1 == null )
            throw new ArgumentNullException( nameof( -1260198367 ) );
        SettingsStorage settingsStorage = new SettingsStorage();
        if ( _param2 != SchemeTypes.Regular )
        {
            _param1 = ( CompositionDiagramElement )_param1.Clone( true );
            _param1.Type = _param2;
        }
        settingsStorage.Set<int>( nameof( -1260198274 ), _param1.SchemaVersion ).Set<string>( nameof( -1260198623 ), _param1.Category ).Set<string>( nameof( -1260198608 ), _param1.Description ).Set<string>( nameof( -1260198326 ), _param1.DocUrl ).Set<SettingsStorage>( nameof( -1260198317 ), _param1.Save() ).Set<SettingsStorage>( nameof( -1260198305 ), this.\u0023\u003DzCJ7edCg\u003D(( CompositionModel<TNode, TLink> )_param1.Model, _param2) );
        return settingsStorage;
    }

    private SettingsStorage \u0023\u003DzCJ7edCg\u003D(
      CompositionModel<TNode, TLink> _param1,
      SchemeTypes _param2)
    {
      return new SettingsStorage().Set<SettingsStorage[ ]>(nameof(-1260198076), _param1.Nodes.Select<TNode, SettingsStorage>(new Func<TNode, SettingsStorage>(new CompositionRegistry<TNode, TLink>.\u0023\u003DzvJHhaci4iYfHtEdhfUcRlok\u003D() { _diagramElement = this, \u0023\u003DzNRGauKM\u003D = _param2 }.\u0023\u003DzDPUPpH_hx8r1X9K0ug\u003D\u003D)).ToArray<SettingsStorage>()).Set<SettingsStorage[ ]>(nameof(-1260198056), _param1.Links.Select<TLink, SettingsStorage>(new Func<TLink, SettingsStorage>(CompositionRegistry<TNode, TLink>.\u0023\u003DqJGQfjBqpUzb3dWyW4khy\u0024k5muPyu5l2iTBxAtPCFbNrO\u0024WI3oIf4X\u0024oBl6kYXByz)).ToArray<SettingsStorage>());
    }

private ICompositionModel \u0023\u003DzJeCezA8\u003D( SettingsStorage _param1)
    {
    CompositionRegistry<TNode, TLink>.\u0023\u003DzCzXqrtZqsvTUUPdRRikcY\u00244\u003D zqsvTuuPdRrikcY4 = new CompositionRegistry<TNode, TLink>.\u0023\u003DzCzXqrtZqsvTUUPdRRikcY\u00244\u003D();
    zqsvTuuPdRrikcY4._diagramElement = this;
    zqsvTuuPdRrikcY4.\u0023\u003DzvMnT7x4\u003D = _param1;
    CompositionModel<TNode, TLink> compositionModel = this.CreateNewBehavior();
    compositionModel.ExecuteTransaction( nameof( -1260198348 ), new Action<CompositionModel<TNode, TLink>>( zqsvTuuPdRrikcY4.\u0023\u003Dz5EfMCmtYdFWOPnSBMg\u003D\u003D) );
    return ( ICompositionModel )compositionModel;
}

CompositionDiagramElement ICompositionRegistry.\u0023\u003DzgNqd9cXJwuM1Jf1Gsq4\u0024n7LSBOGK(
  byte[ ] _param1,
  Func<SecureString> _param2)
    {
    return this.Deserialize( _param1.DeserializeInvariant(), _param2 );
}

byte[ ] ICompositionRegistry.\u0023\u003Dz1u6I_kRt3N7qEVGlc9Lg31x9b3ox(
  CompositionDiagramElement _param1,
  SchemeTypes? _param2,
  SecureString _param3)
    {
    return this.Serialize( _param1, _param2, _param3 ).SerializeInvariant();
}

CompositionDiagramElement ICompositionRegistry.\u0023\u003DzRoFWYIuCI35V0vlFtO3tTsVByEhL6vlQSQ\u003D\u003D(
  string _param1,
  Exception _param2)
    {
    if ( _param1.IsEmpty() )
        throw new ArgumentNullException( nameof( -1260198389 ) );
    if ( _param2 == null )
        throw new ArgumentNullException( nameof( -1260198370 ) );
    Guid guid = ( ( string )_param1 ).Replace( nameof( -1260198379 ), string.Empty ).Replace( '_', '-' ).To<Guid>();
    CompositionDiagramElement composition = this.CreateComposition();
    composition.Name = _param1;
    composition.Description = _param2.Message;
    composition.IsLoaded = false;
    composition.UpdateTypeId( new Guid?( guid ) );
    return composition;
}

private void OnDiagramElementsAdded( DiagramElement _param1 )
{
    List<ValueTuple<TNode, SettingsStorage>> valueTupleList;
    if ( !this._guidDictionary.TryGetAndRemove<Guid, List<ValueTuple<TNode, SettingsStorage>>>( _param1.TypeId, out valueTupleList ) )
        return;
    foreach ( ValueTuple<TNode, SettingsStorage> valueTuple in valueTupleList )
    {
        TNode node = valueTuple.Item1;
        SettingsStorage storage = valueTuple.Item2;
        DiagramElement diagramElement = _param1.Clone( false );
        diagramElement.Load( storage );
        node.Element = diagramElement;
    }
}

internal static SettingsStorage \u0023\u003DqJGQfjBqpUzb3dWyW4khy\u0024k5muPyu5l2iTBxAtPCFbNrO\u0024WI3oIf4X\u0024oBl6kYXByz(
  TLink _param0 )
    {
    return new SettingsStorage().Set<string>( nameof( -1260198167 ), _param0.From ).Set<string>( nameof( -1260198148 ), _param0.FromPort ).Set<string>( nameof( -1260198193 ), _param0.To ).Set<string>( nameof( -1260198204 ), _param0.ToPort );
}

internal static TLink \u0023\u003Dq9H57Q_kRTQ3DQ0AwfQHjWXg1g4wNikAr6CGKHaInf6NqG4JAYsbGdEYt9Emp1BSt(
  SettingsStorage _param0 )
    {
    return new TLink() { From = _param0.GetValue<string>( nameof( -1260198167 ), ( string )null ), FromPort = _param0.GetValue<string>( nameof( -1260198148 ), ( string )null ), To = _param0.GetValue<string>( nameof( -1260198193 ), ( string )null ), ToPort = _param0.GetValue<string>( nameof( -1260198204 ), ( string )null ) };
}

private sealed class \u0023\u003DzCzXqrtZqsvTUUPdRRikcY\u00244\u003D
    {
      public CompositionRegistry<TX, TY> _diagramElement;
public SettingsStorage \u0023\u003DzvMnT7x4\u003D;

internal TX \u0023\u003DqVSAESgQajnjlquqJ9T5x8QbgC6JV3ZhTYQZLcbdMF89JEhI7TwJ3lsK8vzZ6\u0024bCy(
  SettingsStorage _param1 )
      {
    float x = _param1.GetValue<float>( nameof( -1260198779 ), 0.0f );
    float y = _param1.GetValue<float>( nameof( -1260198755 ), 0.0f );
        TX zvelQgjE = new TX() { Key = _param1.GetValue<string>( nameof( -1260198769 ), ( string )null ), Location = new PointF( x, y ), Figure = _param1.GetValue<string>( nameof( -1260198763 ), ( string )null ) };
    if ( !_param1.ContainsKey( nameof( -1260198023 ) ) )
    {
        CompositionRegistry <TX, TY>.\u0023\u003DzX_G0QB3uBzc7uKhDfxuXVtY\u003D qb3uBzc7uKhDfxuXvtY = new CompositionRegistry<TX, TY>.\u0023\u003DzX_G0QB3uBzc7uKhDfxuXVtY\u003D();
        qb3uBzc7uKhDfxuXvtY.\u0023\u003DzmNVmtn8\u003D = _param1.GetValue<string>( nameof( -1260198449 ), ( string )null ).To<Guid>();
        SettingsStorage storage = _param1.GetValue<SettingsStorage>( nameof( -1260198042 ), ( SettingsStorage )null );
        zvelQgjE.TypeId = qb3uBzc7uKhDfxuXvtY.\u0023\u003DzmNVmtn8\u003D;
        // ISSUE: explicit non-virtual call
        DiagramElement diagramElement1 = __nonvirtual( this._diagramElement.DiagramElements ).FirstOrDefault<DiagramElement>( new Func<DiagramElement, bool>( qb3uBzc7uKhDfxuXvtY.\u0023\u003DzoETudYbWTZ5MdMRZ\u0024g\u003D\u003D) );
        if ( diagramElement1 != null )
        {
            try
            {
                DiagramElement diagramElement2 = diagramElement1.Clone( false );
                if ( storage != null )
                    diagramElement2.Load( storage );
                zvelQgjE.Element = diagramElement2;
            }
            catch ( Exception ex )
            {
                ex.LogError( ( string )null );
                this.\u0023\u003Dq1toum5UxELfT36mgz1mgQ\u002409qjrQ_VXaGpqP543nEQroTsEGTboXpYftZsocYzhQ( qb3uBzc7uKhDfxuXvtY.\u0023\u003DzmNVmtn8\u003D, zvelQgjE, storage, ex.Message );
            }
        }
        else
            this.\u0023\u003Dq1toum5UxELfT36mgz1mgQ\u002409qjrQ_VXaGpqP543nEQroTsEGTboXpYftZsocYzhQ( qb3uBzc7uKhDfxuXvtY.\u0023\u003DzmNVmtn8\u003D, zvelQgjE, storage, LocalizedStrings.Str3047Params.Put( new object[1]
            {
              (object) qb3uBzc7uKhDfxuXvtY.\u0023\u003DzmNVmtn8\u003D
            } ) );
    }
    else
    {
        SettingsStorage settingsStorage = _param1.GetValue<SettingsStorage>( nameof( -1260198023 ), ( SettingsStorage )null );
        // ISSUE: explicit non-virtual call
        CompositionDiagramElement composition = __nonvirtual( this._diagramElement.CreateComposition() );
        this._diagramElement.\u0023\u003DzxekB70g\u003D(composition, settingsStorage);
          zvelQgjE.Element = (DiagramElement) composition;
        }
        return zvelQgjE;
      }

      internal void \u0023\u003Dq1toum5UxELfT36mgz1mgQ\u002409qjrQ_VXaGpqP543nEQroTsEGTboXpYftZsocYzhQ(
        Guid _param1,
        TX _param2,
        SettingsStorage _param3,
        string _param4)
      {
        this._diagramElement._guidDictionary.SafeAdd<Guid, List<ValueTuple<TX, SettingsStorage>>>(_param1).Add(new ValueTuple<TX, SettingsStorage>(_param2, _param3));
        _param2.Element = (DiagramElement) null;
        _param2.Text = _param4;
      }

      internal void \u0023\u003Dz5EfMCmtYdFWOPnSBMg\u003D\u003D(
        CompositionModel<TX, TY> _param1)
      {
        _param1.Nodes = (IEnumerable<TX>) new ObservableCollection<TX>(((IEnumerable<SettingsStorage>) this.\u0023\u003DzvMnT7x4\u003D.GetValue<SettingsStorage[]>(nameof(-1260198076), (SettingsStorage[]) null)).Select<SettingsStorage, TX>(new Func<SettingsStorage, TX>(this.\u0023\u003DqVSAESgQajnjlquqJ9T5x8QbgC6JV3ZhTYQZLcbdMF89JEhI7TwJ3lsK8vzZ6\u0024bCy)));
        _param1.Links = (IEnumerable<TY>) new ObservableCollection<TY>(((IEnumerable<SettingsStorage>) this.\u0023\u003DzvMnT7x4\u003D.GetValue<SettingsStorage[]>(nameof(-1260198056), (SettingsStorage[]) null)).Select<SettingsStorage, TY>(new Func<SettingsStorage, TY>(CompositionRegistry<TX, TY>.\u0023\u003Dq9H57Q_kRTQ3DQ0AwfQHjWXg1g4wNikAr6CGKHaInf6NqG4JAYsbGdEYt9Emp1BSt)));
      }
    }

    private sealed class \u0023\u003DzFyODYqqh581ZOAIucslcCVQ\u003D
    {
      public TX \u0023\u003Dz6P4XQT0\u003D;

      internal bool \u0023\u003DzddZHr\u0024M0hacH_NOagg\u003D\u003D(
        ValueTuple<TX, SettingsStorage> _param1)
      {
        return (object) _param1.Item1 == (object) this.\u0023\u003Dz6P4XQT0\u003D;
      }
    }

    private sealed class \u0023\u003DzX_G0QB3uBzc7uKhDfxuXVtY\u003D
    {
      public Guid \u0023\u003DzmNVmtn8\u003D;

      internal bool \u0023\u003DzoETudYbWTZ5MdMRZ\u0024g\u003D\u003D(DiagramElement _param1)
      {
        return _param1.TypeId == this.\u0023\u003DzmNVmtn8\u003D;
      }
    }

    private sealed class \u0023\u003DzvJHhaci4iYfHtEdhfUcRlok\u003D
    {
      public CompositionRegistry<TX, TY> _diagramElement;
      public SchemeTypes \u0023\u003DzNRGauKM\u003D;

      internal SettingsStorage \u0023\u003DqO_G8XNjeN0F27B\u0024PQwMkoIk4xQSE30Ch2nb668BqxMtfxPq\u0024m1foI2umPTHhw89n(
        TX _param1,
        SchemeTypes _param2)
      {
        CompositionRegistry<TX, TY>.\u0023\u003DzFyODYqqh581ZOAIucslcCVQ\u003D yqqh581ZoaIucslcCvq = new CompositionRegistry<TX, TY>.\u0023\u003DzFyODYqqh581ZOAIucslcCVQ\u003D();
        yqqh581ZoaIucslcCvq.\u0023\u003Dz6P4XQT0\u003D = _param1;
        SettingsStorage settingsStorage = new SettingsStorage();
        settingsStorage.Set<string>(nameof(-1260198769), yqqh581ZoaIucslcCvq.\u0023\u003Dz6P4XQT0\u003D.Key).Set<float>(nameof(-1260198779), yqqh581ZoaIucslcCvq.\u0023\u003Dz6P4XQT0\u003D.Location.X).Set<float>(nameof(-1260198755), yqqh581ZoaIucslcCvq.\u0023\u003Dz6P4XQT0\u003D.Location.Y).Set<string>(nameof(-1260198763), yqqh581ZoaIucslcCvq.\u0023\u003Dz6P4XQT0\u003D.Figure);
        if (yqqh581ZoaIucslcCvq.\u0023\u003Dz6P4XQT0\u003D.Element != null)
        {
          CompositionDiagramElement element = yqqh581ZoaIucslcCvq.\u0023\u003Dz6P4XQT0\u003D.Element as CompositionDiagramElement;
          if (element == null || _param2 == SchemeTypes.Regular)
          {
            settingsStorage.SetValue<string>(nameof(-1260198449), yqqh581ZoaIucslcCvq.\u0023\u003Dz6P4XQT0\u003D.Element.TypeId.ToString());
            settingsStorage.SetValue<SettingsStorage>(nameof(-1260198042), yqqh581ZoaIucslcCvq.\u0023\u003Dz6P4XQT0\u003D.Element.Save());
          }
          else
            settingsStorage.SetValue<SettingsStorage>(nameof(-1260198023), this._diagramElement._settingStorage(element, _param2));
        }
        else
        {
          settingsStorage.SetValue<Guid>(nameof(-1260198449), yqqh581ZoaIucslcCvq.\u0023\u003Dz6P4XQT0\u003D.TypeId);
          List<ValueTuple<TX, SettingsStorage>> source;
          if (!this._diagramElement._guidDictionary.TryGetValue(yqqh581ZoaIucslcCvq.\u0023\u003Dz6P4XQT0\u003D.TypeId, out source))
            return settingsStorage;
          ValueTuple<TX, SettingsStorage>? nullable = source.Where<ValueTuple<TX, SettingsStorage>>(new Func<ValueTuple<TX, SettingsStorage>, bool>(yqqh581ZoaIucslcCvq.\u0023\u003DzddZHr\u0024M0hacH_NOagg\u003D\u003D)).FirstOr<ValueTuple<TX, SettingsStorage>>();
          if (nullable.HasValue)
            settingsStorage.SetValue<SettingsStorage>(nameof(-1260198042), nullable.Value.Item2);
        }
        return settingsStorage;
      }

      internal SettingsStorage \u0023\u003DzDPUPpH_hx8r1X9K0ug\u003D\u003D(
        TX _param1)
      {
        return this.\u0023\u003DqO_G8XNjeN0F27B\u0024PQwMkoIk4xQSE30Ch2nb668BqxMtfxPq\u0024m1foI2umPTHhw89n(_param1, this.\u0023\u003DzNRGauKM\u003D);
      }
    }
  }
}
