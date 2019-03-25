using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SWT1ATM;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using SWT1ATM.Output;

namespace SWT1ATM.Unit.Test
{
    [TestFixture]
    public class TestOutput
    {


        private IVehicleFormatter _format;
        private IOutput _uut;
        private string _path;
        private ITrackFilter _track;
        private List<IVehicle> _vehicles;
        private IATM _atm;
        #region setup
        [SetUp]
        public void Setup()
        {
            _vehicles = new List<IVehicle>();
            _path = @"c:\Temp\SeparationCondition.txt";
            _format = Substitute.For<IVehicleFormatter>();
            _track = Substitute.For<ITrackFilter>();
            _uut = new LogOutput(_format,_track);
            _atm = Substitute.For<IATM>();
            var air0 = new Aircraft(1000, 1000, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 123), "XCE321");
            var air1 = new Aircraft(1000, 1000, 1000, new DateTime(2019, 06, 06, 12, 12, 12, 123), "XXE321");
            _vehicles.Add(air0);
            _vehicles.Add(air1);
        }
        #endregion
        #region FileOutputTests

        [Test]
        public void CreateANewFileWhenNoFileIsPresent()
        {
            var separationMock = Substitute.For<IAtmSeparationCondition>();
            if (File.Exists(_path))
                File.Delete(_path);
            separationMock.SeparationConditionEvent += _uut.LogVehicleData;
            separationMock.SeparationConditionEvent += Raise.EventWith(this, new FormattedTransponderDataEventArgs(_vehicles));
            Assert.That(File.Exists(_path));
            
        }

        [Test]
        public void AppendAFileWhenFileIsPresent()
        {
            var separationMock = Substitute.For<IAtmSeparationCondition>();
            _format.VehicleToString(_vehicles[0]).Returns("NewTest");
            if (File.Exists(_path))
                File.Delete(_path);
            var myFileWriter = new System.IO.StreamWriter(_path);
            myFileWriter.Write("Test:");
            myFileWriter.Close();
            separationMock.SeparationConditionEvent += _uut.LogVehicleData;
            separationMock.SeparationConditionEvent += Raise.EventWith(this, new FormattedTransponderDataEventArgs(_vehicles));

            var myFile = new System.IO.StreamReader(_path, Encoding.UTF8);
            Assert.That(myFile.ReadToEnd() == "Test:NewTest");
            myFile.Close();
        }
        [Test]
        public void LoggerDoesntThrowErrorWithZeroLenghtList()
        {
            var separationMock = Substitute.For<IAtmSeparationCondition>();
            List<IVehicle> emptyList = new List<IVehicle>();
            separationMock.SeparationConditionEvent += _uut.LogVehicleData;
            Assert.That(() => separationMock.SeparationConditionEvent += Raise.EventWith(this, new FormattedTransponderDataEventArgs(emptyList)), Throws.Nothing);
        }

        #endregion

        #region TerminalOutput
        [Test]
        public void TerminalDoesntThrowErrorWithZeroLengthList()
        {
            _uut = new TerminalOutput(_format,_atm);
            List<IVehicle> emptyList = new List<IVehicle>();
            var separationMock = Substitute.For<IAtmSeparationCondition>();
            separationMock.SeparationConditionEvent += _uut.LogVehicleData;
            Assert.That(() => separationMock.SeparationConditionEvent += Raise.EventWith(this, new FormattedTransponderDataEventArgs(emptyList)), Throws.Nothing);
        }

        [Test]
        public void TerminalTwiceRaisedEventLogVehicleDataIsCalledTwice()
        {
            _uut = new TerminalOutput(_format, _atm);
            var separationMock = Substitute.For<IAtmSeparationCondition>();
            separationMock.SeparationConditionEvent += _uut.LogVehicleData;
            separationMock.SeparationConditionEvent += Raise.EventWith(this, new FormattedTransponderDataEventArgs(_vehicles));
            separationMock.SeparationConditionEvent += Raise.EventWith(this, new FormattedTransponderDataEventArgs(_vehicles));
            _format.Received(2);
        }
        #endregion

        #region VehicleToSTring
        [Test]
        public void AirplaneFormatterReturnExpectedString()
        {
            var tstamp = new DateTime();
            var plane = new Aircraft(10, 10, 10, tstamp, "air0");
            string expectedString = "Tag: air0 Coordinates: X: 10, Y: 10, Z: 10, Direction: 0 degrees."+" Date: "+tstamp+"\n\r";
            _format = new AirplaneFormatter();
            Assert.That(_format.VehicleToString(plane).Equals(expectedString));
            _format.VehicleToString(new Aircraft(10,10,10,tstamp,"air0"));
        }

        #endregion
    }
}