using Castle.DynamicProxy;
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
    public class SalespersonLogicTester
    {
        SalespersonLogic logic;
        Mock<IRepository<Salesperson>> mockSalespersonRepo;
        [SetUp]
        public void Init()
        {
            mockSalespersonRepo = new Mock<IRepository<Salesperson>>();
            mockSalespersonRepo.Setup(t => t.ReadAll()).Returns(new List<Salesperson>()
            {
                new Salesperson()
                {
                    SalesId = 1,
                    Name = "Olivia Briggs",
                    Age = 30,
                    Realestates = new List<RealEstate>()
                    {
                        new RealEstate() 
                        {                     
                        RealEstateId = 1,
                        RealEstateCity = "Budapest",
                        },
                        new RealEstate()
                        {
                        RealEstateId = 2,
                        RealEstateCity = "Budapest",
                        },
                    } //#3
                },
                new Salesperson()
                {
                    SalesId = 2,
                    Name = "David Holmes",
                    Age = 50,
                    Realestates = new List<RealEstate>()
                    {
                        new RealEstate()
                        {
                        RealEstateId = 3,
                        RealEstateCity = "Budapest",
                        },
                    } //#4
                },
                new Salesperson()
                {
                    SalesId= 3,
                    Name = "Peter Parker",
                    Age = 34,
                    Realestates = new List<RealEstate>()
                    {
                        new RealEstate()
                        {
                        RealEstateId = 4,
                        RealEstateCity = "Budapest",
                        },
                        new RealEstate()
                        {
                        RealEstateId = 5,
                        RealEstateCity = "Budapest",
                        },                        
                        new RealEstate()
                        {
                        RealEstateId = 6,
                        RealEstateCity = "Budapest",
                        },                        
                        new RealEstate()
                        {
                        RealEstateId = 7,
                        RealEstateCity = "Budapest",
                        },
                    } //#2
                },
                new Salesperson("4#Lázár Vilmos#42")
                {
                    SalesId = 4,
                    Name = "Lázár Vilmos",
                    Age = 42,
                    Realestates = new List<RealEstate>()
                    {
                        new RealEstate()
                        {
                        RealEstateId = 8,
                        RealEstateCity = "Budapest",
                        },
                        new RealEstate()
                        {
                        RealEstateId = 9,
                        RealEstateCity = "Budapest",
                        },
                        new RealEstate()
                        {
                        RealEstateId = 10,
                        RealEstateCity = "Budapest",
                        },
                        new RealEstate()
                        {
                        RealEstateId = 11,
                        RealEstateCity = "Budapest",
                        },
                        new RealEstate()
                        {
                        RealEstateId = 12,
                        RealEstateCity = "Budapest",
                        },
                    } //#1
                }
            }.AsQueryable());
            logic = new SalespersonLogic(mockSalespersonRepo.Object);
        }

        [Test]
        public void MostRealEstatesTest()
        {
            var top3 = new List<string>();
            top3.Add("Lázár Vilmos");
            top3.Add("Peter Parker");
            top3.Add("Olivia Briggs");
            var actual = logic.MostRealEstates().ToList();
            Assert.That(actual, Is.EqualTo(top3));
        }

        [Test]
        public void SalesPersonCreateSuccessTest()
        {
            var newSalesPerson = new Salesperson() { Name="TestSubject1", Age=20 };
            logic.Create(newSalesPerson);
            mockSalespersonRepo.Verify(t => t.Create(newSalesPerson), Times.Once);
        }
        [Test]
        public void SalesPersonCreateFailTest()
        {
            var newSalesPerson = new Salesperson() { Name = "TestSubject2", Age = 15 };
            try
            {
                logic.Create(newSalesPerson);
            }
            catch (Exception)
            {
            }
            mockSalespersonRepo.Verify(t => t.Create(newSalesPerson), Times.Never);
        }
        [Test]
        public void SalespersonUpdate()
        {
            var updatedsalesperson = new Salesperson() { Age = 1, Name = "badtest", SalesId = 1 };
            try
            {
                logic.Update(updatedsalesperson);
            }
            catch (Exception)
            {
            }
            mockSalespersonRepo.Verify(r => r.Update(updatedsalesperson), Times.Never);
        }
    }
}
