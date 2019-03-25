using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SWT1ATM.Factory;
using SWT1ATM.Output;
using TransponderReceiver;
namespace SWT1ATM.Unit.Test
{
    [TestFixture]
    class TestFactory
    {
        private ATM_Factory _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new ATM_Factory();
        }

        [Test]
        public void CreateATM_FilterIsCreated()
        { 

            var test = _uut.CreateAtm(Substitute.For<ITrackFilter>());
            Assert.That(test, Is.TypeOf<Atm>());
          
        }

        [Test]
        public void CreateInstanceAircraft_AircraftIsCreated()
        {
       

            var test = _uut.CreateInstanceAirCraft(1, 1, 1, DateTime.MinValue, "test");

            Assert.That((test.Tag),Is.EqualTo("test"));
            Assert.That((test.X),Is.EqualTo(1));
            Assert.That((test.Y),Is.EqualTo(1));
            Assert.That((test.Z),Is.EqualTo(1));
            Assert.That((test.Timestamp),Is.EqualTo(DateTime.MinValue));
        
        }

        [Test]
        public void CreateInstanceTrackFilter_TrackFilterIsCreated()
        {

            var test = _uut.CreateInstanceTrackFilter(Substitute.For<ITransponderReceiver>(), Substitute.For<Abstract_Factory>(),1, 1, 1, 1, 1, 1);

            Assert.That(test,Is.TypeOf<TrackFilter>());

        }

        [Test]
        public void CreateInstanceAirplaneFormatter_AirplaneFormatterIsCreated()
        {

            var test = _uut.CreateInstanceAirplaneFormatter();

            Assert.That(test,Is.TypeOf<AirplaneFormatter>());


        }

        [Test]

        public void CreateInstanceTerminalOutput_TerminalOutputIsCreated()
        {
            var uut = new ATM_Factory();

            var test = uut.CreateInstanceTerminalOutput(Substitute.For<IVehicleFormatter>(),new Atm(Substitute.For<ITrackFilter>()));

            Assert.That(test, Is.TypeOf<TerminalOutput>());
        }

        [Test]
        public void CreateInstanceLogOutput_InstanceLogOutputIsCreated()
        {
            var test = _uut.CreateInstanceLogOutput(Substitute.For<IVehicleFormatter>(), Substitute.For<ITrackFilter>());

            Assert.That(test, Is.TypeOf<LogOutput>());
            
        }

        [Test]
        public void CreateInstanceAtmSeparationCondition_AtmSeparationConditionIsCreated()
        {
            var test = _uut.CreateInstanceAtmSeparationCondition(new Atm(new TrackFilter(Substitute.For<ITransponderReceiver>(), new ATM_Factory())), 1, 1);

            Assert.That(test,Is.TypeOf<ATMRTSeparationCondition>());
        }
    }
}
