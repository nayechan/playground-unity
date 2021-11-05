using System;
using System.Collections;
using System.Collections.Generic;

/*
 * JsonUtility.ToJson 메서드에서 제네릭 (List<ToyData>) 타입의 직렬화를 지원하지 않아
 * 직렬화를 가능하도록 만든 Wrapper 클래스입니다.
 */

namespace SandboxEditor.Data.Toy
{
    [Serializable]
    public class ToysData : IList<ToyData>
    {
        public List<ToyData> toysData = new List<ToyData>();
        public IEnumerator<ToyData> GetEnumerator()
        {
            return toysData.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(ToyData item)
        {
            toysData.Add(item);
        }

        public void Clear()
        {
            toysData.Clear();
        }

        public bool Contains(ToyData item)
        {
            return toysData.Contains(item);
        }

        public void CopyTo(ToyData[] array, int arrayIndex)
        {
            toysData.CopyTo(array, arrayIndex);
        }

        public bool Remove(ToyData item)
        {
            return toysData.Remove(item);
        }

        public int Count => toysData.Count;
        public bool IsReadOnly => false;
        public int IndexOf(ToyData item)
        {
            return toysData.IndexOf(item);
        }

        public void Insert(int index, ToyData item)
        {
            toysData.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            toysData.RemoveAt(index);
        }

        public ToyData this[int index]
        {
            get => toysData[index];
            set => toysData[index] = value;
        }
    }
}