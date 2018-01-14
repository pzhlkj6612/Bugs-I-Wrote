using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INI
{
    public class INIStruct<TValue> : BiMapping, IEnumerable<TValue> where TValue : InMapping, new()
    {
        Dictionary<string, TValue> dict;//Storage.
        List<string> order;//Order.

        public IEnumerator<TValue> GetEnumerator()
        {
            foreach (var key in order)
                yield return dict[key];
            yield break;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public INIStruct()
        {
            dict = new Dictionary<string, TValue>();
            order = new List<string>();
        }
        public INIStruct(INIStruct<TValue> s)
        {
            dict = new Dictionary<string, TValue>(s.dict);
            order = new List<string>(s.order);
        }

        public int Count
        {
            get { return dict.Count; }
        }
        public List<string> Keys
        {
            get { return dict.Keys.ToList(); }
        }
        public List<TValue> Values
        {
            get { return dict.Values.ToList(); }
        }
        public TValue this[string key]
        {
            get
            {
                if (Contains(key))
                    return dict[key];
                throw new KeyNotFoundException($"\"{key}\" not existed.");
            }
            set
            {
                if (Contains(key))
                    dict[key] = value;
                else
                    Add(key, value);
            }
        }
        public string this[TValue value]
        {
            get
            {
                string result = dict.FirstOrDefault(line => line.Value.Equals(value)).Key;
                if (result != null)
                    return result;
                throw new KeyNotFoundException($"The key which value is \"{value}\" not existed.");
            }
        }

        public void Add(string key, TValue value)
        {
            if (Contains(key))
                throw new ArgumentException($"\"{key}\" existed.");

            AddChild(value);//Map it.
            dict.Add(key, value);
            order.Add(key);
        }
        public void Insert(int index, string key, TValue value)
        {
            if (index < 0 || index > Count)
                throw new IndexOutOfRangeException($"Index \"{index}\" out of range, current count: {Count}.");
            if (Contains(key))
                throw new ArgumentException($"\"{key}\" existed.");

            AddChild(value);//Map it.
            dict.Add(key, value);
            order.Insert(index, key);
        }
        public int IndexOf(string key)
        {
            if (Contains(key) == false)
                return -1;
            return order.IndexOf(key);
        }
        public bool Contains(string key)
        {
            return dict.ContainsKey(key);
        }
        public bool TryGetValue(string key, out TValue value)
        {
            return dict.TryGetValue(key, out value);
        }
        public bool Remove(string key)
        {
            if (Contains(key) == false)
                return false;

            RemoveChild(dict[key]);//Map it.
            dict.Remove(key);
            order.Remove(key);

            return true;
        }
        public void Clear()
        {
            dict.Clear();//linesDic = new Dictionary<string, INILine>();
            order.Clear();//linesOrder = new List<string>();
        }

        public void MoveKeyByRef(string toBeMovedKey, string refKey, bool beforeRef)
        {
            if (Contains(toBeMovedKey) == false)//Not existed.
                throw new ArgumentException($"\"{toBeMovedKey}\" not existed.");
            if (Contains(refKey) == false)//Not existed.
                throw new ArgumentException($"\"{refKey}\" not existed.");

            order.Remove(toBeMovedKey);
            if (beforeRef)
                order.Insert(order.IndexOf(refKey), toBeMovedKey);
            else
                order.Insert(order.IndexOf(refKey) + 1, toBeMovedKey);
        }
        public void RenameKey(string oldName, string newName)
        {
            if (Contains(oldName) == false)//Not existed.
                throw new ArgumentException($"\"{oldName}\" not existed.");
            if (Contains(newName))//Existed.
                throw new ArgumentException($"\"{newName}\" existed.");

            Insert(IndexOf(oldName), newName, new TValue());//new TValue(oldName) - error CS0417: 'TValue': cannot provide arguments when creating an instance of a variable type
            this[newName] = this[oldName];

            RemoveChild(this[oldName]);//Demap it.
            AddChild(this[newName]);//Map it.
            this[oldName].SetParent(null);//Demap it.
            this[newName].SetParent(this);//Map it.
            Remove(oldName);
        }
    }
}
