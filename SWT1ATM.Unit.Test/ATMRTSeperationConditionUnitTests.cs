using System;
using System.Collections.Generic;
using ATMV3;
using NUnit.Framework;

namespace SWT1ATM.Unit.Test
{
    [TestFixture]
    [Author("Christian Melsen")]
    public class ATMRTSeparationCondition
    {
        private ATM_RT_Separation_Condition _uut;
        private List<Aircraft> aircrafts;
        [SetUp]
        public void setup()
        {
            _uut = new ATM_RT_Separation_Condition(5000, 500);
            aircrafts = new List<Aircraft>();

            var air0 = new Aircraft(1000, 1000, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 123), "XCE321", 0, 0);
            var air1 = new Aircraft(1000, 6001, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 125), "XCF892", 0, 0);
            var air2 = new Aircraft(1000, 6000, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 127), "XCF893", 0, 0);
            var air3 = new Aircraft(1000, 5999, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 129), "XCF894", 0, 0);
            var air4 = new Aircraft(4535, 4535, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 131), "XCF895", 0, 0);
            var air5 = new Aircraft(4536, 4536, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 133), "XCF896", 0, 0);
            var air6 = new Aircraft(1000, 1000, 1501, new DateTime(2019, 06, 06, 12, 12, 12, 135), "XCF897", 0, 0);

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

        #endregion
    }
}
