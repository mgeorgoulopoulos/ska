using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ska.GA {
	class Evolver<T> where T : Evolvable, new() {


		const int populationSize = 50;
		const int rankSelection = 25;
		const double crossoverRate = 0.9;
		const double mutationRate = 0.05;

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

		public void updateGeneration() {
			generationCount++;

			// calculate fitness values
			fitnessCalculator.assignFitnessValues(population);

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

			// print top 5 members
			for (int i = 0; i < 1; i++) {
				if (i >= population.Count) break;
				Console.Write(population[i].toString() + "\t");
			}
			Console.Write("\n");
		}

		void produceOffspring() {
			// pick two random parents from the top-ranking
			T parent1 = topRankingRandom;
			T parent2 = topRankingRandom;

			// start by cloning the parents
			T offspring1 = new T();
			T offspring2 = new T();
			offspring1.fromString(parent1.toString());
			offspring2.fromString(parent2.toString());

			// if we roll the dice and they tell us to crossover, overwrite the clones with hybrids!
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

		public T champion {
			get {
				return population[0];
			}
		}
	}
}
