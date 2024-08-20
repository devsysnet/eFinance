﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="HomeDashboard.aspx.cs" Inherits="eFinance.HomeDashboard" %>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="TabName" runat="server" />
       <asp:HiddenField ID="HiddenField1"  runat="server" />
       <asp:HiddenField ID="HiddenField2"  runat="server" />

     <script>
       
         init.push(function () {
       var nilai1 = parseInt(document.getElementById('<%=HiddenField2.ClientID%>').value);
 
             var myConfig1 = {
                 type: "gauge",
                 globals: {
                     fontSize: 14
                 },
                 plotarea: {
                     marginTop: 80
                 },
                 plot: {
                     size: '100%',
                     valueBox: {
                         placement: 'center',
                         text: '%v', //default
                         fontSize: 35,

                     }
                 },

                 scaleR: {
                     aperture: 180,
                     minValue: 0,
                     maxValue: 100,
                     center: {
                         visible: false
                     },
                     tick: {
                         visible: true,
                         color: 'black'
                     },
                     item: {
                         offsetR: 0,

                     },
                     labels: ['0', '10', '20', '30', '40', '50', '60', '70', '80', '90', '100'],
                     ring: {
                         size: 50,
                         rules: [{
                             rule: '%v <= 25',
                             backgroundColor: '#ec6e67'
                         },
                           {
                               rule: '%v > 25 && %v < 66',
                               backgroundColor: '#f8ed34'
                           },

                           {
                               rule: '%v >= 66  && %v <= 100',
                               backgroundColor: '#55b56a'
                           }
                         ]
                     }
                 },
                 series: [{
                     values: [nilai1], // starting value
                     backgroundColor: 'black',
                     indicator: [10, 10, 10, 10, 0.75],
                     animation: {
                         effect: 2,
                         method: 1,
                         sequence: 4,
                         speed: 900
                     },
                 }]
             };
 
             zingchart.render({
                 id: 'myChart1',
                 data: myConfig1,
                 height: 330,
                 width: '100%'
             });
         })
  </script>
     <script>
       
         init.push(function () {
       var nilai = parseInt(document.getElementById('<%=HiddenField1.ClientID%>').value);
 
             var myConfig = {
                 type: "gauge",
                 globals: {
                     fontSize: 14
                 },
                 plotarea: {
                     marginTop: 80
                 },
                 plot: {
                     size: '100%',
                     valueBox: {
                         placement: 'center',
                         text: '%v', //default
                         fontSize: 35,
                         
                     }
                 },
         
                 scaleR: {
                     aperture: 180,
                     minValue: 0,
                     maxValue: 200,
                     center: {
                         visible: false
                     },
                     tick: {
                         visible: true,
                         color : 'black'
                     },
                     item: {
                         offsetR: 0,
                        
                     },
                     //labels: ['0', '10', '20', '30', '40', '50', '60', '70', '80', '90', '100', '110', '120'],
                     //labels: ['0', '25', '50', '75', '100', '105', '110', '115', '120'],
                     labels: ['0', '20', '40', '60', '80', '100', '120', '140', '160','180'],
                     ring: {
                         size: 50,
                         rules: [{
                             rule: '%v <= 40',
                             backgroundColor: '#55b56a'
                         },
                           {
                               rule: '%v > 40 && %v < 100',
                               backgroundColor: '#f8ed34'
                           },
                          
                           {
                               rule: '%v >= 100  && %v <= 200',
                               backgroundColor: '#ec6e67'
                           }
                         ]
                     }
                 },
                 series: [{
                     values: [nilai], // starting value
                     backgroundColor: 'black',
                     indicator: [10, 10, 10, 10, 0.75],
                     animation: {
                         effect: 2,
                         method: 1,
                         sequence: 4,
                         speed: 900
                     },
                 }]
             };
 
             zingchart.render({
                 id: 'myChart',
                 data: myConfig,
                 height: 330,
                 width: '100%'
             });
         })

  </script>
   
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
    <style type="text/css">
        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }     
    </style>
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
                                        <asp:DropDownList ID="cboTahun" runat="server" CssClass="form-control" AutoPostBack="true"   OnSelectedIndexChanged="cboTahun_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <!-- Nav tabs -->
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="active">
                                <a href="#tab-uang" data-toggle="tab" role="tab">Data Keuangan
                                </a>
                            </li>
                            <li>
                                <a href="#tab-siswa" data-toggle="tab" role="tab">Data Siswa
                                </a>
                            </li>
                            <li>
                                <a href="#tab-hrd" data-toggle="tab" role="tab">Data HRD
                                </a>
                            </li> 
                             <li>
                                <a href="#tab-bypass" data-toggle="tab" role="tab">Bypass to MyHomeSchool
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
                                    <div class="col-md-2">
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
                                    <div class="col-md-10">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive" style="margin-top:10px;">
                                                    <div class="row">
                                                        <div class="col-md-2"></div>
                                                        <div class="col-md-5">
                                                             <div style="z-index:-10" id="myChart"></div>
                                                            <asp:Button BorderStyle="None" style="background:none;padding:0;width:100%;position:absolute; margin-top:-100px;font-weight:bold;margin-left:-10px" runat="server" ID="btnBR" Text="BUDGET VS REALISASI" OnClick="linkBR_Click" />
                                                        </div>
                                                        <div class="col-md-5">
                                                            <div id="myChart1"></div>
                                                            <asp:Button BorderStyle="None" style="background:none;padding:0;width:100%;position:absolute; margin-top:-100px;font-weight:bold;margin-left:-10px" runat="server" ID="btnPP" Text="PENDAPATAN VS PENERIMAAN" OnClick="linkPP_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane" id="tab-siswa">
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
                            </div>

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
                                    <div class="col-sm-12">
                                        <div class="table-responsive"> 
                                            <asp:GridView ID="grdTracking" SkinID="GridView" runat="server" AutoGenerateColumns="false" OnDataBound="grdTracking_DataBound" OnRowDataBound="grdTracking_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="namaCabang" HeaderText="Perwakilan / Unit" ItemStyle-Width="300px" />
                                                    <asp:BoundField DataField="ket" HeaderText="Akun" ItemStyle-Width="350px" />
                                                    <asp:BoundField DataField="M1" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="K1" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="M2" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="K2" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="M3" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="K3" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="M4" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="K4" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="M5" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="K5" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="M6" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="K6" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="M7" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="K7" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="M8" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="K8" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="M9" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="K9" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="M10" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="K10" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="M11" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="K11" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="M12" HeaderText="M" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="K12" HeaderText="K" ItemStyle-HorizontalAlign="Center" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane" id="tab-bypass">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridViewBypass" SkinID="GridView" runat="server" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Perwakilan" DataField="namaPerwakilan" />
                                                    <asp:BoundField HeaderText="Cabang" DataField="namaCabang" />
                                                    <asp:TemplateField HeaderText="Bypass">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server"  Visible='<%# String.IsNullOrEmpty(Eval("linkUrl").ToString()) %>' Text="Tidak ada link URL"></asp:Label>
                                                            <a runat="server" target="_blank" class="btn btn-primary" Visible='<%# !String.IsNullOrEmpty(Eval("linkUrl").ToString()) %>' href='<%# Eval("linkUrl", "{0}/sak/bypass") %>'>Bypass</a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>


                           <%-- <div class="tab-pane" id="tab-balancecash">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-1 control-label text-left">Filter</label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="cboAkunBC" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboAkunBC_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="table-responsive"> 
                                            <asp:GridView ID="grdHarianGL" DataKeyNames="norek" SkinID="GridView" runat="server" AllowPaging="false" OnRowCommand="grdHarianGL_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <%# Container.DataItemIndex + 1 %>
                                                                <asp:HiddenField runat="server" ID="hdnbulan" Value='<%# Eval("bulan") %>' />
                                                                <asp:HiddenField runat="server" ID="hdnbln" Value='<%# Eval("bln") %>' />
                                                                <asp:HiddenField runat="server" ID="hdnthn" Value='<%# Eval("tahun") %>' />
                                                                <asp:HiddenField runat="server" ID="hdnnorek" Value='<%# Eval("norek") %>' />
                                                                <asp:HiddenField runat="server" ID="hdnnocabang" Value='<%# Eval("nocabang") %>' />
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ket" SortExpression="ket" HeaderText="Rekening" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="bulan" SortExpression="bulan" HeaderText="Bulan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="saldoawal" SortExpression="saldoawal" HeaderText="Saldo Awal" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField DataField="penerimaan" SortExpression="penerimaan" HeaderText="Penerimaan" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField DataField="pengeluaran" SortExpression="pengeluaran" HeaderText="Pengeluaran" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField DataField="saldoakhir" SortExpression="saldoakhir" HeaderText="Saldo Akhir" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField DataField="tglmax" SortExpression="tglmax" HeaderText="Tanggal Posting" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left" />
                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <asp:Button ID="btnSelectHarian" runat="server" class="btn btn-xs btn-labeled btn-info" Text="Harian Kas" CommandName="SelectHarian" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <asp:Button ID="btnSelectDetail" runat="server" class="btn btn-xs btn-labeled btn-success" Text="Detil Kas" CommandName="SelectDetail" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
													<asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
														<ItemTemplate>
															<div class="text-center">
																<asp:Button ID="btnSelectkas" runat="server" class="btn btn-xs btn-labeled btn-warning" Text="Bulanan" CommandName="SelectDetailkas" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
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
                    </div>


                </div>
            </div>
        </div>
    </div>

    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgHarianKas" runat="server" PopupControlID="panelHarianKas" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelHarianKas" runat="server" Width="1200" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Buku Harian Kas</h4>
            </div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="modal-body col-overflow-500">
                <h4 class="modal-title">Bulan <asp:Label runat="server" ID="lblbln"></asp:Label> Tahun <asp:Label runat="server" ID="lblthn"></asp:Label>, Rekening <asp:Label runat="server" ID="lblrek"></asp:Label>, Unit <asp:Label runat="server" ID="lblunit"></asp:Label></h4>
                <asp:GridView ID="grdAccount" DataKeyNames="nomorkode" SkinID="GridView" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="Nomor Kode" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="nomorkode" runat="server" Text='<%# Eval("nomorkode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tanggal" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("tgl") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipe" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="jenis" runat="server" Text='<%# Eval("tipe") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dari/Untuk" SortExpression="Ket" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="cust" runat="server" Text='<%# Eval("cust") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Uraian" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="uraian" runat="server" Text='<%# Eval("uraian") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Debet" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="debet" runat="server" Text='<%# Eval("debet") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kredit" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="kredit" runat="server" Text='<%# Eval("kredit") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Saldo Akhir" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="kredit" runat="server" Text='<%# Eval("Saldoakhir") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton2" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgDetilKas" runat="server" PopupControlID="panelDetilKas" TargetControlID="LinkButton2" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelDetilKas" runat="server" Width="1200" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Detil Kas</h4>
            </div>
            <asp:Label ID="Label4" runat="server"></asp:Label>
            <div class="modal-body col-overflow-500">
                <h4 class="modal-title">Bulan <asp:Label runat="server" ID="lblbln2"></asp:Label> Tahun <asp:Label runat="server" ID="lblthn2"></asp:Label>, Unit <asp:Label runat="server" ID="lblunit2"></asp:Label></h4>
                <asp:GridView ID="GridView1" DataKeyNames="kdrek" SkinID="GridView" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="COA" SortExpression="kdrek" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai" SortExpression="ptd" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="Nilai" runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:GridView ID="grdAccount1" DataKeyNames="kdrek" SkinID="GridView" runat="server" ShowHeader="false">
                    <Columns>
                        <asp:TemplateField HeaderText="COA" SortExpression="COA" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="Keterangan" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="ket" runat="server" Text='<%# Eval("ket") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai" SortExpression="nilai" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="nilai" runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:GridView ID="grdAccount2" DataKeyNames="kdrek" SkinID="GridView" runat="server" ShowHeader="false">
                    <Columns>
                        <asp:TemplateField HeaderText="COA" SortExpression="COA" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="Keterangan" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="ket" runat="server" Text='<%# Eval("ket") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai" SortExpression="nilai" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="nilai" runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:GridView ID="grdAccount3" DataKeyNames="kdrek" SkinID="GridView" runat="server" ShowHeader="false">
                    <Columns>
                        <asp:TemplateField HeaderText="COA" SortExpression="COA" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="Keterangan" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="ket" runat="server" Text='<%# Eval("ket") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai" SortExpression="nilai" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="nilai" runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>
	
	<asp:LinkButton ID="LinkButton3" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="grddeatailbln" runat="server" PopupControlID="panel1" TargetControlID="LinkButton3" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panel1" runat="server" Width="1200" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Buku Harian Kas</h4>
            </div>
            <asp:Label ID="Label8" runat="server"></asp:Label>
            <div class="modal-body col-overflow-500">
                <h4 class="modal-title">Bulan <asp:Label runat="server" ID="Label5"></asp:Label> Tahun <asp:Label runat="server" ID="Label7"></asp:Label>, Unit <asp:Label runat="server" ID="Label6"></asp:Label></h4>
                <asp:GridView ID="GridView2" DataKeyNames="kdrek" SkinID="GridView" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="COA" SortExpression="kdrek" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai" SortExpression="ptd" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="Nilai" runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:GridView ID="GridView3" DataKeyNames="kdrek" SkinID="GridView" runat="server" ShowHeader="false">
                    <Columns>
                        <asp:TemplateField HeaderText="COA" SortExpression="COA" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="Keterangan" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="ket" runat="server" Text='<%# Eval("ket") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai" SortExpression="nilai" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="nilai" runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>
     <script>
	     $(function () {
	         LoadChart1();

	     });

	     Sys.Application.add_init(appl_init);
	     function appl_init() {
	         var pgRegMgr = Sys.WebForms.PageRequestManager.getInstance();
	         pgRegMgr.add_beginRequest(LoadChart1);
	         pgRegMgr.add_endRequest(LoadChart1);
	     }
        function LoadChart1() {
       var nilai = parseInt(document.getElementById('<%=HiddenField2.ClientID%>').value);
 
             var myConfig = {
                 type: "gauge",
                 globals: {
                     fontSize: 14
                 },
                 plotarea: {
                     marginTop: 80
                 },
                 plot: {
                     size: '100%',
                     valueBox: {
                         placement: 'center',
                         text: '%v', //default
                         fontSize: 35,
                         
                     }
                 },
         
                 scaleR: {
                     aperture: 180,
                     minValue: 0,
                     maxValue: 200,
                     center: {
                         visible: false
                     },
                     tick: {
                         visible: true,
                         color : 'black'
                     },
                     item: {
                         offsetR: 0,
                        
                     },
                     //labels: ['0', '10', '20', '30', '40', '50', '60', '70', '80', '90', '100', '110', '120'],
                     //labels: ['0', '25', '50', '75', '100', '105', '110', '115', '120'],
                     labels: ['0', '20', '40', '60', '80', '100', '120', '140', '160','180'],
                     ring: {
                         size: 50,
                         rules: [{
                             rule: '%v <= 25',
                             backgroundColor: '#ec6e67'
                         },
                           {
                               rule: '%v > 25 && %v < 66',
                               backgroundColor: '#f8ed34'
                           },

                           {
                               rule: '%v >= 66  ',
                               backgroundColor: '#55b56a'
                           }
                         ]
                     }
                 },
                 series: [{
                     values: [nilai], // starting value
                     backgroundColor: 'black',
                     indicator: [10, 10, 10, 10, 0.75],
                     animation: {
                         effect: 2,
                         method: 1,
                         sequence: 4,
                         speed: 900
                     },
                 }]
             };
 
             return zingchart.render({
                 id: 'myChart1',
                 data: myConfig,
                 height: 330,
                 width: '100%'
             });
        }
	     LoadChart();
  </script>
	 <script>
	     $(function () {
	         LoadChart();

	     });

	     Sys.Application.add_init(appl_init);
	     function appl_init() {
	         var pgRegMgr = Sys.WebForms.PageRequestManager.getInstance();
	         pgRegMgr.add_beginRequest(LoadChart);
	         pgRegMgr.add_endRequest(LoadChart);
	     }
        function LoadChart() {
       var nilai = parseInt(document.getElementById('<%=HiddenField1.ClientID%>').value);
 
             var myConfig = {
                 type: "gauge",
                 globals: {
                     fontSize: 14
                 },
                 plotarea: {
                     marginTop: 80
                 },
                 plot: {
                     size: '100%',
                     valueBox: {
                         placement: 'center',
                         text: '%v', //default
                         fontSize: 35,
                         
                     }
                 },
         
                 scaleR: {
                     aperture: 180,
                     minValue: 0,
                     maxValue: 200,
                     center: {
                         visible: false
                     },
                     tick: {
                         visible: true,
                         color : 'black'
                     },
                     item: {
                         offsetR: 0,
                        
                     },
                     //labels: ['0', '10', '20', '30', '40', '50', '60', '70', '80', '90', '100', '110', '120'],
                     //labels: ['0', '25', '50', '75', '100', '105', '110', '115', '120'],
                     labels: ['0', '20', '40', '60', '80', '100', '120', '140', '160','180'],
                     ring: {
                         size: 50,
                         rules: [{
                             rule: '%v <= 40',
                             backgroundColor: '#55b56a'
                         },
                           {
                               rule: '%v > 40 && %v < 100',
                               backgroundColor: '#f8ed34'
                           },
                          
                           {
                               rule: '%v >= 100  && %v <= 200',
                               backgroundColor: '#ec6e67'
                           }
                         ]
                     }
                 },
                 series: [{
                     values: [nilai], // starting value
                     backgroundColor: 'black',
                     indicator: [10, 10, 10, 10, 0.75],
                     animation: {
                         effect: 2,
                         method: 1,
                         sequence: 4,
                         speed: 900
                     },
                 }]
             };
 
             return zingchart.render({
                 id: 'myChart',
                 data: myConfig,
                 height: 330,
                 width: '100%'
             });
        }
	     LoadChart();
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
