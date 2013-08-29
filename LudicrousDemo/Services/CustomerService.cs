using System.Collections.Generic;
using LudicrousDemo.Common;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Cors;

namespace LudicrousDemo.Services
{
    public class CustomerService : Service
    {
        public IList<Customer> Any(CustomerSearch search)
        {
            // Example retrieving using OrmLite
            //if (search.Ids != null && search.Ids.Length > 0)
            //    return Db.GetByIds<Customer>(search.Ids);

            // Example retrieving using Redis
            if (search.Ids != null && search.Ids.Length > 0)
                return Redis.As<Customer>().GetByIds(search.Ids);

            return Redis.As<Customer>().GetAll();
        }

        public Customer Post(Customer customer)
        {
            var redis = Redis.As<Customer>();

            if (customer.Id == default(long))
                customer.Id = redis.GetNextSequence();

            redis.Store(customer);

            return customer;
        }

        public Customer Put(Customer customer)
        {
            return Post(customer);
        }

        public void Delete(Customer customer)
        {
            if (customer.Id == default (long)) return;
            Redis.As<Customer>().DeleteById(customer.Id);
        }

        [EnableCors("*", "GET,POST,PUT,DELETE,OPTIONS")]
        public void Options(Customer customer)
        {
            
        }
    }
}