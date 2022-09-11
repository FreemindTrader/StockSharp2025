// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.Errors
// Assembly: StockSharp.Web.Api.Interfaces, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8DA76177-7390-4832-AC1B-2142F78BAD4C
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Interfaces.dll

using Ecng.Common;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Net;

namespace StockSharp.Web.Api.Interfaces
{
    public static class Errors
    {
        public static ExpiredException Expired(string entityName, object entityId)
        {
            return new ExpiredException(string.Format("{0} {1} expired.", entityName, entityId));
        }

        public static KeyNotFoundException NotExist(
          string entityName,
          object entityId)
        {
            return new KeyNotFoundException(string.Format("{0} {1} doesn't exit.", entityName, entityId));
        }

        public static KeyNotFoundException Deleted<TEntity>(this TEntity entity) where TEntity : BaseEntity
        {
            return Deleted( typeof(TEntity).Name, entity.Id);
        }

        public static KeyNotFoundException Deleted(
          string entityName,
          object entityId)
        {
            return new KeyNotFoundException(string.Format("{0} {1} deleted.", entityName, entityId));
        }

        public static KeyNotFoundException Deleted(string entityName, long entityId)
        {
            return new KeyNotFoundException(string.Format("{0} {1} deleted.", entityName, entityId));
        }

        public static ForbiddenException NotAccess<TEntity>(
          this TEntity entity,
          Client client)
          where TEntity : BaseEntity
        {
            return entity.NotAccess( client.CheckOnNull( "value").Id);
        }

        public static ForbiddenException NotAccess<TEntity>(
          this TEntity entity,
          long clientId)
          where TEntity : BaseEntity
        {
            return NotAccess( typeof(TEntity).Name, entity.Id, clientId);
        }

        public static ForbiddenException NotAccess(
          string entityName,
          object entityId,
          long clientId)
        {
            return new ForbiddenException(string.Format("Client {0} doesn't have access to {1} {2}.", clientId, entityName, entityId));
        }

        public static ForbiddenException ClientMustBe(long clientId, string roleName)
        {
            return new ForbiddenException(string.Format("Client {0} must be {1}.", clientId, roleName));
        }

        public static UnauthorizedAccessException ClientNotApproved(
          long clientId)
        {
            return new UnauthorizedAccessException(string.Format("Client {0} doesn't approved.", clientId));
        }

        public static UnauthorizedAccessException ClientNotFound()
        {
            return new UnauthorizedAccessException("Client not found.");
        }

        public static DuplicateException AlreadyExist(
          string entityName,
          object entityId)
        {
            return new DuplicateException(string.Format("{0} {1} already exist.", entityName, entityId));
        }

        public static UnauthorizedAccessException ClientLocked(long clientId)
        {
            return new UnauthorizedAccessException(string.Format("Client {0} is locked.", clientId));
        }

        public static UnauthorizedAccessException PasswordForEmailIsEmpty(
          string email)
        {
            return new UnauthorizedAccessException("Password is empty for passed email " + email + ".");
        }

        public static InvalidOperationException UnknownEmail(string email)
        {
            return new InvalidOperationException("Unknown email '" + email + "'.");
        }

        public static InvalidOperationException HasSuspicious(long clientId)
        {
            return new InvalidOperationException(string.Format("Has suspicios for client {0}.", clientId));
        }

        public static UnauthorizedAccessException PasswordForEmailIsIncorrect(
          string email)
        {
            return new UnauthorizedAccessException("Email " + email + " provided with wrong password.");
        }

        public static InvalidOperationException SessionNotActive(long sessionId)
        {
            return new InvalidOperationException(string.Format("Session {0} not active and cannot be modified.", sessionId));
        }

        public static UnauthorizedAccessException IsNotSafeAddress(
          this IPAddress address)
        {
            return new UnauthorizedAccessException(string.Format("Address {0} is not safe and operation cannot be processed.", address));
        }

        public static UnauthorizedAccessException ClientCannotPublishPackage(
          long clientId,
          string packageId)
        {
            return new UnauthorizedAccessException(string.Format("Client {0} cannot publish {1} package.", clientId, packageId));
        }

        public static InvalidOperationException NewPasswordIsEmpty(
          long clientId)
        {
            return new InvalidOperationException(string.Format("Client {0} provided empty new password.", clientId));
        }

        public static InvalidOperationException NewPasswordSameOld(
          long clientId)
        {
            return new InvalidOperationException(string.Format("Client {0} provided new password same old.", clientId));
        }

        public static InvalidOperationException FileNotImage(long fileId)
        {
            return new InvalidOperationException(string.Format("File {0} isn't image.", fileId));
        }

        public static InvalidOperationException FileNotRoot(long fileId)
        {
            return new InvalidOperationException(string.Format("File {0} is not root.", fileId));
        }

        public static InvalidOperationException NotImage(this File file )
        {
            return FileNotImage( file.Id);
        }

        public static InvalidOperationException NotRoot(this File file )
        {
            return FileNotRoot( file.Id);
        }

        public static InvalidOperationException UnknownCompression(
          this Compressions compressions)
        {
            return new InvalidOperationException(string.Format("Unknown compression '{0}'.", compressions));
        }

        public static InvalidOperationException ClientCannotUpload(
          long clientId)
        {
            return new InvalidOperationException(string.Format("Client {0} cannot upload.", clientId));
        }

        public static InvalidOperationException ClientCannotBlockSelf(
          long clientId)
        {
            return new InvalidOperationException(string.Format("Client {0} cannot block him self.", clientId));
        }

        public static InvalidOperationException ExecutorCannotBeSelected(
          long messageId,
          long clientId)
        {
            return new InvalidOperationException(string.Format("Executor {0} cannot be selected by {1}.", messageId, clientId));
        }

        public static InvalidOperationException ScoreIsLow(this float score)
        {
            throw new Exception(string.Format("Score {0}", score));
        }

        public static InvalidOperationException DomainsNotPresent(
          this string entityName,
          object entityId)
        {
            throw new Exception(string.Format("Domains for {0}({1}) not present.", entityName, entityId));
        }

        public static InvalidOperationException DomainsNotPresent<TEntity>(
          this TEntity entity)
          where TEntity : BaseEntity
        {
            throw new Exception(string.Format("Domains for {0}({1}) not present.", typeof(TEntity).Name, entity.CheckOnNull("value").Id));
        }
    }
}
