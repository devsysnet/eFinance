<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstCurrencyUpdate.aspx.cs" Inherits="eFinance.Pages.Master.Update.MstCurrencyUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function EditRow(id) {
            document.getElementById('<%=hdnMode.ClientID%>').value = "edit";
            document.getElementById('<%=hdnId.ClientID%>').value = id;
            eval("<%=execBind%>");
            return false;
        }
        function DeleteRow(id, name) {
            bootbox.confirm({
                message: "Anda yakin untuk menghapus data <b>" + name + "</b> ?",
                callback: function (result) {
                    if (result == true) {
                        document.getElementById('<%=hdnMode.ClientID%>').value = "delete";
                        document.getElementById('<%=hdnId.ClientID%>').value = id;
                        eval("<%=execBind%>");
                    }
                },
                className: "bootbox-sm"
            });
            return false;
        }
        function DeleteAll() {
            bootbox.confirm({
                message: "Are you sure to delete all selected data?",
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
            var GridVwHeaderChckbox = document.getElementById("<%=grdCurrency.ClientID %>");
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
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" CausesValidation="false" OnClick="btnCari_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="grdCurrency" DataKeyNames="noMataUang" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdCurrency_PageIndexChanging"
                            OnSelectedIndexChanged="grdCurrency_SelectedIndexChanged">
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
                                <asp:BoundField DataField="kodeMataUang" SortExpression="kodeMataUang" HeaderText="Kode Mata Uang" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="namaMataUang" SortExpression="namaMataUang" HeaderText="Nama Mata Uang" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="Negara" SortExpression="negara" HeaderText="Country" ItemStyle-Width="20%" />
                                <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Detail" CommandName="Select" />
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
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Currency Name :</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control" required ID="txtCurrencyName" type="text" placeholder="Enter Currency Name"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Code :</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control" required ID="txtCode" type="text" placeholder="Enter Code"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Country :</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control" required ID="txtCountry" type="text" placeholder="Enter Country"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Currency Status :</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboStatus" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="1">Aktif</asp:ListItem>
                                                <asp:ListItem Value="0">Non Aktif</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Set As Default :</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboSet" CssClass="form-control" runat="server" Enabled="false">
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="form-row">
                                <div class="col-md-12">
                                    <div class="text-center">
                                        <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click" />
                                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click" />
                                    </div>
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
