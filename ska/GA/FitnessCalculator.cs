using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ska.GA {
	class FitnessCalculator<T> where T : Evolvable {
		public abstract float getFitness(T specimen);
	}
}
