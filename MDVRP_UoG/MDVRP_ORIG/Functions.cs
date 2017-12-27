using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading;


// ReSharper disable InvertIf

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

        /// <summary>
        /// Returns parents to crossover on them
        /// </summary>
        /// <param name="population">The population (all chromosomes)</param>
        /// <param name="populationSize">Size of each tournament set</param>
        /// <returns>Two chromosomes as parent to crossover</returns>
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
                // typically we are selecting both chromosomes in tournament set (binary tournament)
                int randomChromosome = random.Next(0, tournamentSet.Count);
                Chromosome firstChromosome = new Chromosome(-1,-1);
                firstChromosome = tournamentSet[randomChromosome];

                int randomChromosome2 = random.Next(0, tournamentSet.Count);
                Chromosome secondChromosome = new Chromosome(-1, -1);
                secondChromosome = tournamentSet[randomChromosome2];

                // output to crossover
                tournamentOutput.Add(firstChromosome);
                tournamentOutput.Add(secondChromosome);
            }
            return tournamentOutput;
        }

        /// <summary>
        /// We get a tournament set of chromosomes to select the fittest.
        /// As we sue binary tournament, size of tournament population is 2 (k = 2)
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

        /// <summary>
        /// Crossover on parent were selected by tournament method
        /// </summary>
        /// <param name="parent">A list of two parent to crossover</param>
        /// <returns>Two eligible childes</returns>
        public static List<Chromosome> CrossOver(List<Chromosome> parent)
        {
            Chromosome parent1 = parent[0].Clone();
            Chromosome parent2 = parent[1].Clone();
            List<Customer> randomRoute1 = RandomRoute(parent1);
            List<Customer> randomRoute2 = RandomRoute(parent2);
            DeletingRoute(randomRoute1, parent2);
            DeletingRoute(randomRoute2, parent1);
            for (int i = 0; i < randomRoute1.Count; i++)
            {
                Insert(randomRoute1[i], parent2);
            }
            for (int i = 0; i < randomRoute2.Count; i++)
            {
                Insert(randomRoute2[i], parent1);
            }
            List<Chromosome> result = new List<Chromosome>();
            result.Add(parent1);
            result.Add(parent2);
            return result;
        }

        /// <summary>
        /// Selecting a random route in each parent (part of crossover)
        /// </summary>
        /// <param name="chromosome">The parent chromosome</param>
        /// <returns></returns>
        private static List<Customer> RandomRoute(Chromosome chromosome)
        {
            List<Customer> deletedRoute = new List<Customer>();
            Random random = new Random();
            int d = random.Next(chromosome.Count + 1);
            int r = random.Next(chromosome[d].Count);

            int i;
            if (chromosome[d][r].IsNull == false)
            {
                i = r;
                while (chromosome[d][i].IsNull == false)
                {
                    deletedRoute.Add(chromosome[d][i]);
                    i++;
                }
                i = r - 1;
                while (chromosome[d][i].IsNull == false)
                {
                    deletedRoute.Add(chromosome[d][i]);
                    i--;
                }
            }
            else
            {
                i = r + 1;
                while (chromosome[d][i].IsNull == false)
                {
                    deletedRoute.Add(chromosome[d][i]);
                    i++;
                }
            }
            return deletedRoute;
        }

        /// <summary>
        /// Deleting randomly selected route. The deleted customers will be added in better place (part of crossover)
        /// </summary>
        /// <param name="route">The randomly selected route in parent chromosome</param>
        /// <param name="chromosome">The parent chromosome</param>
        private static void DeletingRoute(List<Customer> route, Chromosome chromosome)
        {
            for (int i = 0; i < chromosome.Count; i++)
            {
                for (int j = 1; j < chromosome[i].Count - 1; j++)
                {
                    for (int k = 0; k < route.Count; k++)
                    {
                        if (chromosome[i][j] == route[k])
                        {
                            chromosome[i].RemoveAt(j);
                            if (chromosome[i][j].IsNull && chromosome[i][j - 1].IsNull)
                            {
                                chromosome[i].RemoveAt(j);
                            }
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// After deleting all customers from randomly selected route, we add them in better place (part of crossover)
        /// </summary>
        /// <param name="customer">Removed customer from randomly selected route of a parent</param>
        /// <param name="chromosome">Parent chromosome</param>
        private static void Insert(Customer customer, Chromosome chromosome)
        {
            double min = EuclideanDistance(customer, chromosome[0]);
            int index = 0;
            for (int i = 1; i < chromosome.Count; i++)
            {
                double temp = EuclideanDistance(customer, chromosome[i]);
                if (temp < min)
                {
                    min = temp;
                    index = i;
                }
            }
            List<double> distance = new List<double>();
            List<int> customerCost = new List<int>();
            int k = -1;
            for (int i = 0; i < chromosome[index].Count-1; i++)
            {
                if (chromosome[index][i].IsNull)
                {
                    customerCost.Add(0);
                    distance.Add(0);
                    k++;
                }
                customerCost[k] += chromosome[index][i].Cost;
                distance[k] += EuclideanDistance(chromosome[index][i], chromosome[index][i + 1]);
            }

            k = -1;
            int index2 = -1;
            min = -1;
            for (int i = 0; i < chromosome[index].Count-1; i++)
            {
                if (chromosome[index][i].IsNull)
                {
                    k++;
                }
                if ( customerCost[k] + customer.Cost < chromosome[index].Capacity)
                {
                    double t1 = EuclideanDistance(chromosome[index][i], chromosome[index][i + 1]);
                    double t2 = EuclideanDistance(chromosome[index][i], customer) + EuclideanDistance(customer, chromosome[index][i + 1]);
                    double t3 = distance[k] - t1 + t2;
                    if (min == -1)
                    {
                        min = t3;
                        index2 = i;
                    }
                    else if (min > t3)
                    {
                        min = t3;
                        index2 = i;
                    }
                }
            }

            if (index2 == -1)
            {
                Customer nullCustomer = new Customer
                {
                    IsNull = true,
                    X = chromosome[index].X,
                    Y = chromosome[index].Y,
                    Id = chromosome[index].Id
                };
                chromosome[index].Add(customer);
                chromosome[index].Add(nullCustomer);
            }
            else
            {
                chromosome[index2].Insert(index2 + 1, customer);
            }

        }

        // Intra-depot mutation methods


        /// <summary>
        /// Reversal mutation mutate a chromosome with reversing a cut in chromosome.
        /// </summary>
        /// <param name="chromosome">Chromosome to Mutate</param>
        /// <returns></returns>
        public static Chromosome ReversalMutation(Chromosome chromosome)
        {
            Random randomGenerator = new Random();
            List<Customer> mutatedRoute = new List<Customer>();

            int randomDepot = randomGenerator.Next(0, chromosome.ChromosomeList.Count);
            Depot depot = new Depot();
            depot = chromosome[randomDepot];

            List<Customer> route = new List<Customer>();
            route = MutationRoute(depot);
            mutatedRoute = route;

            int firstCut = randomGenerator.Next(0, route.Count);
            int secondCut = randomGenerator.Next(0, route.Count);
            while (secondCut < firstCut)
            {
                secondCut = randomGenerator.Next(0, route.Count);
            }
            

            if (firstCut == secondCut) {}
            else
            {
                for (int i = secondCut,k=firstCut; i >= firstCut; i--,k++)
                {
                    mutatedRoute[k] = route[i];
                }
            }
            // TODO: I mutated a new List<Customer>. So i don't which route must be updated. So next time I Will fix it.

            return null; // TODO : check this shit

        }

        /// <summary>
        /// For Reversal mutation we need to get a path to cut it by two points , then replace reversed. (Part of intra-depot mutation)
        /// </summary>
        /// <param name="depot">The depot we want to find random route</param>
        /// <returns>A route for reverse</returns>
        private static List<Customer> MutationRoute(Depot depot)
        {
            List<Customer> route = new List<Customer>();

            Random randomGenerator = new Random();
            int customerInRouteIndex = randomGenerator.Next(0, depot.DepotCustomers.Count);
            Customer customer = depot[customerInRouteIndex];
            while (customer.IsNull)
            {
                customerInRouteIndex = randomGenerator.Next(0, depot.DepotCustomers.Count);
                customer = depot[customerInRouteIndex];
            }

            int routeStartIndex = customerInRouteIndex;
            for (int i = customerInRouteIndex - 1; i >= 0 ; i--)
            {
                Customer temp = depot[i];
                if (!temp.IsNull)
                {
                    routeStartIndex = i;
                }
                else
                {
                    break;
                }
            }

            int routeEndIndex = customerInRouteIndex;
            for (int i = customerInRouteIndex+1; i < depot.DepotCustomers.Count; i++)
            {
                Customer temp = depot[i];
                if (!temp.IsNull)
                {
                    routeEndIndex = i;
                }
                else
                {
                    break;
                }
            }

            route = depot.DepotCustomers.GetRange(routeStartIndex, routeEndIndex);

            return route;
        }

    }
}
