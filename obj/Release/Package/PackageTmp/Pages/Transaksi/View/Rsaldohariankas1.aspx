<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Rsaldohariankas1.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.Rsaldohariankas1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <script type="text/javascript">
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdHarianGL.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[5].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                             <div class="form-group">
                                <div class="form-inline">
                                        <div class="col-sm-10">Cabang    
                                         <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Enabled="true" width="250"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <asp:GridView ID="grdHarianGL" DataKeyNames="kdrek" SkinID="GridView" runat="server" AllowPaging="false" PageSize="10" OnPageIndexChanging="grdHarianGL_PageIndexChanging">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="kdrek" SortExpression="kdrek" HeaderText="Kode Rekening" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="ket" SortExpression="ket" HeaderText="Rekening" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="tgl" SortExpression="tgl" HeaderText="Tanggal" ItemStyle-Width="10%" DataFormatString="{0:dd MMM yyyy}" ItemStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="Saldo">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label ID="nilai" runat="server" Text='<%# Bind("nilai", "{0:#,0.00}") %>' Width="180"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                            <HeaderTemplate>
                                                <div class="text-center">
                                                    <%--<asp:CheckBox ID="chkCheckAll" runat="server" CssClass="px" onclick="return CheckAllData(this);" />--%>
                                                </div>
                                            </HeaderTemplate>
                                            <%--<ItemTemplate>
                                                <div class="text-center">
                                                    <asp:CheckBox ID="chkCheck" runat="server" CssClass="px chkCheck" />
                                                </div>
                                            </ItemTemplate>--%>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <%--<asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Posting" OnClick="btnPosting_Click"></asp:Button>--%>
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
