<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TranspotonganGajiviewi.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TranspotonganGajiviewi" %>
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
            <div class="col-sm-4">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <asp:TextBox ID="dtMulai" runat="server" CssClass="form-control date"></asp:TextBox>
                        <asp:TextBox ID="dtSampai" runat="server" CssClass="form-control date"></asp:TextBox>
                     </div>
                </div>
            </div>
            
            <div class="col-sm-8">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Cabang : </label>
                         <asp:DropDownList ID="cboCabang" class="form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdKas" DataKeyNames="nokaryawan" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdKas_PageIndexChanging"
                    OnSelectedIndexChanged="grdKas_SelectedIndexChanged" OnRowCommand="grdKasView_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                     <%--<asp:HiddenField ID="hdnIdPrint" runat="server" value='<%# Eval("nokaryawan") %>'/>--%>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="tgl" SortExpression="tgl" HeaderText="Tanggal" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="jenis" SortExpression="jenis" HeaderText="Jenis" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                         <asp:BoundField DataField="kodethr" SortExpression="kodethr" HeaderText="Kode Transaksi" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="nama" SortExpression="nama" HeaderText="Nama Karyawan" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Jabatan" SortExpression="Jabatan" HeaderText="Jabatan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="golongan" SortExpression="golongan" HeaderText="Golongan" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="nilai" SortExpression="nilai" HeaderText="Nilai" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right" />
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

