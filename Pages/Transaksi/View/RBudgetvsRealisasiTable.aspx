<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RBudgetvsRealisasiTable.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RBudgetvsRealisasiTable" %>
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

        .tableFixHead tr td:first-child {
			position: -webkit-sticky;
			position: sticky;
			left: 0;
            margin-top:20px;
			background: #ccc;
            z-index:1;
		}
       .tableFixHead tr td:first-child + td {
			position: -webkit-sticky;
			position: sticky;
			left: 60px;
            margin-top:20px;
			background: #ccc;
            z-index:1;
		}
         .tableFixHead {
			position: relative;
			width:100%;
			z-index: 100;
			margin: auto;
			overflow: scroll;
			height: 80vh;
		}
		.tableFixHead table {
			width: 100%;
			min-width: 100px;
			margin: auto;
			border-collapse: separate;
			border-spacing: 0;
		}
		.table-wrap {
			position: relative;
		}
		.tableFixHead th,
		.tableFixHead td {
			padding: 5px 10px;
			border: 1px solid #000;
			#background: #fff;
			vertical-align: top;
			text-align: left;
		}
		.tableFixHead  th {
			background: #f6bf71;
			position: -webkit-sticky;
			position: sticky;
			top: 0;
            z-index:9;

		}
		td{
			z-index: -4;

		}
       
        .tableFixHead   tr:nth-child(2) th {
            top: 25px;
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
                        <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Width="300"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:DropDownList ID="cboYear" runat="server" Width="120">
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Cari" OnClick="btnCari_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" ></asp:Button>
                        <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>
                     </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row ">
            <div class="col-sm-12 overflow-x-table">
                <div  id="pajak" visible="false" runat="server">
                <div class="tableFixHead table-responsive overflow-x-table">
                 <asp:GridView ID="grdAccount"  OnDataBound="grdAccount_DataBound" DataKeyNames="kdrek" SkinID="GridView" runat="server" BorderWidth="1px" BorderColor="Black">
                  <Columns>
                     <asp:TemplateField HeaderText="KODE AKUN" SortExpression="Ket" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="left">
                       <ItemTemplate>
                         <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek").ToString().Replace(" ","") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="AKUN" SortExpression="Ket" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left">
                       <ItemTemplate>
                         <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket").ToString().Replace(" "," ") %>' Width="300"></asp:Label>
                       </ItemTemplate>
                        </asp:TemplateField>
                     
                       <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                       <ItemTemplate>
                         <asp:Label ID="debet"  runat="server" Text='<%# Eval("Budget_Januari") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="debet"  runat="server" Text='<%# Eval("Realisasi_Januari") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                       <asp:Label ID="debetrl"  runat="server" Text='<%# Eval("Budget_Februari") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                       <ItemTemplate>
                         <asp:Label ID="kreditrl"  runat="server" Text='<%# Eval("Realisasi_Februari") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="debetner"  runat="server" Text='<%# Eval("Budget_Maret") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Realisasi_Maret") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Budget_April") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Realisasi_April") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Budget_Mei") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Realisasi_Mei") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Budget_Juni") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Realisasi_Juni") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField> 
                                   <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Budget_Juli") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                     <ItemTemplate>
                       <asp:Label ID="debet"  runat="server" Text='<%# Eval("Realisasi_Juli") %>'></asp:Label>
                     </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="debet"  runat="server" Text='<%# Eval("Budget_Agustus") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                     <ItemTemplate>
                       <asp:Label ID="debetrl"  runat="server" Text='<%# Eval("Realisasi_Agustus") %>'></asp:Label>
                     </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                     <ItemTemplate>
                       <asp:Label ID="kreditrl"  runat="server" Text='<%# Eval("Budget_September") %>'></asp:Label>
                     </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="debetner"  runat="server" Text='<%# Eval("Realisasi_September") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Budget_Oktober") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Realisasi_Oktober") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Budget_November") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Realisasi_November") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Budget_Desember") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Realisasi_Desember") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField> 
                    </Columns>
                </asp:GridView> 
                    </div>
                    </div>
                </div>
                <%-- <div  id="tahunAjaran" visible="false" runat="server">
                 <asp:GridView ID="GridView1" DataKeyNames="kdrek" SkinID="GridView" runat="server" BorderWidth="1px" BorderColor="Black"  OnDataBound="GridView1_DataBound">
                  <Columns>

                     <asp:TemplateField HeaderText="KODE AKUN" SortExpression="Ket" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="left">
                       <ItemTemplate>
                         <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek").ToString().Replace(" ","") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="AKUN" SortExpression="Ket" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left">
                       <ItemTemplate>
                         <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket").ToString().Replace(" ","") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Budget_Juli") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                     <ItemTemplate>
                       <asp:Label ID="debet"  runat="server" Text='<%# Eval("Realisasi_Juli") %>'></asp:Label>
                     </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="debet"  runat="server" Text='<%# Eval("Budget_Agustus") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                     <ItemTemplate>
                       <asp:Label ID="debetrl"  runat="server" Text='<%# Eval("Realisasi_Agustus") %>'></asp:Label>
                     </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                     <ItemTemplate>
                       <asp:Label ID="kreditrl"  runat="server" Text='<%# Eval("Budget_September") %>'></asp:Label>
                     </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="debetner"  runat="server" Text='<%# Eval("Realisasi_September") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Budget_Oktober") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Realisasi_Oktober") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Budget_November") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Realisasi_November") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Budget_Desember") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Realisasi_Desember") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField> 
                               <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                       <ItemTemplate>
                         <asp:Label ID="debet"  runat="server" Text='<%# Eval("Budget_Januari") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="debet"  runat="server" Text='<%# Eval("Realisasi_Januari") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                       <asp:Label ID="debetrl"  runat="server" Text='<%# Eval("Budget_Februari") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                       <ItemTemplate>
                         <asp:Label ID="kreditrl"  runat="server" Text='<%# Eval("Realisasi_Februari") %>'></asp:Label>
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="debetner"  runat="server" Text='<%# Eval("Budget_Maret") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Realisasi_Maret") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Budget_April") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Realisasi_April") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Budget_Mei") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Realisasi_Mei") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Budget" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Budget_Juni") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Realisasi" SortExpression="ytd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                      <ItemTemplate>
                        <asp:Label ID="kreditner"  runat="server" Text='<%# Eval("Realisasi_Juni") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField> 
                   
                   
                    </Columns>
                </asp:GridView> 
                    </div>--%>
            </div>
        </div>
    <%--</div>--%>
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
                freezesize: 2, // Freeze Number of Columns. 
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

