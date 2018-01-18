using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoLib.Contracts;
using GeoLib.Data;
using GeoLib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GeoLib.Tests
{
    [TestClass]
    public class ManagerTests
    {
        [TestMethod]
        public void test_zip_code_retrieval()
        {
            Mock<IZipCodeRepository> mockZipCodeRepository = new Mock<IZipCodeRepository>();

            ZipCode zipCode = new ZipCode()
            {
                City = "LINCOLN PARK",
                State = new State() { Abbreviation = "NJ"},
                Zip = "07035"
            };

            ZipCode zipCode2 = new ZipCode()
            {
                City ="MIGDAL",
                State = new State() { Abbreviation = "WI" },
                Zip = "1111"
            };

            mockZipCodeRepository.Setup(obj => obj.GetByZip("07035")).Returns(zipCode);
            mockZipCodeRepository.Setup(obj => obj.GetByZip("1111")).Returns(zipCode2);

            IGeoService geoService = new GeoManager(mockZipCodeRepository.Object);

            ZipCodeData data = geoService.GetZipInfo("07035");
            ZipCodeData data2 = geoService.GetZipInfo("1111");

            Assert.IsTrue(data.City.ToUpper() == "LINCOLN PARK");
            Assert.IsTrue(data.State == "NJ");

            Assert.IsTrue(data2.City.ToUpper() == "MIGDAL");
            Assert.IsTrue(data2.State == "WI");
        }
    }
}
