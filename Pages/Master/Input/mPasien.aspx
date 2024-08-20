<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="mPasien.aspx.cs" Inherits="eFinance.Pages.Master.Input.mPasien" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="col-sm-6">
                               <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama Pasien <span class="mandatory">*</span></label>
                                    <div class="col-sm-7">
                                        <asp:TextBox runat="server" class="form-control " ID="namaPasien" type="text" placeholder="Masukkan Nama Pasien"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">NIK </label>
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" class="form-control " ID="nik" type="text" placeholder="Masukkan NIK"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Kelamin</label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="cbojnskelamin" runat="server">
                                            <asp:ListItem Value="0">--Pilih Jenis Kelamin---</asp:ListItem>
                                            <asp:ListItem Value="1">Laki Laki</asp:ListItem>
                                            <asp:ListItem Value="2">Perempuan</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Alamat</label>
                                    <div class="col-sm-7">
                                        <asp:TextBox runat="server" class="form-control " ID="Alamat" TextMode="MultiLine" type="text" placeholder="Alamat"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kota</label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="cboKota" class="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Agama</label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="cboAgama" class="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                 <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kota Lahir</label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="cboKotalahir" class="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal Lahir</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="dtTgllahir" runat="server" CssClass="form-control date"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Golongsn Darah</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="cboGoldarah" runat="server">
                                            <asp:ListItem Value="0">--Golongan Darah--</asp:ListItem>
                                            <asp:ListItem Value="A">A</asp:ListItem>
                                            <asp:ListItem Value="B">B</asp:ListItem>
                                            <asp:ListItem Value="AB">AB</asp:ListItem>
                                            <asp:ListItem Value="O">O</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                               <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Telpon</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox runat="server" class="form-control " ID="notelp" type="text" placeholder="Telphon"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">No.HP</label>
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" class="form-control " ID="nohp" type="text" placeholder="Nomor HP"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Peserta BPJS</label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="cboBpjs" runat="server">
                                            <asp:ListItem Value="0">--Pilih Peserta BPJS---</asp:ListItem>
                                            <asp:ListItem Value="0">Tidak</asp:ListItem>
                                            <asp:ListItem Value="1">Ya</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nomor BPJS</label>
                                    <div class="col-sm-7">
                                        <asp:TextBox runat="server" class="form-control " ID="noBpjs" type="text" placeholder="Nomor BPJS"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterangan</label>
                                    <div class="col-sm-7">
                                        <asp:TextBox runat="server" class="form-control " ID="uraian" TextMode="MultiLine" type="text" placeholder="Keterangan"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="text-center">
                                <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
