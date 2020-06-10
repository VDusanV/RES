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
    public class PunjacElekAutoTest
    {
        [Test]
        [TestCase(100)]
        [TestCase(100.0001)]
        [TestCase(5023121)]
        public void PunjacEADobriParametri(double snaga)
        {
            PunjacElekAuto auto = new PunjacElekAuto(snaga);
            Assert.AreEqual(auto.MaxSnagaBaterije, snaga);
            Assert.AreEqual(auto.AutoNaPunjacu, false);
            Assert.AreEqual(auto.DaLiZelimoDaSePuni, true);
            Assert.AreEqual(auto.NapunjenostBaterije, 0);
            Assert.AreEqual(auto.VremePunjenjaOd, new DateTime(2010, 10, 10, 21, 0, 0));
            Assert.AreEqual(auto.VremePunjenjaDo, new DateTime(2010, 10, 10, 6, 0, 0));


        }
        [Test]
        [TestCase(0)]
        [TestCase(0.00000001)]
        public void PunjacEAGranicniParametri(double snaga)
        {
            PunjacElekAuto auto = new PunjacElekAuto(snaga);
            Assert.AreEqual(auto.MaxSnagaBaterije, snaga);
            Assert.AreEqual(auto.AutoNaPunjacu, false);
            Assert.AreEqual(auto.DaLiZelimoDaSePuni, true);
            Assert.AreEqual(auto.NapunjenostBaterije, 0);
            Assert.AreEqual(auto.VremePunjenjaOd, new DateTime(2010, 10, 10, 21, 0, 0));
            Assert.AreEqual(auto.VremePunjenjaDo, new DateTime(2010, 10, 10, 6, 0, 0));


        }
        [Test]
        [TestCase(-100)]
        [TestCase(-100.0001)]
        [TestCase(-5023121)]
        public void PunjacEALosiParametri(double snaga)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                PunjacElekAuto auto = new PunjacElekAuto(snaga);
            }
            );

        }
    }
}
