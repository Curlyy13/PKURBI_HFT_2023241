using Moq;
using NUnit.Framework;
using PKURBI_HFT_2023241.Logic;
using PKURBI_HFT_2023241.Models;
using PKURBI_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKURBI_HFT_2023241.Test
{
    [TestFixture]
    public class TenantLogicTester
    {
        TenantLogic logic;
        Mock<IRepository<Tenant>> mockTenantRepo;
        [SetUp]
        public void Init()
        {
            mockTenantRepo = new Mock<IRepository<Tenant>>();
            mockTenantRepo.Setup(t => t.ReadAll()).Returns(new List<Tenant>()
            {
                new Tenant()
                {
                    TenantId = 1,
                    Name = "Olivia Briggs",
                    Phone = 708842211,
                    Realestates = new List<RealEstate>()
                    {
                        new RealEstate()
                        {
                            RealEstateId = 1,
                            RealEstateCity = "Budapest",
                            RealEstateValue = 350000
                        },
                        new RealEstate()
                        {
                            RealEstateId = 2,
                            RealEstateCity = "Roma",
                            RealEstateValue = 250000
                        },
                    }
                },
                new Tenant()
                {
                    TenantId = 2,
                    Name = "Bennett Parks",
                    Phone = 702185525,
                    Realestates = new List<RealEstate>()
                    {
                        new RealEstate()
                        {
                            RealEstateId = 3,
                            RealEstateCity = "Paris",
                            RealEstateValue = 500000
                        },
                        new RealEstate()
                        {
                            RealEstateId = 4,
                            RealEstateCity = "Budapest",
                            RealEstateValue = 100000
                        },
                        new RealEstate()
                        {
                            RealEstateId = 7,
                            RealEstateCity = "Budapest",
                            RealEstateValue = 170000
                        },
                    }
                },
                new Tenant()
                {
                    TenantId = 3,
                    Name = "Les Kain",
                    Phone = 708547511,
                    Realestates = new List<RealEstate>()
                    {
                        new RealEstate()
                        {
                            RealEstateId = 5,
                            RealEstateCity = "Paris",
                            RealEstateValue = 200000
                        },
                    }
                },
                new Tenant()
                {
                    TenantId = 4,
                    Name = "Brigham Glisson",
                    Phone = 708748291,
                    Realestates = new List<RealEstate>()
                    {
                        new RealEstate()
                        {
                            RealEstateId = 6,
                            RealEstateCity = "Berlin",
                            RealEstateValue = 200000
                        },
                    }
                },
            }.AsQueryable());
            logic = new TenantLogic(mockTenantRepo.Object);
        }

        [Test]
        public void TenantCreateSuccessTest()
        {
            var newTenant = new Tenant() { Phone = 123456789 };
            logic.Create(newTenant);
            mockTenantRepo.Verify(t => t.Create(newTenant), Times.Once);
        }
        [Test]
        public void TenantCreateFailTest()
        {
            var newTenant = new Tenant() { Phone = 12345678 };
            try
            {
                logic.Create(newTenant);
            }
            catch (Exception)
            {
            }
            mockTenantRepo.Verify(t => t.Create(newTenant), Times.Never);
        }
        [Test]
        public void TenantsByCityTest()
        {
            var expected = new List<Tenants>() 
            {
                new Tenants() {Name="Bennett Parks", EstateCount=3},
                new Tenants() {Name="Olivia Briggs", EstateCount=2},
                new Tenants() {Name="Les Kain", EstateCount=1},
                new Tenants() {Name="Brigham Glisson", EstateCount=1},
            };
            var actual = logic.TenantsByCity().ToList();
            Assert.That(expected, Is.EqualTo(actual));
        }
    }
}
