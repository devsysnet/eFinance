<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RPR.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RPR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                                                <div class="col-sm-2">
                                                    <asp:DropDownList ID="cboYear" class="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="cboCabang" class="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                                    <asp:Button ID="btnConvertExcel" runat="server" CssClass="btn btn-success" Text="Unduh" OnClick="btnConvertExcel_Click" />
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 overflow-x-table">
                                            <div class="table-responsive">
                                                <asp:GridView ID="grdPRView" SkinID="GridView" Width="1300" runat="server" OnRowDataBound="grdPRView_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="tgl_PR" HeaderText="Tgl PR" HeaderStyle-CssClass="text-center" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d-MMM-yyyy}" />
                                                        <asp:BoundField DataField="Kode_PR" HeaderText="Kode PR" HeaderStyle-CssClass="text-center" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Peminta" HeaderText="Peminta" HeaderStyle-CssClass="text-center" HeaderStyle-Width="250" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Peruntukan_untuk" HeaderText="Peruntukan untuk" HeaderStyle-CssClass="text-center" HeaderStyle-Width="150" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Barang" HeaderText="Barang" HeaderStyle-CssClass="text-center" HeaderStyle-Width="300" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Qty" HeaderText="Qty" HeaderStyle-CssClass="text-center" HeaderStyle-Width="50" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" />
                                                        <asp:BoundField DataField="Harga_Satuan" HeaderText="Harga Satuan" HeaderStyle-CssClass="text-center" HeaderStyle-Width="120" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                                        <asp:BoundField DataField="Total" HeaderText="Total" HeaderStyle-CssClass="text-center" HeaderStyle-Width="120" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                                        <asp:BoundField DataField="SPesifikasi" HeaderText="Spesifikasi" HeaderStyle-CssClass="text-center" HeaderStyle-Width="150" ItemStyle-HorizontalAlign="Left" />
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
            </div>
            <div id="tabForm" runat="server" visible="false">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnConvertExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

