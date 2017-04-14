using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ska.GA {
	class Evolver<T> where T : Evolvable, new() {


		const int populationSize = 50;
		const int rankSelection = 25;
		const double crossoverRate = 0.7;
		const double mutationRate = 0.001;

		List<T> population = new List<T>();

		int generationCount = 0;

		FitnessCalculator<T> fitnessCalculator;

		public Evolver(FitnessCalculator<T> fc) {
			this.fitnessCalculator = fc;
		}

		public void initialize() {
			// start with a random population
			for (int i = 0; i < populationSize; i++) {
				T s = new T();
				s.randomize();
				population.Add(s);
			}
		}

		void updateGeneration() {
			generationCount++;

			// calculate fitness values
			foreach (var s in population) s.fitness = fitnessCalculator.getFitness(s);

			// sort population by fitness
			population = population.OrderByDescending(o => o.fitness).ToList();

			printGeneration();

			// discard bottom-ranked specimen
			population.RemoveRange(rankSelection, populationSize - rankSelection);

			// produce offspring
			for (int i = 0; i < populationSize - rankSelection; i += 2) {
				produceOffspring();
			}
		}

		void printGeneration() {
			double averageFitness = 0.0;
			foreach (var s in population) {
				averageFitness += s.fitness;
			}
			averageFitness /= (double)populationSize;
			double maxFitness = population[0].fitness;

			Console.Write("" + generationCount + "\t" + averageFitness + "\t" + maxFitness + "\n");
		}

		void produceOffspring() {
			// pick two random parents from the top-ranking
			T parent1 = topRankingRandom;
			T parent2 = topRankingRandom;

			T offspring1 = new T();
			T offspring2 = new T();

			offspring1.randomize();
			offspring2.randomize();

			// crossover chromosome
			if (rollDiceForCrossover()) {
				parent1.crossover(parent1, parent2,  offspring1, offspring2);
			}

			// mutate both offsprings
			mutate(offspring1);
			mutate(offspring2);

			// insert them to the population
			if (population.Count < populationSize) population.Add(offspring1);
			if (population.Count < populationSize) population.Add(offspring2);
		}

		// specimen mutation
		void mutate(T s) {
			for (int i = 0; i < s.getLocusCount(); i++) {
				if (rollDiceForMutation()) s.mutateLocus(i);
			}
		}

		T topRankingRandom {
			get {
				return population[Helper.RandomInt(0, rankSelection)];
			}
		}

		bool rollDiceForCrossover() {
			return Helper.RandomDouble() < crossoverRate;
		}

		bool rollDiceForMutation() {
			return Helper.RandomDouble() < mutationRate;
		}


	}
}
