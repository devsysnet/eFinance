<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="mPasienupdate.aspx.cs" Inherits="eFinance.Pages.Master.Update.mPasienupdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" />

    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-8">
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <label>Search :</label>
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdSiswa" DataKeyNames="nopasien" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdSiswa_PageIndexChanging" OnRowCommand="grdSiswa_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="NO." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nik" HeaderText="NIK" HeaderStyle-CssClass="text-center" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="namaPasien" HeaderText="Nama" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="alamat" HeaderText="Alamat" HeaderStyle-CssClass="text-center" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Ubah" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectDel" runat="server" class="btn btn-xs btn-labeled btn-danger" Text="Hapus" CommandName="SelectDelete" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
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
                                    <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Status</label>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ID="cbosts" runat="server" Width="100">
                                            <asp:ListItem Value="1">Aktiv</asp:ListItem>
                                            <asp:ListItem Value="0">NonAktiv</asp:ListItem>
                                        </asp:DropDownList>
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
                                    <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Back" OnClick="btnCancel_Click"></asp:Button>
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
