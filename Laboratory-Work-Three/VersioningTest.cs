using NUnit.Framework;
using System;

namespace Laboratory_Work_Three
{
    [TestFixture]
    class VersioningTest
    {
		[Test]
		public void MoreTest()
		{
			Assert.Throws<ArgumentException>(() =>
			{
                _ = new Versioning("1.0,0-beta.alpha") > new Versioning("1.0.0-beta.1");
			});

			Assert.Throws<ArgumentException>(() =>
			{
				_ = new Versioning("QWERTY") > new Versioning("1.0.0-beta.1");
			});

			Assert.IsTrue(new Versioning("1.0.0-alpha.beta") > new Versioning("1.0.0-alpha.1"));
			Assert.IsTrue(new Versioning("1.0.0") > new Versioning("1.0.0-alpha"));
			Assert.IsTrue(new Versioning("1.0.1") > new Versioning("1.0.0"));
			Assert.IsTrue(new Versioning("1.1.0") > new Versioning("1.0.0"));
			Assert.IsTrue(new Versioning("1.1.1") > new Versioning("1.1.0"));
		}

		[Test]
		public void LessTest()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				_ = new Versioning("?.!,0-beta.alpha") > new Versioning("1.0.0-alpha.1");
			});

			Assert.IsTrue(new Versioning("1.0.0-beta.1") < new Versioning("1.0.0-beta.alpha"));
			Assert.IsFalse(new Versioning("1.0.0") < new Versioning("1.0.0-alpha"));
		}

		[Test]
		public void MoreOrEqualTest()
		{
			Assert.IsTrue(new Versioning("1.1.0") >= new Versioning("1.0.0"));
			Assert.IsTrue(new Versioning("1.0.0") >= new Versioning("1.0.0"));
			Assert.IsTrue(new Versioning("1.0.0") >= new Versioning("1.0.0-alpha"));
		}

		[Test]
		public void LessOrEqualTest()
		{
			Assert.IsTrue(new Versioning("1.0.0") <= new Versioning("1.1.0"));
			Assert.IsTrue(new Versioning("1.1.0") <= new Versioning("1.1.0"));
			Assert.IsTrue(new Versioning("1.1.0-alpha") <= new Versioning("1.1.0"));
			Assert.IsTrue(new Versioning("1.1.0-alpha") <= new Versioning("1.1.0-alpha"));
		}

		[Test]
		public void EqualTest()
		{
			Assert.IsTrue(new Versioning("1.0.0") == new Versioning("1.0.0"));
			Assert.IsTrue(new Versioning("1.0.0") == new Versioning("1.0.0"));
			Assert.IsTrue(new Versioning("1.1.1-beta") == new Versioning("1.1.1-beta"));
		}

		[Test]
		public void NoEqualTest()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				bool metka = new Versioning("1.0,0-beta.alpha") > new Versioning("qwerty");
			});

			Assert.IsTrue(new Versioning("1.0.0") != new Versioning("1.0.1"));
			Assert.IsFalse(new Versioning("1.0.0") != new Versioning("1.0.0"));
		}

		[Test]
		public void ToStringTest()
		{
			Assert.AreEqual("1.0.0", new Versioning("1.0.0").ToString());
			Assert.AreEqual("1.1.1-alpha", new Versioning("1.1.1-alpha").ToString());
		}
	}
}
