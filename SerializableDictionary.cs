using System.Collections.Generic;
using UnityEngine;

public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
	[SerializeField]
	private TKey[] _keys = null;
	[SerializeField]
	private TValue[] _values = null;

	public SerializableDictionary() { }

	public SerializableDictionary( IDictionary<TKey, TValue> dictionary ) : base( dictionary.Count )
	{
		foreach( var kvp in dictionary )
		{
			this[kvp.Key] = kvp.Value;
		}
	}

	public void AddRange( IDictionary<TKey, TValue> dict )
	{
		this.Clear();
		foreach( var kvp in dict )
		{
			this[kvp.Key] = kvp.Value;
		}
	}

	public void OnAfterDeserialize()
	{
		if( null != _keys && null != _values && _keys.Length == _values.Length )
		{
			this.Clear();
			int max = _keys.Length;
			for( int i = 0; i < max; ++i )
			{
				this[_keys[i]] = _values[i];
			}

			_keys = null;
			_values = null;
		}

	}

	public void OnBeforeSerialize()
	{
		int length = this.Count;
		_keys = new TKey[length];
		_values = new TValue[length];

		int i = 0;
		foreach( var kvp in this )
		{
			_keys[i] = kvp.Key;
			_values[i] = kvp.Value;
			++i;
		}
	}
}
