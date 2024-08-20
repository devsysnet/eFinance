<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RpantuanTagihan.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RpantuanTagihan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function OpenReport() {
            //            OpenReportViewer();
            window.open('../../../Report/ReportViewer.aspx', 'name', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,modal=yes,width=1000,height=600');
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
                
    <asp:HiddenField ID="hdnParent" runat="server" Value="0" />
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
             <div class="col-sm-5">
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
                        <asp:ListItem Value="3">Detail</asp:ListItem>
                        </asp:DropDownList>
                        
                     </div>
                </div>
            </div>
                        
            <div class="col-sm-7">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Jenis : </label>
                        <asp:DropDownList ID="cbojnsrpt" runat="server" Width="120">
                        <asp:ListItem Value="1">Bulanan</asp:ListItem>
                        <asp:ListItem Value="2">Semesteran</asp:ListItem>
                        <asp:ListItem Value="3">Tahunan</asp:ListItem>
                         </asp:DropDownList>&nbsp;&nbsp;
                        <label>Cabang : </label>
                         <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" width="200"></asp:DropDownList>
                         <asp:DropDownList ID="cbothnajaran" runat="server" CssClass="form-control" Width="115">                                     
                        </asp:DropDownList>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                         <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>  
                           <asp:Button ID="btnprintPajak" runat="server"   CssClass="btn btn-success " Text="Print" OnClick="printPajak"></asp:Button>
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
                <asp:GridView ID="grdAccount" DataKeyNames="kdrek" SkinID="GridView" runat="server">
                        <Columns>
                          <asp:TemplateField HeaderText="Kode Akun" SortExpression="Ket" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Ket" runat="server" Text='<%# Eval("kdrek").ToString()%>'></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField>
                         <asp:TemplateField HeaderText="Nama Akun" SortExpression="Ket" ItemStyle-Width="18%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField>
                           <asp:TemplateField HeaderText="Anggaran" SortExpression="ptd" ItemStyle-Width="14%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="ptd"  runat="server" Text='<%# Eval("rencana") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField> 
                          <asp:TemplateField HeaderText="Realisasi Bulan ini" SortExpression="ptd" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="ptd"  runat="server" Text='<%# Eval("ptd") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField> 
                          <asp:TemplateField HeaderText="Realisasi sd Bulan Ini" SortExpression="YTD" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="ytd"  runat="server" Text='<%# Eval("ytd") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Capaian" SortExpression="YTD" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="ytd"  runat="server" Text='<%# Eval("persen") %>'></asp:Label>
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
