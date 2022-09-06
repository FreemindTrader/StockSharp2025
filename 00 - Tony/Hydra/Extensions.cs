// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Extensions
// Assembly: Hydra, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BCB1262-CD24-4283-B1AE-24E756F47247
// Assembly location: T:\00-StockSharp\Data\Hydra.dll

using Ecng.Xaml;
using StockSharp.Localization;
using StockSharp.Logging;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace StockSharp.Hydra
{
  internal static class Extensions
  {
    public static Task ContinueWithExceptionHandling(
      this Task task,
      Window window,
      Action<bool> action)
    {
      if (task == null)
        throw new ArgumentNullException(nameof (task));
      if (window == null)
        throw new ArgumentNullException(nameof (window));
      if (action == null)
        throw new ArgumentNullException(nameof (action));
      return task.ContinueWith( t =>
      {
          if ( task.IsFaulted )
          {
              Exception error;
              if ( task.Exception != null )
              {
                  task.Exception.LogError( null );
                  error = task.Exception.InnerException ?? task.Exception;
              }
              else
              {
                  error = new InvalidOperationException( LocalizedStrings.Str2914 );
                  error.LogError( null );
              }
              int num = ( int )new MessageBoxBuilder().Caption( LocalizedStrings.Str2915 ).Error( error ).Owner( window ).Show();
          }
          action( !task.IsFaulted );
      }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    public static Task ContinueWithExceptionHandling<T>(
      this Task<T> task,
      Window window,
      Action<bool, T> action)
    {
      if (task == null)
        throw new ArgumentNullException(nameof (task));
      if (window == null)
        throw new ArgumentNullException(nameof (window));
      if (action == null)
        throw new ArgumentNullException(nameof (action));
      return task.ContinueWith( t =>
      {
          if ( task.IsFaulted )
          {
              Exception error;
              if ( task.Exception != null )
              {
                  task.Exception.LogError( null );
                  error = task.Exception.InnerException ?? task.Exception;
              }
              else
              {
                  error = new InvalidOperationException( LocalizedStrings.Str2914 );
                  error.LogError( null );
              }
              int num = ( int )new MessageBoxBuilder().Caption( LocalizedStrings.Str2915 ).Error( error ).Owner( window ).Show();
          }
          action( !task.IsFaulted, t.Result );
      }, TaskScheduler.FromCurrentSynchronizationContext());
    }
  }
}
