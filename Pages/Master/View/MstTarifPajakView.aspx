<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstTarifPajakView.aspx.cs" Inherits="eFinance.Pages.Master.View.MstTarifPajakView" %>
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
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" MaxLength="50" PlaceHolder="Nama tarifpajak"></asp:TextBox>
                                        <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnCari_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <asp:GridView ID="grdtarifpajak" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdtarifpajak_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="tarifpajak" SortExpression="tarifpajak" HeaderText="Dari Penghasilan" ItemStyle-Width="30%" />
                                        <asp:BoundField DataField="tarifpajak1" SortExpression="tarifpajak1" HeaderText="Sampai Penghasilan" ItemStyle-Width="30%" />
                                        <asp:BoundField DataField="persen" SortExpression="persen" HeaderText="Persen" ItemStyle-Width="30%" />
                                        <asp:BoundField DataField="tingkat" SortExpression="tingkat" HeaderText="Tingkat" ItemStyle-Width="30%" />
                                        <asp:BoundField DataField="sts" SortExpression="sts" HeaderText="STATUS" ItemStyle-Width="25%" />
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