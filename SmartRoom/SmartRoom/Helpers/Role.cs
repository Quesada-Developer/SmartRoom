﻿
namespace SmartRoom.Web.Helpers
{

    //NotAuthorized is used in methods as a return value so thrrows are not used
    public enum CourseRole
    {
        owner = 0, professor = 1, nonStudentRole = 2, student = 3, visitor = 4, NotAuthorized = 404 
    }
}