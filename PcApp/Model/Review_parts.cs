using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcApp.Model
{
    class Review_parts
    {
        private int P_id;
        public int part_id
        {
            get { return P_id; }
            set { P_id = value; }
        }

        private int R_id;
        public int review_id
        {
            get { return R_id; }
            set { R_id = value; }
        }
    }
}
