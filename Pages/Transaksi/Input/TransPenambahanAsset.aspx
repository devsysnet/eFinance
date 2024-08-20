<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransPenambahanAsset.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransPenambahanAsset" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Lokasi Barang :</label>
                                <div class="col-sm-3">
                                        <asp:DropDownList ID="cboLokasi" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboLokasi_SelectedIndexChanged">
                                        </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Sub Lokasi :</label>
                                <div class="col-sm-3">
                                        <asp:DropDownList ID="cboSubLokasi" runat="server">
                                        </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Barang :</label>
                                <div class="form-inline">
                                    <div class="col-sm-5">
                                    <asp:TextBox runat="server" class="form-control" ID="txtNamaBarang" ></asp:TextBox>
                                    </div>
                                        <asp:Button runat="server" ID="Button1" CssClass="btn btn-primary" Text="Search" OnClick="Button1_Click"></asp:Button>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Asset :</label>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" class="form-control" ID="txtNamaAsset" Enabled="false"></asp:TextBox>
                                    <asp:HiddenField ID="hdnNoAsset" runat="server" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Lokasi :</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" class="form-control" Enabled="false" ID="txtLokasi"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tanggal Asset:</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" class="form-control date" ID="dtTanggal"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nilai Perolehan :</label>
                                <div class="form-inline">
                                    <div class="col-sm-5">
                                    <asp:TextBox runat="server" class="form-control" ID="txtNilai"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Uraian :</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" class="form-control" ID="txtUraian" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-md-12">
                                    <div class="text-center">
                                        <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click"></asp:Button>
                                        <asp:Button runat="server" ID="btnResetRek" CssClass="btn btn-danger" Text="Reset" OnClick="btnResetRek_Click"></asp:Button>
                                    </div>
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
