<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="ARSiswa.aspx.cs" Inherits="eFinance.Pages.Master.View.ARSiswa" %>

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
                        <%--<asp:DropDownList ID="cboPerwakilan" runat="server" CssClass="form-control"  Width="250" AutoPostBack="true" OnSelectedIndexChanged="cboPerwakilan_SelectedIndexChanged"></asp:DropDownList>--%>
                        <asp:DropDownList ID="cboCabang" runat="server" CssClass="form-control"  Width="250" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged"></asp:DropDownList>
                        <asp:DropDownList ID="cboKelas" runat="server" CssClass="form-control" Width="140"></asp:DropDownList>
                        <asp:DropDownList ID="cboJnsTrans" runat="server" CssClass="form-control" Width="180"></asp:DropDownList>
                        
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Periode :   </label>
                        <asp:DropDownList ID="cboMonth" runat="server" Width="120">
                            <asp:ListItem Value="1">Januari</asp:ListItem>
                            <asp:ListItem Value="2">Februari</asp:ListItem>
                            <asp:ListItem Value="3">Maret</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">Mei</asp:ListItem>
                            <asp:ListItem Value="6">Juni</asp:ListItem>
                            <asp:ListItem Value="7">Juli</asp:ListItem>
                            <asp:ListItem Value="8">Agustus</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">Oktober</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">Desember</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="cboYear" runat="server" Width="100">
                        </asp:DropDownList>
                        <asp:DropDownList ID="cbosts" runat="server" Width="120">
                            <asp:ListItem Value="1">Lunas</asp:ListItem>
                            <asp:ListItem Value="2">Belum Lunas</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button runat="server" ID="Button1" CssClass="btn btn-primary" Text="Search" OnClick="btnPosting_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdAccount" DataKeyNames="namaSiswa" SkinID="GridView" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="Nama Siswa" SortExpression="Ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("namaSiswa").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kelas" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="Grup" runat="server" Text='<%# Eval("kelas") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Jenis Transaksi" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="Grup" runat="server" Text='<%# Eval("Jenistransaksi") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai Invoice" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="Grup" runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai Bayar" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="Grup" runat="server" Text='<%# Eval("nilaibayar") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Saldo Piutang" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="Grup" runat="server" Text='<%# Eval("saldo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
