<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="HomeHRD.aspx.cs" Inherits="eFinance.HomeHRD" %>

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
    <style type="text/css">
        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }     
    </style>
    <div class="row" style="background:#ededed">
        <div class="form-horizontal" style="background:#ededed">
            <div class="col-sm-12">
                <div class="" style="background:#ededed">
                    <div class="panel-body" id="" style="background:#ededed;">
                        <div class="row">

                                    <div class="col-sm-3" style="margin-left:30px;background:white;padding:20px;padding-bottom:30px;border-radius:20px; box-shadow:0 .5rem 1rem rgba(0,0,0,.15)!important">
                                         <label class=" control-label text-left">Yayasan</label>

                                        <asp:DropDownList ID="cboYayasan" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboYayasan_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                    </div>
                                   
                                    <div class="col-sm-3" style="margin-left:10px;background:white;padding:20px;padding-bottom:30px;border-radius:20px; box-shadow:0 .5rem 1rem rgba(0,0,0,.15)!important" id="comboPerwakilan" runat="server">
                                         <label class=" control-label text-left">Perwakilan</label>

                                        <asp:DropDownList ID="cboPerwakilan" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboPerwakilan_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3" id="comboUnit" runat="server"  style="margin-left:10px;background:white;padding:20px;padding-bottom:30px;border-radius:20px; box-shadow:0 .5rem 1rem rgba(0,0,0,.15)!important">
                                         <label class=" control-label text-left">Cabang</label>

                                        <asp:DropDownList ID="cboUnit" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboUnit_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2"  style="margin-left:10px;background:white;padding:20px;padding-bottom:30px;border-radius:20px; box-shadow:0 .5rem 1rem rgba(0,0,0,.15)!important">
                                         <label  class=" control-label text-left">Tahun</label>

                                        <asp:DropDownList ID="cboTahun" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboTahun_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                        </div>
                        <br />
                        <!-- Nav tabs -->
                        <ul class="nav nav-tabs" style="display:none" role="tablist">
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
                           
                            <%--<li>
                                <a href="#tab-balancecash" data-toggle="tab" role="tab">Balance Cash
                                </a>
                            </li>--%>
                             <%--<li>
                                <a href="#tab-tracking" data-toggle="tab" role="tab">Tracking
                                </a>
                            </li>--%>
                             <li>
                                <a href="#tab-bypass" data-toggle="tab" role="tab">Bypass to MyHomeSchool
                                </a>
                            </li>
                        </ul>
                        <!-- Tab panes -->
                        <div class="tab-content tab-content">
                            <div role="tabpanel" class="tab-pane active" id="tab-uang">
                                <div class="row">
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="panel panel-info panel-dark widget-profile"  style="display:none" >
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
                                        <div class="panel panel-warning panel-dark widget-profile"  style="display:none" >
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
                                        <div class="panel panel-danger panel-dark widget-profile"  style="display:none" >
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
                                        <div class="panel panel-info panel-dark widget-profile"  style="display:none" >
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
                                        <div class="panel panel-dark bg-pa-purple widget-profile"  style="display:none" >
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
                                    <div class="col-md-3" style="margin-left:30px;border-radius:20px;background:white;box-shadow:0 .5rem 1rem rgba(0,0,0,.15)!important;padding-top:20px" >
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
                                    <div class="col-md-3" style="margin-left:10px;border-radius:20px;background:white;box-shadow:0 .5rem 1rem rgba(0,0,0,.15)!important">
                                        
                                        <div class="row" >

                                            <div class="col-md-10" >
                                                <div class="table-responsive">
                                                    <asp:Chart ID="Chart1" runat="server"   Height="415px">
                                                        <Legends >
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
                                                                <AxisY Title="Grafik Jumlah Agama">
                                                                </AxisY>
                                                            </asp:ChartArea>
                                                        </ChartAreas>
                                                    </asp:Chart>
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <div class="row" style="display:none">
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
                                       <div class="col-md-3">
                                           <div class="row" >
                                            <div class="col-md-6" style="background-color:#ededed;" >
                                                          <div class="stat-panel" style="margin-left:5px;background:#ededed;width:352px; ">
                                                <!-- Success background, bordered, without top and bottom borders, without left border, without padding, vertically and horizontally centered text, large text -->
                                                <a href="Pages/Master/View/MasterDataKaryawan.aspx" class="stat-cell col-xs-4     no-border-vr no-border-l no-padding valign-middle text-center text-lg" style=" border-radius:20px  0px 0px 20px;background-color:#5f8aee;color:white">
                                         
                                                    <table style="width:105px;margin-left:2px;>
                                                        <tr>
                                                            <td style="text-align:center">
                                                                 <span style="font-size:30px;margin-top:-38px;position:absolute; ">   <asp:Label ID="lbltotKaryawan" runat="server" Text="0"></asp:Label></span>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                                <td style="text-align:center">
                                                                <span style="position:absolute ;margin-top:3px">
                                                        <svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" fill="currentColor" class="bi bi-people-fill" viewBox="0 0 16 16">
                                                        <path d="M7 14s-1 0-1-1 1-4 5-4 5 3 5 4-1 1-1 1zm4-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6m-5.784 6A2.24 2.24 0 0 1 5 13c0-1.355.68-2.75 1.936-3.72A6.3 6.3 0 0 0 5 9c-4 0-5 3-5 4s1 1 1 1zM4.5 8a2.5 2.5 0 1 0 0-5 2.5 2.5 0 0 0 0 5"/>
                                                        </svg>
                                                    </span>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                 
                                                               
                                                </a>
                                                <!-- /.stat-cell -->
                                                <!-- Without padding, extra small text -->
                                                <div class="stat-cell col-xs-8 no-padding valign-middle" >
                                                    <!-- Add parent div.stat-rows if you want build nested rows -->
                                                    <div class="stat-rows" >
                                                        <div class="stat-row" style="">
                                                            <!-- Success background, small padding, vertically aligned text -->
                                                            <a href="#" class="stat-cell     valign-middle" style="padding:20px; border-radius:0px 20px 0px 0px;background-color:#64d380;color:white;font-size:14pt; ">
                                                               <span  style="margin-top:20px"> <asp:Label ID="lbltotGuru" runat="server" Text="0"></asp:Label>
                                                                guru</span>
									                         <span style="margin-left:84px;position:absolute;margin-top:-7px"> <svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" fill="currentColor" class="bi bi-person-fill-check" viewBox="0 0 16 16">
  <path d="M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7m1.679-4.493-1.335 2.226a.75.75 0 0 1-1.174.144l-.774-.773a.5.5 0 0 1 .708-.708l.547.548 1.17-1.951a.5.5 0 1 1 .858.514M11 5a3 3 0 1 1-6 0 3 3 0 0 1 6 0"/>
  <path d="M2 13c0 1 1 1 1 1h5.256A4.5 4.5 0 0 1 8 12.5a4.5 4.5 0 0 1 1.544-3.393Q8.844 9.002 8 9c-5 0-6 3-6 4"/>
