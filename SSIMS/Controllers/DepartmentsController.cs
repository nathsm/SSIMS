﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SSIMS.Database;
using SSIMS.DAL;
using SSIMS.Models;
using System.Diagnostics;

namespace SSIMS.Controllers
{
    public class DepartmentsController : Controller
    {

        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Departments
        public ActionResult Index()
        {

            var departments = unitOfWork.DepartmentRepository.Get(includeProperties: "CollectionPoint, DeptHead, DepRep");
            ViewBag.RepList = unitOfWork.StaffRepository.GetDeptRepList();
            Debug.WriteLine("number of heads: " + unitOfWork.StaffRepository.GetDeptHeadList().Count());
            var Deps  = unitOfWork.DepartmentRepository.Get(filter:x=>x.ID!="STOR",includeProperties: "DeptHeadAuthorization.Staff");
            ViewBag.Departments = Deps;
            List<string> authNames = new List<string>();
            foreach(Department d in Deps)
            {
                if (d.DeptHeadAuthorization == null)
                    authNames.Add("none");
                else
                    authNames.Add(d.DeptHeadAuthorization.Staff.Name);
            }
            ViewBag.Auth = authNames;
            ViewBag.DeptCount = unitOfWork.DepartmentRepository.Get().Count();
            return View(departments.ToList());
        }

        // GET: Departments/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = unitOfWork.DepartmentRepository.GetByID(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.CollectionPointID = new SelectList(unitOfWork.CollectionPointRepository.Get(), "ID", "Location"); 
            ViewBag.DeptHeadID = new SelectList(unitOfWork.StaffRepository.Get(), "ID", "UserAccountID");
            ViewBag.DeptRepID = new SelectList(unitOfWork.StaffRepository.Get(), "ID", "UserAccountID");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DeptRepID,DeptHeadID,CollectionPointID,DeptHeadAutorizationID,DeptName,PhoneNumber,FaxNumber")] Department department)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.DepartmentRepository.Update(department);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            ViewBag.CollectionPointID = new SelectList(unitOfWork.CollectionPointRepository.Get(), "ID", "Location", department.CollectionPoint.ID);
            ViewBag.DeptHeadID = new SelectList(unitOfWork.StaffRepository.Get(), "ID", "UserAccountID", department.DeptHead.ID);
            ViewBag.DeptRepID = new SelectList(unitOfWork.StaffRepository.Get(), "ID", "UserAccountID", department.DeptRep.ID);
            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = unitOfWork.DepartmentRepository.GetByID(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.CollectionPointID = new SelectList(unitOfWork.CollectionPointRepository.Get(), "ID", "Location", department.CollectionPoint.ID);
            ViewBag.DeptHeadID = new SelectList(unitOfWork.StaffRepository.Get(), "ID", "UserAccountID", department.DeptHead.ID);
            ViewBag.DeptRepID = new SelectList(unitOfWork.StaffRepository.Get(), "ID", "UserAccountID", department.DeptRep.ID);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DeptRepID,DeptHeadID,CollectionPointID,DeptHeadAutorizationID,DeptName,PhoneNumber,FaxNumber")] Department department)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.DepartmentRepository.Update(department);
                unitOfWork.Save();
                return RedirectToAction("Index");

            }
            ViewBag.CollectionPointID = new SelectList(unitOfWork.CollectionPointRepository.Get(), "ID", "Location", department.CollectionPoint.ID);
            ViewBag.DeptHeadID = new SelectList(unitOfWork.StaffRepository.Get(), "ID", "UserAccountID", department.DeptHead.ID);
            ViewBag.DeptRepID = new SelectList(unitOfWork.StaffRepository.Get(), "ID", "UserAccountID", department.DeptRep.ID);

            //ViewBag.CollectionPointID = new SelectList(unitOfWork.CollectionPointRepository.Get(), "ID", "Location", department.CollectionPoint.ID);
            //ViewBag.DeptHeadID = new SelectList(unitOfWork.StaffRepository.Get(), "UserAccountID", department.DeptHead.ID);
            //ViewBag.DeptRepID = new SelectList(unitOfWork.StaffRepository.Get(), "UserAccountID", department.DeptRep.ID);
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = unitOfWork.DepartmentRepository.GetByID(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Department department = unitOfWork.DepartmentRepository.GetByID(id);
            unitOfWork.DepartmentRepository.Delete(department);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

/*        //delegate authority
        public ActionResult DelegateAuthority()
        {
            DeptHeadAuthorization deptHead = new DeptHeadAuthorization();
            return null;
        }*/

        //select collection point
       /* public ActionResult SelectCollectionPoint(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.CollectionPointID = new SelectList(db.CollectionPoints, "ID", "Location", department.CollectionPoint.ID);
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectCollectionPoint([Bind(Include = "ID,CollectionPointID,DeptName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CollectionPointID = new SelectList(db.CollectionPoints, "ID", "Location", department.CollectionPoint.ID);
            return View(department);
        }*/
    }
}