using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webSaglikProjesi.Models;

namespace webSaglikProjesi
{
    public partial class Default : System.Web.UI.Page
    {
        DataModel.SaglikUrunleriEntities ent = new DataModel.SaglikUrunleriEntities();
        cSepet spt = new cSepet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                UrunleriGetir();
        }
        private void UrunleriGetir()
        {
            var Uruns = (from urun in ent.Urunler
                         where urun.silindi == false
                         select urun).ToList();
            dlstUrunler.DataSource = Uruns;
            dlstUrunler.DataBind();

            if(Session["sepet"] != null)
            {
                DataTable dt = (DataTable)Session["sepet"];
                GridView gvOzet = (GridView)this.Master.FindControl("gvSepetOzet");
                gvOzet.Columns[0].FooterText = "Toplam : ";
                gvOzet.Columns[1].FooterText = string.Format("{0:C}", ToplamTutarBul());
                gvOzet.DataSource = dt;
                gvOzet.DataBind();
            } 
        }
        protected void dlstUrunler_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if(e.CommandName == "detay")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("Details.aspx?urunid=" + id);
            }
            else if(e.CommandName == "sepet")
            {
                if(Session["sepet"] == null)
                {
                    Session["sepet"] = spt.YeniSepet();
                }
                DataTable dt = (DataTable)Session["sepet"];
                DataRow dr;
                int ID = Convert.ToInt32(e.CommandArgument);
                Label UrunAdi = (Label)e.Item.FindControl("lblUrunAdi");
                Label Fiyat = (Label)e.Item.FindControl("lblFiyat");
                string[] Degerler = Fiyat.Text.Split(' '); //{0:C} formatından dolayı gelen sembolü kaldırmak için...
                Fiyat.Text = Degerler[0];
                TextBox Adet = (TextBox)e.Item.FindControl("txtAdet");
                bool Varmi = false;
                //Sepeti (DataTable) kontrol ediyoruz. Eklenmek istenen ürün önceden seçilmiş mi?
                foreach (DataRow urun in dt.Rows)
                {
                    if(Convert.ToInt32(urun["urunID"]) == ID)
                    {
                        Varmi = true;
                        urun["adet"] = Convert.ToInt32(urun["adet"]) + Convert.ToInt32(Adet.Text);
                        urun["tutar"] = Convert.ToDecimal(urun["tutar"]) + (Convert.ToInt32(Adet.Text) * Convert.ToDecimal(Fiyat.Text));
                        break;
                    }
                }
                if (Varmi == false)
                {
                    dr = dt.NewRow();
                    dr["urunID"] = ID;
                    dr["urunAd"] = UrunAdi.Text;
                    dr["adet"] = Convert.ToInt32(Adet.Text);
                    dr["fiyat"] = Convert.ToDecimal(Fiyat.Text);
                    dr["tutar"] = Convert.ToInt32(Adet.Text) * Convert.ToDecimal(Fiyat.Text);
                    dt.Rows.Add(dr);
                }               
                Session["sepet"] = dt;
                Response.Redirect("Sepet.aspx");
                //Response.Redirect("Default.aspx");
            }
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
    }
}