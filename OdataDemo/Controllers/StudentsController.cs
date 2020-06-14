using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using OdataDemo.Models;

namespace OdataDemo.Controllers
{
    //[Route("odata/[Controller]")]
    //[ApiController]
    public class StudentsController: ODataController
    {
        //[HttpGet]
        [EnableQuery()]
        public IEnumerable<Student> Get()
        {
            return new List<Student>
            {
                CreateNewStudent("0f8fad5b-d9cb-469f-a165-708677289502", "Cody Allen", 130),
                CreateNewStudent("1f8fad5b-d9cb-469f-a165-708677289502", "Viral Pandya", 140),
                CreateNewStudent("2f8fad5b-d9cb-469f-a165-708677289502", "student3", 120),
                CreateNewStudent("3f8fad5b-d9cb-469f-a165-708677289502", "student4", 150),
                CreateNewStudent("4f8fad5b-d9cb-469f-a165-708677289502", "student5", 100)
            };
        }
        
        public Student CreateNewStudent(string id, string name, int score)
        {
            return new Student
            {
                Id = new Guid(id),
                Name = name,
                Score = score
            };
        }
    }
}
