using ConsoleTools;
using PKURBI_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Transactions;

namespace PKURBI_HFT_2023241.Client
{
    internal class Program
    {
        static RestService rest;
        static void Create(string entity)
        {
            if (entity == "RealEstate")
            {
                Console.WriteLine("Enter RealEstate location(city): ");
                string city = Console.ReadLine();
                Console.WriteLine("Enter RealEstate value: ");
                int value = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter RealEstate basicarea: ");
                int basicarea = int.Parse(Console.ReadLine());
                rest.Post(new RealEstate() { RealEstateCity = city, RealEstateValue = value, BasicArea = basicarea }, "RealEstate");
            }
            if (entity == "Salesperson")
            {
                Console.WriteLine("Enter Salesperson name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Salesperson age: ");
                int age = int.Parse(Console.ReadLine());
                rest.Post(new Salesperson() { Name = name, Age = age }, "Salesperson");
            }
            if (entity == "Tenant")
            {
                Console.WriteLine("Enter Tenant name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Tenant phone (MUST BE 9 CHARACTERS) : ");
                int phone = int.Parse(Console.ReadLine());
                rest.Post(new Tenant() { Name = name, Phone = phone }, "Tenant");
            }
        }
        static void List(string entity)
        {
            if (entity == "RealEstate")
            {
                List<RealEstate> realEstates = rest.Get<RealEstate>("RealEstate");
                //Console.WriteLine("ID\tLocation");
                Console.WriteLine("{0,-2} {1,-20} {2,-10}","ID", "Location", "Value");
                foreach (var item in realEstates)
                {
                    //Console.WriteLine(item.RealEstateId + ":\t"+item.RealEstateCity);
                    Console.WriteLine("{0,-2} {1,-20} {2,-10}",item.RealEstateId,item.RealEstateCity,item.RealEstateValue);
                }
            }
            if (entity == "Salesperson")
            {
                List<Salesperson> salesPeople = rest.Get<Salesperson>("Salesperson");
                Console.WriteLine("{0,-2} {1,-20} {2,-4}", "ID", "Name", "Age");
                foreach (var item in salesPeople)
                {
                    Console.WriteLine("{0,-2} {1,-20} {2,-4}", item.SalesId, item.Name, item.Age);
                }
            }
            if (entity == "Tenant")
            {
                List<Tenant> tenants = rest.Get<Tenant>("Tenant");
                Console.WriteLine("{0,-2} {1,-20} {2,-10}", "ID", "Name", "Phone");
                foreach(var item in tenants)
                {
                    Console.WriteLine("{0,-2} {1,-20} {2,-10}", item.TenantId, item.Name, item.Phone);
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            if (entity == "RealEstate")
            {
                Console.WriteLine("Enter the RealEstate's ID to update: ");
                int id = int.Parse(Console.ReadLine());
                RealEstate current = rest.Get<RealEstate>(id, "RealEstate");
                Console.WriteLine($"New RealEstate location (old: {current.RealEstateCity}) : ");
                string newCity = Console.ReadLine();
                Console.WriteLine($"New RealEstate value: (old: {current.RealEstateValue}) : ");
                int newValue = int.Parse(Console.ReadLine());
                Console.WriteLine($"New RealEstate basicarea: (old: {current.BasicArea}) : ");
                int newBasicarea = int.Parse(Console.ReadLine());
                current.RealEstateCity = newCity; current.RealEstateValue = newValue; current.BasicArea = newBasicarea;
                rest.Put(current, "RealEstate");
            }
            if (entity == "Salesperson")
            {
                Console.WriteLine("Enter the Salesperson's ID to update: ");
                int id = int.Parse(Console.ReadLine());
                Salesperson current = rest.Get<Salesperson>(id, "Salesperson");
                Console.WriteLine($"New Salesperson name: (old: {current.Name}) : ");
                string newName = Console.ReadLine();
                Console.WriteLine($"New Salesperson age: (old: {current.Age}) : ");
                int newAge = int.Parse(Console.ReadLine());
                current.Name = newName; current.Age = newAge;
                rest.Put(current, "Salesperson");
            }
            if (entity == "Tenant")
            {
                Console.WriteLine("Enter the Tenant's ID to update: ");
                int id = int.Parse(Console.ReadLine());
                Tenant current = rest.Get<Tenant>(id, "Tenant");
                Console.WriteLine($"New Tenant name (old: {current.Name}) : ");
                string newName = Console.ReadLine();
                Console.WriteLine($"New Tenant phone (This must be 9 characters) (old: {current.Phone}) : ");
                int newPhone = int.Parse(Console.ReadLine());
                current.Name = newName; current.Phone = newPhone;
                rest.Put(current, "Tenant");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "RealEstate")
            {
                Console.WriteLine("Enter RealEstate's ID to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "RealEstate");
            }
            if (entity == "Salesperson")
            {
                Console.WriteLine("Enter Salesperson's ID to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "Salesperson");
            }
            if (entity == "Tenant")
            {
                Console.WriteLine("Enter Tenant's ID to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "Tenant");
            }
        }
        static void AvgPriceBySalespersonID()
        {
            Console.WriteLine("Enter the Salesperson ID: ");
            int id = int.Parse(Console.ReadLine());
            double? result = rest.Get<double?>(id, "NCRealEstate/AvgPriceBySalespersonID");
            Console.WriteLine("The avarage price of the given Salesman is : " + result);
            Console.ReadLine();
        }
        static void MostRealEstates()
        {
            Console.WriteLine("The most top 3 salesman who has the most RealEstates: ");
            var top3 = rest.Get<string>("NCSalesperson/MostRealEstates");
            int index = 0;
            foreach (var item in top3)
            {
                index++;
                Console.WriteLine(index + ".: "+item);
            }
            Console.ReadLine();
        }
        static void TenantsByCity()
        {
            var TenantList = rest.Get<Tenants>("NCTenant/TenantsByCity");
            Console.WriteLine("{0,-15} {1,-2}", "Name", "Number of Estates");
            foreach (var item in TenantList)
            {
                Console.WriteLine("{0,-15} {1,8}", item.Name, item.EstateCount);
            }
            Console.ReadLine();
        }
        static void BasicInformation()
        {
            Console.WriteLine("Enter the ID of the RealEstate: ");
            int id = int.Parse(Console.ReadLine());
            var basicinfo = rest.Get<BasicInfo>(id, "NCRealEstate/BasicInformation");
            Console.WriteLine("Location: "+basicinfo.Location+"\nValue: "+basicinfo.Value+"\nBasicArea: "
                +basicinfo.Area+"\nSalesperson: "+basicinfo.Salesperson+"\nTenant: "+basicinfo.Tenant
                +"\nContact: "+basicinfo.TenantContact);
            Console.ReadLine();
        }
        static void AvgPriceByCity()
        {
            var query = rest.Get<AvgPrices>("NCRealEstate/AvgPriceByCity");
            Console.WriteLine("The avarage values of the RealEstates by cities: ");
            Console.WriteLine("{0,-20} {1,-10}", "City", "AvgValue");
            foreach (var item in query)
            {
                Console.WriteLine("{0,-20} {1,-10}", item.City, item.AvgPrice);
            }
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
        }
    }
}
