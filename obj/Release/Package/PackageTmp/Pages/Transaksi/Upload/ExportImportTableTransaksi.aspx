<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="ExportImportTableTransaksi.aspx.cs" Inherits="eFinance.Pages.Transaksi.Upload.ExportImportTableTransaksi" %>

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
                                <div class="form-horizontal">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Aksi <span class="mandatory">*</span></label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="cboAksi" runat="server" CssClass="form-control" Width="150" AutoPostBack="true" OnTextChanged="cboAksi_TextChanged">
                                                    <asp:ListItem Value="">--Pilih Aksi--</asp:ListItem>
                                                    <asp:ListItem Value="Export">Export</asp:ListItem>
                                                    <asp:ListItem Value="Import">Import</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="showExport">
                                            <label class="col-sm-3 control-label">Periode <span class="mandatory">*</span></label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="cboMonth" runat="server" Enabled="false" Width="110"></asp:DropDownList>
                                                &nbsp;&nbsp;
                                                <asp:DropDownList ID="cboYear" runat="server" Enabled="false" Width="88"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Tabel <span class="mandatory">*</span></label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="cboTabel" runat="server" Width="250"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="showImport">
                                            <label class="col-sm-3 control-label">Pilih File <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:FileUpload ID="flUpload" Class="fileupload" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label"></label>
                                            <div class="col-sm-9">
                                                <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Export" Enabled="false" OnClick="btnExport_Click"></asp:Button>
                                                <asp:Button runat="server" ID="btnImport" CssClass="btn btn-success" Text="Import" Enabled="false" OnClick="btnImport_Click"></asp:Button>
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:PostBackTrigger ControlID="btnImport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
