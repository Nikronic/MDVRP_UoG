using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDVRP_ORIG
{
    public class Chromosome : IList<Depot>
    {
        /// <summary>
        /// Chromosome is list of lists.
        /// The upper level list is depots and the lower level is list of customers for each depot(upper level lists).
        /// </summary>
        public List<Depot> ChromosomeList  { get; set; }
       
        /// <summary>
        /// Constructing empty base list by an input size.
        /// </summary>
        /// <param name="depots">The number of depots for initializing base list.</param>
        public Chromosome(int depots ,int capacity)
        {
            ChromosomeList = new List<Depot>(depots);
            
            for (int i = 0; i < depots; i++)
            {
                ChromosomeList[i] = new Depot();
                ChromosomeList[i].Capacity = capacity;
            }
        }

        public int Count => ChromosomeList.Count;

        public double Fitness { get; set; }

        public bool IsReadOnly => false;

        public Depot this[int index] { get { return ChromosomeList[index]; } set { ChromosomeList[index] = value; } }

        public int IndexOf(Depot item)
        {
            return ChromosomeList.IndexOf(item);
        }

        public void Insert(int index, Depot item)
        {
            ChromosomeList.Insert(index,item);
        }

        public void RemoveAt(int index)
        {
            ChromosomeList.RemoveAt(index);
        }

        public void Add(Depot item)
        {
            ChromosomeList.Add(item);
        }

        public void Clear()
        {
            ChromosomeList.Clear();
        }

        public bool Contains(Depot item)
        {
            return ChromosomeList.Contains(item);
        }

        public void CopyTo(Depot[] array, int arrayIndex)
        {
            ChromosomeList.CopyTo(array,arrayIndex);
        }

        public bool Remove(Depot item)
        {
            return ChromosomeList.Remove(item);
        }

        public IEnumerator<Depot> GetEnumerator()
        {
            return ChromosomeList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ChromosomeList.GetEnumerator();
        }
    }
}
