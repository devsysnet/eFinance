<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="mKategoribrgupd.aspx.cs" Inherits="eFinance.Pages.Master.Update.mKategoribrgupd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
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
            var GridVwHeaderChckbox = document.getElementById("<%=grdKategoriBrg.ClientID %>");
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
                                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" OnClientClick="return DeleteAll()" Text="Hapus" />
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <label>Cari :</label>
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdKategoriBrg" DataKeyNames="kategori" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdKategoriBrg_PageIndexChanging"
                                        OnSelectedIndexChanged="grdKategoriBrg_SelectedIndexChanged">
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
                                            <asp:BoundField DataField="jns" SortExpression="kdGudang" HeaderText="Kategori" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="kategori" SortExpression="kdGudang" HeaderText="Sub Kategori" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
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
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kategori</label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="cbojnsBarang" runat="server" Enabled="false">
                                            <asp:ListItem Value="0">----</asp:ListItem>
                                            <asp:ListItem Value="1">Asset</asp:ListItem>
                                            <asp:ListItem Value="2">Non Asset</asp:ListItem>
                                            <asp:ListItem Value="3">Jasa</asp:ListItem>
                                            <asp:ListItem Value="4">Inventaris</asp:ListItem>
                                            <asp:ListItem Value="5">Sales</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-8">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdKategoriBrgDetil" SkinID="GridView" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Sub kategori" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:TextBox ID="txtkategori" runat="server" CssClass="form-control" Width="300"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnkategori" runat="server" />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="COA" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:DropDownList ID="cbnorek" CssClass="form-control" runat="server" Width="300"></asp:DropDownList>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="COA PENYUSUTAN" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cborekkd" CssClass="form-control" runat="server" Width="150"></asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="COA BIAYA PENYUSUTAN" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cborekdb" CssClass="form-control" runat="server" Width="150"></asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="text-center">
                                            <asp:Button runat="server" ID="btnUpdate" CssClass="btn btn-primary" Text="Ubah" OnClick="btnUpdate_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                            <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Batal" OnClick="btnCancel_Click"></asp:Button>
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
