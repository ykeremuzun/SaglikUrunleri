using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webSaglikProjesi
{
    public partial class UyeKayit : System.Web.UI.Page
    {
        DataModel.SaglikUrunleriEntities ent = new DataModel.SaglikUrunleriEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnKayitOl_Click(object sender, EventArgs e)
        {
            if(cbxOkudum.Checked == false)
            {
                lblMesaj.Text = "Gizlilik sözleşmesini okudum işaretlemelisiniz!";
            }
            else
            {
                if(EmailKontrol(txtEmail.Text))
                {
                    lblMesaj.Text = "Aynı mail adresi zaten kayıtlı!";
                    txtEmail.Focus();
                }
                else
                {
                    DataModel.Kullanicilar k = new DataModel.Kullanicilar();
                    k.kullaniciad = txtEmail.Text;
                    k.sifre = txtSifre.Text; //md5 şifrelenerek veritabanına atılabilir.
                    k.ad = txtAdi.Text;
                    k.soyad = txtSoyadi.Text;
                    k.tckno = txtTCKNo.Text;
                    k.telefon = txtTelefon.Text;
                    k.adres = txtAdres.Text;
                    k.ilce = txtIlce.Text;
                    k.il = txtIl.Text;
                    ent.Kullanicilar.Add(k);
                    try
                    {
                        ent.SaveChanges();
                        lblMesaj.Text = "Üye kayıt işleminiz gerçekleşmiştir.";
                        btnKayitOl.Visible = false;
                        btnDevam.Visible = true;
                        //Response.Redirect("Adres.aspx");
                    }
                    catch (Exception ex)
                    {
                        string hata = ex.Message;
                    }
                }
            }
        }
        private bool EmailKontrol(string email)
        {
            var kullanici = (from k in ent.Kullanicilar
                             where k.kullaniciad == email && k.silindi == false
                             select k).FirstOrDefault();
            if (kullanici != null) return true;
            return false;
        }
        protected void btnDevam_Click(object sender, EventArgs e)
        {
            Response.Redirect("Adres.aspx");
        }
    }
}