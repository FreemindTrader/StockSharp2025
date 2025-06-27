// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.Linq.ApiClientDataQuery`1
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Linq;
using Ecng.Reflection;
using StockSharp.Web.DomainModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client.Linq
{
    internal class ApiClientDataQuery<T> : IOrderedQueryable<T>, IEnumerable<T>, IEnumerable, IOrderedQueryable, IQueryable, IQueryable<T>, IQueryProvider
    where T : BaseEntity
    {
        private readonly object _service;
        private readonly long _maxTake;
        private readonly string _methodName;
        private readonly Expression _expression;

        public ApiClientDataQuery( object service, string methodName, long maxTake )
        {
            object obj = service;
            if ( obj == null )
                throw new ArgumentNullException( nameof( service ) );
            this._service = obj;
            if ( StringHelper.IsEmpty( methodName ) )
                throw new ArgumentNullException( nameof( methodName ) );
            this._methodName = methodName;
            this._maxTake = maxTake;
            this._expression = ( Expression ) Expression.Constant( ( object ) this );
        }

        public ApiClientDataQuery(
          object service,
          string methodName,
          long maxTake,
          Expression expression )
          : this( service, methodName, maxTake )
        {
            Expression expression1 = expression;
            if ( expression1 == null )
                throw new ArgumentNullException( nameof( expression ) );
            this._expression = expression1;
        }

        Type IQueryable.ElementType
        {
            get
            {
                return typeof( T );
            }
        }

        IQueryProvider IQueryable.Provider
        {
            get
            {
                return ( IQueryProvider ) this;
            }
        }

        Expression IQueryable.Expression
        {
            get
            {
                return this._expression;
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return Enumerable.Cast<T>( ( ( IQueryProvider ) this ).Execute<T [ ]>( this._expression ) ).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ( IEnumerator ) ( ( IEnumerable<T> ) this ).GetEnumerator();
        }

        IQueryable IQueryProvider.CreateQuery( Expression expression )
        {
            throw new NotSupportedException();
        }

        IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(
          Expression expression )
        {
            return ( IQueryable<TElement> ) TypeHelper.CreateInstance<IQueryable<TElement>>( TypeHelper.Make( typeof( ApiClientDataQuery<> ), new Type [1]
            {
        typeof (TElement)
            } ), new object [4]
            {
        this._service,
         this._methodName,
         this._maxTake,
         expression
            } );
        }

        object IQueryProvider.Execute( Expression expression )
        {
            throw new NotSupportedException();
        }

        TResult IQueryProvider.Execute<TResult>( Expression expression )
        {
            ApiClientDataQuery<T>.Visitor visitor = new ApiClientDataQuery<T>.Visitor();
            visitor.Visit( expression );
            MethodInfo member = (MethodInfo) ReflectionHelper.GetMember<MethodInfo>(this._service.GetType(), this._methodName, Array.Empty<Type>());
            if ( ( object ) member == null )
                throw new InvalidOperationException( this._service.GetType().AssemblyQualifiedName );
            ParameterInfo[] parameters1 = member.GetParameters();
            object[] parameters2 = new object[parameters1.Length];
            using ( Dictionary<string, ValueTuple<ComparisonOperator, object>>.Enumerator enumerator = visitor.Filters.GetEnumerator() )
            {
                while ( enumerator.MoveNext() )
                {
                    KeyValuePair<string, ValueTuple<ComparisonOperator, object>> current = enumerator.Current;
                    string name = current.Key;
                    object id = current.Value.Item2;
                    BaseEntity baseEntity = id as BaseEntity;
                    if ( baseEntity != null )
                        id = ( object ) baseEntity.Id;
                    int index1 = CollectionHelper.IndexOf<ParameterInfo>( parameters1,  (p => StringHelper.EqualsIgnoreCase(p.Name, name)));
                    if ( index1 != -1 )
                    {
                        ParameterInfo parameterInfo = parameters1[index1];
                        string str = id as string;
                        parameters2 [index1] = str == null || !( parameterInfo.ParameterType == typeof( DateTime ) ) && !( parameterInfo.ParameterType == typeof( DateTime? ) ) ? Converter.To( id, parameterInfo.ParameterType ) : ( object ) TimeHelper.ToDateTime( str, "yyyyMMdd", ( CultureInfo ) null );
                        int index2 = CollectionHelper.IndexOf<ParameterInfo>( parameters1,  (p => StringHelper.EqualsIgnoreCase(p.Name, name + "Like")));
                        if ( index2 != -1 )
                            parameters2 [index2] = ( object ) current.Value.Item1;
                    }
                }
            }
            if ( ( object ) visitor.OrderBy != null )
            {
                parameters2 [3] = ( object ) visitor.OrderBy.Name;
                if ( visitor.OrderByDesc )
                    parameters2 [4] = ( object ) true;
            }
            if ( visitor.IsCount )
            {
                parameters2 [1] = ( object ) 0L;
            }
            else
            {
                parameters2 [0] = ( object ) visitor.Skip;
                parameters2 [1] = ( object ) ( visitor.Take ?? this._maxTake );
                if ( visitor.IsSet )
                    parameters2 [5] = ( object ) true;
            }
            parameters2 [parameters2.Length - 1] = ( object ) visitor.CancellationToken;
            Task task = (Task) member.Invoke(this._service, parameters2);
            if ( visitor.IsSet )
                return Converter.To<TResult>( ( object ) task );
            if ( ( object ) ReflectionHelper.GetGenericType( typeof( TResult ), typeof( ValueTask<> ) ) != null )
            {
                if ( visitor.IsCount )
                    return Converter.To<TResult>( ( object ) ( ValueTask<long> ) AsyncHelper.AsValueTask<long>(  task.ContinueWith<long>( ( Func<Task, long> ) ( t => ( ( IBaseEntitySet ) AsyncHelper.GetResult<IBaseEntitySet>( t ) ).Count ), TaskContinuationOptions.ExecuteSynchronously ) ) );
                return Converter.To<TResult>( ReflectionHelper.Make( ( MethodInfo ) ReflectionHelper.GetMember<MethodInfo>( this.GetType(), "GetTask", Array.Empty<Type>() ), new Type [1]
                {
          ReflectionHelper.GetItemType(((IEnumerable<Type>) typeof (TResult).GetGenericArguments()).First<Type>())
                } ).Invoke( ( object ) null, ( object [ ] ) new Task [1]
                {
          task
                } ) );
            }
            IBaseEntitySet result = (IBaseEntitySet) AsyncHelper.GetResult<IBaseEntitySet>(task);
            if ( !visitor.IsCount )
                return Converter.To<TResult>( ( object ) result.Items );
            return Converter.To<TResult>( ( object ) result.Count );
        }

        private static ValueTask<TItem [ ]> GetTask<TItem>( Task task )
        {
            return ( ValueTask<TItem [ ]> ) Converter.To<ValueTask<TItem [ ]>>( ( object ) ( ValueTask<TItem [ ]> ) AsyncHelper.AsValueTask<TItem [ ]>(  ( ( Task ) TypeHelper.CheckOnNull<Task>(  task, nameof( task ) ) ).ContinueWith<TItem [ ]>( ( Func<Task, TItem [ ]> ) ( t => ( TItem [ ] ) ( ( IBaseEntitySet ) AsyncHelper.GetResult<IBaseEntitySet>( t ) ).Items ), TaskContinuationOptions.ExecuteSynchronously ) ) );
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

            
            public Dictionary<string, (ComparisonOperator op, object val)> Filters { get; } = new Dictionary<string, ValueTuple<ComparisonOperator, object>>();

            protected override Expression VisitMethodCall( MethodCallExpression node )
            {
                if ( node.Method.Name == "Count" )
                    this.IsCount = true;
                else if ( node.Method.Name == "ToEntitySetAsync" )
                {
                    this.IsSet = true;
                    this.CancellationToken = ( CancellationToken ) ExpressionExtensions.GetValue<CancellationToken>( node.Arguments [1] );
                }
                else if ( node.Method.Name == "CountAsync" )
                {
                    this.IsCount = true;
                    this.CancellationToken = ( CancellationToken ) ExpressionExtensions.GetValue<CancellationToken>( node.Arguments [1] );
                }
                else if ( node.Method.Name == "ToArrayAsync" )
                    this.CancellationToken = ( CancellationToken ) ExpressionExtensions.GetValue<CancellationToken>( node.Arguments [1] );
                else if ( node.Method.Name == "Skip" )
                    this.Skip = ( long ) ExpressionExtensions.GetValue<long>( node.Arguments [1] );
                else if ( node.Method.Name == "Take" )
                    this.Take = new long?( ( long ) ExpressionExtensions.GetValue<long>( node.Arguments [1] ) );
                else if ( node.Method.Name == "OrderBy" || node.Method.Name == "OrderByDescending" )
                {
                    this.OrderBy = ( ( MemberExpression ) ( ( LambdaExpression ) ExpressionExtensions.StripQuotes( node.Arguments [1] ) ).Body ).Member;
                    if ( node.Method.Name == "OrderByDescending" )
                        this.OrderByDesc = true;
                }
                else if ( node.Method.Name == "Where" )
                {
                    Expression body = ((LambdaExpression) ExpressionExtensions.StripQuotes(node.Arguments[1])).Body;
                    BinaryExpression be = body as BinaryExpression;
                    if ( be != null )
                    {
                        if ( be.NodeType == ExpressionType.AndAlso )
                        {
                            ProcessBinary( ( BinaryExpression ) be.Left );
                            ProcessBinary( ( BinaryExpression ) be.Right );
                        }
                        else
                            ProcessBinary( be );
                    }
                    else
                    {
                        MethodCallExpression methodCallExpression = (MethodCallExpression) body;
                        if ( methodCallExpression.Method.Name != "By" )
                            throw new InvalidOperationException( methodCallExpression.Method.Name );
                        this.Filters [( string ) ExpressionExtensions.GetValue<string>( methodCallExpression.Arguments [1] )] = new ValueTuple<ComparisonOperator, object>( ( ComparisonOperator ) 0, ( object ) ( string ) ExpressionExtensions.GetValue<string>( methodCallExpression.Arguments [2] ) );
                    }
                }
                return base.VisitMethodCall( node );

                void ProcessBinary( BinaryExpression be )
                {
                    ComparisonOperator comparisonOperator = ExpressionExtensions.ToOperator(be.NodeType);
                    Expression expression = be.Left;
                    Expression right = be.Right;
                    if ( expression is BinaryExpression )
                    {
                        be = ( BinaryExpression ) right;
                        expression = be.Left;
                        right = be.Right;
                    }
                    MethodCallExpression methodCallExpression1 = expression as MethodCallExpression;
                    if ( methodCallExpression1 != null )
                        expression = methodCallExpression1.Object;
                    MethodCallExpression methodCallExpression2 = right as MethodCallExpression;
                    if ( methodCallExpression2 != null )
                        right = methodCallExpression2.Object;
                    UnaryExpression unaryExpression = expression as UnaryExpression;
                    if ( unaryExpression != null )
                        expression = unaryExpression.Operand;
                    MemberInfo member = ExpressionExtensions.GetInnerMember((MemberExpression) expression).Member;
                    this.Filters [member.Name + ( TypeHelper.Is<BaseEntity>( ReflectionHelper.GetMemberType( member ), true ) ? "Id" : string.Empty )] = new ValueTuple<ComparisonOperator, object>( comparisonOperator, ( object ) ExpressionExtensions.GetValue<object>( right ) );
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
}
