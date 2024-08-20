<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RListApproval.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RListApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="form-horizontal">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <div class="col-sm-1 control-label text-right">
                                            <span>Filter Cari</span>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboCabang" CssClass="form-control" runat="server" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboTransaksi" CssClass="form-control" runat="server" OnSelectedIndexChanged="cboTransaksi_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 overflow-x-table">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdListApproval" SkinID="GridView" runat="server" OnRowDataBound="grdListApproval_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="tahun" HeaderText="Tahun" HeaderStyle-CssClass="text-center" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="tglSimpan" HeaderText="Tgl Simpan" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" />
                                                <asp:BoundField DataField="kdTrans" HeaderText="Kode Transaksi" HeaderStyle-CssClass="text-center" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="tglTrans" HeaderText="Tgl Transaksi" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                                                <asp:BoundField DataField="cabang" HeaderText="Nama Cabang" HeaderStyle-CssClass="text-center" ItemStyle-Width="18%" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="namaUser" HeaderText="Nama User" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="levelApp" HeaderText="Level Appr ke-" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="tglApp" HeaderText="Tgl Appr" HeaderStyle-CssClass="text-center" ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" />
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
