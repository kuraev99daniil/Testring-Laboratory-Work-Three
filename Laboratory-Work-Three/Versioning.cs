using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Laboratory_Work_Three
{
    public class Versioning : IComparable<Versioning>
    {
		private readonly string preRelease;

		private readonly List<int> mainVersionParts;

		public Versioning(string version)
		{
			if (!IsCorrect(version))
			{
				throw new ArgumentException("Значение не корректно!");
			}

			mainVersionParts = new List<int>();

			var splitPreRelease = version.Split("-");

			mainVersionParts = splitPreRelease[0]
				.Split('.')
				.ToList()
				.ConvertAll(value => int.Parse(value));

			if (splitPreRelease.Length > 1)
			{
				preRelease = splitPreRelease[1];
			}
			else
			{
				preRelease = null;
			}
		}

		public int CompareTo(Versioning otherVersion)
		{
			for (int i = 0; i < 3; i++)
			{
				var currentVersion = mainVersionParts[i];
				var version2 = otherVersion.mainVersionParts[i];

				if (currentVersion > version2) return 1;

				if (currentVersion == version2) continue;

				return -1;
			}

			return 0;
		}

		public static bool operator > (Versioning version1, Versioning version2)
		{
			return IsMore(version1, version2);
		}
		public static bool operator < (Versioning version1, Versioning version2)
		{
			return !IsMore(version1, version2);
		}

		private static bool IsCorrect(string version)
		{
			return Regex.IsMatch(version, @"\d+\.\d+\.\d+-?[\w+\.\w+]*");
		}

		private static bool IsMore(Versioning v1, Versioning v2)
		{
			switch (v1.CompareTo(v2))
            {
				case -1:
					{
						return false;
					}
				case 0:
					{
						return ComparePreRelease(v1.preRelease, v2.preRelease) > 0;
					}
				case 1:
                    {
						return true;
                    }
				default:
                    {
						throw new Exception("Неверная работа Comparator");
                    }
			}
		}

		private static int ComparePreRelease(string preRelease1, string preRelease2)
		{
			if (preRelease1 == null && preRelease2 != null) return 1;

			if (preRelease1 == null && preRelease2 == null) return 0;

			if (preRelease1 != null && preRelease2 == null) return -1;

			var splitPreRelease1 = preRelease1.Split(".");
			var splitPreRelease2 = preRelease2.Split(".");

			if (splitPreRelease1.Length > splitPreRelease2.Length) return 1;
			if (splitPreRelease1.Length < splitPreRelease2.Length) return -1;

			for (int i = 0; i < splitPreRelease1.Length; i++)
			{
				var compareResult = string.Compare(splitPreRelease1[i], splitPreRelease2[i]);

				if (compareResult == 0) continue;

				return compareResult;
			}

			return 0;
		}

		public static bool operator >= (Versioning version1, Versioning version2)
		{
			return IsMoreOrEqual(version1, version2);
		}

		public static bool operator <= (Versioning version1, Versioning version2)
		{
			return IsLessOrEqual(version1, version2);
		}

		private static bool IsMoreOrEqual(Versioning v1, Versioning v2)
		{
			switch (v1.CompareTo(v2))
			{
				case -1:
					{
						return false;
					}
				case >= 0:
					{
						return ComparePreRelease(v1.preRelease, v2.preRelease) >= 0;
					}
				default:
					{
						throw new Exception("Неверная работа Comparator");
					}
			}
		}

		private static bool IsLessOrEqual(Versioning v1, Versioning v2)
		{
			switch (v1.CompareTo(v2))
			{
				case 1:
					{
						return false;
					}
				case <= 0:
					{
						return ComparePreRelease(v1.preRelease, v2.preRelease) <= 0;
					}
				default:
					{
						throw new Exception("Неверная работа Comparator");
					}
			}
		}

		public static bool operator ==(Versioning version1, Versioning version2)
		{
			return IsEqual(version1, version2);
		}

		public static bool operator !=(Versioning version1, Versioning version2)
		{
			return !IsEqual(version1, version2);
		}

		private static bool IsEqual(Versioning v1, Versioning v2)
		{
			return v1.ToString() == v2.ToString();
		}

		public override string ToString()
		{
			if (preRelease != null)
			{
				return $"{mainVersionParts[0]}.{mainVersionParts[1]}.{mainVersionParts[2]}-{preRelease}";
			}

			return $"{mainVersionParts[0]}.{mainVersionParts[1]}.{mainVersionParts[2]}";
		}
    }
}
