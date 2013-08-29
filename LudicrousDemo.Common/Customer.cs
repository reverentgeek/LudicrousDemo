using ServiceStack.ServiceHost;

namespace LudicrousDemo.Common
{
    [Route("/customer", "POST,OPTIONS")]
    [Route("/customer/{id}", "GET,PUT,DELETE")]
    public class Customer : IReturn<Customer>
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
