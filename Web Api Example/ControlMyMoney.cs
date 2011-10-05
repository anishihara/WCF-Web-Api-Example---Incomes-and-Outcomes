using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Web_Api_Example
{
    [ServiceContract]
    public class ControlMyMoney
    {
        [WebGet(UriTemplate = "")]
        public double expenses()
        {
            return -992.12;
        }
    }
}