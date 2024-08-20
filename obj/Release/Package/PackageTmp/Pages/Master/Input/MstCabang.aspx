<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstCabang.aspx.cs" Inherits="eFinance.Pages.Master.Input.MstCabang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kategori Usaha</label>
                                <div class="col-sm-5">
                                    <asp:DropDownList runat="server" class="form-control " ID="Kategori" width="180">
                                        <asp:ListItem Value=""> ---Pilih Kategori Usaha--- </asp:ListItem>
                                        <asp:ListItem Value="Sekolah"> Sekolah </asp:ListItem>
                                        <asp:ListItem Value="Yayasan"> Yayasan </asp:ListItem>
                                        <asp:ListItem Value="Properti"> Properti </asp:ListItem>
                                        <asp:ListItem Value="Trade"> Trade </asp:ListItem>
                                        <asp:ListItem Value="Jasa"> Jasa </asp:ListItem>
                                        </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama Cabang</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" class="form-control" ID="txtNama"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Alamat</label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" class="form-control " ID="txtAlamat" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kota</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" class="form-control " ID="txtKota"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kode Pos</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" class="form-control " ID="txtKodePos"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">No Telpon</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" class="form-control " ID="txtNoTelp"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">No Fax</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" class="form-control " ID="txtNoFax"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Email</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" class="form-control " ID="txtEmail"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Office Finance</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" class="form-control " ID="txtOffice"></asp:TextBox>
                                </div>
                            </div>
                           
                             <%-- jika kategori sekolah --%>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Status</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" class="form-control " ID="cboStatus" AutoPostBack="true" OnSelectedIndexChanged="cboStatus_SelectedIndexChanged">
                                        <asp:ListItem Value=""> ---Pilih Status--- </asp:ListItem>
                                        <asp:ListItem Value="0"> Yayasan </asp:ListItem>
                                        <asp:ListItem Value="4"> Adm Yayasan </asp:ListItem>
                                        <asp:ListItem Value="1"> Perwakilan </asp:ListItem>
                                        <asp:ListItem Value="3"> Adm Perwakilan </asp:ListItem>
                                        <asp:ListItem Value="2"> Unit </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Parent Unit</label>
                                <div class="col-sm-5">
                                    <asp:DropDownList ID="cboParent" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kategori Unit</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" class="form-control" ID="cboUnit">
                                        <asp:ListItem Value="">-</asp:ListItem>
                                        <asp:ListItem Value="TK"> TK </asp:ListItem>
                                        <asp:ListItem Value="SD"> SD </asp:ListItem>
                                        <asp:ListItem Value="SMP"> SMP </asp:ListItem>
                                        <asp:ListItem Value="SMA"> SMA </asp:ListItem>
                                        <asp:ListItem Value="SMK"> SMK </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Menggunakan MHS</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" class="form-control" ID="mhs">
                                        <asp:ListItem Value="1">Ya</asp:ListItem>
                                        <asp:ListItem Value="0">Tidak</asp:ListItem>
                                      </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Pelunasan</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" class="form-control" ID="allPelunasan">
                                        <asp:ListItem Value="1">Ya</asp:ListItem>
                                        <asp:ListItem Value="0">Tidak</asp:ListItem>
                                      </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Cetak Voucher</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" class="form-control" ID="cetakVoucher">
                                        <asp:ListItem Value="1">Ya</asp:ListItem>
                                        <asp:ListItem Value="0">Tidak</asp:ListItem>
                                      </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="text-center">
                                        <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
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
