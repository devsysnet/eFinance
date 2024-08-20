﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Home2.aspx.cs" Inherits="eFinance.Home2" %>
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
                            <div class="form-group">
                                <label class="col-sm-2 control-label text-right">Pilih Perwakilan / Unit</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cboYayasan" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboYayasan_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                </div>
                                <div class="col-sm-3" id="comboPerwakilan" runat="server">
                                    <asp:DropDownList ID="cboPerwakilan" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboPerwakilan_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-sm-3" id="comboUnit" runat="server">
                                    <asp:DropDownList ID="cboUnit" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboUnit_SelectedIndexChanged"></asp:DropDownList>
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
                           <%-- <li>
                                <a href="#tab-siswa" data-toggle="tab" role="tab">Data Siswa
                            <asp:Label ID="lbltahun2" runat="server"></asp:Label>
                                </a>
                            </li>--%>
                            <li>
                                <a href="#tab-hrd" data-toggle="tab" role="tab">Data HRD
                            <asp:Label ID="lbltahun3" runat="server"></asp:Label>
                                </a>
                            </li>
                            <li>
                                <a href="#tab-tracking" data-toggle="tab" role="tab">Tracking
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
                                                    <span class="text-xs">PENGELUARAN (ytd)</span>
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
                                                    <asp:Chart ID="Chart1" runat="server" Width="750px" Height="350px">
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
                                                    <asp:Chart ID="Chart2" runat="server" Width="700px">
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

                          <%--  <div class="tab-pane" id="tab-siswa">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-3">
                                            <div class="stat-panel">
                                                <div class="stat-cell bg-info valign-middle">
                                                    <i class="fa fa-users bg-icon"></i>
                                                    <span class="text-lg"><strong>
                                                        <asp:Label ID="lblTotalSiswa" runat="server" Text="0"></asp:Label></strong></span><br>
                                                    <span class="text-bg">
                                                        <asp:HyperLink ID="HyperLink3" runat="server" Text="TOTAL SISWA" NavigateUrl="~/Pages/Master/View/MasterDatasiswa.aspx"></asp:HyperLink></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="stat-panel">
                                                <div class="stat-cell bg-warning valign-middle">
                                                    <i class="fa fa-refresh bg-icon"></i>
                                                    <span class="text-lg"><strong>
                                                        <asp:Label ID="Label1" runat="server" Text="0"></asp:Label></strong></span><br>
                                                    <span class="text-bg">
                                                        <asp:HyperLink ID="HyperLink4" runat="server" Text="ABSENSI" NavigateUrl="#"></asp:HyperLink></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="stat-panel">
                                                <div class="stat-cell bg-success valign-middle">
                                                    <i class="fa fa-tasks bg-icon"></i>
                                                    <span class="text-lg"><strong>
                                                        <asp:Label ID="Label2" runat="server" Text="0"></asp:Label></strong></span><br>
                                                    <span class="text-bg">
                                                        <asp:HyperLink ID="HyperLink5" runat="server" Text="PASSING GRADE" NavigateUrl="#"></asp:HyperLink></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="stat-panel">
                                                <div class="stat-cell bg-danger valign-middle">
                                                    <i class="fa fa-cogs bg-icon"></i>
                                                    <span class="text-lg"><strong>
                                                        <asp:Label ID="Label3" runat="server" Text="0"></asp:Label></strong></span><br>
                                                    <span class="text-bg">
                                                        <asp:HyperLink ID="HyperLink6" runat="server" Text="POINT" NavigateUrl="#"></asp:HyperLink></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            <div class="panel panel-default panel-dark widget-profile">
                                                <div class="panel-heading">
                                                    <div class="widget-profile-header">
                                                        <span>Agama Siswa</span>
                                                    </div>
                                                </div>
                                                <!-- / .panel-heading -->
                                                <div class="table-responsive">
                                                    <asp:Chart ID="Chart3" runat="server" Width="300px" Height="230px">
                                                        <Titles>
                                                            <asp:Title ShadowOffset="3" Name="Items" />
                                                        </Titles>
                                                        <Legends>
                                                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default3" LegendStyle="Row" />
                                                        </Legends>
                                                        <Series>
                                                            <asp:Series Name="Default3" />
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="ChartArea3" BorderWidth="0" />
                                                        </ChartAreas>
                                                    </asp:Chart>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-sm-4">
                                            <div class="panel panel-default panel-dark widget-profile">
                                                <div class="panel-heading">
                                                    <div class="widget-profile-header">
                                                        <span>Jenis Kelamin Siswa</span>
                                                    </div>
                                                </div>
                                                <!-- / .panel-heading -->
                                                <div class="table-responsive">
                                                    <asp:Chart ID="Chart4" runat="server" Width="300px" Height="230px">
                                                        <Titles>
                                                            <asp:Title ShadowOffset="3" Name="Items" />
                                                        </Titles>
                                                        <Legends>
                                                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default4" LegendStyle="Row" />
                                                        </Legends>
                                                        <Series>
                                                            <asp:Series Name="Default4" />
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="ChartArea4" BorderWidth="0" />
                                                        </ChartAreas>
                                                    </asp:Chart>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>--%>

                            <div class="tab-pane" id="tab-hrd">
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
                            </div>

                            <div class="tab-pane" id="tab-tracking">
                                <div class="row">
                                    <div class="col-sm-12 overflow-x-table">                                       
                                        <div class="table-responsive">
                                        <asp:GridView ID="grdTracking" BorderStyle="None" SkinID="GridView" runat="server" AutoGenerateColumns="false" OnDataBound="grdTracking_DataBound" OnRowDataBound="grdTracking_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="namaCabang" HeaderText="Perwakilan / Unit" ItemStyle-Width="300px" />
                                                <asp:BoundField DataField="ket" HeaderText="Akun" ItemStyle-Width="350px" />
                                                <asp:BoundField DataField="M1" HeaderText="M" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField DataField="K1" HeaderText="K" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField DataField="M2" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="K2" HeaderText="K" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField DataField="M3" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="K3" HeaderText="K" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField DataField="M4" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="K4" HeaderText="K" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField DataField="M5" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="K5" HeaderText="K" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField DataField="M6" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="K6" HeaderText="K" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField DataField="M7" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="K7" HeaderText="K" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField DataField="M8" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="K8" HeaderText="K" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField DataField="M9" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="K9" HeaderText="K" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField DataField="M10" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="K10" HeaderText="K" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField DataField="M11" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="K11" HeaderText="K" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField DataField="M12" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="K12" HeaderText="K" ItemStyle-HorizontalAlign="Center"/>
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
