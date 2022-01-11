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
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        OracleBaglantisi bgl = new OracleBaglantisi();

        void listele()
        {
            OracleDataAdapter da = new OracleDataAdapter("select* from TBL_FATURABILGI", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            TxtAlici.Text = "";
            TxtId.Text = "";
            TxtSeri.Text = "";
            TxtSiraNo.Text = "";
            TxtTalan.Text = "";
            TxtTEden.Text = "";
            TxtVergi.Text = "";
            TxtTarih.Text = "";
        }

        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void BtnKaydet_Click_1(object sender, EventArgs e)
        {
            if (TxtFaturaId.Text == "")
            {
                OracleCommand komut = new OracleCommand("insert into TBL_FATURABILGI(FATURABILGIID,SERI,SIRANO,TARIH,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) values (:p1,:p2,:p3,:p4,:p5,:p6,:p7,:p8)", bgl.baglanti());
                komut.Parameters.Add("@p1", TxtId.Text);
                komut.Parameters.Add("@p2", TxtSeri.Text);
                komut.Parameters.Add("@p3", TxtSiraNo.Text);
                komut.Parameters.Add("@p4", TxtTarih.Text);
                komut.Parameters.Add("@p5", TxtVergi.Text);
                komut.Parameters.Add("@p6", TxtAlici.Text);
                komut.Parameters.Add("@p7", TxtTEden.Text);
                komut.Parameters.Add("@p8", TxtTalan.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura sisteme kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();

            }
            if (TxtFaturaId.Text!="")
            {
                OracleCommand komut2 = new OracleCommand("insert into TBL_FATURADETAY(FATURAURUNID,URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values (:p1,:p2,:p3,:p4,:p5,:p6)", bgl.baglanti());
                komut2.Parameters.Add("@p1", TxtFaturaId.Text);
                komut2.Parameters.Add("@p2", TxtUrunAd.Text);
                komut2.Parameters.Add("@p3", TxtMiktar.Text);
                komut2.Parameters.Add("@p4", TxtFiyat.Text);
                komut2.Parameters.Add("@p5", TxtTutar.Text);
                komut2.Parameters.Add("@p6", TxtFaturaId.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura sisteme kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
        }

        private void gridView1_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtId.Text = dr["FATURABILGIID"].ToString();
                TxtSiraNo.Text = dr["SIRANO"].ToString();
                TxtSeri.Text = dr["SERI"].ToString();
                TxtTarih.Text = dr["TARIH"].ToString();
                TxtAlici.Text = dr["ALICI"].ToString();
                TxtTEden.Text = dr["TESLIMEDEN"].ToString();
                TxtTalan .Text = dr["TESLIMALAN"].ToString();
                TxtVergi.Text = dr["VERGIDAIRE"].ToString();



            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            OracleCommand komut = new OracleCommand("delete from TBL_FATURABILGI where FATURABILGIID=:p1", bgl.baglanti());
            komut.Parameters.Add(":p1", TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            OracleCommand komut = new OracleCommand("update TBL_FATURABILGI set   SERI=:P1, SIRANO=:P2, TARIH=:P3, VERGIDAIRE=:P4, ALICI=:P5, TESLIMEDEN=:P6,TESLIMALAN=:P7 where FATURABILGIID=:P8", bgl.baglanti());

            komut.Parameters.Add("@P1", TxtSeri.Text);
            komut.Parameters.Add("@P2", TxtSiraNo.Text);
            komut.Parameters.Add("@P3", TxtTarih.Text);
            komut.Parameters.Add("@P4", TxtVergi.Text);
            komut.Parameters.Add("@P5", TxtAlici.Text); ;
            komut.Parameters.Add("@P6", TxtTEden.Text);
            komut.Parameters.Add("@P7", TxtTalan.Text);
   
            komut.Parameters.Add("@p8", TxtId.Text);

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunler fr = new FrmFaturaUrunler();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                fr.id = dr["FATURABILGIID"].ToString();
            }
            fr.Show();
        }
        int a;

        private void TxtSiraNo_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}

