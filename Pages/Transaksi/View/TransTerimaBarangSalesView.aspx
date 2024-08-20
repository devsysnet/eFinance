<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransTerimaBarangSalesView.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TransTerimaBarangSalesView" %>
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
            <div class="col-sm-5">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <asp:TextBox ID="dtMulai" runat="server" CssClass="form-control date"></asp:TextBox>
                        <asp:TextBox ID="dtSampai" runat="server" CssClass="form-control date"></asp:TextBox>
                     </div>
                </div>
            </div>
            
            <div class="col-sm-7">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <asp:DropDownList runat="server" CssClass="form-control" Width="200" ID="cboTransaction">
                            <asp:ListItem Value="0">--Pilih Jenis Permintaan--</asp:ListItem>
                            <asp:ListItem Value="1">Barang Aset</asp:ListItem>
                            <asp:ListItem Value="3">Jasa</asp:ListItem>
                            <asp:ListItem Value="4">Barang Operasional</asp:ListItem>
                            <asp:ListItem Value="5">Barang Sales</asp:ListItem>
                        </asp:DropDownList>    
                        <label>Cabang : </label>
                         <asp:DropDownList ID="cboCabang" class="form-control" runat="server"></asp:DropDownList>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"/>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdReceive" DataKeyNames="noTerima" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdReceive_PageIndexChanging" OnRowDataBound="grdReceive_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="tglTerima" HeaderText="Tgl Terima" ItemStyle-Width="7%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                        <asp:BoundField DataField="kodeTerima" HeaderText="Kode Terima" ItemStyle-Width="8%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="penerima" HeaderText="Peminta" ItemStyle-Width="12%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="suratjalan" HeaderText="Surat Jalan" ItemStyle-Width="12%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="catatanpenerima" HeaderText="Keterangan" ItemStyle-Width="15%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="namaBarang" HeaderText="Nama Barang" ItemStyle-Width="15%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="qtyTerima" HeaderText="Qty Terima" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div id="tabForm" runat="server" visible="false">
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-6">
                                    
                                   </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                
                            </div>
                        </div>
                           <div class="form-group row">
                            <div class="col-md-12">
                                <div class="text-center">
                                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>



