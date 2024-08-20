<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MSupplier.aspx.cs" Inherits="eFinance.Pages.Master.Input.MSupplier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama Supplier <span class="mandatory">*</span></label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="namaSupplier"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Supplier</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cbosupp" class="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Alamat</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="Alamat" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kota</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cboKota" class="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">NPWP </label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="npwp"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Alamat NPWP</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="AlamatNPWP" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">No.Telp</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="telpKantor"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Fax</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="Fax"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Email</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="email"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama PIC</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="namaPIC"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Telp PIC</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="telpPIC"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">No Account</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="noaccount"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Bank</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="Bank"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterangan 1</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="keterangan" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterangan 2</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="keterangan1" TextMode="MultiLine" Width="500"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterangan 3</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="keterangan2" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                                <div class="col-md-12">
                                    <div class="text-center">
                                        <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"/>
                                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click" />
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
