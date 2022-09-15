// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.ObservableDictionary`2
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Ecng.ComponentModel
{
  [Serializable]
  public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, ISerializable, IDeserializationCallback, INotifyCollectionChanged, INotifyPropertyChanged
  {
    private readonly Dictionary<TKey, TValue> _dictionaryCache = new Dictionary<TKey, TValue>();
    private readonly ObservableDictionary<TKey, TValue>.KeyedDictionaryEntryCollection _keyedEntryCollection;
    private int _countCache;
    private int _dictionaryCacheVersion;
    private int _version;
    [NonSerialized]
    private readonly SerializationInfo _siInfo;

    public ObservableDictionary()
    {
      this._keyedEntryCollection = new ObservableDictionary<TKey, TValue>.KeyedDictionaryEntryCollection();
    }

    public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
    {
      this._keyedEntryCollection = new ObservableDictionary<TKey, TValue>.KeyedDictionaryEntryCollection();
      foreach (KeyValuePair<TKey, TValue> keyValuePair in (IEnumerable<KeyValuePair<TKey, TValue>>) dictionary)
        this.DoAddEntry(keyValuePair.Key, keyValuePair.Value);
    }

    public ObservableDictionary(IEqualityComparer<TKey> comparer)
    {
      this._keyedEntryCollection = new ObservableDictionary<TKey, TValue>.KeyedDictionaryEntryCollection(comparer);
    }

    public ObservableDictionary(
      IDictionary<TKey, TValue> dictionary,
      IEqualityComparer<TKey> comparer)
    {
      this._keyedEntryCollection = new ObservableDictionary<TKey, TValue>.KeyedDictionaryEntryCollection(comparer);
      foreach (KeyValuePair<TKey, TValue> keyValuePair in (IEnumerable<KeyValuePair<TKey, TValue>>) dictionary)
        this.DoAddEntry(keyValuePair.Key, keyValuePair.Value);
    }

    protected ObservableDictionary(SerializationInfo info, StreamingContext context)
    {
      this._siInfo = info;
    }

    public IEqualityComparer<TKey> Comparer
    {
      get
      {
        return this._keyedEntryCollection.Comparer;
      }
    }

    public int Count
    {
      get
      {
        return this._keyedEntryCollection.Count;
      }
    }

    public Dictionary<TKey, TValue>.KeyCollection Keys
    {
      get
      {
        return this.TrueDictionary.Keys;
      }
    }

    public TValue this[TKey key]
    {
      get
      {
        return (TValue) this._keyedEntryCollection[key].Value;
      }
      set
      {
        this.DoSetEntry(key, value);
      }
    }

    public Dictionary<TKey, TValue>.ValueCollection Values
    {
      get
      {
        return this.TrueDictionary.Values;
      }
    }

    private Dictionary<TKey, TValue> TrueDictionary
    {
      get
      {
        if (this._dictionaryCacheVersion != this._version)
        {
          this._dictionaryCache.Clear();
          foreach (DictionaryEntry keyedEntry in (Collection<DictionaryEntry>) this._keyedEntryCollection)
            this._dictionaryCache.Add((TKey) keyedEntry.Key, (TValue) keyedEntry.Value);
          this._dictionaryCacheVersion = this._version;
        }
        return this._dictionaryCache;
      }
    }

    public void Add(TKey key, TValue value)
    {
      this.DoAddEntry(key, value);
    }

    public void Clear()
    {
      this.DoClearEntries();
    }

    public bool ContainsKey(TKey key)
    {
      return this._keyedEntryCollection.Contains(key);
    }

    public bool ContainsValue(TValue value)
    {
      return this.TrueDictionary.ContainsValue(value);
    }

    public IEnumerator GetEnumerator()
    {
      return (IEnumerator) new ObservableDictionary<TKey, TValue>.Enumerator(this, false);
    }

    public bool Remove(TKey key)
    {
      return this.DoRemoveEntry(key);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
      bool flag = this._keyedEntryCollection.Contains(key);
      value = flag ? (TValue) this._keyedEntryCollection[key].Value : default (TValue);
      return flag;
    }

    protected virtual bool AddEntry(TKey key, TValue value)
    {
      this._keyedEntryCollection.Add(new DictionaryEntry((object) key, (object) value));
      return true;
    }

    protected virtual bool ClearEntries()
    {
      int num = this.Count > 0 ? 1 : 0;
      if (num == 0)
        return num != 0;
      this._keyedEntryCollection.Clear();
      return num != 0;
    }

    protected int GetIndexAndEntryForKey(TKey key, out DictionaryEntry entry)
    {
      entry = new DictionaryEntry();
      int num = -1;
      if (this._keyedEntryCollection.Contains(key))
      {
        entry = this._keyedEntryCollection[key];
        num = this._keyedEntryCollection.IndexOf(entry);
      }
      return num;
    }

    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
      NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
      if (collectionChanged == null)
        return;
      collectionChanged((object) this, args);
    }

    protected virtual void OnPropertyChanged(string name)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(name));
    }

    protected virtual bool RemoveEntry(TKey key)
    {
      return this._keyedEntryCollection.Remove(key);
    }

    protected virtual bool SetEntry(TKey key, TValue value)
    {
      bool flag = this._keyedEntryCollection.Contains(key);
      if (flag && value.Equals((object) (TValue) this._keyedEntryCollection[key].Value))
        return false;
      if (flag)
        this._keyedEntryCollection.Remove(key);
      this._keyedEntryCollection.Add(new DictionaryEntry((object) key, (object) value));
      return true;
    }

    private void DoAddEntry(TKey key, TValue value)
    {
      if (!this.AddEntry(key, value))
        return;
      ++this._version;
      DictionaryEntry entry;
      int indexAndEntryForKey = this.GetIndexAndEntryForKey(key, out entry);
      this.FireEntryAddedNotifications(entry, indexAndEntryForKey);
    }

    private void DoClearEntries()
    {
      if (!this.ClearEntries())
        return;
      ++this._version;
      this.FireResetNotifications();
    }

    private bool DoRemoveEntry(TKey key)
    {
      DictionaryEntry entry;
      int indexAndEntryForKey = this.GetIndexAndEntryForKey(key, out entry);
      int num = this.RemoveEntry(key) ? 1 : 0;
      if (num == 0)
        return num != 0;
      ++this._version;
      if (indexAndEntryForKey <= -1)
        return num != 0;
      this.FireEntryRemovedNotifications(entry, indexAndEntryForKey);
      return num != 0;
    }

    private void DoSetEntry(TKey key, TValue value)
    {
      DictionaryEntry entry;
      int indexAndEntryForKey1 = this.GetIndexAndEntryForKey(key, out entry);
      if (!this.SetEntry(key, value))
        return;
      ++this._version;
      if (indexAndEntryForKey1 > -1)
      {
        this.FireEntryRemovedNotifications(entry, indexAndEntryForKey1);
        --this._countCache;
      }
      int indexAndEntryForKey2 = this.GetIndexAndEntryForKey(key, out entry);
      this.FireEntryAddedNotifications(entry, indexAndEntryForKey2);
    }

    private void FireEntryAddedNotifications(DictionaryEntry entry, int index)
    {
      this.FirePropertyChangedNotifications();
      this.OnCollectionChanged(index > -1 ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (object) new KeyValuePair<TKey, TValue>((TKey) entry.Key, (TValue) entry.Value), index) : new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    private void FireEntryRemovedNotifications(DictionaryEntry entry, int index)
    {
      this.FirePropertyChangedNotifications();
      this.OnCollectionChanged(index > -1 ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, (object) new KeyValuePair<TKey, TValue>((TKey) entry.Key, (TValue) entry.Value), index) : new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    private void FirePropertyChangedNotifications()
    {
      if (this.Count == this._countCache)
        return;
      this._countCache = this.Count;
      this.OnPropertyChanged("Count");
      this.OnPropertyChanged("Item[]");
      this.OnPropertyChanged("Keys");
      this.OnPropertyChanged("Values");
    }

    private void FireResetNotifications()
    {
      this.FirePropertyChangedNotifications();
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
    {
      this.DoAddEntry(key, value);
    }

    bool IDictionary<TKey, TValue>.Remove(TKey key)
    {
      return this.DoRemoveEntry(key);
    }

    bool IDictionary<TKey, TValue>.ContainsKey(TKey key)
    {
      return this._keyedEntryCollection.Contains(key);
    }

    bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
    {
      return this.TryGetValue(key, out value);
    }

    ICollection<TKey> IDictionary<TKey, TValue>.Keys
    {
      get
      {
        return (ICollection<TKey>) this.Keys;
      }
    }

    ICollection<TValue> IDictionary<TKey, TValue>.Values
    {
      get
      {
        return (ICollection<TValue>) this.Values;
      }
    }

    TValue IDictionary<TKey, TValue>.this[TKey key]
    {
      get
      {
        return (TValue) this._keyedEntryCollection[key].Value;
      }
      set
      {
        this.DoSetEntry(key, value);
      }
    }

    void IDictionary.Add(object key, object value)
    {
      this.DoAddEntry((TKey) key, (TValue) value);
    }

    void IDictionary.Clear()
    {
      this.DoClearEntries();
    }

    bool IDictionary.Contains(object key)
    {
      return this._keyedEntryCollection.Contains((TKey) key);
    }

    IDictionaryEnumerator IDictionary.GetEnumerator()
    {
      return (IDictionaryEnumerator) new ObservableDictionary<TKey, TValue>.Enumerator(this, true);
    }

    bool IDictionary.IsFixedSize
    {
      get
      {
        return false;
      }
    }

    bool IDictionary.IsReadOnly
    {
      get
      {
        return false;
      }
    }

    object IDictionary.this[object key]
    {
      get
      {
        return this._keyedEntryCollection[(TKey) key].Value;
      }
      set
      {
        this.DoSetEntry((TKey) key, (TValue) value);
      }
    }

    ICollection IDictionary.Keys
    {
      get
      {
        return (ICollection) this.Keys;
      }
    }

    void IDictionary.Remove(object key)
    {
      this.DoRemoveEntry((TKey) key);
    }

    ICollection IDictionary.Values
    {
      get
      {
        return (ICollection) this.Values;
      }
    }

    void ICollection<KeyValuePair<TKey, TValue>>.Add(
      KeyValuePair<TKey, TValue> kvp)
    {
      this.DoAddEntry(kvp.Key, kvp.Value);
    }

    void ICollection<KeyValuePair<TKey, TValue>>.Clear()
    {
      this.DoClearEntries();
    }

    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(
      KeyValuePair<TKey, TValue> kvp)
    {
      return this._keyedEntryCollection.Contains(kvp.Key);
    }

    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(
      KeyValuePair<TKey, TValue>[] array,
      int index)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (index), "CopyTo() failed:  array parameter was null");
      if (index < 0 || index > array.Length)
        throw new ArgumentOutOfRangeException(nameof (index), "CopyTo() failed:  index parameter was outside the bounds of the supplied array");
      if (array.Length - index < this._keyedEntryCollection.Count)
        throw new ArgumentException("CopyTo() failed:  supplied array was too small", nameof (index));
      foreach (DictionaryEntry keyedEntry in (Collection<DictionaryEntry>) this._keyedEntryCollection)
        array[index++] = new KeyValuePair<TKey, TValue>((TKey) keyedEntry.Key, (TValue) keyedEntry.Value);
    }

    int ICollection<KeyValuePair<TKey, TValue>>.Count
    {
      get
      {
        return this._keyedEntryCollection.Count;
      }
    }

    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
    {
      get
      {
        return false;
      }
    }

    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(
      KeyValuePair<TKey, TValue> kvp)
    {
      return this.DoRemoveEntry(kvp.Key);
    }

    void ICollection.CopyTo(Array array, int index)
    {
      ((ICollection) this._keyedEntryCollection).CopyTo(array, index);
    }

    int ICollection.Count
    {
      get
      {
        return this._keyedEntryCollection.Count;
      }
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return ((ICollection) this._keyedEntryCollection).IsSynchronized;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return ((ICollection) this._keyedEntryCollection).SyncRoot;
      }
    }

    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
    {
      return (IEnumerator<KeyValuePair<TKey, TValue>>) new ObservableDictionary<TKey, TValue>.Enumerator(this, false);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      Collection<DictionaryEntry> collection = new Collection<DictionaryEntry>();
      foreach (DictionaryEntry keyedEntry in (Collection<DictionaryEntry>) this._keyedEntryCollection)
        collection.Add(keyedEntry);
      info.AddValue("entries", (object) collection);
    }

    public virtual void OnDeserialization(object sender)
    {
      if (this._siInfo == null)
        return;
      foreach (DictionaryEntry dictionaryEntry in (Collection<DictionaryEntry>) this._siInfo.GetValue("entries", typeof (Collection<DictionaryEntry>)))
        this.AddEntry((TKey) dictionaryEntry.Key, (TValue) dictionaryEntry.Value);
    }

    event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
    {
      add
      {
        this.CollectionChanged += value;
      }
      remove
      {
        this.CollectionChanged -= value;
      }
    }

    protected virtual event NotifyCollectionChangedEventHandler CollectionChanged;

    event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
    {
      add
      {
        this.PropertyChanged += value;
      }
      remove
      {
        this.PropertyChanged -= value;
      }
    }

    protected virtual event PropertyChangedEventHandler PropertyChanged;

    protected class KeyedDictionaryEntryCollection : KeyedCollection<TKey, DictionaryEntry>
    {
      public KeyedDictionaryEntryCollection()
      {
      }

      public KeyedDictionaryEntryCollection(IEqualityComparer<TKey> comparer)
        : base(comparer)
      {
      }

      protected override TKey GetKeyForItem(DictionaryEntry entry)
      {
        return (TKey) entry.Key;
      }
    }

    [Serializable]
    public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable, IDictionaryEnumerator
    {
      private readonly ObservableDictionary<TKey, TValue> _dictionary;
      private readonly int _version;
      private int _index;
      private KeyValuePair<TKey, TValue> _current;
      private readonly bool _isDictionaryEntryEnumerator;

      internal Enumerator(
        ObservableDictionary<TKey, TValue> dictionary,
        bool isDictionaryEntryEnumerator)
      {
        this._dictionary = dictionary;
        this._version = dictionary._version;
        this._index = -1;
        this._isDictionaryEntryEnumerator = isDictionaryEntryEnumerator;
        this._current = new KeyValuePair<TKey, TValue>();
      }

      public KeyValuePair<TKey, TValue> Current
      {
        get
        {
          this.ValidateCurrent();
          return this._current;
        }
      }

      public void Dispose()
      {
      }

      public bool MoveNext()
      {
        this.ValidateVersion();
        ++this._index;
        if (this._index < this._dictionary._keyedEntryCollection.Count)
        {
          this._current = new KeyValuePair<TKey, TValue>((TKey) this._dictionary._keyedEntryCollection[this._index].Key, (TValue) this._dictionary._keyedEntryCollection[this._index].Value);
          return true;
        }
        this._index = -2;
        this._current = new KeyValuePair<TKey, TValue>();
        return false;
      }

      private void ValidateCurrent()
      {
        if (this._index == -1)
          throw new InvalidOperationException("The enumerator has not been started.");
        if (this._index == -2)
          throw new InvalidOperationException("The enumerator has reached the end of the collection.");
      }

      private void ValidateVersion()
      {
        if (this._version != this._dictionary._version)
          throw new InvalidOperationException("The enumerator is not valid because the dictionary changed.");
      }

      object IEnumerator.Current
      {
        get
        {
          this.ValidateCurrent();
          if (this._isDictionaryEntryEnumerator)
            return (object) new DictionaryEntry((object) this._current.Key, (object) this._current.Value);
          return (object) new KeyValuePair<TKey, TValue>(this._current.Key, this._current.Value);
        }
      }

      void IEnumerator.Reset()
      {
        this.ValidateVersion();
        this._index = -1;
        this._current = new KeyValuePair<TKey, TValue>();
      }

      DictionaryEntry IDictionaryEnumerator.Entry
      {
        get
        {
          this.ValidateCurrent();
          return new DictionaryEntry((object) this._current.Key, (object) this._current.Value);
        }
      }

      object IDictionaryEnumerator.Key
      {
        get
        {
          this.ValidateCurrent();
          return (object) this._current.Key;
        }
      }

      object IDictionaryEnumerator.Value
      {
        get
        {
          this.ValidateCurrent();
          return (object) this._current.Value;
        }
      }
    }
  }
}