</svg></span>
                                                            </a>
                                                        </div>
                                                        <div class="stat-row" >
                                                            <!-- Success darker background, small padding, vertically aligned text -->
                                                            <a href="#" class="stat-cell     valign-middle" style="padding:20px; border-radius:0px 0px 20px 0px;background-color:#fe697c;color:white;font-size:14pt; " >
                                                                <asp:Label ID="lbltotNonGuru" runat="server" Text="0"></asp:Label>
                                                                non guru
									                            <span style="margin-left:50px;position:absolute;margin-top:-7px"><svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" fill="currentColor" class="bi bi-person-fill-x" viewBox="0 0 16 16">
  <path d="M11 5a3 3 0 1 1-6 0 3 3 0 0 1 6 0m-9 8c0 1 1 1 1 1h5.256A4.5 4.5 0 0 1 8 12.5a4.5 4.5 0 0 1 1.544-3.393Q8.844 9.002 8 9c-5 0-6 3-6 4"/>
  <path d="M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7m-.646-4.854.646.647.646-.647a.5.5 0 0 1 .708.708l-.647.646.647.646a.5.5 0 0 1-.708.708l-.646-.647-.646.647a.5.5 0 0 1-.708-.708l.647-.646-.647-.646a.5.5 0 0 1 .708-.708"/>
