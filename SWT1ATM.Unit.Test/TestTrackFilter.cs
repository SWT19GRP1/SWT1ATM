using System;
using System.Collections.Generic;
using System.Xml.Schema;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using SWT1ATM;
using TransponderReceiver;

namespace SWT1ATM.Unit.Test
{
    [TestFixture]
    public class TestTrackFilter
    {
        public bool WasCalled;
        private ITransponderReceiver _transponderReceiver;
        private TrackFilter _uut;
        private TrackfilterDto _dto;

        [SetUp]
        public void Setup()
        {
            WasCalled = false;
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _uut = new TrackFilter(_transponderReceiver);
            _dto = new TrackfilterDto("ATR423", 39045, 12932, 1400, new DateTime(2015, 11, 06, 21, 34, 56, 789));


            _transponderReceiver.TransponderDataReady += (sender, args) => WasCalled = true;
            _uut.AirTrackToMonitorEvent += (sender, args) =>  WasCalled = true;

        }

        [Test]
        public void TrackFiler_DefaultValues_SetAndGetCorrect()
        {
            Assert.That(_uut.XLength, Is.EqualTo(80000));   
            Assert.That(_uut.YWidth, Is.EqualTo(80000));   
            Assert.That(_uut.ZHeight, Is.EqualTo(19500));   
            Assert.That(_uut.XOffset, Is.EqualTo(0));   
            Assert.That(_uut.YOffset, Is.EqualTo(0));   
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
            _transponderReceiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(new List<string>()));
            Assert.That(WasCalled);
        }

        //[Test]
        //public void TrackFilter_OnFormattedDataEventRaisedWithRightValues()
        //{
        //    _uut.AirTrackToMonitorEvent += Raise.EventWith(new FormattedTransponderDataEventArgs(_dto));
        //    Assert.That(WasCalled);
        //}

        [Test]
        public void TrackFilter_OnFormattedDataEvent_RaisesEvent()
        {
            _uut.OnAirTrackToMonitorEvent(_dto);
            Assert.That(WasCalled);
        }

        [Test]
        public void TrackFilter_HandlerOnRaiseTrackInsideMonitoringAreaEvent_CorrectEventRaised()
        {
            _uut.HandlerOnRaiseTrackInsideMonitoringAreaEvent(this,new RawTransponderDataEventArgs(new List<string>(){ "ATR423;39045;12932;14000;20151006213456789" }));
            Assert.That(WasCalled);
        }


        [Test]
        public void TrackFilter_zCoordinateTooHigh_DoesNotCallEvent()
        {
            _uut.ZHeight = 20001;
            Assert.That(WasCalled, Is.EqualTo(false));
        }

        [Test]
        public void TrackFilter_zCoordinateTooLow_DoesNotCallEvent()
        {
            _uut.ZHeight = 499;
            Assert.That(WasCalled, Is.EqualTo(false));
        }

        [Test]
        public void TrackFilter_xCoordinateTooHigh_DoesNotCallEvent()
        {
            _uut.XLength = 80001;

            Assert.That(WasCalled, Is.EqualTo(false));
        }

        [Test]
        public void TrackFilter_xCoordinateTooLow_DoesNotCallEvent()
        {
            _uut.XLength = -1;
            Assert.That(WasCalled, Is.EqualTo(false));
        }

        [Test]
        public void TrackFilter_yCoordinateTooHigh_DoesNotCallEvent()
        {
            _uut.YWidth = 80001;

            Assert.That(WasCalled, Is.EqualTo(false));
        }

        [Test]
        public void TrackFilter_yCoordinateTooLow_DoesNotCallEvent()
        {
            _uut.YWidth = -1;

            Assert.That(WasCalled, Is.EqualTo(false));
        }

        [Test]
        public void TrackfilterDTO_AfterConstruction_DataIsCorrect()
        {
            Assert.That(_dto.Tag, Is.EqualTo("ATR423"));
            Assert.That(_dto.X, Is.EqualTo(39045));
            Assert.That(_dto.Y, Is.EqualTo(12932));
            Assert.That(_dto.Z, Is.EqualTo(1400));
            Assert.That(_dto.Time, Is.EqualTo(new DateTime(2015, 11, 06, 21, 34, 56, 789)));
        }



    }
}
