<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransLokasiAssetView.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TransLokasiAssetView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdLokasiAssetView.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="form-horizontal">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <div class="col-sm-1 control-label">
                                            <span>Filter Cari</span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboCabang" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboLokasi" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboLokasi_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboSubLokasi" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboSubLokasi_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" placeholder="Masukan kata"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 overflow-x-table">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdLokasiAssetView" DataKeyNames="noAset" SkinID="GridView" runat="server" >
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="kodeAsset" HeaderText="Kode Aset" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="namaAsset" HeaderText="Nama Aset Baru" HeaderStyle-CssClass="text-center" ItemStyle-Width="18%" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="lokasi" HeaderText="Lokasi" HeaderStyle-CssClass="text-center" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="subLokasi" HeaderText="Sub Lokasi" HeaderStyle-CssClass="text-center" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="coaDebit" HeaderText="COA (Debit)" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="coaKredit" HeaderText="COA (Kredit)" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="button" runat="server">
                                <div class="col-sm-12 ">
                                    <div class="text-center">
                                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="tabForm" runat="server">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
