<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransInvoiceNew.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransInvoiceNew" %>
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
            var Qty = 0, unitPrice = 0, hasil = 0;
            var gridView = document.getElementById("<%=grdSOD.ClientID %>");

            for (var i = 1; i < gridView.rows.length; i++) {
                Qty = removeMoney(gridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value) * 1;
                unitPrice = removeMoney(gridView.rows[i].cells[6].getElementsByTagName("INPUT")[0].value) * 1;
                discount = removeMoney(gridView.rows[i].cells[7].getElementsByTagName("INPUT")[0].value) * 1;

                //gridView.rows[i].cells[6].getElementsByTagName("INPUT")[0].value = money((unitPrice * Qty) / 100);
                gridView.rows[i].cells[8].getElementsByTagName("INPUT")[0].value = money((unitPrice * Qty) - discount);
                hasil += removeMoney(gridView.rows[i].cells[8].getElementsByTagName("INPUT")[0].value);
            }
            document.getElementById("BodyContent_txtSubTotal").value = money(hasil);
            CalculateTotal();

        }

        function CalculateTotal()
        {
            var tax = document.getElementById("<%=cboTax.ClientID %>").value;
            var ppn1 = document.getElementById("<%=persenppn.ClientID %>").value;

            if (tax == "1") {
                var txtDiscount = removeMoney(document.getElementById("<%=txtDiscountH.ClientID %>").value) * 1;
                var SubTotal = removeMoney(document.getElementById("BodyContent_txtSubTotal").value);
                var PPN = Math.round((SubTotal - txtDiscount) * (ppn1 / 100));

                var TotalAmount = SubTotal + PPN + txtDiscount;
                document.getElementById("BodyContent_txtTotal").value = money(TotalAmount);
                document.getElementById("BodyContent_txtPPN").value = money(PPN);

            }
            else
            {
                var txtDiscount = removeMoney(document.getElementById("<%=txtDiscountH.ClientID %>").value) * 1;
                var SubTotal = removeMoney(document.getElementById("BodyContent_txtSubTotal").value);
                var PPN =0;

                var TotalAmount = SubTotal + PPN + txtDiscount;
                document.getElementById("BodyContent_txtTotal").value = money(TotalAmount);
                document.getElementById("BodyContent_txtPPN").value = money(PPN);
            }

        }

        ///// untuk "tipe" = menunjukan perbedaan pengiriman no index yang dikirim
        ///// untuk "jenisQuery" = perbedaan sql yang akan dipakai pada storeProcedure yang telah dibuat

        //Popup untuk Supplier
        var popup;
        function SelectCus() {
            popup = window.open("<%=Func.BaseUrl%>Pages/Popup/PopupCustomer.aspx?tipe=5", "Popup", "width=1200,height=500");
            popup.focus();
            return false
        }
    </script>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnPersenPPn" runat="server" />
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="col-sm-8">
                <%--<asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" OnClientClick="return DeleteAll()" Text="Delete all selected" />--%>
            </div>
            <div class="col-sm-4">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Search : </label>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" ></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdSO" DataKeyNames="noQoutation" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdSO_PageIndexChanging"
                   OnRowCommand="grdSO_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField runat="server" ID="hdnCust" Value='<%#Eval("noCust") %>' />
                                    <asp:HiddenField runat="server" ID="hdnKdCust" Value='<%#Eval("kdCust") %>' />
                                    <asp:HiddenField runat="server" ID="hdnalamatCust" Value='<%#Eval("alamatCust") %>' />
                                    <asp:HiddenField runat="server" ID="hdnTelpCust" Value='<%#Eval("noTelpCust") %>' />
                                    <asp:HiddenField runat="server" ID="hdnKreditLimit" Value='<%#Eval("kreditlimit") %>' />
                                    <asp:HiddenField runat="server" ID="hdnQoutationId" Value='<%#Eval("kdQoutation") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="kdQoutation" HeaderStyle-CssClass="text-center" HeaderText="Quotation Code" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="namaCust" HeaderStyle-CssClass="text-center" HeaderText="Customer Name" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="alamatCust" HeaderStyle-CssClass="text-center" HeaderText="Customer Address" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Select" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnClose" runat="server" class="btn btn-primary btn-sm" Text="Closing" CommandName="detail" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'/>
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
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-6">
                                    <div class="form-group row">
                                        <label class="col-sm-4 control-label">Kode :</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cbokode" OnSelectedIndexChanged="cbokode_SelectedIndexChanged" AutoPostBack="true" Width="100">
                                                <asp:ListItem Value="1">New</asp:ListItem>
                                                <asp:ListItem Value="2">Old</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cbolistkodeold" Width="150">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Invoice date <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control date" ID="dtSO"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-4 control-label">Customer <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox runat="server" ID="txtnamacust" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                <asp:HiddenField ID="hdnCustomer" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Address </label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" ID="txtAddress" Enabled="false" CssClass="form-control" Width="340" Height="65" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Phone </label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" ID="txtPhone" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Sales Name </label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" ID="txtSales" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-4 control-label">Delivery </label>
                                            <div class="col-sm-6">
                                                <asp:TextBox runat="server" ID="txtDelivery" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                <asp:HiddenField ID="hdnDelivery" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Delivery Address </label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" ID="txtDeliveryAddress" Enabled="false" CssClass="form-control" Width="340" Height="65" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Customer PO ref no </label>
                                        <div class="col-sm-5">
                                            <asp:textbox runat="server" CssClass="form-control" ID="txtCustomerPOref" Enabled="false"></asp:textbox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-4 control-label text-right">TOP <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox runat="server" CssClass="form-control numeric" Width="50" Text="0" ID="txtTOP" Enabled=false></asp:TextBox>&nbsp;Days
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label text-right">Tax Status :</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cboTax" OnSelectedIndexChanged="cboTax_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label text-right">Tax No :</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cboTaxNo" OnSelectedIndexChanged="cboTaxNo_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="1">New</asp:ListItem>
                                                <asp:ListItem Value="2">Old</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cbolistkodetaxnew">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cbolistkodetaxold">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label text-right">Persen Pajak:</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control numeric" Width="50" Text="0" ID="persenppn" onblur="return Calculate(event);" Enabled=true></asp:TextBox>&nbsp;
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label text-right">Remarks </label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtRemarks" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label text-right">Akan Digabung </label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cbogabung" OnSelectedIndexChanged="cboTaxNo_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Tidak</asp:ListItem>
                                                <asp:ListItem Value="1">Ya</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                 
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <asp:GridView ID="grdSOD" SkinID="GridView" runat="server" OnSelectedIndexChanged="grdSOD_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnUnit" runat="server" />
                                                    <asp:HiddenField ID="hdnParameter" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Product" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="text-center">
                                                            <asp:TextBox runat="server" ID="txtProduct" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnProduct" runat="server" Value="0" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Product Name" ItemStyle-Width="25%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" ID="txtProductName" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Packing" ItemStyle-Width="6%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label ID="lblPacking" runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty" ItemStyle-Width="6%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txtQty" onblur="return Calculate(event);" Enabled="false"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Unit" ItemStyle-Width="6%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label ID="lblUnit" runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Unit Price" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                           <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txtUnitPrice" onblur="return Calculate(event);" Enabled="false"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Discount" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txtDiscount" onblur="return Calculate(event);" Enabled="false"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Total" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:TextBox runat="server" ID="txtTotal" Enabled="false" CssClass="form-control money" Text="0.00" onblur="return Calculate(event);"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="form-inline">
                                        <label class="col-sm-10 control-label">Sub Total </label>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtSubTotal" runat="server" CssClass="form-control money" Width="157" Text="0.00" onblur="return CalculateTotal(event);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="form-inline">
                                        <label class="col-sm-10 control-label">Discount </label>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtDiscountH" runat="server" CssClass="form-control money" Width="157" Text="0.00" onblur="return CalculateTotal(event);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="form-inline">
                                        <label class="col-sm-10 control-label">PPN <asp:Label ID="lblPersenPPn" runat="server" Text='<%# Bind("PersenPPn") %>'></asp:Label>% </label>
                                        <div class="col-sm-2">
                                            <%--<asp:TextBox ID="txtPPN" runat="server" CssClass="form-control money" Width="157" Text="0.00" onblur="return CalculateTotal(event);"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtPPN" runat="server" CssClass="form-control money" Width="157" Text="0.00" onblur="return CalculateTotal(event);"></asp:TextBox>
                                          
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="form-inline">
                                        <label class="col-sm-10 control-label">Total </label>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtTotal" runat="server" Enabled="false" CssClass="form-control money" Width="157" Text="0.00" onblur="return CalculateTotal(event);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-12">
                                <div class="text-center">
                                    <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click"></asp:Button>
                                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgCustomer" runat="server" PopupControlID="panelCustomer" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelCustomer" runat="server" align="center" Width="60%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Product</h4>
            </div>
            <div class="row">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-5">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtSearchCust" runat="server" CssClass="form-control" MaxLength="50" ></asp:TextBox>
                            <asp:Button ID="btnSearchCust" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearchCust_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body col-overflow-500">
                <div class="table-responsive">
                    <asp:GridView ID="grdCustomer" DataKeyNames="noCust" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdCustomer_PageIndexChanging" OnSelectedIndexChanged="grdCustomer_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField runat="server" ID="hdnTelp" Value='<%#Eval("noTelpCust") %>' />
                                        <asp:HiddenField runat="server" ID="hdnKreditLimit" Value='<%#Eval("kreditlimit") %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="kdCust" SortExpression="kdCust" HeaderText="Customer Code" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="namaCust" SortExpression="namaCust" HeaderText="Customer Name" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="alamatCust" SortExpression="alamatCust" HeaderText="Customer Address" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Select" CommandName="Select" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnBatal" runat="server" Text="Close" CssClass="btn btn-danger" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>
    <asp:LinkButton ID="LinkButton2" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgProduct" runat="server" PopupControlID="panelProduct" TargetControlID="LinkButton2" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelProduct" runat="server" align="center" Width="60%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Product</h4>
                <asp:HiddenField runat="server" ID="hdnParameterProd" />
            </div>
            <div class="row">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-5">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtSearchProduct" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                            <asp:Button ID="btnSearchProduct" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearchProduct_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body col-overflow-500">
                <div class="table-responsive">
                    <asp:GridView ID="grdProduct" DataKeyNames="noproduct" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdProduct_PageIndexChanging" OnSelectedIndexChanged="grdProduct_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField ID="hdnNoProd" runat="server" Value='<%#Eval("noproduct")%>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-left" HeaderText="Product Code" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblKdProd" Text='<%#Eval("prodno")%>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-left" HeaderText="Product Name" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblNamaProd" Text='<%#Eval("prodnm")%>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelectSub" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Select" CausesValidation="false" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="Button2" runat="server" Text="Close" CssClass="btn btn-danger" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
