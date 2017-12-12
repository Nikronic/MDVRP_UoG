using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDVRP_ORIG
{
    public class Chromosome
    {
        public List<List<Customer>> ChromosomeList  { get; set; }
        
        public Chromosome(int depots)
        {
            this.ChromosomeList = new List<List<Customer>>(depots);

            for (int i = 0; i < depots; i++)
            {
                this.ChromosomeList[i] = new List<Customer>();
            }
        }
                
    }
}
