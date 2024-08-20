<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="DownloadARSiswa.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.DownloadARSiswa" %>

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
            XLSX.writeFile(wb, fn || ('DownloadARSiswa.' + (type || 'xlsx')));
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
                                    <div class="col-sm-8">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tahun <span class="mandatory">*</span></label>
                                                <div class="col-sm-5">
                                                    <asp:DropDownList ID="cboTahun" runat="server" CssClass="form-control" Width="150" AutoPostBack="true" OnSelectedIndexChanged="cboTahun_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Bulan <span class="mandatory">*</span></label>
                                                <div class="col-sm-5">
                                                    <asp:DropDownList ID="cboBulan" runat="server" CssClass="form-control" Width="150" AutoPostBack="true" OnSelectedIndexChanged="cboBulan_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Perwakilan / Unit <span class="mandatory">*</span></label>
                                                <div class="col-sm-5">
                                                    <asp:DropDownList ID="cboPerwakilanUnit" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboPerwakilanUnit_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">&nbsp;</label>
                                                <div class="col-sm-5">
                                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                                </div>
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
                                                <asp:TemplateField HeaderText="No Virtual" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="noVirtual" runat="server" Text='<%# Eval("noVirtual").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nama Siswa" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="namaSiswa" runat="server" Text='<%# Eval("namaSiswa") %>'></asp:Label>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
