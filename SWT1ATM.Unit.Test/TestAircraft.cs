using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;


namespace SWT1ATM.Unit.Test
{

    [TestFixture]
    class TestAircraft
    {
        private Aircraft _uut;
        private Aircraft _isUutAircraft;
        private Aircraft _isUutAircraft2;
        private Aircraft _notUutAircraft;

        [SetUp]
        public void Setup()
        {
            _uut = new Aircraft(6000, 5000, 10000, new DateTime(2019, 06, 06, 12, 12, 12, 123), "ATR546");
            _notUutAircraft = new Aircraft(8800, 15000, 8000, new DateTime(2019, 06, 12, 4, 16, 12, 123), "ATT746");
            _isUutAircraft = new Aircraft(5000, 5000, 6000, new DateTime(2019, 06, 06, 14, 12, 12, 123), "ATR546");
            _isUutAircraft2 = new Aircraft(8000, 3000, 6000, new DateTime(2019, 06, 06, 16, 12, 12, 123), "ATR546");



        }

        [Test]
        public void Aircraft_Timestamp_Updates()
        {
           _uut.Update(_isUutAircraft);

            Assert.That((_uut.Timestamp), Is.EqualTo(_isUutAircraft.Timestamp));
        }

        [Test]
        public void Aircraft_Timestamp_No_Update()
        {
            _uut.Update(_notUutAircraft);

            Assert.That((_uut.Timestamp), Is.Not.EqualTo(_notUutAircraft.Timestamp));
        }

        [Test]
        public void Aircraft_CorrectSpeed_is_found()
        {
            _uut.Update(_isUutAircraft);

            Assert.That((_uut.Speed), Is.EqualTo(0.572).Within(0.005));
        }
        [Test]
        public void Aircraft_CorrectSpeed_is_found2()
        {

            _uut.Update(_isUutAircraft2);



            Assert.That((_uut.Speed), Is.EqualTo(0.340).Within(0.005));

        }

        [Test]
        public void Aircraft_Correct_X()
        {
            _uut.Update(_isUutAircraft);

            Assert.That((_uut.X), Is.EqualTo(_isUutAircraft.X));
        }

        [Test]
        public void Aircraft_Correct_Y()
        {
            _uut.Update(_isUutAircraft);

            Assert.That((_uut.Y), Is.EqualTo(_isUutAircraft.Y));
        }

        [Test]
        public void Aircraft_Correct_Z()
        {
            _uut.Update(_isUutAircraft);

            Assert.That((_uut.Z), Is.EqualTo(_isUutAircraft.Z));
        }

        [Test]
        public void Aircraft_Correct_Direction_DeltaXisNegative()
        {
            _uut.Update(_isUutAircraft);

            Assert.That((_uut.Direction), Is.EqualTo(270).Within(0.0005));
        }

        [Test]
        public void Aircraft_Correct_Direction_DeltaXisPositive()
        {
            _uut.Update(_isUutAircraft2);

            Assert.That((_uut.Direction), Is.EqualTo(135).Within(0.0005));
        }
    }
}
