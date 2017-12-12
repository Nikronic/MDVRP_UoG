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


        public static Depot Routing(double[,] savingMatrices)
        {
            int routesCount = 0;
            List<List<Customer>> routes = new List<List<Customer>>();
            for (int c1 = 0; c1 < savingMatrices.Length; c1++)
            {
                routes[routesCount] = new List<Customer>();
                for (int c2 = 0; c2 < savingMatrices.Length; c2++)
                {
                    
                }
            }
            return null;
        }


        /// <summary>
        /// Based on Saving Matrices in Wright-Clarke algorithm, we find out how many routes(vehicles) needed.
        /// The initialize lists with sequence of customers without assuming any limitation.
        /// </summary>
        /// <param name="depot"></param>
        /// <returns></returns>
        public static Depot SavingMatrices(Depot depot)
        {
            double[,] matrix = new double[depot.Count, depot.Count];

            for (int c1 = 0; c1 < depot.Count; c1++)
            {
                Customer customer1 = depot[c1];
                for (int c2 = 0; c2 < depot.Count; c2++)
                {
                    Customer customer2 = depot[c2];
                    if (c1 == c2) { }
                    else
                    {
                        matrix[c1, c2] = EuclideanDistance(customer1, depot) + EuclideanDistance(customer2, depot) -
                                         EuclideanDistance(customer1, customer2);
                    }
                }
            }



            return null; //TODO ******
        }


    }
}
