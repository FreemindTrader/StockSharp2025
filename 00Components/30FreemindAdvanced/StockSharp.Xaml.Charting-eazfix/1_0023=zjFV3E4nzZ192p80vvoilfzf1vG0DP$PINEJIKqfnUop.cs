// Decompiled with JetBrains decompiler
// Type: #=zjFV3E4nzZ192p80vvoilfzf1vG0DP$PINEJIKqfnUopKfI8OT5_BZ8JPFd9ajk0SArLBIIs=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
internal sealed class \u0023\u003DzjFV3E4nzZ192p80vvoilfzf1vG0DP\u0024PINEJIKqfnUopKfI8OT5_BZ8JPFd9ajk0SArLBIIs\u003D : 
  \u0023\u003DzXTZ2b\u0024bZtVqaBDtznjklQzroW5BoBmJmtevt_T1hEovvhMdDh7AlyPpCANOzxM\u0024uOQ\u003D\u003D
{
  private double[,] \u0023\u003DzE_Rfjp377uwj = new double[4, 2];
  private bool \u0023\u003Dz8PW3zMA4tVaI;

  public \u0023\u003DzjFV3E4nzZ192p80vvoilfzf1vG0DP\u0024PINEJIKqfnUopKfI8OT5_BZ8JPFd9ajk0SArLBIIs\u003D()
  {
    this.\u0023\u003Dz8PW3zMA4tVaI = false;
  }

  public \u0023\u003DzjFV3E4nzZ192p80vvoilfzf1vG0DP\u0024PINEJIKqfnUopKfI8OT5_BZ8JPFd9ajk0SArLBIIs\u003D(
    double[] _param1,
    double[] _param2)
  {
    this.\u0023\u003Dzn3jkXVeq2GUBYK2tJJ8afzk\u003D(_param1, _param2);
  }

  public \u0023\u003DzjFV3E4nzZ192p80vvoilfzf1vG0DP\u0024PINEJIKqfnUopKfI8OT5_BZ8JPFd9ajk0SArLBIIs\u003D(
    double _param1,
    double _param2,
    double _param3,
    double _param4,
    double[] _param5)
  {
    this.\u0023\u003DzYr6YTezm6bfVwU4yyQ\u003D\u003D(_param1, _param2, _param3, _param4, _param5);
  }

  public \u0023\u003DzjFV3E4nzZ192p80vvoilfzf1vG0DP\u0024PINEJIKqfnUopKfI8OT5_BZ8JPFd9ajk0SArLBIIs\u003D(
    double[] _param1,
    double _param2,
    double _param3,
    double _param4,
    double _param5)
  {
    this.\u0023\u003Dz1qz9PcUtElV_en2uiA\u003D\u003D(_param1, _param2, _param3, _param4, _param5);
  }

  public void \u0023\u003Dzn3jkXVeq2GUBYK2tJJ8afzk\u003D(double[] _param1, double[] _param2)
  {
    double[,] numArray1 = new double[4, 4];
    double[,] numArray2 = new double[4, 2];
    for (uint index1 = 0; index1 < 4U; ++index1)
    {
      uint index2 = index1 * 2U;
      uint index3 = index2 + 1U;
      numArray1[(int) index1, 0] = 1.0;
      numArray1[(int) index1, 1] = _param1[(int) index2] * _param1[(int) index3];
      numArray1[(int) index1, 2] = _param1[(int) index2];
      numArray1[(int) index1, 3] = _param1[(int) index3];
      numArray2[(int) index1, 0] = _param2[(int) index2];
      numArray2[(int) index1, 1] = _param2[(int) index3];
    }
    this.\u0023\u003Dz8PW3zMA4tVaI = \u0023\u003Dz4tSiDY285eyTFNy9DwhLpD1UwAzu2BuP9lqlOFDmwILYsPaMHR9_6vDu77nAwU3HyQ\u003D\u003D.\u0023\u003Dz2weMuqtP1bYM(numArray1, numArray2, this.\u0023\u003DzE_Rfjp377uwj);
  }

  public void \u0023\u003DzYr6YTezm6bfVwU4yyQ\u003D\u003D(
    double _param1,
    double _param2,
    double _param3,
    double _param4,
    double[] _param5)
  {
    double[] numArray = new double[8];
    numArray[0] = numArray[6] = _param1;
    numArray[2] = numArray[4] = _param3;
    numArray[1] = numArray[3] = _param2;
    numArray[5] = numArray[7] = _param4;
    this.\u0023\u003Dzn3jkXVeq2GUBYK2tJJ8afzk\u003D(numArray, _param5);
  }

  public void \u0023\u003Dz1qz9PcUtElV_en2uiA\u003D\u003D(
    double[] _param1,
    double _param2,
    double _param3,
    double _param4,
    double _param5)
  {
    double[] numArray = new double[8];
    numArray[0] = numArray[6] = _param2;
    numArray[2] = numArray[4] = _param4;
    numArray[1] = numArray[3] = _param3;
    numArray[5] = numArray[7] = _param5;
    this.\u0023\u003Dzn3jkXVeq2GUBYK2tJJ8afzk\u003D(_param1, numArray);
  }

  public bool \u0023\u003DzTQw5DGfvJbLF() => this.\u0023\u003Dz8PW3zMA4tVaI;

  public void \u0023\u003DzhA5n1D0\u003D(ref double _param1, ref double _param2)
  {
    double num1 = _param1;
    double num2 = _param2;
    double num3 = num1 * num2;
    _param1 = this.\u0023\u003DzE_Rfjp377uwj[0, 0] + this.\u0023\u003DzE_Rfjp377uwj[1, 0] * num3 + this.\u0023\u003DzE_Rfjp377uwj[2, 0] * num1 + this.\u0023\u003DzE_Rfjp377uwj[3, 0] * num2;
    _param2 = this.\u0023\u003DzE_Rfjp377uwj[0, 1] + this.\u0023\u003DzE_Rfjp377uwj[1, 1] * num3 + this.\u0023\u003DzE_Rfjp377uwj[2, 1] * num1 + this.\u0023\u003DzE_Rfjp377uwj[3, 1] * num2;
  }

  public \u0023\u003DzjFV3E4nzZ192p80vvoilfzf1vG0DP\u0024PINEJIKqfnUopKfI8OT5_BZ8JPFd9ajk0SArLBIIs\u003D.\u0023\u003DzBOORYnnbiC2X \u0023\u003DzoLjFgpI\u003D(
    double _param1,
    double _param2,
    double _param3)
  {
    return new \u0023\u003DzjFV3E4nzZ192p80vvoilfzf1vG0DP\u0024PINEJIKqfnUopKfI8OT5_BZ8JPFd9ajk0SArLBIIs\u003D.\u0023\u003DzBOORYnnbiC2X(_param1, _param2, _param3, this.\u0023\u003DzE_Rfjp377uwj);
  }

  internal sealed class \u0023\u003DzBOORYnnbiC2X
  {
    private double \u0023\u003DzgyMj__wzNkSr;
    private double \u0023\u003DzwefICal7Dcfa;
    public double \u0023\u003DzwP120vA\u003D;
    public double \u0023\u003Dzi8jDI4I\u003D;

    public \u0023\u003DzBOORYnnbiC2X()
    {
    }

    public \u0023\u003DzBOORYnnbiC2X(
      double _param1,
      double _param2,
      double _param3,
      double[,] _param4)
    {
      this.\u0023\u003DzgyMj__wzNkSr = _param4[1, 0] * _param3 * _param2 + _param4[2, 0] * _param3;
      this.\u0023\u003DzwefICal7Dcfa = _param4[1, 1] * _param3 * _param2 + _param4[2, 1] * _param3;
      this.\u0023\u003DzwP120vA\u003D = _param4[0, 0] + _param4[1, 0] * _param1 * _param2 + _param4[2, 0] * _param1 + _param4[3, 0] * _param2;
      this.\u0023\u003Dzi8jDI4I\u003D = _param4[0, 1] + _param4[1, 1] * _param1 * _param2 + _param4[2, 1] * _param1 + _param4[3, 1] * _param2;
    }

    public static \u0023\u003DzjFV3E4nzZ192p80vvoilfzf1vG0DP\u0024PINEJIKqfnUopKfI8OT5_BZ8JPFd9ajk0SArLBIIs\u003D.\u0023\u003DzBOORYnnbiC2X operator ++(
      \u0023\u003DzjFV3E4nzZ192p80vvoilfzf1vG0DP\u0024PINEJIKqfnUopKfI8OT5_BZ8JPFd9ajk0SArLBIIs\u003D.\u0023\u003DzBOORYnnbiC2X _param0)
    {
      _param0.\u0023\u003DzwP120vA\u003D += _param0.\u0023\u003DzgyMj__wzNkSr;
      _param0.\u0023\u003Dzi8jDI4I\u003D += _param0.\u0023\u003DzwefICal7Dcfa;
      return _param0;
    }
  }
}
