<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstCabangView.aspx.cs" Inherits="eFinance.Pages.Master.View.MstCabangView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnID" />
     <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-8">
                            </div>
                            <div class="col-sm-4">
                                <div class="form-inline">
                                    <div class="col-sm-12">
                                        <label>Filter :</label>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"  />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdCabang" DataKeyNames="noCabang" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdCabang_PageIndexChanging"
                                        OnSelectedIndexChanged="grdCabang_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="kdCabang" HeaderStyle-CssClass="text-center" HeaderText="Kode Cabang" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="namaCabang" HeaderStyle-CssClass="text-center" HeaderText="Nama Cabang" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="namaOfficerFin" HeaderStyle-CssClass="text-center" HeaderText="Nama Office" ItemStyle-Width="20%" />
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Detail" CommandName="Select" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="form-horizontal">
                            <div class="col-sm-12">
                                  <div class="form-group">
                                    <label class="col-sm-4 text-right">Kategori Usaha</label>
                                    <div class="col-sm-6">
                                        <asp:Label runat="server" ID="lblkategoriusaha"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 text-right">Nama Cabang</label>
                                    <div class="col-sm-6">
                                        <asp:Label runat="server" ID="lblNama"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 text-right">Alamat</label>
                                    <div class="col-sm-6">
                                        <asp:Label runat="server" ID="lblAlamat" Height="60"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 text-right">Kota</label>
                                    <div class="col-sm-6">
                                        <asp:Label runat="server" ID="lblKota"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 text-right">Kode Pos</label>
                                    <div class="col-sm-2">
                                        <asp:Label runat="server" ID="lblKodePos"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 text-right">No Telpon</label>
                                    <div class="col-sm-6">
                                        <asp:Label runat="server" ID="lblNoTelp"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 text-right">No Fax</label>
                                    <div class="col-sm-6">
                                        <asp:Label runat="server" ID="lblNoFax"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 text-right">Email</label>
                                    <div class="col-sm-6">
                                        <asp:Label runat="server" ID="lblEmail"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 text-right">Office Finance</label>
                                    <div class="col-sm-6">
                                        <asp:Label runat="server" ID="lblOffice"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 text-right">Status</label>
                                    <div class="col-sm-6">
                                        <asp:Label runat="server" ID="lblStatus"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 text-right">Menggunakan MHS</label>
                                    <div class="col-sm-6">
                                        <asp:Label runat="server" ID="labelmhs"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 text-right">Pelunasan Semua Jenis Transaksi</label>
                                    <div class="col-sm-6">
                                        <asp:Label runat="server" ID="labelallpelunasan"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 text-right">Cetak Voucher</label>
                                    <div class="col-sm-6">
                                        <asp:Label runat="server" ID="labelcetakvoucher"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="form-row">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <asp:Button ID="btnBack" runat="server" CssClass="btn btn-danger" Text="Back" OnClick="btnBack_Click" />
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
    </div>
</asp:Content>
