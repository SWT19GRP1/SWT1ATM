using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SWT1ATM.Factory;
using TransponderReceiver;

namespace SWT1ATM.Unit.Test
{
    [TestFixture]
    class TestAtm
    {
        private bool _ATMMonitorEventWasCalled;
        public Atm _uut { get; set; }

        [SetUp]
        public void Setup()
        {
            _uut = new Atm(Substitute.For<ITrackFilter>());
            _uut.ATMMonitorEvent += (sender, args) => _ATMMonitorEventWasCalled = true;
        }

        [Test]
        public void Atm_OntrackDataRecieved_ListIsEmptyAirplanesIsEmpty()
        {
            _uut.OnTrackDataRecieved(this, new FormattedTransponderDataEventArgs(new List<IVehicle>()));
            Assert.That(_uut.AirCrafts.Count, Is.EqualTo(0));
        }

        [Test]
        public void Atm_OntrackDataRecieved_DataArgsHasOneEntryListHasOneEntry()
        {
            _uut.OnTrackDataRecieved(this, new FormattedTransponderDataEventArgs(new List<IVehicle>() { new Aircraft(0, 0, 0, DateTime.Now, "YOLO69") }));
            Assert.That(_uut.AirCrafts.Count, Is.EqualTo(1));
        }

        [Test]
        public void Atm_OnTrackDataRecieved_AircraftInListNotAddedTwice()
        {
            _uut.AirCrafts.Add(new Aircraft(0, 0, 0, DateTime.Now, "YOLO69"));
            _uut.OnTrackDataRecieved(this, new FormattedTransponderDataEventArgs(new List<IVehicle>() { new Aircraft(0, 0, 0, DateTime.Now, "YOLO69") }));
            Assert.That(_uut.AirCrafts[0].Tag, Is.EqualTo("YOLO69"));
        }

        [Test]
        public void Atm_ATMMonitorEvent_EventIsRaised()
        {
            _uut.OnTrackDataRecieved(this, new FormattedTransponderDataEventArgs(new List<IVehicle>()));
            Assert.That(_ATMMonitorEventWasCalled);
        }

        [Test]
        public void Atm_OnRemoveAirPlainRecievedEvent_AirplaneIsRemoved()
        {
            _uut.AirCrafts.Add(new Aircraft(0, 0, 0, DateTime.Now, "YOLO69"));
            _uut.OnRemoveAirPlainRecievedEvent(this, new FormattedTransponderDataEventArgs(new List<IVehicle>() { new Aircraft(0, 0, 0, DateTime.Now, "YOLO69") }));
            Assert.That(_uut.AirCrafts.Count, Is.EqualTo(0));
        }

        [Test]
        public void Atm_OnRemoveAirPlainRecievedEvent_AirplaneIsNotInList()
        {
            _uut.AirCrafts.Add(new Aircraft(0, 0, 0, DateTime.Now, "SOME23"));
            _uut.OnRemoveAirPlainRecievedEvent(this, new FormattedTransponderDataEventArgs(new List<IVehicle>() { new Aircraft(0, 0, 0, DateTime.Now, "YOLO69") }));
            Assert.That(_uut.AirCrafts.Count, Is.EqualTo(1));
        }
    }
}
