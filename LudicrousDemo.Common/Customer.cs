﻿using ServiceStack.ServiceHost;

namespace LudicrousDemo.Common
{
    [Route("/customer")]
    [Route("/customer/{id}")]
    public class Customer : IReturn<Customer>
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
