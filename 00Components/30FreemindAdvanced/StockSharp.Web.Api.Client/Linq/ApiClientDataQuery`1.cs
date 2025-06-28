// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.Linq.ApiClientDataQuery`1
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Linq;
using Ecng.Reflection;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client.Linq;

internal class ApiClientDataQuery<T> :
  IOrderedQueryable<T>,
  IEnumerable<T>,
  IEnumerable,
  IOrderedQueryable,
  IQueryable,
  IQueryable<T>,
  IQueryProvider
  where T : BaseEntity
{
    private readonly object _service;
    private readonly long _maxTake;
    private readonly string _methodName;
    private readonly Expression _expression;

    public ApiClientDataQuery(object service, string methodName, long maxTake)
    {
        this._service = service ?? throw new ArgumentNullException(nameof(service));
        this._methodName = !StringHelper.IsEmpty(methodName) ? methodName : throw new ArgumentNullException(nameof(methodName));
        this._maxTake = maxTake;
        this._expression = (Expression)Expression.Constant((object)this);
    }

    public ApiClientDataQuery(
      object service,
      string methodName,
      long maxTake,
      Expression expression)
      : this(service, methodName, maxTake)
    {
        this._expression = expression ?? throw new ArgumentNullException(nameof(expression));
    }

    Type IQueryable.ElementType => typeof(T);

    IQueryProvider IQueryable.Provider => (IQueryProvider)this;

    Expression IQueryable.Expression => this._expression;

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return Enumerable.Cast<T>(((IQueryProvider)this).Execute<T[]>(this._expression)).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator)((IEnumerable<T>)this).GetEnumerator();

    IQueryable IQueryProvider.CreateQuery(Expression expression) => throw new NotSupportedException();

    IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
    {
        return TypeHelper.CreateInstance<IQueryable<TElement>>(TypeHelper.Make(typeof(ApiClientDataQuery<>), new Type[1]
        {
      typeof (TElement)
        }), new object[4]
        {
      this._service,
      (object) this._methodName,
      (object) this._maxTake,
      (object) expression
        });
    }

    object IQueryProvider.Execute(Expression expression) => throw new NotSupportedException();

    TResult IQueryProvider.Execute<TResult>(Expression expression)
    {
        ApiClientDataQuery<T>.Visitor visitor = new ApiClientDataQuery<T>.Visitor();
        visitor.Visit(expression);
        MethodInfo member = ReflectionHelper.GetMember<MethodInfo>(this._service.GetType(), this._methodName, Array.Empty<Type>());
        ParameterInfo[] parameterInfoArray = (object)member != null ? member.GetParameters() : throw new InvalidOperationException(this._service.GetType().AssemblyQualifiedName);
        object[] parameters = new object[parameterInfoArray.Length];
        foreach (KeyValuePair<string, (ComparisonOperator op, object val)> filter in visitor.Filters)
        {
            string name = filter.Key;
            object obj = filter.Value.val;
            if (obj is BaseEntity baseEntity)
                obj = (object)baseEntity.Id;
            int index1 = CollectionHelper.IndexOf<ParameterInfo>((IEnumerable<ParameterInfo>)parameterInfoArray, (Func<ParameterInfo, bool>)(p => StringHelper.EqualsIgnoreCase(p.Name, name)));
            if (index1 != -1)
            {
                ParameterInfo parameterInfo = parameterInfoArray[index1];
                parameters[index1] = !(obj is string str) || !(parameterInfo.ParameterType == typeof(DateTime)) && !(parameterInfo.ParameterType == typeof(DateTime?)) ? Converter.To(obj, parameterInfo.ParameterType) : (object)TimeHelper.ToDateTime(str, "yyyyMMdd", (CultureInfo)null);
                int index2 = CollectionHelper.IndexOf<ParameterInfo>((IEnumerable<ParameterInfo>)parameterInfoArray, (Func<ParameterInfo, bool>)(p => StringHelper.EqualsIgnoreCase(p.Name, name + "Like")));
                if (index2 != -1)
                    parameters[index2] = (object)filter.Value.op;
            }
        }
        if ((object)visitor.OrderBy != null)
        {
            parameters[3] = (object)visitor.OrderBy.Name;
            if (visitor.OrderByDesc)
                parameters[4] = (object)true;
        }
        if (visitor.IsCount)
        {
            parameters[1] = (object)0L;
        }
        else
        {
            parameters[0] = (object)visitor.Skip;
            parameters[1] = (object)(visitor.Take ?? this._maxTake);
            if (visitor.IsSet)
                parameters[5] = (object)true;
        }
        parameters[parameters.Length - 1] = (object)visitor.CancellationToken;
        Task task = (Task)member.Invoke(this._service, parameters);
        if (visitor.IsSet)
            return Converter.To<TResult>((object)task);
        if ((object)ReflectionHelper.GetGenericType(typeof(TResult), typeof(ValueTask<>)) != null)
        {
            if (visitor.IsCount)
                return Converter.To<TResult>((object)AsyncHelper.AsValueTask<long>(task.ContinueWith<long>((Func<Task, long>)(t => AsyncHelper.GetResult<IBaseEntitySet>(t).Count), TaskContinuationOptions.ExecuteSynchronously)));
            Type itemType = ReflectionHelper.GetItemType(((IEnumerable<Type>)typeof(TResult).GetGenericArguments()).First<Type>());
            return Converter.To<TResult>(ReflectionHelper.Make(ReflectionHelper.GetMember<MethodInfo>(this.GetType(), "GetTask", Array.Empty<Type>()), new Type[1]
            {
        itemType
            }).Invoke((object)null, (object[])new Task[1]
            {
        task
            }));
        }
        IBaseEntitySet result = AsyncHelper.GetResult<IBaseEntitySet>(task);
        return !visitor.IsCount ? Converter.To<TResult>((object)result.Items) : Converter.To<TResult>((object)result.Count);
    }

    private static ValueTask<TItem[]> GetTask<TItem>(Task task)
    {
        return Converter.To<ValueTask<TItem[]>>((object)AsyncHelper.AsValueTask<TItem[]>(TypeHelper.CheckOnNull<Task>(task, nameof(task)).ContinueWith<TItem[]>((Func<Task, TItem[]>)(t => (TItem[])AsyncHelper.GetResult<IBaseEntitySet>(t).Items), TaskContinuationOptions.ExecuteSynchronously)));
    }

    private class Visitor : ExpressionVisitor
    {
        public bool IsCount { get; private set; }

        public bool IsSet { get; private set; }

        public long Skip { get; private set; }

        public long? Take { get; private set; }

        public CancellationToken CancellationToken { get; private set; }

        public MemberInfo OrderBy { get; private set; }

        public bool OrderByDesc { get; private set; }

        public Dictionary<string, (ComparisonOperator op, object val)> Filters { get; } = new Dictionary<string, (ComparisonOperator, object)>();

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == "Count")
                this.IsCount = true;
            else if (node.Method.Name == "ToEntitySetAsync")
            {
                this.IsSet = true;
                this.CancellationToken = ExpressionExtensions.GetValue<CancellationToken>(node.Arguments[1]);
            }
            else if (node.Method.Name == "CountAsync")
            {
                this.IsCount = true;
                this.CancellationToken = ExpressionExtensions.GetValue<CancellationToken>(node.Arguments[1]);
            }
            else if (node.Method.Name == "ToArrayAsync")
                this.CancellationToken = ExpressionExtensions.GetValue<CancellationToken>(node.Arguments[1]);
            else if (node.Method.Name == "Skip")
                this.Skip = ExpressionExtensions.GetValue<long>(node.Arguments[1]);
            else if (node.Method.Name == "Take")
                this.Take = new long?(ExpressionExtensions.GetValue<long>(node.Arguments[1]));
            else if (node.Method.Name == "OrderBy" || node.Method.Name == "OrderByDescending")
            {
                this.OrderBy = ((MemberExpression)((LambdaExpression)ExpressionExtensions.StripQuotes(node.Arguments[1])).Body).Member;
                if (node.Method.Name == "OrderByDescending")
                    this.OrderByDesc = true;
            }
            else if (node.Method.Name == "Where")
            {
                Expression body = ((LambdaExpression)ExpressionExtensions.StripQuotes(node.Arguments[1])).Body;
                if (body is BinaryExpression be)
                {
                    if (be.NodeType == ExpressionType.AndAlso)
                    {
                        ProcessBinary((BinaryExpression)be.Left);
                        ProcessBinary((BinaryExpression)be.Right);
                    }
                    else
                        ProcessBinary(be);
                }
                else
                {
                    MethodCallExpression methodCallExpression = (MethodCallExpression)body;
                    if (methodCallExpression.Method.Name != "By")
                        throw new InvalidOperationException(methodCallExpression.Method.Name);
                    this.Filters[ExpressionExtensions.GetValue<string>(methodCallExpression.Arguments[1])] = ((ComparisonOperator)0, (object)ExpressionExtensions.GetValue<string>(methodCallExpression.Arguments[2]));
                }
            }
            return base.VisitMethodCall(node);

            void ProcessBinary(BinaryExpression be)
            {
                ComparisonOperator comparisonOperator = ExpressionExtensions.ToOperator(be.NodeType);
                Expression expression = be.Left;
                Expression right = be.Right;
                if (expression is BinaryExpression)
                {
                    be = (BinaryExpression)right;
                    expression = be.Left;
                    right = be.Right;
                }
                if (expression is MethodCallExpression methodCallExpression1)
                    expression = methodCallExpression1.Object;
                if (right is MethodCallExpression methodCallExpression2)
                    right = methodCallExpression2.Object;
                if (expression is UnaryExpression unaryExpression)
                    expression = unaryExpression.Operand;
                MemberInfo member = ExpressionExtensions.GetInnerMember((MemberExpression)expression).Member;
                this.Filters[member.Name + (TypeHelper.Is<BaseEntity>(ReflectionHelper.GetMemberType(member), true) ? "Id" : string.Empty)] = (comparisonOperator, ExpressionExtensions.GetValue<object>(right));
            }
        }
    }

    private static class ArgsIdxs
    {
        public const int Skip = 0;
        public const int Take = 1;
        public const int Deleted = 2;
        public const int OrderBy = 3;
        public const int OrderByDesc = 4;
        public const int TotalCount = 5;
    }
}
