using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using web_Api_crud.Models;

namespace web_Api_crud.Controllers
{
    public class Crud_APIController : ApiController
    {
        Web_APIEntities Db = new Web_APIEntities();
        [HttpGet]
       public IHttpActionResult GetEmployees()
        {
            List<Employee> list = Db.Employees.ToList();
            return Ok(list);
        }
        [HttpGet]
        public IHttpActionResult GetEmployeesById(int id)
        {
            var emp = Db.Employees.Where(model => model.Id == id).FirstOrDefault();
            return Ok(emp);
        }
        [HttpPost]
        public IHttpActionResult InsertEmp(Employee e)
        {
            Db.Employees.Add(e);
            Db.SaveChanges();
            return Ok();
        }
        [HttpPut]
        public IHttpActionResult Empupdate(Employee e)
        {
            var emp = Db.Employees.Where(model => model.Id == e.Id).FirstOrDefault();
            if (emp != null)
            {
                emp.Id = e.Id;
                emp.Name = e.Name;
                emp.gender = e.gender;
                emp.Age = e.Age;
                emp.Designation = e.Designation;
                emp.Salary = e.Salary;
                Db.SaveChanges();


            }
            else NotFound();
            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult EmpDelete (int id)
        {
            var emp = Db.Employees.Where(model => model.Id == id).FirstOrDefault();
            Db.Entry(emp).State=EntityState.Deleted;
            Db.SaveChanges();
            
            return Ok(emp);
        }

    }
}
