using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel
{
    public class Student : Entity
    {
        public Email Email { get; }
        public StudentName Name { get; private set; }
        public ICollection<Address> Addresses { get; private set; }

        private readonly List<Enrollment> _enrollments = new();
        public virtual ICollection<Enrollment> Enrollments => _enrollments.ToList();

        protected Student()
        {
        }

        public Student(Email email, StudentName name, ICollection<Address> addresses) : this()
        {
            Email = email;
            EditPersonalInfo(name, addresses);
        }

        public void EditPersonalInfo(StudentName name, ICollection<Address> addresses)
        {
            Name = name;
            Addresses = addresses;
        }

        public virtual void Enroll(Course course, Grade grade)
        {
            if (_enrollments.Count >= 2)
                throw new Exception("Cannot have more than 2 enrollments");

            if (_enrollments.Any(x => x.Course == course))
                throw new Exception($"Student '{Name}' already enrolled into course '{course.Name}'");

            var enrollment = new Enrollment(this, course, grade);
            _enrollments.Add(enrollment);
        }
    }
}
