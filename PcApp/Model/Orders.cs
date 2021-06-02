using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcApp.Model
{//
    class Orders
    {
        private string id;
        public string orders_id
        {
            get { return id; }
            set { id = value; }
        }


        private string U_id;
        public string user_id
        {
            get { return U_id; }
            set { U_id = value; }
        }


        private string manager;
        public string orders_manager
        {
            get { return manager; }
            set
            {
                manager = value;

            }
        }


        private int P_id;
        public int part_id
        {
            get { return P_id; }
            set { P_id = value; }
        }
    }
}
