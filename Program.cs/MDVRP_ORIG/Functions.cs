using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDVRP_ORIG
{
    class Functions
    {
        /// <summary>
        /// Compute the Euclidean distance between two customers to group them to the depots.
        /// </summary>
        /// <param name="customer">The first customer</param>
        /// <param name="customer2">The second customer</param>
        /// <returns></returns>
        public double EuclideanDistance(Customer customer, Customer customer2)
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
        public double EuclideanDistance(Customer customer, Depot depot)
        {
            double distance = Math.Sqrt(Math.Pow((customer.X - depot.X), 2) + Math.Pow((customer.Y - depot.Y), 2));
            return distance;
        }


        public Chromosome SavingMatrices(Chromosome chromosome)
        {
            Chromosome UpdatedChromosome = new Chromosome(chromosome.ChromosomeList.Capacity);
            foreach (var depot in chromosome.ChromosomeList)
            {
                foreach (var path in depot)
                {
                    
                }
            }
            
        }



    }
}
