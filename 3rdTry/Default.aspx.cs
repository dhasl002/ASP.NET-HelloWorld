using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3rdTry
{
    
    public partial class _Default : Page
    {

        static string blockChain = "";

        public string getBlockChain()
        {
            return blockChain;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Update(object sender, EventArgs e)
        {
            blockChain += UserName.Text.ToString() + " ";
        }
    }
}