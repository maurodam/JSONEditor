using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using DataTable = System.Data.DataTable;

namespace JSONEditor
{
    public partial class Home : Form
    {
        private DataTable dataTable;
        private string jsonString;

        public Home()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (!Check())
                return;

            string jsonString = txtJson.Text;

            try
            {
                DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(jsonString);
                dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il caricamento del JSON: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Boolean Check()
        {
            var result = true;

            if (txtJson.Text.Trim() == "")
                result = false;

            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Check())
                return;

            try
            {
                dataTable = (DataTable)dataGridView.DataSource;

                jsonString = JsonConvert.SerializeObject(dataTable, Formatting.Indented);

                if (!jsonString.StartsWith("[") || !jsonString.EndsWith("]"))
                    jsonString = "[" + jsonString + "]";

                // Scrivi la stringa JSON nel file di testo
                //File.WriteAllText("data.json", jsonString);
                //MessageBox.Show("Modifiche salvate correttamente.", "Salvataggio", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtJson.Text = jsonString;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il salvataggio del JSON: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataTable = new DataTable();
            dataGridView.DataSource = dataTable;
        }

        void CleanAllForm(){
            txtJson.Text = "";
            dataGridView.DataSource = null;
        }
    }
}
