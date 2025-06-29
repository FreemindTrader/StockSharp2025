// Decompiled with JetBrains decompiler
// Type: #=zSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;

#nullable disable
internal struct \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq
{
  
  public int \u0023\u003DzLNbWazE\u003D;
  
  public int \u0023\u003DzrS5XlCc\u003D;
  
  public int \u0023\u003DzxpGWEWQ\u003D;
  
  public int \u0023\u003Dz3MxuAfw\u003D;
  
  public int \u0023\u003DzOTSgnUI\u003D;
  
  public int \u0023\u003Dz5QE1hBg\u003D;
  
  public int \u0023\u003DznfnGDE0\u003D;
  
  public int \u0023\u003Dz9HVnJ6Q\u003D;
  
  public bool \u0023\u003DzsSA0E44\u003D;
  
  public int \u0023\u003DzdeLhDzg\u003D;
  
  public int \u0023\u003Dzeids9mY\u003D;
  
  public int \u0023\u003DzcTlKzwLym0ae;
  
  public static byte[] \u0023\u003Dze5IoM_Jn6uMJVz6mAr6qc3MOgvlvgi_Yiw\u003D\u003D = new byte[8]
  {
    (byte) 0,
    (byte) 0,
    (byte) 1,
    (byte) 1,
    (byte) 3,
    (byte) 3,
    (byte) 2,
    (byte) 2
  };
  
  public static byte[] \u0023\u003DzACGNfqPcnCR58sAQlVn18cQ3r7fW = new byte[8]
  {
    (byte) 0,
    (byte) 1,
    (byte) 2,
    (byte) 1,
    (byte) 0,
    (byte) 3,
    (byte) 2,
    (byte) 3
  };

  public \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq(
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    this.\u0023\u003DzLNbWazE\u003D = _param1;
    this.\u0023\u003DzrS5XlCc\u003D = _param2;
    this.\u0023\u003DzxpGWEWQ\u003D = _param3;
    this.\u0023\u003Dz3MxuAfw\u003D = _param4;
    this.\u0023\u003DzOTSgnUI\u003D = Math.Abs(_param3 - _param1);
    this.\u0023\u003Dz5QE1hBg\u003D = Math.Abs(_param4 - _param2);
    this.\u0023\u003DznfnGDE0\u003D = _param3 > _param1 ? 1 : -1;
    this.\u0023\u003Dz9HVnJ6Q\u003D = _param4 > _param2 ? 1 : -1;
    this.\u0023\u003DzsSA0E44\u003D = this.\u0023\u003Dz5QE1hBg\u003D >= this.\u0023\u003DzOTSgnUI\u003D;
    this.\u0023\u003DzdeLhDzg\u003D = this.\u0023\u003DzsSA0E44\u003D ? this.\u0023\u003Dz9HVnJ6Q\u003D : this.\u0023\u003DznfnGDE0\u003D;
    this.\u0023\u003Dzeids9mY\u003D = _param5;
    this.\u0023\u003DzcTlKzwLym0ae = this.\u0023\u003Dz9HVnJ6Q\u003D & 4 | this.\u0023\u003DznfnGDE0\u003D & 2 | (this.\u0023\u003DzsSA0E44\u003D ? 1 : 0);
  }

  public uint \u0023\u003DzC2hK0\u0024VQy6X87oqsfL\u0024M3jzPNkO3()
  {
    return (uint) \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq.\u0023\u003Dze5IoM_Jn6uMJVz6mAr6qc3MOgvlvgi_Yiw\u003D\u003D[this.\u0023\u003DzcTlKzwLym0ae];
  }

  public uint \u0023\u003DzmTpfE0fZS4Lq_yRo0Fd9Z5rMrbvq()
  {
    return (uint) \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq.\u0023\u003DzACGNfqPcnCR58sAQlVn18cQ3r7fW[this.\u0023\u003DzcTlKzwLym0ae];
  }

  public bool \u0023\u003DzXLR5Fj3TSIKOeVCn7P0EMv4L5q19rQ6QgA\u003D\u003D(
    \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq _param1)
  {
    return (int) \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq.\u0023\u003Dze5IoM_Jn6uMJVz6mAr6qc3MOgvlvgi_Yiw\u003D\u003D[this.\u0023\u003DzcTlKzwLym0ae] == (int) \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq.\u0023\u003Dze5IoM_Jn6uMJVz6mAr6qc3MOgvlvgi_Yiw\u003D\u003D[_param1.\u0023\u003DzcTlKzwLym0ae];
  }

  public bool \u0023\u003DzyToNi4BS0TEnyiZXp5vG6rLikNUFYvADTA\u003D\u003D(
    \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq _param1)
  {
    return (int) \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq.\u0023\u003DzACGNfqPcnCR58sAQlVn18cQ3r7fW[this.\u0023\u003DzcTlKzwLym0ae] == (int) \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq.\u0023\u003DzACGNfqPcnCR58sAQlVn18cQ3r7fW[_param1.\u0023\u003DzcTlKzwLym0ae];
  }

  public void \u0023\u003Dz7g9OZUYDMLSX(
    out \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq _param1,
    out \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq _param2)
  {
    int num1 = this.\u0023\u003DzLNbWazE\u003D + this.\u0023\u003DzxpGWEWQ\u003D >> 1;
    int num2 = this.\u0023\u003DzrS5XlCc\u003D + this.\u0023\u003Dz3MxuAfw\u003D >> 1;
    int num3 = this.\u0023\u003Dzeids9mY\u003D >> 1;
    _param1 = this;
    _param2 = this;
    _param1.\u0023\u003DzxpGWEWQ\u003D = num1;
    _param1.\u0023\u003Dz3MxuAfw\u003D = num2;
    _param1.\u0023\u003Dzeids9mY\u003D = num3;
    _param1.\u0023\u003DzOTSgnUI\u003D = Math.Abs(_param1.\u0023\u003DzxpGWEWQ\u003D - _param1.\u0023\u003DzLNbWazE\u003D);
    _param1.\u0023\u003Dz5QE1hBg\u003D = Math.Abs(_param1.\u0023\u003Dz3MxuAfw\u003D - _param1.\u0023\u003DzrS5XlCc\u003D);
    _param2.\u0023\u003DzLNbWazE\u003D = num1;
    _param2.\u0023\u003DzrS5XlCc\u003D = num2;
    _param2.\u0023\u003Dzeids9mY\u003D = num3;
    _param2.\u0023\u003DzOTSgnUI\u003D = Math.Abs(_param2.\u0023\u003DzxpGWEWQ\u003D - _param2.\u0023\u003DzLNbWazE\u003D);
    _param2.\u0023\u003Dz5QE1hBg\u003D = Math.Abs(_param2.\u0023\u003Dz3MxuAfw\u003D - _param2.\u0023\u003DzrS5XlCc\u003D);
  }
}
