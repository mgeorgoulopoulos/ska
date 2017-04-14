using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ska.GA {
	abstract class Evolvable {

		public double fitness = 0.0;

		// convert to/from string representation
		public abstract string toString();
		public abstract void fromString(string s);

		public abstract void randomize();

		// how many genetic locations (letters in its dna) does this specimen have?
		public abstract int getLocusCount();

		// apply the mutation operator to the specified locus
		public abstract void mutateLocus(int which);
		
		// crossover operator
		public abstract void crossover(Evolvable parentA, Evolvable parentB, Evolvable childA, Evolvable childB);
	}
}
