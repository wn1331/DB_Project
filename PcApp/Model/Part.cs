using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcApp.Model
{

    class Part : Notifier
    {
        private string id;
        public string part_id
        {
            get { return id; }
            set { id = value; }
        }


        private string category;
        public string part_category
        {
            get { return category; }
            set { category = value; }
        }


        private string name;
        public string part_name
        {
            get { return name; }
            set { name = value;
          
            }
        }


        private int price;
        public int part_price
        {
            get { return price; }
            set { price = value; }
        }


        private string company;
        public string part_company
        {
            get { return company; }
            set { company = value; }
        }



        private string quantity;//수량
        public string part_quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
    }
}
