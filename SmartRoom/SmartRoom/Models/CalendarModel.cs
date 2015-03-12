//need to add the Nuget package https://www.nuget.org/packages/Google.Apis.Calendar.v3/
//PM> Install-Package Google.Apis.Calendar.v3

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace SmartRoom.Web.Models
{
    public class CalendarModel
    {
        //RESOURCES:    Acl (access control)[delete, get, insert, list, patch, update, watch]
        //              CalendarList [delete, get, insert, list, patch, update, watch]
        //              Calenders [clear, delete, get, insert, patch, update]
        //              Channels [stop]
        //              Colors [get]
        //              Events [delete, get, import, insert, instances, list, move, patch, quickAdd, update, watch]
        //              FreeBusy [get]
        //              Settings [get, list, watch]
        //here is where I insert the Google API stuffs
    }
}