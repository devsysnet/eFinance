<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="mGudang.aspx.cs" Inherits="eFinance.Pages.Master.Input.mGudang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kode Gudang :</label>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" class="form-control" ID="txtkdGudang" placeholder="Enter Warehouse Code"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Gudang :</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" class="form-control" ID="txtNamaGudang" placeholder="Enter Warehouse Name"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Jenis Gudang :</label>
                                <div class="col-sm-2">
                                    <asp:DropDownList runat="server" ID="cboJenisGudang">
                                        <asp:ListItem Value="Stok">Stok</asp:ListItem>
                                        <asp:ListItem Value="Penjualan">Penjualan</asp:ListItem>
                                        <asp:ListItem Value="Rusak">Rusak</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Keterangan</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" class="form-control" ID="txtKet" TextMode="MultiLine" placeholder="Enter Description"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
