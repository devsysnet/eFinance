<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RLedger1.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RLedger1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function OpenReport() {
            //            OpenReportViewer();
            window.open('../../../Report/ReportViewer.aspx', 'name', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,modal=yes,width=1000,height=600');
        }

    </script>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="col-sm-8">
                <div class="form-inline">
                    <div class="col-sm-8">
                        <label>Periode :  </label>
                        <asp:DropDownList ID="cboMonth" runat="server" Width="120">
                            <asp:ListItem Value="1">Januari</asp:ListItem>
                            <asp:ListItem Value="2">Februari</asp:ListItem>
                            <asp:ListItem Value="3">Maret</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">Mei</asp:ListItem>
                            <asp:ListItem Value="6">Juni</asp:ListItem>
                            <asp:ListItem Value="7">Juli</asp:ListItem>
                            <asp:ListItem Value="8">Agustus</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">Oktober</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">Desember</asp:ListItem>
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                         <asp:DropDownList ID="cboYear1" runat="server" Width="100">
                        </asp:DropDownList>
                         <asp:DropDownList ID="cboMonth1" runat="server" Width="120">
                            <asp:ListItem Value="1">Januari</asp:ListItem>
                            <asp:ListItem Value="2">Februari</asp:ListItem>
                            <asp:ListItem Value="3">Maret</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">Mei</asp:ListItem>
                            <asp:ListItem Value="6">Juni</asp:ListItem>
                            <asp:ListItem Value="7">Juli</asp:ListItem>
                            <asp:ListItem Value="8">Agustus</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">Oktober</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">Desember</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="cboYear" runat="server" Width="100">
                        </asp:DropDownList>

                    </div>
                   
                </div>
            </div>

            <div class="col-sm-8">
                <div class="form-inline">
                     <div class="col-sm-2">

                     <asp:DropDownList ID="cbothnajaran" runat="server" CssClass="form-control" Width="140">
                                <asp:ListItem Value="0">Pilih Tahun Ajaran</asp:ListItem>
                                                <asp:ListItem Value="2023-2024">2023-2024</asp:ListItem>
                                                <asp:ListItem Value="2022-2023">2022-2023</asp:ListItem>
                                                <asp:ListItem Value="2021-2022">2021-2022</asp:ListItem>
                                                <asp:ListItem Value="2020-2021">  2020-2021</asp:ListItem>
                                              
                        </asp:DropDownList>
                        </div>
                    <div class="col-sm-10"  >
                        <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Width="200" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                        <label>COA : </label>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Button ID="btnSearchCOA" runat="server" CssClass="btn btn-warning" Text="Cari COA" OnClick="btnSearchCOA_Click" />
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                        <asp:Button runat="server" ID="btnPrint" CssClass="btn btn-success" Text="Print" OnClick="btnPrint_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <div class="form-group" runat="server" id="divtombol">
                     <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>
                </div>
                <asp:GridView ID="grdAccount" DataKeyNames="kdtran" SkinID="GridView" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="Transaction Code" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("kdtran") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Create Transaction Date" SortExpression="Ket" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("tgl1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Transaction Date" SortExpression="ptd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="Grup" runat="server" Text='<%# Eval("tgl") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" SortExpression="ytd" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="uraian" runat="server" Text='<%# Eval("uraian") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Debet" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="debet" runat="server" Text='<%# Eval("debet") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credit" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="kredit" runat="server" Text='<%# Eval("kredit") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Balance" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="saldoakhir" runat="server" Text='<%# Eval("saldoakhir") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgAkun" runat="server" PopupControlID="panelAkun" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelAkun" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-md">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Akun</h4>
            </div>
            <div class="row">
                <div class="col-sm-5">
                </div>
                <div class="col-sm-7">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtSearchAccount" runat="server" CssClass="form-control" placeholder="Masukan kata"></asp:TextBox>
                            <asp:Button ID="btnSearchAccount" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearchAccount_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body col-overflow-500">
                <asp:Label ID="lblMessageAkun" runat="server"></asp:Label>
                <asp:HiddenField runat="server" ID="hdnParameter" />
                <div class="table-responsive">
                    <asp:GridView ID="grdAkun" SkinID="GridView" DataKeyNames="noRek" runat="server" OnSelectedIndexChanged="grdAkun_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kode" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblkdAkun" Text='<%#Eval("kdRek")%>'></asp:Label>
                                        <asp:HiddenField ID="hdnnoAkun" runat="server" Value='<%#Eval("noRek")%>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Akun" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblKet" Text='<%#Eval("ket")%>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelectSub" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnTutup" runat="server" Text="Tutup" CssClass="btn btn-danger" CausesValidation="false" OnClick="btnTutup_Click" />
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

