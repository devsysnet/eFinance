<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="PostingInvoice.aspx.cs" Inherits="eFinance.Pages.Transaksi.Posting.PostingInvoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdPosting.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="col-sm-8">
            </div>
            <div class="col-sm-4">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Search : </label>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search.."></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnCari_Click" />
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdPosting" DataKeyNames="noInv" SkinID="GridView" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
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
                                    <asp:HiddenField ID="hdnnoInv" runat="server" Value='<%# Eval("noInv") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="kdInv" SortExpression="kdConf" HeaderText="Kode Invoice" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="tglInv" SortExpression="tglConf" HeaderText="Tanggal Invoice" ItemStyle-Width="20%" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="nopajak" SortExpression="kdDO" HeaderText="Nomor Pajak" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="namaCust" SortExpression="namaCust" HeaderText="Customer" ItemStyle-Width="20%" />
                       </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row" runat="server" id="button">
            <div class="form-group row">
                <div class="col-md-12">
                    <div class="text-center">
                        <asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Posting" OnClick="btnPosting_Click"></asp:Button>
                        <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
