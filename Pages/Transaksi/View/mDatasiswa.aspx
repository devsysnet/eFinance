<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="mDatasiswa.aspx.cs" Inherits="eFinance.Pages.Master.View.mDatasiswa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="TabName" runat="server" />
    <script type="text/javascript">
        $(function () {

            SetTabs();
        });
        Sys.Application.add_init(appl_init);
        function appl_init() {
            var pgRegMgr = Sys.WebForms.PageRequestManager.getInstance();
            pgRegMgr.add_beginRequest(SetTabs);
            pgRegMgr.add_endRequest(SetTabs);
        }
        function SetTabs() {
            var tabName = $("#<%=TabName.ClientID%>").val();
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("#<%=TabName.ClientID%>").val($(this).attr("href").replace("#", ""));
            });

        };
    </script>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group ">
                                    <div class="col-sm-1 control-label">
                                        <span>Filter Cari</span>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="cboKelas" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboKelas_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Masukan kata" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Button runat="server"  ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click"></asp:Button>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdSiswa" DataKeyNames="noSiswa" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdSiswa_PageIndexChanging"
                                        OnSelectedIndexChanged="grdSiswa_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="NO." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nik" HeaderText="NIK" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="nis" HeaderText="NIS" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="nisn" HeaderText="NISN" HeaderStyle-CssClass="text-center" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="namaSiswa" HeaderText="Nama Siswa" HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="nilaiBayar" HeaderText="Nilai Bayar" HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="right" />

                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-xs" Text="Detail" CommandName="Select" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="tabForm" runat="server" visible="false">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="col-sm-12">
                                <div class="panel">
                                    <div class="panel-body" id="Tabs" role="tabpanel">
                                        <!-- Nav tabs -->
                                        <ul class="nav nav-tabs" role="tablist">
                                            <li class="active">
                                                <a href="#tab-umum" data-toggle="tab" role="tab">Umum
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#tab-akun" data-toggle="tab" role="tab">Akun</a>
                                            </li>
                                            <li>
                                                <a href="#tab-piutang" data-toggle="tab" role="tab">Pembayaran</a>
                                            </li>
                                        </ul>
                                        <!-- Tab panes -->
                                        <div class="tab-content tab-content-bordered">
                                            <div class="tab-pane active" id="tab-umum">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-horizontal">
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">NIK :</label>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNIK" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">NIS :</label>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNIS" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="form-inline">
                                                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">NISN :</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNISN" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Lengkap :</label>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNama" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Jenis Kelamin :</label>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox ID="txtJK" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Agama :</label>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox ID="txtAgama" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-horizontal">
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Alamat :</label>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtAlamat" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kota :</label>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtKota" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="form-inline">
                                                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kota Lahir :</label>
                                                                    <div class="col-sm-5">
                                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtKotaLhr" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tgl Lahir :</label>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtTglLahir" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Ortu :</label>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox ID="txtOrtu" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Telpon :</label>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox ID="txtTelp" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane" id="tab-akun">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="form-horizontal">
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Virtual Account :</label>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtVA" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Keterangan 1 :</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtKet1" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Keterangan 2 :</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtKet2" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Keterangan 3 :</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtKet3" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane" id="tab-piutang">
                                                <div class="row">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="grdPiutSiswa" SkinID="GridView" runat="server">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                                    <ItemTemplate>
                                                                        <div class="text-center">
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="tahunajaran" HeaderText="Tahun Ajaran" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:BoundField DataField="transaksi" HeaderText="Transaksi" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="tgl" HeaderText="Tanggal Transaksi" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                                                                <asp:BoundField DataField="nilai" HeaderText="nilai" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                                                <asp:BoundField DataField="kodebayar" HeaderText="Kode Bayar" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="tglbayar" HeaderText="Tanggal Bayar" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                                                                <asp:BoundField DataField="nilaibayar" HeaderText="Nilai Bayar" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                                                <asp:BoundField DataField="saldo" HeaderText="Saldo" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Back" OnClick="btnCancel_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
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
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
