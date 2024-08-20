<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="PostingbulananGL.aspx.cs" Inherits="eFinance.Pages.Transaksi.Posting.PostingbulananGL" %>
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
                       <asp:DropDownList ID="cbobln" runat="server" Enabled="false"  Width="110"></asp:DropDownList>
                       <asp:DropDownList ID="cboYear" runat="server" Enabled="false" Width="100">
                       </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Post" OnClick="btnPosting_Click"></asp:Button>
                     </div>
                </div>
            </div>
        </div>
   </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

