using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationCalculator
{
	class Program
	{
		//Configuration
		private const int _minYear = 1900;
		private const int _maxYear = 2000;

		private const int _minYearlyBirthCount = 5000;
		private const int _maxYearlyBirthCount = 10000;

		private const int _minLifeSpan = 1;
		private const int _maxLifeSpan = 100;

		private static void Main(string[] args)
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Input a name to search the birth registries from 1900 - 2000.");
				Console.Write("> ");

				string input = Console.ReadLine();

				if (string.IsNullOrWhiteSpace(input))
				{
					continue;
				}

				//Convert the name to an int to use as the seed for the data set
				int randomSeed = input.NameToInt();
				
				//Generate a seeded data set, based on the name
				Person[] matchedPeople = GenerateDataSet(randomSeed);
				Console.WriteLine($"\nFound {matchedPeople.Length} results.");

				//Calculate all of the yearly censuses
				Census[] censuses = CalculateYearlyCensuses(matchedPeople);

				//Output the censuses in order of highest population to lowest
				OutputCensuses(censuses);

				Console.Write("\nPress any key to search again...");
				Console.ReadKey();
			}
		}

		/// <summary>
		/// Generates a data set of people whose birth and death years fall within the year range.
		/// </summary>
		/// <param name="seed">Seed to seed the random instance.</param>
		/// <returns></returns>
		private static Person[] GenerateDataSet(int seed)
		{
			//Seed a new random instance
			Random random = new Random(seed);

			//Generate the data set of people matching the parameters
			List<Person> matchedPeople = new List<Person>();
			Person person;

			for (int year = _minYear; year < _maxYear; year++)
			{
				int personCount = random.Next(_minYearlyBirthCount, _maxYearlyBirthCount + 1);

				for (int i = 0; i < personCount; i++)
				{
					person = new Person(year, year + random.Next(_minLifeSpan, _maxLifeSpan + 1));

					//Only add the person if the death year falls within the year range
					if (person.DeathYear <= _maxYear)
					{
						matchedPeople.Add(person);
					}
				}
			}

			return matchedPeople.ToArray();
		}
		
		/// <summary>
		/// Calculate the yearly population based on the input people.
		/// </summary>
		/// <param name="people">People to be sorted into censuses.</param>
		/// <returns></returns>
		private static Census[] CalculateYearlyCensuses(Person[] people)
		{
			//Initialize the censuses array
			int yearCount = _maxYear - _minYear;
			Census[] censuses = new Census[yearCount];

			for (int i = 0; i < yearCount; i++)
			{
				censuses[i] = new Census(_minYear + i);
			}

			//Loop through each person and add them to the censuses for each matched year
			for (int i = 0, personCount = people.Length; i < personCount; i++)
			{
				//Calculate the index modifier to keep the index of the census in line with the next loop
				int indexModifier = people[i].BirthYear - _minYear;

				for (int x = 0, maxX = people[i].DeathYear - people[i].BirthYear; x < maxX; x++)
				{
					censuses[x + indexModifier].PeopleCount++;
				}
			}

			return censuses;
		}

		/// <summary>
		/// Output and display the results of each census in order from most populated to least.
		/// </summary>
		/// <param name="censuses">Censuses to be sorted and displayed.</param>
		private static void OutputCensuses(Census[] censuses)
		{
			censuses = censuses.OrderByDescending(c => c.PeopleCount).ToArray();

			for (int i = 0, count = censuses.Length; i < count; i++)
			{
				Console.WriteLine($"#{i + 1}:\t{censuses[i].Year}\t{censuses[i].PeopleCount} people");
			}
		}
	}
}
