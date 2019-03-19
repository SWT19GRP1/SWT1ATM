using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using SWT1ATM;

namespace SWT1ATM.Unit.Test
{
    [TestFixture]
    [Author("Christian Melsen")]
    public class ATMRTSeperationConditionUnitTests
    {
        private ATMRTSeparationCondition _uut;
        private List<IVehicle> aircrafts;
        [SetUp]
        public void setup()
        {
            _uut = new ATMRTSeparationCondition(5000, 500);
            aircrafts = new List<IVehicle>();

            var air0 = new Aircraft(1000, 1000, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 123), "XCE321");
            var air1 = new Aircraft(1000, 6001, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 125), "XCF892");
            var air2 = new Aircraft(1000, 6000, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 127), "XCF893");
            var air3 = new Aircraft(1000, 5999, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 129), "XCF894");
            var air4 = new Aircraft(4535, 4535, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 131), "XCF895");
            var air5 = new Aircraft(4536, 4536, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 133), "XCF896");
            var air6 = new Aircraft(1000, 1000, 1501, new DateTime(2019, 06, 06, 12, 12, 12, 135), "XCF897");

            aircrafts.Add(air0);
            aircrafts.Add(air1);
            aircrafts.Add(air2);
            aircrafts.Add(air3);
            aircrafts.Add(air4);
            aircrafts.Add(air5);
            aircrafts.Add(air6);
        }

        #region SeparationDetectionUnitTests

        [Test]
        public void SeparationDetection_TwoPlanesThatAreNotClose_ResultIsFalse()
        {
            var result = _uut.SeparationDetection(aircrafts[0], aircrafts[1]);
            Assert.That(result, Is.False);
        }

        [Test]
        public void SeparationDetection_TwoPlanesThatAreTooClose_ResultIsTrue()
        {
            var result = _uut.SeparationDetection(aircrafts[0], aircrafts[2]);
            Assert.That(result, Is.True);
        }

        [Test]
        public void SeparationDetection_TwoPlanesThatAreTooClose2_ResultIsTrue()
        {
            var result = _uut.SeparationDetection(aircrafts[0], aircrafts[3]);
            Assert.That(result, Is.True);
        }

        [Test]
        public void SeparationDetection_TwoPlanesThatAreTooCloseMixDimension_ResultIsTrue()
        {
            var result = _uut.SeparationDetection(aircrafts[0], aircrafts[4]);
            Assert.That(result, Is.True);
        }

        [Test]
        public void SeparationDetection_TwoPlanesThatAreNotCloseMixDimension_ResultIsFalse()
        {
            var result = _uut.SeparationDetection(aircrafts[0], aircrafts[5]);
            Assert.That(result, Is.False);
        }

        [Test]
        public void SeparationDetection_TwoPlanesThatAreTooCloseButHeightDiffIsAcceptable_ResultIsFalse()
        {
            var result = _uut.SeparationDetection(aircrafts[0], aircrafts[6]);
            Assert.That(result, Is.False);
        }

        #endregion

        #region SeparationConditionUnitTests

        [Test]
        public void SeparationCondition_ListContainsAircraftThatAreTooClose_EventRaised()
        {
            List<IVehicle> testList = new List<IVehicle>();
            testList.Add(aircrafts[0]);
            testList.Add(aircrafts[3]);

            var outputter = Substitute.For<IOutput>();
            _uut.SeparationConditionEvent += outputter.LogVehicleData;

            _uut.UpdateSeparationDetection(testList);

            outputter.Received().LogVehicleData(Arg.Any<object>(), Arg.Any<FormattedTransponderDataEventArgs>());
        }

        [Test]
        public void SeparationCondition_FullList_13EventsRaised()
        {
            var outputter = Substitute.For<IOutput>();
            _uut.SeparationConditionEvent += outputter.LogVehicleData;

            _uut.UpdateSeparationDetection(aircrafts);

            outputter.Received(13).LogVehicleData(Arg.Any<object>(), Arg.Any<FormattedTransponderDataEventArgs>());
        }

        [Test]
        public void SeparationCondition_ListContainsAircraftThatAreNotTooClose_NoEventsRaised()
        {
            List<IVehicle> testList = new List<IVehicle>();
            testList.Add(aircrafts[0]);
            testList.Add(aircrafts[5]);

            var outputter = Substitute.For<IOutput>();
            _uut.SeparationConditionEvent += outputter.LogVehicleData;

            _uut.UpdateSeparationDetection(testList);

            outputter.DidNotReceive().LogVehicleData(Arg.Any<object>(), Arg.Any<FormattedTransponderDataEventArgs>());
        }

        #endregion
    }
}
