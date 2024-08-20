<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstKomponengajiUpdate.aspx.cs" Inherits="eFinance.Pages.Master.Update.MstKomponengajiUpdate" %>
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
            var GridVwHeaderChckbox = document.getElementById("<%=grdkomponengaji.ClientID %>");
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
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" MaxLength="50" PlaceHolder="komponengaji Name"></asp:TextBox>
                                        <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnCari_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdkomponengaji" DataKeyNames="nokomponengaji" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdkomponengaji_PageIndexChanging">
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
                                            <asp:BoundField DataField="komponengaji" SortExpression="komponengaji" HeaderText="Komponen Gaji" ItemStyle-Width="30%" />
                                            <asp:BoundField DataField="kategori" SortExpression="kategori" HeaderText="Kategori" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="jenis" SortExpression="jenis" HeaderText="Jenis" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="pph21" SortExpression="pph21" HeaderText="Unsur PPH21" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="absensi" SortExpression="absensi" HeaderText="Unsur Absesni" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="bpjs" SortExpression="bpjs" HeaderText="Unsur Iuran" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="bulanan" SortExpression="bulanan" HeaderText="Bulanan" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="ket" SortExpression="ket" HeaderText="COA" ItemStyle-Width="30%" />
                                            <asp:BoundField DataField="jeniskegiatan" SortExpression="ket" HeaderText="Jenis Kegiatan" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="sts" SortExpression="sts" HeaderText="STATUS" ItemStyle-Width="10%" />
                                            <asp:TemplateField HeaderText="EDIT" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <a href="javascript:void()" class="btn btn-xs btn-labeled btn-primary" onclick="return EditRow('<%#Eval("nokomponengaji")%>')">Edit</a>
                                                        <%--<a href="javascript:void()" class="btn btn-xs btn-labeled btn-danger" onclick="return DeleteRow('<%#Eval("nokomponengaji")%>', '<%#Eval("komponengaji")%>')">Delete</a>--%>
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
                                <label class="col-sm-3 control-label">Komponen Gaji</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtNamaArea" runat="server" CssClass="form-control" placeholder="komponengaji"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Kategori</label>
                                <div class="col-sm-3">
                                     <asp:DropDownList ID="cboKategori" runat="server" CssClass="form-control">
                                     </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Jenis</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cboJenis" runat="server" CssClass="form-control" Width="150">
                                    <asp:ListItem Value="1">Tetap</asp:ListItem>
                                     <asp:ListItem Value="0">Tidak Tetap</asp:ListItem>
                                     </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Unsur PPH21</label>
                                <div class="col-sm-3">
                                   <asp:DropDownList ID="cbopph21" runat="server" CssClass="form-control" Width="100">
                                   <asp:ListItem Value="1">Ya</asp:ListItem>
                                   <asp:ListItem Value="0">Tidak</asp:ListItem>
                                   </asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                <label class="col-sm-3 control-label">Unsur Absesni</label>
                                <div class="col-sm-3">
                                   <asp:DropDownList ID="cboabsensi" runat="server" CssClass="form-control" Width="100">
                                   <asp:ListItem Value="1">Ya</asp:ListItem>
                                   <asp:ListItem Value="0">Tidak</asp:ListItem>
                                   </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Unsur Iuran</label>
                                <div class="col-sm-3">
                                   <asp:DropDownList ID="cbobpjs" runat="server" CssClass="form-control" Width="100">
                                   <asp:ListItem Value="1">Ya</asp:ListItem>
                                   <asp:ListItem Value="0">Tidak</asp:ListItem>
                                   </asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                <label class="col-sm-3 control-label">Bulanan</label>
                                <div class="col-sm-3">
                                   <asp:DropDownList ID="cbobulanan" runat="server" CssClass="form-control" Width="50">
                                   <asp:ListItem Value="1">Bulanan</asp:ListItem>
                                   <asp:ListItem Value="0">Tahunan</asp:ListItem>
                                   </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">COA</label>
                                <div class="col-sm-3">
                                  <asp:DropDownList ID="cbnorek" CssClass="form-control" runat="server"  Width="150"></asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                <label class="col-sm-3 control-label">Jenis Kegiatan</label>
                                <div class="col-sm-3">
                                  <asp:DropDownList ID="cbojeniskegiatan" CssClass="form-control" runat="server"  Width="150"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Status <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cboStatus" runat="server" CssClass="form-control" Width="100">
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


