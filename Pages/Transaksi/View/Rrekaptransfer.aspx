<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Rrekaptransfer.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.Rrekaptransfer" %>
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
                                        <div class="col-sm-10">Tahun  :    
                                        <asp:DropDownList ID="cboYear" runat="server" Width="100"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <asp:GridView ID="grdHarianGL" DataKeyNames="ket" SkinID="GridView" runat="server" AllowPaging="false" PageSize="10" OnPageIndexChanging="grdHarianGL_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="ket" SortExpression="ket" HeaderText="Rekening" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="unit" SortExpression="unit" HeaderText="Unit" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="Jan" SortExpression="Jan" HeaderText="Jan" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Feb" SortExpression="Feb" HeaderText="Feb" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Mar" SortExpression="Mar" HeaderText="Mar" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Apr" SortExpression="Apr" HeaderText="Apr" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Mei" SortExpression="Mei" HeaderText="Mei" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Jun" SortExpression="Jun" HeaderText="Jun" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Jul" SortExpression="Jul" HeaderText="Jul" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Ags" SortExpression="Ags" HeaderText="Ags" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Sept" SortExpression="Sept" HeaderText="Sept" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Okt" SortExpression="Okt" HeaderText="Okt" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Nov" SortExpression="Nov" HeaderText="Nov" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Des" SortExpression="Des" HeaderText="Des" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" />
                                                                               
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

