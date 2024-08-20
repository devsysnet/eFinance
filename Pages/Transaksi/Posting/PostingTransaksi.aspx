<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="PostingTransaksi.aspx.cs" Inherits="eFinance.Pages.Transaksi.Posting.PostingTransaksi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
             <div class="col-sm-12">
                <div class="form-inline">
                    <div class="col-sm-12">
                       <label>Periode :   </label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Width="250" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged"></asp:DropDownList>
                       <asp:DropDownList ID="cbojenis" runat="server" Width="250">
                       </asp:DropDownList>
                       <asp:DropDownList ID="cbothnajaran" runat="server" CssClass="form-control" Width="150"></asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Post" OnClick="btnPosting_Click"></asp:Button>
                     </div>
                </div>
            </div>
        </div>
   </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
