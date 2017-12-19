using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
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


        public static List<Chromosome> Tournament(List<Chromosome> population,int populationSize)
        {
            List<Chromosome> tournamentOutput = new List<Chromosome>();

            List<Chromosome> tournamentSet = new List<Chromosome>();
            tournamentSet = TournamentPopulation(population, populationSize);
            const double probabilityOfTournament = 0.8f; // the parameter of paper
            Random random = new Random();
            int randomProb = random.Next();
            if (randomProb <= probabilityOfTournament)
            {
                List<Chromosome> tournamentSet2 = new List<Chromosome>();
                tournamentSet2 = TournamentPopulation(population, populationSize);

                // get to fittest
                Chromosome firstFittest = new Chromosome(-1,-1);
                firstFittest = FittestChromosome(tournamentSet);
                Chromosome secondFittest = new Chromosome(-1,-1);
                secondFittest = FittestChromosome(tournamentSet2);

                // output to crossover
                tournamentOutput.Add(firstFittest);
                tournamentOutput.Add(secondFittest);
            }
            else
            {
                int randomChromosome = random.Next(0, tournamentSet.Count);
                Chromosome firstChromosome = new Chromosome(-1,-1);
                firstChromosome = tournamentSet[randomChromosome];

                int randomChromosome2 = random.Next(0, tournamentSet.Count);
                Chromosome secondChromosome = new Chromosome(-1, -1);
                secondChromosome = tournamentSet[randomChromosome];

                // output to crossover
                tournamentOutput.Add(firstChromosome);
                tournamentOutput.Add(secondChromosome);
            }
            return tournamentOutput;
        }

        /// <summary>
        /// We get a tournament set of chromosomes to select the fittest.
        /// </summary>
        /// <param name="k">Size of each tournament set</param>
        /// <param name="population">Tournament set</param>
        /// <returns></returns>
        private static List<Chromosome> TournamentPopulation(List<Chromosome> population ,int k)
        {
            Random randomGenerator = new Random();
            List<Chromosome> populationSet = new List<Chromosome>();
            while (populationSet.Count != k)
            {
                int randomNumber = randomGenerator.Next(0, population.Count);
                if (!populationSet.Contains(population[randomNumber]))
                {
                    populationSet.Add(population[randomNumber]);
                }
            }
            return populationSet;
        }

        /// <summary>
        /// Return the fittest chromosome in the tournament set.
        /// </summary>
        /// <param name="tournamentSet"></param>
        /// <returns></returns>
        private static Chromosome FittestChromosome(List<Chromosome> tournamentSet)
        {
            Chromosome fittest = new Chromosome(-1,-1);
            foreach (Chromosome chromosome in tournamentSet)
            {
                if (chromosome.Fitness>= fittest.Fitness)
                {
                    fittest = chromosome;
                }
            }
            return fittest;
        }
    }
}
