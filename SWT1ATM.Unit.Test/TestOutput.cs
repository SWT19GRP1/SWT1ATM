using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SWT1ATM;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using SWT1ATM.Interfaces;
using SWT1ATM.Output;

namespace SWT1ATM.Unit.Test
{
    [TestFixture]
    public class TestOutput
    {


        private IVehicleFormatter _format;
        private IOutput _uut;
        private string _path;
        private List<IVehicle> _vehicles;
        private IATM _atm;
        private IAtmSeparationCondition _separation;
        #region setup
        [SetUp]
        public void Setup()
        {
            _vehicles = new List<IVehicle>();
            _path = @"c:\Temp\SeparationCondition.txt";
            _format = Substitute.For<IVehicleFormatter>();
            _separation = Substitute.For<IAtmSeparationCondition>();
            _atm = Substitute.For<IATM>();
            var air0 = new Aircraft(1000, 1000, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 123), "XCE321");
            var air1 = new Aircraft(1000, 1000, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 123), "XXE321");
            _vehicles.Add(air0);
            _vehicles.Add(air1);
        }
        #endregion

        /*
        #region FileOutputTests

        [Test]
        public void CreateANewFileWhenNoFileIsPresent()
        {
            _uut = new LogOutput(_format, _separation);
            if (File.Exists(_path))
                File.Delete(_path);
            _separation.SeparationConditionEvent += Raise.EventWith(this, new FormattedTransponderDataEventArgs(_vehicles));
            Assert.That(File.Exists(_path));
            
        }

        [Test]
        public void AppendAFileWhenFileIsPresent()
        {
            _uut = new LogOutput(_format, _separation);
            var separationMock = Substitute.For<IAtmSeparationCondition>();
            _format.VehicleToString(_vehicles[0]).Returns("NewTest");
            if (File.Exists(_path))
                File.Delete(_path);
            var myFileWriter = new System.IO.StreamWriter(_path);
            myFileWriter.Write("Test:");
            myFileWriter.Close();
            _separation.SeparationConditionEvent += Raise.EventWith(this, new FormattedTransponderDataEventArgs(_vehicles));

            var myFile = new System.IO.StreamReader(_path, Encoding.UTF8);
            Assert.That(myFile.ReadToEnd() == "Test:NewTest");
            myFile.Close();
        }
        [Test]
        public void LoggerDoesntThrowErrorWithZeroLenghtList()
        {
            _uut = new LogOutput(_format, _separation);
            List<IVehicle> emptyList = new List<IVehicle>();
            Assert.That(() => _separation.SeparationConditionEvent += Raise.EventWith(this, new FormattedTransponderDataEventArgs(emptyList)), Throws.Nothing);
        }

        #endregion

        #region TerminalOutput
        [Test]
        public void TerminalDoesntThrowErrorWithZeroLengthList()
        {
            _uut = new TerminalOutput(_format,_atm, _separation);
            List<IVehicle> emptyList = new List<IVehicle>();
            Assert.That(() => _atm.ATMMonitorEvent += Raise.EventWith(this, new FormattedTransponderDataEventArgs(emptyList)), Throws.Nothing);
        }

        [Test]
        public void TerminalTwiceRaisedEventLogVehicleDataIsCalledTwice()
        {
            _uut = new TerminalOutput(_format, _atm, _separation);
            _atm.ATMMonitorEvent += Raise.EventWith(this, new FormattedTransponderDataEventArgs(_vehicles));
            _atm.ATMMonitorEvent += Raise.EventWith(this, new FormattedTransponderDataEventArgs(_vehicles));
            _format.Received(2);
        }
        #endregion
        

        #region VehicleToSTring
        [Test]
        public void AirplaneFormatterReturnExpectedString()
        {
            var tstamp = new DateTime();
            var plane = new Aircraft(10, 10, 10, tstamp, "air0");
            string expectedString = "Tag: air0 Coordinates X: 00010, Y: 00010, Z: 00010, Direction: 000 degrees, Speed: 000.00 m/s, Date: 01/01/0001 00:00:00\n";
            _format = new AirplaneFormatter();
            Assert.That(_format.VehicleToString(plane).Equals(expectedString));
        }

        #endregion
    */
    }
}