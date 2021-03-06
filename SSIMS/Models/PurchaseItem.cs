﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SSIMS.DAL;

namespace SSIMS.Models
{
    public class PurchaseItem
    {
        public int ID { get; set; }
        public int Qty { get; set; }

        public Tender Tender { get; set; }

        public virtual PurchaseOrder PurchaseOrder {get; set;}

        public PurchaseItem()
        {
        }

        public PurchaseItem(Item item, Supplier supplier, int qty, UnitOfWork uow)
        {
            Qty = qty;
            Tender = uow.TenderRepository.Get(filter: t => t.Supplier.ID == supplier.ID && t.Item.ID == item.ID).FirstOrDefault();
        }

        public PurchaseItem(string itemID, string SupplierID, int qty, UnitOfWork uow)
        {
            Qty = qty;
            Tender = uow.TenderRepository.Get(filter: t => t.Supplier.ID == SupplierID && t.Item.ID == itemID).FirstOrDefault();
        }
        public virtual double Amount
        {
            get { return this.Qty * this.Tender.Price; }
        }
    }
}