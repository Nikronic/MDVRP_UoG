using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDVRP_ORIG
{
    public static class Functions
    {

        /// <summary>
        /// Compute the Euclidean distance between two customers to group them to the depots.
        /// </summary>
        /// <param name="customer">The first customer</param>
        /// <param name="customer2">The second customer</param>
        /// <returns></returns>
        public static double EuclideanDistance(Customer customer, Customer customer2)
        {
            double distance = Math.Sqrt(Math.Pow((customer.X - customer2.X), 2) + Math.Pow((customer.Y - customer2.Y), 2));
            return distance;
        }

        /// <summary>
        /// Compute the Euclidean distance between a customers and a depot to group them to the depots.
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="depot"></param>
        /// <returns></returns>
        public static double EuclideanDistance(Customer customer, Depot depot)
        {
            double distance = Math.Sqrt(Math.Pow((customer.X - depot.X), 2) + Math.Pow((customer.Y - depot.Y), 2));
            return distance;
        }

        /// <summary>
        /// Sequentially insert customers to routes until we reach weight limitation.
        /// </summary>
        /// <param name="depot"></param>
        /// <returns></returns>
        public static void Routing(this Depot depot)
        {
            int weight = 0;
            Customer nullCustomer = new Customer();
            for (int i = 0; i < depot.Count; i++)
            {               
                if (weight + depot[i].Cost > depot.Capacity)
                {
                    depot.Insert(i, nullCustomer);
                    nullCustomer.IsNull = true;
                    nullCustomer.X = depot[i].X;
                    nullCustomer.Y = depot[i].Y;
                    nullCustomer.Id = depot[i].Id;
                    weight = 0;
                }
                weight += depot[i].Cost;
            }
            depot.Add(nullCustomer);
        }

        /// <summary>
        /// For building initial population, we build random chromosomes.
        /// </summary>
        /// <param name="depot">Depot</param>
        public static void RandomList(this Depot depot)
        {
            var random = new Random();
            for (int i = 0; i < depot.Count; i++)
            {
                int ran = random.Next(i-1,depot.Count);
                Customer temp = depot[i];
                depot[i] = depot[ran];
                depot[ran] = temp;
            }
        }

        /// <summary>
        /// Out initial population contains chromosomes with same genes. So clone them by first chromosome.
        /// </summary>
        /// <param name="chromosome">First chromosome</param>
        /// <returns></returns>
        public static Chromosome Clone (this Chromosome chromosome)
        {
            Chromosome clone = new Chromosome(chromosome.Count,chromosome[0].Capacity);
            for (int i = 0; i < chromosome.Count; i++)
            {
                clone[i].Id = chromosome[i].Id;
                clone[i].X = chromosome[i].X;
                clone[i].Y = chromosome[i].Y;
                clone[i].Capacity = chromosome[i].Capacity;

                for (int j = 0; j < chromosome[i].Count; j++)
                {
                    clone[i].Add(chromosome[i][j]);
                }
            }
            return clone;
        }


        public static List<Chromosome> Tournament(List<Chromosome> population)
        {

            return null;
        }
    }
}
