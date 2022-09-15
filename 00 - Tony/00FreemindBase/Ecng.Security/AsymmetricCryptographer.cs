// Decompiled with JetBrains decompiler
// Type: Ecng.Security.AsymmetricCryptographer
// Assembly: Ecng.Security, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC731BA6-0108-4E2D-8E5E-F8573AC505F7
// Assembly location: T:\00-FreemindTrader\packages\ecng.security\1.0.118\lib\netstandard2.0\Ecng.Security.dll

using Ecng.Common;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
using System;
using System.Security.Cryptography;

namespace Ecng.Security
{
  public class AsymmetricCryptographer : Disposable
  {
    private readonly AsymmetricCryptographer.AsymmetricAlgorithmWrapper _encryptor;
    private readonly AsymmetricCryptographer.AsymmetricAlgorithmWrapper _decryptor;

    public AsymmetricCryptographer(
      Type algorithmType,
      ProtectedKey publicKey,
      ProtectedKey privateKey)
      : this(publicKey == null ? (AsymmetricCryptographer.AsymmetricAlgorithmWrapper) null : new AsymmetricCryptographer.AsymmetricAlgorithmWrapper(algorithmType, publicKey), privateKey == null ? (AsymmetricCryptographer.AsymmetricAlgorithmWrapper) null : new AsymmetricCryptographer.AsymmetricAlgorithmWrapper(algorithmType, privateKey))
    {
    }

    public AsymmetricCryptographer(Type algorithmType, byte[] publicKey)
      : this(publicKey == null ? (AsymmetricCryptographer.AsymmetricAlgorithmWrapper) null : new AsymmetricCryptographer.AsymmetricAlgorithmWrapper(algorithmType, publicKey), (AsymmetricCryptographer.AsymmetricAlgorithmWrapper) null)
    {
    }

    protected AsymmetricCryptographer(AsymmetricAlgorithm encryptor, AsymmetricAlgorithm decryptor)
      : this(new AsymmetricCryptographer.AsymmetricAlgorithmWrapper(encryptor), new AsymmetricCryptographer.AsymmetricAlgorithmWrapper(decryptor))
    {
    }

    private AsymmetricCryptographer(
      AsymmetricCryptographer.AsymmetricAlgorithmWrapper encryptor,
      AsymmetricCryptographer.AsymmetricAlgorithmWrapper decryptor)
    {
      if (encryptor == null && decryptor == null)
        throw new ArgumentException();
      this._encryptor = encryptor;
      this._decryptor = decryptor;
    }

    public static AsymmetricCryptographer CreateFromPublicKey(
      Type algorithmType,
      ProtectedKey publicKey)
    {
      return new AsymmetricCryptographer(new AsymmetricCryptographer.AsymmetricAlgorithmWrapper(algorithmType, publicKey), (AsymmetricCryptographer.AsymmetricAlgorithmWrapper) null);
    }

    public static AsymmetricCryptographer CreateFromPrivateKey(
      Type algorithmType,
      ProtectedKey privateKey)
    {
      return new AsymmetricCryptographer((AsymmetricCryptographer.AsymmetricAlgorithmWrapper) null, new AsymmetricCryptographer.AsymmetricAlgorithmWrapper(algorithmType, privateKey));
    }

    public byte[] Encrypt(byte[] plainText)
    {
      if (this._encryptor == null)
        throw new InvalidOperationException();
      return this._encryptor.Encrypt(plainText);
    }

    public byte[] Decrypt(byte[] encryptedText)
    {
      if (this._decryptor == null)
        throw new InvalidOperationException();
      return this._decryptor.Decrypt(encryptedText);
    }

    protected override void DisposeManaged()
    {
      if ((Equatable<Wrapper<AsymmetricAlgorithm>>) this._encryptor != (Wrapper<AsymmetricAlgorithm>) null)
        this._encryptor.Dispose();
      if ((Equatable<Wrapper<AsymmetricAlgorithm>>) this._decryptor != (Wrapper<AsymmetricAlgorithm>) null)
        this._decryptor.Dispose();
      base.DisposeManaged();
    }

    public byte[] CreateSignature(byte[] data)
    {
      if (this._decryptor == null)
        throw new InvalidOperationException();
      return this._decryptor.CreateSignature(data);
    }

