<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="UploadARSiswaYayasan.aspx.cs" Inherits="eFinance.Pages.Transaksi.Upload.UploadARSiswaYayasan" %>
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
                                <div class="col-sm-8">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Unit <span class="mandatory">*</span></label>
                                            <div class="col-sm-5">
                                               <asp:DropDownList ID="cboUnit" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboUnit_SelectedIndexChanged"></asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tahun Ajaran <span class="mandatory">*</span></label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="cboTA" runat="server" CssClass="form-control" Width="150"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal Transaksi <span class="mandatory">*</span></label>
                                            <div class="col-sm-5">
                                                <asp:TextBox runat="server" CssClass="form-control date" ID="dtBayar"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Pembayaran <span class="mandatory">*</span></label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="cboKategori" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="form-inline">
                                                <label class="col-sm-3 control-label">File <span class="mandatory">*</span></label>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="cboflUpload" runat="server" CssClass="form-control" Width="120">
                                                        <asp:ListItem Value="csv/txt">CSV / TXT</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:FileUpload ID="flUpload" Class="fileupload" runat="server" />
                                                    <asp:Button runat="server" ID="btnUpload" CssClass="btn btn-primary" Text="Upload" OnClick="btnUpload_Click"></asp:Button>
                                                    <span>Download template upload tagihan</span>
                                                    <asp:Button runat="server" ID="btnDownload" CssClass="btn btn-success" Text="Download" OnClick="btnDownload_Click"></asp:Button>
                                                    
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="btnDownload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
