<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="DownloadFormatMandiri.aspx.cs" Inherits="eFinance.Pages.Transaksi.Upload.DownloadFormatMandiri" %>
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
                                 <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Width="300"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                                <asp:DropDownList runat="server" CssClass="form-control" ID="cboKelas" Width="200"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                                 <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdSiswa"  SkinID="GridView" runat="server" AllowPaging="False" PageSize="100" OnPageIndexChanging="grdSiswa_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <%--<asp:HiddenField ID="hdnJmlTunggakan" runat="server" Value='<%# Bind("tanda") %>' />--%>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="NoVA" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnik" runat="server" Text='<%# Bind("no_va") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Currency" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnis" runat="server" Text='<%# Bind("currency") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Siswa" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblkelas" runat="server" Text='<%# Bind("nama") %>'>></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Sekolah" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblkelas" runat="server" Text='<%# Bind("sekolah") %>'>></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kelas" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblkelas" runat="server" Text='<%# Bind("kelas") %>'>></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Keterangan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblkelas" runat="server" Text='<%# Bind("keterangan") %>'>></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Periode Open" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblkelas" runat="server" Text='<%# Bind("periode_open") %>'>></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Periode Close" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblkelas" runat="server" Text='<%# Bind("periode_close") %>'>></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="subbill_01" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblkelas" runat="server" Text='<%# Bind("subbill_01") %>'>></asp:Label>
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
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>