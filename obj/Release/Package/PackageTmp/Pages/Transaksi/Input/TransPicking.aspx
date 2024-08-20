<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransPicking.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransPicking" %>
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

        function Calculate() {

            var GridView = document.getElementById("<%=grdPicking1.ClientID %>");
            var subTotal = 0;
            for (var i = 1; i < GridView.rows.length; i++) {
                if (GridView.rows[i].cells[0].getElementsByTagName("INPUT")[1].checked == true) {
                    subTotal += (removeMoney(GridView.rows[i].cells[8].getElementsByTagName("INPUT")[0].value) * 1);
                }
            }
            document.getElementById("<%=txtTotalQty.ClientID %>").value = money(subTotal * 1);

        }

    </script>
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    
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
                                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-primary" Text="Search" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdPOLocal" DataKeyNames="noSO" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPOLocal_PageIndexChanging" 
                                        OnSelectedIndexChanged="grdPOLocal_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                        
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="kdSO" SortExpression="kdPO" HeaderText="Kode SO" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="tglSO" SortExpression="namaSup" HeaderText="Tanggal SO" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="kdCust" SortExpression="namaSup" HeaderText="Kode Customer" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="namaCust" SortExpression="tglPO" HeaderText="Nama Customer" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="text-center" />
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="Gudang">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:DropDownList ID="cboSlcGudang" class="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Detail" CommandName="Select" />
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
                        <div class="row" style="margin-top: 10px;">
                            <div class="col-sm-6">
                                <div class="form-horizontal">
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">No SO :</label>
                                        <div class="col-sm-4">
                                            <asp:Label ID="lblNoSO" runat="server" CssClass="form-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tanggal SO :</label>
                                        <div class="col-sm-4">
                                            <asp:Label ID="lblTglSO" runat="server" CssClass="form-label"></asp:Label>
                                        </div>
                                    </div>
                                            <asp:Label Visible="false" ID="lblPOCust" runat="server" CssClass="form-label"></asp:Label>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kode Customer :</label>
                                        <div class="col-sm-4">
                                            <asp:HiddenField ID="hdnNoCust" runat="server" />
                                            <asp:Label ID="lblKdCust" runat="server" CssClass="form-label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Customer :</label>
                                        <div class="col-sm-5">
                                            <asp:Label ID="lblNamaCust" runat="server" CssClass="form-label"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-horizontal">
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tanggal Picking :</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" class="form-control date" ID="dtPicking"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Penanggung Jawab :</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboGudang" class="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Penerima :</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboGudang1" class="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Keterangan :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtKeterangan" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                  
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="grdPO" DataKeyNames="noproduct,noDetSO" SkinID="GridView" runat="server" AllowPaging="true" OnRowCommand="grdPO_RowCommand" OnRowDataBound="grdPO_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <%# Container.DataItemIndex + 1 %>.
                                                <asp:HiddenField ID="txtHdnValue" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                                <asp:HiddenField ID="txtHdnValue1" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kode Barang" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:Label runat="server" class="form-label" ID="lblProduct" Text='<%# Bind("kodeBarang") %>'></asp:Label>
                                                <asp:HiddenField ID="hdnDetSO" runat="server" Value='<%# Bind("noDetSO") %>' />
                                                <asp:HiddenField ID="hdnNoProduct" runat="server" Value='<%# Bind("noProduct") %>' />
                                                <asp:HiddenField ID="hdnKonfersiBesar" runat="server" Value='<%# Bind("konfersiBesar") %>' />
                                                <asp:HiddenField ID="hdnKonfersi1" runat="server" Value='<%# Bind("konfersi1") %>' />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText=" Nama Barang" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label runat="server" CssClass="form-label" ID="lblProductName" Text='<%# Bind("namaBarang") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                             <div class="text-center">
                                              <asp:TextBox runat="server" CssClass="form-control int" ID="lblManufacture" Width="100" Text='<%# Bind("qty", "{0:#,0.00}") %>'></asp:TextBox>
                                                  </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:HiddenField ID="hdnPack" runat="server" Value='<%# Bind("packingx") %>' />
                                                <asp:Label runat="server" CssClass="form-label" ID="lblPacking" Text='<%# Bind("satuan") %>'></asp:Label>
                                               
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty " ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" CssClass="form-control int" ID="lblQtySO" Width="100" Text='<%# Bind("qtySatuanBesar", "{0:#,0.00}") %>'></asp:TextBox>
                                                <asp:HiddenField ID="hdnNoGudang" runat="server" value='<%# Bind("noGudang") %>'/>
                                            <asp:HiddenField ID="hdnNoLok" runat="server" value='<%# Bind("noLokasiGudang") %>'/>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan Besar" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:Label runat="server" CssClass="form-label" ID="lblSisaQty" Text='<%# Bind("satuanbesar") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                             <div class="text-center">
                                              <asp:TextBox runat="server" CssClass="form-control int" ID="lblqtySatuanBesar1" Width="100" Text='<%# Bind("qtySatuanBesar1", "{0:#,0.00}") %>'></asp:TextBox>
                                                  </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan Besar1" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:Label runat="server" CssClass="form-label" ID="lblSisaQtysatuanBesar1" Text='<%# Bind("satuanbesar1") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderStyle-CssClass="text-center" HeaderText="Qty Pick" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            
                                            <div class="text-center">
                                                <asp:TextBox runat="server" CssClass="form-control int" ID="txtQtyPick" Width="50"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false"  HeaderStyle-CssClass="text-center" HeaderText="Detail" ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <div class="text-left">
                                                <asp:Button ID="btnDetail" runat="server" CssClass="btn btn-primary" Text="detail"  CommandName="detail" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="" ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:CheckBox ID="chkSelectH" runat="server" CssClass="px chkCheck" Enabled="false" />
                                                <asp:HiddenField ID="hdnNoSAH" runat="server" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-12">
                                <div class="text-center">
                                    <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabFormm" runat="server" visible="false">
                        <div class="row" style="margin-top: 10px;">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgDetil" runat="server" PopupControlID="panelDetil" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelDetil" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">Detil</h4>
            </div>
            <div class="modal-body col-overflow-400">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                <asp:HiddenField ID="hdnRowIndex" runat="server" />
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Product :</label>
                                    <div class="col-sm-8">
                                        <asp:Label ID="lblProd" runat="server" CssClass="form-label"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Manufature :</label>
                                    <div class="col-sm-8">
                                        <asp:Label ID="lblManufature1" runat="server" CssClass="form-label"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kemasan :</label>
                                    <div class="col-sm-8">
                                        <asp:Label ID="lblkemasan" runat="server" CssClass="form-label"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Gudang:</label>
                                    <div class="col-sm-8">
                                        <asp:Label ID="lblGudang" runat="server" CssClass="form-label"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Total Qty:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtTotalQty" runat="server" CssClass="form-control" Width="100" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>                                     
                            </div>
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="grdPicking1" SkinID="GridView" runat="server" AllowPaging="false">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="" ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:HiddenField ID="hdnStsPilih" runat="server" Value='<%# Eval("stsPilih") %>' />
                                                <asp:CheckBox ID="chkSelect" runat="server" CssClass="px chkCheck" onClick="Calculate()" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <%# Container.DataItemIndex + 1 %>.
                                                <asp:HiddenField ID="txtHdnValue" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Product Location" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <div class="text-left">
                                                <asp:Label runat="server" class="form-label" ID="lblProductLocation" Text='<%# Bind("LokGud") %>'></asp:Label>
                                                <asp:HiddenField ID="hdnNoGudang" runat="server" Value='<%# Bind("noGudang") %>' />
                                                <asp:HiddenField ID="hdnNoLok" runat="server" Value='<%# Bind("noLokasiGudang") %>' />
                                                <asp:HiddenField ID="hdnNoSA" runat="server" Value='<%# Bind("noSA") %>' />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Batch" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:Label runat="server" CssClass="form-label" ID="lblBatch" Text='<%# Bind("lotno") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Wadah" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:Label runat="server" CssClass="form-label" ID="lblwadah" Text='<%# Bind("wadahNo") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="SO Date" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:Label runat="server" CssClass="form-label" ID="lblProduction" Text='<%# Bind("tglSO", "{0:dd MMM yyyy}") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Expired" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:Label runat="server" CssClass="form-label" ID="lblExpired" Text='<%# Bind("EXPIRED", "{0:dd MMM yyyy}") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Stock" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:Label runat="server" CssClass="form-label" ID="lblStock" Text='<%# Bind("qtyTersedia") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty Pick" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtQtyPick" Text='<%# Bind("qty4") %>'></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-12">
                                <div class="text-center">
                                    <asp:Button runat="server" ID="btnSend" CssClass="btn btn-success" Text="Send" OnClick="btnSend_Click"></asp:Button>
                                    <asp:Button ID="btnKembali" runat="server" Text="Close" CssClass="btn btn-danger" OnClick="btnKembali_Click" />
                                </div>
                            </div>
                        </div>
                </div>
        </div>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
