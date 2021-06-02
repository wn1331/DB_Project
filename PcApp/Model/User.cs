using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcApp.Model
{
    class User
    {
        private string id;
        public string user_id
        {
            get { return id; }
            set { id = value; }
        }

        private string pwd;
        public string user_pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }


        private string phone;
        public string user_phone
        {
            get { return phone; }
            set { phone = value; }
        }


        private int point;
        public int user_point
        {
            get { return point; }
            set { point = value; }
        }


        private string name;
        public string user_name
        {
            get { return name; }
            set { name = value; }
        }

        private string address;
        public string user_address
        {
            get { return address; }
            set { address = value; }
        }
    }
}
