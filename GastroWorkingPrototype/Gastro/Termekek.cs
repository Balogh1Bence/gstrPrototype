using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastro
{
    public class Termekek
    {
       private int Tkod;
       private string Tnev;
       private int Tar;
       private int Tkeszl;
       private string Tmert;
       private int Tkatkod;
        public Termekek(int Tkod, string Tnev, int Tar, int Tkeszl, string Tmert,  int Tkatkod)
        {
            this.Tkod = Tkod;
            this.Tnev = Tnev;
            this.Tar = Tar;
            this.Tkeszl = Tkeszl;
            this.Tmert = Tmert;
            this.Tkatkod = Tkatkod;
        }
       public Termekek() { this.Tkod = 0; this.Tnev = ""; this.Tar = 0; this.Tkeszl = 0; this.Tmert = ""; this.Tkatkod = 0;  }
        public void setTkod (int Tkod) { this.Tkod = Tkod; }
        public void setTnev(string Tnev) { this.Tnev = Tnev; }
        public void setTar(int Tar) { this.Tar=Tar; }
        public void setTkeszl(int Tkeszl) { this.Tkeszl = Tkeszl; }
        public void setTmert(string Tmert) { this.Tmert = Tmert; }
        public void setTkatkod(int Tkatkod) { this.Tkatkod = Tkatkod; }

        public int getTkod() { return Tkod; }
        public string getTnev() { return Tnev; }
        public int getTar() { return Tar; }
        public int getTkeszl() { return Tkeszl; }
        public string getTmert() { return Tmert; }
        public int getTkatkod() { return Tkatkod; }





    }
}
