<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="mMapCOAView.aspx.cs" Inherits="eFinance.Pages.Master.View.mMapCOAView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
<%--        function DeleteAll() {
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
            var GridVwHeaderChckbox = document.getElementById("<%=grdMapCOA.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }--%>

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
                                <asp:Button Style="opacity:0" ID="btnDelete" runat="server" CssClass="btn btn-danger" OnClientClick="return DeleteAll()" Text="Delete all selected" />
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
                                    <asp:GridView ID="grdMapCOA" DataKeyNames="noCOAMap" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdMapCOA_PageIndexChanging"
                                        OnSelectedIndexChanged="grdMapCOA_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                     <%--       <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
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
                                            </asp:TemplateField>--%>
                                            <asp:BoundField DataField="Ket" SortExpression="Ket" HeaderText="Rekening Aktivitas" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="ketBos" SortExpression="ketBos" HeaderText="Rekening Dana BOS" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
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
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-10">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdInstansi" SkinID="GridView" runat="server" OnRowCommand="grdInstansi_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </div>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Rekening Aktivitas" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="form-inline">
                                                            <div class="text-center">
                                                                <asp:HiddenField ID="hdnNoCOAMap" runat="server" Value='<%# Bind("noCOAMap") %>' />
                                                                <asp:TextBox runat="server" ID="txtAccount" Enabled="false" CssClass="form-control" Width="320" text='<%# Bind("Ket") %>'></asp:TextBox>
                                                                <asp:HiddenField ID="hdnAccount" Value='<%# Bind("noRek") %>' runat="server" />
                                                               <%-- <asp:ImageButton   ID="btnImgAccount" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image form-control" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                                <asp:ImageButton ID="btnClear" runat="server" ImageUrl="~/assets/images/icon_trash.png" CssClass="btn-image form-control" CommandName="Clear" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Rekening Aktivitas" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="form-inline">
                                                            <div class="text-center">
                                                                <asp:TextBox runat="server" ID="txtAccountDanaBOS" Enabled="false" CssClass="form-control" Width="320" text='<%# Bind("ketBos") %>'></asp:TextBox>
                                                                <asp:HiddenField ID="hdnAccountDanaBOS" Value='<%# Bind("noRekBos") %>' runat="server" />
                                                                <%--<asp:ImageButton ID="btnImgAccountDanaBOS" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image form-control" CommandName="SelectDanaBOS" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                                <asp:ImageButton ID="btnClearDanaBOS" runat="server" ImageUrl="~/assets/images/icon_trash.png" CssClass="btn-image form-control" CommandName="ClearDanaBOS" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="text-center">
                                            <%--<asp:Button runat="server" ID="btnUpdate" CssClass="btn btn-primary" Text="Update" OnClick="btnUpdate_Click"></asp:Button>--%>
                                            <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Back" OnClick="btnCancel_Click"></asp:Button>
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
     <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgBank" runat="server" PopupControlID="panelBank" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelBank" runat="server" align="center" Width="60%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Account Aktivitas                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           </h4>
            </div>
            <div class="row">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-5">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtSearchBank" runat="server" CssClass="form-control" MaxLength="50" placeholder="Masukan kata"></asp:TextBox>
                            <asp:Button ID="btnSearchBank" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearchBank_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body col-overflow-500">
                <asp:Label runat="server" ID="lblMessageError"></asp:Label>
                <div class="table-responsive">
                    <asp:HiddenField runat="server" ID="hdnParameterProd" />
                    <asp:GridView ID="grdBank" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdBank_PageIndexChanging" OnSelectedIndexChanged="grdBank_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Akun" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblKdRek" Text='<%#Eval("kdRek")%>'></asp:Label>
                                        <asp:HiddenField ID="hdnNoRek" runat="server" Value='<%#Eval("noRek")%>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Akun" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblKet" Text='<%#Eval("Ket")%>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelectSub" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnBatal" runat="server" Text="Tutup" CssClass="btn btn-danger" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>

     <asp:LinkButton ID="LinkButton12" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgDanaBOS" runat="server" PopupControlID="panelAkunDanaBOS" TargetControlID="LinkButton12" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelAkunDanaBOS" runat="server" align="center" Width="60%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Account Dana BOS</h4>
            </div>
            <div class="row">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-5">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtSearchDanaBOS" runat="server" CssClass="form-control" MaxLength="50" placeholder="Masukan kata"></asp:TextBox>
  <asp:Button ID="btnSearchDanaBOS" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearchDanaBOS_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body col-overflow-500">
                <div class="table-responsive">
                    <asp:HiddenField runat="server" ID="hdnParameterProdDanaBOS" />
                    <asp:GridView ID="grdAkunDanaBOS" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdAkunDanaBOS_PageIndexChanging" OnSelectedIndexChanged="grdAkunDanaBOS_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Akun" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblKdRekDanaBOS" Text='<%#Eval("kdRek")%>'></asp:Label>
                                        <asp:HiddenField ID="hdnNoRekDanaBOS" runat="server" Value='<%#Eval("noRek")%>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Akun" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblKetDanaBOS" Text='<%#Eval("Ket")%>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelectSubDanaBOS" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnBatalDanaBos" runat="server" Text="Tutup" CssClass="btn btn-danger" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
