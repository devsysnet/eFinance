<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstAsset.aspx.cs" Inherits="eFinance.Pages.Master.Input.MstAsset" %>
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
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Bangunan :</label>
                                <div class="form-inline">
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="cboBangunan" runat="server">
                                            <asp:ListItem Value="1">Bangunan</asp:ListItem>
                                            <asp:ListItem Value="2">Non Bangunan</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kelompok :</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" class="form-control" ID="txtKelompok" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Masa :</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" class="form-control" ID="txtMasa"></asp:TextBox>
                                </div>
                                Tahun
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Jenis Tarif :</label>
                                <div class="form-inline">
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="cboJenis" runat="server">
                                            <asp:ListItem Value="1">Garis Lurus</asp:ListItem>
                                            <asp:ListItem Value="2">Saldo Menurun</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tarif :</label>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" class="form-control" ID="txtTarif"></asp:TextBox>
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
