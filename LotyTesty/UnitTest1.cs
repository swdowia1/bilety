using Loty;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LotyTesty
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// cena biletu 100 minimu 60
        /// </summary>
        Flight _FlightOK;
        public UnitTest1()
        {
            _FlightOK = new Flight("KLM 12345 BCA", "Amsterdam", "Warszawa", DateTime.Now.AddHours(5), new List<DayOfWeek> { DayOfWeek.Thursday }, 60m);

            _FlightOK.AddPrice(DateTime.Now.Date.AddDays(-1), 100);


        }
        [TestMethod]
        public void HeadTest_bezZapisu()
        {

            var tenantB = new TenantB("Tenant B");
            Person p = new Person(Gender.M, "janek", 1978, 12, 21);



            var discountCriteria = new List<IDiscountCriterion>
            {
                new BirthdayDiscountCriterion(),
                new Konczy_na_a_Criterion(),
                new GenderDiscountCriterion(Gender.M)
            };

            var purchaseService = new PurchaseService(discountCriteria, 10);

            var appliedCriteria = new List<string>();
            decimal finalPrice = purchaseService.PurchaseFlight(tenantB, _FlightOK, DateTime.Now, p, out appliedCriteria);
            Assert.AreEqual(80m, finalPrice, "spodziewana inni znizka");
            Assert.IsTrue(appliedCriteria.Count == 0);

        }
        [TestMethod]
        public void HeadTest_zapis()
        {
            var tenantA = new TenantA("Tenant A");

            Person p = new Person(Gender.M, "janek", 2024, 12, 14);



            var discountCriteria = new List<IDiscountCriterion>
            {
                new BirthdayDiscountCriterion(),
                new Konczy_na_a_Criterion(),
                new GenderDiscountCriterion(Gender.M)
            };

            var purchaseService = new PurchaseService(discountCriteria, 10);

            var appliedCriteria = new List<string>();
            decimal finalPrice = purchaseService.PurchaseFlight(tenantA, _FlightOK, DateTime.Now, p, out appliedCriteria);
            Assert.AreEqual(70m, finalPrice, "spodziewana inni znizka");
            Assert.AreEqual(3, appliedCriteria.Count, "Powinno być 3 znizki");

        }
        [TestMethod]
        public void trzy_z_czeterch()
        {
            //cena biletu
            var bilet = new Flight("KLM 12345 BCA", "Amsterdam", "Warszawa", DateTime.Now.AddHours(5), new List<DayOfWeek> { DayOfWeek.Thursday }, 84m);

            bilet.AddPrice(DateTime.Now.Date.AddDays(-1), 100);
            var tenantA = new TenantA("Tenant A");

            Person p = new Person(Gender.M, "janek", 2010, 12, 14);



            var discountCriteria = new List<IDiscountCriterion>
            {
                new bilet_na_dziecko_Criterion(),
                new bilet_na_sobota()
            };

            var purchaseService = new PurchaseService(discountCriteria, 10);

            var appliedCriteria = new List<string>();
            decimal finalPrice = purchaseService.PurchaseFlight(tenantA, bilet, DateTime.Now, p, out appliedCriteria);
            Assert.AreEqual(90m, finalPrice, "spodziewana inni znizka");
            Assert.AreEqual(1, appliedCriteria.Count, "Powinno być 3 znizki");

        }
        [TestMethod]
        public void Valid_Flight()
        {
            var tt = _FlightOK.GetPrice(DateTime.Now.AddDays(3));

            Assert.AreEqual("KLM 12345 BCA", _FlightOK.FlightId, "Nie przypisał się numer");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Nie przeszedl test bo numer jest ok")]
        public void Valid_Flight_throw_ArgumentException()
        {
            var flight = new Flight("KLM", "Amsterdam", "Africa", DateTime.Now.AddHours(5), new List<DayOfWeek> { DayOfWeek.Thursday }, 80m);
            flight.AddPrice(DateTime.Now.Date, 30);

        }



        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException), "Nie przeszedl ceny podstawowa nie jest mniejsza od minmalnej")]
        //public void Valid_FlightPrice_throw_ArgumentException_MinPriceGreaterBase()
        //{

        //    var t = new FlightPrice(_FlightOK, DateTime.Now, 60m, 80m);
        //}

    }
}
