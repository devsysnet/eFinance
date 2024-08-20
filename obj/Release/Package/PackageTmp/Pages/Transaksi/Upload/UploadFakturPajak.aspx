<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="UploadFakturPajak.aspx.cs" Inherits="eFinance.Pages.Transaksi.Upload.UploadFakturPajak" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdUploadFakturPajak.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[7].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-6">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="form-inline">
                                                <div class="col-sm-12">
                                                    <label>Search : </label>
                                                    <asp:TextBox ID="dtMulai" runat="server" CssClass="form-control date" MaxLength="50"></asp:TextBox>
                                                    <asp:TextBox ID="dtSampai" runat="server" CssClass="form-control date" MaxLength="50"></asp:TextBox>
                                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="form-inline">
                                                <div class="col-sm-12">
                                                    <label>Export To : </label>
                                                    <asp:DropDownList ID="cboType" runat="server" Width="100">
                                                        <asp:ListItem Value="csv">CSV</asp:ListItem>
                                                        <asp:ListItem Value="xlsx">EXCEL</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <asp:GridView ID="grdUploadFakturPajak" DataKeyNames="noInvoice" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdUploadFakturPajak_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField runat="server" ID="hdntipeInv" Value='<%# Eval("jenis") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="jenis" SortExpression="jenis" HeaderText="Invoice Type" HeaderStyle-CssClass="text-center" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="kdCust" SortExpression="kdCust" HeaderText="Customer Code" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="namaCust" SortExpression="namaCust" HeaderText="Customer Name" HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" />
                                        <asp:BoundField DataField="tglInvoice" SortExpression="tglInvoice" HeaderText="Invoice Date" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                                        <asp:BoundField DataField="kodeInvoice" SortExpression="kodeInvoice" HeaderText="Invoice Number" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="nomorPajak" SortExpression="nomorPajak" HeaderText="Tax Number" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
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
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 ">
                                <div class="text-center">
                                    <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary" Text="EXPORT SELECTED DATA" OnClick="btnExport_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--END GRID--%>
                    <%--START FORM--%>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="form-horizontal">
                            
                        </div>
                    </div>
                </div>
                <%--END TAB FORM--%>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
