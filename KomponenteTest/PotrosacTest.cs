using Komponente;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomponenteTest
{
    [TestFixture]
    public class PotrosacTest
    {
        [Test]
        [TestCase("Bojler", 200)]
        [TestCase("Frizider", 400.5)]
        [TestCase("Televizor", 300.01)]
        public void PotrosacKonstruktorDobriParametri(string naziv, double potrosnja)
        {
            Potrosac potrosac = new Potrosac(naziv, potrosnja);

            Assert.AreEqual(potrosac.JedinstvenoIme, naziv);
            Assert.AreEqual(potrosac.Potrosnja, potrosnja);

        }

        [Test]
        [TestCase("A", 200)]
        [TestCase("C", 0)]
        [TestCase("Mikser", 0.000001)]
        [TestCase("b", 0.000001)]
        public void PotrosacKonstruktorGranicniParametri(string naziv, double potrosnja)
        {
            Potrosac potrosac = new Potrosac(naziv, potrosnja);

            Assert.AreEqual(potrosac.JedinstvenoIme, naziv);
            Assert.AreEqual(potrosac.Potrosnja, potrosnja);

        }

        [Test]
        [TestCase("", 100)]
        [TestCase("", -300)]
        [TestCase("Kuvalo", -120)]
        [TestCase(null, -120)]
        [TestCase(null, 2000)]
        public void OsobaKonstruktorLosiParametri(string naziv, double potrosnja)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Potrosac potrosac = new Potrosac(naziv, potrosnja);
            }

            );
        }
    }
}
