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
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }


        OracleBaglantisi bgl = new OracleBaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
             OracleDataAdapter da = new OracleDataAdapter("Select * From TBL_URUNLER", bgl.baglanti());
             da.Fill(dt);
             gridControl1.DataSource = dt;

           /* //PROSCEDURE
            OracleCommand cmd = new OracleCommand("PROCEDURE_URUN", bgl.baglanti());
            cmd.CommandType = CommandType.StoredProcedure;
            OracleDataAdapter dr = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            dr.Fill(dt);
            gridControl1.DataSource = dt;*/


        }




        void temizle()
        {
            TxtUrunAd.Text = "";
            TxtId.Text = "";
            TxtMarka.Text = "";
            TxtModel.Text = "";
            MaskYıl.Text = "";
            NudAdet.Text = "";
            TxtAlis.Text = "";
            TxtSatis.Text = " ";
            RchDetay.Text = "";


        }

        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
           
        }
       
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            //Verileri kaydetme
            OracleCommand komut = new OracleCommand("insert into TBL_URUNLER(ID,URUNAD,MARKA,MODEL,YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY) VALUES(:p1,:p2,:p3,:p4,:p5,:p6,:p7,:p8,:p9)", bgl.baglanti());
            komut.Parameters.Add("@p1", TxtId.Text);
            komut.Parameters.Add("@p2", TxtUrunAd.Text);
            komut.Parameters.Add("@p3", TxtMarka.Text);
            komut.Parameters.Add("@p4", TxtModel.Text);
            komut.Parameters.Add("@p5", MaskYıl.Text);
            komut.Parameters.Add("@p6", NudAdet.Value); 
            komut.Parameters.Add("@p7",TxtAlis.Text);
            komut.Parameters.Add("@p8",TxtSatis.Text);
            komut.Parameters.Add("@p9", RchDetay.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            
            MessageBox.Show("Ürün sisteme eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            // Verileri Silme
            OracleCommand komutsil = new OracleCommand("delete from TBL_URUNLER where ID=:p1", bgl.baglanti());
            komutsil.Parameters.Add("@p1", TxtId.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün sistemden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            TxtId.Text = dr["ID"].ToString();
            TxtUrunAd.Text = dr["URUNAD"].ToString();
            TxtMarka.Text = dr["MARKA"].ToString();
            TxtModel.Text = dr["MODEL"].ToString();
            MaskYıl.Text = dr["YIL"].ToString();
            NudAdet.Value = decimal.Parse(dr["ADET"].ToString());
            TxtAlis.Text = dr["ALISFIYAT"].ToString();
            TxtSatis.Text = dr["SATISFIYAT"].ToString();
            RchDetay.Text = dr["DETAY"].ToString();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            //Verileri güncelleme
            OracleCommand komut = new OracleCommand("update TBL_URUNLER set  URUNAD=:P1, MARKA=:P2, MODEL=:P3, YIL=:P4, ADET=:P5, ALISFIYAT=:P6, SATISFIYAT=:P7,DETAY=:P8 where ID=:P9", bgl.baglanti());
            
            komut.Parameters.Add("@p1", TxtUrunAd.Text);
            komut.Parameters.Add("@p2", TxtMarka.Text);
            komut.Parameters.Add("@p3", TxtModel.Text);
            komut.Parameters.Add("@p4", MaskYıl.Text);
            komut.Parameters.Add("@p5", NudAdet.Value); ;
            komut.Parameters.Add("@p6", TxtAlis.Text);
            komut.Parameters.Add("@p7", TxtSatis.Text);
            komut.Parameters.Add("@p8", RchDetay.Text);
            komut.Parameters.Add("@p9", TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
