using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Laboratory_Work_Three
{
    public class Versioning
    {
		private List<string> versionParts;

		public Versioning(string version)
		{
			if (!IsCorrect(version))
			{
				throw new ArgumentException("Значение не корректно!");
			}

			versionParts = new List<string>();

			var splitPreRelease = version.Split("-");

			versionParts = splitPreRelease[0]
				.Split('.')
				.ToList();

			if (splitPreRelease.Length > 1)
			{
				versionParts.Add(splitPreRelease[1]);
			}
			else
			{
				versionParts.Add(null);
			}
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
			var versionParts1 = v1.versionParts;
			var versionParts2 = v2.versionParts;

			for (int i = 0; i < 3; i++)
			{
				var version1 = int.Parse(versionParts1[i]);
				var version2 = int.Parse(versionParts2[i]);

				if (version1 > version2) return true;

				if (version1 == version2) continue;

				return false;
			}

			return ComparePreRelease(versionParts1[3], versionParts2[3]) > 0;
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
				var splitPreReleaseChar1 = splitPreRelease1[i].ToCharArray();
				var splitPreReleaseChar2 = splitPreRelease2[i].ToCharArray();

				if (splitPreReleaseChar1.Length > splitPreReleaseChar2.Length) return 1;
				if (splitPreReleaseChar1.Length < splitPreReleaseChar2.Length) return -1;

				for (int j = 0; j < splitPreReleaseChar1.Length; j++)
				{
					var isDigitChar1 = char.IsDigit(splitPreReleaseChar1[j]);
					var isDigitChar2 = char.IsDigit(splitPreReleaseChar2[j]);

					if (!isDigitChar1 && isDigitChar2)
					{
						return 1;
					}

					if (isDigitChar1 && !isDigitChar2)
					{
						return -1;
					}

					if (isDigitChar1 && isDigitChar2)
					{
						var digit1 = int.Parse(splitPreReleaseChar1[j].ToString());
						var digit2 = int.Parse(splitPreReleaseChar2[j].ToString());

						return digit1 > digit2 ? 1 : -1;
					}

					if (!isDigitChar1 && !isDigitChar2)
					{
						if (splitPreReleaseChar1[j] > splitPreReleaseChar2[j]) return 1;
						if (splitPreReleaseChar1[j] < splitPreReleaseChar2[j]) return -1;
					}
				}
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
			var versionParts1 = v1.versionParts;
			var versionParts2 = v2.versionParts;

			for (int i = 0; i < 3; i++)
			{
				var version1 = int.Parse(versionParts1[i]);
				var version2 = int.Parse(versionParts2[i]);

				if (version1 > version2) return true;

				if (version1 == version2) continue;

				return false;
			}

			return ComparePreRelease(versionParts1[3], versionParts2[3]) >= 0;
		}

		private static bool IsLessOrEqual(Versioning v1, Versioning v2)
		{
			var versionParts1 = v1.versionParts;
			var versionParts2 = v2.versionParts;

			for (int i = 0; i < 3; i++)
			{
				var version1 = int.Parse(versionParts1[i]);
				var version2 = int.Parse(versionParts2[i]);

				if (version1 < version2) return true;

				if (version1 == version2) continue;

				return false;
			}

			return ComparePreRelease(versionParts1[3], versionParts2[3]) <= 0;
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
			if (versionParts[3] != null)
			{
				return $"{versionParts[0]}.{versionParts[1]}.{versionParts[2]}-{versionParts[3]}";
			}

			return $"{versionParts[0]}.{versionParts[1]}.{versionParts[2]}";
		}
	}
}
