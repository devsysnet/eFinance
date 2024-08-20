<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TranskenaikanGajiView.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TranskenaikanGajiView" %>
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
                <asp:GridView ID="grdKas" DataKeyNames="nonaikgaji" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdKas_PageIndexChanging"
                    OnSelectedIndexChanged="grdKas_SelectedIndexChanged" OnRowCommand="grdKasView_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                     <asp:HiddenField ID="hdnIdPrint" runat="server" value='<%# Eval("nonaikgaji") %>'/>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Tgl" SortExpression="Tgl" HeaderText="Tanggal" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="kdtran" SortExpression="kdtran" HeaderText="Kode Transaksi" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="jenis" SortExpression="jenis" HeaderText="Jenis Kenaikan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="nama" SortExpression="nama" HeaderText="Nama Karyawan" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="gollama" SortExpression="gollama" HeaderText="Dari Golongan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="golbaru" SortExpression="golbaru" HeaderText="Ke Golongan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="tglmulai" SortExpression="tglmulai" HeaderText="Tanggal Mulai" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Detil" CommandName="Select" />
                                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-success btn-sm" Text="Print" CommandName="detail" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'/>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
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
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Kode Surat</label>
                                        <div class="col-sm-5"><asp:TextBox CssClass="form-control" runat="server" ID="txtkdtran" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Tanggal Surat</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control date" Enabled="false" ID="dttgltrs"></asp:TextBox>
                                        </div>
                                    </div> 
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Nama</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtnama" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-sm-5 control-label">Tempat/Tanggal Lahir</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txttempatlahir" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Jabatan</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtJabatan" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Tanggal Diangkat</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control date" Enabled="false" ID="dttgldiangkat"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-sm-5 control-label">Golongan Lama</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtgollama" Enabled="false" Width="200"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-sm-5 control-label">Golongan Baru</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtgolbaru" Enabled="false"  Width="200"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Gaji Lama</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtgajilama" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-sm-5 control-label">Gaji Baru</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtgajibaru" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Tanggal Mulai</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control date" Enabled="false" ID="dttglmulai"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
