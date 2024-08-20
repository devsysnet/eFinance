  <%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransInvoiceNewUpdate.aspx.cs" Inherits="eFinance.Pages.Transaksi.Update.TransInvoiceNewUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function DeleteAll() {
            bootbox.confirm({
                message: "Are you sure to delete data?",
                callback: function (result) {
                    if (result == true) {
                        document.getElementById('<%=hdnMode.ClientID%>').value = "deleteall";
                        eval("<%=execBind%>");
                    }
                },
                className: "bootbox-sm"
            });
            return false;

        }
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdPOImport.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
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
        function Calculate() {
            var Qty = 0, unitPrice = 0, hasil = 0;
            var gridView = document.getElementById("<%=grdSOD.ClientID %>");
            var txtDiscount = removeMoney(document.getElementById("<%=txtDiscountH.ClientID %>").value) * 1;

            for (var i = 1; i < gridView.rows.length; i++) {
                Qty = removeMoney(gridView.rows[i].cells[3].getElementsByTagName("INPUT")[0].value) * 1;
                unitPrice = removeMoney(gridView.rows[i].cells[5].getElementsByTagName("INPUT")[0].value) * 1;
                discount = removeMoney(gridView.rows[i].cells[6].getElementsByTagName("INPUT")[0].value) * 1;

                //gridView.rows[i].cells[6].getElementsByTagName("INPUT")[0].value = money((unitPrice * Qty) / 100);
                gridView.rows[i].cells[7].getElementsByTagName("INPUT")[0].value = money((unitPrice * Qty) - discount);
                hasil += removeMoney(gridView.rows[i].cells[7].getElementsByTagName("INPUT")[0].value);
            }
            document.getElementById("BodyContent_txtSubTotal").value = money(hasil);
            CalculateTotal();
        }

        function CalculateTotal()
        {
            var txtDiscount = removeMoney(document.getElementById("<%=txtDiscountH.ClientID %>").value) * 1;
            var SubTotal = removeMoney(document.getElementById("BodyContent_txtSubTotal").value);
            var PPN = Math.round((SubTotal - txtDiscount) * (10 / 100));

            var TotalAmount = SubTotal + PPN + txtDiscount;
            document.getElementById("BodyContent_txtTotal").value = money(TotalAmount);
            document.getElementById("BodyContent_txtPPN").value = money(PPN);

        }

    </script>
    <asp:Button ID="cmdMode" runat="server" OnClick="cmdMode_Click" Style="display: none;" Text="Mode" />
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="col-sm-3">
                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" OnClientClick="return DeleteAll()" Text="Delete all selected" />

            </div>
            <div class="col-sm-5">
                <div class="form-inline">
                    <label>Start : </label>
                    <asp:TextBox ID="dtSearchStart" runat="server" CssClass="form-control date"></asp:TextBox>
                    <label>End : </label>
                    <asp:TextBox ID="dtSearchEnd" runat="server" CssClass="form-control date"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-4">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Search : </label>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search.."></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdPOImport" DataKeyNames="noInv" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPOImport_PageIndexChanging"
                    OnSelectedIndexChanged="grdPOImport_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnIdPrint" runat="server" Value='<%# Eval("noInv") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                            <HeaderTemplate>
                                <div class="text-center">
                                    <asp:CheckBox ID="chkCheckAll" runat="server" CssClass="px" onclick="return CheckAllData(this);" />
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:CheckBox ID="chkCheck" runat="server" CssClass="px chkCheck" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="kdInv" HeaderText="Invoice Code" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="tglInv" HeaderText="Invoice Date" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                        <asp:BoundField DataField="namaCust" HeaderText="Customer Name" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="alamatCust" HeaderText="Customer Address" HeaderStyle-CssClass="text-center" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
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

    <div id="tabForm" runat="server" visible="false">
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-4 control-label">Kode Invoice <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox runat="server" ID="TextBox1" Enabled="false" CssClass="form-control"></asp:TextBox>
                                            </div>
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
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtCustomerPOref" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-4 control-label">TOP <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox runat="server" CssClass="form-control numeric" Width="50" Text="0" ID="txtTOP" Enabled="false"></asp:TextBox>&nbsp;Days
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Remarks </label>
                                        <div class="col-sm-7">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtRemarks" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <asp:GridView ID="grdSOD" SkinID="GridView" runat="server">
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
                                                            <%--<asp:ImageButton ID="btnImgProduct" CssClass="btn-image form-control" runat="server" ImageUrl="~/assets/images/icon_search.png" CommandName="select" CausesValidation="false" />--%>
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
                                        <%-- <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Estimasi Date" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" class="form-control date" Width="60" ID="dtEstimasi"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Expired Date" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cboExpiredDate" runat="server"></asp:DropDownList>
                                                    <asp:CheckBox ID="chkDefault" runat="server" Text="Default" OnCheckedChanged="chkDefault_CheckedChanged" AutoPostBack="true" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty" ItemStyle-Width="6%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txtQty" onblur="return Calculate(event);" Enabled="false"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Packing" ItemStyle-Width="6%">
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
                                                    <%--<asp:HiddenField ID="hdnnoconfD" runat="server" />--%>
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
                                        <label class="col-sm-10 control-label">PPN (10%) </label>
                                        <div class="col-sm-2">
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
                                    <%--<asp:Button runat="server" ID="btnAddRow" CssClass="btn btn-success" Text="Add Row" OnClick="btnAddRow_Click"></asp:Button>--%>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