</svg></span>
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <!-- /.stat-rows -->
                                                </div>
                                                <!-- /.stat-cell -->
                                            </div>
                                                </div>
                                        </div>
                                        <div class="row" >
                                            <div class="col-md-6" style="margin-left:15px;border-radius:20px;background:white;box-shadow:0 .5rem 1rem rgba(0,0,0,.15)!important;padding:20px;width:90%">

                                     <span style="font-weight:bold;font-size:12pt">   Jenis Kelamin</span>
                                                <br /><br />
                                                <table style="width:100%">
                                                    <tr>
                                                        <td align="center">
                                        <img src="https://i.ibb.co/cXk1WQ8/fml-removebg-preview.png" alt="fml-removebg-preview" style="width:37px;">

                                                        </td>
                                                        <td align="center">
                                                <img src="https://i.ibb.co/wrzPhKj/man-removebg-preview.png" alt="man-removebg-preview" style="width:37px;">

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <span style="font-weight:bold;font-size:50px;color:#fe697c"><asp:Label runat="server" ID="female"></asp:Label></span>
                                                        </td>
                                                        <td align="center">
                                                            <span style="font-weight:bold;font-size:50px;color:#01b0f3"><asp:Label runat="server" ID="male"></asp:Label></span>
                                                        </td>
                                                    </tr>
                                                </table>
                                                </div>
                                                
                                            </div>
                                       </div>
                                           <div class="col-md-3" style="margin-left:30px;border-radius:20px;background:white;box-shadow:0 .5rem 1rem rgba(0,0,0,.15)!important;width:280px;padding:20px;margin-top:0px;height:410px">
                                        <span style="font-weight:bold;font-size:12pt">   Lama Kerja</span>
                                        <table   style="border:1px solid black;width:100%;margin-top:50px"> 
                                            <tr   style="border:1px solid black">
                                                <td style="padding:10px">
                                                   <span style="font-size:12pt"> 0 - 5 Tahun</span>
                                                </td>
                                                <td align="center"   style="border:1px solid black;padding:10px">
                                                    <span><asp:Label runat="server" ID="Label9"></asp:Label> Orang</span>
                                                </td>
                                            </tr>
                                            <tr   style="border:1px solid black">
                                                <td style="padding:10px">
                                                    <span style="font-size:12pt"> 6 - 10 Tahun</span>
                                                </td>
                                                 <td align="center"   style="border:1px solid black;padding:10px">
                                                    <span><asp:Label runat="server" ID="Label10"></asp:Label> Orang</span>
                                                </td>
                                            </tr>
                                           <tr   style="border:1px solid black">
                                                <td style="padding:10px">
                                                    <span style="font-size:12pt"> 11 - 15 Tahun</span>
                                                </td>
                                                 <td align="center"   style="border:1px solid black;padding:10px">
                                                    <span><asp:Label runat="server" ID="Label11"></asp:Label> Orang</span>
                                                </td>
                                            </tr>
                                          <tr   style="border:1px solid black">
                                                <td style="padding:10px">
                                                    <span style="font-size:12pt"> 16 - 20 Tahun</span>
                                                </td>
                                                 <td align="center"   style="border:1px solid black;padding:10px">
                                                    <span><asp:Label runat="server" ID="Label12"></asp:Label> Orang</span>
                                                </td>
                                            </tr>
                                           <tr   style="border:1px solid black">
                                                <td style="padding:10px">
                                                    <span style="font-size:12pt">20 Tahun Keatas</span>
                                                </td>
                                                  <td align="center"  style="border:1px solid black;padding:10px">
                                                    <span><asp:Label runat="server" ID="Label13"></asp:Label> Orang</span>
                                                </td>
                                            </tr>
                                             <tr   style="border:1px solid black">
                                                <td style="padding:10px">
                                                    <span style="font-size:12pt">Pensiun</span>
                                                </td>
                                                  <td align="center"  style="border:1px solid black;padding:10px">
                                                    <span><asp:Label runat="server" ID="Label14"></asp:Label> Orang</span>
                                                </td>
                                            </tr>
                                        </table>
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
