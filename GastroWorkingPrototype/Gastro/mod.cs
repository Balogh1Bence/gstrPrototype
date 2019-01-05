using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gastro
{
    public partial class mod : Form
    {

        private Termekek k;
        public mod(Termekek k)
        {

            this.k = k;

            InitializeComponent();

            MessageBox.Show("Test");
            Tkod.Text = k.getTkod().ToString();
            Tnev.Text = k.getTnev();
            Tar.Text = k.getTar().ToString();
            Tkeszl.Text = k.getTkeszl().ToString();
            Tmert.Text = k.getTmert();
            Tkatkod.Text = k.getTkeszl().ToString();


        }

        private void mod_Load(object sender, EventArgs e)
        {

        }

        private void ok_Click(object sender, EventArgs e)
        {

            k.setTkod(Convert.ToInt32(Tkod.Text));
            k.setTnev(Tnev.Text);
            k.setTar((Convert.ToInt32(Tar.Text)));
            k.setTkeszl(Convert.ToInt32(Tkeszl.Text));
            k.setTmert(Tmert.Text);
            k.setTkatkod(Convert.ToInt32(Tkatkod.Text));


            this.Close();
        }    
        /*public int getTkod() { return Convert.ToInt32(Tkod.Text); }
        public string getNev() { return Tnev.Text; }
        public int getTar() { return Convert.ToInt32(Tar.Text); }
        public int getTkeszl() { return Convert.ToInt32(Tkeszl.Text); }
        public string getTmert() { return Tmert.Text; }
        public int getTkatkod() { return Convert.ToInt32(Tkatkod.Text); }*/
        

    }
}
