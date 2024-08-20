<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RKasView.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RKasView" %>
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
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                                         <div class="form-horizontal">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Cabang : </label>
                                        <div class="col-sm-10">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged" Width="250"></asp:DropDownList>&nbsp;&nbsp;
                                             <asp:DropDownList ID="cboYear" runat="server" Width="100"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Search" OnClick="btnPosting_Click"></asp:Button>
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12">
                             <div class="form-group" runat="server" id="divtombol">
                     <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>
                </div>
                            <div class="table-responsive">
                                <asp:GridView ID="grdHarianGL" DataKeyNames="norek" SkinID="GridView" runat="server" AllowPaging="false" PageSize="10" OnPageIndexChanging="grdHarianGL_PageIndexChanging" OnRowCommand="grdHarianGL_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField runat="server" ID="hdnbln" Value='<%# Eval("bln") %>' />
                                                    <asp:HiddenField runat="server" ID="hdnnorek" Value='<%# Eval("norek") %>' />
                                                    <asp:HiddenField runat="server" ID="hdnnocabang" Value='<%# Eval("nocabang") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="kdrek" SortExpression="kdrek" HeaderText="Kode Akun" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="center" />
                                        <asp:BoundField DataField="ket" SortExpression="ket" HeaderText="Akun" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="namaCabang" SortExpression="namaCabang" HeaderText="Cabang" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left" />
                                        <asp:BoundField DataField="bln" SortExpression="bln" HeaderText="Bulan" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left" />
                                        <asp:BoundField DataField="totalmskkas" SortExpression="totalmskkas" HeaderText="Total Masuk Kas" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="totalklrkas" SortExpression="totalklrkas" HeaderText="Total Keluar Kas" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="totalmskgl" SortExpression="totalmskgl" HeaderText="Total Masuk Debet GL" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="totalklrgl" SortExpression="totalklrgl" HeaderText="Total Keluar Debet GL" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="selesihdebet" SortExpression="selesihdebet" HeaderText="Selisih Debet" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="selisihkredit" SortExpression="selisihkredit" HeaderText="Selisih Kredit" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />
                            
                                        <%--<asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
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
                                        </asp:TemplateField>--%>
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
        </div>
    </div>

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
                <asp:HiddenField runat="server" ID="hdnblnHarianKas" />
                                                    <asp:HiddenField runat="server" ID="hdnthnHarianKas"  />
                                                    <asp:HiddenField runat="server" ID="hdnnorekHarianKas"  />
                                                    <asp:HiddenField runat="server" ID="hdnnocabangHarianKas"  />
                <h4 class="modal-title">Bulan <asp:Label runat="server" ID="lblbln"></asp:Label> Tahun <asp:Label runat="server" ID="lblthn"></asp:Label>, Rekening <asp:Label runat="server" ID="lblrek"></asp:Label>, Unit <asp:Label runat="server" ID="lblunit"></asp:Label></h4>
               
                <asp:GridView ID="grdAccount" DataKeyNames="nomorkode" SkinID="GridView" runat="server">

                    <Columns>
                        <asp:TemplateField HeaderText="Nomor Kode" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                         
                                <asp:Label ID="nomorkode" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tanggal" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="left">
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
                                <asp:Label ID="uraian" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Debet" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="debet" runat="server" Text='<%# Eval("debet") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kredit" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="kredit" runat="server" Text='<%# Eval("kredit") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Saldo Akhir" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
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
                                <asp:Label ID="kdrek" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="Ket" runat="server" ></asp:Label>
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
                                <asp:Label ID="kdrek" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="Keterangan" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="ket" runat="server" ></asp:Label>
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
                                <asp:Label ID="kdrek" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="Keterangan" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="ket" runat="server"></asp:Label>
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
                                <asp:Label ID="kdrek" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="Keterangan" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="ket" runat="server"></asp:Label>
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
                                <asp:Label ID="kdrek" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="Ket" runat="server"></asp:Label>
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
                                <asp:Label ID="kdrek" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="Keterangan" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="ket" runat="server"></asp:Label>
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
        </Triggers>
        </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

