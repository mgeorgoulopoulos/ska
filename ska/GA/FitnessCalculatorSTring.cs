using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ska.GA {
	class FitnessCalculatorString : FitnessCalculator<EvolvableString> {
		public override double getFitness(EvolvableString specimen) {
			const string target = "hello";
			string s = specimen.toString();

			double correct = 0.0;
			for (int i = 0; i < EvolvableString.SIZE; i++) {
				if (s[i] == target[i]) correct++;
			}

			return correct / EvolvableString.SIZE;
		}
	}
}
