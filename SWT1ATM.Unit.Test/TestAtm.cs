using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SWT1ATM.Factory;

namespace SWT1ATM.Unit.Test
{
    [TestFixture]
    class TestAtm
    {
        
     private Atm _uut;

            [SetUp]
            public void Setup()
            {
                _uut = new Atm(Substitute.For<ITrackFilter>());
            }

     
        
    }
}
