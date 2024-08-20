<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RbudgetvsRealisasi1.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RbudgetvsRealisasi1" %>
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
             <div class="col-sm-7">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Cabang :   </label>
                         <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Enabled="true" Width="230"></asp:DropDownList>
                        <asp:DropDownList ID="cboYear" runat="server" Width="100">
                        </asp:DropDownList>
                        
                     </div>
                </div>
            </div>
            
            <div class="col-sm-5">
                <div class="form-inline">
                    <div class="col-sm-8">
                         <asp:Button BorderStyle="None" Visible="false"  CssClass="btn btn-success" runat="server" ID="linkBR" Text="Detail" OnClick="linkBR_Click" />
                        <asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Search" OnClick="btnPosting_Click"></asp:Button>
                         <%--<asp:Button runat="server" ID="btnPrint" CssClass="btn btn-success" Text="Print" OnClick="btnPrint_Click"></asp:Button>--%>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row" >
            <div class="col-sm-12 overflow-x-table" >
              <asp:Chart ID="Chart2" runat="server" Visible="false" Width="1300px">
                                                        <Legends>
                                                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" LegendStyle="Row" />
                                                        </Legends>
                                                        <Series>
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="ChartArea1">
                                                            </asp:ChartArea>
                                                        </ChartAreas>
                                                    </asp:Chart>
                 
            </div>
        </div>
       <%-- <div class="row">
            <div class="col-md-5">
            </div>
            <div class="col-md-2 overflow-x-table">
                    
            </div>
            <div class="col-md-5">
            </div>
        </div>--%>
   
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

