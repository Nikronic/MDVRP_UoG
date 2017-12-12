using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDVRP_ORIG
{
    public class Chromosome
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
        public Chromosome(int depots)
        {
            ChromosomeList = new List<Depot>(depots);

            for (int i = 0; i < depots; i++)
            {
                ChromosomeList[i] = new Depot();
            }
        }
                
    }
}
