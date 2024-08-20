<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstPendindikanUpdate.aspx.cs" Inherits="eFinance.Pages.Master.Update.MstPendindikanUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
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
            var GridVwHeaderChckbox = document.getElementById("<%=grdpendindikan.ClientID %>");
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
                                <div class="form-inline">
                                    <div class="col-sm-12">
                                        <label>Search : </label>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" MaxLength="50" PlaceHolder="pendindikan Name"></asp:TextBox>
                                        <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnCari_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdpendindikan" DataKeyNames="nopendindikan" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdpendindikan_PageIndexChanging">
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
                                            <asp:BoundField DataField="pendindikan" SortExpression="pendindikan" HeaderText="CITY" ItemStyle-Width="30%" />
                                            <asp:BoundField DataField="sts" SortExpression="sts" HeaderText="STATUS" ItemStyle-Width="25%" />
                                            <asp:TemplateField HeaderText="EDIT" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <a href="javascript:void()" class="btn btn-xs btn-labeled btn-primary" onclick="return EditRow('<%#Eval("nopendindikan")%>')">Edit</a>
                                                        <%--<a href="javascript:void()" class="btn btn-xs btn-labeled btn-danger" onclick="return DeleteRow('<%#Eval("nopendindikan")%>', '<%#Eval("pendindikan")%>')">Delete</a>--%>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--END GRID--%>
                    <%--START FORM--%>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Area Name <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtNamaArea" runat="server" CssClass="form-control" MaxLength="50" placeholder="Nama Area"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Status <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cboStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="">-------</asp:ListItem>
                                        <asp:ListItem Value="1">Aktif</asp:ListItem>
                                        <asp:ListItem Value="0">Tidak Aktif</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label"></label>
                                <div class="col-sm-5">
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--END FORM--%>
                    <%--START VIEW--%>
                    <div id="tabView" runat="server" visible="false">
                    </div>
                    <%--END VIEW--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
