using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace TicariOtomasyon
{
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }

        OracleBaglantisi bgl = new OracleBaglantisi();
        private void BtnGiris_Click(object sender, EventArgs e)
        {
            OracleCommand komut = new OracleCommand("select * from TBL_ADMIN where KULLANICIADI=:p1 and SIFRE=:p2",bgl.baglanti());
            komut.Parameters.Add("@p1", TxtKullanici.Text);
            komut.Parameters.Add("@p2", TxtSifre.Text);
           // komut.ExecuteNonQuery();
            OracleDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmAnaModul fr = new FrmAnaModul();
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı ya da Şifre", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            bgl.baglanti().Close();
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {

        }

        private void BtnMusteriGiris_Click(object sender, EventArgs e)
        {
            FrmStoklar fr = new FrmStoklar();
            fr.Show();
            this.Hide();
        }
    }
}
