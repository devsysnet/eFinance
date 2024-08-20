<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Rdatatdkbalance.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.Rdatatdkbalance" %>
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
                                        <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <asp:GridView ID="grdHarianGL" DataKeyNames="kdtran" SkinID="GridView" runat="server" AllowPaging="false" PageSize="10" OnPageIndexChanging="grdHarianGL_PageIndexChanging">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="kdtran" SortExpression="kdtran" HeaderText="Kode Transaksi" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="namaCabang" SortExpression="namaCabang" HeaderText="Nama Cabang" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="tgl" SortExpression="tgl" HeaderText="Tanggal" ItemStyle-Width="10%" DataFormatString="{0:dd MMM yyyy}" ItemStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="Debet">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label ID="nilai" runat="server" Text='<%# Bind("debet", "{0:#,0.00}") %>' Width="180"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="Kredit">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label ID="nilai" runat="server" Text='<%# Bind("kredit", "{0:#,0.00}") %>' Width="180"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="Selisih">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label ID="nilai" runat="server" Text='<%# Bind("selisih", "{0:#,0.00}") %>' Width="180"></asp:Label>
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

     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
