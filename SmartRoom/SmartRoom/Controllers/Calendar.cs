using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRoom.Web.Controllers
{

    public class Calendar
    {

    }
    public class createEvent(String Summary,){
        
        event myevent = new Event()
        {
                Summary = "Appointment",
                Location = "Somewhere",
                Start = new EventDateTime() {
                DateTime = new DateTime("2011-06-03T10:00:00.000:-07:00"),
                TimeZone = "America/Los_Angeles"
            },
            End = new EventDateTime() {
                DateTime = new DateTime("2011-06-03T10:25:00.000:-07:00"),
                TimeZone = "America/Los_Angeles"
            },
            Recurrence = new String[] {
                "RRULE:FREQ=WEEKLY;UNTIL=20110701T170000Z"
            },
            Attendees = new List<EventAttendee>()
            {
                new EventAttendee() { Email: "divepup101@gmail.com" }
            }
        };
    }
}