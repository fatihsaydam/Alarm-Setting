using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmKur
{
    public partial class Form1 : Form
    {
        SoundPlayer _alarmSesi = new SoundPlayer();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tmrFormSaati.Start();
        }

        private void tmrFormSaati_Tick(object sender, EventArgs e)
        {
            this.Text = "Alarm    " + " Saat " + DateTime.Now.ToShortTimeString();
        }

        private void btnKur_Click(object sender, EventArgs e)
        {
            tmrAlarm.Enabled = true;
            this.Hide();
        }

        private void tmrAlarm_Tick(object sender, EventArgs e)
        {
            
            if (mtxtSaat.Text == DateTime.Now.ToShortTimeString() && (dtpTarih.Value.ToShortDateString()==DateTime.Now.ToShortDateString()))
            {
                if (rdbSessiz.Checked)
                {
                    tmrAlarm.Enabled = false;
                    this.Show();
                    tmrResimTitrestir.Enabled = true;
                }
                else
                {
                    tmrAlarm.Enabled = false;
                    this.Show();
                    _alarmSesi.Play();
                    tmrResimTitrestir.Enabled = true;
                }
                
            }
        }

        private void tmrResimTitrestir_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            Point b = pcbResim.Location;
            for (int i = 0; i < 50; i++)
            {
                int x = r.Next(1, 10);
                int y = r.Next(1, 10);
                pcbResim.Location = new Point(b.X + x, b.Y + y);
                Thread.Sleep(0);
            }
            pcbResim.Location = b;
        }

        private void btnDurdur_Click(object sender, EventArgs e)
        {
            tmrResimTitrestir.Enabled = false;
            _alarmSesi.Stop();
        }

        private void gösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnIptalEt_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(mtxtSaat.Text + " alarmını iptal etmek istediğinizden emin misiniz? ", "Uyarı", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
            {
                return;
            }

            mtxtSaat.Text = null;
        }

        private void btnSesDosyasi_Click(object sender, EventArgs e)
        {
            DialogResult btnSesDosyasıSec = openFileDialogSesDosyasi.ShowDialog();
            _alarmSesi.SoundLocation = openFileDialogSesDosyasi.FileName;
        }
    }
}
