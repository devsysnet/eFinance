<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="UploadVAMandiriYayasan.aspx.cs" Inherits="eFinance.Pages.Transaksi.Upload.UploadVAMandiriYayasan" %>
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
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                     <div class="form-group">
                                            <label class="col-sm-3 control-label">Unit<span class="mandatory">*</span></label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cboUnit" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="showhidefile">
                                            <label class="col-sm-3 control-label">Pilih File <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:FileUpload ID="flUpload" Class="fileupload" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="divtombol">
                                            <label class="col-sm-3 control-label"></label>
                                            <div class="col-sm-9">
                                                <asp:Button runat="server" ID="btnImport" CssClass="btn btn-success" Text="Upload" OnClick="btnImport_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                                <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click"></asp:Button>
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
            <asp:PostBackTrigger ControlID="btnImport" />
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
