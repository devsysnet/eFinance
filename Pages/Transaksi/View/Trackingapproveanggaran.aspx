<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Trackingapproveanggaran.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.Trackingapproveanggaran" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnnoYysn" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-sm-1 control-label">
                                            <span>Filter Cari</span>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboPerwakilan" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboPerwakilan_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdSiswa" DataKeyNames="noCabang" SkinID="GridView" runat="server" AllowPaging="false" PageSize="100" OnPageIndexChanging="grdSiswa_PageIndexChanging" OnRowDataBound="grdSiswa_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <%--<asp:HiddenField ID="hdnJmlTunggakan" runat="server" Value='<%# Bind("jml") %>' />--%>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:BoundField DataField="kdCabang" HeaderText="Kode Unit" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="namaCabang" HeaderText="Nama Unit" HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="apprKe" HeaderText="Approve Ke" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="status" HeaderText="Status" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />--%>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kode Unit" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnik" runat="server" Text='<%# Bind("kdCabang") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Unit" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnis" runat="server" Text='<%# Bind("unit") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Approve Ke" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnisn" runat="server" Text='<%# Bind("apprKe") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Status" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNamaSiswa" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
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
