using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SWT1ATM;
using NSubstitute;
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
        private List<IVehicle> _vehicles;
        #region setup
        [SetUp]
        public void Setup()
        {
            _vehicles = new List<IVehicle>();
            _path = @"c:\Temp\SeparationCondition.txt";
            _format = Substitute.For<IVehicleFormatter>();
            _uut = new LogOutput(_format);
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
            separationMock.SeparationConditionEvent += Raise.EventWith(this, new SeparationConditionEventArgs(_vehicles));
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
            separationMock.SeparationConditionEvent += Raise.EventWith(this, new SeparationConditionEventArgs(_vehicles));

            var myFile = new System.IO.StreamReader(_path, Encoding.UTF8);
            Assert.That(myFile.ReadToEnd() == "Test:NewTest");
            myFile.Close();
        }
        #endregion

    }
}