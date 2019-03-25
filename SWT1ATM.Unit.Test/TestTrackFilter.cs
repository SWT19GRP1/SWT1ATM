using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;
using SWT1ATM.Factory;

namespace SWT1ATM.Unit.Test
{
    [TestFixture]
    public class TestTrackFilter
    {
        private bool _outWasCalled;
        private bool _inWasCalled;
        private bool _transponderWasCalled;
        private ITransponderReceiver _transponderReceiver;
        private TrackFilter _uut;
        private IVehicle _aircraft;
        private List<IVehicle> _aircraftlist;

        [SetUp]
        public void Setup()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _aircraft = new Aircraft(0,0,0,DateTime.Now,"YOLO69");

            _uut = new TrackFilter(_transponderReceiver, new ATM_Factory());
            _aircraftlist = new List<IVehicle> {_aircraft};

            _transponderReceiver.TransponderDataReady += (sender, args) => _transponderWasCalled = true;
            _uut.AirTrackToMonitorEvent += (sender, args) => _inWasCalled = true;
            _uut.AirTrackOutSideMonitorEvent += (sender, args) => _outWasCalled = true;
        }

        [Test]
        public void TrackFiler_DefaultValues_SetAndGetCorrect()
        {
            Assert.That(_uut.XLength, Is.EqualTo(70000));   
            Assert.That(_uut.YWidth, Is.EqualTo(70000));   
            Assert.That(_uut.ZHeight, Is.EqualTo(19500));   
            Assert.That(_uut.XOffset, Is.EqualTo(10000));   
            Assert.That(_uut.YOffset, Is.EqualTo(10000));   
            Assert.That(_uut.ZOffset, Is.EqualTo(500));              
        }


        [Test]
        public void GetDate__ReturnCorrectDateTime()
        {
            string str = "20190708121414123";
            DateTime dt = _uut.GetDate(str);
            Assert.That(dt, Is.EqualTo(new DateTime(2019,07,08,12,14,14,123)));
        }


        [Test]
        public void TrackFilter_ReactsToTransponderEvents()
        {
            _transponderReceiver.TransponderDataReady += Raise.EventWith(this, new RawTransponderDataEventArgs(new List<string>()));
            Assert.True(_transponderWasCalled);
        }

        [Test]
        public void TrackFiler_AirTrackToMonitorEvent_raisesEvent()
        {
            _uut.OnAirTrackToMonitorEvent(_aircraftlist);
            Assert.That(_inWasCalled);
        }

        [Test]
        public void TrackFilter_AirTrackOutSideMonitorEvent_RaisesEvent()
        {
            _uut.OnAirTrackOutSideMonitorEvent(_aircraftlist);
            Assert.That(_outWasCalled);
        }

