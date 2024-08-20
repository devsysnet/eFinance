<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstJabatanView.aspx.cs" Inherits="eFinance.Pages.Master.View.MstJabatanView" %>
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
                                        <label>Filter : </label>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" ></asp:TextBox>
                                        <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnCari_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <asp:GridView ID="grdJabatan" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdJabatan_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Jabatan" HeaderStyle-CssClass="text-center" HeaderText="Jabatan" ItemStyle-Width="30%" />
                                        <asp:BoundField DataField="setjammasuk" HeaderStyle-CssClass="text-center" HeaderText="Jam Masuk" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"  />
                                        <asp:BoundField DataField="setjamkeluar" HeaderStyle-CssClass="text-center" HeaderText="Jam Keluar" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"  />
                                        <asp:BoundField DataField="potongan" HeaderStyle-CssClass="text-center" HeaderText="Ada Potongan Keterlambatan" ItemStyle-Width="20%" />
                                        <asp:BoundField DataField="sts" HeaderStyle-CssClass="text-center" HeaderText="Status" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
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