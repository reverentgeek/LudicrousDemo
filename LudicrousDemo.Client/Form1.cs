using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LudicrousDemo.Common;
using ServiceStack.ServiceClient.Web;

namespace LudicrousDemo.Client
{
    public partial class Form1 : Form
    {
        private const string BaseUrl = "http://localdemo.com/api";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefereshCustomerList();
        }

        private void RefereshCustomerList()
        {
            listView1.Items.Clear();
            ClearCustomer();
            var client = new JsonServiceClient(BaseUrl);
            var customers = client.Get<List<Customer>>(new CustomerSearch());
            foreach (var customer in customers)
            {
                var item = new ListViewItem {Text = customer.UserName, Tag = customer.Id};
                item.SubItems.Add(customer.FirstName);
                item.SubItems.Add(customer.LastName);
                item.SubItems.Add(customer.Email);
                listView1.Items.Add(item);
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = listView1.SelectedItems.Count > 0 ? listView1.SelectedItems[0] : null;
            if (item == null)
            {
                ClearCustomer();
            }
            else
            {
                var id = (long) item.Tag;
                var client = new JsonServiceClient(BaseUrl);
                var customers = client.Get(new CustomerSearch{ Ids = new long[] { id }});
                if (customers != null && customers.Count > 0)
                    ShowCustomer(customers[0]);
            }
        }

        private void ShowCustomer(Customer customer)
        {
            FirstName.Text = customer.FirstName;
            LastName.Text = customer.LastName;
            UserName.Text = customer.UserName;
            Email.Text = customer.Email;
            CustomerId.Text = customer.Id.ToString();
            CurrentId = customer.Id;
        }

        private void ClearCustomer()
        {
            FirstName.Text = "";
            LastName.Text = "";
            UserName.Text = "";
            Email.Text = "";
            CustomerId.Text = "";
            CurrentId = default(long);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var customer = new Customer
            {
                Id = CurrentId,
                Email = Email.Text,
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                UserName = UserName.Text
            };
            var client = new JsonServiceClient(BaseUrl);
            if (CurrentId != default(long))
                client.Post(customer);
            else
                client.Put(customer);

            RefereshCustomerList();
        }

        private void AddNew_Click(object sender, EventArgs e)
        {
            ClearCustomer();
            UserName.Focus();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (CurrentId == default(long)) return;

            var client = new JsonServiceClient(BaseUrl);
            client.Delete<Customer>(new Customer {Id = CurrentId});
            RefereshCustomerList();
        }

        private long CurrentId { get; set; }
    }
}
