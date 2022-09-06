// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.BaseStrategyDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Algo.Indicators;
using StockSharp.Algo.Strategies;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace StockSharp.Diagram.Elements
{
  /// <summary>The element which is using strategy, based on S#.API.</summary>
  public abstract class BaseStrategyDiagramElement : DiagramElement
  {
    
    private readonly List<BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D> \u0023\u003Dzc6OpkNWccEuPD5_rXg\u003D\u003D = new List<BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D>();
    
    private readonly List<DiagramSocket> _outputSockets = new List<DiagramSocket>();
    
    private readonly DiagramElementParam<IPersistable> \u0023\u003DzMYGoOVKslW0H;
    
    private SettingsStorage \u0023\u003Dz7b2DOqc\u003D;
    
    private string \u0023\u003Dz_tTYAx4\u003D;
    
    private DiagramSocket \u0023\u003DzsE6yV\u0024mTuULj;
    
    private Type \u0023\u003DzXSYSKy_4RTSuHmf\u0024h5oISjE\u003D;
    
    private readonly DiagramElementParam<bool> _bDiagramElementParam;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.BaseStrategyDiagramElement" />.
    /// </summary>
    protected BaseStrategyDiagramElement()
    {
      this.\u0023\u003DzMYGoOVKslW0H = this.AddParam<IPersistable>(nameof(-1260196627), (IPersistable) null).SetDisplay<DiagramElementParam<IPersistable>>(LocalizedStrings.Strategy, LocalizedStrings.Str1507, LocalizedStrings.Str1507, 30).SetOnValueChangingHandler<IPersistable>(new Action<IPersistable, IPersistable>(this.\u0023\u003DzGlg_PudavRgju3sdKgGrsn8\u003D)).SetOnValueChangedHandler<IPersistable>(new Action<IPersistable>(this.\u0023\u003DzqjtxII8ZBHzdKnwgS6dqevw\u003D)).SetSaveLoadHandlers<IPersistable>(new Func<IPersistable, SettingsStorage>(this.\u0023\u003DzBWSa\u0024L\u0024SMj9x), new Func<SettingsStorage, IPersistable>(this.\u0023\u003DzStwB7iOFzcUQ)).SetExpandable<DiagramElementParam<IPersistable>>().SetNotifyOnChange<DiagramElementParam<IPersistable>>(false);
      this._bDiagramElementParam = this.AddParam<bool>(nameof(-1260196612), false).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Strategy, LocalizedStrings.ShowStrategySocket, LocalizedStrings.ShowStrategySocket, 50).SetOnValueChangedHandler<bool>(new Action<bool>(this.\u0023\u003Dzuq2o2VB_6LyxGymo9OEu6xE\u003D)).SetIsParam<DiagramElementParam<bool>>();
    }

    /// <summary>The renderer type for indicator extended drawing.</summary>
    public Type Painter
    {
      get
      {
        return this.\u0023\u003DzXSYSKy_4RTSuHmf\u0024h5oISjE\u003D;
      }
    }

    private void \u0023\u003DzFp_ie3g3zJD_ua7gSw\u003D\u003D(Type _param1)
    {
      this.\u0023\u003DzXSYSKy_4RTSuHmf\u0024h5oISjE\u003D = _param1;
    }

    /// <summary>The instance.</summary>
    protected IPersistable Instance
    {
      get
      {
        return this.\u0023\u003DzMYGoOVKslW0H.Value;
      }
      set
      {
        this.\u0023\u003DzMYGoOVKslW0H.Value = value;
        this.\u0023\u003Dz_tTYAx4\u003D = ((object) value)?.GetType().Name;
        this.\u0023\u003DzFp_ie3g3zJD_ua7gSw\u003D\u003D(value != null ? ((object) value).GetType().GetAttribute<IndicatorPainterAttribute>(true)?.Painter : (Type) null);
      }
    }

    /// <summary>Show strategy socket.</summary>
    public bool ShowStrategySocket
    {
      get
      {
        return this._bDiagramElementParam.Value;
      }
      set
      {
        this._bDiagramElementParam.Value = value;
      }
    }

    /// <inheritdoc />
    protected override void OnStart()
    {
      IPersistable instance = this.Instance;
      if (instance == null)
        throw new InvalidOperationException(LocalizedStrings.StrategyNotSelected);
      Strategy strategy = instance as Strategy;
      if (strategy == null)
        return;
      foreach (KeyValuePair<string, object> keyValuePair in (SynchronizedDictionary<string, object>) this.Strategy.Environment)
        strategy.Environment[keyValuePair.Key] = keyValuePair.Value;
      if (this.ShowStrategySocket && this.\u0023\u003DzsE6yV\u0024mTuULj != null)
        this.RaiseProcessOutput(this.\u0023\u003DzsE6yV\u0024mTuULj, this.Strategy.CurrentTime, (object) strategy, (DiagramSocketValue) null, (Subscription) null);
      this.Strategy.ChildStrategies.Add(strategy);
      base.OnStart();
    }

    /// <inheritdoc />
    protected override void OnStop()
    {
      base.OnStop();
      IPersistable instance = this.Instance;
      if (instance == null)
        return;
      Strategy strategy = instance as Strategy;
      if (strategy == null)
        return;
      this.Strategy.ChildStrategies.Remove(strategy);
      strategy.Parent = (ILogSource) null;
      strategy.Connector = (Connector) null;
      strategy.Portfolio = (Portfolio) null;
      strategy.Security = (Security) null;
    }

    /// <inheritdoc />
    protected override void OnReseted()
    {
      (this.Instance as Strategy)?.Reset();
      (this.Instance as IIndicator)?.Reset();
    }

    /// <summary>Remove external sockets.</summary>
    protected void RemoveExternalSockets()
    {
      this.\u0023\u003DzPPrIhYZMXxJbHCtyLg\u003D\u003D();
      this.Instance = (IPersistable) null;
    }

    /// <inheritdoc />
    public override void Load(SettingsStorage storage)
    {
      base.Load(storage);
      this.\u0023\u003Dz_tTYAx4\u003D = storage.GetValue<string>(nameof(-1260196667), (string) null);
      this.\u0023\u003Dzz79juRUUEXV\u0024();
    }

    /// <inheritdoc />
    public override void Save(SettingsStorage storage)
    {
      base.Save(storage);
      storage.SetValue<string>(nameof(-1260196667), this.\u0023\u003Dz_tTYAx4\u003D);
    }

    private SettingsStorage \u0023\u003DzBWSa\u0024L\u0024SMj9x(IPersistable _param1)
    {
      if (_param1 == null)
        return this.\u0023\u003Dz7b2DOqc\u003D ?? new SettingsStorage();
      return _param1.Save();
    }

    private IPersistable \u0023\u003DzStwB7iOFzcUQ(SettingsStorage _param1)
    {
      this.\u0023\u003Dz7b2DOqc\u003D = _param1;
      IPersistable instance = this.Instance;
      if (instance == null)
        return instance;
      instance.Load(_param1);
      return instance;
    }

    private static string \u0023\u003Dz9\u00247PPVtv304n(MethodInfo _param0)
    {
      if (_param0 == (MethodInfo) null)
        throw new ArgumentNullException(nameof(-1260196692));
      return string.Concat(_param0.Name, nameof(-1260196759), ((IEnumerable<ParameterInfo>) _param0.GetParameters()).Select<ParameterInfo, string>(BaseStrategyDiagramElement.LamdaShit.\u0023\u003Dzw_lAJuljRaKBPhiOSw\u003D\u003D ?? (BaseStrategyDiagramElement.LamdaShit.\u0023\u003Dzw_lAJuljRaKBPhiOSw\u003D\u003D = new Func<ParameterInfo, string>(BaseStrategyDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzJIf5jvftVxDkQz25MieTGls\u003D))).JoinCommaSpace(), nameof(-1260198591));
    }

    private static void \u0023\u003Dz0LnvuKwG77Q7UkuAQmNk6aw\u003D(Type _param0)
    {
      foreach (EventInfo provider in _param0.GetEvents(BindingFlags.Instance | BindingFlags.Public))
      {
        if (provider.GetAttribute<DiagramExternalAttribute>(true) != null)
        {
          MethodInfo method = provider.EventHandlerType.GetMethod(nameof(-1260196701));
          if (method.GetParameters().Length != 1)
            throw new InvalidOperationException(LocalizedStrings.ExternalSocketOneParam.Put((object[]) new object[1]{ (object) provider.Name }));
          if (method.ReturnType != typeof (void))
            throw new InvalidOperationException(LocalizedStrings.ExternalSocketReturnType.Put((object[]) new object[1]{ (object) provider.Name }));
        }
      }
    }

    private void \u0023\u003DzWiYDem_SPAZZq7tYWw\u003D\u003D(object _param1)
    {
      Type type = ((object) _param1).GetType();
      if (_param1 is IIndicator)
      {
        DiagramSocket diagramSocket = this.\u0023\u003DzO\u0024W1priG7AAc();
        this.\u0023\u003DzEg00vJgQe3Xs(_param1, type, diagramSocket);
      }
      else
      {
        this.\u0023\u003Dzgu78PNxCY1zur6T6zQ\u003D\u003D(_param1, type);
        this.\u0023\u003Dz95u0cB0INg90kNd_ZA\u003D\u003D(_param1, type);
      }
      this.\u0023\u003Dzz79juRUUEXV\u0024();
    }

    private DiagramSocket \u0023\u003DzO\u0024W1priG7AAc()
    {
      DiagramSocket[] diagramSocketArray = this._outputSockets.CopyAndClear<DiagramSocket>();
      bool isNew;
      DiagramSocket orAddOutput = this.GetOrAddOutput(DiagramElement.GenerateSocketId(nameof(-1260196684)), nameof(-1260196684), DiagramSocketType.IndicatorValue, out isNew, int.MaxValue, int.MaxValue, new bool?());
      this.RaiseSocketChanged(orAddOutput);
      this._outputSockets.Add(orAddOutput);
      foreach (DiagramSocket socket in diagramSocketArray)
      {
        if (socket != orAddOutput)
          this.RemoveSocket(socket);
      }
      return orAddOutput;
    }

    private Action<IIndicatorValue> \u0023\u003DzuojmSds0HL2UbCWnKA\u003D\u003D(
      DiagramSocket _param1)
    {
      Action<DiagramSocket, object> action = new Action<DiagramSocket, object>(((DiagramElement) this).RaiseProcessOutput);
      Type type = ((object) action).GetType();
      ParameterExpression parameterExpression1 = Expression.Parameter(typeof (DiagramSocket), nameof(-1260196730));
      ParameterExpression parameterExpression2 = Expression.Parameter(typeof (IIndicatorValue), nameof(-1260196709));
      UnaryExpression unaryExpression = Expression.Convert((Expression) parameterExpression2, typeof (object));
      MethodCallExpression methodCallExpression = Expression.Call((Expression) Expression.Constant((object) action), type.GetMethod(nameof(-1260196701)), (Expression) parameterExpression1, (Expression) unaryExpression);
      return (Action<IIndicatorValue>) Expression.Lambda(new BaseStrategyDiagramElement.\u0023\u003DzCSlYaVtSalSr(_param1).Visit((Expression) methodCallExpression), parameterExpression2).Compile();
    }

    private void \u0023\u003DzEg00vJgQe3Xs(object _param1, Type _param2, DiagramSocket _param3)
    {
      BaseStrategyDiagramElement.SomeNodeFunctions325 nuxviCy5rFsoeWgcIi = new BaseStrategyDiagramElement.SomeNodeFunctions325();
      BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D[] zUy4FqScArray = this.\u0023\u003Dzc6OpkNWccEuPD5_rXg\u003D\u003D.CopyAndClear<BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D>();
      MethodInfo method = _param2.GetMethod(nameof(-1260196684));
      nuxviCy5rFsoeWgcIi.\u0023\u003DzociBWNI\u003D = BaseStrategyDiagramElement.\u0023\u003Dz9\u00247PPVtv304n(method);
      BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D zUy4FqSc1 = ((IEnumerable<BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D>) zUy4FqScArray).FirstOrDefault<BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D>(new Func<BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D, bool>(nuxviCy5rFsoeWgcIi.\u0023\u003DzVJkYI4\u0024xQLxcEDT_QwyqMsk\u003D)) ?? new BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D();
      zUy4FqSc1.\u0023\u003DzEYUYonQ\u003D(_param1);
      zUy4FqSc1.\u0023\u003Dz6OOtFfw\u003D(method);
      zUy4FqSc1.\u0023\u003DzsGHV3nXNjXzY(this.\u0023\u003DzuojmSds0HL2UbCWnKA\u003D\u003D(_param3));
      DiagramSocket[] diagramSocketArray = zUy4FqSc1.\u0023\u003DzQYBOI\u0024JRTtfVACob4Q\u003D\u003D().CopyAndClear<DiagramSocket>();
      bool isNew;
      DiagramSocket orAddInput = this.GetOrAddInput(DiagramElement.GenerateSocketId(nuxviCy5rFsoeWgcIi.\u0023\u003DzociBWNI\u003D), method.Name, DiagramSocketType.Any, out isNew, new Action<DiagramSocketValue>(zUy4FqSc1.\u0023\u003DzsriRuCQ\u003D), 1, int.MaxValue, false, new bool?());
      this.RaiseSocketChanged(orAddInput);
      zUy4FqSc1.\u0023\u003DzQYBOI\u0024JRTtfVACob4Q\u003D\u003D().Add(orAddInput);
      List<DiagramSocket> diagramSocketList = zUy4FqSc1.\u0023\u003DzQYBOI\u0024JRTtfVACob4Q\u003D\u003D();
      foreach (DiagramSocket socket in ((IEnumerable<DiagramSocket>) diagramSocketArray).Except<DiagramSocket>((IEnumerable<DiagramSocket>) diagramSocketList))
        this.RemoveSocket(socket);
      this.\u0023\u003Dzc6OpkNWccEuPD5_rXg\u003D\u003D.Add(zUy4FqSc1);
      foreach (BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D zUy4FqSc2 in zUy4FqScArray)
      {
        if (zUy4FqSc2 != zUy4FqSc1)
        {
          foreach (DiagramSocket socket in zUy4FqSc2.\u0023\u003DzQYBOI\u0024JRTtfVACob4Q\u003D\u003D())
            this.RemoveSocket(socket);
        }
      }
    }

    private void \u0023\u003Dzgu78PNxCY1zur6T6zQ\u003D\u003D(object _param1, Type _param2)
    {
      IEnumerable<MethodInfo> methodInfos = ((IEnumerable<MethodInfo>) _param2.GetMethods(BindingFlags.Instance | BindingFlags.Public)).Where<MethodInfo>(BaseStrategyDiagramElement.LamdaShit.\u0023\u003DzycPsaMV6yBucpQjp6g\u003D\u003D ?? (BaseStrategyDiagramElement.LamdaShit.\u0023\u003DzycPsaMV6yBucpQjp6g\u003D\u003D = new Func<MethodInfo, bool>(BaseStrategyDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzUG75p2SinSF5A5RbZbPz_kKkE_LT)));
      BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D[] zUy4FqScArray = this.\u0023\u003Dzc6OpkNWccEuPD5_rXg\u003D\u003D.CopyAndClear<BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D>();
      foreach (MethodInfo methodInfo in methodInfos)
      {
        BaseStrategyDiagramElement.\u0023\u003DzsNI5MbiT0jY3DBYV4D1mqRI\u003D t0jY3DbyV4D1mqRi = new BaseStrategyDiagramElement.\u0023\u003DzsNI5MbiT0jY3DBYV4D1mqRI\u003D();
        t0jY3DbyV4D1mqRi.\u0023\u003DzociBWNI\u003D = BaseStrategyDiagramElement.\u0023\u003Dz9\u00247PPVtv304n(methodInfo);
        BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D zUy4FqSc = ((IEnumerable<BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D>) zUy4FqScArray).FirstOrDefault<BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D>(new Func<BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D, bool>(t0jY3DbyV4D1mqRi.\u0023\u003DzD3KAwzOX27NW9G5f1rD_y1v2YYc\u0024)) ?? new BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D();
        zUy4FqSc.\u0023\u003DzEYUYonQ\u003D(_param1);
        zUy4FqSc.\u0023\u003Dz6OOtFfw\u003D(methodInfo);
        ParameterInfo[] parameters = methodInfo.GetParameters();
        DiagramSocket[] diagramSocketArray = zUy4FqSc.\u0023\u003DzQYBOI\u0024JRTtfVACob4Q\u003D\u003D().CopyAndClear<DiagramSocket>();
        foreach (ParameterInfo parameterInfo in parameters)
        {
          DiagramSocketType socketType = DiagramSocketType.GetSocketType(parameterInfo.ParameterType);
          bool isNew;
          DiagramSocket orAddInput = this.GetOrAddInput(DiagramElement.GenerateSocketId(string.Concat(t0jY3DbyV4D1mqRi.\u0023\u003DzociBWNI\u003D, nameof(-1260198860), parameterInfo.Name)), string.Concat(methodInfo.Name, nameof(-1260198860), parameterInfo.Name), socketType, out isNew, new Action<DiagramSocketValue>(zUy4FqSc.\u0023\u003Dz5fAD0Vw\u003D), 1, int.MaxValue, false, new bool?());
          this.RaiseSocketChanged(orAddInput);
          zUy4FqSc.\u0023\u003DzQYBOI\u0024JRTtfVACob4Q\u003D\u003D().Add(orAddInput);
        }
        foreach (DiagramSocket socket in ((IEnumerable<DiagramSocket>) diagramSocketArray).Except<DiagramSocket>((IEnumerable<DiagramSocket>) zUy4FqSc.\u0023\u003DzQYBOI\u0024JRTtfVACob4Q\u003D\u003D()))
          this.RemoveSocket(socket);
        this.\u0023\u003Dzc6OpkNWccEuPD5_rXg\u003D\u003D.Add(zUy4FqSc);
      }
      foreach (DiagramSocket socket in ((IEnumerable<BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D>) zUy4FqScArray).Except<BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D>((IEnumerable<BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D>) this.\u0023\u003Dzc6OpkNWccEuPD5_rXg\u003D\u003D).SelectMany<BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D, DiagramSocket>(BaseStrategyDiagramElement.LamdaShit.\u0023\u003DzBMTDhUJo5n6RjKufAw\u003D\u003D ?? (BaseStrategyDiagramElement.LamdaShit.\u0023\u003DzBMTDhUJo5n6RjKufAw\u003D\u003D = new Func<BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D, IEnumerable<DiagramSocket>>(BaseStrategyDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzlaP0sncDZ6PWtiK4F5rJXm8dBG8n))))
        this.RemoveSocket(socket);
    }

    private void \u0023\u003Dz95u0cB0INg90kNd_ZA\u003D\u003D(object _param1, Type _param2)
    {
      EventInfo[] events = _param2.GetEvents(BindingFlags.Instance | BindingFlags.Public);
      DiagramSocket[] diagramSocketArray = this._outputSockets.CopyAndClear<DiagramSocket>();
      foreach (EventInfo provider in events)
      {
        if (provider.GetAttribute<DiagramExternalAttribute>(true) != null)
        {
          ParameterInfo parameterInfo = ((IEnumerable<ParameterInfo>) provider.EventHandlerType.GetMethod(nameof(-1260196701)).GetParameters()).Single<ParameterInfo>();
          DiagramSocketType socketType = DiagramSocketType.GetSocketType(parameterInfo.ParameterType);
          bool isNew;
          DiagramSocket orAddOutput = this.GetOrAddOutput(DiagramElement.GenerateSocketId(provider.Name), provider.Name, socketType, out isNew, int.MaxValue, int.MaxValue, new bool?());
          Action<DiagramSocket, object> action = new Action<DiagramSocket, object>(((DiagramElement) this).RaiseProcessOutput);
          Type type = ((object) action).GetType();
          ParameterExpression parameterExpression1 = Expression.Parameter(typeof (DiagramSocket), nameof(-1260196730));
          ParameterExpression parameterExpression2 = Expression.Parameter(parameterInfo.ParameterType, parameterInfo.Name);
          UnaryExpression unaryExpression = Expression.Convert((Expression) parameterExpression2, typeof (object));
          MethodCallExpression methodCallExpression = Expression.Call((Expression) Expression.Constant((object) action), type.GetMethod(nameof(-1260196701)), (Expression) parameterExpression1, (Expression) unaryExpression);
          Delegate @delegate = Expression.Lambda(new BaseStrategyDiagramElement.\u0023\u003DzCSlYaVtSalSr(orAddOutput).Visit((Expression) methodCallExpression), parameterExpression2).Compile();
          Delegate handler = Delegate.CreateDelegate(provider.EventHandlerType, (object) @delegate, nameof(-1260196701), false);
          provider.AddEventHandler(_param1, handler);
          this.RaiseSocketChanged(orAddOutput);
          this._outputSockets.Add(orAddOutput);
        }
      }
      foreach (DiagramSocket socket in ((IEnumerable<DiagramSocket>) diagramSocketArray).Except<DiagramSocket>((IEnumerable<DiagramSocket>) this._outputSockets))
        this.RemoveSocket(socket);
    }

    private void \u0023\u003DzPPrIhYZMXxJbHCtyLg\u003D\u003D()
    {
      this.\u0023\u003Dzc6OpkNWccEuPD5_rXg\u003D\u003D.Clear();
      this._outputSockets.Clear();
      foreach (DiagramSocket socket in this.InputSockets.ToArray<DiagramSocket>())
        this.RemoveSocket(socket);
      foreach (DiagramSocket socket in this.OutputSockets.ToArray<DiagramSocket>())
      {
        if (socket != this.\u0023\u003DzsE6yV\u0024mTuULj)
          this.RemoveSocket(socket);
      }
    }

    private void \u0023\u003DzGEm\u0024NjuLL8zF()
    {
      this.RaiseParameterValueChanged(nameof(-1260196627));
    }

    /// <summary>Is type compatible.</summary>
    /// <param name="type">Type.</param>
    /// <returns>Check result.</returns>
    public static bool IsTypeCompatible(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof(-1260195996));
      if (type.IsAbstract || !type.IsPublic)
        return false;
      if (!type.IsSubclassOf(typeof (Strategy)))
        return typeof (IIndicator).IsAssignableFrom(type);
      return true;
    }

    private void \u0023\u003Dzz79juRUUEXV\u0024()
    {
      BaseStrategyDiagramElement.\u0023\u003DzAE3e7v95H2dbvOtPWQAtdw0\u003D ae3e7v95H2dbvOtPwqAtdw0 = new BaseStrategyDiagramElement.\u0023\u003DzAE3e7v95H2dbvOtPWQAtdw0\u003D();
      ae3e7v95H2dbvOtPwqAtdw0.\u0023\u003DzUZnu2cE\u003D = DiagramElement.GenerateSocketId(nameof(-1260195973));
      bool flag1 = this._bDiagramElementParam.Value;
      bool flag2 = this.OutputSockets.FirstOrDefault<DiagramSocket>(new Func<DiagramSocket, bool>(ae3e7v95H2dbvOtPwqAtdw0.\u0023\u003Dz_qq\u0024_PO0jEZ2HGsBp2tph1M\u003D)) != null && this.\u0023\u003DzsE6yV\u0024mTuULj != null;
      if (flag1 == flag2)
        return;
      if (this.\u0023\u003DzsE6yV\u0024mTuULj != null)
        this.RemoveSocket(this.\u0023\u003DzsE6yV\u0024mTuULj);
      bool isNew;
      this.\u0023\u003DzsE6yV\u0024mTuULj = !flag1 ? (DiagramSocket) null : this.GetOrAddOutput(ae3e7v95H2dbvOtPwqAtdw0.\u0023\u003DzUZnu2cE\u003D, LocalizedStrings.Strategy, DiagramSocketType.Strategy, out isNew, int.MaxValue, 0, new bool?());
    }

    private void \u0023\u003DzGlg_PudavRgju3sdKgGrsn8\u003D(
      IPersistable _param1,
      IPersistable _param2)
    {
      if (_param2 != null)
        BaseStrategyDiagramElement.\u0023\u003Dz0LnvuKwG77Q7UkuAQmNk6aw\u003D(((object) _param2).GetType());
      if (_param1 != null)
      {
        Strategy strategy = _param1 as Strategy;
        if (strategy == null)
        {
          IIndicator indicator = _param1 as IIndicator;
          if (indicator != null)
            indicator.Reseted -= new Action(this.\u0023\u003DzGEm\u0024NjuLL8zF);
        }
        else
          strategy.ParametersChanged -= new Action(this.\u0023\u003DzGEm\u0024NjuLL8zF);
        this.\u0023\u003Dz7b2DOqc\u003D = _param1.Save();
      }
      if (_param2 == null)
        return;
      Strategy strategy1 = _param2 as Strategy;
      if (strategy1 == null)
      {
        IIndicator indicator = _param2 as IIndicator;
        if (indicator != null)
          indicator.Reseted += new Action(this.\u0023\u003DzGEm\u0024NjuLL8zF);
      }
      else
        strategy1.ParametersChanged += new Action(this.\u0023\u003DzGEm\u0024NjuLL8zF);
      string name = ((object) _param2).GetType().Name;
      if (this.\u0023\u003Dz_tTYAx4\u003D != null && !this.\u0023\u003Dz_tTYAx4\u003D.EqualsIgnoreCase(name))
        this.\u0023\u003Dz7b2DOqc\u003D = (SettingsStorage) null;
      if (this.\u0023\u003Dz7b2DOqc\u003D == null)
        return;
      if (this.\u0023\u003Dz7b2DOqc\u003D.Count <= 0)
        return;
      try
      {
        _param2.Load(this.\u0023\u003Dz7b2DOqc\u003D);
      }
      catch (object ex)
      {
      }
    }

    private void \u0023\u003DzqjtxII8ZBHzdKnwgS6dqevw\u003D(IPersistable _param1)
    {
      DiagramElement.Dispatcher.InvokeAsync(new Action(new BaseStrategyDiagramElement.\u0023\u003DzmBcIfU2D1f2SdVx7CrKd0oc\u003D()
      {
        _diagramElement = this,
        _dsv = _param1
      }.\u0023\u003DzSkum_pT9U\u0024UTPlBUXA\u003D\u003D));
    }

    private void \u0023\u003Dzuq2o2VB_6LyxGymo9OEu6xE\u003D(bool _param1)
    {
      this.\u0023\u003Dzz79juRUUEXV\u0024();
    }

    private sealed class SomeNodeFunctions325
    {
      public string \u0023\u003DzociBWNI\u003D;

      internal bool \u0023\u003DzVJkYI4\u0024xQLxcEDT_QwyqMsk\u003D(
        BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D _param1)
      {
        return BaseStrategyDiagramElement.\u0023\u003Dz9\u00247PPVtv304n(_param1.\u0023\u003DzTvBAq\u0024c\u003D()) == this.\u0023\u003DzociBWNI\u003D;
      }
    }

    private sealed class \u0023\u003DzAE3e7v95H2dbvOtPWQAtdw0\u003D
    {
      public string \u0023\u003DzUZnu2cE\u003D;

      internal bool \u0023\u003Dz_qq\u0024_PO0jEZ2HGsBp2tph1M\u003D(DiagramSocket _param1)
      {
        return _param1.Id.EqualsIgnoreCase(this.\u0023\u003DzUZnu2cE\u003D);
      }
    }

    private sealed class \u0023\u003DzCSlYaVtSalSr : ExpressionVisitor
    {
      
      private readonly DiagramSocket _scoket;

      public \u0023\u003DzCSlYaVtSalSr(DiagramSocket _param1)
      {
        this._scoket = _param1;
      }

      protected override Expression VisitParameter(ParameterExpression _param1)
      {
        if (!(_param1.Type == typeof (DiagramSocket)))
          return base.VisitParameter(_param1);
        return (Expression) Expression.Constant((object) this._scoket);
      }
    }

    [Serializable]
    private sealed class LamdaShit
    {
      public static readonly BaseStrategyDiagramElement.LamdaShit _lamdaShit = new BaseStrategyDiagramElement.LamdaShit();
      public static Func<ParameterInfo, string> \u0023\u003Dzw_lAJuljRaKBPhiOSw\u003D\u003D;
      public static Func<MethodInfo, bool> \u0023\u003DzycPsaMV6yBucpQjp6g\u003D\u003D;
      public static Func<BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D, IEnumerable<DiagramSocket>> \u0023\u003DzBMTDhUJo5n6RjKufAw\u003D\u003D;

      internal string \u0023\u003DzJIf5jvftVxDkQz25MieTGls\u003D(ParameterInfo _param1)
      {
        return _param1.ParameterType.Name;
      }

      internal bool \u0023\u003DzUG75p2SinSF5A5RbZbPz_kKkE_LT(MethodInfo _param1)
      {
        return _param1.GetAttribute<DiagramExternalAttribute>(true) != null;
      }

      internal IEnumerable<DiagramSocket> \u0023\u003DzlaP0sncDZ6PWtiK4F5rJXm8dBG8n(
        BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D _param1)
      {
        return (IEnumerable<DiagramSocket>) _param1.\u0023\u003DzQYBOI\u0024JRTtfVACob4Q\u003D\u003D();
      }
    }

    private sealed class \u0023\u003DzUY4FQSc\u003D
    {
      private readonly Dictionary<DiagramSocket, object> \u0023\u003DzT2JKe5w\u003D = new Dictionary<DiagramSocket, object>();
      private readonly List<DiagramSocket> \u0023\u003DztfCzYrGgRlZFKdxB2OMFLD0\u003D = new List<DiagramSocket>();
      private object \u0023\u003DzPtgu1zCOj9_xdJNeWg\u003D\u003D;
      private MethodInfo \u0023\u003DzSNVsqDzMEjm_n2_iVw\u003D\u003D;
      private Action<IIndicatorValue> \u0023\u003Dz1TiF08IS2ExZz3B3\u0024A\u003D\u003D;

      public object \u0023\u003DzNVglE14\u003D()
      {
        return this.\u0023\u003DzPtgu1zCOj9_xdJNeWg\u003D\u003D;
      }

      public void \u0023\u003DzEYUYonQ\u003D(object _param1)
      {
        this.\u0023\u003DzPtgu1zCOj9_xdJNeWg\u003D\u003D = _param1;
      }

      public MethodInfo \u0023\u003DzTvBAq\u0024c\u003D()
      {
        return this.\u0023\u003DzSNVsqDzMEjm_n2_iVw\u003D\u003D;
      }

      public void \u0023\u003Dz6OOtFfw\u003D(MethodInfo _param1)
      {
        this.\u0023\u003DzSNVsqDzMEjm_n2_iVw\u003D\u003D = _param1;
      }

      public Action<IIndicatorValue> \u0023\u003Dzz5IvSeeUq_vw()
      {
        return this.\u0023\u003Dz1TiF08IS2ExZz3B3\u0024A\u003D\u003D;
      }

      public void \u0023\u003DzsGHV3nXNjXzY(Action<IIndicatorValue> _param1)
      {
        this.\u0023\u003Dz1TiF08IS2ExZz3B3\u0024A\u003D\u003D = _param1;
      }

      public List<DiagramSocket> \u0023\u003DzQYBOI\u0024JRTtfVACob4Q\u003D\u003D()
      {
        return this.\u0023\u003DztfCzYrGgRlZFKdxB2OMFLD0\u003D;
      }

      public void \u0023\u003Dz5fAD0Vw\u003D(DiagramSocketValue _param1)
      {
        if (_param1 == null)
          throw new ArgumentNullException(nameof(-1260196861));
        this.\u0023\u003Dz5fAD0Vw\u003D(_param1, _param1.Value);
      }

      public void \u0023\u003DzsriRuCQ\u003D(DiagramSocketValue _param1)
      {
        if (_param1 == null)
          throw new ArgumentNullException(nameof(-1260196842));
        object obj = this.\u0023\u003Dz5fAD0Vw\u003D(_param1, (object) ((IIndicator) this.\u0023\u003DzNVglE14\u003D()).ConvertToIIndicatorValue(_param1.Value));
        if (obj == null)
          return;
        Action<IIndicatorValue> action = this.\u0023\u003Dzz5IvSeeUq_vw();
        if (action == null)
          return;
        action((IIndicatorValue) obj);
      }

      private object \u0023\u003Dz5fAD0Vw\u003D(DiagramSocketValue _param1, object _param2)
      {
        this.\u0023\u003DzT2JKe5w\u003D[_param1.Socket] = _param2;
        if (this.\u0023\u003DzT2JKe5w\u003D.Count != this.\u0023\u003DzQYBOI\u0024JRTtfVACob4Q\u003D\u003D().Count)
          return (object) null;
        object[] array = this.\u0023\u003DzQYBOI\u0024JRTtfVACob4Q\u003D\u003D().Select<DiagramSocket, object>(new Func<DiagramSocket, object>(this.\u0023\u003DzDGnSvontCJy2JmGF6Q\u003D\u003D)).ToArray<object>();
        this.\u0023\u003DzT2JKe5w\u003D.Clear();
        return this.\u0023\u003DzTvBAq\u0024c\u003D().Invoke(this.\u0023\u003DzNVglE14\u003D(), array);
      }

      private object \u0023\u003DzDGnSvontCJy2JmGF6Q\u003D\u003D(DiagramSocket _param1)
      {
        return this.\u0023\u003DzT2JKe5w\u003D[_param1];
      }
    }

    private sealed class \u0023\u003DzmBcIfU2D1f2SdVx7CrKd0oc\u003D
    {
      public IPersistable _dsv;
      public BaseStrategyDiagramElement _diagramElement;

      internal void \u0023\u003DzSkum_pT9U\u0024UTPlBUXA\u003D\u003D()
      {
        if (this._dsv != null)
          this._diagramElement.\u0023\u003DzWiYDem_SPAZZq7tYWw\u003D\u003D((object) this._dsv);
        else
          this._diagramElement.\u0023\u003DzPPrIhYZMXxJbHCtyLg\u003D\u003D();
        this._diagramElement.RaisePropertiesChanged();
      }
    }

    private sealed class \u0023\u003DzsNI5MbiT0jY3DBYV4D1mqRI\u003D
    {
      public string \u0023\u003DzociBWNI\u003D;

      internal bool \u0023\u003DzD3KAwzOX27NW9G5f1rD_y1v2YYc\u0024(
        BaseStrategyDiagramElement.\u0023\u003DzUY4FQSc\u003D _param1)
      {
        return BaseStrategyDiagramElement.\u0023\u003Dz9\u00247PPVtv304n(_param1.\u0023\u003DzTvBAq\u0024c\u003D()) == this.\u0023\u003DzociBWNI\u003D;
      }
    }
  }
}