    public bool VerifySignature(byte[] data, byte[] signature)
    {
      if (this._encryptor == null)
        throw new InvalidOperationException();
      return this._encryptor.VerifySignature(data, signature);
    }

    private sealed class AsymmetricAlgorithmWrapper : Wrapper<AsymmetricAlgorithm>
    {
      public AsymmetricAlgorithmWrapper(Type algorithmType, ProtectedKey key)
        : this(AsymmetricCryptographer.AsymmetricAlgorithmWrapper.CreateAlgo(algorithmType, key))
      {
      }

      public AsymmetricAlgorithmWrapper(Type algorithmType, byte[] key)
        : this(AsymmetricCryptographer.AsymmetricAlgorithmWrapper.CreateAlgo(algorithmType, key))
      {
      }

      public AsymmetricAlgorithmWrapper(AsymmetricAlgorithm value)
        : base(value)
      {
      }

      private static AsymmetricAlgorithm CreateAlgo(
        Type algorithmType,
        byte[] key)
      {
        if ((object) algorithmType == null)
          throw new ArgumentNullException(nameof (algorithmType));
        if (!algorithmType.Is<AsymmetricAlgorithm>())
          throw new ArgumentException(nameof (algorithmType));
        if (key == null)
          throw new ArgumentNullException(nameof (key));
        AsymmetricAlgorithm instance = algorithmType.CreateInstance<AsymmetricAlgorithm>();
        if (algorithmType.Is<RSACryptoServiceProvider>())
          ((RSA) instance).ImportParameters(key.ToRsa());
        return instance;
      }

      private static AsymmetricAlgorithm CreateAlgo(
        Type algorithmType,
        ProtectedKey key)
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key));
        return AsymmetricCryptographer.AsymmetricAlgorithmWrapper.CreateAlgo(algorithmType, key.DecryptedKey);
      }

      public byte[] Encrypt(byte[] plainText)
      {
        RSACryptoServiceProvider cryptoServiceProvider = this.Value as RSACryptoServiceProvider;
        if (cryptoServiceProvider != null)
          return cryptoServiceProvider.Encrypt(plainText, false);
        throw new NotImplementedException();
      }

      public byte[] Decrypt(byte[] encryptedText)
      {
        RSACryptoServiceProvider cryptoServiceProvider = this.Value as RSACryptoServiceProvider;
        if (cryptoServiceProvider != null)
          return cryptoServiceProvider.Decrypt(encryptedText, false);
        throw new NotImplementedException();
      }

      public byte[] CreateSignature(byte[] data)
      {
        RSACryptoServiceProvider cryptoServiceProvider1 = this.Value as RSACryptoServiceProvider;
        if (cryptoServiceProvider1 != null)
        {
          using (SHA1CryptoServiceProvider cryptoServiceProvider2 = new SHA1CryptoServiceProvider())
            return cryptoServiceProvider1.SignData(data, (object) cryptoServiceProvider2);
        }
        else
        {
          DSACryptoServiceProvider cryptoServiceProvider2 = this.Value as DSACryptoServiceProvider;
          if (cryptoServiceProvider2 != null)
            return cryptoServiceProvider2.SignData(data);
          throw new NotSupportedException();
        }
      }

      public bool VerifySignature(byte[] data, byte[] signature)
      {
        RSACryptoServiceProvider cryptoServiceProvider1 = this.Value as RSACryptoServiceProvider;
        if (cryptoServiceProvider1 != null)
        {
          using (SHA1CryptoServiceProvider cryptoServiceProvider2 = new SHA1CryptoServiceProvider())
            return cryptoServiceProvider1.VerifyData(data, (object) cryptoServiceProvider2, signature);
        }
        else
        {
          DSACryptoServiceProvider cryptoServiceProvider2 = this.Value as DSACryptoServiceProvider;
          if (cryptoServiceProvider2 != null)
            return cryptoServiceProvider2.VerifySignature(data, signature);
          throw new NotSupportedException();
        }
      }

      public override Wrapper<AsymmetricAlgorithm> Clone()
      {
        throw new NotSupportedException();
      }

      protected override void DisposeManaged()
      {
        this.Value.Clear();
        base.DisposeManaged();
      }
    }
  }
}
