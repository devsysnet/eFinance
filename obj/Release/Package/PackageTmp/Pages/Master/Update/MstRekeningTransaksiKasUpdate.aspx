<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstRekeningTransaksiKasUpdate.aspx.cs" Inherits="eFinance.Pages.Master.Update.MstRekeningTransaksiKasUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">

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
            var GridVwHeaderChckbox = document.getElementById("<%=grdAsset.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }

    </script>
    <asp:Button ID="cmdMode" runat="server" Style="display: none;" OnClick="cmdMode_Click" Text="Mode" />
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
                                <div class="form-group">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <label>Search :</label>
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                            <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnCari_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdAsset" SkinID="GridView" DataKeyNames="noTransKas" runat="server" AllowPaging="true" PageSize="10"
                                        OnPageIndexChanging="grdAsset_PageIndexChanging" OnSelectedIndexChanged="grdAsset_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <%--<asp:HiddenField ID="hdnnoParameterApprove" runat="server" Value='<%# Eval("noParameterApprove") %>' />--%>
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
                                            <asp:BoundField DataField="jnsTransKas" SortExpression="JenisTransaksi" HeaderText="Jenis Transaksi" ItemStyle-Width="15%" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnDetail" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Edit" CommandName="Select" />
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
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="panel">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-3">
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">JenisTransaksi</label>
                                                    <div class="col-sm-5">
                                                        <asp:DropDownList ID="cboLokasi" runat="server"  Enabled="false" >
                                                        </asp:DropDownList>
                                                    </div>
                                                    <%--<asp:Button runat="server" ID="Button1" CssClass="btn btn-success" Text="Add Lokasi" OnClick="Button1_Click"></asp:Button>--%>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3">
                                            </div>
                                            <div class="col-md-6">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grdInstansi" SkinID="GridView" runat="server">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                                <ItemTemplate>
                                                                    <div class="text-center">
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                        <asp:TextBox ID="hdnNoTrsRek" style = "display:none" runat="server" ></asp:TextBox>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Rekening" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <div class="text-center">
                                                                        <asp:DropDownList runat="server" class="form-control" ID="txtRekening"></asp:DropDownList>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="text-center">
                                        <%--<asp:Button runat="server" ID="btnAdd" CssClass="btn btn-success" Text="Add Row" OnClick="btnAdd_Click"></asp:Button>--%>
                                        <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click"></asp:Button>
                                        <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--END FORM--%>
                </div>
            </div>
        </div>
    </div>
    <asp:LinkButton ID="LinkButton2" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="mpe2" runat="server" PopupControlID="panel1" TargetControlID="LinkButton2" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panel1" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content size-sedang">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Add Brand</h4>
            </div>
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <div class="modal-body ">
                <div class="row">
                    <div class="col-sm-7">
                        <%--<asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" Text="Delete / Delete all" OnClick="btnDelete_Click" CausesValidation="false" />--%>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group ">
                            <div class="form-inline">
                                <label class="col-sm-3 control-label">Search</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    <asp:Button ID="btnCariBrand" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnCariBrand_Click" CausesValidation="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:GridView ID="grdBrand" DataKeyNames="noLokasi" runat="server" SkinID="GridView" AllowPaging="true" PageSize="5" OnPageIndexChanging="grdBrand_PageIndexChanging"
                        OnRowCommand="grdBrand_RowCommand" OnRowDeleting="grdBrand_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-danger btn-sm" />
                                        <asp:Button ID="btnEdit" runat="server" CommandName="detail" Text="Edit" CssClass="btn btn-primary btn-sm" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <%# Container.DataItemIndex + 1 %>.
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lokasi" SortExpression="kodeReagent">
                                <ItemTemplate>
                                    <asp:Label ID="lblInstansi" runat="server" Text='<%# Bind("JnsTransKas") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <div class="form-group">
                    <label for="pjs-ex1-fullname" class="col-sm-2 control-label text-right">Lokasi :</label>
                    <div class="col-sm-9">
                        <asp:TextBox runat="server" class="form-control" ID="txtBrand" type="text"></asp:TextBox>
                        <asp:HiddenField ID="hdnBrand" runat="server" />
                    </div>
                    <div class="col-sm-1">
                        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click"></asp:Button>
                        <asp:Button runat="server" ID="btnUpdateBrand" CssClass="btn btn-primary" Text="Update" OnClick="btnUpdateBrand_Click" Visible="false"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

