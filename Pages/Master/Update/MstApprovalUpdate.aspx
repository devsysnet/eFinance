<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstApprovalUpdate.aspx.cs" Inherits="eFinance.Pages.Master.Update.MstApprovalUpdate" %>

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
            var GridVwHeaderChckbox = document.getElementById("<%=grdApprovalScheme.ClientID %>");
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
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" OnClientClick="return DeleteAll()" Text="Hapus" />
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="cboUntukHeader" runat="server">
                                        <asp:ListItem Value="-">--Pilih Peruntukan--</asp:ListItem>
                                        <asp:ListItem Value="Unit">Unit</asp:ListItem>
                                        <asp:ListItem Value="Perwakilan">Perwakilan</asp:ListItem>
                                        <asp:ListItem Value="Yayasan">Yayasan</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-6 text-right">
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <div class="col-sm-12">
                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary" Text="Cari" OnClick="btnCari_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdApprovalScheme" SkinID="GridView" DataKeyNames="noApprove" runat="server" AllowPaging="true" PageSize="10"
                                        OnPageIndexChanging="grdApprovalScheme_PageIndexChanging" OnSelectedIndexChanged="grdApprovalScheme_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:HiddenField ID="hdnnoParameterApprove" runat="server" Value='<%# Eval("noParameterApprove") %>' />
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
                                            <asp:BoundField DataField="namaParameterApprove" HeaderStyle-CssClass="text-center" SortExpression="namaParameterApprove" HeaderText="Parameter" ItemStyle-Width="15%" />
                                            <asp:BoundField DataField="namaUser" HeaderStyle-CssClass="text-center" SortExpression="namaUser" HeaderText="User Name" ItemStyle-Width="15%" />
                                            <asp:BoundField DataField="namaCabang" HeaderStyle-CssClass="text-center" SortExpression="namaCabang" HeaderText="Cabang" ItemStyle-Width="12%" />
                                            <asp:BoundField DataField="hakAkses" HeaderStyle-CssClass="text-center" SortExpression="hakAkses" HeaderText="Jabatan" ItemStyle-Width="15%" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="3%">
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
                            <div class="row">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Peruntukan</label>
                                        <div class="controls col-sm-4">
                                            <asp:DropDownList ID="cboUntuk" runat="server" Enabled="false">
                                                <asp:ListItem Value="-">--Pilih Peruntukan--</asp:ListItem>
                                                <asp:ListItem Value="Unit">Unit</asp:ListItem>
                                                <asp:ListItem Value="Perwakilan">Perwakilan</asp:ListItem>
                                                <asp:ListItem Value="Yayasan">Yayasan</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Kategori</label>
                                        <div class="controls col-sm-4">
                                            <asp:DropDownList ID="cboCategory" runat="server"  Enabled="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="text-center">
                                    <div class="col-sm-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdApproval" SkinID="GridView" Width="60%" runat="server" HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Jabatan" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <div class="form-inline">
                                                                    <asp:DropDownList ID="cboJabatan" runat="server"  Enabled="false">
                                                                        <asp:ListItem Value="0">--Pilih Jabatan--</asp:ListItem>
                                                                        <asp:ListItem Value="Kepala Yayasan">Kepala Yayasan</asp:ListItem>
                                                                        <asp:ListItem Value="Kepala Perwakilan">Kepala Perwakilan</asp:ListItem>
                                                                        <asp:ListItem Value="Kepala Sekolah">Kepala Sekolah</asp:ListItem>
                                                                        <asp:ListItem Value="Bendahara">Bendahara</asp:ListItem>
                                                                    </asp:DropDownList> 
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Approval" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <asp:HiddenField ID="hdnLevel" runat="server" />
                                                                <asp:DropDownList ID="cboLevel" runat="server" Enabled="false">
                                                                    <asp:ListItem Value="1">Approval 1</asp:ListItem>
                                                                    <asp:ListItem Value="2">Approval 2</asp:ListItem>
                                                                    <asp:ListItem Value="3">Approval 3</asp:ListItem>
                                                                    <asp:ListItem Value="4">Approval 4</asp:ListItem>
                                                                    <asp:ListItem Value="5">Approval 5</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="text-center">
                                        <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click" />
                                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnReset_Click" />
                                    </div>
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
