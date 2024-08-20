<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransQuotationView.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TransQuotationView" %>
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
            var GridVwHeaderChckbox = document.getElementById("<%=grdInvoiceGen.ClientID %>");
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
            var tax = document.getElementById("<%=cboTax.ClientID %>").value;
            var GridView = document.getElementById("<%=grdSOD.ClientID %>");
            var subTotal = 0;
                for (var i = 1; i < GridView.rows.length; i++) {
                    if (GridView.rows[i].cells[0].innerHTML.trim() != "No records data") {
                        var qty = GridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value * 1;
                        var harga = removeMoney(GridView.rows[i].cells[6].getElementsByTagName("INPUT")[0].value) * 1;
                        if (qty != 0 || harga != 0)
                        {
                            GridView.rows[i].cells[7].getElementsByTagName("INPUT")[0].value = money(qty * harga)
                        }
                        subTotal += (removeMoney(GridView.rows[i].cells[7].getElementsByTagName("INPUT")[0].value) * 1);

                    }
                }
                if (tax == "1") {
                    var ppn1 = ((subTotal) * 10 / 100) * 1;
                    var Netto = (subTotal + ppn1) * 1;
                    document.getElementById("<%=txtSubTotal.ClientID %>").value = money(subTotal);
                    document.getElementById("<%=txtPPN.ClientID %>").value = money(ppn1);
                    document.getElementById("<%=txtTotal.ClientID %>").value = money(Netto * 1);
                }
                else
                {
                    var ppn1 = 0;
                    var Netto = (subTotal + ppn1) * 1;
                    document.getElementById("<%=txtSubTotal.ClientID %>").value = money(subTotal);
                    document.getElementById("<%=txtPPN.ClientID %>").value = money(ppn1);
                    document.getElementById("<%=txtTotal.ClientID %>").value = money(Netto * 1);
                }
                    
        }

        function CalculateTotal()
        {
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
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch3_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdInvoiceGen" DataKeyNames="noQuotation" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdInvoiceGen_PageIndexChanging"
                    OnSelectedIndexChanged="grdInvoiceGen_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnIdPrint" runat="server" Value='<%# Eval("noQuotation") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                     
                        <asp:BoundField DataField="kdQuotation" HeaderText="Quotation Code" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="tglQuotation" HeaderText="Quotation Date" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                        <asp:BoundField DataField="namaCust" HeaderText="Customer" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
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
                               <div class="col-sm-6">
                        <div class="form-horizontal">
                            <div class="form-group row">
                                <div class="form-inline">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Quotation Date :</label>
                                    <div class="col-sm-4">
                                        <asp:Calendar ID="TglPO" runat="server" OnSelectionChanged="TglPO_SelectionChanged" Visible="false"></asp:Calendar>&nbsp;<asp:TextBox ID="dtPO" runat="server" CssClass="form-control" Width="150" Enabled="false" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Customer Name<span class="mandatory">*</span> :</label>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtIDCust" Enabled="false"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hdnNoCust" />
                                    <asp:HiddenField runat="server" ID="hdnCIF" />
                                </div>
                                
                                <div class="col-sm-5">
                                    <asp:TextBox ID="lblNamaCust" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Address :</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="lblAddress" Width="340" Height="65" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Phone / Fax :</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="lblPhoneFax" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Type :</label>
                                <div class="col-sm-5">
                                    <asp:DropDownList ID="cboType" Enabled="false" class="form-control" runat="server" AutoPostBack="true">
                                        <asp:ListItem Value="1">TRADE</asp:ListItem>
                                        <asp:ListItem Value="2">INDEN IMPORT</asp:ListItem>
                                        <asp:ListItem Value="3">INDEN LOCAL</asp:ListItem>
                                       
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Sales Name :</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="cboSales" Enabled="false" class="form-control" runat="server">
                                        <%--<asp:ListItem Value="1">Sales1</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Delivery Address :</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="txtDeliveryAddress" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            
                             
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-horizontal">
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">TOP :</label>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" Enabled="false" class="form-control" ID="txtTop"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:Label ID="Label2" runat="server" Text="Days"></asp:Label>
                                </div>
                                
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Payment Type :</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="cboPayment" Enabled="false" class="form-control" runat="server">
                                        <asp:ListItem Value="1">Normal</asp:ListItem>
                                        <asp:ListItem Value="2">In Advance</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Transaction type :</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="cboTax" Enabled="false" class="form-control" runat="server">
                                        <asp:ListItem Value="TAX">TAX</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Currency :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cboCurrency" Enabled="false" class="form-control" runat="server" OnSelectedIndexChanged="cboCurrency_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Label ID="Kurstrs" runat="server" CssClass="form-label" Text="kurs IDR"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" Enabled="false" class="form-control money" ID="txtKurs"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kurs Transaction :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cboCurrencyTrans" Enabled="false" class="form-control" runat="server" OnSelectedIndexChanged="cboCurrencyTrans_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Label ID="Kurstrs2" runat="server"  CssClass="form-label" Text="kurs IDR"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" Enabled="false" class="form-control money" Text="0.00" ID="txtkursrate"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Credit Limit :</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="lblkreditlimit" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Remaining Limit :</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" CssClass="form-control money" Text="0.00"  ID="lblsisakreditlimit" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Name PIC :</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="cboPICName" class="form-control" Enabled="false" runat="server" AutoPostBack="true" OnTextChanged="cboPICName_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtEmailCP" Enabled="false" placeholder="Email PIC"></asp:TextBox>
                                </div>
                            </div>
                           <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Remarks :</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="txtRemarks" Width="340" Height="65" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Quotation Validity :</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="dtQuoValidity"></asp:TextBox>
                                </div>
                             </div>
                        </div>
                    </div>
                </div>>
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <asp:GridView ID="grdSOD" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnNoQuotationD" runat="server"/>
                                                    <asp:HiddenField ID="hdnParameter" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Product" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <div class="col-md-12">
                                                    <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtProduct" Width="100"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnProduct" runat="server" />
                                                   
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Product Name" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtProductName" Enabled="false"  Width="170"></asp:TextBox>
                                                <asp:HiddenField ID="hdnHasil" runat="server" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="CIF" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:DropDownList runat="server" ID="ddCIF" Enabled="false" Width="150"></asp:DropDownList>
                                                <asp:TextBox runat="server" CssClass="form-control money" Enabled="false" ID="txtCIFValue" Text="0"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" class="form-control money" Enabled="false" Text="0.00" ID="txtQty"  Width="80" onblur="return Calculate()" onkeyup="return Calculate()"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Unit" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                  <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="lblUnit"   Width="170"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Unit Price" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" Enabled="false" class="form-control money" Text="0.00" ID="txtUnitPrice"  Width="120" onblur="return Calculate()" onkeyup="return Calculate()"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Total" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" CssClass="form-control money" Text="0.000" ID="txtTotal1" Enabled="false" Width="120"></asp:TextBox>
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
                                            <asp:TextBox ID="txtSubTotal" Enabled="false" runat="server" CssClass="form-control money"  Text="0.00" onblur="return CalculateTotal(event);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="form-inline">
                                        <label for="pjs-ex1-fullname" class="col-sm-10 control-label text-right">PPN <asp:Label ID="lblPersenPPn" runat="server" Text='<%# Bind("PersenPPn") %>'></asp:Label>% :</label>
                            <div class="col-sm-2">
                                <asp:TextBox runat="server" CssClass="form-control money" ID="txtPPN" Text="0.00"  Enabled="false"></asp:TextBox>
                            </div>
                                    </div>
                                </div>
                               
                                <div class="form-group">
                                    <div class="form-inline">
                                        <label class="col-sm-10 control-label">Total </label>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtTotal" runat="server" Enabled="false" CssClass="form-control money"  Text="0.00" onblur="return CalculateTotal(event);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-12">
                                <div class="text-center">
                                    <%--<asp:Button runat="server" ID="btnAddRow" CssClass="btn btn-success" Text="Add Row" OnClick="btnAddRow_Click"></asp:Button>--%>
                                    
                                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
 <asp:LinkButton ID="LinkButton4" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgKeterangan" runat="server" PopupControlID="panel2" TargetControlID="LinkButton4" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panel2" runat="server" align="center" Width="60%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Keterangan</h4>
                <asp:HiddenField runat="server" ID="HiddenField1" />
            </div>
            <div class="row">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-5">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" MaxLength="50" placeholder="Search..."></asp:TextBox>
                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-sm" Text="Search"  />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body col-overflow-500">
                <div class="table-responsive">
                    <asp:GridView ID="grdKeterangan" DataKeyNames="noKeterangan" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnRowDataBound="grdKeterangan_RowDataBound" OnPageIndexChanging="grdKeterangan_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField ID="hdnNoProd" runat="server" Value='<%#Eval("noKeterangan")%>' />
                                        <asp:HiddenField ID="hdnStsPilih" runat="server" Value='<%# Eval("stsPilih") %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-left" HeaderText="Keterangan" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblKdProd" Text='<%#Eval("Keterangan")%>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelectSub" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="Button4" runat="server" Text="Submit" CssClass="btn btn-primary" CausesValidation="false" OnClick="Button4_Click" />
            </div>
        </div>
    </asp:Panel>
    <asp:LinkButton ID="LinkButton2" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="panelAddMenu" TargetControlID="LinkButton2" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelAddMenu" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg-full">
        <div class="modal-content size-besar">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Add Product</h4>
            </div>
            <div class="modal-body col-overflow-500">
                <asp:Label runat="server" ID="lblMessageError"></asp:Label>
                <div class="row">
                    <div class="col-sm-7">
                    </div>
                    <div class="col-sm-5">
                        <div class="form-group ">
                            <div class="form-inline">
                                <div class="col-sm-12">
                                    <label>Search :</label>
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:HiddenField ID="txtHdnPopup" runat="server" />
                    <asp:GridView ID="grdProduct" runat="server" SkinID="GridView" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdProduct_PageIndexChanging" OnSelectedIndexChanged="grdProduct_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <%# Container.DataItemIndex + 1 %>.
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Code" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblKodeReagent" runat="server" Text='<%# Bind("KodeBarang") %>'></asp:Label>
                                    <asp:HiddenField ID="hidNoReagent" runat="server" Value='<%# Eval("noBarang") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Name" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblNamaReagent" runat="server" Text='<%# Bind("namaBarang") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Packing" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblPacking" runat="server" Text='<%# Bind("satuan") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="" ShowHeader="False">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:Button ID="btnSelect" runat="server" CommandName="Select" Text="Add" CssClass="btn btn-primary btn-sm" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnCloseMenu" runat="server" Text="Close" CssClass="btn btn-danger btn-sm" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton3" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="panelAlamatKirim" TargetControlID="LinkButton3" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelAlamatKirim" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Delivery Address</h4>
            </div>
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <div class="modal-body ">
                <div class="modal-body col-overflow-400">
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
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton5" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="panel3" TargetControlID="LinkButton5" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panel3" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content" style="width= 600px; height= 900px;">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Add Customer</h4>
            </div>
            <div class="modal-body ">
                <div class="row">
                    <div class="col-sm-7">
                        <%--<asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" Text="Delete / Delete all" OnClick="btnDelete_Click" CausesValidation="false" />--%>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group ">
                            <div class="form-inline">
                                <label class="col-sm-3 control-label">Search</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnCari_Click" CausesValidation="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <asp:GridView ID="grdCustomer" runat="server" SkinID="GridView" AllowPaging="true" PageSize="7" OnSelectedIndexChanged="grdCustomer_SelectedIndexChanged" OnPageIndexChanging="grdCustomer_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <%# Container.DataItemIndex + 1 %>.
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer ID" SortExpression="kodeReagent">
                                <ItemTemplate>
                                    <asp:Label ID="lblKodeReagent" runat="server" Text='<%# Bind("kdCust") %>'></asp:Label>
                                    <asp:HiddenField ID="hidNoReagent" runat="server" Value='<%# Eval("noCust") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hdnNoFax" runat="server" Value='<%# Eval("noTelpCust") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hdnTermCust" runat="server" Value='<%# Eval("termCust") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hdnKreditLimit" runat="server" Value='<%# Eval("KreditLimit") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hdnSisaKreditLimit" runat="server" Value='<%# Eval("SisaKreditLimit") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Name" SortExpression="namaReagent">
                                <ItemTemplate>
                                    <asp:Label ID="lblNamaReagent" runat="server" Text='<%# Bind("namaCust") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address" SortExpression="namaReagent">
                                <ItemTemplate>
                                    <asp:Label ID="lblaAddress" runat="server" Text='<%# Bind("alamatCust") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ShowHeader="False">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:Button ID="btnSelect" runat="server" CommandName="Select" Text="Add" CssClass="btn btn-primary btn-sm" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="Button5" runat="server" Text="Close" CssClass="btn btn-danger btn-sm" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>
    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgSupplier" runat="server" PopupControlID="panelSupplier" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelSupplier" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content size-sedang">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Supplier Data</h4>
            </div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="modal-body ">
                <div class="row">
                    <div class="col-sm-7">
                        <%--<asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" Text="Delete / Delete all" OnClick="btnDelete_Click" CausesValidation="false" />--%>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group ">
                            <div class="form-inline">
                                <label class="col-sm-1 control-label">Search</label>
                                <div class="col-sm-11">
                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control input-sm" type="search"></asp:TextBox>
                                    <asp:Button ID="btnCari1" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnCari1_Click" CausesValidation="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="grdSupp" runat="server" SkinID="GridView" AllowPaging="true" PageSize="7" OnSelectedIndexChanged="grdSupp_SelectedIndexChanged" OnPageIndexChanging="grdSupp_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <%# Container.DataItemIndex + 1 %>.
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer ID" SortExpression="kodeReagent">
                                <ItemTemplate>
                                    <asp:Label ID="lblKodeSup" runat="server" Text='<%# Bind("kdSup") %>'></asp:Label>
                                    <asp:HiddenField ID="hdnNoSupp" runat="server" Value='<%# Eval("noSup") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hdnNoSP" runat="server" Value='<%# Eval("noSP") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hdnNoFax" runat="server" Value='<%# Eval("noTelpSP") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Name" SortExpression="namaReagent">
                                <ItemTemplate>
                                    <asp:Label ID="lblNamaSup" runat="server" Text='<%# Bind("namasup") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address" SortExpression="namaReagent">
                                <ItemTemplate>
                                    <asp:Label ID="lblaAddress" runat="server" Text='<%# Bind("alamatSup") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No Telp" SortExpression="namaReagent">
                                <ItemTemplate>
                                    <asp:Label ID="lblNoTel" runat="server" Text='<%# Bind("noTelpSP") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ShowHeader="False">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:Button ID="btnSelect" runat="server" CommandName="Select" Text="Add" CssClass="btn btn-primary btn-sm" />
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
