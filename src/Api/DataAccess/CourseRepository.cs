using System.Linq;
using DomainModel;

namespace Api.DataAccess
{
    public sealed class CourseRepository
    {
        private static readonly Course[] AllCourses =
        {
            new(1, "Calculus", 5),
            new(2, "History", 4),
            new(3, "Literature", 4)
        };

        public Course GetByName(string name)
        {
            return AllCourses.SingleOrDefault(x => x.Name == name);
        }
    }
}
