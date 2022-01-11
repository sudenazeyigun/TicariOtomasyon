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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }
        OracleBaglantisi bgl = new OracleBaglantisi();
        void firmalistesi()
        {
            OracleDataAdapter da = new OracleDataAdapter("select* from TBL_FIRMALAR", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

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
            
            TxtMail.Text = "";
            TxtSektor.Text = "";
            TxtVergiDairesi.Text = "";
            TxtYetkili.Text = "";
            TxtYGorev.Text = "";
            MskFax.Text = "";
            MskTelefon1.Text = "";
            MskTelefon2.Text = "";
            MskTelefon3.Text = "";
            RchAdres.Text = "";
            
            TxtYtc.Text = "";
            CmbIl.Text = "";
            CmbIlce.Text = "";


        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            firmalistesi();
            temizle();
            sehirlistesi();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtId.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSektor.Text = dr["SEKTOR"].ToString();
                MskTelefon1.Text = dr["TELEFON1"].ToString();
                MskTelefon2.Text = dr["TELEFON2"].ToString();
                MskTelefon3.Text = dr["TELEFON3"].ToString();
                TxtYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                TxtYGorev.Text = dr["YETKILISTATU"].ToString();
                TxtYtc.Text = dr["YETKILITC"].ToString();
                CmbIl.Text = dr["IL"].ToString();
                CmbIlce.Text = dr["ILCE"].ToString();
                MskFax.Text = dr["FAX"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                TxtVergiDairesi.Text = dr["VERGIDAIRE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            OracleCommand komut = new OracleCommand("insert into TBL_FIRMALAR (ID,AD,YETKILISTATU," +
                "YETKILIADSOYAD,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,YETKILITC,SEKTOR)VALUES" +
                "(:p1,:p2,:p3,:p4,:p5,:p6,:p7,:p8,:p9,:p10,:p11,:p12,:p13,:p14,:p15)",bgl.baglanti());
            komut.Parameters.Add("@p1", TxtId.Text);
            komut.Parameters.Add("@p2", TxtAd.Text);
            komut.Parameters.Add("@p3", TxtYGorev.Text);
            komut.Parameters.Add("@p4", TxtYetkili.Text);
            komut.Parameters.Add("@p5", MskTelefon1.Text);
            komut.Parameters.Add("@p6", MskTelefon2.Text);
            komut.Parameters.Add("@p7", MskTelefon3.Text);
            komut.Parameters.Add("@p8", TxtMail.Text);
            komut.Parameters.Add("@p9", MskFax.Text);
            komut.Parameters.Add("@p10", CmbIl.Text);
            komut.Parameters.Add("@p11", CmbIlce.Text);
            komut.Parameters.Add("@p12", TxtVergiDairesi.Text);
            komut.Parameters.Add("@p13", RchAdres.Text);
            komut.Parameters.Add("@p14", TxtYtc.Text);
           
            komut.Parameters.Add("@p15", TxtSektor.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma sisteme eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmalistesi();
            temizle();

        }

        private void CmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbIlce.Properties.Items.Clear();
            OracleCommand komut = new OracleCommand("select ILCE_ISIM from TBL_ILCELER where IL_NO=:p1", bgl.baglanti());
            komut.Parameters.Add("@p1", CmbIl.SelectedIndex + 1);
            OracleDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbIlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            OracleCommand komutsil = new OracleCommand("delete from TBL_FIRMALAR where ID=:p1", bgl.baglanti());
            komutsil.Parameters.Add("@p1", TxtId.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma sistemden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmalistesi();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            //Verileri güncelleme
            OracleCommand komut = new OracleCommand("update TBL_FIRMALAR set AD=:P1, YETKILISTATU=:P2, YETKILIADSOYAD=:P3, TELEFON1=:P4, TELEFON2=:P5, TELEFON3=:P6, MAIL=:P7, FAX=:P8, IL=:P9, ILCE=:P10, VERGIDAIRE=:P11, ADRES=:P12, YETKILITC=:P13, SEKTOR=:P14 WHERE ID=:P15 ", bgl.baglanti());
            komut.Parameters.Add("@p1", TxtAd.Text);
            komut.Parameters.Add("@p2", TxtYGorev.Text);
            komut.Parameters.Add("@p3", TxtYetkili.Text);
            komut.Parameters.Add("@p4", MskTelefon1.Text);
            komut.Parameters.Add("@p5", MskTelefon2.Text);
            komut.Parameters.Add("@p6", MskTelefon3.Text);
            komut.Parameters.Add("@p7", TxtMail.Text);
            komut.Parameters.Add("@p8", MskFax.Text);
            komut.Parameters.Add("@p9", CmbIl.Text);
            komut.Parameters.Add("@p10", CmbIlce.Text);
            komut.Parameters.Add("@p11", TxtVergiDairesi.Text);
            komut.Parameters.Add("@p12", RchAdres.Text);
            komut.Parameters.Add("@p13", TxtYtc.Text);

            komut.Parameters.Add("@p14", TxtSektor.Text);
            komut.Parameters.Add("@p15", TxtId.Text);

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma bilgileri güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmalistesi();
            temizle();
        }
    }
}
