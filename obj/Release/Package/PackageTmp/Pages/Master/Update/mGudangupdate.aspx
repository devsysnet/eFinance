<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="mGudangupdate.aspx.cs" Inherits="eFinance.Pages.Master.Update.mGudangupdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function DeleteAll() {

            document.getElementById('<%=hdnMode.ClientID%>').value = "deleteall";
            eval("<%=execBind%>");

            return false;
        }
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdGudang.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }

    </script>
    <asp:Button ID="cmdMode" runat="server" OnClick="cmdMode_Click" Style="display: none;" Text="Mode" />
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-8">
                                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" OnClientClick="return DeleteAll()" Text="Delete all selected" />
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <label>Search :</label>
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdGudang" DataKeyNames="noGudang" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdGudang_PageIndexChanging"
                                        OnSelectedIndexChanged="grdGudang_SelectedIndexChanged">
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
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="kdGudang" SortExpression="kdGudang" HeaderText="Gudang ID" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="namaGudang" SortExpression="namaGudang" HeaderText="Gudang" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
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
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Gudang :</label>
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" class="form-control" ID="txtNamaGudang" placeholder="Enter Warehouse Name"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Jenis Gudang :</label>
                                    <div class="col-sm-2">
                                        <asp:DropDownList runat="server" ID="cboJenisGudang">
                                            <asp:ListItem Value="Stok">Stok</asp:ListItem>
                                            <asp:ListItem Value="Penjualan">Penjualan</asp:ListItem>
                                            <asp:ListItem Value="Rusak">Rusak</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Keterangan</label>
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" class="form-control" ID="txtKet" TextMode="MultiLine" placeholder="Enter Description"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="text-center">
                                            <asp:Button runat="server" ID="btnUpdate" CssClass="btn btn-success" Text="Update" OnClick="btnUpdate_Click"></asp:Button>
                                            <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click"></asp:Button>
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
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
