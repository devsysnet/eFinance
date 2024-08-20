<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Rbalancekas.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.Rbalancekas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdHarianGL.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[5].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
        <asp:HiddenField ID="hdnnoYysn" runat="server" />

    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="col-sm-7">
                                    <div class="form-group">
                                      <label>Perwakilan : </label>

                                            <asp:DropDownList ID="cboPerwakilan" runat="server" Width="250" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboPerwakilan_SelectedIndexChanged"></asp:DropDownList>

                        <label>Unit : </label>
                        <asp:DropDownList ID="cboCabang" Width="200" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboUnit_SelectedIndexChanged"></asp:DropDownList>
                            <asp:DropDownList ID="cboYear" runat="server" Width="100"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-5">
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Account : </label>
                                        <div class="col-sm-10">
                                            <div class="input-group">
                                                <div class="form-inline">
                                                    <div class="input-group-btn">
                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="cboAccount"></asp:DropDownList>
                                                        <asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Search" OnClick="btnPosting_Click"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <asp:GridView ID="grdHarianGL" DataKeyNames="norek" SkinID="GridView" runat="server" AllowPaging="false" PageSize="10" OnPageIndexChanging="grdHarianGL_PageIndexChanging" OnRowCommand="grdHarianGL_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField runat="server" ID="hdnbulan" Value='<%# Eval("bulan") %>' />
                                                    <asp:HiddenField runat="server" ID="hdnbln" Value='<%# Eval("bln") %>' />
                                                    <asp:HiddenField runat="server" ID="hdnthn" Value='<%# Eval("tahun") %>' />
                                                    <asp:HiddenField runat="server" ID="hdnnorek" Value='<%# Eval("norek") %>' />
                                                    <asp:HiddenField runat="server" ID="hdnnocabang" Value='<%# Eval("nocabang") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ket" SortExpression="ket" HeaderText="Rekening" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="bulan" SortExpression="bulan" HeaderText="Bulan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="saldoawal" SortExpression="saldoawal" HeaderText="Saldo Awal" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="penerimaan" SortExpression="penerimaan" HeaderText="Penerimaan" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="pengeluaran" SortExpression="pengeluaran" HeaderText="Pengeluaran" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="saldoakhir" SortExpression="saldoakhir" HeaderText="Saldo Akhir" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="tglmax" SortExpression="tglmax" HeaderText="Tanggal Posting" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left" />
                                        <asp:BoundField DataField="tglmaxmsk" SortExpression="tglmaxmsk" HeaderText="Tgl Terakhir Input Penerimaan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left" />
                                        <asp:BoundField DataField="tglmaxklr" SortExpression="tglmaxklr" HeaderText="Tgl Terakhir Input Pengeluaran" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left" />
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Button ID="btnSelectHarian" runat="server" class="btn btn-xs btn-labeled btn-info" Text="Harian Kas" CommandName="SelectHarian" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Button ID="btnSelectDetail" runat="server" class="btn btn-xs btn-labeled btn-success" Text="Detil Kas" CommandName="SelectDetail" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Button ID="btnSelectkas" runat="server" class="btn btn-xs btn-labeled btn-success" Text="Bulanan" CommandName="SelectDetailkas" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectDetailRekapTransaksi" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Detail" CommandName="Detail" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="Saldo">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label ID="nilai" runat="server" Text='<%# Bind("nilai", "{0:#,0.00}") %>' Width="180"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <%--<asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Posting" OnClick="btnPosting_Click"></asp:Button>--%>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
                <div id="tabForm" runat="server">
                    <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdRekap" DataKeyNames="noTransaksi" SkinID="GridView" runat="server" AllowPaging="false" PageSize="10"  OnRowCommand="grdRekap_RowCommand">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="0%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%--<%# Container.DataItemIndex + 1 %>--%>
                                                        <asp:HiddenField ID="hdnId" runat="server" value='<%# Eval("noTransaksi") %>'/>
                                                        <asp:HiddenField ID="hdntgl" runat="server" value='<%# Eval("tanggal") %>'/>
                                                    <%--    <asp:HiddenField ID="hdnnorekRTransaksi" runat="server" value='<%# Eval("noRek") %>'/>--%>
                                                        <asp:HiddenField ID="hdnNocabangRTransaksi" runat="server" value='<%# Eval("noCabang") %>'/>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:BoundField DataField="Tgl" HeaderText="Tanggal" HeaderStyle-CssClass="text-center" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="jenisTransaksi" HeaderText="Jenis Transaksi" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Ket" HeaderText="Bank" HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="nilai" HeaderText="Nilai" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectDetailRekapTransaksi" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Detail" CommandName="SelectDetailRekapTransaksi" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    <div class="form-group row">
                                <div class="col-md-12">
                                    <div class="text-center">
                                      <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Kembali" CausesValidation="false" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                            </div>
                </div>
                 <div id="divHariankas" runat="server">
                    <div class="row">
                        <div class="form-inline">
                                       
                                    </div>
                            <div class="col-sm-12">
                                <div class="form-group" runat="server" id="div1">
                                                                                        <asp:HiddenField runat="server" ID="hdnblnHarianKas"  />
                                                        <asp:HiddenField runat="server" ID="hdnthnHarianKas"  />
                                                    <asp:HiddenField runat="server" ID="hdnnorekHarianKas"  />
                                                    <asp:HiddenField runat="server" ID="hdnnocabangHarianKas"  />
                     <asp:Button runat="server" ID="Button1" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>
                </div> 
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView5" DataKeyNames="nomorkode" SkinID="GridView" runat="server"  >
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="0%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%--<%# Container.DataItemIndex + 1 %>--%>
                                                      <%--  <asp:HiddenField ID="hdnId" runat="server" value='<%# Eval("noTransaksi") %>'/>
                                                        <asp:HiddenField ID="hdntgl" runat="server" value='<%# Eval("tanggal") %>'/>--%>
                                                    <%--    <asp:HiddenField ID="hdnnorekRTransaksi" runat="server" value='<%# Eval("noRek") %>'/>--%>
                                                       <%-- <asp:HiddenField ID="hdnNocabangRTransaksi" runat="server" value='<%# Eval("noCabang") %>'/>--%>   
    
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:BoundField DataField="nomorkode" HeaderText="Nomor Kode" HeaderStyle-CssClass="text-center" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="tgl" HeaderText="Tanggal" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="tipe" HeaderText="Tipe" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="cust" HeaderText="Dari/Untuk" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="uraian" HeaderText="Uraian" HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="debet" HeaderText="Debet" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right" />
                                            <asp:BoundField DataField="kredit" HeaderText="Kredit" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right" />
                                            <asp:BoundField DataField="Saldoakhir" HeaderText="Saldo Akhir" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right" />
                                            <%--<asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectDetailRekapTransaksi" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Detail" CommandName="SelectDetailRekapTransaksi" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                                        <div class="form-group row">
                                <div class="col-md-12">
                                    <div class="text-center">
                                      <asp:Button ID="Button2" runat="server" CssClass="btn btn-danger" Text="Kembali" CausesValidation="false" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                            </div>
                </div>
            </div>
        </div>
    </div>
    
    <asp:LinkButton ID="LinkButton4" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgDetilKasRTransaksi" runat="server" PopupControlID="panelDetilKasRTransaksi" TargetControlID="LinkButton4" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelDetilKasRTransaksi" runat="server" Width="1200" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Detil Kas</h4>
            </div>
            <asp:Label ID="Label5" runat="server"></asp:Label>
            <div class="modal-body col-overflow-500">
                
                <asp:GridView ID="GridView4" SkinID="GridView" runat="server">
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
    </asp:Panel>
    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgHarianKas" runat="server" PopupControlID="panelHarianKas" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelHarianKas" runat="server" Width="1200" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Buku Harian Kas</h4>
            </div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="modal-body col-overflow-500">
 
                <h4 class="modal-title">Bulan <asp:Label runat="server" ID="lblbln"></asp:Label> Tahun <asp:Label runat="server" ID="lblthn"></asp:Label>, Rekening <asp:Label runat="server" ID="lblrek"></asp:Label>, Unit <asp:Label runat="server" ID="lblunit"></asp:Label></h4>
                <div class="form-group" runat="server" id="divtombol">
                     <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>
                </div>
                <asp:GridView ID="grdAccount" DataKeyNames="nomorkode" SkinID="GridView" runat="server">

                    <Columns>
                        <asp:TemplateField HeaderText="Nomor Kode" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>            
                             <asp:Label ID="nomorkode" runat="server" Text='<%# Eval("nomorkode").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>            
                             <asp:Label ID="nomorkode1" runat="server" Text='<%# Eval("cabangasal").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tanggal" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("tgl") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipe" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="jenis" runat="server" Text='<%# Eval("tipe") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dari/Untuk" SortExpression="Ket" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="cust" runat="server" Text='<%# Eval("cust") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Uraian" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="uraian" runat="server" Text='<%# Eval("uraian").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Debet" SortExpression="ptd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="debet" runat="server" Text='<%# Eval("debet") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kredit" SortExpression="ptd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="kredit" runat="server" Text='<%# Eval("kredit") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Saldo Akhir" SortExpression="ptd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="kredit" runat="server" Text='<%# Eval("Saldoakhir") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton2" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgDetilKas" runat="server" PopupControlID="panelDetilKas" TargetControlID="LinkButton2" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelDetilKas" runat="server" Width="1200" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Detil Kas</h4>
            </div>
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <div class="modal-body col-overflow-500">
                <h4 class="modal-title">Bulan <asp:Label runat="server" ID="lblbln2"></asp:Label> Tahun <asp:Label runat="server" ID="lblthn2"></asp:Label>, Unit <asp:Label runat="server" ID="lblunit2"></asp:Label></h4>
                <asp:GridView ID="GridView1" DataKeyNames="kdrek" SkinID="GridView" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="COA" SortExpression="kdrek" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai" SortExpression="ptd" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="Nilai" runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:GridView ID="grdAccount1" DataKeyNames="kdrek" SkinID="GridView" runat="server" ShowHeader="false">
                    <Columns>
                        <asp:TemplateField HeaderText="COA" SortExpression="COA" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="Keterangan" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="ket" runat="server" Text='<%# Eval("ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai" SortExpression="nilai" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="nilai" runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:GridView ID="grdAccount2" DataKeyNames="kdrek" SkinID="GridView" runat="server" ShowHeader="false">
                    <Columns>
                        <asp:TemplateField HeaderText="COA" SortExpression="COA" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="Keterangan" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="ket" runat="server" Text='<%# Eval("ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai" SortExpression="nilai" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="nilai" runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:GridView ID="grdAccount3" DataKeyNames="kdrek" SkinID="GridView" runat="server" ShowHeader="false">
                    <Columns>
                        <asp:TemplateField HeaderText="COA" SortExpression="COA" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="Keterangan" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="ket" runat="server" Text='<%# Eval("ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai" SortExpression="nilai" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="nilai" runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton3" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="grddeatailbln" runat="server" PopupControlID="panel1" TargetControlID="LinkButton3" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panel1" runat="server" Width="1200" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Buku Harian Kas</h4>
            </div>
            <asp:Label ID="Label2" runat="server"></asp:Label>
            <div class="modal-body col-overflow-500">
                <h4 class="modal-title">Bulan <asp:Label runat="server" ID="Label3"></asp:Label> Tahun <asp:Label runat="server" ID="Label4"></asp:Label>, Unit <asp:Label runat="server" ID="Label6"></asp:Label></h4>
                <asp:GridView ID="GridView2" DataKeyNames="kdrek" SkinID="GridView" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="COA" SortExpression="kdrek" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai" SortExpression="ptd" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="Nilai" runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:GridView ID="GridView3" DataKeyNames="kdrek" SkinID="GridView" runat="server" ShowHeader="false">
                    <Columns>
                        <asp:TemplateField HeaderText="COA" SortExpression="COA" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="Keterangan" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="ket" runat="server" Text='<%# Eval("ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai" SortExpression="nilai" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="nilai" runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:PostBackTrigger ControlID="Button1" />
                 
        </Triggers>
        </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
