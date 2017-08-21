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
        public string ConnStr = "DSN=MySQL";
        public Form1()
        {
            InitializeComponent();
        }

        void FillData(String table)
        {
            using (OdbcConnection c = new OdbcConnection(ConnStr))
            {
                c.Open();
                using (OdbcDataAdapter adapter = new OdbcDataAdapter("Select * from " + table, c))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Tag = table;
                }
                c.Close();
            }
        }

        void Delete(int row, String table)
        {
            String sql = "Delete from " + table + " WHERE id= " + row;
            CommandSQL(sql);
        }

        void Add(String row, String table)
        {
            String sql = "INSERT INTO " + table + " WHERE id= " + row;
            CommandSQL(sql);
        }

        void CommandSQL(String sql)
        {
            using (OdbcConnection c = new OdbcConnection(ConnStr))
            {
                c.Open();
                using (OdbcCommand command = new OdbcCommand(sql, c))
                {
                    command.ExecuteNonQuery();
                }
                c.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            FillData(((Label)sender).Tag.ToString());
        }

        private void label2_Click(object sender, EventArgs e)
        {
            FillData(((Label)sender).Tag.ToString());
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int rowSelected = e.RowIndex;
                if (e.RowIndex != -1)
                {
                    this.dataGridView1.Rows[rowSelected].Selected = true;
                }
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {

        }

        private void delete_Click(object sender, EventArgs e)
        {
    
            DataGridView ContextMenuOwner = (DataGridView)((ContextMenuStrip)((ToolStripItem)sender).Owner).SourceControl;         
            Delete(ContextMenuOwner.CurrentCell.RowIndex+1,ContextMenuOwner.Tag.ToString());
            FillData(ContextMenuOwner.Tag.ToString());
        }




    }
}
