using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INI
{
    public class INIStruct<TValue> : IEnumerable<TValue> where TValue : new()
    {
        string parent;//How to use it?

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
                throw new KeyNotFoundException($"Key named \"{key}\" Not existed.");
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
                throw new KeyNotFoundException($"Key value is \"{value}\" Not existed.");
            }
        }

        public void Add(string key, TValue value)
        {
            if (Contains(key))
                throw new ArgumentException("Existed.");
            dict.Add(key, value);
            order.Add(key);
        }
        public void Insert(int index, string key, TValue value)
        {
            if (index < 0 || index > Count)
                throw new IndexOutOfRangeException("Index which used to insert out of range.");
            if (Contains(key))
                throw new ArgumentException("Existed.");
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
                throw new ArgumentException($"{toBeMovedKey} Not existed.");
            if (Contains(refKey) == false)//Not existed.
                throw new ArgumentException($"{refKey} Ref not existed.");
            order.Remove(toBeMovedKey);
            if (beforeRef)
                order.Insert(order.IndexOf(refKey), toBeMovedKey);
            else
                order.Insert(order.IndexOf(refKey) + 1, toBeMovedKey);
        }
        public void RenameKey(string oldName, string newName)
        {
            if (Contains(oldName) == false)//Not existed.
                throw new ArgumentException($"{oldName} Not existed.");
            if (Contains(newName))//Existed.
                throw new ArgumentException($"{newName} Existed.");
            Insert(IndexOf(oldName), newName, new TValue());
            //Insert(IndexOf(oldName), newName, new TValue(oldName)); - error CS0417: 'TValue': cannot provide arguments when creating an instance of a variable type
            this[newName] = this[oldName];
            Remove(oldName);
        }
    }
}
