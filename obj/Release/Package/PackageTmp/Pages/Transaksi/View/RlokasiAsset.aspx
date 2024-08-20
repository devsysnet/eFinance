<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RLokasiAsset.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RLokasiAsset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function OpenReport() {
            //            OpenReportViewer();
            window.open('../../../Report/ReportViewer.aspx', 'name', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,modal=yes,width=1000,height=600');
        }
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
                                <div class="form-group">
                                    <div class="col-sm-1 control-label">
                                        <span>Filter Cari</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="cboCabang" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="cboCariLokasi" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboCariLokasi_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="cboCariSubLokasi" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboCariSubLokasi_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="txtSearchAsset" runat="server" CssClass="form-control" placeholder="Masukan kata" AutoPostBack="true" OnTextChanged="txtSearchAsset_TextChanged"></asp:TextBox>
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
                                                        <asp:HiddenField ID="hdnIdPrint" runat="server" Value='<%# Eval("noAset") %>' />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="kodeAsset" HeaderText="Kode Aset" HeaderStyle-CssClass="text-center" ItemStyle-Width="43%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="tglAsset" HeaderText="Tgl Aset" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="namaAsset" HeaderText="Nama Aset" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="lokasi" HeaderText="Lokasi" HeaderStyle-CssClass="text-center" ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="11%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Detil" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-success btn-sm" Text="Print" CommandName="detail" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
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
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Nama Barang </label>
                                    <div class="form-inline">
                                        <div class="col-sm-9">
                                            <asp:HiddenField runat="server" ID="hdnBarang" />
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNamaBarang" Enabled="false"></asp:TextBox>
                                        </div>
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
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Tanggal Asset </label>
                                    <div class="col-sm-3">
                                        <asp:TextBox runat="server" CssClass="form-control" ID="dtTanggal" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Nilai Perolehan </label>
                                    <div class="form-inline">
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control money" ID="txtNilai" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Uraian </label>
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtUraian" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Kelompok </label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="cboKelompok" CssClass="form-control" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">COA (Debet) </label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="cborekdb" CssClass="form-control" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">COA (Kredit) </label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="cborekkd" CssClass="form-control" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Dana BOS</label>
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="cboDana" CssClass="form-control" runat="server" Enabled="false">
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
                                        <asp:TextBox ID="txtKetLainnya" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Keadaan </label>
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="cboKeadaan" CssClass="form-control" runat="server" Enabled="false">
                                            <asp:ListItem Value="">---Pilih Keadaan---</asp:ListItem>
                                            <asp:ListItem Value="Baik">Baik</asp:ListItem>
                                            <asp:ListItem Value="Rusak">Rusak</asp:ListItem>
                                            <asp:ListItem Value="Kurang">Kurang</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-md-12">
                                        <div class="text-center">
                                            <asp:Button runat="server" ID="btnResetRek" CssClass="btn btn-danger" Text="Batal" OnClick="btnResetRek_Click"></asp:Button>
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

   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
