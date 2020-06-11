using ElektroDistribucija;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektroDistribucijaTest
{
    [TestFixture]
    public class ElektrodistribucijaTest
    {
        private ElektroDistribucija.Elektrodistribucija elektrodistribucija;

        [SetUp]
        public void Setup()
        {
            elektrodistribucija = new Elektrodistribucija();
        }


        [Test]
        [TestCase(200.0, 300.0)]
        [TestCase(400.0, 500.0)]
        public void ElektroDistribucijaDobriParametri(double a, double b)
        {
            Elektrodistribucija e = new Elektrodistribucija(a, b);
            Assert.AreEqual(e.CenaPoKilovatcasu, b);
            Assert.AreEqual(e.SnagaRazmene, a);

            //izracunajCenuTest
            Assert.IsNotNull(e.IzracunajCenu());

        }
        [Test]
        [TestCase(1.0, 0.0)]
        [TestCase(0.0, 1.0)]
        [TestCase(0.0, 0.0)]
        public void ElektroDistribucijaGranicniParametri(double a, double b)
        {
            Elektrodistribucija e = new Elektrodistribucija(a, b);
            Assert.AreEqual(e.CenaPoKilovatcasu, b);
            Assert.AreEqual(e.SnagaRazmene, a);

        }

        [Test]
        [TestCase(-1.0, 300.0)]
        [TestCase(300.0, -300.0)]
        [TestCase(-300.0, -300.0)]
        public void ElektroDistribucijaLosiParametri(double a, double b)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Elektrodistribucija e = new Elektrodistribucija(a, b);

            });
        }

        [Test]
        public void ElektrodistribucijaKonstruktor()
        {
            Assert.AreNotEqual(elektrodistribucija, null);
        }

        

    }
}
