<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="eFinance.Home" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
           <asp:HiddenField ID="HiddenField1"  runat="server" />
       <asp:HiddenField ID="HiddenField2"  runat="server" />
     <asp:HiddenField ID="TabName" runat="server" />
     <script type="text/javascript" src="https://unpkg.com/xlsx@0.15.1/dist/xlsx.full.min.js"></script>
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

        function ExportToExcel(type, fn, dl) {
            var elt = document.getElementById('BodyContent_GridView4');
            var wb = XLSX.utils.table_to_book(elt, { sheet: "sheet1" });
            console.log(elt)
            return dl ?
                XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
                XLSX.writeFile(wb, fn || ('daftar_ppdb.' + (type || 'xlsx')));
        }
    </script>
    <style type="text/css">
        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }     
        th{
            width:50px;
        }
       
    </style>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                                </a>
                            </li>
                              <li>
                                <a href="#tab-budget" data-toggle="tab" role="tab">Budget vs Realisasi
                                </a>
                            </li>
                         <%--     <li>
                                <a href="#tab-tracking" data-toggle="tab" role="tab">Tagihan
                                </a>
                            </li>--%>
                            <li>
                                <a href="#tab-siswa" data-toggle="tab" role="tab">Data Siswa
                                </a>
                            </li>
                            <li>
                                <a href="#tab-hrd" data-toggle="tab" role="tab">Data HRD
                                </a>
                            </li>
                           
                            <li>
                                <a href="#tab-balancecash" data-toggle="tab" role="tab">PPDB
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
                                                    <%--<span class="text-xs">PENDAPATAN (ytd)</span>--%>
                                                    <asp:Button BorderStyle="None" style="background:none;padding:0" runat="server" ID="linkRekabPendapatan" Text="PENDAPATAN (ytd)" OnClick="linkRekabPendapatan_Click" />
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
                                                    <%--<span class="text-xs">PENERIMAAN (ytd)</span>--%>
                                                    <asp:Button BorderStyle="None" style="background:none;padding:0" runat="server" ID="linkRekabPenerimaan" Text="PENERIMAAN (ytd)" OnClick="linkRekabPenerimaan_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="panel panel-info panel-dark widget-profile">
                                            <div class="panel-heading">
                                                <div class="widget-profile-header">
                                                    <span class="text-md"><strong>
                                                        <asp:Label ID="lblTerimaKas" runat="server" Text="0"></asp:Label></strong></span><br>
                                                    <%--<span class="text-xs">SALDO KAS</span>--%>
                                                    <asp:Button BorderStyle="None" style="background:none;padding:0" runat="server" ID="linkRsaldokas" Text="Saldo Kas" OnClick="linksaldoKas_Click" />
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
                                                    <%--<span class="text-xs">SALDO BANK</span>--%>
                                                     <asp:Button BorderStyle="None" style="background:none;padding:0" runat="server" ID="linkRsaldoBank" Text="Saldo Bank" OnClick="linksaldoBank_Click" />
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
                                       
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane" id="tab-siswa">
                                <div class="row">
                                    <%--<br />--%>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-4" style="margin-left:15px;display:none">
                                            <div class="stat-panel">
                                                <!-- Success background, bordered, without top and bottom borders, without left border, without padding, vertically and horizontally centered text, large text -->
                                              <a href="#" class="stat-cell col-xs-5 bg-info bordered no-border-vr no-border-l no-padding valign-middle text-center text-lg">
                                                    <i class="fa fa-users"></i>&nbsp;&nbsp;<asp:Label ID="lblTotalSiswa1" runat="server" Text="0"></asp:Label>
                                                </a>
                                                <!-- /.stat-cell -->
                                                <!-- Without padding, extra small text -->
                                                
                                                <!-- /.stat-cell -->
                                            </div>
                                            <!-- /.stat-panel -->

                                        </div>
                                            <div class="form-group" >
                                                
                                                <div class="col-sm-3">
                                                    <asp:DropDownList Visible="false" ID="cboSts" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboSts_SelectedIndexChanged">
                                                         <asp:ListItem Value="">Semua</asp:ListItem>
                                                         <asp:ListItem Value="1">Aktif</asp:ListItem>
                                                         <asp:ListItem Value="0">Tidak Aktif</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <table width="100%">
                                        <tr>
                                          <%--  <td valign="top" rowspan="2">
                                               
                                            </td>--%>
                                            <td valign="top"   style="padding-left:50px">
                                                <asp:GridView Height="280" ID="GridView55" SkinID="GridView" runat="server" AutoGenerateColumns="false" Width="300">
                                                <Columns>
                                                    <asp:BoundField DataField="agama" HeaderText="Agama"   />
                                                    <asp:BoundField DataField="jml" HeaderText="Total"   />
                                                </Columns>
                                            </asp:GridView>
                                            </td>
                                            <td valign="center" style="padding-left:30px;height:140px">
                                                  <div  class="stat-cell bg-info valign-middle" style="height:150px">
                                                    <i class="fa fa-users bg-icon"></i>
                                                    <span class="text-lg"><strong>
                                                        <asp:Label ID="lblTotalSiswa" runat="server" Text="0" Font-Size="20"></asp:Label></strong></span><br>
                                                    <span class="text-bg">
                                                        <asp:HyperLink ID="HyperLink3" runat="server" Text="TOTAL SISWA" NavigateUrl="~/Pages/Transaksi/View/mDatasiswa.aspx"></asp:HyperLink></span>

                                                </div>
                                                <div class="stat-cell no-padding valign-middle" style="height:80px;width:300px;">
                                                    <!-- Add parent div.stat-rows if you want build nested rows -->
                                                    <div class="stat-rows">
                                                        <div class="stat-row">
                                                            <!-- Success background, small padding, vertically aligned text -->
                                                            <a href="#" style="font-weight:bold" class="stat-cell bg-success padding-sm valign-middle">
                                                                <asp:Label ID="lblTotalSiswaactive" runat="server" Text="0" Font-Size="16"></asp:Label>
                                                                Active
									                            <i class="fa fa-users pull-right"></i>
                                                            </a>
                                                        </div>
                                                        <div class="stat-row">
                                                            <!-- Success darker background, small padding, vertically aligned text -->
                                                            <a href="#" style="font-weight:bold" class="stat-cell bg-danger padding-sm valign-middle">
                                                                <asp:Label ID="lblTotalSiswaanonctive" runat="server" Text="0" Font-Size="16"></asp:Label>
                                                                Non Active
									                            <i class="fa fa-users pull-right"></i>
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <!-- /.stat-rows -->
                                                </div>
                                            </div>
                                            </div>
                                            </td>
                                             <td valign="center" style="padding-left:30px;height:100px">
                                                 <table>
                                                     <div class="row" style="margin-top:10px;width:250px;margin-left:-10px">
                                                     <div class="col-md-4" style="margin-left:5px;border-radius:10px;background:white;box-shadow:0 .5rem 1rem rgba(0,0,0,.15)!important;padding:50px;width:100%;">                                                                   
                                                        <span style="font-weight:bold;font-size:12pt">Jenis Kelamin</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                              <img alt="fml-removebg-preview" src="<%=Func.BaseUrl%>assets/images/jenis_kelamin/female.png" style="width:20px;"> <span style="font-weight:bold;font-size:20px;color:#fe697c">
                                                               <asp:Label ID="jnskelaminlakisiswa" runat="server"></asp:Label>
                                                                 </span>
                                                                <img alt="man-removebg-preview" src="<%=Func.BaseUrl%>assets/images/jenis_kelamin/male.png" style="width:20px;"> <span style="font-weight:bold;font-size:20px;color:#01b0f3">
                                                                <asp:Label ID="jnskelaminperempuansiswa" runat="server"></asp:Label>
                                                                </span></img></img>
                                                            </div>
                                                       </div>              
                                                    </table>    
                                              </td>                
                                            
                                         <tr>
                                             <td colspan="3" valign="top" style="padding-left:20px">
                                                 <br />
                                                   <asp:GridView Height="221px" ID="GridView10" SkinID="GridView" runat="server"  OnDataBound="GridView10_DataBound" AutoGenerateColumns="false">
                                        
                                            </asp:GridView>
                                             </td>
                                         </tr>
                                    </table> 
                              </div>
                              </div>

                            <div class="tab-pane" id="tab-hrd">
                                <table width="100%">
                                    <tr>
                                        <td valign="top">
                                             &nbsp;</td>
                                        <td valign="top" style="padding-left:20px">
                                                <asp:GridView Width="200" ID="GridView9" SkinID="GridView" runat="server" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField ItemStyle-Width="40" DataField="agama" HeaderText="Agama"   />
                                                    <asp:BoundField ItemStyle-Width="40"  DataField="jml" HeaderText="Total"   />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                        <td valign="top"  style="padding-left:10px;height:150px">
                                            <div class="stat-panel" style="width:250px;height:150px">
                                                <!-- Success background, bordered, without top and bottom borders, without left border, without padding, vertically and horizontally centered text, large text -->
                                                <a href="Pages/Master/View/MasterDataKaryawan.aspx" class="stat-cell col-xs-4     no-border-vr no-border-l no-padding valign-middle text-center text-lg" style=" border-radius:5px  0px 0px 5px;background-color:#5f8aee;color:white;">
                                         
                                                  <span style="font-size:14px;margin-top:-20px;position:absolute;margin-left:-10px">   <asp:Label ID="lbltotKaryawan" runat="server" Font-Size="15" Text="0"></asp:Label></span>
                                                               <span style="position:absolute ;margin-left:-10px;margin-top:-3px">
                                                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-people-fill" viewBox="0 0 16 16">
                                                      <path d="M7 14s-1 0-1-1 1-4 5-4 5 3 5 4-1 1-1 1zm4-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6m-5.784 6A2.24 2.24 0 0 1 5 13c0-1.355.68-2.75 1.936-3.72A6.3 6.3 0 0 0 5 9c-4 0-5 3-5 4s1 1 1 1zM4.5 8a2.5 2.5 0 1 0 0-5 2.5 2.5 0 0 0 0 5"/>
                                                    </svg>
                                                    </span>
                                                </a>
                                                <!-- /.stat-cell -->
                                                <!-- Without padding, extra small text -->
                                                <div class="stat-cell col-xs-8 no-padding valign-middle" >
                                                    <!-- Add parent div.stat-rows if you want build nested rows -->
                                                    <div class="stat-rows" >
                                                        <div class="stat-row" style="">
                                                            <!-- Success background, small padding, vertically aligned text -->
                                                            <a href="#" class="stat-cell     valign-middle" style="padding:20px; border-radius:0px 5px 0px 0px;background-color:#64d380;color:white;font-size:13pt; ">
                                                               <span  style="font-size:14px; margin-top:20px"> <asp:Label Font-Size="15" ID="lbltotGuru" runat="server" Text="0"></asp:Label>
                                                                guru</span>
									                      
                                                            </a>
                                                        </div>
                                                        <div class="stat-row" >
                                                            <!-- Success darker background, small padding, vertically aligned text -->
                                                            <a href="#" class="stat-cell     valign-middle" style="padding:5px; border-radius:0px 0px 5px 0px;background-color:#fe697c;color:white;font-size:13pt; " >
                                                               <span style="font-size:14px"> <asp:Label ID="lbltotNonGuru" runat="server" Font-Size="15" Text="0"></asp:Label>
                                                                non guru</span>
									                      
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <!-- /.stat-rows -->
                                                </div>
                                               
                                        </td>
                                        <td valign="top" style="padding-left:10px">
                                             <div style="box-shadow:0 .5rem 1rem rgba(0,0,0,.15)!important; padding:10px">
                                                    <span style="font-weight:bold;font-size:12pt">Jenis Kelamin</span>
                                                <br /><br />
                                                <table style="width:100%">
                                                    <tr>
                                                        <td align="center">
                                                            <img src="<%=Func.BaseUrl%>assets/images/jenis_kelamin/female.png" alt="fml-removebg-preview" style="width:20px;">

                                                        </td>
                                                        <td align="center">
                                                             <img src="<%=Func.BaseUrl%>assets/images/jenis_kelamin/male.png" alt="man-removebg-preview" style="width:20px;">
    
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <span style="font-weight:bold;font-size:20px;color:#fe697c"><asp:Label runat="server" ID="female"></asp:Label></span>
                                                        </td>
                                                        <td align="center">
                                                            <span style="font-weight:bold;font-size:20px;color:#01b0f3"><asp:Label runat="server" ID="male"></asp:Label></span>
                                                        </td>
                                                    </tr>
                                                </table>
                                                </div>
                                        </td>
                                        <td valign="top" rowspan="2">
                                             <div style="margin-left:10px;border-radius:20px;background:white;box-shadow:0 .5rem 1rem rgba(0,0,0,.15)!important;width:333px; padding:10px;margin-top:0px;height:350px">
                                        <span style="font-weight:bold;font-size:10pt"> Lama Kerja</span>
                                        <table   style="border:1px solid black;width:100%;margin-top:50px"> 
                                            <tr   style="border:1px solid black">
                                                <td style="padding:8px">
                                                   <span style="font-size:10pt"> 0 - 5 Tahun</span>
                                                </td>
                                                <td align="center"   style="border:1px solid black;padding:8px">
                                                    <span style="font-size:10pt"><asp:Label runat="server" ID="Label9"></asp:Label> Orang</span>
                                                </td>
                                            </tr>
                                            <tr   style="border:1px solid black">
                                                <td style="padding:8px">
                                                    <span style="font-size:10pt"> 6 - 10 Tahun</span>
                                                </td>
                                                 <td align="center"   style="border:1px solid black;padding:8px">
                                                    <span style="font-size:10pt"><asp:Label runat="server" ID="Label10"></asp:Label> Orang</span>
                                                </td>
                                            </tr>
                                           <tr   style="border:1px solid black">
                                                <td style="padding:8px">
                                                    <span style="font-size:10pt"> 11 - 15 Tahun</span>
                                                </td>
                                                 <td align="center" style="border:1px solid black;padding:8px">
                                                    <span style="font-size:10pt"><asp:Label runat="server" ID="Label11"></asp:Label> Orang</span>
                                                </td>
                                            </tr>
                                          <tr   style="border:1px solid black">
                                                <td style="padding:8px">
                                                    <span style="font-size:10pt"> 16 - 20 Tahun</span>
                                                </td>
                                                 <td align="center"   style="border:1px solid black;padding:8px">
                                                    <span style="font-size:10pt"><asp:Label runat="server" ID="Label12"></asp:Label> Orang</span>
                                                </td>
                                            </tr>
                                           <tr   style="border:1px solid black">
                                                <td style="padding:8px">
                                                    <span style="font-size:10pt">20++ Tahun </span>
                                                </td>
                                                  <td align="center"  style="border:1px solid black;padding:8px">
                                                    <span style="font-size:10pt"><asp:Label runat="server" ID="Label13"></asp:Label> Orang</span>
                                                </td>
                                            </tr>
                                             <tr   style="border:1px solid black">
                                                <td style="padding:8px">
                                                    <span style="font-size:10pt">Pensiun</span>
                                                </td>
                                                  <td align="center"  style="border:1px solid black;padding:8px">
                                                    <span style="font-size:10pt"><asp:Label runat="server" ID="Label14"></asp:Label> Orang</span>
                                                </td>
                                            </tr>
                                        </table>
                                   </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" colspan="4" style="padding-left:20px" width="600">
                                             <asp:GridView  ID="GridView11" SkinID="GridView" runat="server" AutoGenerateColumns="false">
                                                <Columns>
                                                   
                                                     <asp:BoundField DataField="namacabang" HeaderText="Nama Cabang"   />
                                                     <asp:BoundField DataField="statuspegawai" HeaderText="Status Pegawai"   />
                                                    <asp:BoundField DataField="total" HeaderText="Total"   />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                <div class="row">             
                               </div>
                                <div class="row" style="display:none">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            <div class="stat-panel">
                                                <!-- Success background, bordered, without top and bottom borders, without left border, without padding, vertically and horizontally centered text, large text -->
                                                <a href="Pages/Master/View/MasterDataKaryawan.aspx" class="stat-cell col-xs-5 bg-info bordered no-border-vr no-border-l no-padding valign-middle text-center text-lg">
                                                    <i class="fa fa-users"></i>&nbsp;&nbsp;<asp:Label ID="lbltotKaryawan1" runat="server" Text="0"></asp:Label>
                                                </a>
                                                <!-- /.stat-cell -->
                                                <!-- Without padding, extra small text -->
                                                <div class="stat-cell col-xs-7 no-padding valign-middle">
                                                    <!-- Add parent div.stat-rows if you want build nested rows -->
                                                    <div class="stat-rows">
                                                        <div class="stat-row">
                                                            <!-- Success background, small padding, vertically aligned text -->
                                                            <a href="#" class="stat-cell bg-warning padding-sm valign-middle">
                                                                <asp:Label ID="lbltotGuru1" runat="server" Text="0"></asp:Label>
                                                                guru
									                            <i class="fa fa-users pull-right"></i>
                                                            </a>
                                                        </div>
                                                        <div class="stat-row">
                                                            <!-- Success darker background, small padding, vertically aligned text -->
                                                            <a href="#" class="stat-cell bg-success padding-sm valign-middle">
                                                                <asp:Label ID="lbltotNonGuru1" runat="server" Text="0"></asp:Label>
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
                             
               <%--           <div class="tab-pane" id="tab-tracking">
                                <table>
                                    <tr>
                                        <td valign="top">
                                             <div class="panel   widget-profile" style="background-color:#FF7F3E;color:white">
                                            <div class="panel-heading" style="background-color:#FF6969">
                                                <div class="widget-profile-header">
                                                    <span class="text-md"><strong>
                                                        <asp:Label ID="lbltagian" runat="server" Text="0"></asp:Label></strong></span><br>
                                                    <asp:Button BorderStyle="None" style="background:none;padding:0" runat="server" ID="Button1" Text="Tagihan"  />
                                                </div>
                                            </div>
                                        </div>
                                        </td>
                                         <td valign="top" style="padding-left:20px">
                                               <div class="panel   widget-profile" style="background-color:#15F5BA;color:white">
                                            <div class="panel-heading" style="background-color:#40A578">
                                                <div class="widget-profile-header">
                                                        <span class="text-md"><strong>
                                                        <asp:Label ID="lblpendapatan" runat="server" Text="0"></asp:Label></strong></span><br>
                                                    <asp:Button BorderStyle="None" style="background:none;padding:0" runat="server" ID="Button2" Text="Pendapatan"   />
                                                </div>
                                            </div>
                                        </div>
                                        </td>
                                         <td valign="top" style="padding-left:20px">
                                               <div class="panel   widget-profile" style="background-color:#102C57;color:white">
                                            <div class="panel-heading" style="background-color:#615EFC">
                                                <div class="widget-profile-header">
                                                    <span class="text-md"><strong>
                                                        <asp:Label ID="lblrasio" runat="server" Text="0"></asp:Label></strong></span><br>
                                                    <asp:Button BorderStyle="None" style="background:none;padding:0" runat="server" ID="Button3" Text="Rasio" />
                                                </div>
                                            </div>
                                        </div>
                                        </td>
                                    </tr>
                                </table>
                                <div class="row">

                                    <div class="col-sm-12">
                                      <asp:Button  runat="server"  CssClass="btn btn-primary btn-sm" ID="Button4" Text="Search" OnClick="searchtagihan" />
                                        <div class="table-responsive"> 
                                           <asp:GridView ID="GridView12" SkinID="GridView" runat="server" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Perwakilan" DataField="parent" />
                                                    <asp:BoundField HeaderText="Cabang" DataField="cabang" />
                                                   <asp:BoundField HeaderText="Jan" DataField="jan" />
                                                    <asp:BoundField HeaderText="Feb" DataField="feb" />
                                                    <asp:BoundField HeaderText="Mar" DataField="mar" />
                                                    <asp:BoundField HeaderText="Apr" DataField="apr" />
                                                    <asp:BoundField HeaderText="Mei" DataField="mei" />
                                                    <asp:BoundField HeaderText="Jun" DataField="jun" />
                                                    <asp:BoundField HeaderText="Jul" DataField="jul" />
                                                    <asp:BoundField HeaderText="Ags" DataField="ags" />
                                                    <asp:BoundField HeaderText="Sept" DataField="sept" />
                                                    <asp:BoundField HeaderText="Okt" DataField="okt" />
                                                    <asp:BoundField HeaderText="Nov" DataField="nov" />
                                                    <asp:BoundField HeaderText="Des" DataField="decs" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>

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
                                                            <a runat="server" target="_blank" class="btn btn-primary" Visible='<%# !String.IsNullOrEmpty(Eval("linkUrl").ToString()) %>' href='<%# Eval("linkUrl", "{0}/sak/bypass?email=")+Eval("emailUser") %>'>Bypass</a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane" id="tab-budget">
                                 <div class="row">
                                            <div class="col-md-10">
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


                            <div class="tab-pane" id="tab-balancecash">
                                <button onclick="ExportToExcel('xlsx')" class="btn btn-success">Download Excel</button>
                                <div style="height:20px"></div>

                                <div style="width:100px">
                                 <asp:DropDownList ID="cboFilterCabang" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboFilterCabang_SelectedIndexChanged" Width="150"></asp:DropDownList>
                                 
                                </div>
                               
                                <div style="height:20px"></div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView4" SkinID="GridView" runat="server" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Perwakilan" DataField="namaPerwakilan" />
                                                    <asp:BoundField HeaderText="Cabang" DataField="namaCabang" />
                                                    <asp:BoundField HeaderText="Tanggal" DataField="tgl" DataFormatString="{0:dd-MMM-yyyy}"  />
                                                    <asp:BoundField HeaderText="Total" DataField="totalsiswa" />
                                                    <asp:BoundField HeaderText="Laki - Laki" DataField="laki" />
                                                    <asp:BoundField HeaderText="Perempuan" DataField="perempuan" />
                                                    <asp:BoundField HeaderText="Internal" DataField="internal" />
                                                    <asp:BoundField HeaderText="External" DataField="ext" />
                                                    <asp:BoundField HeaderText="Status Masuk" DataField="statusmasuk" />
                                                    <asp:BoundField HeaderText="Status Tidak Masuk" DataField="statustdkmasuk" />
                                                    <asp:BoundField HeaderText="Tahun Ajaran" DataField="tahunAjaran" />
                                                    
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
	   </ContentTemplate>
      
    </asp:UpdatePanel>
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
	     LoadChart1();
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
