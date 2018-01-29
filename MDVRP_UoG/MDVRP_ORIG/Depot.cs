using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MDVRP_ORIG
{
    public class Depot :IList<Customer>
    {
        public Customer this[int index] {
            get
            {
               return DepotCustomers[index];
            }
            set
            {
                DepotCustomers[index] = value;
            }
        }

        /// <summary>
        /// The ID for depots
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// X coordinate of depot
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y coordinate of depot
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Capacity of each depot
        /// </summary>
        public int Capacity { get; set; }

        public Depot (int id,int x, int y, int capacity)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Capacity = capacity;
            this.DepotCustomers = new List<Customer>();
        }

        public Depot () { }

        /// <summary>
        /// Each depot has list of customers
        /// </summary>
        public List<Customer> DepotCustomers { get; set; }

        /// <summary>
        /// Number of customers
        /// </summary>
        public int Count => DepotCustomers.Count;

        public bool IsReadOnly => false;

        public void Add(Customer item)
        {
            DepotCustomers.Add(item);
        }

        public void Clear()
        {
            DepotCustomers.Clear();
        }

        public bool Contains(Customer item)
        {
            return DepotCustomers.Contains(item);
        }

        public void CopyTo(Customer[] array, int arrayIndex)
        {
            DepotCustomers.CopyTo(array,arrayIndex);
        }

        public IEnumerator<Customer> GetEnumerator()
        {
            return DepotCustomers.GetEnumerator();
        }

        public int IndexOf(Customer item)
        {
            return DepotCustomers.IndexOf(item);
        }

        public void Insert(int index, Customer item)
        {
            DepotCustomers.Insert(index,item);
        }

        public bool Remove(Customer item)
        {
            return DepotCustomers.Remove(item);
        }

        public void RemoveAt(int index)
        {
            DepotCustomers.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return DepotCustomers.GetEnumerator();
        }
    }
}
