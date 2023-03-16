using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace New_work_1
{
    public partial class Товары : Form
    {

        static string connectionString = "Data Source=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;User ID=DESKTOP-Q5KENKS\\SQLEXPRESS;Initial Catalog=Trade1;Data Source=DESKTOP-Q5KENKS\\SQLEXPRESS";
        static SqlConnection connection = new SqlConnection(connectionString);
        Color col = System.Drawing.ColorTranslator.FromHtml("#fff67a");
        //Color col = Color.Red;

        List<product> lst = new List<product>();
        int cnt_rows;
 
        Dictionary<string, int> order = new Dictionary<string, int>();
        

        internal static Товары newForm;

        public Товары()
        {
            InitializeComponent();
        }

        private void Tovar_Load(object sender, EventArgs e)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = ("SELECT ProductArticleNumber, ProductName, ProductDescription, ProductCategory, ProductManufacturer, ProductProvider, ProductCost, ProductDiscountAmount, ProductDiscountMax, ProductQuantityInStock, ProductMeasure FROM Product ");
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataset = new DataSet();

            adapter.Fill(dataset);
            int cnt_rows;
            cnt_rows = dataset.Tables[0].Rows.Count;
            label3.Text = cnt_rows.ToString();

            connection.Close();
            ProductGrid.DataSource = dataset.Tables[0];



            ProductGrid.Rows[0].Selected = true;
            ProductGrid.Rows[0].Cells[0].Selected = true;

            int cnt_rows_Numb;
            cnt_rows_Numb = ProductGrid.Rows.Count - 1;
            label11.Text = cnt_rows_Numb.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void ProductGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {

            if (ProductGrid.Rows[e.RowIndex].Cells[0].Value != DBNull.Value) ;
            {
                int valu = Convert.ToInt32(ProductGrid.Rows[e.RowIndex].Cells[7].Value);

                if (valu > 3)
                {
                    ProductGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = col;
                }
            }
           
        }

        private void ProductGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {



        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    (ProductGrid.DataSource as DataTable).DefaultView.RowFilter = "";
                    break;
                case 1:
                    (ProductGrid.DataSource as DataTable).DefaultView.RowFilter = $"ProductDiscountAmount <=2";
                    break;
                case 2:
                    (ProductGrid.DataSource as DataTable).DefaultView.RowFilter = $"ProductDiscountAmount >2 and ProductDiscountAmount <5";
                    break;
                case 3:
                    (ProductGrid.DataSource as DataTable).DefaultView.RowFilter = $"ProductDiscountAmount >=5";
                    break;
            }

            int cnt_rows_Numb;
            cnt_rows_Numb = ProductGrid.Rows.Count - 1;
            label11.Text = cnt_rows_Numb.ToString();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            (ProductGrid.DataSource as DataTable).DefaultView.RowFilter = $"ProductName Like '%{searchTb.Text}%' ";

            int cnt_rows_Numb;
            cnt_rows_Numb = ProductGrid.Rows.Count - 1;
            label11.Text = cnt_rows_Numb.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            
         
           
        }

    
        private void ProductGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = ProductGrid.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    ProductGrid.ClearSelection();
                    ProductGrid.Rows[hit.RowIndex].Selected = true;
                    contextMenuStrip1.Show(ProductGrid, e.Location);
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool inOrder = false;
            string position = ProductGrid.SelectedRows[0].Cells[1].Value.ToString();
            int price = Convert.ToInt32(ProductGrid.SelectedRows[0].Cells[6].Value);
            product pr = new product(position, price, 1);

            foreach (product prod in lst)
            {
                if (prod.articule == pr.articule)
                {
                    prod.quantity++; 
                    inOrder = true;
                }
            }
            if (!inOrder)
                lst.Add(pr);

            if (lst.Count > 0)
                btnOrder.Visible = true;
            label14.Text = lst.Count().ToString();
           



            /*if (order.ContainsKey(position))
             {
                 MessageBox.Show("Выбранная позиция уже находится в корзине!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
             return;

             }

             else
                 order.Add(position, price);

             if (order.Count() > 0)
             {
                 label14.Text = order.Count().ToString();
                 btnOrder.Visible = true;
             }*/
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            
            Корзина newForm = new Корзина(lst);
            newForm.Show();

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

