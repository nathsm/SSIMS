﻿using SSIMS.Database;
using SSIMS.Models;
using SSIMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSIMS.DAL
{
    public class DocumentItemRepository : GenericRepository<DocumentItem>
    {

        public DocumentItemRepository(DatabaseContext context)
            : base(context)
        {
        }
        //save documentitem through item_id and qty
        public void InsertDocumentItembyItemandQty(Item item, int qty)
        {
            DocumentItem doitem = new DocumentItem();
            doitem.Item = item;
            doitem.Qty = qty;
            dbSet.Add(doitem);
        }
        //requsition list for staff themselves
        public List<RequisitionCreateViewModel> GetRequisitionList(Staff staff)
        {
            using (var context = new DatabaseContext())
            {
                List<DocumentItem> doItems = new List<DocumentItem>();
                doItems = context.DocumentItems.AsNoTracking()
                    .Where(x => x.Document.CreatedByStaff == staff)
                    .ToList();

                
                    List<RequisitionCreateViewModel> requisitionListDisplay = new List<RequisitionCreateViewModel>();
                    foreach (var x in doItems)
                    {
                    var requisitionDisplay = new RequisitionCreateViewModel()
                    {
                        SelectedCategory = x.Item.Description,
                        Quantity = x.Qty,
                        CreatedDate = x.Document.CreatedDate,
                        //Status = x.Document.Status
                    };
                        requisitionListDisplay.Add(requisitionDisplay);
                    }
                    return requisitionListDisplay;
               
            }
        }
    }
}