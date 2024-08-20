<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="AdmFileSetup.aspx.cs" Inherits="eFinance.Pages.Master.Input.AdmFileSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdMenu.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-sm-2 control-label text-right">1st Menu</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cboMenuOne" runat="server" OnSelectedIndexChanged="cboMenuOne_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label text-right">2st Menu</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cboMenuTwo" runat="server" OnSelectedIndexChanged="cboMenuTwo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--END--%>
                    <div class="row">
                        <div class="col-sm-12 overflow-x-table">
                            <div class="text-left">
                                <h5><b>3rd Menu Data</b></h5>
                            </div>
                            <asp:GridView ID="grdMenu" DataKeyNames="noMenu" SkinID="GridView" runat="server" OnRowDataBound="grdMenu_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="">
                                        <HeaderTemplate>
                                            <div class="text-center">
                                                <asp:CheckBox ID="chkCheckAll" runat="server" CssClass="px" onclick="return CheckAllData(this);" />
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:CheckBox ID="chkCheck" runat="server" />
                                                <asp:HiddenField ID="hdnNoParent" runat="server" Value='<%# Bind("parentMenu") %>' />

                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Sort Number">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox ID="txtSortNumber" runat="server" CssClass="form-control" Width="80" Text='<%# Bind("noUrut") %>'></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Menu Name">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox ID="txtMenuName" runat="server" CssClass="form-control" Width="200" Text='<%# Bind("namaMenu") %>'></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Menu Title">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox ID="txtMenuTitle" runat="server" CssClass="form-control" Width="150" Text='<%# Bind("judulMenu") %>'></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Site Map">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox ID="txtSiteMap" runat="server" CssClass="form-control" Width="300" Text='<%# Bind("siteMapMenu") %>'></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="URL / File Name">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox ID="txtUrlMenu" runat="server" CssClass="form-control" Width="350" Text='<%# Bind("namaFileMenu") %>'></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Icon">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox ID="txtIcon" runat="server" CssClass="form-control" Width="200" Text='<%# Bind("iconMenu") %>'></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Status">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:DropDownList ID="cboStatus" runat="server">
                                                    <asp:ListItem Value="1">Show</asp:ListItem>
                                                    <asp:ListItem Value="2">Hide</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Sub Menu">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:DropDownList ID="cboSubMenu" runat="server" Width="250">
                                                </asp:DropDownList>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 ">
                            <div class="text-center">
                                <asp:Button ID="btnAddRow" runat="server" CssClass="btn btn-success" Text="Add Row" OnClick="btnAddRow_Click" />
                                <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update Menu" OnClick="btnUpdate_Click" />
                                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete Menu" OnClick="btnDelete_Click" />
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
