<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RincomeStatement1.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RincomeStatement1" %>
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
    <asp:HiddenField ID="hdnnoYysn" runat="server" />
    <div id="tabGrid" runat="server">
        <div class="row">
             <div class="col-sm-6">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Periode :   </label>
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
                        </asp:DropDownList>
                        <asp:DropDownList ID="cboYear" runat="server" Width="100">
                        </asp:DropDownList>&nbsp;&nbsp;
                        <label>Jenis : </label>
                        <asp:DropDownList ID="cboreport" runat="server" Width="120">
                        <asp:ListItem Value="1">Summary I</asp:ListItem>
                        <asp:ListItem Value="2">Summary II</asp:ListItem>
                        <asp:ListItem Value="3">Detail</asp:ListItem>
                        </asp:DropDownList>
                        
                     </div>
                </div>
            </div>
                        
            <div class="col-sm-6">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Cabang : </label>
                          <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" AutoPostBack="true" OnSelectedIndexChanged="cboPerwakilan_SelectedIndexChanged"></asp:DropDownList>
                        <asp:DropDownList ID="cboUnit" Width="200" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                         <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>  
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdAccount" DataKeyNames="kdrek" SkinID="GridView" runat="server">
                        <Columns>
                         <asp:TemplateField HeaderText="AKUN" SortExpression="Ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="BULAN BERJALAN" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="ptd"  runat="server" Text='<%# Eval("ptd") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField> 
                            <asp:TemplateField HeaderText="SAMPAI BULAN INI" SortExpression="YTD" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="ytd"  runat="server" Text='<%# Eval("ytd") %>'></asp:Label>
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

