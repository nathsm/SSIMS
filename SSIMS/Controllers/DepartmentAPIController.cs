﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SSIMS.Database;
using SSIMS.Models;
using SSIMS.ViewModels;
using System.Diagnostics;
using SSIMS.DAL;

namespace SSIMS.Controllers
{
    public class DepartmentAPIController : ApiController
    {
        UnitOfWork uow = new UnitOfWork();

        [HttpGet]
        public Department Get(string username)
        {
            Staff currentstaff = uow.StaffRepository.Get(filter: x => x.UserAccountID == username, includeProperties: "Department").FirstOrDefault();
            Department department = currentstaff.Department;

            return department;
        }

    }
}
