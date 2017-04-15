using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ska.GA;

namespace ska
{
    class Program
    {
        static void Main(string[] args)
        {
			Evolver<EvolvableString> evolver = new Evolver<EvolvableString>(new FitnessCalculatorString());
			evolver.initialize();

			for (int i = 0; i < 100000; i++) {
				evolver.updateGeneration();
				if (evolver.champion.fitness > 0.99) break;
			}
        }
    }
}
