using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ska.GA {
	class EvolvableString : Evolvable {

		public const string targetString = "turing believes machines think";
		public static int SIZE { get { return targetString.Length; } }


		private char[] dna = new char[SIZE];

		public override string toString() {
			return new string(dna);
		}

		public override void fromString(string s) {
			for (int i = 0; i < SIZE; i++) dna[i] = s[i];
		}

		public override void randomize() {
			for (int i = 0; i < SIZE; i++) dna[i] = getRandomLetter();
		}

		// how many genetic locations (letters in its dna) does this specimen have?
		public override int getLocusCount() {
			return SIZE;
		}

		// apply the mutation operator to the specified locus
		public override void mutateLocus(int which) {
			if (which < 0 || which > SIZE) return;
			dna[which] = getRandomLetter();
		}

		private static char getRandomLetter() {
			char ret = 'a';
			ret += (char)Helper.RandomInt(0, 'z' - 'a' + 2);
			if (ret > 'z') return ' '; // hack to make whitespace possible
			return ret;
		}

		public override void crossover(Evolvable parentA, Evolvable parentB, Evolvable childA, Evolvable childB) {
			var pA = parentA as EvolvableString;
			var pB = parentB as EvolvableString;
			var cA = childA as EvolvableString;
			var cB = childB as EvolvableString;

			int m = Helper.RandomInt(0, SIZE + 1);

			for (int i = 0; i < SIZE; i++) {
				if (i < m) {
					cA.dna[i] = pA.dna[i];
					cB.dna[i] = pB.dna[i];
				} else {
					cA.dna[i] = pB.dna[i];
					cB.dna[i] = pB.dna[i];
				}
			}
		}

	}
}
