using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SWT1ATM.Unit.Test
{
    [TestFixture]
    [Author("Christian Melsen")]
    public class AtmrtSeparationCondition
    {
        private AtmRtSeparationCondition _uut;
        private List<Aircraft> _aircrafts;
        [SetUp]
        public void Setup()
        {
            _uut = new AtmRtSeparationCondition(5000, 500);
            _aircrafts = new List<Aircraft>();

            var air0 = new Aircraft(1000, 1000, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 123), "XCE321");
            var air1 = new Aircraft(1000, 6001, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 125), "XCF892");
            var air2 = new Aircraft(1000, 6000, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 127), "XCF893");
            var air3 = new Aircraft(1000, 5999, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 129), "XCF894");
            var air4 = new Aircraft(4535, 4535, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 131), "XCF895");
            var air5 = new Aircraft(4536, 4536, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 133), "XCF896");
            var air6 = new Aircraft(1000, 1000, 1501, new DateTime(2019, 06, 06, 12, 12, 12, 135), "XCF897");

            _aircrafts.Add(air0);
            _aircrafts.Add(air1);
            _aircrafts.Add(air2);
            _aircrafts.Add(air3);
            _aircrafts.Add(air4);
            _aircrafts.Add(air5);
            _aircrafts.Add(air6);
        }

        #region SeparationDetectionUnitTests

        [Test]
        public void SeparationDetection_TwoPlanesThatAreNotClose_ResultIsFalse()
        {
            var result = _uut.SeparationDetection(_aircrafts[0], _aircrafts[1]);
            Assert.That(result, Is.False);
        }

        [Test]
        public void SeparationDetection_TwoPlanesThatAreTooClose_ResultIsTrue()
        {
            var result = _uut.SeparationDetection(_aircrafts[0], _aircrafts[2]);
            Assert.That(result, Is.True);
        }

        [Test]
        public void SeparationDetection_TwoPlanesThatAreTooClose2_ResultIsTrue()
        {
            var result = _uut.SeparationDetection(_aircrafts[0], _aircrafts[3]);
            Assert.That(result, Is.True);
        }

        [Test]
        public void SeparationDetection_TwoPlanesThatAreTooCloseMixDimension_ResultIsTrue()
        {
            var result = _uut.SeparationDetection(_aircrafts[0], _aircrafts[4]);
            Assert.That(result, Is.True);
        }

        [Test]
        public void SeparationDetection_TwoPlanesThatAreNotCloseMixDimension_ResultIsFalse()
        {
            var result = _uut.SeparationDetection(_aircrafts[0], _aircrafts[5]);
            Assert.That(result, Is.False);
        }

        [Test]
        public void SeparationDetection_TwoPlanesThatAreTooCloseButHeightDiffIsAcceptable_ResultIsFalse()
        {
            var result = _uut.SeparationDetection(_aircrafts[0], _aircrafts[6]);
            Assert.That(result, Is.False);
        }

        #endregion

        #region SeparationConditionUnitTests

        #endregion
    }
}
