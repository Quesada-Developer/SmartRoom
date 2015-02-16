using SmartRoom.Database;
using SmartRoom.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRoom.Web.DataServices
{
    public class getTestInfo
    {
        private readonly int _id;

        public int Id
        {
            get { return _id; }
        }

        private readonly string _name;

        public string Name
        {
            get { return _name; }
        } 

        private readonly bool _test;

        public bool Test
        {
            get { return _test; }
        } 

        private readonly DateTime _todaysdate;

        public DateTime Todaysdate
        {
            get { return _todaysdate; }
        }


        public getTestInfo(int id, string name, bool test, DateTime today)
        {
            this._id = id;
            this._name = name;
            this._test = test;
            this._todaysdate = today;
        }
        public getTestInfo(string name)
        {
            this._name = name;
        }
    }

    public class GetTestInfoList : List<getTestInfo>
    {
        public GetTestInfoList(SrEntities db)
        {
            var query = from test in db.tests
                        select test;
            var result = query.FirstOrDefault();

            if (result != null)
            {
                foreach (var q in query)
                {
                    Add(new getTestInfo(q.id, q.name, q.testBool, q.todaysdate));
                }
            }
        }
        public GetTestInfoList(SrEntities db)
        {
            var query = from test in db.tests
                        select test.name;
            var result = query.FirstOrDefault();

            if (result != null)
            {
                foreach (var q in query)
                {
                    Add(new getTestInfo(q));
                }
            }
        }
        public GetTestInfoList(SrEntities db, DateTime twoyearsago)
        {
            var query = from test in db.tests
                        where test.todaysdate >= twoyearsago
                        select test.name;
            var result = query.FirstOrDefault();

            if (result != null)
            {
                foreach (var q in query)
                {
                    Add(new getTestInfo(q));
                }
            }
        }
        public GetTestInfoList(SrEntities db, int id)
        {
            var query = from test in db.tests
                        where test.id == id
                        select test.name;
            var result = query.FirstOrDefault();

            if (result != null)
            {
                foreach (var q in query)
                {
                    Add(new getTestInfo(q));
                }
            }
        }
        public GetTestInfoList(SrEntities db, string name)
        {
            var query = from test in db.tests
                        join test2 in db.test2cs on test.id equals test2.id
                        select test.name;
            var result = query.FirstOrDefault();

            if (result != null)
            {
                foreach (var q in query)
                {
                    Add(new getTestInfo(q));
                }
            }
        }

        public GetTestInfoList(SrEntities db)
        {
            test r1 = new test { id=2,name="whatever",todaysdate=DateTime.Now};

            db.tests.Add(r1);
            db.SaveChanges();
        }
    }
}