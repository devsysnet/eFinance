<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransPindahAssetView.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TransPindahAssetView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-inline">
                                    <div class="col-sm-12">
                                        <asp:TextBox ID="dtMulai" runat="server" CssClass="form-control date"></asp:TextBox>
                                        <asp:TextBox ID="dtSampai" runat="server" CssClass="form-control date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-6">
                                <div class="form-inline">
                                    <div class="col-sm-12">
                                        <asp:TextBox runat="server" ID="txtCariAset" CssClass="form-control"></asp:TextBox>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <asp:GridView ID="grdAsetUpdate" DataKeyNames="noPindahAsset" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdAsetUpdate_PageIndexChanging"
                                    OnRowCommand="grdAsetUpdate_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="tglPindah" HeaderText="Tgl Pindah" ItemStyle-Width="7%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="kdPindah" HeaderText="Kode Transaksi" ItemStyle-Width="8%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="kodeAsset" HeaderText="Kode Asset" ItemStyle-Width="10%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="namaAsset" HeaderText="Asset" ItemStyle-Width="20%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="namacabang" HeaderText="Unit Tujuan" ItemStyle-Width="10%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="uraian" HeaderText="Alasan Pindah" ItemStyle-Width="30%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Detail" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Button ID="btnSelectDel" runat="server" class="btn btn-xs btn-labeled btn-danger" Text="Hapus" CommandName="SelectDelete" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server" visible="false">

                        <div class="row">
                            <div class="form-horizontal">
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Nama Barang Aset <span class="mandatory">*</span></label>
                                    <div class="form-inline">
                                        <div class="col-sm-9">
                                            <asp:HiddenField runat="server" ID="hdnBarangAset" />
                                            <asp:HiddenField runat="server" ID="hdnBarangAsetlm" />
                                            <asp:HiddenField runat="server" ID="hdnnopindah" />
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNamaBarangAset" Enabled="false"></asp:TextBox>
                                            <asp:ImageButton ID="btnBrowseAsset" runat="server" ImageUrl="~/assets/images/icon_search.png" Enabled="false" CssClass="btn-image form-control" OnClick="btnBrowseAsset_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Kode Aset </label>
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" ID="txtKodeAset" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Lokasi Barang </label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="cboLokasi" CssClass="form-control" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Sub Lokasi </label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="cboSubLokasi" CssClass="form-control" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Ke Unit </label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="cbocabang" CssClass="form-control" runat="server" Enabled="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Tgl Pindah <span class="mandatory">*</span></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox runat="server" ID="dtMusnah" CssClass="form-control date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Alasan Pindah Asset <span class="mandatory">*</span></label>
                                    <div class="col-sm-7">
                                        <asp:TextBox runat="server" ID="txtAlasan" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-md-12">
                                        <div class="text-center">
                                            <%--<asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>--%>
                                            <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgAddData" runat="server" PopupControlID="panelAddData" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelAddData" runat="server" align="center" Style="display: none" Width="80%" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Barang Aset </h4>
            </div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="modal-body ">
                <div class="modal-body col-overflow-400">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <div class="form-inline">
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:Button ID="btnAsset" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnAsset_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="grdDataAsset" DataKeyNames="noAset" SkinID="GridView" AllowPaging="true" PageSize="10" runat="server" OnSelectedIndexChanged="grdDataAsset_SelectedIndexChanged" OnPageIndexChanging="grdDataAsset_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:HiddenField ID="hdnnoAset" runat="server" Value='<%# Bind("noAset") %>' />
                                            <asp:HiddenField ID="hdnnoLokasi" runat="server" Value='<%# Bind("noLokasi") %>' />
                                            <asp:HiddenField ID="hdnnoSubLokasi" runat="server" Value='<%# Bind("noSubLokasi") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="kodeAsset" HeaderStyle-CssClass="text-center" HeaderText="Kode Aset" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="namaAsset" HeaderStyle-CssClass="text-center" HeaderText="Nama Aset" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="lokasi" HeaderStyle-CssClass="text-center" HeaderText="Lokasi" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="sublokasi" HeaderStyle-CssClass="text-center" HeaderText="Sub Lokasi" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="" ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-primary btn-xs" Text="Pilih" CausesValidation="false" CommandName="Select" />
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
