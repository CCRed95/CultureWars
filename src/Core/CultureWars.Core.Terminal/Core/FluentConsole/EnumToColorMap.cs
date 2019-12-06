using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CultureWars.Core.FluentConsole
{
	internal class EnumToColorMap<TEnum>
		: IDictionary<TEnum, Color>
			where TEnum
				: Enum
	{ 
		private static readonly Dictionary<TEnum, Color> _classificationToColorMap
			= new Dictionary<TEnum, Color>();


		public EnumToColorMap()
		{
		}
		

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<TEnum, Color>> GetEnumerator()
		{
			return _classificationToColorMap.GetEnumerator();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)_classificationToColorMap).GetEnumerator();
		}

		/// <inheritdoc />
		public void Add(KeyValuePair<TEnum, Color> item)
		{
			_classificationToColorMap.Add(item.Key, item.Value);
		}

		/// <inheritdoc />
		public void Clear()
		{
			_classificationToColorMap.Clear();
		}

		/// <inheritdoc />
		public bool Contains(KeyValuePair<TEnum, Color> item)
		{
			return _classificationToColorMap.Contains(item);
		}

		/// <inheritdoc />
		public void CopyTo(KeyValuePair<TEnum, Color>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
			//_classificationToColorMap.CopyTo(array, arrayIndex);
		}

		/// <inheritdoc />
		public bool Remove(KeyValuePair<TEnum, Color> item)
		{
			throw new NotImplementedException();
			//return _classificationToColorMap.Remove(item);
		}

		/// <inheritdoc />
		public int Count
		{
			get => _classificationToColorMap.Count;
		}
		/// <inheritdoc />
		public bool IsReadOnly
		{
			get => false;
		}

		/// <inheritdoc />
		public bool ContainsKey(TEnum key)
		{
			return _classificationToColorMap.ContainsKey(key);
		}

		/// <inheritdoc />
		public void Add(TEnum key, Color value)
		{
			_classificationToColorMap.Add(key, value);
		}

		/// <inheritdoc />
		public bool Remove(TEnum key)
		{
			return _classificationToColorMap.Remove(key);
		}

		/// <inheritdoc />
		public bool TryGetValue(TEnum key, out Color value)
		{
			return _classificationToColorMap.TryGetValue(key, out value);
		}

		/// <inheritdoc />
		public Color this[TEnum key]
		{
			get => _classificationToColorMap[key];
			set => _classificationToColorMap[key] = value;
		}
		/// <inheritdoc />
		public ICollection<TEnum> Keys
		{
			get => _classificationToColorMap.Keys;
		}
		/// <inheritdoc />
		public ICollection<Color> Values
		{
			get => _classificationToColorMap.Values;
		}
	}
}