using System.Collections.Generic;

public class PriorityQueue<TElement, TPriority> where TPriority : System.IComparable<TPriority>
{
            private List<KeyValuePair<TElement, TPriority>> _elements = new List<KeyValuePair<TElement, TPriority>>();

            public int Count => _elements.Count;

            public void Enqueue(TElement item, TPriority priority)
            {
                _elements.Add(new KeyValuePair<TElement, TPriority>(item, priority));
            }

            public TElement Dequeue()
            {
                int bestIndex = 0;
                for (int i = 1; i < _elements.Count; i++)
                {
                    if (_elements[i].Value.CompareTo(_elements[bestIndex].Value) < 0)
                    {
                        bestIndex = i;
                    }
                }
                TElement bestItem = _elements[bestIndex].Key;
                _elements.RemoveAt(bestIndex);
                return bestItem;
            }
        }
