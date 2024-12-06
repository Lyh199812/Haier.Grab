using System;

namespace Base.Client.Entity
{
    public class ResultEntity<T>
    {
        //        {
        //  "state": 200,
        //  "exceptionMessage": null,
        //  "data": {
        //    "userId": 3,
        //    "userName": "admin",
        //    "password": "ABFB5D41B5DCCF7B34A90F32EC475E77",
        //    "userIcon": "image/show/1001.png",
        //    "realName": "Administrator",
        //    "age": 30,
        //    "state": 1
        //  }
        //}

        public int state { get; set; }
        public string exceptionMessage { get; set; }
        public T data { get; set; }
    }
}
