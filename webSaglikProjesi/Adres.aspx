<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Adres.aspx.cs" Inherits="webSaglikProjesi.Adres" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var baslik = this.document.getElementById("ortaBaslik");
        baslik.innerText = "Adres Onay İşlemleri";
    </script>
    <div style="text-align:center">
        <img src="Content/style/images/adresonay2.jpg" /><br />
        Sitemizden alışveriş yapabilmeniz için öncelikle üye olmanız gerekmektedir. Eğer üye değilseniz <a href="UyeKayit.aspx">yeni üye kaydı için tıklayınız.</a><br /><br />
        <table style="text-align:left">
            <tr>
                <td style="width:120px">
                    <asp:Label ID="Label1" runat="server" Text="kullanıcı adı(email)"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" Width="199px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="kullanıcı adı boş geçilemez." ForeColor="Red" ValidationGroup="1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="şifre"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtSifre" TextMode="Password" runat="server" Width="199px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSifre" runat="server" ControlToValidate="txtSifre" ErrorMessage="şifre boş geçilemez." ForeColor="Red" ValidationGroup="1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td></td>
                <td style="text-align:center">
                    <asp:Button ID="btnGiris" CssClass="bluebutton" runat="server" Text="Giriş" Width="85px" OnClick="btnGiris_Click" ValidationGroup="1" />
                </td>
            </tr>
        </table>
        <asp:CheckBox ID="cbxUnuttum" runat="server" Text="şifremi unuttum" AutoPostBack="True" OnCheckedChanged="cbxUnuttum_CheckedChanged" /><br />
        <asp:Label ID="lblMesaj" runat="server" ForeColor="Red"></asp:Label>
        <asp:Panel ID="pnlAdres" runat="server" Visible="false">
        <table style="text-align:left">
            <tr>
                <td style="width:120px">
                    <asp:Label ID="Label3" runat="server" Text="Teslim Adresi"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtAdres" runat="server" TextMode="MultiLine" Width="199px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="İlçe"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtIlce" runat="server" Width="199px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="İl"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtIl" runat="server" Width="199px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Telefon"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTelefon" runat="server" Width="199px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td style="text-align:center">
                    <asp:Button ID="btnAdresOnay" runat="server" CssClass="bluebutton" Text="Adres Onay" Width="85px" OnClick="btnAdresOnay_Click" ValidationGroup="2" />
                </td>
            </tr>
        </table>
        </asp:Panel>
    </div>
</asp:Content>
