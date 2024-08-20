<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Rdepositoyysn.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.Rdepositoyysn" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function OpenReport() {
            //            OpenReportViewer();
            window.open('../../../Report/ReportViewer.aspx', 'name', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,modal=yes,width=1000,height=600');
        }

    </script>
    <asp:HiddenField ID="hdnYayasan" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="col-sm-12">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Filter :   </label>
                         <asp:DropDownList ID="cboCabang" runat="server" CssClass="form-control"  Width="280"></asp:DropDownList>
                         <asp:DropDownList runat="server" AutoPostBack="false"  CssClass="form-control" ID="cbojenissrt" Width="200"></asp:DropDownList>
                         <asp:DropDownList ID="cbocair" runat="server" CssClass="form-control" Enabled="true" Width="150">
                         <asp:ListItem Value="0">Belum Cair</asp:ListItem>
                         <asp:ListItem Value="1">Cair</asp:ListItem></asp:DropDownList>&nbsp;&nbsp;
                         <asp:Button runat="server" ID="Button1" CssClass="btn btn-primary" Text="Search" OnClick="btnPosting_Click"></asp:Button>
                      
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdAccount" DataKeyNames="kdTransaksi" SkinID="GridView" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="Kode Transaksi" SortExpression="Ket" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kdTransaksi" runat="server" Text='<%# Eval("kdTransaksi") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nomor" SortExpression="Nomor" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="LEFT">
                            <ItemTemplate>
                                <asp:Label ID="nomor" runat="server" Text='<%# Eval("nomor") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nomor Pendaftaran" SortExpression="nomorpendaftaran" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="LEFT">
                            <ItemTemplate>
                                <asp:Label ID="nomorpendaftaran" runat="server" Text='<%# Eval("nomorpendaftaran") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bank" SortExpression="Bank" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="LEFT">
                            <ItemTemplate>
                                <asp:Label ID="NamaBank" runat="server" Text='<%# Eval("NamaBank") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tanggal Deposito" SortExpression="tglDeposito" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="tglDeposito" runat="server" Text='<%# Eval("tglDeposito") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tanggal Cair" SortExpression="tglJatuhTempo" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="tglJatuhTempo" runat="server" Text='<%# Eval("tglJatuhTempo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai" SortExpression="nominal" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="nominal" runat="server" Text='<%# Eval("nominal") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Jenis Bunga" SortExpression="jnsbunga" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="jnsbunga" runat="server" Text='<%# Eval("jnsbunga") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bunga" SortExpression="bunga" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="bunga" runat="server" Text='<%# Eval("bunga") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Deskripsi" SortExpression="ptd" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="deskripsi" runat="server" Text='<%# Eval("deskripsi") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>--%>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
