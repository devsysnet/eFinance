<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="FormDokterUmum.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.FormDokterUmum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="TabName" runat="server" />
    <script type="text/javascript">
        $(function () {
            SetTabs();

        });
        
        Sys.Application.add_init(appl_init);
        function appl_init() {
            var pgRegMgr = Sys.WebForms.PageRequestManager.getInstance();
            pgRegMgr.add_beginRequest(SetTabs);
            pgRegMgr.add_endRequest(SetTabs);
        }
        function SetTabs() {
            var tabName = $("#<%=TabName.ClientID%>").val();
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("#<%=TabName.ClientID%>").val($(this).attr("href").replace("#", ""));
            });

        };

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
        function round(value, decimals) {
            return Number(Math.round(value + 'e' + decimals) + 'e-' + decimals);
        }

        function Calculate() {
            var qty = 0, harga = 0, total = 0;
            var biaya = removeMoney(document.getElementById("<%=txtBiaya.ClientID %>").value);
            var biayaObat = removeMoney(document.getElementById("<%=txtBiayaObat.ClientID %>").value);
            var gridView = document.getElementById("<%=grdObat.ClientID %>");

            for (var i = 1; i < gridView.rows.length; i++) {
                qty = removeMoney(gridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value) * 1;
                harga = removeMoney(gridView.rows[i].cells[6].getElementsByTagName("INPUT")[0].value) * 1;

                total += qty * harga;
            }

            document.getElementById("BodyContent_txtBiaya").value = money(round(total, 2));

            document.getElementById("BodyContent_txtTotal").value = money(round(total + biayaObat, 2));
        }
    </script>
    <asp:HiddenField ID="hdnId" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:GridView ID="grdRegister" DataKeyNames="noMedik" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdRegister_PageIndexChanging" OnRowCommand="grdRegister_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="tglmedik" HeaderStyle-CssClass="text-center" HeaderText="Tgl Register" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                                        <asp:BoundField DataField="noregister" HeaderStyle-CssClass="text-center" HeaderText="No Register" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="namaPasien" HeaderStyle-CssClass="text-center" HeaderText="Nama Pasien" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="uraian" HeaderStyle-CssClass="text-center" HeaderText="Keluhan" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Button ID="btnSelectDetil" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Detil" CommandName="SelectDetil" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-left">No. Registrasi <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:HiddenField runat="server" ID="hdnReg" />
                                            <div class="input-group-btn">
                                                <asp:TextBox ID="txtNoReg" Enabled="false" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Nama </label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtNamaReg" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Alamat </label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtAlamatReg" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="form-inline">
                                        <label class="col-sm-3 control-label">Umur </label>
                                        <div class="col-sm-9">
                                            <asp:Textbox runat="server" ID="txtThn" CssClass="form-control" Placeholder="Thn" Width="50" Enabled="false"></asp:Textbox> Thn
                                            <asp:Textbox runat="server" ID="txtBln" CssClass="form-control" Placeholder="Bln" Width="50" Enabled="false"></asp:Textbox> Bln
                                            <%--<asp:Textbox runat="server" ID="txtHari" CssClass="form-control" Placeholder="Hr" Width="80"></asp:Textbox>--%>
                                        </div>
                                            </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Tgl Periksa</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control date" ID="dtPeriksa"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-sm-3 control-label">Berat (Kg) </label>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtBerat" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Tinggi Badan (cm)</label>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtTinggi" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-sm-3 control-label">Suhu Badan</label>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtShBadan" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-sm-3 control-label">Tekanan Darah</label>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txttekanan" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-12">
                                    <div class="panel-body" id="Tabs" role="tabpanel">
                                        <ul class="nav nav-tabs" role="tablist">
                                            <li class="active">
                                                <a href="#tab-periksa" data-toggle="tab" role="tab">Pemeriksaan
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#tab-history" data-toggle="tab" role="tab">History
                                                </a>
                                            </li>
                                        </ul>
                                        <div class="tab-content tab-content-bordered">
                                            <div role="tabpanel" class="tab-pane active" id="tab-periksa">
                                                <div class="row">
                                                    <div class="form-horizontal">
                                                        <div class="col-sm-4">
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">Anamnesa </label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtAnamnesa" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">Diagnosa </label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtDiagnosa" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">Tindakan / Terapi </label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtTindakan" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-horizontal">
                                                    <div class="col-sm-9 overflow-x-table">
                                                        <asp:GridView ID="grdObat" SkinID="GridView" runat="server" OnRowCommand="grdObat_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                                    <ItemTemplate>
                                                                        <div class="text-center">
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                            <asp:HiddenField ID="hdnParameter" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kode" ItemStyle-Width="12%">
                                                                    <ItemTemplate>
                                                                        <div class="form-group">
                                                                            <div class="form-inline">
                                                                                <div class="text-center">
                                                                                    <asp:TextBox runat="server" ID="txtKode" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:HiddenField ID="hdnKode" runat="server" />
                                                                                    <asp:HiddenField ID="hdnIndexSA" runat="server" />
                                                                                    <asp:ImageButton ID="btnImgAccount" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image form-control" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Obat" ItemStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <div class="text-left">
                                                                            <asp:Label runat="server" ID="lblDescription"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Stok" ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <div class="text-center">
                                                                            <asp:Label runat="server" ID="lblStok"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty" ItemStyle-Width="6%">
                                                                    <ItemTemplate>
                                                                        <div class="text-left">
                                                                            <asp:TextBox runat="server" ID="txtQty" CssClass="form-control money" onKeyUp="Calculate()" onKeyDown="Calculate()"></asp:TextBox>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan" ItemStyle-Width="4%">
                                                                    <ItemTemplate>
                                                                        <div class="text-center">
                                                                            <asp:Label runat="server" ID="lblSatuan"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Harga" ItemStyle-Width="9%">
                                                                    <ItemTemplate>
                                                                        <div class="text-left">
                                                                            <asp:TextBox runat="server" ID="txtHrg" CssClass="form-control money" onKeyUp="Calculate()" onKeyDown="Calculate()"></asp:TextBox>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Keterangan" ItemStyle-Width="12%">
                                                                    <ItemTemplate>
                                                                        <div class="text-left">
                                                                            <asp:TextBox runat="server" ID="txtKet" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label text-right">Jumlah </label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label text-right">Obat </label>
                                                                <div class="col-sm-5 text-right">
                                                                    <asp:TextBox ID="txtBiaya" runat="server" Enabled="false" CssClass="form-control money" Text="0.00"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                 <label class="col-sm-3 control-label text-right">Biaya Dokter </label>
                                                                <div class="col-sm-5 text-right">
                                                                     <asp:TextBox ID="txtBiayaObat" runat="server" CssClass="form-control money" Text="0.00" onKeyUp="return Calculate(event);" onKeyDown="return Calculate(event);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                 <label class="col-sm-3 control-label text-right">Total </label>
                                                                <div class="col-sm-5 text-right">
                                                                     <asp:TextBox ID="txtTotal" runat="server" Enabled="false" CssClass="form-control money" Text="0.00"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div role="tabpanel" class="tab-pane" id="tab-history">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <asp:GridView ID="grdRegisterHistory" SkinID="GridView" runat="server">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                                    <ItemTemplate>
                                                                        <div class="text-center">
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="tglPeriksa" HeaderStyle-CssClass="text-center" HeaderText="Tgl Periksa" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                                                                <asp:BoundField DataField="anamnesa" HeaderStyle-CssClass="text-center" HeaderText="Anamnesa" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="diagnosa" HeaderStyle-CssClass="text-center" HeaderText="Diagnosa" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="tindakan" HeaderStyle-CssClass="text-center" HeaderText="Tindakan / Terapi" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="tinggibadan" HeaderStyle-CssClass="text-center" HeaderText="Tinggi" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="beratbadan" HeaderStyle-CssClass="text-center" HeaderText="Berat" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="suhubadan" HeaderStyle-CssClass="text-center" HeaderText="Suhu" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="tekanandarah" HeaderStyle-CssClass="text-center" HeaderText="Tekanan Darah" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="obat" HeaderStyle-CssClass="text-center" HeaderText="Obat" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                                                                
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-12">
                                <div class="text-center">
                                    <asp:Button runat="server" ID="btnAddRow" CssClass="btn btn-success" Text="Tambah Baris" OnClick="btnAddRow_Click"></asp:Button>
                                    <asp:Button runat="server" ID="btnProses" CssClass="btn btn-primary" Text="Proses" OnClick="btnProses_Click"></asp:Button>
                                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgObat" runat="server" PopupControlID="panelObat" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelObat" runat="server" align="center" Width="60%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Obat</h4>
            </div>
            <div class="modal-body col-overflow-500">
                <asp:Label runat="server" ID="lblMessageError"></asp:Label>
                <div class="table-responsive">
                <asp:HiddenField runat="server" ID="hdnParameterObat" />
                    <asp:GridView ID="grdObatDetil" SkinID="GridView" runat="server" AllowPaging="false" PageSize="10" OnSelectedIndexChanged="grdObatDetil_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kode" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblKdObat" Text='<%#Eval("kodeBarang")%>'></asp:Label>
                                        <asp:HiddenField ID="hdnNoObat" runat="server" Value='<%#Eval("noBarang")%>'/>
                                        <asp:HiddenField ID="hdnQty" runat="server" Value='<%#Eval("qty")%>'/>
                                        <asp:HiddenField ID="hdnHarga" runat="server" Value='<%#Eval("harga")%>'/>
                                        <asp:HiddenField ID="hdnNoSA" runat="server" Value='<%#Eval("noSA")%>'/>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Obat" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblKet" Text='<%#Eval("namaBarang")%>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblSat" Text='<%#Eval("satuanKecil")%>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Batch" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblBatch" Text='<%#Eval("lotno")%>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Expired" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblExpired" Text='<%#Eval("expired")%>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelectSub" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnBatal" runat="server" Text="Tutup" CssClass="btn btn-danger" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
