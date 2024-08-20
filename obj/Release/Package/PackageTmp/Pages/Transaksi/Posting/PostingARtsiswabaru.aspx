<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="PostingARtsiswabaru.aspx.cs" Inherits="eFinance.Pages.Transaksi.Posting.PostingARtsiswabaru" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-inline">
                                    <div class="col-sm-12">
                                        <label>Tanggal Tahun Ajaran Dari :   </label>
                                        <asp:DropDownList ID="cboMonth" runat="server" Enabled="false" Width="150">
                                        </asp:DropDownList>&nbsp;&nbsp;&nbsp; Sampai : &nbsp;&nbsp;
                                        <asp:DropDownList ID="cboYear" runat="server" Enabled="false" Width="150">
                                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Post" OnClick="btnPosting_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>




