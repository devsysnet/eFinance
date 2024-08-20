<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransInvoiceGenView.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TransInvoiceGenView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
        <script type="text/javascript">
        function OpenReport() {
            //            OpenReportViewer();
            window.open('../../../Report/ReportViewer.aspx', 'name', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,modal=yes,width=1000,height=600');
        }
    </script>
    <script type="text/javascript">
        function DeleteAll() {
            bootbox.confirm({
                message: "Are you sure to delete data?",
                callback: function (result) {
                    if (result == true) {
                        document.getElementById('<%=hdnMode.ClientID%>').value = "deleteall";
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
                        var qty = GridView.rows[i].cells[2].getElementsByTagName("INPUT")[0].value * 1;
                        var harga = removeMoney(GridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value) * 1;
                        if (qty != 0 || harga != 0)
                        {
                            GridView.rows[i].cells[5].getElementsByTagName("INPUT")[0].value = money(qty * harga)
                        }
                        subTotal += (removeMoney(GridView.rows[i].cells[5].getElementsByTagName("INPUT")[0].value) * 1);

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
                        <div class="col-sm-3">

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
                <asp:GridView ID="grdInvoiceGen" DataKeyNames="noInvoicegen" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdInvoiceGen_PageIndexChanging"
                    OnSelectedIndexChanged="grdInvoiceGen_SelectedIndexChanged" OnRowCommand="grdInvoiceGen_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnIdPrint" runat="server" Value='<%# Eval("noInvoicegen") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:BoundField DataField="kdInvoice" HeaderText="Invoice Code" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="tglInvoice" HeaderText="Invoice Date" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                        <asp:BoundField DataField="namaCust" HeaderText="Customer" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="alamatCust" HeaderText="Customer Address" HeaderStyle-CssClass="text-center" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Detail" CommandName="Select" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-success btn-sm" Text="Print" CommandName="detail" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'/>
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
                                                <asp:TextBox runat="server" ID="TxtKode" Enabled="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Invoice date <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="dtInv"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-4 control-label">Customer <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:HiddenField runat="server" ID="hdnNoCustomer" />
                                                 <asp:TextBox ID="txtNamaPelanggan" Enabled="false" TextMode="MultiLine" runat="server" CssClass="form-control col-sm-5" Width="200" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Address </label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" ID="txtAlamatPelanggan" Enabled="false" CssClass="form-control" Width="340" Height="65" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Sales Name </label>
                                        <div class="col-sm-5">
                                             <asp:DropDownList ID="cboSales" Enabled="false" class="form-control" runat="server">
                                    </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                               <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Bank</label>
                                  <div class="col-sm-3">
                                           <asp:TextBox ID="txtBankName" Enabled="false"  runat="server" CssClass="form-control col-sm-5" Width="200" />
                                  </div>
                            </div>
                             <div class="form-group">
                                  <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">No Rek</label>
                                       <div class="col-sm-3">
                                           <asp:TextBox ID="txtNoRek" runat="server" CssClass="form-control" Enabled="false"  ></asp:TextBox>
                                       </div>
                               </div>
                                </div>
                                <div class="col-sm-6">
<div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">TOP<span class="mandatory">*</span> :</label>
                                <div class="col-sm-2">
                                     <asp:TextBox runat="server" Enabled="false" class="form-control" ID="txtTop"></asp:TextBox>
                                </div>
                                Days
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Currency<span class="mandatory">*</span> :</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="cboCurrency" Enabled="false" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboCurrency_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:Label ID="Kurstrs" runat="server"  CssClass="form-label" Text="kurs"></asp:Label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" Enabled="false" class="form-control" ID="txtKurs"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">PO No :</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" Enabled="false" class="form-control" ID="txtPOno"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Ref No :</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" Enabled="false" class="form-control" ID="txtNoref"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tax Status :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="cboTax" OnSelectedIndexChanged="cboTax_SelectedIndexChanged"  AutoPostBack="true">
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="2">No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tax No :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="cboTaxNo" OnSelectedIndexChanged="cboTaxNo_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1">New</asp:ListItem>
                                        <asp:ListItem Value="2">Old</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="cbolistkodetaxnew">
                                    </asp:DropDownList>
                                </div>
                               
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Remarks :</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="txtRemarks" Width="340" Height="65" TextMode="MultiLine"></asp:TextBox>
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
                                                    <asp:HiddenField ID="hdnNoInvoicegenD" runat="server"/>
                                                    <asp:HiddenField ID="hdnParameter" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Product Name" ItemStyle-Width="25%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtProductName" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-CssClass="text-center"  HeaderText="Qty" ItemStyle-Width="6%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control money" Text="0.00" ID="txtQty" onblur="return Calculate(event);" ></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan" ItemStyle-Width="6%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="txtSatuan"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Unit Price" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control money" Text="0.00" ID="txtUnitPrice" onblur="return Calculate(event);" ></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Total" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtTotal"  CssClass="form-control money" Text="0.00" onblur="return Calculate(event);"></asp:TextBox>
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
    <asp:LinkButton ID="LinkButton1" runat="server" Style=""></asp:LinkButton>
       <asp:ModalPopupExtender ID="dlgAddData" runat="server" PopupControlID="panelAddDataPelanggan" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
        </asp:ModalPopupExtender>
            <asp:Panel ID="panelAddDataPelanggan" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Add Data Pelanggan</h4>
                    </div>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    <div class="modal-body ">
                        <div class="modal-body col-overflow-400">
                            <div class="table-responsive">
                                <asp:GridView ID="grdDataPelanggan" DataKeyNames="noCust" ShowFooter="true" SkinID="GridView" runat="server" OnSelectedIndexChanged="grdDataPelanggan_SelectedIndexChanged">
                                    <Columns>
                                        <asp:BoundField DataField="noCust" SortExpression="noCust" HeaderText="No." ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="namaCust" SortExpression="namaCust" HeaderText="Nama Pelanggan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="alamatCust" SortExpression="alamatCust" HeaderText="Alamat Pelanggan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Action" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-primary" Text="Select" CausesValidation="false" CommandName="Select"  />
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
