<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Postpaymentgatewayall.aspx.cs" Inherits="eFinance.Pages.Transaksi.Posting.Postpaymentgatewayall" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
      
        <div class="panel">
            <div class="panel-body">
                <div>
                    <div class="row mt-5">
                        <div class="col-sm-5">
                            <div class="form-inline">
                                <div class="col-sm-12">
                                    <%--3. Posting: Update data Jurnal (tKas, tKasDetil, TransPiutang) berdarkan payment_transaction yang sudah paid.--%>
                                    <asp:Button runat="server" ID="btnPostingJurnal" CssClass="btn btn-primary" Text="Posting Jurnal" OnClick="btnPosting_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>


