using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FillData();
        }

        void FillData()
        {
            using (OdbcConnection c = new OdbcConnection("DSN=MySQL"))
            {
                c.Open();
                using (OdbcDataAdapter adapter = new OdbcDataAdapter("Select * from protections", c))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
        }
    }
}
