<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstProductLocationUpdate.aspx.cs" Inherits="eFinance.Pages.Master.Update.MstProductLocationUpdate1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="TabName" runat="server" />
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
            var GridVwHeaderChckbox = document.getElementById("<%=grdVoucher.ClientID %>");
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
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" CausesValidation="false" OnClick="btnCari_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="grdVoucher" DataKeyNames="noproduct" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10"
                            OnSelectedIndexChanged="grdVoucher_SelectedIndexChanged" OnPageIndexChanging="grdVoucher_PageIndexChanging">
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
                                <asp:BoundField DataField="prodno" HeaderStyle-CssClass="text-center" HeaderText="Kode Product" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="prodnm" HeaderStyle-CssClass="text-center" HeaderText="Nama Product" ItemStyle-Width="25%" />
                                <asp:BoundField DataField="manufactur" HeaderStyle-CssClass="text-center" HeaderText="Manufactur" ItemStyle-Width="30%" />
                                <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Edit" CommandName="Select" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row" style="margin-top: 10px;">
                            <div class="col-sm-12">
                                <div class="form-horizontal">
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kode Barang :</label>
                                        <asp:HiddenField ID="hdnNoProduct" runat="server" />
                                        <asp:Label runat="server" class="col-6 col-form-label" ID="lblKodeBarang"></asp:Label>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Barang :</label>
                                        <asp:Label runat="server" class="col-6 col-form-label" ID="lblNamaBarang"></asp:Label>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Jenis :</label>
                                        <asp:Label runat="server" class="col-6 col-form-label" ID="lblJenis"></asp:Label>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Minimum Safety Stok :</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtMinStok"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kapasitas :</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtKapasitas"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="grdProdLoc" SkinID="GridView" runat="server" OnRowDataBound="grdProdLoc_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <%# Container.DataItemIndex + 1 %>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Gudang">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <asp:HiddenField ID="hdnNoProduct" runat="server" Value='<%# Bind("noLokasiBarang") %>' />
                                            <asp:HiddenField ID="hdnNoGudang" runat="server" Value='<%# Bind("noGudang") %>' />
                                            <asp:DropDownList ID="cboGudang" runat="server" CssClass="form-control" OnSelectedIndexChanged="cboGudang_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Lokasi Gudang">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <asp:HiddenField ID="hdnNoLokGud" runat="server" Value='<%# Bind("noLokasiGudang") %>' />
                                            <asp:DropDownList ID="cboLokasiGudang" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="form-group row">
                            <div class="col-md-12">
                                <div class="text-center">
                                    <asp:Button runat="server" ID="Button1" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                    <asp:Button runat="server" ID="Button2" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabView" runat="server" visible="false">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
