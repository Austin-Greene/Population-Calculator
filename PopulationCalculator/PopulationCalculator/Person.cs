using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationCalculator
{
	class Person
	{
		public int BirthYear;
		public int DeathYear;

		public Person(int birthYear, int deathYear)
		{
			BirthYear = birthYear;
			DeathYear = deathYear;
		}
	}
}
