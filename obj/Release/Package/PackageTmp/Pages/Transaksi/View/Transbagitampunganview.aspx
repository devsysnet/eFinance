<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Transbagitampunganview.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.Transbagitampunganview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="TabName" runat="server" />
    <asp:Button ID="cmdMode" runat="server" OnClick="cmdMode_Click" Style="display: none;" Text="Mode" />
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group ">
                                    <div class="col-sm-8">
                                        <div class="form-group ">
                                             <div class="form-inline">
                                            <asp:TextBox ID="dtMulai" runat="server" CssClass="form-control date"></asp:TextBox>
                                            <asp:TextBox ID="dtSampai" runat="server" CssClass="form-control date"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group ">
                                            <div class="form-inline">
                                                <label>Filter :</label>
                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnCari_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdGajiKar" DataKeyNames="notampunganD" SkinID="GridView" runat="server" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdGajiKar_PageIndexChanging"
                                        OnSelectedIndexChanged="grdGajiKar_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                    <asp:BoundField DataField="jenisTampungan" HeaderText="Jenis" ItemStyle-Width="15%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="kdtran" HeaderText="Nomor Alokasi" ItemStyle-Width="15%" HeaderStyle-CssClass="text-center" />
                                                    <asp:BoundField DataField="nomorkode" HeaderText="Nomor Kode" ItemStyle-Width="15%" HeaderStyle-CssClass="text-center" />
                                                    <asp:BoundField DataField="namacabang" HeaderText="Unit" ItemStyle-Width="15%" HeaderStyle-CssClass="text-center" />
                                                    <asp:BoundField DataField="tgl" HeaderText="Tanggal" ItemStyle-Width="10%" HeaderStyle-CssClass="text-center" />
                                                    <asp:BoundField DataField="nilai" HeaderText="Nilai" ItemStyle-Width="10%" HeaderStyle-CssClass="text-center" />
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
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-horizontal">
                                     <div class="form-group">
                                        <label class="col-sm-5 control-label text-right">Jenis</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtjenis" placeholder="NUPTK" Enabled="false"></asp:TextBox>
                                            <asp:HiddenField ID="hdnotampungan" runat="server" />
                       
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-sm-5 control-label text-right">Nomor Kode</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNomorkode" placeholder="Nama" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-sm-5 control-label text-right">Kode Alokasi</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtkdtran" placeholder="Nama" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-sm-5 control-label text-right">Unit</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtUnit" placeholder="Nama" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                  

                                </div>
                            </div>
                            <%--pindah kolom--%>
                            <div class="col-sm-6">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <label class="col-sm-5 control-label text-right">Tanggal Masuk</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" CssClass="form-control date" ID="dtmasuk" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label text-right">Nilai</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" CssClass="form-control money" ID="txtNilai" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-sm-5 control-label text-right">Uraian</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtRemark" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                   
                                                          
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdSaldoGL" DataKeyNames="noTran" SkinID="GridView" runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:HiddenField ID="hdnNoRek" runat="server" Value='<%# Bind("noTran") %>' />
                                                        <asp:HiddenField ID="hdnnotkasdetil" runat="server" Value='<%# Bind("noTkasDetil") %>' />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" HeaderText="COA">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("ket") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="Nilai">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox ID="txtDebet" runat="server" CssClass="form-control money" Width="180" Text='<%# Bind("debet","{0:N2}") %>'></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group row">
                                <div class="col-md-12">
                                    <div class="text-center">
                                        <%--<asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses Simpan';"></asp:Button>--%>
                                        <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabView" runat="server" visible="false">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


