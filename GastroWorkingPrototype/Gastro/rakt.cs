using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConnectToMysqlDatabase;
namespace Gastro
{
    public partial class rakt : Form
    {
        MySQLDatabaseInterface mdi = new MySQLDatabaseInterface();
        private DataTable termekek;
        private bool lettmodositva = false;
        public rakt()
        {
            InitializeComponent();
            vezerlokindulaskor();
        }

        private void betöltés_Click(object sender, EventArgs e)
        {
            feltolt();
            betöltés.Visible = false;
            
        }
        private void feltolt()
        {
            Adatbazis a = new Adatbazis();
            mdi = a.kapcsolodas(); mdi.open();
            termekek = mdi.getToDataTable("SELECT * FROM termekek");
            
            dataGridView1.DataSource = termekek;
            szerkesztés.Visible = true;
        }
        private void vezerlokindulaskor() {
            új.Visible = false;
            szerkesztés.Visible = false;
            törlés.Visible = false;
            mégsem.Visible = false;
            mentés.Visible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToDeleteRows = false;

        }
        private void beallitvezerloketnemszerk()
        {
            új.Visible = true;
            szerkesztés.Visible = true;
            törlés.Visible = true;
            mentés.Visible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToDeleteRows = false;



        }

        private void szerkesztés_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
            int kod = 0, ar = 0, keszl = 0, katkod = 0;
            string nev = "", mert = "";
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                kod = Convert.ToInt32(row.Cells[0].Value.ToString());
                MessageBox.Show(kod+" ");

                nev = row.Cells[1].Value.ToString();
                ar = Convert.ToInt32(row.Cells[2].Value.ToString());
                keszl = Convert.ToInt32(row.Cells[3].Value.ToString());
                mert = row.Cells[4].Value.ToString();
                katkod = Convert.ToInt32(row.Cells[5].Value.ToString());


            }

            Termekek t = new Termekek(kod, nev, ar, keszl, mert, katkod);
            mod modosit = new mod(t);

            if (modosit.ShowDialog() != DialogResult.None)
            {

                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    row.Cells[0].Value = t.getTkod();
                    row.Cells[1].Value = t.getTnev();
                    row.Cells[2].Value = t.getTar();
                    row.Cells[3].Value = t.getTkeszl();
                    row.Cells[4].Value = t.getTmert();
                    row.Cells[5].Value = t.getTkatkod();

                }
            }
            beallitvezerloketnemszerk();
            dataGridView1.ReadOnly = false;
            mentés.Visible = true;
        }
        private void szerkszethetoek()
        {
            új.Visible = false;
            szerkesztés.Visible = false;
            törlés.Visible = false;
            mentés.Visible = true;      
            dataGridView1.ReadOnly = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToDeleteRows = false;         
        }

        private void mégsem_Click(object sender, EventArgs e)
        {
            feltolt();
            beallitvezerloketnemszerk();

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            lettmodositva = true;

        }

        private void mentés_Click(object sender, EventArgs e)
        {
            if (!lettmodositva)
            { MessageBox.Show("Nem lett módosítva egy adat sem.", "Módosítás", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else { mdi.updateChangesInTable(termekek);
                beallitvezerloketnemszerk();
                lettmodositva = false;
            }
        }

        private void kilépés_Click(object sender, EventArgs e)
        {
            if (lettmodositva)
            { if (MessageBox.Show("Nem mentett adatok vannak! Valóban ki akar lépni?", "Kilépés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                { mdi.close();
                    this.Close();
                } }
            else
            { mdi.close();
                this.Close(); }
        }

        private void törlés_Click(object sender, EventArgs e)
        {
            try
            {      int sor = dataGridView1.SelectedRows[0].Index;
                if (MessageBox.Show("Valóban törölni akarja a sort?", "Törlés",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation                     )== DialogResult.Yes)
                {dataGridView1.Rows.RemoveAt(sor);
                    mentés.Visible = true;
                    lettmodositva = true;
                }
                else
                    return;
            }     catch (Exception ex)
            {         MessageBox.Show(
                "Jelölje ki a törlendő sort!",
                "Törlés", 
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
        }
        private void beallitvezerloketujfelvitelkor()
        {
            új.Visible = false;
            mentés.Enabled = true;
            mentés.Visible = true;
            szerkesztés.Enabled = false;
            törlés.Enabled = false;
            mégsem.Visible = true;
            mégsem.Enabled = true;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.ReadOnly = false;

        }

        private void új_Click(object sender, EventArgs e)
        {
            beallitvezerloketujfelvitelkor();
            int sor = dataGridView1.Rows.Count - 1;
     
         
            dataGridView1.FirstDisplayedScrollingRowIndex = sor;
            dataGridView1.Columns[0].ReadOnly = true;

        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
           
            Adatbazis ujida = new Adatbazis();
            MySQLDatabaseInterface mdiujid = ujida.kapcsolodas();
            mdiujid.open();
            int max;
            bool siker = int.TryParse(mdiujid.executeScalarQuery("SELECT MAX(Tkod) FROM termekek"), out max);
            if (!siker) { MessageBox.Show("Nem lehet megállapítani a következő rekord kulcsát. Adatbázis lekérdezési hiba.",
                "Hiba...", 
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return; }
            mdiujid.close();
            e.Row.Cells[0].Value = max + 1;

        }
    }
}
