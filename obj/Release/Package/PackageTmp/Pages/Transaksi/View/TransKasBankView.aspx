<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransKasBankView.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TransKasBankView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function money(money) {
            var num = money;
            if (num != "") {
                var array = num.toString().split('');
                var index = -3;
                while (array.length + index > 0) {
                    array.splice(index, 0, ',');
                    index -= 4;
                }

                money = array.join('') + '.00';
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
            var debit = 0, kredit = 0, hasilDebit = 0, hasilKredit = 0;
            var gridView = document.getElementById("<%=grdKasBank.ClientID %>");

            for (var i = 1; i < gridView.rows.length; i++) {
                debit = removeMoney(gridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value) * 1;
                kredit = removeMoney(gridView.rows[i].cells[5].getElementsByTagName("INPUT")[0].value) * 1;

                if (debit != 0)
                    hasilDebit += debit;
                else if (kredit != 0)
                    hasilKredit += kredit;
            }
            if (hasilDebit != 0)
                document.getElementById("BodyContent_txtTotalDebit").value = money(hasilDebit);
            else if (hasilKredit != 0)
                document.getElementById("BodyContent_txtTotalKredit").value = money(hasilKredit);
        }
    </script>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="col-sm-8">
                <%--<asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" OnClientClick="return DeleteAll()" Text="Delete all selected" />--%>
            </div>
            <div class="col-sm-4">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Search : </label>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search.."></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdKas" DataKeyNames="noKas" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdKas_PageIndexChanging"
                    OnSelectedIndexChanged="grdKas_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Tgl" SortExpression="Tgl" HeaderText="Date Kas & Bank" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="nomorKode" SortExpression="nomorKode" HeaderText="Code Kas & Bank" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Cust" SortExpression="Cust" HeaderText="Customer Name" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Edit" CommandName="Select" />
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
                                        <label class="col-sm-5 control-label">Transaction <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="cboTransaction">
                                                <asp:ListItem Value="0">-------</asp:ListItem>
                                                <asp:ListItem Value="1">Bank Receipt</asp:ListItem>
                                                <asp:ListItem Value="2">Bank Disbursement</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Account <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="cboAccount"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Date <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control date" Enabled="false" ID="dtKas"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Value <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control money" Enabled="false" Text="0.00" ID="txtValue"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">From / To <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <div runat="server" id="formFromToCus">
                                                <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="cboFromToCus">
                                                    <asp:ListItem Value="0">-------</asp:ListItem>
                                                    <asp:ListItem Value="1">Other</asp:ListItem>
                                                    <asp:ListItem Value="2">Salesman</asp:ListItem>
                                                    <asp:ListItem Value="3">Customer</asp:ListItem>
                                                    <asp:ListItem Value="4">Down payment customer</asp:ListItem>
                                                    <asp:ListItem Value="5">Customer allocation</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div runat="server" id="forFormToSupp" visible="false">
                                                <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="cboFromToSupp">
                                                    <asp:ListItem Value="0">-------</asp:ListItem>
                                                    <asp:ListItem Value="1">Other</asp:ListItem>
                                                    <asp:ListItem Value="2">Salesman</asp:ListItem>
                                                    <asp:ListItem Value="6">Supplier</asp:ListItem>
                                                    <asp:ListItem Value="7">Down payment Supplier</asp:ListItem>
                                                    <asp:ListItem Value="8">Supplier allocation</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="formSalesman" visible="false">
                                        <div class="form-group">
                                            <div class="form-inline">
                                                <label class="col-sm-5 control-label">Salesman <span class="mandatory">*</span></label>
                                                <div class="col-sm-6">
                                                    <asp:HiddenField runat="server" ID="hdnSalesman" />
                                                    <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtSalesman"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="formCustomer" visible="false">
                                        <div class="form-group">
                                            <div class="form-inline">
                                                <label class="col-sm-5 control-label">Customer <span class="mandatory">*</span></label>
                                                <div class="col-sm-6">
                                                    <asp:HiddenField runat="server" ID="hdnCustomer" />
                                                    <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtCustomer"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="formSupplier" visible="false">
                                        <div class="form-group">
                                            <div class="form-inline">
                                                <label class="col-sm-5 control-label">Supplier <span class="mandatory">*</span></label>
                                                <div class="col-sm-6">
                                                    <asp:HiddenField runat="server" ID="hdnSupplier" />
                                                    <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtSupplier"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Currency <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="cboCurrency"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Bank </label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="txtBank"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Giro No <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="txtGiroNo"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Giro Date <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" Enabled="false" CssClass="form-control date" ID="dtGiro"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Remark </label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="txtRemark" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <asp:GridView ID="grdKasBank" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnKasBank" runat="server" />
                                                    <asp:HiddenField ID="hdnParameter" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Account" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="text-center">
                                                            <asp:TextBox runat="server" ID="txtAccount" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnAccount" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Description" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label runat="server" ID="lblDescription"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Remark" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="txtRemark"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Debit" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control money" Text="0.00" ID="txtDebit"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Credit" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control money" Text="0.00" ID="txtKredit"></asp:TextBox>
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
                                        <label class="col-sm-8 control-label">Total </label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtTotalDebit" runat="server" Enabled="false" CssClass="form-control money" Width="160" Text="0.00"></asp:TextBox>
                                            <asp:TextBox ID="txtTotalKredit" runat="server" Enabled="false" CssClass="form-control money" Width="160" Text="0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-12">
                                <div class="text-center">
                                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click"></asp:Button>
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
