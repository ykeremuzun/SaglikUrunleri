<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UyeKayit.aspx.cs" Inherits="webSaglikProjesi.UyeKayit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 220px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var baslik = this.document.getElementById("ortaBaslik");
        baslik.innerText = "Yeni Üye Kayıt İşlemleri";
    </script>
    <div style="float:left; width: 351px;">
    <table style="text-align:left; width: 351px;">
        <tr>
            <td colspan="2"><asp:Label ID="lblMesaj" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
            <tr>
                <td style="width:120px">
                    <asp:Label ID="Label1" runat="server" Text="Kullanıcı Adı(email)"></asp:Label></td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtEmail" runat="server" Width="199px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="kullanıcı adı boş geçilemez." ForeColor="Red" >*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email uygun formatta değil." ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Şifre"></asp:Label></td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtSifre" TextMode="Password" runat="server" Width="199px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSifre" runat="server" ControlToValidate="txtSifre" ErrorMessage="şifre boş geçilemez." ForeColor="Red" >*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Şifre Tekrar"></asp:Label></td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtSifreTekrar" TextMode="Password" runat="server" Width="199px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSifreTekrar" runat="server" ControlToValidate="txtSifreTekrar" ErrorMessage="şifre boş geçilemez." ForeColor="Red" >*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvSifreTekrar" runat="server" ControlToCompare="txtSifre" ControlToValidate="txtSifreTekrar" ErrorMessage="şifreler aynı değil." ForeColor="Red">*</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Adı"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtAdi" runat="server" Width="199px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAdi" runat="server" ControlToValidate="txtAdi" ErrorMessage="isim boş geçilemez." ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Soyadı"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtSoyadi" runat="server" Width="199px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSoyadi" runat="server" ControlToValidate="txtSoyadi" ErrorMessage="soyisim boş geçilemez." ForeColor="Red" >*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="TCKimlik No"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTCKNo" runat="server" TextMode="Number" MaxLength="11" Width="199px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTCKNo" runat="server" ControlToValidate="txtTCKNo" ErrorMessage="TCKimlik No boş geçilemez." ForeColor="Red" >*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Telefon"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTelefon" runat="server" Width="199px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:120px">
                    <asp:Label ID="Label4" runat="server" Text="Teslim Adresi"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtAdres" runat="server" TextMode="MultiLine" Width="199px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="İlçe"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtIlce" runat="server" Width="199px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="İl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtIl" runat="server" Width="199px"></asp:TextBox>
                </td>
            </tr>
        <tr>
            <td colspan="2" style="text-align:center">
                <asp:CheckBox ID="cbxOkudum" runat="server" Text="Gizlilik Sözleşmesini Okudum" /></td>
        </tr>
            <tr>
                <td></td>
                <td style="text-align:center" class="auto-style1">
                    <asp:Button ID="btnKayitOl" CssClass="bluebutton" runat="server" Text="Kayıt Ol" Width="85px" OnClick="btnKayitOl_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDevam" CssClass="bluebutton" runat="server" Text="Devam" Width="85px" OnClick="btnDevam_Click" Visible="False" />
                </td>
            </tr>
        </table>
    </div>
    <div style="float:left; width:143px; height:60px">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        <br /><br /><br />
        <asp:TextBox ID="txtSozlesme" TextMode="MultiLine" runat="server" Height="129px" Width="148px" Text="Gizlilik Sözleşmesi : Girmiş olduğunuz kişisel bilgileriniz, 3.şahıs ve kurumlarla kesinlikle paylaşılmayacaktır. Her türlü özel bilgi ve ödeme işlemleriniz 128 bit SSL güvenşlik sertifikalarıyla şifrelenmektedir." BackColor="White" Font-Names="Verdana" Font-Size="8pt"></asp:TextBox>
    </div>
</asp:Content>
