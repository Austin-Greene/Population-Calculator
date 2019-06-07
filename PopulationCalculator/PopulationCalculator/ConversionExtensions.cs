using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationCalculator
{
	static class ConversionExtensions
	{
		/// <summary>
		/// Uses numerology to convert letters to numbers:
		/// 1 = a, j, s
		/// 2 = b, k, t
		/// 3 = c, l, u
		/// 4 = d, m, v
		/// 5 = e, n, w
		/// 6 = f, o, x
		/// 7 = g, p, y
		/// 8 = h, q, z
		/// 9 = i, r
		/// </summary>
		/// <param name="name">String to convert.</param>
		/// <returns></returns>
		public static int NameToInt(this string name)
		{
			name = name.ToLower();

			string digits = "";

			for (int i = 0, count = name.Length; i < count; i++)
			{
				//Only execute on letters
				if (char.IsLetter(name[i]))
				{
					//Convert the character to an integer, following the numerology pattern
					digits += ((name[i] - 'a') % 9) + 1;
				}
			}

			//If this were true numerology, the digits would be added together.
			return int.Parse(digits);
		}
	}
}
