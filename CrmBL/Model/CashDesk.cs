﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrmBL.Model;

namespace CrmBL.Model
{
    public class CashDesk
    {
        CrmContext db;
        public Seller Seller { get; set; }
        public Queue<Cart> Queue { get; set; }
        public int Number { get; set; }
        public int MaxQueueLenght { get; set; }
        public int ExitCustomer { get; set; }
        public bool IsModel { get; set; }
        public int Count => Queue.Count;

        public event EventHandler<Check> CheckClosed;

        public CashDesk( int number, Seller seller, CrmContext db)
        {
            this.db = db ?? new CrmContext();
            Seller = seller;
            Number = number;
            Queue = new Queue<Cart>();
            IsModel = true;
            MaxQueueLenght = 10;
        }

        public void Enqueue(Cart cart)
        {
            if (Queue.Count < MaxQueueLenght)
            {
                Queue.Enqueue(cart);
            }
            else
            {
                ExitCustomer++;
            }
        }

        public decimal Dequeue()
        {
            decimal sum = 0;
            if (Queue.Count == 0)
            {
                return 0;
            }
            var card = Queue.Dequeue();

            if (card != null)
            {
                var check = new Check()
                {
                    SellerId = Seller.SellerID,
                    Seller = Seller,
                    CustomerID = card.Customer.Id,
                    Customer = card.Customer,
                    Created = DateTime.Now
                };

                if (!IsModel)
                {
                    db.Checks.Add(check);
                    db.SaveChanges();
                }
                else
                {
                    check.CheckId = 0;
                }

                var sells = new List<Sell>();

                foreach (Product product in card)
                {
                    if (product.Count > 0)
                    {
                        var sell = new Sell()
                        {
                            CheckId = check.CheckId,
                            Check = check,
                            ProductId = product.ProductId,
                            Product = product
                        };

                        sells.Add(sell);

                        if (!IsModel)
                        {
                            db.Sells.Add(sell);
                        }

                        product.Count--;
                        sum += product.Price;
                    }
                }

                check.Price = sum;

                if (!IsModel)
                {
                    db.SaveChanges();
                }

                CheckClosed?.Invoke(this, check);
            }

            return sum;
        }

        public override string ToString()
        {
            return $"Касса №{Number}";
        }
    }
}
