<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="mDatasiswa.aspx.cs" Inherits="eFinance.Pages.Master.Input.mDatasiswa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="col-sm-6">
                                
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">NIS <span class="mandatory">*</span></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox runat="server" class="form-control " ID="nis" type="text" placeholder="Masukkan NIS"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama Siswa <span class="mandatory">*</span></label>
                                    <div class="col-sm-7">
                                        <asp:TextBox runat="server" class="form-control " ID="nama" type="text" placeholder="Masukkan Nama Siswa"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal Masuk <span class="mandatory">*</span></label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="dttglmasuk" runat="server" CssClass="form-control date"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">NIK </label>
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" class="form-control " ID="nik" type="text" placeholder="Masukkan NIK"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">NISN <span class="mandatory">*</span></label>
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" class="form-control " ID="nisn" type="text" placeholder="Masukkan NISN"></asp:TextBox>
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
                                        <asp:TextBox runat="server" class="form-control " ID="Alamat" TextMode="MultiLine" type="text" placeholder="Name"></asp:TextBox>
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
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama Orang Tua<span class="mandatory">*</span></label>
                                    <div class="col-sm-7">
                                        <asp:TextBox runat="server" class="form-control " ID="namaOrangtua" type="text" placeholder="Nama Orang Tua"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Telpon</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox runat="server" class="form-control " ID="telp" type="text"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">No.VA</label>
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" class="form-control " ID="novirtual" type="text"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Email Orang Tua</label>
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" class="form-control " ID="emailtxt" type="text"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterangan 1</label>
                                    <div class="col-sm-7">
                                        <asp:TextBox runat="server" class="form-control " ID="keterangan" TextMode="MultiLine" type="text" placeholder="Name"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterangan 2</label>
                                    <div class="col-sm-7">
                                        <asp:TextBox runat="server" class="form-control " ID="keterangan1" TextMode="MultiLine" type="text" placeholder="Name"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterangan 3</label>
                                    <div class="col-sm-7">
                                        <asp:TextBox runat="server" class="form-control " ID="keterangan2" TextMode="MultiLine" type="text" placeholder="Name"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Dapat Discount/Beasiswa</label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="discount" runat="server" Width="100">
                                             <asp:ListItem Value="1">Ya</asp:ListItem>
                                             <asp:ListItem Value="0">Tidak</asp:ListItem>
                                        </asp:DropDownList>
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
