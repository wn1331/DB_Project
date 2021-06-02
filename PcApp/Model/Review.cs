using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcApp.Model
{
    class Review
    {
        private int R_id;
        public int review_id
        {
            get { return R_id; }
            set { R_id = value; }
        }
        //
        private string U_id;
        public string user_id
        {
            get { return U_id; }
            set { U_id = value; }
        }

        private string R_text;
        public string review_text
        {
            get { return R_text; }
            set { R_text = value; }
        }
    }
}
