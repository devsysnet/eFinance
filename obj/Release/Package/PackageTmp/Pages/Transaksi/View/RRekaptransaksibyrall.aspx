<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RRekaptransaksibyrall.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RRekaptransaksibyrall" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                         <div class="row">
                            <div class="col-sm-12">
                                <div class="form-horizontal">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-1 control-label">Periode :</label>
                                            <div class="col-sm-6">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="dtMulai" runat="server" CssClass="form-control date"></asp:TextBox>&nbsp; 
                                                    <asp:TextBox ID="dtSampai" runat="server" CssClass="form-control date"></asp:TextBox>&nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-horizontal">
                                     <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Cabang : </label>
                                            <div class="col-sm-10">
                                               <asp:DropDownList ID="cboCabang" runat="server" CssClass="form-control" Width="350" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged"></asp:DropDownList>
                                                 <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                            </div>
                                        </div>
                                    </div>
                                    <%--<div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Transaksi : </label>
                                            <div class="col-sm-10">
                                                <div class="input-group">
                                                    <div class="form-inline">
                                                        <div class="input-group-btn">
                                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cbotransaksi" Width="150"></asp:DropDownList>
                                                           
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdRekap" DataKeyNames="ket" SkinID="GridView" runat="server" AllowPaging="false" PageSize="10" OnPageIndexChanging="grdRekap_PageIndexChanging" OnRowCommand="grdRekap_RowCommand">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="0%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%--<%# Container.DataItemIndex + 1 %>--%>
                                                        <asp:HiddenField ID="hdnId" runat="server" value='<%# Eval("noTransaksi") %>'/>
                                                        <asp:HiddenField ID="hdntgl" runat="server" value='<%# Eval("tanggal") %>'/>
                                                        <asp:HiddenField ID="hdnnorek" runat="server" value='<%# Eval("noRek") %>'/>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:BoundField DataField="Tgl" HeaderText="Tanggal" HeaderStyle-CssClass="text-center" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                            <%--<asp:BoundField DataField="jenisTransaksi" HeaderText="Jenis Transaksi" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />--%>
                                            <asp:BoundField DataField="Ket" HeaderText="Bank" HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="nilai" HeaderText="Nilai" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Detail" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control " ID="txjnstrs" placeholder="Jenis Transaksi" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kelas </label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control " ID="txtnkategori" placeholder="Kategori" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tagihan</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control " ID="txtcoadebet" placeholder="COA Debet" Enabled="false" ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Pembayaran</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control " ID="txtcoakredit" placeholder="COA Kredit" Enabled="false" ></asp:TextBox>
                                        </div>
                                    </div>
                                  <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Saldo</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control " ID="Textbank" placeholder="KodeVA" Enabled="false" ></asp:TextBox>
                                        </div>
                                    </div>
                                 <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Kembali" OnClick="btnCancel_Click"></asp:Button>
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
    <asp:LinkButton ID="LinkButton2" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgDetilKas" runat="server" PopupControlID="panelDetilKas" TargetControlID="LinkButton2" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelDetilKas" runat="server" Width="1200" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Detil Kas</h4>
                <asp:HiddenField runat="server" ID="hdnnotransaksi" />
                <asp:HiddenField runat="server" ID="hdnnorekdet"  />
                <asp:HiddenField runat="server" ID="hdntgldet"  />
                <asp:HiddenField runat="server" ID="hdnnocabangdet"  />
                <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>
            </div>
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <div class="modal-body col-overflow-500">
                
                <asp:GridView ID="GridView1" SkinID="GridView" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="Kode Transaksi" SortExpression="nomorKode" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="nomorKode" runat="server" Text='<%# Eval("nomorKode").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama Siswa" SortExpression="namaSiswa" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="namaSiswa" runat="server" Text='<%# Eval("namaSiswa").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NoVirtual" SortExpression="NoVirtual" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kelas" runat="server" Text='<%# Eval("novirtual").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kelas" SortExpression="kelas" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kelas" runat="server" Text='<%# Eval("kelas").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bulan Tagihan" SortExpression="bln" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="bln" runat="server" Text='<%# Eval("bln").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tahun" SortExpression="thn" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="thn" runat="server" Text='<%# Eval("thn").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NilaiTagihan" SortExpression="ptd" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="Nilai" runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai Bayar" SortExpression="ptd" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="Nilai" runat="server" Text='<%# Eval("nilaiBayar") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai Diskon" SortExpression="ptd" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="Nilai" runat="server" Text='<%# Eval("diskon") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <%--<asp:TemplateField HeaderText="Saldo" SortExpression="ptd" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="Nilai" runat="server" Text='<%# Eval("saldo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>

    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

