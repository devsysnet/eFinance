<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransKomponenGaji.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TransKomponenGaji" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript" src="https://unpkg.com/xlsx@0.15.1/dist/xlsx.full.min.js"></script>
<script type="text/javascript">
    function ExportToExcel(type, fn, dl) {
        var elt = document.getElementById('BodyContent_grdDownloadARSiswa');
        var wb = XLSX.utils.table_to_book(elt, { sheet: "sheet1" });
        return dl ?
            XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
            XLSX.writeFile(wb, fn || ('komponengaji.' + (type || 'xlsx')));
    }
</script>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-body">
                            <div id="tabGrid" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div>
                                            <div class="form-group">
                                                 <label>Periode :  </label>
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
                                                <%--<asp:DropDownList ID="cboMonth1" runat="server" Width="120">
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

                                                </asp:DropDownList>--%>&nbsp;&nbsp;
                                                <label for="pjs-ex1-fullname">Tahun :</label>
                                                    <asp:DropDownList ID="cboTahun" runat="server" CssClass="form-control" Width="150" AutoPostBack="true" OnSelectedIndexChanged="cboTahun_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;
                                                <label>Cabang :</label>
                                                  <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Width="200" ></asp:DropDownList>&nbsp;
                                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />

                                            </div>
                                            
                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="tabForm" runat="server">
                                <div class="row">
                                    <div class="col-sm-12 overflow-x-table">
                                        <button onclick="ExportToExcel('xlsx')" class="btn btn-success btn-sm">Download</button>
                                        <br />
                                        <asp:GridView ID="grdDownloadARSiswa" SkinID="GridView" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Nama Karyawan" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="noVirtual" runat="server" Text='<%# Eval("nama").ToString() %>' Width="30%"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <%-- <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="namaSiswa" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>