using Moq;
using NUnit.Framework;
using PKURBI_HFT_2023241.Logic;
using PKURBI_HFT_2023241.Models;
using PKURBI_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PKURBI_HFT_2023241.Test
{
    [TestFixture]
    public class RealEstateLogicTester
    {
        RealEstateLogic logic;
        Mock<IRepository<RealEstate>> mockRealEstateRepo;
        [SetUp]
        public void Init() 
        {
            mockRealEstateRepo = new Mock<IRepository<RealEstate>>();
            mockRealEstateRepo.Setup(t => t.ReadAll()).Returns(new List<RealEstate>()
            {
                new RealEstate()
                {
                    RealEstateId = 1,
                    RealEstateCity = "Budapest",
                    RealEstateValue = 265000,
                    BasicArea = 150,
                    SalesId = 2,
                    Salesperson = (new Salesperson() {SalesId=2, Name="David Holmes", Age= 50}),
                    TenantId = 1,
                    Tenant = (new Tenant() { TenantId = 1, Name="Olivia Briggs" , Phone=708842211})
                },
                new RealEstate()
                {
                    RealEstateId=2,
                    RealEstateCity = "Washington",
                    RealEstateValue= 1200000,
                    BasicArea = 200,
                    SalesId = 5,
                    Salesperson = new Salesperson() {SalesId=5, Name="Kovács István", Age=20 },
                    TenantId=3,
                    Tenant = new Tenant() { TenantId= 3, Name="Les Kain", Phone=708547511}
                },
                new RealEstate()
                {
                    RealEstateId=3,
                    RealEstateCity = "Roma",
                    RealEstateValue= 200000,
                    BasicArea = 80,
                    SalesId = 3,
                    Salesperson = new Salesperson() {SalesId=3, Name="Peter Parker", Age=34 },
                    TenantId=6,
                    Tenant = new Tenant() { TenantId= 6, Name="Irene Thompson", Phone=718532364}
                },
            }.AsQueryable());
            logic = new RealEstateLogic(mockRealEstateRepo.Object);
        }

        [Test]
        public void RealEstateCreateSuccessTest()
        {
            var newEstate = new RealEstate() { BasicArea = 30 };
            logic.Create(newEstate);
            mockRealEstateRepo.Verify(r => r.Create(newEstate), Times.Once);
        }
        [Test]
        public void RealEstateCreateFailTest()
        {
            var newEstate = new RealEstate() { BasicArea = 10 };
            try
            {
                logic.Create(newEstate);
            }
            catch (Exception)
            {
            }
            mockRealEstateRepo.Verify(t => t.Create(newEstate), Times.Never);
        }
        [Test]
        public void AvgPriceBySalesPersonIDTest()
        {
            double? actual = logic.AvgPriceBySalespersonID(3);
            Assert.That(actual, Is.EqualTo(200000));
        }
        [Test]
        public void BasicInformationTest()
        {
            var expected = new BasicInfo()
            {
                Location = "Roma",
                Value = 200000,
                Area = 80,
                Salesperson = "Peter Parker",
                Tenant = "Irene Thompson",
                TenantContact = 718532364,
            };
            var actual = logic.BasicInformation(3);
            Assert.That(actual.ToArray()[0], Is.EqualTo(expected));
        }

        [Test]
        public void AvgPriceByCityTest()
        {
            var expected = new List<AvgPrices>()
            {
                new AvgPrices() { City="Budapest", AvgPrice=265000  },
                new AvgPrices() { City="Washington", AvgPrice= 1200000},
                new AvgPrices() { City="Roma", AvgPrice= 200000}
            };
            var actual = logic.AvgPriceByCity().ToList();
            Assert.That(actual, Is.EqualTo(expected));
        }
    }         
}
