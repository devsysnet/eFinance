<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstKomponengajiView.aspx.cs" Inherits="eFinance.Pages.Master.View.MstKomponengajiView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-8">
                                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Hapus semua terpilih" Visible="false" />
                            </div>
                            <div class="col-sm-4">
                                <div class="form-inline">
                                    <div class="col-sm-12">
                                        <label>Search : </label>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" MaxLength="50" PlaceHolder="Nama komponengaji"></asp:TextBox>
                                        <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnCari_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <asp:GridView ID="grdkomponengaji" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdkomponengaji_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="komponengaji" SortExpression="komponengaji" HeaderText="Komponen Gaji" ItemStyle-Width="30%" />
                                        <asp:BoundField DataField="kategori" SortExpression="kategori" HeaderText="Kategori" ItemStyle-Width="20%" />
                                        <asp:BoundField DataField="jenis" SortExpression="persenkaryawan" HeaderText="Jenis" ItemStyle-Width="20%" />
                                        <asp:BoundField DataField="pph21" SortExpression="persenkaryawan" HeaderText="Unsur PPH21" ItemStyle-Width="20%" />
                                         <asp:BoundField DataField="penambah" SortExpression="persenkaryawan" HeaderText="Unsur Absesni" ItemStyle-Width="20%" />
                                        <asp:BoundField DataField="bpjs" SortExpression="persenkaryawan" HeaderText="Unsur Iuran" ItemStyle-Width="20%" />
                                        <asp:BoundField DataField="sts" SortExpression="sts" HeaderText="STATUS" ItemStyle-Width="20%" />
                                    </Columns>
                                </asp:GridView>
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
