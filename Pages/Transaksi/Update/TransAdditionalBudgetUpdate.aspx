<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransAdditionalBudgetUpdate.aspx.cs" Inherits="eFinance.Pages.Transaksi.Update.TransAdditionalBudgetUpdate" %>

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
            var GridVwHeaderChckbox = document.getElementById("<%=grdBudget.ClientID %>");
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
                                    <asp:GridView ID="grdBudget" SkinID="GridView" DataKeyNames="noAdditionalBudget" runat="server" AllowPaging="true" PageSize="10"
                                        OnPageIndexChanging="grdBudget_PageIndexChanging" OnSelectedIndexChanged="grdBudget_SelectedIndexChanged">
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
                                            <asp:BoundField DataField="ket" SortExpression="Rekening" HeaderText="Rekening" ItemStyle-Width="15%" />
                                            <asp:BoundField DataField="nilai" SortExpression="tglKursBuku" HeaderText="Nilai" ItemStyle-Width="15%" />
                                            <asp:BoundField DataField="tgl" SortExpression="nilaiKursBuku" HeaderText="Tanggal" ItemStyle-Width="10%" />
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

                        <div class="form-horizontal">
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Rekening :</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" class="form-control" ID="txtRekening"></asp:TextBox>
                                    <asp:HiddenField ID="hdnNoRek" runat="server" />

                                </div>
                                <div class="col-sm-1">
                                    <asp:ImageButton ID="imgButtonProduct" class="btn-image" runat="server" ImageUrl="~/assets/images/icon_search.png" OnClick="imgButtonProduct_Click" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tanggal Additional :</label>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" class="form-control date" ID="dtAdd"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nilai :</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" class="form-control money" ID="txtNilai" Text="0.00"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Remaks :</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" class="form-control" ID="txtRemark" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-md-12">
                                    <div class="text-center">
                                        <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click"></asp:Button>
                                        <asp:Button runat="server" ID="btnResetRek" CssClass="btn btn-danger" Text="Reset" OnClick="btnResetRek_Click"></asp:Button>
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
    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgParentAccount" runat="server" PopupControlID="panelParent" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelParent" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content size-sedang">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Account Data</h4>
            </div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="modal-body ">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group ">
                            <div class="form-inline">
                                <div class="col-sm-8">
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group ">
                                        <div class="form-inline">
                                            <div class="col-sm-12">
                                                <label>Search :</label>
                                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Search.."></asp:TextBox>
                                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="Button1_Click" CausesValidation="false" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="grdRekening" DataKeyNames="noRek" ShowFooter="true" SkinID="GridView" runat="server" AllowPaging="true" PageSize="5" OnPageIndexChanging="grdRekening_PageIndexChanging" OnSelectedIndexChanged="grdRekening_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="kdRek" SortExpression="kdRek" HeaderText="Account Code" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Ket" SortExpression="Ket" HeaderText="Remark" ItemStyle-Width="20%" />
                            <asp:BoundField DataField="Grup" SortExpression="Grup" HeaderText="Group" ItemStyle-Width="15%" />
                            <asp:BoundField DataField="Kelompok" SortExpression="Kelompok" HeaderText="Kelompok" ItemStyle-Width="20%" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="Select">
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
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
