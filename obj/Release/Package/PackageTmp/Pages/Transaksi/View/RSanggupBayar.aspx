<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RSanggupBayar.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RSanggupBayar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="col-sm-12">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <asp:DropDownList ID="cboCabang" class="form-control" runat="server" Width="350" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged"></asp:DropDownList>
                        <asp:DropDownList ID="cboTA" class="form-control" runat="server" Width="200"></asp:DropDownList>
                        <asp:DropDownList ID="cboKelas" class="form-control" runat="server" Width="200"></asp:DropDownList>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdSanggupBayar" SkinID="GridView" runat="server" AllowPaging="false" PageSize="10" OnPageIndexChanging="grdSanggupBayar_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="nis" HeaderText="NIS" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="namaSiswa" HeaderText="Nama Lengkap" HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" />
                        <asp:BoundField DataField="novirtual" HeaderText="No VA" HeaderStyle-CssClass="text-center" ItemStyle-Width="12%" />
                        <asp:BoundField DataField="spp" HeaderText="SPP /tahun" HeaderStyle-CssClass="text-center" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                        <asp:BoundField DataField="uangkegiatan" HeaderText="Uang Kegiatan /tahun" HeaderStyle-CssClass="text-center" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                        <asp:BoundField DataField="saldo" HeaderText="Saldo" HeaderStyle-CssClass="text-center" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
