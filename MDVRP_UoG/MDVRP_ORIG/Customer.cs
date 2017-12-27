using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDVRP_ORIG
{
    public class Customer
    {
        /// <summary>
        /// The ID for each customer
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// X coordinate of customer
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y coordinate of customer
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// The cost of servicing to each customer.
        /// It depend on the object of the solution.
        /// In this implementation, we assume it as "weight".
        ///     because every vehicle(rout) has a weight limit.
        /// </summary>
        public int Cost { get; set; } = 0;

        /// <summary>
        /// Splitting up paths with adding null customer
        /// </summary>
        public bool IsNull { get; set; } = false;

    }
}