        [Test]
        public void TrackFilter_HandlerOnRaiseTrackInsideMonitoringAreaEvent_CorrectEventRaised()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this,new RawTransponderDataEventArgs(new List<string>(){ "ATR423;39045;12932;14000;20151006213456789" }));
            Assert.That(_uut.vehiclesIn.Count, Is.EqualTo(1));
        }

        [Test]
        public void TrackFilter_HandlerOnRaiseTrackInsideMonitoringAreaEvent_TwoEventTwoInList()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;39045;12932;14000;20151006213456789", "YOLO69;39045;12932;14000;20151006213456789" }));
            Assert.That(_uut.vehiclesIn.Count, Is.EqualTo(2));
        }

        [Test]
        public void TrackFilter_HandlerOnRaiseTrackInsideMonitoringAreaEvent_TwoEventSameAiplaneOneInList()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;39045;12932;14000;20151006213456789" }));
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;39045;12932;14000;20151006213456789" }));
            Assert.That(_uut.vehiclesIn.Count, Is.EqualTo(1));
        }

        [Test]
        public void TrackFilter_zCoordinateTooHigh_IsNotAddedToVehiclesIn()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;39045;12932;20001;20151006213456789" }));
            Assert.That(_uut.vehiclesIn.Count, Is.EqualTo(0));
        }

        [Test]
        public void TrackFilter_zCoordinateTooHigh_IsAddedToVehiclesOut()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;39045;12932;20001;20151006213456789" }));
            Assert.That(_uut.vehiclesOut.Count, Is.EqualTo(1));
        }

        [Test]
        public void TrackFilter_zCoordinateTooLow_IsNotAddedToVehiclesIn()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;71000;45000;499;20151006213456789" }));
            Assert.That(_uut.vehiclesIn.Count, Is.EqualTo(0));
        }

        [Test]
        public void TrackFilter_zCoordinateTooLow_IsAddedToVehiclesOut()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;71000;45000;499;20151006213456789" }));
            Assert.That(_uut.vehiclesOut.Count, Is.EqualTo(1));
        }


        [Test]
        public void TrackFilter_xCoordinateTooHigh_IsNotAddedToVehiclesIn()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;80001;12932;17000;20151006213456789" }));
            Assert.That(_uut.vehiclesIn.Count, Is.EqualTo(0));
        }

        [Test]
        public void TrackFilter_xCoordinateTooHigh_IsAddedToVehiclesOut()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;80001;12932;17000;20151006213456789" }));
            Assert.That(_uut.vehiclesOut.Count, Is.EqualTo(1));
        }

        [Test]
        public void TrackFilter_xCoordinateTooLow_IsNotAddedToVehiclesIn()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;9999;45000;17000;20151006213456789" }));
            Assert.That(_uut.vehiclesIn.Count, Is.EqualTo(0));
        }

        [Test]
        public void TrackFilter_xCoordinateTooLow_IsAddedToVehiclesOut()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;9999;45000;17000;20151006213456789" }));
            Assert.That(_uut.vehiclesOut.Count, Is.EqualTo(1));
        }


        [Test]
        public void TrackFilter_yCoordinateTooHigh_IsNotAddedToVehiclesIn()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;71000;80001;17000;20151006213456789" }));
            Assert.That(_uut.vehiclesIn.Count, Is.EqualTo(0));
        }

        [Test]
        public void TrackFilter_yCoordinateTooHigh_IsAddedToVehiclesOut()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;71000;80001;17000;20151006213456789" }));
            Assert.That(_uut.vehiclesOut.Count, Is.EqualTo(1));
        }


        [Test]
        public void TrackFilter_yCoordinateTooLow_IsNotAddedToVehiclesIn()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;10150;9999;17000;20151006213456789" }));
            Assert.That(_uut.vehiclesIn.Count, Is.EqualTo(0));
        }



        [Test]
        public void TrackFilter_yCoordinateTooLow_IsAddedToVehiclesOut()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;10150;9999;17000;20151006213456789" }));
            Assert.That(_uut.vehiclesOut.Count, Is.EqualTo(1));
        }


        [Test]
        public void Trackfilter_AicraftListDataAndRawEventArgsData_TagIsEqual()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;10150;15000;17000;20151006213456789" }));
            Assert.That(_uut.vehiclesIn[0].Tag, Is.EqualTo("ATR423"));
        }

        [Test]
        public void Trackfilter_AicraftListDataAndRawEventArgsData_XIsEqual()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;10150;15000;17000;20151006213456789" }));
            Assert.That(_uut.vehiclesIn[0].X, Is.EqualTo(10150));
        }


        [Test]
        public void Trackfilter_AicraftListDataAndRawEventArgsData_YIsEqual()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;10150;15000;17000;20151006213456789" }));
            Assert.That(_uut.vehiclesIn[0].Y, Is.EqualTo(15000));
        }


        [Test]
        public void Trackfilter_AicraftListDataAndRawEventArgsData_ZIsEqual()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;10150;15000;17000;20151006213456789" }));
            Assert.That(_uut.vehiclesIn[0].Z, Is.EqualTo(17000));
        }


        [Test]
        public void Trackfilter_AicraftListDataAndRawEventArgsData_DateIsEqual()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this, new RawTransponderDataEventArgs(new List<string>() { "ATR423;10150;15000;17000;20151006213456789" }));
            Assert.That(_uut.vehiclesIn[0].Timestamp, Is.EqualTo(new DateTime(2015,10,06,21,34,56,789)));
        }
        [Test]
        public void Trackfilter_AircraftAfterConstruction_DataIsCorrect()
        {
            Assert.That(_aircraft.Tag, Is.EqualTo("YOLO69"));
            Assert.That(_aircraft.X, Is.EqualTo(0));
            Assert.That(_aircraft.Y, Is.EqualTo(0));
            Assert.That(_aircraft.Z, Is.EqualTo(0));
            Assert.That(_aircraft.Timestamp.Second, Is.EqualTo(DateTime.Now.Second).Within(1));
        }

        [Test]
        public void TrackFilter_FormattedTransponderDataEventArgs_Get()
        {
            var data = new FormattedTransponderDataEventArgs(_aircraftlist);
            Assert.That(data.vehicles, Is.EqualTo(_aircraftlist));
        }


    }
}
