<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RTracking.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RTracking" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-horizontal">
                                    <div class="col-sm-8">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Cabang : </label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Tahun : </label>
                                            <div class="col-sm-9">
                                                <div class="input-group">
                                                    <div class="form-inline">
                                                        <div class="input-group-btn">
                                                            <asp:DropDownList ID="cboTahun" runat="server" CssClass="form-control"></asp:DropDownList>&nbsp;
                                                            <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdTracking" SkinID="GridView" runat="server" AutoGenerateColumns="false" OnDataBound="grdTracking_DataBound" OnRowDataBound="grdTracking_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="namaCabang" HeaderText="Perwakilan / Unit" ItemStyle-Width="300px" />
                                            <asp:BoundField DataField="ket" HeaderText="Akun" ItemStyle-Width="350px" />
                                            <asp:BoundField DataField="M1" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="K1" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="M2" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="K2" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="M3" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="K3" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="M4" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="K4" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="M5" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="K5" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="M6" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="K6" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="M7" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="K7" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="M8" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="K8" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="M9" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="K9" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="M10" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="K10" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="M11" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="K11" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="M12" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="K12" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
