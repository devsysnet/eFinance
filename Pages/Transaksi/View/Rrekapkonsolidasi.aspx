<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Rrekapkonsolidasi.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.Rrekapkonsolidasi" %>
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
             
                 <asp:GridView ID="grdAccount" DataKeyNames="kdrek" SkinID="GridView" runat="server" BorderWidth="1px" BorderColor="Black">
                  <Columns>
                     <asp:TemplateField HeaderText="KODE AKUN" SortExpression="Ket" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="left">
                       <ItemTemplate>
                         <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="AKUN" SortExpression="Ket" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left">
                       <ItemTemplate>
                         <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="CABANG" SortExpression="Ket" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="left">
                       <ItemTemplate>
                         <asp:Label ID="Ket" runat="server" Text='<%# Eval("namaCabang").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="SALDO AWAL" SortExpression="ptd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                       <ItemTemplate>
                         <asp:Label ID="sawal"  runat="server" Text='<%# Eval("sawal") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="JAN" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                       <ItemTemplate>
                         <asp:Label ID="debet"  runat="server" Text='<%# Eval("jand") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="FEB" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="debet"  runat="server" Text='<%# Eval("febd") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MAR" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                       <asp:Label ID="debetrl"  runat="server" Text='<%# Eval("mard") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="APR" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                       <ItemTemplate>
                         <asp:Label ID="kreditrl"  runat="server" Text='<%# Eval("aprd") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="MEI" SortExpression="ytd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="debetner"  runat="server" Text='<%# Eval("meid") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="JUN" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("jund") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="JUL" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("juld") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AGS" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("agsd") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SEPT" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Septd") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OKT" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Oktd") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NOV" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Novd") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="DES" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("descd") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="TOTAL DEBET" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("totd") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="JAN" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                     <ItemTemplate>
                       <asp:Label ID="debet"  runat="server" Text='<%# Eval("Jank") %>'></asp:Label>
                     </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FEB" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="debet"  runat="server" Text='<%# Eval("Febk") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MAR" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                     <ItemTemplate>
                       <asp:Label ID="debetrl"  runat="server" Text='<%# Eval("Mark") %>'></asp:Label>
                     </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="APR" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                     <ItemTemplate>
                       <asp:Label ID="kreditrl"  runat="server" Text='<%# Eval("Aprk") %>'></asp:Label>
                     </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MEI" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="debetner"  runat="server" Text='<%# Eval("Meik") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="JUN" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Junk") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="JUL" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Julk") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AGS" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Agsk") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SEPT" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Septk") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OKT" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Oktk") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NOV" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Novk") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="DES" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("desck") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="TOTAL KREDIT" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("totk") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SALDO AKHIR" SortExpression="ptd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                       <asp:Label ID="sawal"  runat="server" Text='<%# Eval("saldoakhir") %>'></asp:Label>
                     </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="" SortExpression="ptd" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
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

   
     
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, e) {
            $('#BodyContent_grdAccount').gridviewScroll({
                width: '99%',
                height: 600,
                freezesize: 3, // Freeze Number of Columns. 
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
