using Baterija;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaterijaTest
{
    [TestFixture]
    public class BaterijaTest
    {
        private Baterija.Baterija baterija;

        [SetUp]
        public void Setup()
        {
            baterija = new Baterija.Baterija();
        }

        [Test]
        public void BaterijaKonstruktor()
        {
            Assert.IsNotNull(baterija, null);
        }
        

    }
}
