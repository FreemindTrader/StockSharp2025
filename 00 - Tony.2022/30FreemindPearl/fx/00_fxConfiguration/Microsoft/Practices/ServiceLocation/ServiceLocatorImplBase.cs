// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.ServiceLocation.ServiceLocatorImplBase
// Assembly: fx.Configuration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98DE432C-119E-449E-A101-030B64638B2E
// Assembly location: A:\StockSharpBin\Data\fx.Configuration.dll

using fx.Configuration.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Practices.ServiceLocation
{
  public abstract class ServiceLocatorImplBase : IServiceLocator, IServiceProvider
  {
    public virtual object GetService(Type serviceType) => this.GetInstance(serviceType, (string) null);

    public virtual object GetInstance(Type serviceType) => this.GetInstance(serviceType, (string) null);

    public virtual object GetInstance(Type serviceType, string key)
    {
      try
      {
        return this.DoGetInstance(serviceType, key);
      }
      catch (Exception ex)
      {
        throw new ActivationException(this.FormatActivationExceptionMessage(ex, serviceType, key), ex);
      }
    }

    public virtual IEnumerable<object> GetAllInstances(Type serviceType)
    {
      try
      {
        return this.DoGetAllInstances(serviceType);
      }
      catch (Exception ex)
      {
        throw new ActivationException(this.FormatActivateAllExceptionMessage(ex, serviceType), ex);
      }
    }

    public virtual TService GetInstance<TService>() => (TService) this.GetInstance(typeof (TService), (string) null);

    public virtual TService GetInstance<TService>(string key) => (TService) this.GetInstance(typeof (TService), key);

    public virtual IEnumerable<TService> GetAllInstances<TService>()
    {
      foreach (TService allInstance in this.GetAllInstances(typeof (TService)))
        yield return allInstance;
    }

    protected abstract object DoGetInstance(Type serviceType, string key);

    protected abstract IEnumerable<object> DoGetAllInstances(Type serviceType);

    protected virtual string FormatActivationExceptionMessage(
      Exception actualException,
      Type serviceType,
      string key)
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Resources.ActivationExceptionMessage, (object) serviceType.Name, (object) key);
    }

    protected virtual string FormatActivateAllExceptionMessage(
      Exception actualException,
      Type serviceType)
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Resources.ActivateAllExceptionMessage, (object) serviceType.Name);
    }
  }
}
