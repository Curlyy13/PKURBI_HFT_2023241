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
                new Tenant("1#Olivia Briggs#708842211#5"),
                new Tenant("2#Bennett Parks#702185525#2"),
                new Tenant("3#Les Kain#708547511#3"),
                new Tenant("4#Brigham Glisson#708748291#8"),
                new Tenant("5#Edwin Porter#883521447#1"),
                new Tenant("6#Irene Thompson#718532364#4"),
                new Tenant("7#Oriel Hall#903472841#9"),
                new Tenant("8#William Wood#814385122#6"),
                new Tenant("9#Laurence Mccoy#365423414#7")
            }.AsQueryable());
            logic = new TenantLogic(mockTenantRepo.Object);
        }
    }
}
