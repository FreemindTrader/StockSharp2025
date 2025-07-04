// Decompiled with JetBrains decompiler
// Type: #=zN_ef$eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6$8Y
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

#nullable disable
internal sealed class \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y : 
  \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyNMn5sYJZKzXDppYgrIuwPB
{
  private readonly SciChartSurface \u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D;

  internal \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y(
    SciChartSurface _param1)
  {
    this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D = _param1;
  }

  public \u0023\u003Dzgg5QOmcWitJriAsXqwM_mn\u0024OgQPZalPRDrRrx2Q\u003D \u0023\u003Dz\u0024ccLugjL4c3p(
    IRenderContext2D _param1)
  {
    \u0023\u003Dzgg5QOmcWitJriAsXqwM_mn\u0024OgQPZalPRDrRrx2Q\u003D ogQpZalPrDrRrx2Q;
    if (!this.\u0023\u003DzsGkoBP41VLI4((ISciChartSurface) this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D, out ogQpZalPrDrRrx2Q))
    {
      this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D.XAxes.\u0023\u003Dz30RSSSygABj_<IAxis>(\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzxLUmJTOQFZstXBxKkg\u003D\u003D ?? (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzxLUmJTOQFZstXBxKkg\u003D\u003D = new Action<IAxis>(\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzIQem3qD8jTxU5SNvvnOyq\u0024w\u003D)));
      this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D.YAxes.\u0023\u003Dz30RSSSygABj_<IAxis>(\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz4cfR9WtX1fuK0AziEg\u003D\u003D ?? (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz4cfR9WtX1fuK0AziEg\u003D\u003D = new Action<IAxis>(\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz\u0024Fzx1KPXiYDq5k3tb2H1uRc\u003D)));
      _param1.\u0023\u003DzUf222sU\u003D();
      return ogQpZalPrDrRrx2Q;
    }
    using (IUpdateSuspender fq05jnDg3bOrIrgCjote = this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D.SuspendUpdates())
    {
      fq05jnDg3bOrIrgCjote.ResumeTargetOnDispose=false;
      \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("Beginning Render Loop ... ", Array.Empty<object>());
      this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D.RenderableSeries.\u0023\u003Dz30RSSSygABj_<IRenderableSeries>(\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzrvebN45bGKztwoYTiA\u003D\u003D ?? (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzrvebN45bGKztwoYTiA\u003D\u003D = new Action<IRenderableSeries>(\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz12PmAHE\u0024tD8BqW7x3ljg98Q\u003D)));
      Size size = this.\u0023\u003DzMRPYrFQxdo1UO1h10rB90f8\u003D((ISciChartSurface) this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D);
      if (this.\u0023\u003DzsGkoBP41VLI4((ISciChartSurface) this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D, size, out ogQpZalPrDrRrx2Q))
      {
        \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D ji3RvrDhjH0IDtfw = \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dztbp\u0024Gkvr2KbJ((ISciChartSurface) this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D, size);
        _param1.\u0023\u003DzUf222sU\u003D();
        \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003DzXkIFuR87leFI((ISciChartSurface) this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D, ji3RvrDhjH0IDtfw, _param1);
        \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz\u0024jjEdE7fySXB((ISciChartSurface) this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D, ji3RvrDhjH0IDtfw, _param1);
        \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003DzMr2eR\u0024whaAA_(this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D, ji3RvrDhjH0IDtfw);
        this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D.\u0023\u003Dzgpj0vvz6kQmYv5labCkwdSo\u003D();
        if (ji3RvrDhjH0IDtfw.\u0023\u003Dz38sEjvRVtcBw().Any<string>())
          ogQpZalPrDrRrx2Q = new \u0023\u003Dzgg5QOmcWitJriAsXqwM_mn\u0024OgQPZalPRDrRrx2Q\u003D($"{\u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003Dz0N9wkruNEA8n2VLn\u0024A\u003D\u003D?.ToString()}\r\n - {string.Join("\r\n - ", (IEnumerable<string>) ji3RvrDhjH0IDtfw.\u0023\u003Dz38sEjvRVtcBw())}\r\n{\u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003DzCGXxEfvxqF5P?.ToString()}");
      }
    }
    return ogQpZalPrDrRrx2Q;
  }

  private bool \u0023\u003DzsGkoBP41VLI4(
    ISciChartSurface _param1,
    out \u0023\u003Dzgg5QOmcWitJriAsXqwM_mn\u0024OgQPZalPRDrRrx2Q\u003D _param2)
  {
    bool flag = true;
    string str = string.Empty;
    if (_param1.get_RenderSurface() == null)
    {
      str = \u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003Dzz9ZrbqKsk2rEOWELZA\u003D\u003D.Value + \u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003DzCGXxEfvxqF5P?.ToString();
      flag = false;
    }
    else if (_param1.get_XAxes() == null || _param1.get_YAxes() == null)
    {
      str = \u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003DzSPgiXVV8P9OntbEWmi9QaNcIfsic.Value + \u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003DzCGXxEfvxqF5P?.ToString();
      flag = false;
    }
    else if (_param1.get_XAxes().\u0023\u003DzMeGSfVE\u003D<IAxis>())
    {
      str = \u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003Dz1WwzKaZStTVUhWQT3_YVd5E\u003D.Value + \u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003DzCGXxEfvxqF5P?.ToString();
      flag = false;
    }
    else if (_param1.get_YAxes().\u0023\u003DzMeGSfVE\u003D<IAxis>())
    {
      str = \u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003DzJmwTBL5wi7Zy8Thmt0etFrw\u003D.Value + \u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003DzCGXxEfvxqF5P?.ToString();
      flag = false;
    }
    else if (_param1.XAxis is dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd && _param1.get_RenderableSeries().\u0023\u003DzCCMM80zDpO6N<IRenderableSeries>())
    {
      str = \u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003DzlgqIi4B2zuQC1ih8Gx\u0024aMAJJcKZtTnjFo_9FGCE\u003D.Value + \u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003DzCGXxEfvxqF5P?.ToString();
      flag = false;
    }
    _param2 = string.IsNullOrWhiteSpace(str) ? \u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003DzWsHTUE0\u003D : new \u0023\u003Dzgg5QOmcWitJriAsXqwM_mn\u0024OgQPZalPRDrRrx2Q\u003D(str);
    return flag;
  }

  private bool \u0023\u003DzsGkoBP41VLI4(
    ISciChartSurface _param1,
    Size _param2,
    out \u0023\u003Dzgg5QOmcWitJriAsXqwM_mn\u0024OgQPZalPRDrRrx2Q\u003D _param3)
  {
    int num = _param2.Width < 2.0 ? 0 : (_param2.Height >= 2.0 ? 1 : 0);
    bool flag1 = !_param1.get_XAxes().Any<IAxis>(\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzKkIXz5CmqQ1_DowyDQ\u003D\u003D ?? (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzKkIXz5CmqQ1_DowyDQ\u003D\u003D = new Func<IAxis, bool>(\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzSjEALlyd467CDZCTsTwMrbo\u003D))) && !_param1.get_YAxes().Any<IAxis>(\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzPgm0u7xHO4Weuga3SQ\u003D\u003D ?? (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzPgm0u7xHO4Weuga3SQ\u003D\u003D = new Func<IAxis, bool>(\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz5TYzsFNMIz7uGfzQgeU3D4E\u003D)));
    bool flag2 = !_param1.get_XAxes().Any<IAxis>(\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzr3tKWd1XimhmhizQXg\u003D\u003D ?? (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzr3tKWd1XimhmhizQXg\u003D\u003D = new Func<IAxis, bool>(\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzRViYxWKLBIrrEXdSy3aN0E0\u003D))) && !_param1.get_YAxes().Any<IAxis>(\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz08eHob2PYGlidFcWEw\u003D\u003D ?? (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz08eHob2PYGlidFcWEw\u003D\u003D = new Func<IAxis, bool>(\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzqo9jwlb7htkBZP\u0024kf2q4SPc\u003D)));
    bool flag3 = !this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D.RenderableSeries.\u0023\u003DzCCMM80zDpO6N<IRenderableSeries>();
    bool flag4 = flag3 && this.\u0023\u003Dz3jEW\u0024apUUb0ZDjNsDQ\u003D\u003D.RenderableSeries.All<IRenderableSeries>(\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz\u0024by00HMkgfLkjvi3Lg\u003D\u003D ?? (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz\u0024by00HMkgfLkjvi3Lg\u003D\u003D = new Func<IRenderableSeries, bool>(\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz6hBTksUWEhZTQqEktRG1x1Q\u003D)));
    bool flag5 = _param1.get_RenderSurface() != null;
    string str = string.Empty;
    if (num == 0)
      str = $"{str}{\u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003DzZfZaD0daRA_Ayq3hAw\u003D\u003D?.ToString()}\r\n\r\n";
    if (!flag1)
      str = $"{str}{\u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003Dz7fmvaUdOaMs43deOXxIUBmxuL2EB\u0024BAvPMeAJ4c\u003D?.ToString()}\r\n\r\n";
    if (!flag2)
      str = $"{str}{\u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003DzlBYRG1LNre\u0024H9D3BpA\u003D\u003D?.ToString()}\r\n\r\n";
    if (!flag3)
      str = $"{str}{\u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003DzBtDGwTzEpj7wy4EhQgmbsn5ekSZR?.ToString()}\r\n\r\n";
    if (!flag4)
      str = $"{str}{\u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003Dz5eJmyrndq8Dw2Juf7A\u003D\u003D?.ToString()}\r\n\r\n";
    if (!flag5)
      str = $"{str}{\u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003Dzz9ZrbqKsk2rEOWELZA\u003D\u003D?.ToString()}\r\n\r\n";
    if (!string.IsNullOrWhiteSpace(str))
      str += \u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003DzCGXxEfvxqF5P?.ToString();
    _param3 = string.IsNullOrWhiteSpace(str) ? \u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003DzWsHTUE0\u003D : new \u0023\u003Dzgg5QOmcWitJriAsXqwM_mn\u0024OgQPZalPRDrRrx2Q\u003D(str);
    return (num & (flag1 ? 1 : 0) & (flag2 ? 1 : 0)) != 0;
  }

  internal Size \u0023\u003DzMRPYrFQxdo1UO1h10rB90f8\u003D(
    ISciChartSurface _param1)
  {
    \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003DzryEzCCc\u0024xGDHkstU1g\u003D\u003D zryEzCccXGdHkstU1g = new \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003DzryEzCCc\u0024xGDHkstU1g\u003D\u003D();
    zryEzCccXGdHkstU1g.\u0023\u003DzRRvwDu67s9Rm = this;
    zryEzCccXGdHkstU1g.\u0023\u003Dzyyh4GZw\u003D = _param1;
    using (IUpdateSuspender fq05jnDg3bOrIrgCjote = zryEzCccXGdHkstU1g.\u0023\u003Dzyyh4GZw\u003D.SuspendUpdates())
    {
      fq05jnDg3bOrIrgCjote.ResumeTargetOnDispose=false;
      if (zryEzCccXGdHkstU1g.\u0023\u003Dzyyh4GZw\u003D.get_ViewportManager() == null)
        throw new InvalidOperationException("UltrachartSurface.ViewportManager is null. Try setting a new DefaultViewportManager()");
      zryEzCccXGdHkstU1g.\u0023\u003Dzyyh4GZw\u003D.get_XAxes().\u0023\u003Dz30RSSSygABj_<IAxis>(new Action<IAxis>(zryEzCccXGdHkstU1g.\u0023\u003DzYoUR26ISw2UpoBPdXNwkaJzrEw2s));
      zryEzCccXGdHkstU1g.\u0023\u003Dzyyh4GZw\u003D.get_YAxes().\u0023\u003Dz30RSSSygABj_<IAxis>(new Action<IAxis>(zryEzCccXGdHkstU1g.\u0023\u003Dz0tlnCwc6fL_hMd81kJD8Xf8mm6KC));
    }
    return zryEzCccXGdHkstU1g.\u0023\u003Dzyyh4GZw\u003D.\u0023\u003DzBr6p5Qw\u0024W6BFNGQPNFOKrj0\u003D();
  }

  internal void \u0023\u003DzM6kifusZQQ4QQbVaepuobjg\u003D(
    IAxis _param1,
    ISciChartSurface _param2)
  {
    using (IUpdateSuspender fq05jnDg3bOrIrgCjote = _param1.SuspendUpdates())
    {
      fq05jnDg3bOrIrgCjote.ResumeTargetOnDispose=false;
      if ((!_param1.get_HasValidVisibleRange() || _param1.get_HasDefaultVisibleRange() ? (_param1.get_AutoRange() == dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd.Once ? 1 : (_param1.get_AutoRange() == dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd.Always ? 1 : 0)) : 0) == 0)
        return;
      IRange abyLt9clZggmJsWhw = _param2.get_ViewportManager().\u0023\u003DzF9GccVo\u0024dKwo(_param1);
      if (abyLt9clZggmJsWhw.Equals((object) _param1.VisibleRange) || !_param1.\u0023\u003Dz2OKbyRBzRCBL(abyLt9clZggmJsWhw))
        return;
      _param1.VisibleRange = abyLt9clZggmJsWhw;
    }
  }

  internal static \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D \u0023\u003Dztbp\u0024Gkvr2KbJ(
    ISciChartSurface _param0,
    Size _param1)
  {
    \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.SomeClass34343 zPKCmcad6Nxc5A8A1 = new \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.SomeClass34343();
    zPKCmcad6Nxc5A8A1.\u0023\u003Dzyyh4GZw\u003D = _param0;
    ObservableCollection<IRenderableSeries> renderableSeries = zPKCmcad6Nxc5A8A1.\u0023\u003Dzyyh4GZw\u003D.get_RenderableSeries();
    List<IRenderableSeries> list = renderableSeries != null ? renderableSeries.Where<IRenderableSeries>(new Func<IRenderableSeries, bool>(zPKCmcad6Nxc5A8A1.\u0023\u003DzFJV4yCyS4GkfcIY8Kg\u003D\u003D)).ToList<IRenderableSeries>() : (List<IRenderableSeries>) null;
    // ISSUE: explicit non-virtual call
    int count = list != null ? __nonvirtual (list.Count) : 0;
    \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.SomeClass34343 zPKCmcad6Nxc5A8A2 = zPKCmcad6Nxc5A8A1;
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D ji3RvrDhjH0IDtfw1 = new \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D();
    ji3RvrDhjH0IDtfw1.\u0023\u003Dz_li6Ttc\u003D = _param1;
    ji3RvrDhjH0IDtfw1.\u0023\u003DzKBMzFep0KndT(new List<string>());
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D ji3RvrDhjH0IDtfw2 = ji3RvrDhjH0IDtfw1;
    zPKCmcad6Nxc5A8A2.\u0023\u003Dz7Pgaal_fKVBn = ji3RvrDhjH0IDtfw2;
    if (Math.Abs(zPKCmcad6Nxc5A8A1.\u0023\u003Dz7Pgaal_fKVBn.\u0023\u003Dz_li6Ttc\u003D.Width) < double.Epsilon || Math.Abs(zPKCmcad6Nxc5A8A1.\u0023\u003Dz7Pgaal_fKVBn.\u0023\u003Dz_li6Ttc\u003D.Height) < double.Epsilon)
    {
      ji3RvrDhjH0IDtfw1 = new \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D();
      return ji3RvrDhjH0IDtfw1;
    }
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("Drawing {0}: Width={1}, Height={2}", new object[3]
    {
      (object) zPKCmcad6Nxc5A8A1.\u0023\u003Dzyyh4GZw\u003D.GetType().Name,
      (object) zPKCmcad6Nxc5A8A1.\u0023\u003Dz7Pgaal_fKVBn.\u0023\u003Dz_li6Ttc\u003D.Width,
      (object) zPKCmcad6Nxc5A8A1.\u0023\u003Dz7Pgaal_fKVBn.\u0023\u003Dz_li6Ttc\u003D.Height
    });
    zPKCmcad6Nxc5A8A1.\u0023\u003Dz7Pgaal_fKVBn.\u0023\u003DzRS6ptUHIm4pSg8PXQHYA71s\u003D = new IRenderableSeries[count];
    zPKCmcad6Nxc5A8A1.\u0023\u003Dz7Pgaal_fKVBn.\u0023\u003Dz4nxjMSnapDjJ = new \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ[count];
    zPKCmcad6Nxc5A8A1.\u0023\u003Dz7Pgaal_fKVBn.\u0023\u003Dzoc6wScE\u003D = new \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D[count];
    zPKCmcad6Nxc5A8A1.\u0023\u003Dz7Pgaal_fKVBn.\u0023\u003Dz8O95DKv93zY9 = new IndexRange [count];
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6S86EZeND1KSf7Q5ckAbN6LxyEWNToOUjo1\u00243K\u00241Ho2jpA\u003D\u003D toOujo13K1Ho2jpA = zPKCmcad6Nxc5A8A1.\u0023\u003Dzyyh4GZw\u003D.\u0023\u003Dzu\u0024P3XgkcE7BC().\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6S86EZeND1KSf7Q5ckAbN6LxyEWNToOUjo1\u00243K\u00241Ho2jpA\u003D\u003D>();
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) toOujo13K1Ho2jpA, "resamplerFactory");
    \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz4ASrz8c\u0024JOa7(zPKCmcad6Nxc5A8A1.\u0023\u003Dzyyh4GZw\u003D);
    for (int index = 0; index < count; ++index)
    {
      IRenderableSeries s1JolYrWoYpqmQ6ug = list[index];
      \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D ns01UjmP40FpxAl2jmQ;
      IndexRange  g8Oq2rGx6KyfAreq;
      \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ftrixUnpTllY1PkTyq;
      \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz2iW_UgNEfXLUkWrnJw\u003D\u003D(zPKCmcad6Nxc5A8A1.\u0023\u003Dzyyh4GZw\u003D.get_XAxes(), s1JolYrWoYpqmQ6ug, zPKCmcad6Nxc5A8A1.\u0023\u003Dz7Pgaal_fKVBn, toOujo13K1Ho2jpA, out ns01UjmP40FpxAl2jmQ, out g8Oq2rGx6KyfAreq, out ftrixUnpTllY1PkTyq);
      zPKCmcad6Nxc5A8A1.\u0023\u003Dz7Pgaal_fKVBn.\u0023\u003DzRS6ptUHIm4pSg8PXQHYA71s\u003D[index] = list[index];
      zPKCmcad6Nxc5A8A1.\u0023\u003Dz7Pgaal_fKVBn.\u0023\u003Dzoc6wScE\u003D[index] = ns01UjmP40FpxAl2jmQ;
      zPKCmcad6Nxc5A8A1.\u0023\u003Dz7Pgaal_fKVBn.\u0023\u003Dz8O95DKv93zY9[index] = g8Oq2rGx6KyfAreq;
      zPKCmcad6Nxc5A8A1.\u0023\u003Dz7Pgaal_fKVBn.\u0023\u003Dz4nxjMSnapDjJ[index] = ftrixUnpTllY1PkTyq;
    }
    \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz4ASrz8c\u0024JOa7(zPKCmcad6Nxc5A8A1.\u0023\u003Dzyyh4GZw\u003D, zPKCmcad6Nxc5A8A1.\u0023\u003Dz7Pgaal_fKVBn);
    \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz3rSTr1zRgknd(zPKCmcad6Nxc5A8A1.\u0023\u003Dzyyh4GZw\u003D, zPKCmcad6Nxc5A8A1.\u0023\u003Dz7Pgaal_fKVBn);
    zPKCmcad6Nxc5A8A1.\u0023\u003Dz7Pgaal_fKVBn.\u0023\u003DzSBrmxtNmmDcWbby6Gm0UVio\u003D = (IDictionary<string, \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>>) new Dictionary<string, \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>>();
    zPKCmcad6Nxc5A8A1.\u0023\u003Dz7Pgaal_fKVBn.\u0023\u003DzPmZCkENGGLQws2poeTWSb6E\u003D = (IDictionary<string, \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>>) new Dictionary<string, \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>>();
    zPKCmcad6Nxc5A8A1.\u0023\u003Dzyyh4GZw\u003D.get_YAxes().\u0023\u003Dz30RSSSygABj_<IAxis>(new Action<IAxis>(zPKCmcad6Nxc5A8A1.\u0023\u003Dzk_1CgJv47CJNqo8WAA\u003D\u003D));
    zPKCmcad6Nxc5A8A1.\u0023\u003Dzyyh4GZw\u003D.get_XAxes().\u0023\u003Dz30RSSSygABj_<IAxis>(new Action<IAxis>(zPKCmcad6Nxc5A8A1.\u0023\u003DzpCJ3SPiIlRwAd0naiA\u003D\u003D));
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSXWkz2jl56XJoPdfqB4\u003D wkz2jl56XjoPdfqB4 = zPKCmcad6Nxc5A8A1.\u0023\u003Dzyyh4GZw\u003D.\u0023\u003Dzu\u0024P3XgkcE7BC().\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSXWkz2jl56XJoPdfqB4\u003D>();
    zPKCmcad6Nxc5A8A1.\u0023\u003Dz7Pgaal_fKVBn.\u0023\u003Dzh7CDlE8GGxyy = wkz2jl56XjoPdfqB4.\u0023\u003DzhGnS3f5TTzO8();
    return zPKCmcad6Nxc5A8A1.\u0023\u003Dz7Pgaal_fKVBn;
  }

  private static void \u0023\u003Dz2iW_UgNEfXLUkWrnJw\u003D\u003D(
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D _param0,
    IRenderableSeries _param1,
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param2,
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6S86EZeND1KSf7Q5ckAbN6LxyEWNToOUjo1\u00243K\u00241Ho2jpA\u003D\u003D _param3,
    out \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D _param4,
    out IndexRange  _param5,
    out \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ _param6)
  {
    _param5 = (IndexRange ) null;
    _param6 = (\u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ) null;
    _param4 = _param1.get_DataSeries();
    if (_param4 == null || !_param1.IsVisible)
      return;
    IAxis dynWmoFzgH4RlWB0lB = _param0.\u0023\u003Dz\u0024YoxjvGBoa2C(_param1.get_XAxisId(), true);
    dynWmoFzgH4RlWB0lB.\u0023\u003DzQ4klw1orSVl\u0024(_param4.get_XType());
    IRange visibleRange = dynWmoFzgH4RlWB0lB.VisibleRange;
    _param5 = _param4.GetIndicesRange(visibleRange);
    if (!_param5.IsDefined)
      return;
    bool flag = _param1.\u0023\u003DztPaciKMZWysZOtqEskMFjk8\u003D();
    bool isCategoryAxis = dynWmoFzgH4RlWB0lB.get_IsCategoryAxis();
    if (isCategoryAxis)
    {
      _param5.Min = Math.Max(_param5.Min - 1, 0);
      _param5.Max = Math.Min(_param5.Max + 1, _param4.get_Count() - 1);
    }
    _param5 = _param1.\u0023\u003DzVAnbwOJn98Ya(_param5);
    _param5 = new IndexRange (Math.Max(0, _param5.Min), Math.Min(_param4.get_Count() - 1, _param5.Max));
    if (!_param5.IsDefined)
      return;
    _param6 = _param4.ToPointSeries(_param1.get_ResamplingMode(), _param5, (int) _param2.\u0023\u003Dz_li6Ttc\u003D.Width, isCategoryAxis, new bool?(flag), visibleRange, _param3, _param1.\u0023\u003DzQavr9eonlwL7DeqLQA\u003D\u003D());
  }

  internal static void \u0023\u003Dz4ASrz8c\u0024JOa7(
    ISciChartSurface _param0)
  {
    foreach (IAxis xax in (Collection<IAxis>) _param0.get_XAxes())
    {
      using (IUpdateSuspender fq05jnDg3bOrIrgCjote = xax.SuspendUpdates())
      {
        fq05jnDg3bOrIrgCjote.ResumeTargetOnDispose=false;
        IRange abyLt9clZggmJsWhw = _param0.get_ViewportManager().\u0023\u003Dzj78Bq7gNYueNTATr1Q\u003D\u003D(xax);
        if (!abyLt9clZggmJsWhw.Equals((object) xax.VisibleRange))
        {
          if (xax.\u0023\u003Dz2OKbyRBzRCBL(abyLt9clZggmJsWhw))
            xax.VisibleRange = abyLt9clZggmJsWhw;
        }
      }
    }
  }

  internal static void \u0023\u003Dz4ASrz8c\u0024JOa7(
    ISciChartSurface _param0,
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1)
  {
    foreach (IAxis dynWmoFzgH4RlWB0lB in _param0.get_XAxes().Where<IAxis>(\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D ?? (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D = new Func<IAxis, bool>(\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzdGNaXcajdlqJiD2ISykIIuZo69wW))))
    {
      \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003DzHF4K0hz6MwLRPm7JFGVw01g\u003D k0hz6MwLrPm7JfgVw01g = new \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003DzHF4K0hz6MwLRPm7JFGVw01g\u003D();
      k0hz6MwLrPm7JfgVw01g.\u0023\u003Dz4bf8Oyc\u003D = dynWmoFzgH4RlWB0lB;
      \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ftrixUnpTllY1PkTyq = \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dzm6624LJWpww_(_param0, _param1, new Func<IRenderableSeries, bool>(k0hz6MwLrPm7JfgVw01g.\u0023\u003DzF3QfbV0ybHhoavXSPCz09zI\u003D));
      k0hz6MwLrPm7JfgVw01g.\u0023\u003Dz4bf8Oyc\u003D.\u0023\u003Dzs15X3Ar32F1\u0024(_param1, ftrixUnpTllY1PkTyq);
    }
  }

  private static void \u0023\u003Dz3rSTr1zRgknd(
    ISciChartSurface _param0,
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1)
  {
    foreach (IAxis yax in (Collection<IAxis>) _param0.get_YAxes())
    {
      \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D dop2SzA2WchXh2wc = new \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D();
      dop2SzA2WchXh2wc.\u0023\u003DzS7JsfCE\u003D = yax;
      using (IUpdateSuspender fq05jnDg3bOrIrgCjote = dop2SzA2WchXh2wc.\u0023\u003DzS7JsfCE\u003D.SuspendUpdates())
      {
        fq05jnDg3bOrIrgCjote.ResumeTargetOnDispose=false;
        IRange abyLt9clZggmJsWhw = _param0.get_ViewportManager().\u0023\u003DzhDjoRkNuf3z7_02BMQ\u003D\u003D(dop2SzA2WchXh2wc.\u0023\u003DzS7JsfCE\u003D, _param1);
        if (!abyLt9clZggmJsWhw.Equals((object) dop2SzA2WchXh2wc.\u0023\u003DzS7JsfCE\u003D.VisibleRange) && dop2SzA2WchXh2wc.\u0023\u003DzS7JsfCE\u003D.\u0023\u003Dz2OKbyRBzRCBL(abyLt9clZggmJsWhw))
          dop2SzA2WchXh2wc.\u0023\u003DzS7JsfCE\u003D.VisibleRange = abyLt9clZggmJsWhw;
        \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ftrixUnpTllY1PkTyq = \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dzm6624LJWpww_(_param0, _param1, new Func<IRenderableSeries, bool>(dop2SzA2WchXh2wc.\u0023\u003DzelRiTdR9_Z_W9jMgDc061as\u003D));
        dop2SzA2WchXh2wc.\u0023\u003DzS7JsfCE\u003D.\u0023\u003Dzs15X3Ar32F1\u0024(_param1, ftrixUnpTllY1PkTyq);
      }
    }
  }

  private static \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ \u0023\u003Dzm6624LJWpww_(
    ISciChartSurface _param0,
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1,
    Func<IRenderableSeries, bool> _param2)
  {
    \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz\u0024BXIxgXe6cQmBJ9OJYFFE7I\u003D xe6cQmBj9OjyffE7I = new \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz\u0024BXIxgXe6cQmBJ9OJYFFE7I\u003D();
    xe6cQmBj9OjyffE7I.\u0023\u003Dzm6bkhTMv4TGS = _param2;
    IRenderableSeries s1JolYrWoYpqmQ6ug = _param0.get_RenderableSeries().FirstOrDefault<IRenderableSeries>(new Func<IRenderableSeries, bool>(xe6cQmBj9OjyffE7I.\u0023\u003DzEQtawKU0gsYICcBClTLrWSc\u003D)) ?? _param0.get_RenderableSeries().FirstOrDefault<IRenderableSeries>(new Func<IRenderableSeries, bool>(xe6cQmBj9OjyffE7I.\u0023\u003Dz\u0024MTmBniphw3al5MgNQIrqkg\u003D));
    if (((IEnumerable<\u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ>) _param1.\u0023\u003Dz4nxjMSnapDjJ).\u0023\u003DzCCMM80zDpO6N<\u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ>() || s1JolYrWoYpqmQ6ug == null)
      return (\u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ) null;
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D[] zoc6wScE = _param1.\u0023\u003Dzoc6wScE\u003D;
    for (int index = 0; index < zoc6wScE.Length; ++index)
    {
      if (_param1.\u0023\u003DzRS6ptUHIm4pSg8PXQHYA71s\u003D[index].IsVisible && zoc6wScE[index] == s1JolYrWoYpqmQ6ug.get_DataSeries())
        return _param1.\u0023\u003Dz4nxjMSnapDjJ[index];
    }
    return (\u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ) null;
  }

  internal static void \u0023\u003DzXkIFuR87leFI(
    ISciChartSurface _param0,
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1,
    IRenderContext2D _param2)
  {
    foreach (IAxis xax in (Collection<IAxis>) _param0.get_XAxes())
    {
      xax.\u0023\u003DzpTR8\u0024ECbZOHX();
      xax.OnDraw(_param2, (IRenderPassData) null);
    }
    foreach (IAxis yax in (Collection<IAxis>) _param0.get_YAxes())
    {
      yax.\u0023\u003DzpTR8\u0024ECbZOHX();
      yax.OnDraw(_param2, (IRenderPassData) null);
    }
    _param2.\u0023\u003Dz7eGjoBhvKuFN().\u0023\u003DzY9qzIPY\u003D();
  }

  internal static void \u0023\u003Dz\u0024jjEdE7fySXB(
    ISciChartSurface _param0,
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1,
    IRenderContext2D _param2)
  {
    if (_param1.\u0023\u003DzRS6ptUHIm4pSg8PXQHYA71s\u003D == null)
      return;
    List<int> intList = new List<int>();
    for (int index = 0; index < _param1.\u0023\u003DzRS6ptUHIm4pSg8PXQHYA71s\u003D.Length; ++index)
    {
      IRenderableSeries uhIm4pSg8PxqhyA71 = _param1.\u0023\u003DzRS6ptUHIm4pSg8PXQHYA71s\u003D[index];
      if (uhIm4pSg8PxqhyA71 != null)
      {
        uhIm4pSg8PxqhyA71.XAxis = _param0.get_XAxes().\u0023\u003Dz\u0024YoxjvGBoa2C(uhIm4pSg8PxqhyA71.get_XAxisId(), true);
        uhIm4pSg8PxqhyA71.YAxis = _param0.get_YAxes().\u0023\u003Dz\u0024YoxjvGBoa2C(uhIm4pSg8PxqhyA71.get_YAxisId(), true);
        if (_param1.\u0023\u003DzRS6ptUHIm4pSg8PXQHYA71s\u003D[index].get_IsSelected())
          intList.Add(index);
        else
          \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003DzGSFc2j3MjwIx(_param0, _param1, _param2, index);
      }
    }
    foreach (int num in intList)
      \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003DzGSFc2j3MjwIx(_param0, _param1, _param2, num);
    _param0.\u0023\u003Dzu\u0024P3XgkcE7BC().\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D>().\u0023\u003DzosHqOAc\u003D<\u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAUdzyaQmAfsVzCpDWg08TVWt>(new \u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAUdzyaQmAfsVzCpDWg08TVWt((object) _param0, _param2));
  }

  private static void \u0023\u003DzGSFc2j3MjwIx(
    ISciChartSurface _param0,
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1,
    IRenderContext2D _param2,
    int _param3)
  {
    IRenderableSeries uhIm4pSg8PxqhyA71 = _param1.\u0023\u003DzRS6ptUHIm4pSg8PXQHYA71s\u003D[_param3];
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w1;
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w2;
    if (!_param1.\u0023\u003DzPmZCkENGGLQws2poeTWSb6E\u003D.TryGetValue(uhIm4pSg8PxqhyA71.get_YAxisId(), out xkzemsMs5tGkouk5w1) || !_param1.\u0023\u003DzSBrmxtNmmDcWbby6Gm0UVio\u003D.TryGetValue(uhIm4pSg8PxqhyA71.get_XAxisId(), out xkzemsMs5tGkouk5w2))
      return;
    RenderPassData  euujfPtWyKeLacKm = new RenderPassData (_param1.\u0023\u003Dz8O95DKv93zY9[_param3], xkzemsMs5tGkouk5w2, xkzemsMs5tGkouk5w1, _param1.\u0023\u003Dz4nxjMSnapDjJ[_param3], _param1.\u0023\u003Dzh7CDlE8GGxyy);
    uhIm4pSg8PxqhyA71.OnDraw(_param2, (IRenderPassData) euujfPtWyKeLacKm);
  }

  internal static void \u0023\u003DzMr2eR\u0024whaAA_(
    SciChartSurface _param0,
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1)
  {
    if (_param0.Annotations == null)
      return;
    _param0.Annotations.\u0023\u003Dzhwms24tGl3w4(_param1);
  }

  private sealed class \u0023\u003Dz\u0024BXIxgXe6cQmBJ9OJYFFE7I\u003D
  {
    public Func<IRenderableSeries, bool> \u0023\u003Dzm6bkhTMv4TGS;

    internal bool \u0023\u003DzEQtawKU0gsYICcBClTLrWSc\u003D(
      IRenderableSeries _param1)
    {
      if (!this.\u0023\u003Dzm6bkhTMv4TGS(_param1))
        return false;
      \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D dataSeries = _param1.get_DataSeries();
      return dataSeries != null && dataSeries.get_HasValues() && !dataSeries.get_IsSecondary();
    }

    internal bool \u0023\u003Dz\u0024MTmBniphw3al5MgNQIrqkg\u003D(
      IRenderableSeries _param1)
    {
      if (!this.\u0023\u003Dzm6bkhTMv4TGS(_param1))
        return false;
      \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D dataSeries = _param1.get_DataSeries();
      return dataSeries != null && dataSeries.get_HasValues();
    }
  }

  private sealed class \u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D
  {
    public IAxis \u0023\u003DzS7JsfCE\u003D;

    internal bool \u0023\u003DzelRiTdR9_Z_W9jMgDc061as\u003D(
      IRenderableSeries _param1)
    {
      return _param1.get_YAxisId() == this.\u0023\u003DzS7JsfCE\u003D.Id && _param1.IsVisible;
    }
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y.\u0023\u003Dz7qOdpi4\u003D();
    public static Action<IAxis> \u0023\u003DzxLUmJTOQFZstXBxKkg\u003D\u003D;
    public static Action<IAxis> \u0023\u003Dz4cfR9WtX1fuK0AziEg\u003D\u003D;
    public static Action<IRenderableSeries> \u0023\u003DzrvebN45bGKztwoYTiA\u003D\u003D;
    public static Func<IAxis, bool> \u0023\u003DzKkIXz5CmqQ1_DowyDQ\u003D\u003D;
    public static Func<IAxis, bool> \u0023\u003DzPgm0u7xHO4Weuga3SQ\u003D\u003D;
    public static Func<IAxis, bool> \u0023\u003Dzr3tKWd1XimhmhizQXg\u003D\u003D;
    public static Func<IAxis, bool> \u0023\u003Dz08eHob2PYGlidFcWEw\u003D\u003D;
    public static Func<IRenderableSeries, bool> \u0023\u003Dz\u0024by00HMkgfLkjvi3Lg\u003D\u003D;
    public static Func<IAxis, bool> \u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D;

    internal void \u0023\u003DzIQem3qD8jTxU5SNvvnOyq\u0024w\u003D(
      IAxis _param1)
    {
      _param1.\u0023\u003DzUf222sU\u003D();
    }

    internal void \u0023\u003Dz\u0024Fzx1KPXiYDq5k3tb2H1uRc\u003D(
      IAxis _param1)
    {
      _param1.\u0023\u003DzUf222sU\u003D();
    }

    internal void \u0023\u003Dz12PmAHE\u0024tD8BqW7x3ljg98Q\u003D(
      IRenderableSeries _param1)
    {
      _param1.get_DataSeries()?.OnBeginRenderPass();
    }

    internal bool \u0023\u003DzSjEALlyd467CDZCTsTwMrbo\u003D(
      IAxis _param1)
    {
      return !_param1.get_HasValidVisibleRange() && _param1.get_AutoRange() == dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd.Never;
    }

    internal bool \u0023\u003Dz5TYzsFNMIz7uGfzQgeU3D4E\u003D(
      IAxis _param1)
    {
      return !_param1.get_HasValidVisibleRange() && _param1.get_AutoRange() == dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd.Never;
    }

    internal bool \u0023\u003DzRViYxWKLBIrrEXdSy3aN0E0\u003D(
      IAxis _param1)
    {
      return _param1.TickProvider == null;
    }

    internal bool \u0023\u003Dzqo9jwlb7htkBZP\u0024kf2q4SPc\u003D(
      IAxis _param1)
    {
      return _param1.TickProvider == null;
    }

    internal bool \u0023\u003Dz6hBTksUWEhZTQqEktRG1x1Q\u003D(
      IRenderableSeries _param1)
    {
      return _param1.get_DataSeries() != null;
    }

    internal bool \u0023\u003DzdGNaXcajdlqJiD2ISykIIuZo69wW(
      IAxis _param1)
    {
      return _param1 != null;
    }
  }

  private sealed class \u0023\u003DzHF4K0hz6MwLRPm7JFGVw01g\u003D
  {
    public IAxis \u0023\u003Dz4bf8Oyc\u003D;

    internal bool \u0023\u003DzF3QfbV0ybHhoavXSPCz09zI\u003D(
      IRenderableSeries _param1)
    {
      return _param1.get_XAxisId() == this.\u0023\u003Dz4bf8Oyc\u003D.Id && _param1.IsVisible;
    }
  }

  private sealed class SomeClass34343
  {
    public ISciChartSurface \u0023\u003Dzyyh4GZw\u003D;
    public \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D \u0023\u003Dz7Pgaal_fKVBn;

    internal bool \u0023\u003DzFJV4yCyS4GkfcIY8Kg\u003D\u003D(
      IRenderableSeries _param1)
    {
      return this.\u0023\u003Dzyyh4GZw\u003D.get_XAxes().\u0023\u003Dz\u0024YoxjvGBoa2C(_param1.get_XAxisId(), false) != null && this.\u0023\u003Dzyyh4GZw\u003D.get_YAxes().\u0023\u003Dz\u0024YoxjvGBoa2C(_param1.get_YAxisId(), false) != null;
    }

    internal void \u0023\u003Dzk_1CgJv47CJNqo8WAA\u003D\u003D(
      IAxis _param1)
    {
      this.\u0023\u003Dz7Pgaal_fKVBn.\u0023\u003DzPmZCkENGGLQws2poeTWSb6E\u003D.Add(_param1.Id, _param1.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D());
    }

    internal void \u0023\u003DzpCJ3SPiIlRwAd0naiA\u003D\u003D(
      IAxis _param1)
    {
      this.\u0023\u003Dz7Pgaal_fKVBn.\u0023\u003DzSBrmxtNmmDcWbby6Gm0UVio\u003D.Add(_param1.Id, _param1.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D());
    }
  }

  private sealed class \u0023\u003DzryEzCCc\u0024xGDHkstU1g\u003D\u003D
  {
    public \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y \u0023\u003DzRRvwDu67s9Rm;
    public ISciChartSurface \u0023\u003Dzyyh4GZw\u003D;

    internal void \u0023\u003DzYoUR26ISw2UpoBPdXNwkaJzrEw2s(
      IAxis _param1)
    {
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzM6kifusZQQ4QQbVaepuobjg\u003D(_param1, this.\u0023\u003Dzyyh4GZw\u003D);
    }

    internal void \u0023\u003Dz0tlnCwc6fL_hMd81kJD8Xf8mm6KC(
      IAxis _param1)
    {
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzM6kifusZQQ4QQbVaepuobjg\u003D(_param1, this.\u0023\u003Dzyyh4GZw\u003D);
    }
  }
}
