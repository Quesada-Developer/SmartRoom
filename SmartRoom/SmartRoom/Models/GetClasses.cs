using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRoom.Web.Models
{
    public class GetCourse
    {
        private readonly string _CourseTitle;

        public string CourseTitle
        {
            get { return _CourseTitle; }
        } 

        public GetCourse(string CourseTitle)
        {
            this._CourseTitle = CourseTitle;
        }
    }
    public class GetCourseList:List<GetCourse>
    {
        private readonly SmartModel db = new SmartModel();

        public GetCourseList(string userId)
        {
            var query = from cr in db.ClassRoles
                        join c in db.Courses on cr.CourseId equals c.CourseNumber
                        where cr.AccountId == userId
                        select new
                        {
                            c.Title,
                            c.Subject
                        };

            var result = query.FirstOrDefault();

            if (result != null)
                foreach(var q in query)
                    Add(new GetCourse(q.Title));

        }
    }
}