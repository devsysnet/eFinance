<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransSOQuotation.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransSOQuotation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdSOD.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
            Calculate();
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
            var hasil = 0;
            var gridView = document.getElementById("<%=grdSOD.ClientID %>");
            for (var i = 1; i < gridView.rows.length; i++) {

                if (gridView.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked == true) {
                    Qty = removeMoney(gridView.rows[i].cells[7].getElementsByTagName("INPUT")[0].value) * 1;
                    unitPrice = removeMoney(gridView.rows[i].cells[9].getElementsByTagName("INPUT")[0].value) * 1;

                    gridView.rows[i].cells[10].getElementsByTagName("INPUT")[0].value = money((unitPrice * Qty));
                    hasil += removeMoney(gridView.rows[i].cells[10].getElementsByTagName("INPUT")[0].value);

                }
            }
            var SubTotal = hasil;
            var PPN = (SubTotal * (10 / 100));
            var TotalAmount = (SubTotal + PPN);
            document.getElementById("<%=txtSubTotal.ClientID %>").value = money(hasil);
            document.getElementById("<%=txtPPN.ClientID %>").value = money(PPN);
            document.getElementById("<%=txtTotalx.ClientID %>").value = money(TotalAmount);
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="tabGrid" runat="server">
                <div class="row">
                    <div class="col-sm-8">
                        <%--<asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" OnClientClick="return DeleteAll()" Text="Delete all selected" />--%>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-inline">
                            <div class="col-sm-12">
                                <label>Search : </label>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 overflow-x-table">
                        <asp:GridView ID="grdSO" DataKeyNames="noQuotation" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdSO_PageIndexChanging"
                            OnSelectedIndexChanged="grdSO_SelectedIndexChanged" OnRowCommand="grdSO_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:HiddenField runat="server" ID="hdnCust" Value='<%#Eval("noCust") %>' />
                                            <asp:HiddenField runat="server" ID="hdnKdCust" Value='<%#Eval("kdCust") %>' />
                                            <asp:HiddenField runat="server" ID="hdnTelpCust" Value='<%#Eval("noTelpCust") %>' />
                                            <asp:HiddenField runat="server" ID="hdnKreditLimit" Value='<%#Eval("kreditlimit") %>' />
                                            <asp:HiddenField runat="server" ID="hdnQoutationId" Value='<%#Eval("kdQuotation") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="kdQuotation" SortExpression="kdQuotation" HeaderText="Quotation Code" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="namaCust" SortExpression="namaCust" HeaderText="Customer Name" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="alamatCust" SortExpression="alamatCust" HeaderText="Customer Address" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Select" CommandName="Select" />
                                            <asp:Button ID="btnClose" runat="server" class="btn btn-primary btn-sm" Text="Closing" CommandName="detail" />
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
                                                <div class="form-inline">
                                                    <label class="col-sm-4 control-label">SO date <span class="mandatory">*</span></label>
                                                    <div class="col-sm-5">
                                                        <asp:Calendar ID="TglSO" runat="server" OnSelectionChanged="TglSO_SelectionChanged" Visible="false"></asp:Calendar>
                                                        &nbsp;<asp:TextBox ID="dtSO" runat="server" CssClass="form-control" Width="150" Enabled="false"></asp:TextBox>
                                                        <asp:LinkButton ID="lnkPickDate" OnClick="lnkPickDate_Click" runat="server" CausesValidation="false"><img src="../../../assets/images/calendar-icon.gif" /></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <label class="col-sm-4 control-label">Customer <span class="mandatory">*</span></label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox runat="server" ID="txtCustomer" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                        <asp:HiddenField ID="hdnCustomer" runat="server" />
                                                        <asp:ImageButton ID="btnImgCustomer" Visible="false" ImageUrl="~/assets/images/icon_search.png" runat="server" CssClass="btn-image form-control" OnClick="btnImgCustomer_Click" />
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
                                                    <asp:ImageButton ID="btnBrowseAlamatKirim" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image " OnClick="btnBrowseAlamatKirim_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">Customer PO ref no <span class="mandatory">*</span> </label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtCustomerPOref"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">Document PO Upload</label>
                                                <div class="col-sm-5">
                                                    <asp:FileUpload ID="flUpload" runat="server" CssClass="fileupload" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">PO date <span class="mandatory">*</span></label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" CssClass="form-control date" ID="dtPO"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <label class="col-sm-4 control-label">TOP <span class="mandatory">*</span></label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox runat="server" CssClass="form-control numeric" Width="50" Text="0" ID="txtTOP"></asp:TextBox>&nbsp;Days
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <label class="col-sm-4 control-label">Name PIC <span class="mandatory">*</span></label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="cboPICName" class="form-control"  runat="server" AutoPostBack="true" OnTextChanged="cboPICName_TextChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtEmailCP" Enabled="false" placeholder="Email PIC"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">Remarks </label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtRemarks" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">Credit Limit <span class="mandatory">*</span></label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" Enabled="false" ID="txtCreditLimit"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">Balance Receivable <span class="mandatory">*</span></label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" ID="txtBalance" CssClass="form-control money" Text="0.00" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">Remaining Limit <span class="mandatory">*</span></label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" ID="txtRemaining" CssClass="form-control money" Text="0.00" Enabled="false"></asp:TextBox>
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
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="" ItemStyle-Width="1%">
                                                    <HeaderTemplate>
                                                        <div class="text-center">
                                                            <asp:CheckBox ID="chkCheckAll" runat="server" CssClass="px" onclick="return CheckAllData(this);" />
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:CheckBox ID="chkSOD" runat="server" CssClass="px chkCheck" onclick="return Calculate(this);" />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Product" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <div class="form-group">
                                                            <div class="form-inline">
                                                                <div class="text-center">
                                                                    <asp:TextBox runat="server" ID="txtProduct" Width="120" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnProduct" runat="server" Value="0" />
                                                                    <asp:HiddenField ID="hdnleadtime" runat="server" />
                                                                    <asp:ImageButton ID="btnImgProduct" Visible="false" CssClass="btn-image form-control" runat="server" ImageUrl="~/assets/images/icon_search.png" CommandName="select" CausesValidation="false" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Product Name" ItemStyle-Width="15%">
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
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Estimasi Date" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:TextBox runat="server" CssClass="form-control date" Width="60" ID="dtEstimasi"></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Expired Date" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:DropDownList ID="cboExpiredDate" runat="server"></asp:DropDownList>
                                                            <asp:CheckBox ID="chkDefault" runat="server" Text="Default" OnCheckedChanged="chkDefault_CheckedChanged" AutoPostBack="true" />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty" ItemStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:TextBox runat="server" Width="50" CssClass="form-control money" MaxLength="2" Text="0.00" ID="txtQty" onblur="return Calculate(event);" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Unit" ItemStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:Label ID="lblUnit" CssClass="CssClass" runat="server"></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Unit Price" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:TextBox runat="server" Width="95" CssClass="form-control money" Text="0.00" ID="txtUnitPrice" onblur="return Calculate(event);" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Discount" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" Width="95" class="form-control money" Text="0.00" ID="txtDiscount" onblur="return Calculate(event);" Enabled="false"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Total" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <div class="text-right">
                                                            <asp:TextBox runat="server" Width="100" ID="txtTotal" Enabled="false" CssClass="form-control money" Text="0.00" onblur="return Calculate(event);"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnQouationD" runat="server" />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <label class="col-sm-10 control-label">Sub Total </label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txtSubTotal" runat="server" Enabled="false" CssClass="form-control money" Width="157"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <label class="col-sm-10 control-label">PPN (10%) </label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txtPPN" runat="server" Enabled="false" CssClass="form-control money" Width="157"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <label class="col-sm-10 control-label">Total </label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txtTotalx" runat="server" Enabled="false" CssClass="form-control money" Width="157"></asp:TextBox>
                                                    </div>
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
                                    <asp:TextBox ID="txtSearchCust" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
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
            <asp:LinkButton ID="LinkButton3" runat="server" Style="display: none"></asp:LinkButton>
            <asp:ModalPopupExtender ID="dlgDelivery" runat="server" PopupControlID="panelDelivery" TargetControlID="LinkButton3" BackgroundCssClass="modal-background">
            </asp:ModalPopupExtender>
            <asp:Panel ID="panelDelivery" runat="server" align="center" Width="60%" Style="display: none" CssClass="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Delivery Address</h4>
                    </div>
                    <div class="modal-body col-overflow-500">
                        <div class="table-responsive">
                            <asp:GridView ID="grdDataAlamatKirim" SkinID="GridView" runat="server" OnSelectedIndexChanged="grdDataAlamatKirim_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField DataField="alamat" HeaderStyle-CssClass="text-center" HeaderText="Delivery Address" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Action" ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-primary btn-sm" Text="Select" CausesValidation="false" CommandName="Select" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSimpan" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
