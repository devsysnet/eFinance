<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransPemusnahanAsetView.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TransPemusnahanAsetView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-inline">
                                    <div class="col-sm-12">
                                        <asp:TextBox ID="dtMulai" runat="server" CssClass="form-control date"></asp:TextBox>
                                        <asp:TextBox ID="dtSampai" runat="server" CssClass="form-control date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-6">
                                <div class="form-inline">
                                    <div class="col-sm-12">
                                        <asp:TextBox runat="server" ID="txtCariAset" CssClass="form-control"></asp:TextBox>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <asp:GridView ID="grdAsetUpdate" DataKeyNames="noAset" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdAsetUpdate_PageIndexChanging"
                                    OnRowCommand="grdAsetUpdate_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="tglMusnah" HeaderText="Tgl Pemusnahan" ItemStyle-Width="7%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="kodeAsset" HeaderText="Kode Aset" ItemStyle-Width="30%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="namaAsset" HeaderText="Nama Aset" ItemStyle-Width="20%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="alasanMusnah" HeaderText="Alasan Pemusnahan" ItemStyle-Width="25%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Detil" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Nama Barang Aset <span class="mandatory">*</span></label>
                                    <div class="form-inline">
                                        <div class="col-sm-9">
                                            <asp:HiddenField runat="server" ID="hdnBarangAset" />
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNamaBarangAset" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Kode Aset </label>
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" ID="txtKodeAset" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Lokasi Barang </label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="cboLokasi" CssClass="form-control" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Sub Lokasi </label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="cboSubLokasi" CssClass="form-control" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Tgl Pemusnahan <span class="mandatory">*</span></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox runat="server" ID="dtMusnah" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Alasan Pemusnahan <span class="mandatory">*</span></label>
                                    <div class="col-sm-7">
                                        <asp:TextBox runat="server" ID="txtAlasan" CssClass="form-control" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-md-12">
                                        <div class="text-center">
                                            <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click"></asp:Button>
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
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
