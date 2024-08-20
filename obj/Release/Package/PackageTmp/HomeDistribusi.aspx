<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="HomeDistribusi.aspx.cs" Inherits="eFinance.HomeDistribusi" %>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
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
        function money(money) {
            var num = money;
            if (num != "") {
                var string = String(num);
                var string = string.replace('.', ' ');

                var array2 = string.toString().split(' ');
                num = parseInt(num).toFixed(0);

                var array = num.toString().split('');

                var index = -3;
                while (array.length + index > 0) {
                    array.splice(index, 0, ',');
                    index -= 4;
                }
                if (array2.length == 1) {
                    money = array.join('') + '.00';
                } else {
                    money = array.join('') + '.' + array2[1];
                    if (array2.length == 1) {
                        money = array.join('') + '.00';
                    } else {
                        if (array2[1].length == 1) {
                            money = array.join('') + '.' + array2[1].substring(0, 2) + '0';
                        } else {
                            money = array.join('') + '.' + array2[1].substring(0, 2);
                        }
                    }
                }
            } else {
                money = '0.00';
            }
            return money;
        }
        function removeMoney(money) {
            var minus = money.substring(0, 1);
            if (minus == "-") {
                var number = '-' + Number(money.replace(/[^0-9\.]+/g, ""));
            } else {
                var number = Number(money.replace(/[^0-9\.]+/g, ""));
            }
            return number;
        }
    </script>

    <div class="row">
        <div class="form-horizontal">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-body" id="Tabs" role="tabpanel">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label class="col-sm-1 control-label text-left">Filter</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="cboYayasan" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboYayasan_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3" id="comboPerwakilan" runat="server">
                                        <asp:DropDownList ID="cboPerwakilan" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboPerwakilan_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3" id="comboUnit" runat="server">
                                        <asp:DropDownList ID="cboUnit" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboUnit_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="cboTahun" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboTahun_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <!-- Nav tabs -->
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="active">
                                <a href="#tab-uang" data-toggle="tab" role="tab">Data Keuangan
                            <asp:Label ID="lbltahun" runat="server"></asp:Label>
                                </a>
                            </li>
                          <%--  <li>
                                <a href="#tab-hrd" data-toggle="tab" role="tab">Data HRD
                            <asp:Label ID="lbltahun3" runat="server"></asp:Label>
                                </a>
                            </li>--%>
                            <li>
                                <a href="#tab-Stok" data-toggle="tab" role="tab">Stok
                            <asp:Label ID="Label4" runat="server"></asp:Label>
                                </a>
                            </li>
                        </ul>
                        <!-- Tab panes -->
                        <div class="tab-content tab-content-bordered">
                            <div role="tabpanel" class="tab-pane active" id="tab-uang">
                                <div class="row">
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="panel panel-info panel-dark widget-profile">
                                            <div class="panel-heading">
                                                <div class="widget-profile-header">
                                                    <span class="text-md"><strong>
                                                        <asp:Label ID="lblTotBiaya" runat="server" Text="0"></asp:Label></strong></span><br>
                                                    <%--<span class="text-xs">PENGELUARAN (ytd)</span>--%>
                                                    <asp:Button BorderStyle="None" style="background:none;padding:0" runat="server" ID="linkRekabBiaya" Text="PENGELUARAN (ytd)" OnClick="linkRekabBiaya_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="panel panel-warning panel-dark widget-profile">
                                            <div class="panel-heading">
                                                <div class="widget-profile-header">
                                                    <span class="text-md"><strong>
                                                        <asp:Label ID="lblOutstandingAr" runat="server" Text="0"></asp:Label></strong></span><br>
                                                    <span class="text-xs">PENDAPATAN (ytd)</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="panel panel-danger panel-dark widget-profile">
                                            <div class="panel-heading">
                                                <div class="widget-profile-header">
                                                    <span class="text-md"><strong>
                                                        <asp:Label ID="lblTotTerima" runat="server" Text="0"></asp:Label></strong></span><br>
                                                    <span class="text-xs">PENERIMAAN (ytd)</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="panel panel-success panel-dark widget-profile">
                                            <div class="panel-heading">
                                                <div class="widget-profile-header">
                                                    <span class="text-md"><strong>
                                                        <asp:Label ID="lblTerimaKas" runat="server" Text="0"></asp:Label></strong></span><br>
                                                    <span class="text-xs">SALDO KAS</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="panel panel-dark bg-pa-purple widget-profile">
                                            <div class="panel-heading bg-pa-purple">
                                                <div class="widget-profile-header">
                                                    <span class="text-md"><strong>
                                                        <asp:Label ID="lblTerimaBank" runat="server" Text="0"></asp:Label></strong></span><br>
                                                    <span class="text-xs">SALDO BANK</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="panel panel-default panel-dark widget-profile">
                                            <div class="panel-heading">
                                                <div class="widget-profile-header">
                                                    <span>Task List Alert</span>
                                                </div>
                                            </div>
                                            <!-- / .panel-heading -->
                                            <div class="list-group">
                                                <asp:Label ID="lblAlertList" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-9">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:Chart ID="Chart1" runat="server" Width="650px" Height="350px">
                                                        <Legends>
                                                            <asp:Legend Alignment="Center" Docking="Bottom" LegendStyle="Row" />
                                                        </Legends>
                                                        <Series>
                                                            <%-- <asp:Series Name="Series1" LegendText="Biaya" ChartArea="ChartArea1">
                                                                </asp:Series>
 
                                                                <asp:Series Name="Series2" LegendText="Pendapatan" ChartArea="ChartArea1">
                                                                </asp:Series>
 
                                                                <asp:Series Name="Series3" LegendText="Kas Bank" ChartArea="ChartArea1">
                                                                </asp:Series>--%>
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="ChartArea1">
                                                                <%--<AxisX Title="Bulan">
                                                                    </AxisX>;--%>
                                                                <AxisY Title="Total dalam juta">
                                                                </AxisY>
                                                            </asp:ChartArea>
                                                        </ChartAreas>
                                                    </asp:Chart>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:Chart ID="Chart2" runat="server" Width="400px">
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
                                        </div>
                                    </div>
                                </div>
                            </div>


                           <%-- <div class="tab-pane" id="tab-hrd">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            <div class="stat-panel">
                                                <!-- Success background, bordered, without top and bottom borders, without left border, without padding, vertically and horizontally centered text, large text -->
                                                <a href="Pages/Master/View/MasterDataKaryawan.aspx" class="stat-cell col-xs-5 bg-info bordered no-border-vr no-border-l no-padding valign-middle text-center text-lg">
                                                    <i class="fa fa-users"></i>&nbsp;&nbsp;<asp:Label ID="lbltotKaryawan" runat="server" Text="0"></asp:Label>
                                                </a>
                                                <!-- /.stat-cell -->
                                                <!-- Without padding, extra small text -->
                                                <div class="stat-cell col-xs-7 no-padding valign-middle">
                                                    <!-- Add parent div.stat-rows if you want build nested rows -->
                                                    <div class="stat-rows">
                                                        <div class="stat-row">
                                                            <!-- Success background, small padding, vertically aligned text -->
                                                            <a href="#" class="stat-cell bg-warning padding-sm valign-middle">
                                                                <asp:Label ID="lbltotGuru" runat="server" Text="0"></asp:Label>
                                                                guru
									                            <i class="fa fa-users pull-right"></i>
                                                            </a>
                                                        </div>
                                                        <div class="stat-row">
                                                            <!-- Success darker background, small padding, vertically aligned text -->
                                                            <a href="#" class="stat-cell bg-success padding-sm valign-middle">
                                                                <asp:Label ID="lbltotNonGuru" runat="server" Text="0"></asp:Label>
                                                                non guru
									                            <i class="fa fa-users pull-right"></i>
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <!-- /.stat-rows -->
                                                </div>
                                                <!-- /.stat-cell -->
                                            </div>
                                            <!-- /.stat-panel -->

                                        </div>
                                        <div class="col-sm-4">
                                            <div class="stat-panel">
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="stat-panel">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>

                            <div class="tab-pane" id="tab-Stok">
                                <div class="row">
                                    <div class="col-sm-12 overflow-x-table">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdTracking" DataKeyNames="noSA" SkinID="GridView" runat="server" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nama Barang" SortExpression="NamaBarang" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="NamaBarang" runat="server" Text='<%# Eval("namaBarang").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Gudang" SortExpression="gudang" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gudang" runat="server" Text='<%# Eval("namaGudang").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Lokasi Gudang" SortExpression="lokasiGudang" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lokasiGudang" runat="server" Text='<%# Eval("namaLokGud").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stok Asli" SortExpression="stokAsli" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="stokAsli" runat="server" Text='<%# Eval("sisaSA").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stok Sementara" SortExpression="stokSementara" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="stokSementara" runat="server" Text='<%# Eval("sisaSASMT").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal Expired" SortExpression="tglexp" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="tglexp" runat="server" Text='<%# Eval("expired").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
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
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
