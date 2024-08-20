<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RrekapLedgersmt.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RrekapLedgersmt" %>
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
             <div class="col-sm-6">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Dari Periode :   </label>
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
                        </asp:DropDownList>&nbsp;&nbsp;sd &nbsp;
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
            
            <div class="col-sm-6">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Filter : </label>
                          <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang"></asp:DropDownList>
                          <asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Cari" OnClick="btnCari_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" ></asp:Button>
                          <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>  
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdAccount" DataKeyNames="kdrek" SkinID="GridView" runat="server" OnDataBound="grdAccount_DataBound">
                 <Columns>
                 <asp:TemplateField HeaderText="KODE AKUN" SortExpression="Ket" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                 <ItemTemplate>
                 <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                 </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="AKUN" SortExpression="Ket" ItemStyle-Width="40%" ItemStyle-HorizontalAlign="left">
                 <ItemTemplate>
                 <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                 </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="SALDO AWAL" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                 <ItemTemplate>
                 <asp:Label ID="sawal"  runat="server" Text='<%# Eval("sawal") %>'></asp:Label>
                 </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="DEBET" SortExpression="ytd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                 <ItemTemplate>
                 <asp:Label ID="debet"  runat="server" Text='<%# Eval("debet") %>'></asp:Label>
                 </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="KREDIT" SortExpression="ytd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                  <ItemTemplate>
                  <asp:Label ID="debet"  runat="server" Text='<%# Eval("kredit") %>'></asp:Label>
                  </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="DEBET" SortExpression="ytd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                 <ItemTemplate>
                 <asp:Label ID="debetrl"  runat="server" Text='<%# Eval("debetrl") %>'></asp:Label>
                 </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="KREDIT" SortExpression="ytd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                 <ItemTemplate>
                 <asp:Label ID="kreditrl"  runat="server" Text='<%# Eval("kreditrl") %>'></asp:Label>
                 </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="DEBET" SortExpression="ytd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                  <ItemTemplate>
                  <asp:Label ID="debetner"  runat="server" Text='<%# Eval("debetner") %>'></asp:Label>
                  </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="KREDIT" SortExpression="ytd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                  <ItemTemplate>
                  <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("kreditner") %>'></asp:Label>
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

   
     
  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
