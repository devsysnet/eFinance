<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransNomorPajak.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransNomorPajak" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-4 col-form-label">Tgl Input :</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox runat="server" class="form-control date" ID="dtInput"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-4 col-form-label">No Surat :</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" class="form-control" ID="txtNoSurat" type="text" placeholder="Enter NoSurat"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-4 col-form-label">Tgl Surat :</label>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox runat="server" class="form-control date" ID="dtSurat"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-4 col-form-label">Book Pajak :</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox runat="server" class="form-control" ID="txtBookPajakFrom1" Width="120"></asp:TextBox>
                                                            <span>.</span>
                                                            <asp:TextBox runat="server" class="form-control" ID="txtBookPajakFrom2" Width="120"></asp:TextBox>
                                                            <span>.</span>
                                                            <asp:TextBox runat="server" class="form-control" ID="txtBookPajakFrom3" Width="250"></asp:TextBox>
                                                            <span>To</span>
                                                            <asp:TextBox runat="server" class="form-control" ID="txtBookPajakTo1" Width="250"></asp:TextBox>
                                                            <%-- <span>.</span>
                                                            <asp:TextBox runat="server" class="form-control" ID="txtBookPajakTo2"></asp:TextBox>
                                                            <span>.</span>
                                                            <asp:TextBox runat="server" class="form-control" ID="txtBookPajakTo3"></asp:TextBox>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-sm-12">
                                                    <div class="text-center">
                                                        <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click"></asp:Button>
                                                        <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
