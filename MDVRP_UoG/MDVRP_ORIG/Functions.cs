using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDVRP_ORIG
{
    public class Functions
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


        // TODO: این برای چیه علی؟؟؟؟
        public static List<Customer> Matrix( List<Customer> customerList)
        {
            List<Customer> list = new List<Customer>();
            // edit list and insert zeros
            return list;
        }


        /// <summary>
        /// Based on Saving Matrices in Wright-Clarke algorithm, we find out how many routes(vehicles) needed.
        /// The initialize lists with sequence of customers without assuming any limitation.
        /// </summary>
        /// <param name="chromosome">The unprocessed chromosome</param>
        /// <returns></returns>
        public static Chromosome SavingMatrices(Chromosome chromosome)
        {
            // A empty chromosome to update
            Chromosome updatedChromosome = new Chromosome(chromosome.ChromosomeList.Capacity);
            for (int i = 0; i < chromosome.ChromosomeList.Count; i++)
            {
                chromosome.ChromosomeList[i] = new Depot();
            }

            
            for (int d=0 ;d< chromosome.ChromosomeList.Count;d++)
            {
                Depot depot = chromosome.ChromosomeList[d];
                double[,] matrix = new double[d,d];

                // TODO: Nikan has been changed the "Depot" and "Chromosome" class. So we need to change all of out code based on new changes.

                //for (int c1=0;c1<depot ;c1++)
                //{
                //    Customer customer1 = depot[c1];
                //    for (int c2 = 0; c2 < depot.Count; c2++)
                //    {
                //        Customer customer2 = depot[c2];
                //        if (c1 == c2) {}
                //        else
                //        {
                //            matrix[c1,c2] = EuclideanDistance(customer1,depot)+EuclideanDistance()+
                //        }
                //    }
                //}
            }
            return updatedChromosome;
        }


    }
}
