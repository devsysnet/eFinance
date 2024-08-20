<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransInvoiceNewView.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TransInvoiceNewView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function OpenReport() {
            //            OpenReportViewer();
            window.open('../../../Report/ReportViewer.aspx', 'name', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,modal=yes,width=1000,height=600');
        }
    </script>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="col-sm-8">
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
                <asp:GridView ID="grdPOImport" DataKeyNames="noINV" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPOImport_PageIndexChanging"
                    OnRowCommand="grdPOImport_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnIdPrint" runat="server" Value='<%# Eval("noINV") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="kdInv" HeaderStyle-CssClass="text-center" HeaderText="Invoice Code" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="tglInv" HeaderStyle-CssClass="text-center" HeaderText="Invoice Date" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                        <asp:BoundField DataField="namaCust" HeaderStyle-CssClass="text-center" HeaderText="Customer Name" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="alamatCust" HeaderStyle-CssClass="text-center" HeaderText="Customer Address" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Detail" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
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
                                        <label class="col-sm-5 control-label">Qoutation Code<span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" Enabled="false" CssClass="form-control date" ID="txtKdInv"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Qoutation Date <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" Enabled="false" CssClass="form-control date" ID="dtPO"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-5 control-label">Customer</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox runat="server" ID="txtSupplier" Enabled="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Address </label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" ID="txtAddress" Enabled="false" CssClass="form-control" Width="340" Height="65" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Phone </label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" ID="txtPhone" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="table-responsive">
                                    <asp:GridView ID="grdPO" SkinID="GridView" runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:HiddenField ID="hdnPOD" runat="server" />
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
                                                                <asp:HiddenField ID="hdnProduct" runat="server" />
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
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty" ItemStyle-Width="6%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" Enabled="false" ID="txtQty"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Packing" ItemStyle-Width="6%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Label runat="server" class="form-label" ID="lblUnit"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Unit Price" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txtUnitPrice" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Total" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <div class="text-right">
                                                        <asp:TextBox runat="server" ID="txtTotal" Enabled="false" CssClass="form-control money" Text="0.00"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-sm-12">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="form-inline">
                                                <label class="col-sm-10 control-label">Amount </label>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtAmount" runat="server" Enabled="false" CssClass="form-control money" Width="157" Text="0.00"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                    <div class="form-inline">
                                        <label class="col-sm-10 control-label">PPN <asp:Label ID="lblPersenPPn" runat="server">10.00</asp:Label>% </label>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPPN" runat="server" CssClass="form-control money" Width="157" Text="0.00" onblur="return CalculateTotal(event);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                        <div class="form-group">
                                            <div class="form-inline">
                                                <label class="col-sm-10 control-label">Total Amount </label>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtTotalAmount" runat="server" Enabled="false" CssClass="form-control money" Width="157" Text="0.00"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click"></asp:Button>
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
    </div>

</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>--%>
