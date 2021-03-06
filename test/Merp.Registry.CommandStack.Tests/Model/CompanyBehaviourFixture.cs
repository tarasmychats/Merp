﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpTestsEx;
using Merp.Registry.CommandStack.Model;

namespace Merp.Registry.CommandStack.Tests.Model
{
    [TestFixture]
    public class CompanyBehaviourFixture
    {       
        [Test]
        public void ChangeLegalAddress_should_set_LegalAddress_accordingly_to_inputs()
        {
            var companyName = "Mastreeno";
            var nationalIdentificationNumber = "FAKE";
            var vatNumber = "123";
            var address = "Via Torino 51";
            var city = "Milan";
            var postalCode = "20123";
            var province = "MI";
            var country = "Italy";
            var company = Company.Factory.CreateNewEntry(companyName, vatNumber, nationalIdentificationNumber, 
                null, null, null, null, null,
                null, null, null, null, null,
                null, null, null, null, null);
            var effectiveDate = DateTime.Now;
            company.ChangeLegalAddress(address, city, postalCode, province, country, effectiveDate);

            Assert.NotNull(company.LegalAddress);
            Assert.AreEqual(address, company.LegalAddress.Address);
            Assert.AreEqual(city, company.LegalAddress.City);
            Assert.AreEqual(postalCode, company.LegalAddress.PostalCode);
            Assert.AreEqual(province, company.LegalAddress.Province);
            Assert.AreEqual(country, company.LegalAddress.Country);
        }

        [Test]
        public void ChangeBillingAddress_should_set_LegalAddress_accordingly_to_inputs()
        {
            var companyName = "Mastreeno";
            var nationalIdentificationNumber = "FAKE";
            var vatNumber = "123";
            var address = "Via Torino 51";
            var city = "Milan";
            var postalCode = "20123";
            var province = "MI";
            var country = "Italy";
            var company = Company.Factory.CreateNewEntry(companyName, vatNumber, nationalIdentificationNumber, 
                null, null, null, null, null,
                null, null, null, null, null,
                null, null, null, null, null);
            var effectiveDate = DateTime.MaxValue;
            company.ChangeBillingAddress(address, city, postalCode, province, country, effectiveDate);

            Assert.NotNull(company.BillingAddress);
            Assert.AreEqual(address, company.BillingAddress.Address);
            Assert.AreEqual(city, company.BillingAddress.City);
            Assert.AreEqual(postalCode, company.BillingAddress.PostalCode);
            Assert.AreEqual(province, company.BillingAddress.Province);
            Assert.AreEqual(country, company.BillingAddress.Country);
        }

        [Test]
        public void ChangeShippingAddress_should_set_LegalAddress_accordingly_to_inputs()
        {
            var companyName = "Mastreeno";
            var nationalIdentificationNumber = "FAKE";
            var vatNumber = "123";
            var address = "Via Torino 51";
            var city = "Milan";
            var postalCode = "20123";
            var province = "MI";
            var country = "Italy";
            var company = Company.Factory.CreateNewEntry(companyName, vatNumber, nationalIdentificationNumber, 
                null, null, null, null, null,
                null, null, null, null, null,
                null, null, null, null, null);
            var effectiveDate = DateTime.MaxValue;
            company.ChangeShippingAddress(address, city, postalCode, province, country, effectiveDate);

            Assert.NotNull(company.ShippingAddress);
            Assert.AreEqual(address, company.ShippingAddress.Address);
            Assert.AreEqual(city, company.ShippingAddress.City);
            Assert.AreEqual(postalCode, company.ShippingAddress.PostalCode);
            Assert.AreEqual(province, company.ShippingAddress.Province);
            Assert.AreEqual(country, company.ShippingAddress.Country);
        }
        

    }
}
