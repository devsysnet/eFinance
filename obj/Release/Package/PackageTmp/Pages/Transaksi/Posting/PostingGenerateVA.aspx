<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="PostingGenerateVA.aspx.cs" Inherits="eFinance.Pages.Transaksi.Posting.PostingGenerateVA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="panel">
            <div class="panel-body">
                <div>
                    <div class="row">
                        <div class="col-sm-5">
                            <div class="form-inline">
                                <div>
                                    API URL: 
                                    <asp:Label runat="server" ID="lblApiUrl"></asp:Label>
                                </div>
                                <div class="col-sm-12">
                                    <%--1. Generate VA: Mengubah status null menjadi unpaid di payment transaction.--%>
                                    <asp:Button runat="server" ID="btnCreateVA" CssClass="btn btn-primary" Text="Generate VA" PostBackUrl="https://kanisius-api.efinac.co.id/api/PaymentTransaction/CreateVA/All" Target="_blank"></asp:Button>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel">
            <div class="panel-body">
                <div>
                    <div class="row mt-5">
                        <div class="col-sm-5">
                            <div class="form-inline">
                                <div class="col-sm-12">
                                    <%--2. Update: Apabila sudah dibayar, status unpaid menjadi paid di payment_transaction.--%>
                                    <asp:Button runat="server" ID="btnSync" CssClass="btn btn-primary" Text="Update Payment Transaction dari API Bank" PostBackUrl="https://api.efinac.co.id/api​/PaymentTransaction​/Sync" Target="_blank"></asp:Button>
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                 
            </div>
        </div>
        <div class="panel">
            <div class="panel-body">
                <div>
                    <div class="row mt-5">
                        <div class="col-sm-5">
                            <div class="form-inline">
                                <div class="col-sm-12">
                                    <%--3. Posting: Update data Jurnal (tKas, tKasDetil, TransPiutang) berdarkan payment_transaction yang sudah paid.--%>
                                    <%--<asp:Button runat="server" ID="btnPostingJurnal" CssClass="btn btn-primary" Text="Posting Jurnal" OnClick="btnPosting_Click"></asp:Button>--%>
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
