using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ska.GA {
	abstract class FitnessCalculator<T> where T : Evolvable {
		public abstract void assignFitnessValues(List<T> population);
	}
}
