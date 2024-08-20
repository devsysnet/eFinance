<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransPO.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransPO" %>

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

        function CalculatePPn() {
            var Qty = 0, unitPrice = 0, hasil = 0;
            var gridView = document.getElementById("<%=grdPRDetil.ClientID %>");
            var pajak = document.getElementById("<%=cboPajak.ClientID %>").value;
            
            var subTotal = 0;
             for (var i = 1; i < gridView.rows.length; i++) {
                 Qty = removeMoney(gridView.rows[i].cells[6].getElementsByTagName("INPUT")[0].value) * 1;

                 unitPrice = removeMoney(gridView.rows[i].cells[8].getElementsByTagName("INPUT")[0].value) * 1;

                 gridView.rows[i].cells[10].getElementsByTagName("INPUT")[0].value = money((unitPrice * Qty));

                 subTotal += (removeMoney(gridView.rows[i].cells[10].getElementsByTagName("INPUT")[0].value) * 1);
             }

             document.getElementById("<%=txtSubTotal.ClientID %>").value = money(subTotal);

            if (pajak == "Ya") {
                var ppn = (subTotal * 11 / 100) * 1;

                document.getElementById("<%=txtSubTotal.ClientID %>").value = money(subTotal);
                document.getElementById("<%=txtPPn.ClientID %>").value = money(ppn);
                document.getElementById("<%=txtTotal.ClientID %>").value = money(subTotal + ppn);
            }
            else {
                var ppn = 0;
                document.getElementById("<%=txtSubTotal.ClientID %>").value = money(subTotal);
                document.getElementById("<%=txtPPn.ClientID %>").value = money(ppn);
                document.getElementById("<%=txtTotal.ClientID %>").value = money(subTotal + ppn);
            }
            
            
        }

        function CalculatePPh() {
            var gridView = document.getElementById("<%=grdPRDetil.ClientID %>");
            var Qty = 0, unitPrice = 0, hasil = 0;
            var subTotal = 0;
             for (var i = 1; i < gridView.rows.length; i++) {
                 Qty = removeMoney(gridView.rows[i].cells[5].getElementsByTagName("INPUT")[0].value) * 1;
                 unitPrice = removeMoney(gridView.rows[i].cells[6].getElementsByTagName("INPUT")[0].value) * 1;

                 gridView.rows[i].cells[7].getElementsByTagName("INPUT")[0].value = money((unitPrice * Qty));

                 subTotal += (removeMoney(gridView.rows[i].cells[7].getElementsByTagName("INPUT")[0].value) * 1);
             }

             document.getElementById("<%=txtSubTotal.ClientID %>").value = money(subTotal);

            
       }

    </script>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnPersenPajak" runat="server" />
    <asp:HiddenField ID="hdnPersenPPh" runat="server" />                                        
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="form-horizontal">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label class="col-sm-5 control-label">Jenis Permintaan</label>
                        <div class="col-sm-5">
                            <asp:DropDownList runat="server" CssClass="form-control" Width="200" ID="cboTransaction" AutoPostBack="true" OnSelectedIndexChanged="cboTransaction_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Pilih Jenis Permintaan--</asp:ListItem>
                                <asp:ListItem Value="1">Barang Aset</asp:ListItem>
                                <%--Dana tidak masuk ke PO--%>
                                <%--<asp:ListItem Value="2">Dana</asp:ListItem>--%>
                                <asp:ListItem Value="3">Jasa</asp:ListItem>
                                <asp:ListItem Value="4">Barang Operasional</asp:ListItem>
                                <asp:ListItem Value="5">Barang Sales</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" runat="server" id="divDanaBOSH">
                        <label class="col-sm-5 control-label">Dana BOS</label>
                        <div class="col-sm-5">
                            <asp:DropDownList ID="cboDana" class="form-control" runat="server" Width="120" AutoPostBack="true" OnSelectedIndexChanged="cboDana_SelectedIndexChanged">
                                <asp:ListItem Value="Tidak" Selected="True">Tidak</asp:ListItem>
                                <asp:ListItem Value="Ya">Ya</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdPR" DataKeyNames="noBarang" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPR_PageIndexChanging"
                    OnRowDataBound="grdPR_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnnoBarang" runat="server" Value='<%# Bind("noBarang") %>' />
                                    <asp:HiddenField ID="hdnnoPRD" runat="server" Value='<%# Bind("noPRD") %>' />
                                    <asp:HiddenField ID="hdnnoPR" runat="server" Value='<%# Bind("noPR") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="5%" HeaderText="">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:CheckBox ID="chkBrg" runat="server" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="namaCabang" HeaderStyle-CssClass="text-center" HeaderText="Pembuat PO" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="kodeBarang" HeaderStyle-CssClass="text-center" HeaderText="Kode Barang" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="namaBarang" HeaderStyle-CssClass="text-center" HeaderText="Nama Barang" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="5%" HeaderText="Qty Besar">
                            <ItemTemplate>
                                <div class="text-right">
                                    <asp:Label ID="nilaiBesar" runat="server" Text='<%# Bind("qtybesar", "{0:#,0.00}") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="satBesar" HeaderStyle-CssClass="text-center" HeaderText="Sat Besar" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="5%" HeaderText="Qty">
                            <ItemTemplate>
                                <div class="text-right">
                                    <asp:Label ID="nilai" runat="server" Text='<%# Bind("qty", "{0:#,0.00}") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="satuan" HeaderStyle-CssClass="text-center" HeaderText="Satuan" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="noPR" HeaderStyle-CssClass="text-center" HeaderText="No. Permintaan" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="danaBOS" HeaderStyle-CssClass="text-center" HeaderText="Dana BOS" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row text-center" id="button" runat="server">
            <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-primary" Text="Proses" OnClick="btnSubmit_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
            <asp:Button runat="server" ID="btnBatal" CssClass="btn btn-danger" Text="Reset" OnClick="btnBatal_Click"></asp:Button>
        </div>
    </div>
    <div id="tabForm" runat="server" visible="false">
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 text-right">Tgl Beli <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:HiddenField ID="hdnPRD" runat="server" />
                                            <asp:TextBox runat="server" class="form-control date" ID="dtPO"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 text-right">Supplier <span class="mandatory">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" class="form-control" ID="txtIDSup"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hdnNoSupp" />
                                            <asp:HiddenField runat="server" ID="hdnNoSP" />
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:ImageButton ID="imgButtonSup" CssClass="btn-image" runat="server" ImageUrl="~/assets/images/icon_search.png" OnClick="imgButtonSup_Click" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 text-right">Nama PIC </label>
                                        <div class="col-sm-5">
                                            <asp:Label runat="server" ID="lblNama"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 text-right">Alamat  </label>
                                        <div class="col-sm-5">
                                            <asp:Label runat="server" ID="lblAddress" TextMode="MultiLine"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 text-right">Telp </label>
                                        <div class="col-sm-5">
                                            <asp:Label runat="server" ID="lblPhoneFax"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group" id="showPajak" runat="server">
                                        <label class="col-sm-3 control-label">Pajak <span class="mandatory">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboPajak" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="cboPajak_TextChanged">
                                                <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                                <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" id="showPPh" runat="server">
                                        <label class="col-sm-3 control-label">PPh <span class="mandatory">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboPPh" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                                <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Catatan</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtKeterangan" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <asp:GridView ID="grdPRDetil" SkinID="GridView" runat="server" OnRowDataBound="grdPRDetil_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnnoPR" runat="server" />
                                                    <asp:HiddenField ID="hdnParameter" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kode Barang" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="text-center">
                                                            <asp:TextBox runat="server" ID="txtkodebrg" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnAccount" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Barang" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:Label runat="server" ID="lblnamaBarang"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty Besar" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox runat="server" CssClass="form-control money" Enabled="false" Text="0.00" ID="txtQtyBesar" onblur="CalculatePPn(event);"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtsatuanBesar" Enabled="false"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty Kecil" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox runat="server" CssClass="form-control money" Enabled="false" Text="0.00" ID="txtQty" onblur="CalculatePPn(event);"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Total Qty" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox runat="server" CssClass="form-control money" Enabled="false" Text="0.00" ID="txtQtykecil" onblur="CalculatePPn(event);"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtsatuan" Enabled="false"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Harga Satuan" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtHargaSatuan" onblur="CalculatePPn(event);" onchange="CalculatePPh(event);"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Alokasi Budget" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtBudgetPR" Enabled="false"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nilai PO" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txtTotalharga"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 text-right">
                                <div class="form-group">
                                    <label class="col-sm-10 control-label">Sub Total</label>
                                    <div class="col-sm-2">
                                        <asp:TextBox runat="server" ID="txtSubTotal" CssClass="form-control money" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="showDetil" runat="server">
                            <div class="col-sm-12 text-right">
                                <div class="form-group">
                                    <label class="col-sm-10 control-label">PPn <asp:Label ID="lblPersenPPn" runat="server"></asp:Label>%</label>
                                    <div class="col-sm-2">
                                        <asp:TextBox runat="server" ID="txtPPn" CssClass="form-control money" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="showDetil2" runat="server">
                            <div class="col-sm-12 text-right">
                                <div class="form-group">
                                    <label class="col-sm-10 control-label">Total</label>
                                    <div class="col-sm-2">
                                        <asp:TextBox runat="server" ID="txtTotal" CssClass="form-control money" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-md-12">
                                <div class="text-center">
                                    <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="tabDD" runat="server" visible="false">
   
    </div>


    <%-- untuk cari msupiiler --%>
    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgSupplier" runat="server" PopupControlID="panelSupplier" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelSupplier" runat="server" align="center" Width="70%" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content size-sedang">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Supplier Data</h4>
            </div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="modal-body col-overflow-400">
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group ">
                            <div class="form-inline">
                                <label class="col-sm-3 control-label">Cari</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnCari_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="grdSupp" runat="server" SkinID="GridView" AllowPaging="true" PageSize="10" OnSelectedIndexChanged="grdSupp_SelectedIndexChanged" OnPageIndexChanging="grdSupp_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <%# Container.DataItemIndex + 1 %>.
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Supplier ID" >
                                <ItemTemplate>
                                    <asp:Label ID="lblKodeSup" runat="server" Text='<%# Bind("kodeSupplier") %>'></asp:Label>
                                    <asp:HiddenField ID="hidNoSup" runat="server" Value='<%# Eval("noSupplier") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nama Supplier" >
                                <ItemTemplate>
                                    <asp:Label ID="lblNamaSup" runat="server" Text='<%# Bind("namaSupplier") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Alamat" >
                                <ItemTemplate>
                                    <asp:Label ID="lblaAddress" runat="server" Text='<%# Bind("alamat") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No Telp" >
                                <ItemTemplate>
                                    <asp:Label ID="lblNoTel" runat="server" Text='<%# Bind("telpKantor") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ShowHeader="False">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:Button ID="btnSelect" runat="server" CommandName="Select" Text="Pilih" CssClass="btn btn-primary btn-xs" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
