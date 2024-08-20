<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="PostingTahunanGL.aspx.cs" Inherits="eFinance.Pages.Transaksi.Posting.PostingTahunanGL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
             <div class="col-sm-12">
                <div class="form-inline">
                    <div class="col-sm-10">
                        <label>Periode :   </label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Width="250" AutoPostBack="False" ></asp:DropDownList>
                         <asp:DropDownList ID="cboYear" runat="server" Width="100">
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

