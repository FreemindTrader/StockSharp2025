// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.Errors
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9EB02CA6-0DCD-4F94-B6F3-8DF6ED492679
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Interfaces.dll

using System;
using System.Collections.Generic;
using System.Net;
using Ecng.Common;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Interfaces;

public static class Errors
{
    public static ExpiredException Expired(string entityName, object entityId)
    {
        return new ExpiredException($"{entityName} {entityId} expired.");
    }

    public static KeyNotFoundException NotExist(string entityName, object entityId)
    {
        return new KeyNotFoundException($"{entityName} {entityId} doesn't exit.");
    }

    public static KeyNotFoundException Deleted<TEntity>(this TEntity entity) where TEntity : BaseEntity
    {
        return Errors.Deleted(typeof(TEntity).Name, entity.Id);
    }

    public static KeyNotFoundException Deleted(string entityName, object entityId)
    {
        return new KeyNotFoundException($"{entityName} {entityId} deleted.");
    }

    public static KeyNotFoundException Deleted(string entityName, long entityId)
    {
        return new KeyNotFoundException($"{entityName} {entityId} deleted.");
    }

    public static ForbiddenException NotAccess<TEntity>(this TEntity entity, Client client) where TEntity : BaseEntity
    {
        return entity.NotAccess<TEntity>(TypeHelper.CheckOnNull<Client>(client, nameof(client)).Id);
    }

    public static ForbiddenException NotAccess<TEntity>(this TEntity entity, long clientId) where TEntity : BaseEntity
    {
        return Errors.NotAccess(typeof(TEntity).Name, (object)entity.Id, clientId);
    }

    public static ForbiddenException NotAccess(string entityName, object entityId, long clientId)
    {
        return new ForbiddenException($"Client {clientId} doesn't have access to {entityName} {entityId}.");
    }

    public static ForbiddenException ClientMustBe(long clientId, string roleName)
    {
        return new ForbiddenException($"Client {clientId} must be {roleName}.");
    }

    public static UnauthorizedAccessException ClientNotApproved(long clientId)
    {
        return new UnauthorizedAccessException($"Client {clientId} doesn't approved.");
    }

    public static UnauthorizedAccessException ClientNotFound()
    {
        return new UnauthorizedAccessException("Client not found.");
    }

    public static DuplicateException AlreadyExist(string entityName, object entityId)
    {
        return new DuplicateException($"{entityName} {entityId} already exist.");
    }

    public static UnauthorizedAccessException MustBeSafeAddress(this IPAddress addr)
    {
        return new UnauthorizedAccessException($"Address {addr} is not in safe list.");
    }

    public static UnauthorizedAccessException ClientLocked(object clientId)
    {
        return new UnauthorizedAccessException($"Client {clientId} is locked.");
    }

    public static UnauthorizedAccessException PasswordForEmailIsEmpty(string email)
    {
        return new UnauthorizedAccessException($"Password is empty for passed email {email}.");
    }

    public static InvalidOperationException UnknownEmail(string email)
    {
        return new InvalidOperationException($"Unknown email '{email}'.");
    }

    public static InvalidOperationException HasSuspicious(long clientId)
    {
        return new InvalidOperationException($"Has suspicios for client {clientId}.");
    }

    public static UnauthorizedAccessException PasswordForEmailIsIncorrect(string email)
    {
        return new UnauthorizedAccessException($"Email {email} provided with wrong password.");
    }

    public static InvalidOperationException SessionNotActive(long sessionId)
    {
        return new InvalidOperationException($"Session {sessionId} not active and cannot be modified.");
    }

    public static UnauthorizedAccessException IsNotSafeAddress(this IPAddress address)
    {
        return new UnauthorizedAccessException($"Address {address} is not safe and operation cannot be processed.");
    }

    public static UnauthorizedAccessException ClientCannotPublishPackage(
      long clientId,
      string packageId)
    {
        return new UnauthorizedAccessException($"Client {clientId} cannot publish {packageId} package.");
    }

    public static InvalidOperationException NewPasswordIsEmpty(long clientId)
    {
        return new InvalidOperationException($"Client {clientId} provided empty new password.");
    }

    public static InvalidOperationException NewPasswordSameOld(long clientId)
    {
        return new InvalidOperationException($"Client {clientId} provided new password same old.");
    }

    public static InvalidOperationException FileNotImage(long fileId)
    {
        return new InvalidOperationException($"File {fileId} isn't image.");
    }

    public static InvalidOperationException FileNotRoot(long fileId)
    {
        return new InvalidOperationException($"File {fileId} is not root.");
    }

    public static InvalidOperationException NotImage(this StockSharp.Web.DomainModel.File file)
    {
        return Errors.FileNotImage(file.Id);
    }

    public static InvalidOperationException NotRoot(this StockSharp.Web.DomainModel.File file)
    {
        return Errors.FileNotRoot(file.Id);
    }

    public static InvalidOperationException UnknownCompression(this Compressions compressions)
    {
        return new InvalidOperationException($"Unknown compression '{compressions}'.");
    }

    public static InvalidOperationException ClientCannotUpload(long clientId)
    {
        return new InvalidOperationException($"Client {clientId} cannot upload.");
    }

    public static InvalidOperationException ClientCannotBlockSelf(long clientId)
    {
        return new InvalidOperationException($"Client {clientId} cannot block him self.");
    }

    public static InvalidOperationException ExecutorCannotBeSelected(long messageId, long clientId)
    {
        return new InvalidOperationException($"Executor {messageId} cannot be selected by {clientId}.");
    }

    public static InvalidOperationException ScoreIsLow(this float score)
    {
        throw new Exception($"Score {score}");
    }

    public static InvalidOperationException DomainsNotPresent(this string entityName, object entityId)
    {
        throw new Exception($"Domains for {entityName}({entityId}) not present.");
    }

    public static InvalidOperationException DomainsNotPresent<TEntity>(this TEntity entity) where TEntity : BaseEntity
    {
        throw new Exception($"Domains for {typeof(TEntity).Name}({TypeHelper.CheckOnNull<TEntity>(entity, nameof(entity)).Id}) not present.");
    }
}
