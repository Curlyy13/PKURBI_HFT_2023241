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
                new Salesperson("1#John Miller#30#3"),
                new Salesperson("2#David Holmes#50#6"),
                new Salesperson("3#Peter Parker#34#2"),
                new Salesperson("4#Lázár Vilmos#42#1"),
                new Salesperson("5#Kovács István#20#4"),
                new Salesperson("6#Mike Cenat#24#7"),
                new Salesperson("7#John Davis#23#8"),
                new Salesperson("8#David D.#43#9"),
                new Salesperson("9#Joe Trump#44#10"),
            }.AsQueryable());
            logic = new SalespersonLogic(mockSalespersonRepo.Object);
        }
    }
}
