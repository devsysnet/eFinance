<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RdaftarAssetView1.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RdaftarAssetView1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function money(money) {
            var num = money;
            if (num != "") {
                var string = String(num);
                var string = string.replace('.', ' ');

                var array2 = string.toString().split(' ');
                num = parseInt(num).toFixed(0);

                var array = num.toString().split('');

                var index = -3;
                while (array.length + index > 0) {
                    array.splice(index, 0, ',');
                    index -= 4;
                }
                if (array2.length == 1) {
                    money = array.join('') + '.00';
                } else {
                    money = array.join('') + '.' + array2[1];
                    if (array2.length == 1) {
                        money = array.join('') + '.00';
                    } else {
                        if (array2[1].length == 1) {
                            money = array.join('') + '.' + array2[1].substring(0, 2) + '0';
                        } else {
                            money = array.join('') + '.' + array2[1].substring(0, 2);
                        }
                    }
                }
            } else {
                money = '0.00';
            }
            return money;
        }
        function removeMoney(money) {
            var minus = money.substring(0, 1);
            if (minus == "-") {
                var number = '-' + Number(money.replace(/[^0-9\.]+/g, ""));
            } else {
                var number = Number(money.replace(/[^0-9\.]+/g, ""));
            }
            return number;
        }
    </script>

    <asp:HiddenField ID="hdnId" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">

                    <div id="tabGrid" runat="server">
                        <div class="row">
                    
                            <div class="col-sm-12">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-6">
                                                 <asp:DropDownList ID="cboCabang" runat="server" CssClass="form-control"  Width="250" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged"></asp:DropDownList>
                                                 <asp:DropDownList ID="cbojnsbrg" AutoPostBack="true" CssClass="form-control" runat="server"  OnSelectedIndexChanged="cbojnsbrg_SelectedIndexChanged">
                                                 <asp:ListItem Value="0">Pilih Jenis Barang</asp:ListItem>
                                                <asp:ListItem Value="1">Asset Tidak Bergerak</asp:ListItem>
                                                <asp:ListItem Value="2">Asset Bergerak</asp:ListItem>
                                                <asp:ListItem Value="4">Inventaris</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-6">
                                      
                                            <label>Cari :</label>
                                            <asp:TextBox ID="txtSearchAsset" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:Button ID="btnSearchAsset" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearchAsset_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"  />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdAssetUpdate" DataKeyNames="noAset" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdAssetUpdate_PageIndexChanging" OnRowCommand="grdAssetUpdate_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:BoundField DataField="tglAsset" HeaderText="Tgl Aset" HeaderStyle-CssClass="text-center" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="namaAsset" HeaderText="Nama Aset" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="penggunaan" HeaderText="Penggunaan" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="kota" HeaderText="Kota" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="status" HeaderText="Status" HeaderStyle-CssClass="text-center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="tgljthtempo" HeaderText="Tgl Jatuh Tempo" HeaderStyle-CssClass="text-center" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Detail" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" Visible="false" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
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
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Kode Asset</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtKode" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                  <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Jenis Barang <span class="mandatory">*</span></label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="cbojnsBarang" AutoPostBack="true" Enabled="false" CssClass="form-control" runat="server"  OnSelectedIndexChanged="cbojnsbarang_SelectedIndexChanged">
                                         <asp:ListItem Value="0">Pilih Jenis Barang</asp:ListItem>
                                        <asp:ListItem Value="1">Asset Tidak Bergerak</asp:ListItem>
                                        <asp:ListItem Value="2">Asset Bergerak</asp:ListItem>
                                        <asp:ListItem Value="4">Inventaris</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Nama Barang <span class="mandatory">*</span></label>
                                    <div class="form-inline">
                                        <div class="col-sm-9">
                                            <asp:HiddenField runat="server" ID="hdnBarang" />
                                                                           <asp:HiddenField runat="server" ID="hdnkatbarang" />
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNamaBarang" Enabled="false"></asp:TextBox>
                                            <asp:ImageButton Visible="false" ID="btnBrowseAsset" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image form-control" OnClick="btnBrowseAsset_Click" />
                                        </div>
                                    </div>
                                </div>
                                   <%-- start asset tidak bergerak --%>
                            <div runat="server" id="divassettidakbergerak">
                                           <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Penggunaan<span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="txtpenggunaan"></asp:TextBox>
                                </div>
                            </div>
                           
                             <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Alamat<span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="txtalamat"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Kelurahan<span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server"  Enabled="false" CssClass="form-control" ID="txtkelurahan"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Kecamatan<span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="txtkecamatan"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Kabupaten/Kota<span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="txtkota"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group row" runat="server" id="stsTanah" visible="false">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Status<span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cboStatus" runat="server" Enabled="false" CssClass="form-control">
                                        <asp:ListItem Value="HGB">HGB</asp:ListItem>
                                        <asp:ListItem Value="HP">HP</asp:ListItem>
                                        <asp:ListItem Value="HM">HM</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                                 <div class="form-group row" runat="server" id="stsbangunan" visible="false">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Status<span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cbostatusB" runat="server" Enabled="false" CssClass="form-control">
                                        <asp:ListItem Value="IMB">IMB</asp:ListItem>
                                        <asp:ListItem Value="NonIMB">NonIMB</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                                 <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Luas <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" CssClass="form-control " Enabled="false" ID="txtLuas"></asp:TextBox>
                                </div>
                            </div>
                                 <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Tanggal Perolehan <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" CssClass="form-control  " Enabled="false" ID="dtPerolehan"></asp:TextBox>
                                </div>
                            </div>
                                 <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Nilai Perolehan<span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" CssClass="form-control money" Enabled="false" ID="txtnilaiP"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Nomor <label runat="server" id="lblnomor"></label><span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtnomor" Enabled="false" ></asp:TextBox>
                                </div>
                            </div>

                              <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Atas Nama<span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtnama" Enabled="false" ></asp:TextBox>
                                </div>
                            </div>
                              <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Peruntukan<span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtperuntukan" Enabled="false" ></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row" runat="server" id="divtgljthtempo">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Tanggal Jatuh Tempo <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" CssClass="form-control  " Enabled="false" ID="dttgl"></asp:TextBox>
                                </div>
                            </div>
                            </div>
                            <%-- end asset tidak bergerak --%>
                           <%-- start asset bergerak --%>
                                <div runat="server" id="divAssetBergerak">
                                     <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Penggunaan<span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtpenggunaan1" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Atas Nama<span class="mandatory">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtnamapemilik" Enabled="false" ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Jenis Kendaraan<span class="mandatory">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtjnskendaraan"  Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Nomor Kendaraan<span class="mandatory">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtnamakendaraan" Enabled="false" ></asp:TextBox>
                                        </div>
                                    </div>
                                      <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Tanggal Perolehan <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" CssClass="form-control  " Enabled="false" ID="dtPerolehan1"></asp:TextBox>
                                </div>
                            </div>
                                 <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Nilai Perolehan<span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtnilai1" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                                      <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Tanggal  Tempo Pajak <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" CssClass="form-control  " Enabled="false" ID="dttempopajak"></asp:TextBox>
                                </div>
                            </div>
                                </div>
                           <%-- end asset bergerak --%>
                                <div runat="server" id="divAsset">
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Lokasi Barang <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboLokasi" CssClass="form-control" Enabled="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboLokasi_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Sub Lokasi <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboSubLokasi" CssClass="form-control" Enabled="false" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Tanggal Asset <span class="mandatory">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control   " Enabled="false" ID="dtTanggal"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Nilai Perolehan <span class="mandatory">*</span></label>
                                        <div class="form-inline">
                                            <div class="col-sm-5">
                                                <asp:TextBox runat="server" CssClass="form-control money" Enabled="false" ID="txtNilai"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Uraian </label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtUraian" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">kategori <span class="mandatory">*</span></label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="cbokategori" CssClass="form-control" runat="server" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="cboDana_SelectedIndexChangedkat">
                                                <asp:ListItem Value="">---Pilih Kategori---</asp:ListItem>
                                                <asp:ListItem Value="Asset">Asset</asp:ListItem>
                                                <asp:ListItem Value="Invetaris">Invetaris</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                       </div>
                                    <div class="form-group row" runat="server" id="showhideLainnya1">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Kelompok <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboKelompok" CssClass="form-control" Enabled="false" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Dana <span class="mandatory">*</span></label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="cboDana" CssClass="form-control" runat="server" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="cboDana_SelectedIndexChanged">
                                                <asp:ListItem Value="">---Pilih Dana---</asp:ListItem>
                                                <asp:ListItem Value="Tidak">Tidak (Yayasan)</asp:ListItem>
                                                <asp:ListItem Value="Ya">Ya (Bos)</asp:ListItem>
                                                <asp:ListItem Value="Lain">Lainnya</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row" runat="server" id="showhideLainnya">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Keterangan Lainnya </label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtKetLainnya" runat="server" Enabled="false" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Keadaan <span class="mandatory">*</span></label>
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="cboKeadaan" CssClass="form-control" Enabled="false" runat="server">
                                            <asp:ListItem Value="">---Pilih Keadaan---</asp:ListItem>
                                            <asp:ListItem Value="Baik">Baik</asp:ListItem>
                                            <asp:ListItem Value="Rusak">Rusak</asp:ListItem>
                                            <asp:ListItem Value="Kurang">Kurang</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                </div>
                               
                                </div>
                                <div class="form-group row">
                                    <div class="col-md-12">
                                        <div class="text-center">
                                            <asp:Button Visible="false" runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                            <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Kembali" OnClick="btnReset_Click"></asp:Button>
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
    <asp:Panel ID="panelAddData" runat="server" align="center" Style="display: none" Width="60%" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Barang Aset</h4>
            </div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="modal-body ">
                <div class="modal-body col-overflow-400">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group ">
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
                        <asp:GridView ID="grdDataAsset" DataKeyNames="noBarang" SkinID="GridView" AllowPaging="true" PageSize="10" runat="server" OnSelectedIndexChanged="grdDataAsset_SelectedIndexChanged" OnPageIndexChanging="grdDataAsset_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:HiddenField ID="hdnnoBarang" runat="server" Value='<%# Bind("noBarang") %>' />
                                              <asp:HiddenField ID="hdnkategori" runat="server" Value='<%# Bind("kategoriBarang") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="kodeBarang" HeaderStyle-CssClass="text-center" HeaderText="Kode Barang" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="namaBarang" HeaderStyle-CssClass="text-center" HeaderText="Nama Asset" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="kodeAset" HeaderStyle-CssClass="text-center" HeaderText="Kode Asset" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="" ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-primary btn-sm" Text="Pilih" CausesValidation="false" CommandName="Select" />
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
