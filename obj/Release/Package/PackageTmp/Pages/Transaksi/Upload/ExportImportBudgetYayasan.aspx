<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="ExportImportBudgetYayasan.aspx.cs" Inherits="eFinance.Pages.Transaksi.Upload.ExportImportBudgetYayasan" %>
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
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cboAksi" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboAksi_SelectedIndexChanged">
                                                    <asp:ListItem Value="">---Pilih Aksi---</asp:ListItem>
                                                    <asp:ListItem Value="Import">Upload</asp:ListItem>
                                                    <asp:ListItem Value="Export">Download</asp:ListItem>
                                                    <asp:ListItem Value="Update">Update</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="showhidestatus">
                                            <label class="col-sm-3 control-label">Status <span class="mandatory">*</span></label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cboStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboStatus_SelectedIndexChanged">
                                                    <asp:ListItem Value="">---Pilih Status Budget---</asp:ListItem>
                                                    <asp:ListItem Value="New">New</asp:ListItem>
                                                    <asp:ListItem Value="Revisi">Revisi</asp:ListItem>
                                                    <asp:ListItem Value="Update">Update</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                         <div class="form-group" runat="server" id="showhidejenis">
                                            <label class="col-sm-3 control-label">Jenis <span class="mandatory">*</span></label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cbojenis" runat="server" CssClass="form-control" AutoPostBack="true">
                                                    <asp:ListItem Value="">---Pilih Jenis---</asp:ListItem>
                                                    <asp:ListItem Value="yayasan">Yayasan</asp:ListItem>
                                                    <asp:ListItem Value="nonyayasan">Non Yayasan</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="showhidetahun">
                                            <label class="col-sm-3 control-label">Tahun <span class="mandatory">*</span></label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cboYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="showhidefile">
                                            <label class="col-sm-3 control-label">Pilih File <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:FileUpload ID="flUpload" Class="fileupload" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="thnUpdate">
                                            <label class="col-sm-3 control-label">Tahun <span class="mandatory">*</span></label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cbothnupdate" runat="server" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="divtombol">
                                            <label class="col-sm-3 control-label"></label>
                                            <div class="col-sm-9">
                                                <asp:Button runat="server" ID="btnimporttahunan" CssClass="btn btn-success" Visible="false" Text="Uploadd" OnClick="btnImporttahunan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                                <asp:Button runat="server" ID="btnImport" CssClass="btn btn-success" Visible="false" Text="Upload" OnClick="btnImport_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                                <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click"></asp:Button>
                                                <asp:Button runat="server" ID="btnUpdate" CssClass="btn btn-primary" Text="Download Update Anggaran" Visible="false" OnClick="btnUpdate_Click"></asp:Button>

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
            <asp:PostBackTrigger ControlID="btnimporttahunan" />
            <asp:PostBackTrigger ControlID="btnImport" />
            <asp:PostBackTrigger ControlID="btnUpdate" />
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>