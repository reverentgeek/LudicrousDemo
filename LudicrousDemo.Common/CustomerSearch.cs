using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace LudicrousDemo.Common
{
    [Route("/customer/search")]
    public class CustomerSearch : IReturn<List<Customer>>
    {
        public long[] Ids { get; set; }
        public string[] UserNames { get; set; }
        public string[] Emails { get; set; }
    }
}