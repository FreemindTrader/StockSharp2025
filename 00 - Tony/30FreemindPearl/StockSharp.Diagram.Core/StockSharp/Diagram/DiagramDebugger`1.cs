// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.DiagramDebugger`1
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace StockSharp.Diagram
{
  /// <summary>The debugger of the diagram composite element.</summary>
  public class DiagramDebugger<TElement> : IPersistable where TElement : CompositionDiagramElement
  {
    
    private readonly CachedSynchronizedList<DiagramSocketBreakpoint> _diagramSocketBreakpoints = new CachedSynchronizedList<DiagramSocketBreakpoint>();
    
    private readonly SynchronizedDictionary<DebuggerSyncObject, Tuple<TElement, TElement>> \u0023\u003DzZlomYSWzWRzRs04YBCruVV4\u003D = new SynchronizedDictionary<DebuggerSyncObject, Tuple<TElement, TElement>>();
    
    private readonly Func<TElement, Func<DiagramSocket, bool, bool>, Action<DebuggerSyncObject>, Action<DebuggerSyncObject>, DebuggerSyncObject> \u0023\u003DzrUNr3JqaPkgL;
    
    private DebuggerSyncObject _debuggerSyncObject;
    
    private TElement _composition;
    
    private bool \u0023\u003DzYCc1ghHz6zKmZC9Nsg\u003D\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.DiagramDebugger`1" />.
    /// </summary>
    /// <param name="composition">Composite element.</param>
    /// <param name="createSyncObject">Creator.</param>
    public DiagramDebugger(
      TElement composition,
      Func<TElement, Func<DiagramSocket, bool, bool>, Action<DebuggerSyncObject>, Action<DebuggerSyncObject>, DebuggerSyncObject> createSyncObject)
    {
      TElement element = composition;
      if ((object) element == null)
        throw new ArgumentNullException(nameof(-1260199567));
      this.\u0023\u003Dz1TA2suPZ1W5k(element);
      this.Composition.ElementAdded += new Action<DiagramElement>(this.\u0023\u003DzJx1hPj9Sdgka);
      this.Composition.ElementRemoved += new Action<DiagramElement>(this.\u0023\u003Dzy3K0Nyn3I4Pr);
      Func<TElement, Func<DiagramSocket, bool, bool>, Action<DebuggerSyncObject>, Action<DebuggerSyncObject>, DebuggerSyncObject> func = createSyncObject;
      if (func == null)
        throw new ArgumentNullException(nameof(-1260199612));
      this.\u0023\u003DzrUNr3JqaPkgL = func;
      this.Composition.DebuggerSyncObject = this._debuggerSyncObject = this.\u0023\u003DzAu3kmCBue5qh(this.Composition, default (TElement));
    }

    /// <summary>
    /// Breakpoints (sockets, on which the data transmission will be stopped).
    /// </summary>
    public IEnumerable<DiagramSocketBreakpoint> Breakpoints
    {
      get
      {
        return (IEnumerable<DiagramSocketBreakpoint>) this._diagramSocketBreakpoints.Cache;
      }
    }

    /// <summary>
    /// <see langword="true" />, if the debugger is stopped at the entry of the diagram element. Otherwise, <see langword="false" />.
    ///     </summary>
    public bool IsWaitingOnInput
    {
      get
      {
        if (!this._debuggerSyncObject.IsWaitingOnInput)
          return this.\u0023\u003DzZlomYSWzWRzRs04YBCruVV4\u003D.Any<KeyValuePair<DebuggerSyncObject, Tuple<TElement, TElement>>>(DiagramDebugger<TElement>.LamdaShit.\u0023\u003DzMv6PO2HThUrilC6VMQ\u003D\u003D ?? (DiagramDebugger<TElement>.LamdaShit.\u0023\u003DzMv6PO2HThUrilC6VMQ\u003D\u003D = new Func<KeyValuePair<DebuggerSyncObject, Tuple<TElement, TElement>>, bool>(DiagramDebugger<TElement>.LamdaShit._lamdaShit.\u0023\u003DzJrRX\u00244vIpDvUP0qIDsxMgnEx80Pd)));
        return true;
      }
    }

    /// <summary>
    /// <see langword="true" />, if the debugger is stopped at the exit of the diagram element. Otherwise, <see langword="false" />.
    ///     </summary>
    public bool IsWaitingOnOutput
    {
      get
      {
        if (!this._debuggerSyncObject.IsWaitingOnOutput)
          return this.\u0023\u003DzZlomYSWzWRzRs04YBCruVV4\u003D.Any<KeyValuePair<DebuggerSyncObject, Tuple<TElement, TElement>>>(DiagramDebugger<TElement>.LamdaShit.Func00023 ?? (DiagramDebugger<TElement>.LamdaShit.Func00023 = new Func<KeyValuePair<DebuggerSyncObject, Tuple<TElement, TElement>>, bool>(DiagramDebugger<TElement>.LamdaShit._lamdaShit.\u0023\u003DzUyflhixacGq3Tm8eL2lXT7X3im0x)));
        return true;
      }
    }

    /// <summary>
    /// <see langword="true" />, if the debugger is stopped at the entry or exit of the diagram element. Otherwise, <see langword="false" />.
    ///     </summary>
    public bool IsWaiting
    {
      get
      {
        if (!this.IsWaitingOnInput)
          return this.IsWaitingOnOutput;
        return true;
      }
    }

    /// <summary>
    /// <see langword="true" />, if it is possible to go inside of the current diagram element. Otherwise, <see langword="false" />.
    ///     </summary>
    public bool CanStepInto
    {
      get
      {
        return this._debuggerSyncObject.CurrentElement is TElement;
      }
    }

    /// <summary>
    /// <see langword="true" />, if it is possible to go outside from the current diagram element. Otherwise, <see langword="false" />.
    ///     </summary>
    public bool CanStepOut
    {
      get
      {
        return (object) this.\u0023\u003DzZlomYSWzWRzRs04YBCruVV4\u003D[this._debuggerSyncObject].Item2 != null;
      }
    }

    /// <summary>
    /// <see langword="true" />, if the debugger is stopped at the error. Otherwise, <see langword="false" />.
    ///     </summary>
    public bool IsWaitingOnError
    {
      get
      {
        if (this._debuggerSyncObject.CurrentError == null)
          return this.\u0023\u003DzZlomYSWzWRzRs04YBCruVV4\u003D.Any<KeyValuePair<DebuggerSyncObject, Tuple<TElement, TElement>>>(DiagramDebugger<TElement>.LamdaShit.\u0023\u003DzFkECh6zOfwaha4Eg6w\u003D\u003D ?? (DiagramDebugger<TElement>.LamdaShit.\u0023\u003DzFkECh6zOfwaha4Eg6w\u003D\u003D = new Func<KeyValuePair<DebuggerSyncObject, Tuple<TElement, TElement>>, bool>(DiagramDebugger<TElement>.LamdaShit._lamdaShit.\u0023\u003DzP1MlxVYLmPWqnyXZuBni\u0024TlMi_27)));
        return true;
      }
    }

    /// <summary>
    /// <see langword="true" />, if the debugger is used. Otherwise, <see langword="false" />.
    ///     </summary>
    public bool IsEnabled
    {
      get
      {
        return this.\u0023\u003DzYCc1ghHz6zKmZC9Nsg\u003D\u003D;
      }
      set
      {
        this.\u0023\u003DzYCc1ghHz6zKmZC9Nsg\u003D\u003D = value;
      }
    }

    /// <summary>Composite element.</summary>
    public TElement Composition
    {
      get
      {
        return this._composition;
      }
    }

    private void \u0023\u003Dz1TA2suPZ1W5k(TElement _param1)
    {
      if ((object) this._composition == (object) _param1)
        return;
      this._composition = _param1;
      Action<TElement> zpDemcRi = this.\u0023\u003DzpDEmcRI\u003D;
      if (zpDemcRi == null)
        return;
      zpDemcRi(this._composition);
    }

    /// <summary>The diagram composite element change event.</summary>
    public event Action<TElement> CompositionChanged;

    /// <summary>The event of the stop at the breakpoint.</summary>
    public event Action<DiagramSocket> Break;

    /// <summary>The event of the stop at the error.</summary>
    public event Action<DiagramElement> Error;

    private DebuggerSyncObject \u0023\u003DzAu3kmCBue5qh(
      TElement _param1,
      TElement _param2)
    {
      Func<TElement, Func<DiagramSocket, bool, bool>, Action<DebuggerSyncObject>, Action<DebuggerSyncObject>, DebuggerSyncObject> zrUnr3JqaPkgL = this.\u0023\u003DzrUNr3JqaPkgL;
      TElement element1 = _param2;
      if ((object) element1 == null)
        element1 = _param1;
      Func<DiagramSocket, bool, bool> func = new Func<DiagramSocket, bool, bool>(this.\u0023\u003DzzHKcrLBvL64N);
      Action<DebuggerSyncObject> action1 = new Action<DebuggerSyncObject>(this.\u0023\u003DznJNAoT60L_Uk);
      Action<DebuggerSyncObject> action2 = new Action<DebuggerSyncObject>(this.\u0023\u003DztqgPiqUeJuHq);
      DebuggerSyncObject key = zrUnr3JqaPkgL(element1, func, action1, action2);
      this.\u0023\u003DzZlomYSWzWRzRs04YBCruVV4\u003D.Add(key, Tuple.Create<TElement, TElement>(_param1, _param2));
      foreach (DiagramElement element2 in _param1.Elements)
        this.\u0023\u003DzAu3kmCBue5qh(_param1, element2, key);
      return key;
    }

    private bool \u0023\u003DzzHKcrLBvL64N(DiagramSocket _param1, bool _param2)
    {
      DiagramDebugger<TElement>.\u0023\u003DzTZctEJRv\u0024eBu7F9aqhawtwk\u003D rvEBu7F9aqhawtwk = new DiagramDebugger<TElement>.\u0023\u003DzTZctEJRv\u0024eBu7F9aqhawtwk\u003D();
      rvEBu7F9aqhawtwk._socket01 = _param1;
      if (!this.IsEnabled & _param2)
        return false;
      DiagramSocketBreakpoint socketBreakpoint = this.Breakpoints.FirstOrDefault<DiagramSocketBreakpoint>(new Func<DiagramSocketBreakpoint, bool>(rvEBu7F9aqhawtwk.\u0023\u003DzjYpepD1z3MOPDQwiPQ\u003D\u003D));
      if (socketBreakpoint == null)
        return false;
      return socketBreakpoint.NeedBreak();
    }

    private void \u0023\u003DznJNAoT60L_Uk(DebuggerSyncObject _param1)
    {
      Action<DiagramSocket> zAcLsAjM = this.\u0023\u003DzACLsAjM\u003D;
      if (zAcLsAjM != null)
        zAcLsAjM(_param1.CurrentSocket);
      this.\u0023\u003Dz_87VL\u0024DimoGk(_param1);
    }

    private void \u0023\u003DztqgPiqUeJuHq(DebuggerSyncObject _param1)
    {
      Action<DiagramElement> zF78Js4 = this.\u0023\u003DzF7_8JS4\u003D;
      if (zF78Js4 != null)
        zF78Js4(_param1.CurrentElement);
      this.\u0023\u003Dz_87VL\u0024DimoGk(_param1);
    }

    private void \u0023\u003Dz_87VL\u0024DimoGk(DebuggerSyncObject _param1)
    {
      Tuple<TElement, TElement> tuple = this.\u0023\u003DzZlomYSWzWRzRs04YBCruVV4\u003D[_param1];
      if ((object) this.Composition == (object) tuple.Item1)
        return;
      this._debuggerSyncObject = _param1;
      this.\u0023\u003Dz1TA2suPZ1W5k(tuple.Item1);
    }

    private void \u0023\u003DzJx1hPj9Sdgka(DiagramElement _param1)
    {
      DiagramDebugger<TElement>.\u0023\u003DzAE3e7v95H2dbvOtPWQAtdw0\u003D ae3e7v95H2dbvOtPwqAtdw0 = new DiagramDebugger<TElement>.\u0023\u003DzAE3e7v95H2dbvOtPWQAtdw0\u003D();
      ae3e7v95H2dbvOtPwqAtdw0.\u0023\u003Dzj8aWB8I\u003D = this.Composition;
      DebuggerSyncObject key = this.\u0023\u003DzZlomYSWzWRzRs04YBCruVV4\u003D.First<KeyValuePair<DebuggerSyncObject, Tuple<TElement, TElement>>>(new Func<KeyValuePair<DebuggerSyncObject, Tuple<TElement, TElement>>, bool>(ae3e7v95H2dbvOtPwqAtdw0.\u0023\u003DzLh\u0024E2nV\u0024X3UVumYdoVH4\u0024rw\u003D)).Key;
      this.\u0023\u003DzAu3kmCBue5qh(ae3e7v95H2dbvOtPwqAtdw0.\u0023\u003Dzj8aWB8I\u003D, _param1, key);
    }

    private void \u0023\u003Dzy3K0Nyn3I4Pr(DiagramElement _param1)
    {
      foreach (DiagramSocket socket in _param1.InputSockets.Concat<DiagramSocket>((IEnumerable<DiagramSocket>) _param1.OutputSockets))
        this.RemoveBreak(socket);
    }

    private void \u0023\u003DzAu3kmCBue5qh(
      TElement _param1,
      DiagramElement _param2,
      DebuggerSyncObject _param3)
    {
      DiagramElement diagramElement = _param2;
      DebuggerSyncObject debuggerSyncObject = _param3;
      if (debuggerSyncObject == null)
        throw new ArgumentNullException(nameof(-1260199589));
      diagramElement.DebuggerSyncObject = debuggerSyncObject;
      _param2.InputSockets.Concat<DiagramSocket>((IEnumerable<DiagramSocket>) _param2.OutputSockets).Where<DiagramSocket>(DiagramDebugger<TElement>.LamdaShit.\u0023\u003DzPzKGGI4zHkAnFFT0tw\u003D\u003D ?? (DiagramDebugger<TElement>.LamdaShit.\u0023\u003DzPzKGGI4zHkAnFFT0tw\u003D\u003D = new Func<DiagramSocket, bool>(DiagramDebugger<TElement>.LamdaShit._lamdaShit.\u0023\u003Dz1vD9J1_MUtip6RqRiieuUZk\u003D))).ForEach<DiagramSocket>(new Action<DiagramSocket>(this.AddBreak));
      TElement element = _param2 as TElement;
      if ((object) element == null)
        return;
      this.\u0023\u003DzAu3kmCBue5qh(element, _param1);
    }

    /// <summary>To add a breakpoint in the socket.</summary>
    /// <param name="socket">Socket.</param>
    public void AddBreak(DiagramSocket socket)
    {
      if (socket == null)
        throw new ArgumentNullException(nameof(-1260199634));
      this.\u0023\u003DzpfPzp4c\u003D(DiagramDebugger<TElement>.\u0023\u003DzGGav5Kw5E6aL(socket));
    }

    /// <summary>To remove the breakpoint from the socket.</summary>
    /// <param name="socket">Socket.</param>
    public void RemoveBreak(DiagramSocket socket)
    {
      DiagramDebugger<TElement>.\u0023\u003DzCkslNO6yTBhyNNHGhCHLhT4\u003D no6yTbhyNnhGhChLhT4 = new DiagramDebugger<TElement>.\u0023\u003DzCkslNO6yTBhyNNHGhCHLhT4\u003D();
      no6yTbhyNnhGhChLhT4._socket01 = socket;
      if (no6yTbhyNnhGhChLhT4._socket01 == null)
        throw new ArgumentNullException(nameof(-1260199643));
      no6yTbhyNnhGhChLhT4._socket01.IsBreak = false;
      no6yTbhyNnhGhChLhT4._socket01.PropertyChanged -= new PropertyChangedEventHandler(this.\u0023\u003Dzg43Om4XEuzt5);
      lock (this._diagramSocketBreakpoints.SyncRoot)
        this._diagramSocketBreakpoints.RemoveWhere<DiagramSocketBreakpoint>(new Func<DiagramSocketBreakpoint, bool>(no6yTbhyNnhGhChLhT4.\u0023\u003Dzq2XQ7x6uMqK1eBrGVQ\u003D\u003D));
    }

    /// <summary>Remove all breakpoints from the scheme.</summary>
    public void RemoveAllBreaks()
    {
      foreach (DiagramSocketBreakpoint breakpoint in this.Breakpoints)
        breakpoint.Socket.IsBreak = false;
      this._diagramSocketBreakpoints.Clear();
    }

    /// <summary>Whether the socket is the breakpoint.</summary>
    /// <param name="socket">Socket.</param>
    /// <returns>
    /// <see langword="true" />, if the socket is the breakpoint, otherwise, <see langword="false" />.</returns>
    public bool IsBreak(DiagramSocket socket)
    {
      DiagramDebugger<TElement>.\u0023\u003DzCf3Q7rb\u0024cB8Ia1V3yxrMt0s\u003D q7rbCB8Ia1V3yxrMt0s = new DiagramDebugger<TElement>.\u0023\u003DzCf3Q7rb\u0024cB8Ia1V3yxrMt0s\u003D();
      q7rbCB8Ia1V3yxrMt0s._socket01 = socket;
      if (q7rbCB8Ia1V3yxrMt0s._socket01 == null)
        throw new ArgumentNullException(nameof(-1260199624));
      return this.Breakpoints.Any<DiagramSocketBreakpoint>(new Func<DiagramSocketBreakpoint, bool>(q7rbCB8Ia1V3yxrMt0s.\u0023\u003DzhjhN0zID5jzutvGAEA\u003D\u003D));
    }

    /// <summary>Whether the scheme contains the breakpoints.</summary>
    /// <returns>
    /// <see langword="true" />, if the socket is the breakpoint, otherwise, <see langword="false" />.</returns>
    public bool HasBreaks()
    {
      return this.Breakpoints.Any<DiagramSocketBreakpoint>();
    }

    /// <summary>To go to the next element.</summary>
    public void StepNext()
    {
      DebuggerSyncObject syncObject = this.\u0023\u003Dz2PcuxWMW1Las();
      if (syncObject == null)
        return;
      if (syncObject != this._debuggerSyncObject)
        this.\u0023\u003DznJNAoT60L_Uk(syncObject);
      syncObject.ContinueAndWaitOnNext();
    }

    /// <summary>To go inside the diagram composite element.</summary>
    /// <param name="composition">Composite element.</param>
    public void StepInto(TElement composition = null)
    {
      DiagramDebugger<TElement>.\u0023\u003DzzhXz8TA22deF_xlJjVonSgA\u003D ta22deFXlJjVonSgA = new DiagramDebugger<TElement>.\u0023\u003DzzhXz8TA22deF_xlJjVonSgA\u003D();
      ta22deFXlJjVonSgA.\u0023\u003Dzj8aWB8I\u003D = composition;
      if ((object) ta22deFXlJjVonSgA.\u0023\u003Dzj8aWB8I\u003D == null)
      {
        if (!this.CanStepInto)
          return;
        ta22deFXlJjVonSgA.\u0023\u003Dzj8aWB8I\u003D = (TElement) this._debuggerSyncObject.CurrentElement;
      }
      DebuggerSyncObject key = this.\u0023\u003DzZlomYSWzWRzRs04YBCruVV4\u003D.First<KeyValuePair<DebuggerSyncObject, Tuple<TElement, TElement>>>(new Func<KeyValuePair<DebuggerSyncObject, Tuple<TElement, TElement>>, bool>(ta22deFXlJjVonSgA.\u0023\u003DzUcQXxhW2YWU_eyVv6g\u003D\u003D)).Key;
      DebuggerSyncObject z4tQiVmZnaHx = this._debuggerSyncObject;
      this._debuggerSyncObject = key;
      this.\u0023\u003Dz1TA2suPZ1W5k(ta22deFXlJjVonSgA.\u0023\u003Dzj8aWB8I\u003D);
      if (z4tQiVmZnaHx.CurrentElement != (object) ta22deFXlJjVonSgA.\u0023\u003Dzj8aWB8I\u003D)
        return;
      key.SetWaitOnNext();
      z4tQiVmZnaHx.ContinueAndWaitOnNext();
    }

    /// <summary>To exit from the diagram composite element.</summary>
    /// <param name="composition">Composite element.</param>
    public void StepOut(TElement composition = null)
    {
      DiagramDebugger<TElement>.\u0023\u003DzPSLc2cb9SnZj_7ukFtcMSi0\u003D lc2cb9SnZj7ukFtcMsi0 = new DiagramDebugger<TElement>.\u0023\u003DzPSLc2cb9SnZj_7ukFtcMSi0\u003D();
      if (!this.CanStepOut)
        return;
      lc2cb9SnZj7ukFtcMsi0._kvp = this.\u0023\u003DzZlomYSWzWRzRs04YBCruVV4\u003D[this._debuggerSyncObject];
      DebuggerSyncObject key = this.\u0023\u003DzZlomYSWzWRzRs04YBCruVV4\u003D.First<KeyValuePair<DebuggerSyncObject, Tuple<TElement, TElement>>>(new Func<KeyValuePair<DebuggerSyncObject, Tuple<TElement, TElement>>, bool>(lc2cb9SnZj7ukFtcMsi0.\u0023\u003DzgPosVzXyyZ\u0024EPk00BA\u003D\u003D)).Key;
      DebuggerSyncObject z4tQiVmZnaHx = this._debuggerSyncObject;
      this._debuggerSyncObject = key;
      this.\u0023\u003Dz1TA2suPZ1W5k(lc2cb9SnZj7ukFtcMsi0._kvp.Item2);
      if ((object) composition != null && key.CurrentElement != (object) composition)
        return;
      key.SetWaitOnNext();
      z4tQiVmZnaHx.Continue();
    }

    /// <summary>Continue.</summary>
    public void Continue()
    {
      this.\u0023\u003Dz2PcuxWMW1Las()?.Continue();
    }

    /// <summary>Load settings.</summary>
    /// <param name="storage">Settings storage.</param>
    public void Load(SettingsStorage storage)
    {
      SettingsStorage[] settingsStorageArray = storage.GetValue<SettingsStorage[]>(nameof(-1260199665), (SettingsStorage[]) null);
      if (settingsStorageArray == null || (object) this.Composition == null)
        return;
      this.\u0023\u003Dz_OAG\u0024VQ\u003D((IEnumerable<SettingsStorage>) settingsStorageArray);
    }

    /// <summary>Save settings.</summary>
    /// <param name="storage">Settings storage.</param>
    public void Save(SettingsStorage storage)
    {
      storage.SetValue<SettingsStorage[]>(nameof(-1260199665), this._diagramSocketBreakpoints.Select<DiagramSocketBreakpoint, SettingsStorage>(new Func<DiagramSocketBreakpoint, SettingsStorage>(this.\u0023\u003DzFegZ\u0024\u0024dRWez8)).ToArray<SettingsStorage>());
    }

    private void \u0023\u003Dz_OAG\u0024VQ\u003D(IEnumerable<SettingsStorage> _param1)
    {
      foreach (SettingsStorage settingsStorage in _param1)
        this.\u0023\u003DzpfPzp4c\u003D(settingsStorage);
    }

    private void \u0023\u003DzpfPzp4c\u003D(SettingsStorage _param1)
    {
      IEnumerable<Guid> guids = _param1.GetValue<IEnumerable<Guid>>(nameof(-1260199651), (IEnumerable<Guid>) null);
      string str = _param1.GetValue<string>(nameof(-1260199443), (string) null);
      if (str.IsEmpty() || guids == null)
        return;
      DiagramSocket diagramSocket = this.\u0023\u003Dz_Rs4d7PBDw9N(guids, str);
      if (diagramSocket == null)
        return;
      DiagramSocketBreakpoint socketBreakpoint = DiagramDebugger<TElement>.\u0023\u003DzGGav5Kw5E6aL(diagramSocket);
      socketBreakpoint.Load(_param1);
      this.\u0023\u003DzpfPzp4c\u003D(socketBreakpoint);
    }

    private void \u0023\u003DzpfPzp4c\u003D(DiagramSocketBreakpoint _param1)
    {
      DiagramDebugger<TElement>.\u0023\u003DzQGkp6DX2oilqxeHXLx\u0024YNTk\u003D dx2oilqxeHxLxYnTk = new DiagramDebugger<TElement>.\u0023\u003DzQGkp6DX2oilqxeHXLx\u0024YNTk\u003D() { _socket01 = _param1.Socket };
      dx2oilqxeHxLxYnTk._socket01.IsBreak = true;
      if (this._diagramSocketBreakpoints.FirstOrDefault<DiagramSocketBreakpoint>(new Func<DiagramSocketBreakpoint, bool>(dx2oilqxeHxLxYnTk.\u0023\u003Dz0dF5VfDa3EOgdjV0gQ\u003D\u003D)) != null)
        return;
      dx2oilqxeHxLxYnTk._socket01.PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003Dzg43Om4XEuzt5);
      this._diagramSocketBreakpoints.Add(_param1);
    }

    private void \u0023\u003Dzg43Om4XEuzt5(object _param1, PropertyChangedEventArgs _param2)
    {
      DiagramSocket socket = (DiagramSocket) _param1;
      if (_param2.PropertyName != nameof(-1260198464))
        return;
      this.RemoveBreak(socket);
      this.AddBreak(socket);
    }

    private SettingsStorage \u0023\u003DzFegZ\u0024\u0024dRWez8(
      DiagramSocketBreakpoint _param1)
    {
      SettingsStorage settingsStorage = _param1.Save();
      settingsStorage.SetValue<string>(nameof(-1260199443), _param1.Socket.Id);
      settingsStorage.SetValue<IEnumerable<Guid>>(nameof(-1260199651), DiagramDebugger<TElement>.\u0023\u003DzVO4Ve0nspUqy(_param1.Socket.Parent));
      return settingsStorage;
    }

    private DiagramSocket \u0023\u003Dz_Rs4d7PBDw9N(
      IEnumerable<Guid> _param1,
      string _param2)
    {
      DiagramElement diagramElement = this.\u0023\u003DzOCWh58E\u003D(_param1);
      if (diagramElement == null)
        return (DiagramSocket) null;
      return diagramElement.InputSockets.FindById(_param2) ?? diagramElement.OutputSockets.FindById(_param2);
    }

    private DiagramElement \u0023\u003DzOCWh58E\u003D(IEnumerable<Guid> _param1)
    {
      DiagramElement diagramElement = (DiagramElement) null;
      IEnumerable<DiagramElement> elements = this.Composition.Elements;
      foreach (Guid guid in _param1)
      {
        diagramElement = elements.FirstOrDefault<DiagramElement>(new Func<DiagramElement, bool>(new DiagramDebugger<TElement>.\u0023\u003Dz9rJ_2tfPcIUiE_qa6aRmiQs\u003D()
        {
          \u0023\u003Dz9Dkb\u0024Vo\u003D = guid
        }.\u0023\u003DzjiTPicRJRSQ35CZZSw\u003D\u003D));
        TElement element = diagramElement as TElement;
        if ((object) element != null)
          elements = element.Elements;
      }
      return diagramElement;
    }

    private static IEnumerable<Guid> \u0023\u003DzVO4Ve0nspUqy(DiagramElement _param0)
    {
      List<Guid> guidList = new List<Guid>();
      for (; _param0 != null; _param0 = (DiagramElement) _param0.ParentComposition)
        guidList.Insert(0, _param0.Id);
      return (IEnumerable<Guid>) guidList.ToArray();
    }

    private DebuggerSyncObject \u0023\u003Dz2PcuxWMW1Las()
    {
      if (this._debuggerSyncObject.IsWaitingOnInput || this._debuggerSyncObject.IsWaitingOnOutput)
        return this._debuggerSyncObject;
      return this.\u0023\u003DzZlomYSWzWRzRs04YBCruVV4\u003D.Keys.FirstOrDefault<DebuggerSyncObject>(DiagramDebugger<TElement>.LamdaShit.\u0023\u003DzuRR632__tA9aaE0JKA\u003D\u003D ?? (DiagramDebugger<TElement>.LamdaShit.\u0023\u003DzuRR632__tA9aaE0JKA\u003D\u003D = new Func<DebuggerSyncObject, bool>(DiagramDebugger<TElement>.LamdaShit._lamdaShit.\u0023\u003DzE3lwA_XDiVD7aQq3MfwLHLQ\u003D)));
    }

    private static DiagramSocketBreakpoint \u0023\u003DzGGav5Kw5E6aL(
      DiagramSocket _param0)
    {
      DiagramSocketType type = _param0.Type;
      DiagramSocketBreakpoint socketBreakpoint;
      if ((Equatable<DiagramSocketType>) type == DiagramSocketType.Bool)
        socketBreakpoint = (DiagramSocketBreakpoint) new DiagramSocketBreakpointEx(_param0);
      else if ((Equatable<DiagramSocketType>) type == DiagramSocketType.IndicatorValue || (Equatable<DiagramSocketType>) type == DiagramSocketType.Unit)
        socketBreakpoint = (DiagramSocketBreakpoint) new DiagramSocketBreakpointEx2<Decimal>(_param0);
      else if ((Equatable<DiagramSocketType>) type == DiagramSocketType.Date)
        socketBreakpoint = (DiagramSocketBreakpoint) new DiagramSocketBreakpointEx2<DateTimeOffset>(_param0);
      else if ((Equatable<DiagramSocketType>) type == DiagramSocketType.Time)
        socketBreakpoint = (DiagramSocketBreakpoint) new DiagramSocketBreakpointEx2<TimeSpan>(_param0);
      else if (type.Type.IsEnum)
        socketBreakpoint = typeof (DiagramSocketBreakpointEx3<>).Make(type.Type).CreateInstance<DiagramSocketBreakpoint>((object[]) new object[1]
        {
          (object) _param0
        });
      else
        socketBreakpoint = new DiagramSocketBreakpoint(_param0);
      return socketBreakpoint;
    }

    private sealed class \u0023\u003Dz9rJ_2tfPcIUiE_qa6aRmiQs\u003D
    {
      public Guid \u0023\u003Dz9Dkb\u0024Vo\u003D;

      internal bool \u0023\u003DzjiTPicRJRSQ35CZZSw\u003D\u003D(DiagramElement _param1)
      {
        return _param1.Id == this.\u0023\u003Dz9Dkb\u0024Vo\u003D;
      }
    }

    private sealed class \u0023\u003DzAE3e7v95H2dbvOtPWQAtdw0\u003D
    {
      public \u0023\u003DzgPSz4\u00248\u003D \u0023\u003Dzj8aWB8I\u003D;

      internal bool \u0023\u003DzLh\u0024E2nV\u0024X3UVumYdoVH4\u0024rw\u003D(
        KeyValuePair<DebuggerSyncObject, Tuple<\u0023\u003DzgPSz4\u00248\u003D, \u0023\u003DzgPSz4\u00248\u003D>> _param1)
      {
        return (object) _param1.Value.Item1 == (object) this.\u0023\u003Dzj8aWB8I\u003D;
      }
    }

    private sealed class \u0023\u003DzCf3Q7rb\u0024cB8Ia1V3yxrMt0s\u003D
    {
      public DiagramSocket _socket01;

      internal bool \u0023\u003DzhjhN0zID5jzutvGAEA\u003D\u003D(DiagramSocketBreakpoint _param1)
      {
        return _param1.Socket == this._socket01;
      }
    }

    private sealed class \u0023\u003DzCkslNO6yTBhyNNHGhCHLhT4\u003D
    {
      public DiagramSocket _socket01;

      internal bool \u0023\u003Dzq2XQ7x6uMqK1eBrGVQ\u003D\u003D(DiagramSocketBreakpoint _param1)
      {
        return _param1.Socket == this._socket01;
      }
    }

    [Serializable]
    private sealed class LamdaShit
    {
      public static readonly DiagramDebugger<\u0023\u003DzgPSz4\u00248\u003D>.LamdaShit _lamdaShit = new DiagramDebugger<\u0023\u003DzgPSz4\u00248\u003D>.LamdaShit();
      public static Func<KeyValuePair<DebuggerSyncObject, Tuple<\u0023\u003DzgPSz4\u00248\u003D, \u0023\u003DzgPSz4\u00248\u003D>>, bool> \u0023\u003DzMv6PO2HThUrilC6VMQ\u003D\u003D;
      public static Func<KeyValuePair<DebuggerSyncObject, Tuple<\u0023\u003DzgPSz4\u00248\u003D, \u0023\u003DzgPSz4\u00248\u003D>>, bool> Func00023;
      public static Func<KeyValuePair<DebuggerSyncObject, Tuple<\u0023\u003DzgPSz4\u00248\u003D, \u0023\u003DzgPSz4\u00248\u003D>>, bool> \u0023\u003DzFkECh6zOfwaha4Eg6w\u003D\u003D;
      public static Func<DiagramSocket, bool> \u0023\u003DzPzKGGI4zHkAnFFT0tw\u003D\u003D;
      public static Func<DebuggerSyncObject, bool> \u0023\u003DzuRR632__tA9aaE0JKA\u003D\u003D;

      internal bool \u0023\u003DzJrRX\u00244vIpDvUP0qIDsxMgnEx80Pd(
        KeyValuePair<DebuggerSyncObject, Tuple<\u0023\u003DzgPSz4\u00248\u003D, \u0023\u003DzgPSz4\u00248\u003D>> _param1)
      {
        return _param1.Key.IsWaitingOnInput;
      }

      internal bool \u0023\u003DzUyflhixacGq3Tm8eL2lXT7X3im0x(
        KeyValuePair<DebuggerSyncObject, Tuple<\u0023\u003DzgPSz4\u00248\u003D, \u0023\u003DzgPSz4\u00248\u003D>> _param1)
      {
        return _param1.Key.IsWaitingOnOutput;
      }

      internal bool \u0023\u003DzP1MlxVYLmPWqnyXZuBni\u0024TlMi_27(
        KeyValuePair<DebuggerSyncObject, Tuple<\u0023\u003DzgPSz4\u00248\u003D, \u0023\u003DzgPSz4\u00248\u003D>> _param1)
      {
        return _param1.Key.CurrentError != null;
      }

      internal bool \u0023\u003Dz1vD9J1_MUtip6RqRiieuUZk\u003D(DiagramSocket _param1)
      {
        return _param1.IsBreak;
      }

      internal bool \u0023\u003DzE3lwA_XDiVD7aQq3MfwLHLQ\u003D(DebuggerSyncObject _param1)
      {
        if (!_param1.IsWaitingOnInput)
          return _param1.IsWaitingOnOutput;
        return true;
      }
    }

    private sealed class \u0023\u003DzPSLc2cb9SnZj_7ukFtcMSi0\u003D
    {
      public Tuple<\u0023\u003DzgPSz4\u00248\u003D, \u0023\u003DzgPSz4\u00248\u003D> _kvp;

      internal bool \u0023\u003DzgPosVzXyyZ\u0024EPk00BA\u003D\u003D(
        KeyValuePair<DebuggerSyncObject, Tuple<\u0023\u003DzgPSz4\u00248\u003D, \u0023\u003DzgPSz4\u00248\u003D>> _param1)
      {
        return (object) _param1.Value.Item1 == (object) this._kvp.Item2;
      }
    }

    private sealed class \u0023\u003DzQGkp6DX2oilqxeHXLx\u0024YNTk\u003D
    {
      public DiagramSocket _socket01;

      internal bool \u0023\u003Dz0dF5VfDa3EOgdjV0gQ\u003D\u003D(DiagramSocketBreakpoint _param1)
      {
        return _param1.Socket == this._socket01;
      }
    }

    private sealed class \u0023\u003DzTZctEJRv\u0024eBu7F9aqhawtwk\u003D
    {
      public DiagramSocket _socket01;

      internal bool \u0023\u003DzjYpepD1z3MOPDQwiPQ\u003D\u003D(DiagramSocketBreakpoint _param1)
      {
        return _param1.Socket == this._socket01;
      }
    }

    private sealed class \u0023\u003DzzhXz8TA22deF_xlJjVonSgA\u003D
    {
      public \u0023\u003DzgPSz4\u00248\u003D \u0023\u003Dzj8aWB8I\u003D;

      internal bool \u0023\u003DzUcQXxhW2YWU_eyVv6g\u003D\u003D(
        KeyValuePair<DebuggerSyncObject, Tuple<\u0023\u003DzgPSz4\u00248\u003D, \u0023\u003DzgPSz4\u00248\u003D>> _param1)
      {
        return (object) _param1.Value.Item1 == (object) this.\u0023\u003Dzj8aWB8I\u003D;
      }
    }
  }
}
