using System;
using System.Linq;
using Api.DataAccess;
using DomainModel;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/students")]
    public class StudentController : ApplicationController
    {
        private readonly StudentRepository _studentRepository;
        private readonly CourseRepository _courseRepository;

        public StudentController(StudentRepository studentRepository, CourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            var addresses = request.Addresses.Select(x => new Address(x.Street, x.City, x.State, x.ZipCode)).ToList();
            var student = new Student(Email.Initial(request.Email).Value, StudentName.Initial(request.Name).Value, addresses);
            _studentRepository.Save(student);

            var response = new RegisterResponse
            {
                Id = student.Id
            };
            return Ok(response);
        }

        [HttpPut("{id:long}")]
        public IActionResult EditPersonalInfo(long id, EditPersonalInfoRequest request)
        {
            var student = _studentRepository.GetById(id);

            var addresses = request.Addresses.Select(x => new Address(x.Street, x.City, x.State, x.ZipCode)).ToList();
            student.EditPersonalInfo(StudentName.Initial(request.Name).Value, addresses);
            _studentRepository.Save(student);

            return Ok();
        }

        [HttpPost("{id:long}/enrollments")]
        public IActionResult Enroll(long id, EnrollRequest request)
        {
            var student = _studentRepository.GetById(id);

            foreach (var enrollmentDto in request.Enrollments)
            {
                var course = _courseRepository.GetByName(enrollmentDto.Course);
                var grade = Enum.Parse<Grade>(enrollmentDto.Grade);

                student.Enroll(course, grade);
            }

            return Ok();
        }

        [HttpGet("{id:long}")]
        public IActionResult Get(long id)
        {
            var student = _studentRepository.GetById(id);

            var response = new GetResponse
            {
                Addresses = student.Addresses.Select(x => new AddressDto
                {
                    Street = x.Street,
                    City = x.City,
                    State = x.State,
                    ZipCode = x.ZipCode
                }).ToList(),
                Email = student.Email.Value,
                Name = student.Name.Value,
                Enrollments = student.Enrollments.Select(x => new CourseEnrollmentDto
                {
                    Course = x.Course.Name,
                    Grade = x.Grade.ToString()
                }).ToArray()
            };
            return Ok(response);
        }
    }
}
