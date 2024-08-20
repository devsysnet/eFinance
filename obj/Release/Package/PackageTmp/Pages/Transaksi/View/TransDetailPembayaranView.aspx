<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransDetailPembayaranView.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TransDetailPembayaranView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <style>
        .table {
            margin-bottom: 0;
        }
        #BodyContent_grdAccountPanelHeaderContentFreeze {
            height: auto !important;
        }
    </style>
    <script type="text/javascript">
        function OpenReport() {
            //            OpenReportViewer();
            window.open('../../../Report/ReportViewer.aspx', 'name', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,modal=yes,width=1000,height=600');
        }
       
    </script>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
             <div class="col-sm-12">
                <div class="form-inline">
                    <div class="col-sm-10">
                         <asp:DropDownList ID="cboYear" runat="server" Width="100">
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Cari" OnClick="btnCari_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" ></asp:Button>
                        <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>
                     </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12">
             
                 <asp:GridView ID="grdAccount" DataKeyNames="namacabang" SkinID="GridView" runat="server" BorderWidth="1px" BorderColor="Black">
                  <Columns>
                     <asp:TemplateField HeaderText="CABANG" SortExpression="Ket" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left">
                       <ItemTemplate>
                         <asp:Label ID="Unit" runat="server" Text='<%# Eval("namaCabang").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Jenis Transaksi" SortExpression="Ket" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left">
                       <ItemTemplate>
                         <asp:Label ID="Ket" runat="server" Text='<%# Eval("jenisTransaksi").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                      <asp:TemplateField HeaderText="Nilai Tagihan" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                       <ItemTemplate>
                         <asp:Label ID="debet"  runat="server" Text='<%# Eval("nilaitagihan") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Jurnal Tagihan" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="debet"  runat="server" Text='<%# Eval("jurnaltagihan") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Selisih" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                       <asp:Label ID="debetrl"  runat="server" Text='<%# Eval("selisihjurnaltagihan") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Nilai Pembayaran" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                       <ItemTemplate>
                         <asp:Label ID="kreditrl"  runat="server" Text='<%# Eval("nilaibayar") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Jurnal Pembayaran" SortExpression="ytd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="debetner"  runat="server" Text='<%# Eval("Jurnalpembayaran") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Selisih" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("selisihjurnalpembayaran") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Saldo Akhir" SortExpression="ptd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                       <asp:Label ID="sawal"  runat="server" Text='<%# Eval("saldo") %>'></asp:Label>
                     </ItemTemplate>
                    </asp:TemplateField>
                   </Columns>
                </asp:GridView> 
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>

   
     
   <%-- <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, e) {
            $('#BodyContent_grdAccount').gridviewScroll({
                width: '99%',
                height: 600,
                freezesize: 3, // Freeze Number of Columns. 
            });
        });
    </script>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
