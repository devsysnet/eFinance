<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="ReportSlipGajiKaryawan.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.ReportSlipGajiKaryawan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <script type="text/javascript" src="https://unpkg.com/xlsx@0.15.1/dist/xlsx.full.min.js"></script>
    <script>
        function OpenReport() {
            //            OpenReportViewer();
            window.open('../../../Report/ReportViewer.aspx', 'name', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,modal=yes,width=1000,height=600');
        }

        function ExportToExcel(type, fn, dl) {
            var elt = document.getElementById('BodyContent_grdAccount');
            var wb = XLSX.utils.table_to_book(elt, { sheet: "sheet1" });
            return dl ?
                XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
                XLSX.writeFile(wb, fn || ('slipgajikaryawan.' + (type || 'xlsx')));
        }
    </script>
<style>
       .tableFixHead tr td:first-child {
			position: -webkit-sticky;
			position: sticky;
			left: 0;
            margin-top:20px;
			background: #ccc;
            z-index:1;
		}
         .tableFixHead {
			position: relative;
			width:100%;
			z-index: 100;
			margin: auto;
			overflow: scroll;
			height: 80vh;
		}
		.tableFixHead table {
			width: 100%;
			min-width: 100px;
			margin: auto;
			border-collapse: separate;
			border-spacing: 0;
		}
		.table-wrap {
			position: relative;
		}
		.tableFixHead th,
		.tableFixHead td {
			padding: 5px 10px;
			border: 1px solid #000;
			#background: #fff;
			vertical-align: top;
			text-align: left;
		}
		.tableFixHead  th {
			background: #f6bf71;
			position: -webkit-sticky;
			position: sticky;
			top: 0;
            z-index:9;

		}
		td{
			z-index: -4;

		}
       
        .tableFixHead   tr:nth-child(2) th {
            top: 25px;
        }
    </style>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="form-group">
                                   
                                    <div class="col-sm-12">
                                        <label> Filter Cabang  :</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged" Width="250"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                                                 <asp:DropDownList ID="cbobln" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbobln_SelectedIndexChanged" Width="150">
                                                 <asp:ListItem Value="1">Januari</asp:ListItem>
                                                    <asp:ListItem Value="2">Februari</asp:ListItem>
                                                    <asp:ListItem Value="3">Maret</asp:ListItem>
                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                    <asp:ListItem Value="5">Mei</asp:ListItem>
                                                    <asp:ListItem Value="6">Juni</asp:ListItem>
                                                    <asp:ListItem Value="7">Juli</asp:ListItem>
                                                    <asp:ListItem Value="8">Agustus</asp:ListItem>
                                                    <asp:ListItem Value="9">September</asp:ListItem>
                                                    <asp:ListItem Value="10">Oktober</asp:ListItem>
                                                    <asp:ListItem Value="11">November</asp:ListItem>
                                                    <asp:ListItem Value="12">Desember</asp:ListItem>
                                                 </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                                                 <asp:DropDownList ID="cbothn" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbothn_SelectedIndexChanged" width="100">
                                                </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                                               <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="Cari" OnClick="Click"></asp:Button>
                                               <asp:Button runat="server" ID="btnPrint" CssClass="btn btn-success" Text="Print" OnClick="btnPrint_Click"></asp:Button>
                                               <asp:Button runat="server" ID="email" CssClass="btn btn-success" Text="Email" OnClick="btnemail_Click"></asp:Button> 
                                                <button onclick="ExportToExcel('xlsx')" class="btn btn-warning">Download</button>
                                     </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12">
                                    <div class="tableFixHead table-responsive overflow-x-table">
                                    <asp:GridView ID="grdAccount" DataKeyNames="Nama_Pegawai" SkinID="GridView" runat="server" AutoGenerateColumns="false"  AlternatingRowStyle-CssClass="alt">
                                        <Columns>

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




