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
    public class SolarniPanelTest
    {
        [Test]
        [TestCase("Solarni panel 1", 200)]
        [TestCase("Solarni panel 2", 500)]
        [TestCase("Solarni panel 3", 100)]
        public void SolarniPanelDobriParametri(string naziv, double snaga)
        {
            SolarniPanel panel = new SolarniPanel(naziv, snaga);

            Assert.AreEqual(panel.Ime, naziv);
            Assert.AreEqual(panel.MaxSnaga, snaga);
        }

        [Test]
        [TestCase("S", 200)]
        [TestCase("T", 0)]
        [TestCase("Solarni panel 2", 0.000001)]
        [TestCase("a", 0.000001)]
        public void SolarniPanelGranicniParametri(string naziv, double snaga)
        {
            SolarniPanel panel = new SolarniPanel(naziv, snaga);

            Assert.AreEqual(panel.Ime, naziv);
            Assert.AreEqual(panel.MaxSnaga, snaga);
        }


        [Test]
        [TestCase("", 200)]
        [TestCase(null, 200)]
        [TestCase("Sol", -200)]
        public void SolarniPanelLosiParametri(string naziv, double snaga)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                SolarniPanel panel = new SolarniPanel(naziv, snaga);

            }
            );
        }
    }
}
