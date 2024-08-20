<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransArSiswaView.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TransArSiswaView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function OpenReport() {
            //            OpenReportViewer();
            window.open('../../../Report/ReportViewer.aspx', 'name', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,modal=yes,width=1000,height=600');
        }
        function money(money) {
            var num = money;
            if (num != "") {
                var array = num.toString().split('');
                var index = -3;
                while (array.length + index > 0) {
                    array.splice(index, 0, ',');
                    index -= 4;
                }

                money = array.join('') + '.00';
            } else {
                money = '0.00';
            }
            return money;
        }


    </script>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-5">
                                <div class="form-horizontal">
                                                                 
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right">Cabang <span class="mandatory">*</span></label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="cboCabang" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged" Width="250"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" id="showhidePeriod" runat="server">
                                        <label class="col-sm-3 control-label text-right">Periode <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboPeriode" runat="server" CssClass="form-control" Width="150">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-7">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right">Kelas <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboKelas" runat="server" CssClass="form-control" Width="200"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right">Jenis Transaksi <span class="mandatory">*</span></label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="cboKategori" CssClass="form-control" runat="server" width="230"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right"></label>
                                        <div class="col-sm-2">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdARSiswa" SkinID="GridView" runat="server" AllowPaging="false" PageSize="10" OnPageIndexChanging="grdARSiswa_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="namaSiswa" HeaderText="Nama Siswa" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="kelas" HeaderText="Kelas" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="tgl" HeaderText="Tanggal" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="nilai" HeaderText="Nilai" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="right" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="nilaibayar" HeaderText="Nilai Bayar" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="right" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="saldo" HeaderText="Saldo" ItemStyle-Width="14%" ItemStyle-HorizontalAlign="right" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="stscetak" HeaderText="Keterangan" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="text-center" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="cboCabang" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

