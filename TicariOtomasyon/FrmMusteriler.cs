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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }
        OracleBaglantisi bgl = new OracleBaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            OracleDataAdapter da = new OracleDataAdapter("Select * From TBL_MUSTERILER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void sehirlistesi()
        {
            OracleCommand komut = new OracleCommand("select IL_ISIM from TBL_ILLER", bgl.baglanti());
            OracleDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbIl.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
        void temizle()
        {
            TxtAd.Text = "";
            TxtId.Text = "";
            TxtSoyad.Text = "";
            TxtMail.Text = "";
            RchAdres.Text = "";
            MskTelefon.Text = "";
            MskTelefon2.Text = "";
            
            RchAdres.Text = "";
            TxtVergi.Text = "";
            CmbIl.Text = "";
            CmbIlce.Text = "";


        }
        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();
            sehirlistesi();
            temizle();
        }

        private void CmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbIlce.Properties.Items.Clear();
            OracleCommand komut = new OracleCommand("select ILCE_ISIM from TBL_ILCELER where IL_NO=:p1", bgl.baglanti());
            komut.Parameters.Add("@p1",CmbIl.SelectedIndex+1);
            OracleDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbIlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            OracleCommand komut = new OracleCommand("insert into TBL_MUSTERILER(ID,AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE) VALUES(:p1,:p2,:p3,:p4,:p5,:p6,:p7,:p8,:p9,:p10,:p11)" , bgl.baglanti());
            komut.Parameters.Add("@p1", TxtId.Text);
            komut.Parameters.Add("@p2", TxtAd.Text);
            komut.Parameters.Add("@p3", TxtSoyad.Text);
            komut.Parameters.Add("@p4", MskTelefon.Text);
            komut.Parameters.Add("@p5", MskTelefon2.Text);
            komut.Parameters.Add("@p6", MskTc.Text);
            komut.Parameters.Add("@p7", TxtMail.Text);
            komut.Parameters.Add("@p8", CmbIl.Text);
            komut.Parameters.Add("@p9", CmbIlce.Text);
            komut.Parameters.Add("@p10", RchAdres.Text);
            komut.Parameters.Add("@p11", TxtVergi.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri sisteme eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();




        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                TxtId.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSoyad.Text = dr["SOYAD"].ToString();
                MskTelefon.Text = dr["TELEFON"].ToString();
                MskTelefon2.Text = dr["TELEFON2"].ToString();
                CmbIl.Text = dr["IL"].ToString();
                CmbIlce.Text = dr["ILCE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
                TxtVergi.Text = dr["VERGIDAIRE"].ToString();
                MskTc.Text = dr["TC"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();

            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            OracleCommand komut = new OracleCommand("delete from TBL_MUSTERILER where ID=:p1", bgl.baglanti());
            komut.Parameters.Add(":p1", TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            OracleCommand komut = new OracleCommand("update TBL_MUSTERILER set  AD=:P1, SOYAD=:P2, TELEFON=:P3, TELEFON2=:P4, TC=:P5, MAIL=:P6, IL=:P7,ILCE=:P8, VERGIDAIRE=:P9, ADRES=:P10 where ID=:P11", bgl.baglanti());

            komut.Parameters.Add("@p1", TxtAd.Text);
            komut.Parameters.Add("@p2", TxtSoyad.Text);
            komut.Parameters.Add("@p3", MskTelefon.Text);
            komut.Parameters.Add("@p4", MskTelefon2.Text);
            komut.Parameters.Add("@p5", MskTc.Text); ;
            komut.Parameters.Add("@p6", TxtMail.Text);
            komut.Parameters.Add("@p7", CmbIl.Text);
            komut.Parameters.Add("@p8", CmbIlce.Text);
            komut.Parameters.Add("@p9", TxtVergi.Text);
            komut.Parameters.Add("@p10", RchAdres.Text);
            komut.Parameters.Add("@p11", TxtId.Text);

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }
    }
}
