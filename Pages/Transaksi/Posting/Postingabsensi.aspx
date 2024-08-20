<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Postingabsensi.aspx.cs" Inherits="eFinance.Pages.Transaksi.Posting.Postingabsensi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-5">
                                <div class="form-inline">
                                    <div class="col-sm-12">
                                        <asp:HiddenField ID="hdnabsendtg" runat="server" />
                                        <asp:HiddenField ID="hdnabsenptg" runat="server" />
                                        <label>Periode :   </label>
                                        <asp:DropDownList ID="cboMonth" runat="server" Enabled="false" Width="110">
                                        </asp:DropDownList>&nbsp;&nbsp;
                                        <asp:DropDownList ID="cboYear" runat="server" Enabled="false" Width="88">
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
    <%--</div>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>


