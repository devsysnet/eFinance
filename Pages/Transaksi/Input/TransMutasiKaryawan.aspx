<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransMutasiKaryawan.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransMutasiKaryawan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
   

    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Tanggal Mutasi <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="dtDate" runat="server" CssClass="form-control date" Width="230" ></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-sm-5 control-label">Kategori <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                               <asp:DropDownList runat="server" CssClass="form-control" Width="230" ID="cboTransaction" AutoPostBack="true" OnSelectedIndexChanged="cboTransaction_SelectedIndexChanged">
                                                <asp:ListItem Value="0">--Pilih Kategori--</asp:ListItem>
                                                <asp:ListItem Value="Mutasi">Mutasi</asp:ListItem>
                                                 <asp:ListItem Value="Mutasijabatan">Mutasi Jabatan</asp:ListItem>
                                                <asp:ListItem Value="Promosi">Promosi</asp:ListItem>
                                                <asp:ListItem Value="Keluar">Keluar</asp:ListItem>
                                                <asp:ListItem Value="Pensiun">Pensiun</asp:ListItem>
                                                <asp:ListItem Value="Demosi">Demosi</asp:ListItem>

                                         </asp:DropDownList>
                                        </div>
                                    </div>
            
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-5 control-label">Karyawan <span class="mandatory">*</span></label>
                                            <div class="col-sm-5">
                                                <asp:HiddenField runat="server" ID="hdnNoUser" />
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtNamaPeminta" Enabled="false" runat="server" CssClass="form-control" Width="200" />
                                                    <asp:ImageButton ID="btnBrowsePeminta" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image" OnClick="btnBrowsePeminta_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                                            <div class="form-group" visible="false" id="formpindah" runat="server">
                                        <label class="col-sm-5 control-label">Pindah Ke Unit</label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" AutoPostBack="false"  Width="230"></asp:DropDownList>&nbsp;&nbsp;
                                           
                                        </div>
                                    </div>
                                     <div class="form-group" id="formjabatan" visible="false" runat="server">
                                        <label class="col-sm-5 control-label">Pergantian Jabatan <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                               <asp:DropDownList Visible="false" runat="server" CssClass="form-control" Width="230" ID="mutasiK" >
                                                <asp:ListItem Value="0">--Pilih Pergantian Jabatan--</asp:ListItem>
                                                <asp:ListItem Value="Promosi">Promosi</asp:ListItem>
                                                <asp:ListItem Value="Diangkat">Diangkat</asp:ListItem>
                                                <asp:ListItem Value="Demosi">Demosi</asp:ListItem>
                                         </asp:DropDownList>
                                              <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="true" Width="230" ID="cbojabatan" >
                                             
                                         </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">No SK</label>
                                        <div class="col-sm-5">
                                             <asp:TextBox runat="server" CssClass="form-control"  ID="txtNoSK" Width="230"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Diskripsi</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtKeterangan" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                  <div class="row">
                        <div class="col-sm-12">
                            <div class="text-center">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Simpan" CausesValidation="false" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Batal" CausesValidation="false" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgAddData" runat="server" PopupControlID="panelAddDataPeminta" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelAddDataPeminta" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Peminta</h4>
            </div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="modal-body ">
                <div class="modal-body col-overflow-400">
                    <div class="row">
                         <div class="col-sm-6">
                            <div class="form-group ">
                                <div class="form-inline">
                                    <label class="col-sm-3 control-label">Cari</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtSearchMinta" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:Button ID="btnMinta" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnMinta_Click" CausesValidation="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="grdDataPeminta" DataKeyNames="nokaryawan" ShowFooter="true" SkinID="GridView" runat="server" OnSelectedIndexChanged="grdDataPeminta_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:HiddenField ID="hdnNoUserD" runat="server" Value='<%# Bind("noKaryawan") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="nik" HeaderStyle-CssClass="text-center" HeaderText="ID User" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="nama" HeaderStyle-CssClass="text-center" HeaderText="Nama User" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="" ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="panelAddDataBiaya" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Detil</h4>
            </div>

            <div class="modal-body ">
                <asp:Label runat="server" ID="lblMessageError"></asp:Label>
                <div class="modal-body col-overflow-400">
                    <div class="row">
                         <div class="col-sm-6">
                            <div class="form-group ">
                                <div class="form-inline">
                                    <label class="col-sm-3 control-label">Cari</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnCari_Click" CausesValidation="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:HiddenField ID="txtHdnPopup" runat="server" />
                        <asp:GridView ID="grdDataBiaya" DataKeyNames="noBiaya" ShowFooter="true" SkinID="GridView" runat="server" OnSelectedIndexChanged="grdDataBiaya_SelectedIndexChanged" OnRowDataBound="grdDataBiaya_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <%# Container.DataItemIndex + 1 %>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Kode" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="Kode" runat="server" Text='<%# Bind("Kode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nama" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="Nama" runat="server" Text='<%# Bind("Nama") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="" ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>


