<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Mstparametercabang.aspx.cs" Inherits="eFinance.Pages.Master.Input.Mstparametercabang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tahun Ajaran Baru</label>
                                <div class="col-sm-5">
                                    <asp:DropDownList ID="cboTA" runat="server" CssClass="form-control" Width="150"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal Awal Tahun Ajaran Baru</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" CssClass="form-control date" ID="dtKas"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal Akhir Tahun Ajaran Baru</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" CssClass="form-control date" ID="dtKas1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Absensi Include Ke Gaji</label>
                                <div class="col-sm-5">
                                     <asp:DropDownList ID="cboitunggaji" runat="server" Width="100">
                                            <asp:ListItem Value="1">Ya</asp:ListItem>
                                            <asp:ListItem Value="2">Tidak</asp:ListItem>
                                      </asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal Perhitungan Gaji</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" CssClass="form-control date" ID="dtdari"></asp:TextBox>  sd  <asp:TextBox runat="server" CssClass="form-control date" ID="dtsampai"></asp:TextBox>  
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterlambatan Absen Potong Gaji</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="cboPotonggaji" runat="server" Width="100">
                                            <asp:ListItem Value="1">Ya</asp:ListItem>
                                            <asp:ListItem Value="2">Tidak</asp:ListItem>
                                      </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                               <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jam Masuk</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" CssClass="form-control time"  ID="jammasuk" Width="80"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                               <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jam Keluar</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" CssClass="form-control time" ID="jamkeluar" Width="80"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">UMR</label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtNilai" Width="200" Text="0.00"></asp:TextBox>
                                </div>
                            </div>
                       

                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Pembayaran Gaji</label>
                                <div class="col-sm-6">
                                     <asp:DropDownList ID="cbogaji" runat="server" Width="200">
                                        <asp:ListItem Value="2">Unit</asp:ListItem>
                                        <asp:ListItem Value="3">Perwakilan</asp:ListItem>
                                        <asp:ListItem Value="4">Pusat</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Pembayaran Tagihan</label>
                                <div class="col-sm-6">
                                     <asp:DropDownList ID="cbojnsbyr" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="cboLokasi_SelectedIndexChanged">
                                        <asp:ListItem Value="Non_PaymentGateWay">Non_PaymentGateWay</asp:ListItem>
                                        <asp:ListItem Value="PaymentGateWay">PaymentGateWay</asp:ListItem>
                                     </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label"></label>
                                    <div class="col-sm-6"  id="showhidekode" runat="server">
                                      <asp:DropDownList ID="cbobank" runat="server" Width="300">
                                        <asp:ListItem Value="">---Pilih Bank---</asp:ListItem>
                                   </asp:DropDownList>
                                    </div>
                            </div>
                                <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kode Bank</label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="kdbank" Width="200"  ></asp:TextBox>
                                </div>
                            </div>
                                 <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Biaya Admin Bank</label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" CssClass="form-control money" ID="biayaadmbank" Width="200" Text="0.00"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kepala Sekolah</label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="kepalasekolah" Width="200" Text="0.00"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">IP Kantor</label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="ipcabang" Width="200" Text="0.00"></asp:TextBox>
                                </div>
                            </div>
                                 <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Admin</label>
                                    <div class="col-sm-6"   runat="server">
                                          <asp:DropDownList ID="cbojnsbank" runat="server" Width="300">
                                            <asp:ListItem Value="">---Pilih Jenis Admin---</asp:ListItem>
                                            <asp:ListItem Value="ya">Ya</asp:ListItem>
                                            <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                       </asp:DropDownList>
                                    </div>
                            </div>
                               <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Strategi Cuti</label>
                                    <div class="col-sm-6"   runat="server">
                                          <asp:DropDownList ID="cboscuti" runat="server" Width="300">
                                            <asp:ListItem Value="">---Pilih Strategi Cuti---</asp:ListItem>
                                            <asp:ListItem Value="hangus">Hangus</asp:ListItem>
                                            <asp:ListItem Value="tidakhangus">Tidak Hangus</asp:ListItem>
                                       </asp:DropDownList>
                                    </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="text-center">
                                        <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses Simpan';" />
                                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click" />
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