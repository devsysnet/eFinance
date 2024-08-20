<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransKelas.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransKelas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdKelasSiswa.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-8">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <label>Tahun Ajaran : </label>
                                            <asp:DropDownList ID="cboTA" runat="server" CssClass="form-control" Width="150"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
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
                                    <asp:GridView ID="grdKelasSiswa" DataKeyNames="noSiswa" SkinID="GridView" runat="server" AllowPaging="true" PageSize="100" OnPageIndexChanging="grdKelasSiswa_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:HiddenField ID="hdnNoSiswa" runat="server" Value='<%# Bind("noSiswa") %>' />
                                                        <asp:HiddenField ID="hdnJenisSekolah" runat="server" Value='<%# Bind("jnsSekolah") %>' />
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
                                            <asp:BoundField DataField="nisn" HeaderText="NISN" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="nis" HeaderText="NIS" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="namasiswa" HeaderText="Nama Siswa" HeaderStyle-CssClass="text-center" ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="jnsSekolah" HeaderText="Tingkat" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"/>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kelas" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:DropDownList ID="cboJnsSekolah" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Ke" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox ID="txtKelasKe" runat="server" CssClass="form-control" Width="100"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                          <div class="row" id="button" runat="server">
                            <div class="col-sm-12 ">
                                <div class="text-center">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSubmit_Click" />
                                </div>
                            </div>
                        </div>
                        </div>
                        
                    </div>
                    <div id="tabForm" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
