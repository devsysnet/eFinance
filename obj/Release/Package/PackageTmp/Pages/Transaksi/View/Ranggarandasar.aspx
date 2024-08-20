<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Ranggarandasar.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.Ranggarandasar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
 
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdAccount.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
      
        let tes1 = document.getElementById("BodyContent_btnprintPajak");
        function printPajak() {
              $('body').html($('.overflow-x-table').html());
            window.print();
            location.reload()
        } 
          var conceptName = $('#cboYear').find(":selected").text();
          $('.result').text(conceptName);
    </script>
        <asp:HiddenField ID="hdnnoYysn" runat="server" />
   
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
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
                         <asp:DropDownList ID="cboPerwakilan" runat="server" Width="200" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboPerwakilan_SelectedIndexChanged"></asp:DropDownList>
                         <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Width="180"></asp:DropDownList>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                         <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>  
                        <asp:Button ID="btnprintPajak" runat="server"   CssClass="btn btn-success " Text="Print" OnClick="printPajak"/>

                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <div id="headprint">
                    <h1 id="judul" style="text-align:center;color:black" runat="server"></h1>
                    <h3 id="periode" style="text-align:center;color:black" runat="server"></h3>

                </div>
                     <style>
                        .table  td{
                            border: 1px solid  #adb0b3 !important;
                        }
                        .table  th{
                            border: 1px solid #adb0b3 !important;
                        } 
                        .table span{
                            color:black;
                        }
                    </style>
                <asp:GridView ID="grdAccount" DataKeyNames="kdrek1" SkinID="GridView" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="RENCANA PENDAPATAN" SortExpression="Ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek")%>'></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket").ToString().Replace(" ","&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NILAI" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="Grup" runat="server" Text='<%# Eval("debet") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="RENCANA PENGELUARAN" SortExpression="Ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek1")%>'></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket1").ToString().Replace(" ", "&nbsp;")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NILAI" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="Grup" runat="server" Text='<%# Eval("debet1") %>'></asp:Label>
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

