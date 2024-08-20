<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransSuratTugas.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransSuratTugas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                           
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" class="form-control date" ID="date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Dari Tanggal</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" class="form-control date" ID="fromDate"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Sampai Tanggal</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" class="form-control date" ID="toDate"></asp:TextBox>
                                </div>
                            </div>
                           
                             <%-- jika kategori sekolah --%>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Tugas</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="cboUnit">
                                        
                                    </asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Atasan</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cboParent" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                           
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Karyawan</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" class="form-control" ID="mhs">
                                       
                                      </asp:DropDownList>
                                </div>
                            </div>
                           <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Deskripsi</label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" class="form-control " ID="deskripsi" TextMode="MultiLine"></asp:TextBox>
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
