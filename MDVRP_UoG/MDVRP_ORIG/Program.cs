using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MDVRP_ORIG
{
    class Program
    {

        static void Main(string[] args)
        {
            int populaitionSize = 10; //input
            int capacity = 10; //input
            List<Customer> customer = new List<Customer>();//input
            List<Depot> depot = new List<Depot>();//input

            Chromosome chromosomeSample = GenerateChromosomeSample(depot,customer,capacity);

            List<Chromosome> population = GeneratePopulation(populaitionSize, chromosomeSample);

            calculationFitness(population);
                       
        }

        public static Chromosome GenerateChromosomeSample(List<Depot> depot , List<Customer> customer , int capacity)
        {
            Chromosome chromosomeSample = new Chromosome(depot.Count,capacity);
            for (int i = 0; i < depot.Count; i++)
            {
                chromosomeSample[i].Id = depot[i].Id;
                chromosomeSample[i].X = depot[i].X;
                chromosomeSample[i].Y = depot[i].Y;
                chromosomeSample[i].Capacity = depot[i].Capacity;
            }
            for (int i = 0; i < customer.Count; i++)
            {
                double min = Functions.EuclideanDistance(customer[i], chromosomeSample[0]); ;
                int k = 0;
                for (int j = 1; j < chromosomeSample.Count; j++)
                {
                    double temp = Functions.EuclideanDistance(customer[i], chromosomeSample[j]);
                    if (min > temp)
                    {
                        min = temp;
                        k = j;
                    }
                }
                chromosomeSample[k].Add(customer[i]);
            }
            return chromosomeSample;
        }

        public static List<Chromosome> GeneratePopulation (int populaitionSize , Chromosome chromosomeSample)
        {
            List<Chromosome> population = new List<Chromosome>();
            for (int i = 0; i < populaitionSize; i++)
            {
                Chromosome clone = chromosomeSample.Clone();
                for (int j = 0; j < clone.Count; j++)
                {
                    clone[j].RandomList();
                    clone[j].Routing();
                }
                population.Add(clone);
            }
            return population;
        }

        public static void CalculationFitness (List<Chromosome> populaitionList)
        {
            for (int k = 0; k < populaitionList.Count; k++)
            {
                double distance = 0;
                double route = 0;
                for (int i = 0; i < populaitionList.Count; i++)
                {
                    for (int j = 0; j < populaitionList[k][i].Count - 1; j++)
                    {
                        if (populaitionList[k][i][j].IsNull == true)
                        {
                            route = route + 1;
                        }
                        distance += Functions.EuclideanDistance(populaitionList[k][i][j], populaitionList[k][i][j + 1]);
                    }
                }
                populaitionList[k].Fitness = ((100 * route) + ((0.001f) * distance));
            }
        }

    }
}
