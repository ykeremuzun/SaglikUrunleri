using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webSaglikProjesi
{
    public partial class Adres : System.Web.UI.Page
    {
        DataModel.SaglikUrunleriEntities ent = new DataModel.SaglikUrunleriEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGiris_Click(object sender, EventArgs e)
        {
            var kullanici = (from uye in ent.Kullanicilar
                             where uye.kullaniciad == txtEmail.Text && uye.sifre == txtSifre.Text && uye.silindi == false
                             select uye).FirstOrDefault();

            if(kullanici == null)
            {
                lblMesaj.Text = "Hatalı kullanıcı yada şifre girişi!";
                txtEmail.Focus();
            }
            else
            {
                Session["uye"] = kullanici.id;
                pnlAdres.Visible = true;
                txtAdres.Text = kullanici.adres;
                txtIlce.Text = kullanici.ilce;
                txtIl.Text = kullanici.il;
                txtTelefon.Text = kullanici.telefon;
                lblMesaj.Text = "";
                txtAdres.Focus();
            }
        }
        protected void btnAdresOnay_Click(object sender, EventArgs e)
        {
            if(txtAdres.Text.Trim() != "")
            {
                int degisenID = Convert.ToInt32(Session["uye"]);
                var degisen = (from user in ent.Kullanicilar
                               where user.id == degisenID
                               select user).FirstOrDefault();
                degisen.adres = txtAdres.Text;
                degisen.ilce = txtIlce.Text;
                degisen.il = txtIl.Text;
                degisen.telefon = txtTelefon.Text;
                try
                {
                    ent.SaveChanges();
                    lblMesaj.Text = "Adres bilgileriniz güncellendi.";
                    DataModel.Satislar satis = new DataModel.Satislar();
                    satis.kullanicino = Convert.ToInt32(Session["uye"]);
                    satis.tarih = DateTime.Now;
                    satis.teslimtarihi = DateTime.Now.AddDays(3);
                    satis.tutar = ToplamTutarBul();
                    satis.miktar = ToplamAdetBul();
                    satis.durumu = (byte)Models.cEnum.SatisDurumu.siparis;
                    ent.Satislar.Add(satis);
                    ent.SaveChanges();
                    //Satış detayları satisno ya göre sepetten veritabanına aktarılacak.
                    DataModel.SatisDetaylari detay = new DataModel.SatisDetaylari();
                    //detay.satisno = satis.satisno;
                    int SonSatisNo = ent.Satislar.Where(s => s.kullanicino == satis.kullanicino).ToList().Last().satisno;
                    DataTable dt = (DataTable)Session["sepet"];
                    foreach (DataRow urun in dt.Rows)
                    {
                        detay.satisno = SonSatisNo;
                        detay.urunid = Convert.ToInt32(urun["urunID"]);
                        detay.adet = Convert.ToInt32(urun["adet"]);
                        detay.birimfiyat = Convert.ToDecimal(urun["fiyat"]);
                        detay.tutar = Convert.ToDecimal(urun["tutar"]);
                        ent.SatisDetaylari.Add(detay);
                        ent.SaveChanges();
                    }
                    Response.Redirect("Odeme.aspx");
                }
                catch (Exception ex)
                {
                    string hata = ex.Message;
                }
            }
        }
        private int ToplamAdetBul()
        {
            int ToplamAdet = 0;
            DataTable dt = (DataTable)Session["sepet"];
            foreach (DataRow dr in dt.Rows)
            {
                ToplamAdet += Convert.ToInt32(dr["adet"]);
            }
            return ToplamAdet;
        }
        private decimal ToplamTutarBul()
        {
            decimal ToplamTutar = 0;
            DataTable dt = (DataTable)Session["sepet"];
            foreach (DataRow dr in dt.Rows)
            {
                ToplamTutar += Convert.ToDecimal(dr["tutar"]);
            }
            return ToplamTutar;
        }
        protected void cbxUnuttum_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxUnuttum.Checked)
            {
                if (txtEmail.Text.Trim() != "")
                {
                    DataModel.Kullanicilar user = EmailKontrol(txtEmail.Text);
                    if (user != null)
                    {
                        SmtpClient sc = new SmtpClient();
                        sc.Port = 587;
                        sc.Host = "smtp.gmail.com"; //mail.domain.com kullanılır.
                        sc.EnableSsl = true;
                        sc.Credentials = new NetworkCredential("wissendeneme@gmail.com", "wissen123");
                        //smtpclient ile gönderilecek MailMessage türünden bir mail tanımlamalıyız.
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("wissendeneme@gmail.com", "Wissen Yaz-08", System.Text.Encoding.UTF8);
                        mail.To.Add(txtEmail.Text);
                        //mail.CC.Add(txtEmail2.Text);
                        mail.Subject = "Sağlık Ürünleri e-Ticaret Sitesi";
                        //mail.Body = "Sn." + user.ad + "\n" + "Şifreniz : " + user.sifre;
                        StringBuilder sbMesaj = new StringBuilder();
                        sbMesaj.Append("Sayın " + user.ad + " " + user.soyad + ",<br/>" + "Şifreniz : " + user.sifre + "<br/>");
                        sbMesaj.Append("<a href=\"" + Request.Url.Host + "/Adres.aspx\" >Alışverişe devam etmek için tıklayınız...</a>");
                        mail.Body = sbMesaj.ToString();
                        mail.IsBodyHtml = true;
                        try
                        {
                            sc.Send(mail);
                            lblMesaj.Text = "Şifreniz mail adresinize gönderilmiştir.";
                        }
                        catch (Exception ex)
                        {
                            string hata = ex.Message;
                        }
                    }
                    else
                    {
                        lblMesaj.Text = "Email adresi kayıtlı değil!";
                        txtEmail.Focus();
                    }
                }
            }
        }
        private DataModel.Kullanicilar EmailKontrol(string email)
        {
            var kullanici = (from k in ent.Kullanicilar
                             where k.kullaniciad == email && k.silindi == false
                             select k).FirstOrDefault();
            return kullanici;
        }
    }
}