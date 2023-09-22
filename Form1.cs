using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;


namespace ADO_Desconectado
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DataSet ds;
        DataTable dt;

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            ds = new DataSet("Gestion");

            if (File.Exists("Datos.xml"))
            {
                ds.ReadXml("Datos.xml");
                dt = ds.Tables[0];
            }
            else
            {
                dt = new DataTable("Persona");

                dt.Columns.AddRange(new DataColumn[] {new DataColumn("DNI",typeof(string)),
                                                  new DataColumn("Nombre",typeof(string)),
                                                  new DataColumn("Apellido",typeof(string))});
                dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
                ds.Tables.Add(dt);
            }
            dataGridView1.DataSource = dt.DefaultView;
            foreach (DataGridViewColumn x in dataGridView1.Columns) { x.Width = 180; }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                DataRow dr = dt.NewRow();
                dr.ItemArray = new object[] { Interaction.InputBox("DNI: "), Interaction.InputBox("Nombre: "), Interaction.InputBox("Apellido: ") };
                dt.Rows.Add(dr);
                Guardar();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                if (dt.Rows.Count > 0) { dt.Rows.Remove((dataGridView1.SelectedRows[0].DataBoundItem as DataRowView).Row); }
                Guardar();
            
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                DataRow dr = (dataGridView1.SelectedRows[0].DataBoundItem as DataRowView).Row;
                string _nombre =  Interaction.InputBox("Nombre: ","", dr.ItemArray[1].ToString());
                string _apellido = Interaction.InputBox("Apellido: ", "", dr.ItemArray[2].ToString());
                dr.ItemArray = new object[] { dr.ItemArray[0].ToString(),_nombre,_apellido };
                Guardar();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void Guardar()
        {
            ds.WriteXml("Datos.xml", XmlWriteMode.WriteSchema);
        }
    }
}
