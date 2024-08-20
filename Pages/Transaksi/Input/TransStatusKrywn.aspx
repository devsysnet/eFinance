<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransStatusKrywn.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransStatusKrywn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <%--START GRID--%>
    <asp:HiddenField ID="hdnnokaryawanH" runat="server" />
    <asp:HiddenField ID="hdnStatusH" runat="server" />
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdStsKrywn" DataKeyNames="noKaryawan" SkinID="GridView" runat="server" OnSelectedIndexChanged="grdStsKrywn_SelectedIndexChanged" OnPageIndexChanging="grdStsKrywn_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnnoKrywn" runat="server" Value='<%# Eval("noKaryawan") %>' />
                                    <asp:HiddenField ID="hdnStatus" runat="server" Value='<%# Eval("Status") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="idPeg" HeaderText="Kode" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="nama" HeaderText="Nama Lengkap" HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" />
                        <asp:BoundField DataField="tgllahir" HeaderText="Tgl Lahir" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                        <asp:BoundField DataField="tglmasuk" HeaderText="Tgl Masuk" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                        <asp:BoundField DataField="status" HeaderText="Status" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Detil" CommandName="Select" />
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
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">ID Karyawan</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Nama Karyawan</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="Txtnama" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Tempat, Tanggal Lahir</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="Txtttlahir" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">NIP</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="Txtnik" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Tanggal Masuk</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control date" ID="dtMasuk"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Tanggal <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control date" ID="dtInput"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Status Awal</label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="cboStsPegawai" CssClass="form-control" runat="server"  Enabled="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Status Akhir</label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="cboStsPegawaiAkhir" CssClass="form-control" runat="server"  Enabled="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Keterangan</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox TextMode="MultiLine" runat="server" ID="txtKeterangan" CssClass="form-control" Rows="3"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click"></asp:Button>
                                <asp:Button runat="server" ID="Button1" CssClass="btn btn-danger" Text="Batal" OnClick="Button1_Click"></asp:Button>
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
