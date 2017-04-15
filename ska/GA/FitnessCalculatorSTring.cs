using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ska.GA {
	class FitnessCalculatorString : FitnessCalculator<EvolvableString> {
		public override void assignFitnessValues(List<EvolvableString> population) {
			foreach (var specimen in population) {
				string s = specimen.toString();

				double correct = 0.0;
				for (int i = 0; i < EvolvableString.SIZE; i++) {
					if (s[i] == EvolvableString.targetString[i]) correct++;
				}

				specimen.fitness = correct / EvolvableString.SIZE;
			}
			
		}
	}
}
