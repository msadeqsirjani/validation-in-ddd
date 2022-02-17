﻿using System.Collections.Generic;
using System.Linq;
using DomainModel;

namespace Api.DataAccess
{
    public class StudentRepository
    {
        private static readonly List<Student> ExistingStudents = new()
        {
            Alice(),
            Bob()
        };

        private static long _lastId = ExistingStudents.Max(x => x.Id);

        public Student GetById(long id)
        {
            // Retrieving from the database
            return ExistingStudents.SingleOrDefault(x => x.Id == id);
        }

        public void Save(Student student)
        {
            // Setting up the id for new students emulates the ORM behavior
            if (student.Id == 0)
            {
                _lastId++;
                SetId(student, _lastId);
            }

            // Saving to the database
            ExistingStudents.RemoveAll(x => x.Id == student.Id);
            ExistingStudents.Add(student);
        }

        private static void SetId(Entity entity, long id)
        {
            // The use of reflection to set up the Id emulates the ORM behavior
            entity.GetType().GetProperty(nameof(Entity.Id))?.SetValue(entity, id);
        }

        private static Student Alice()
        {
            var alice = new Student(Email.Initial("alice@gmail.com").Value, StudentName.Initial("Alice Alison").Value,
                new List<Address> { new("1234 Main St", "Arlington", "VA", "22201") });
            SetId(alice, 1);
            alice.Enroll(new Course(1, "Calculus", 5), Grade.A);

            return alice;
        }

        private static Student Bob()
        {
            var bob = new Student(Email.Initial("bob@gmail.com").Value, StudentName.Initial("Bob Bobson").Value,
                new List<Address> { new("2345 Second St", "Barlington", "VA", "22202") });
            SetId(bob, 2);
            bob.Enroll(new Course(2, "History", 4), Grade.B);

            return bob;
        }
    }
}
