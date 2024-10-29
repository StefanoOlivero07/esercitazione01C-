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

namespace EsercitazioneC__Ottobre
{
    public partial class Form1 : Form
    {
        List<Contatto> listContatti = new List<Contatto>();

        public Form1()
        {
            InitializeComponent();
            leggiFile();
        }

        private void leggiFile()
        {
            StreamReader sr = new StreamReader("rubrica.csv");
            Contatto persona;
            string[] s;

            while (!sr.EndOfStream)
            {
                s = sr.ReadLine().Split(',');
                persona = Contatto.creaContatto(s[0], s[1], listContatti);
                listContatti.Add(persona);
            }
            sr.Close();
            visualizzaListBox();
        }

        private void btnAggiungi_Click(object sender, EventArgs e)
        {
            string nominativo = txtNominativo.Text;
            string telefono = txtTelefono.Text;

            Contatto persona = Contatto.creaContatto(nominativo, telefono, listContatti);

            if (persona != null)
            {
                svuotaTextBox();
                listContatti.Add(persona);
                visualizzaListBox();
            }
            else
                MessageBox.Show("Contatto già esistente o vuoto", "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void visualizzaListBox()
        {
            lstContatti.Items.Clear();
            foreach (var contatto in listContatti)
                lstContatti.Items.Add(contatto);
        }

        private void btnElimina_Click(object sender, EventArgs e)
        {
            if (lstContatti.SelectedIndex >= 0)
            {
                svuotaTextBox();

                listContatti.RemoveRange(lstContatti.SelectedIndex, 1);
                visualizzaListBox();
            }
        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            if (lstContatti.SelectedIndex >= 0)
            {
                if (txtNominativo.Text != listContatti[lstContatti.SelectedIndex].Nominativo || 
                    txtTelefono.Text != listContatti[lstContatti.SelectedIndex].Telefono)
                {
                    Contatto persona = Contatto.creaContatto(txtNominativo.Text, txtTelefono.Text, listContatti);

                    if (persona != null)
                    {
                        listContatti[lstContatti.SelectedIndex] = persona;
                        svuotaTextBox();
                        visualizzaListBox();
                    }
                    else
                        MessageBox.Show("Il contatto è già esistente o vuoto", "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        private void lstContatti_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNominativo.Text = listContatti[lstContatti.SelectedIndex].Nominativo;
            txtTelefono.Text = listContatti[lstContatti.SelectedIndex].Telefono;
        }
        private void svuotaTextBox()
        {
            txtNominativo.Text = "";
            txtTelefono.Text = "";
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("rubrica.csv");
            string[] s;

            foreach (var contatto in listContatti)
            {
                s = contatto.ToString().Split('-');
                sw.WriteLine(s[0] + "," + s[1]);
            }
            sw.Close();
        }
    }
}
