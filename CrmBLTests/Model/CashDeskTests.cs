﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrmBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBL.Model.Tests
{
    [TestClass()]
    public class CashDeskTests
    {
        [TestMethod()]
        public void CashDeskTest()
        {
            // arrange

            var customer1 = new Customer()
            {
                Name = "test1customer",
                Id = 1
            };

            var customer2 = new Customer()
            {
                Name = "test2customer",
                Id = 2
            };

            var seller = new Seller()
            {
                Name = "test1seller",
                SellerID = 1,
            };

            var product1 = new Product()
            {
                ProductId = 1,
                Name = "pr1",
                Price = 100,
                Count = 10
            };
            var product2 = new Product()
            {
                ProductId = 2,
                Name = "pr2",
                Price = 150,
                Count = 5
            };

            var cart1 = new Cart(customer1);
            cart1.Add(product1);
            cart1.Add(product2);
            cart1.Add(product1);

            var cart2 = new Cart(customer2);
            cart2.Add(product1);
            cart2.Add(product2);
            cart2.Add(product2);

            var cashdesk = new CashDesk(1, seller, null);
            cashdesk.MaxQueueLenght = 20;
            cashdesk.Enqueue(cart1);
            cashdesk.Enqueue(cart2);

            var cart1ExpectedResult = 350;
            var cart2ExpectedResult = 400;

            // act

            var cart1ActualResult = cashdesk.Dequeue();
            var cart2ActualResult = cashdesk.Dequeue();

            // assert 

            Assert.AreEqual(cart1ExpectedResult, cart1ActualResult);
            Assert.AreEqual(cart2ExpectedResult, cart2ActualResult);
            Assert.AreEqual(7, product1.Count);
            Assert.AreEqual(2, product2.Count);

        }
    }
}