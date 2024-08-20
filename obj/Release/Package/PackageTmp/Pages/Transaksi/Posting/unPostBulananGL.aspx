<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="unPostBulananGL.aspx.cs" Inherits="eFinance.Pages.Transaksi.Posting.unPostBulananGL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
             <div class="col-sm-10">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Periode :   </label>
                            <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Width="250" AutoPostBack="false" ></asp:DropDownList>
                            <asp:DropDownList ID="cboMonth" runat="server" Width="120">
                            <asp:ListItem Value="1">Januari</asp:ListItem>
                            <asp:ListItem Value="2">Februari</asp:ListItem>
                            <asp:ListItem Value="3">Maret</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">Mei</asp:ListItem>
                            <asp:ListItem Value="6">Juni</asp:ListItem>
                            <asp:ListItem Value="7">Juli</asp:ListItem>
                            <asp:ListItem Value="8">Agustus</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">Oktober</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">Desember</asp:ListItem>
                        </asp:DropDownList>
                       <asp:DropDownList ID="cboYear" runat="server"  Enabled="true" Width="100">
                       </asp:DropDownList>

                         <asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Post" OnClick="btnPosting_Click"></asp:Button>
                     </div>
                </div>
            </div>
        </div>
   </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

