<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RkesanggupanBayar1.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RkesanggupanBayar1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
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
			height: 150vh;
		}
		.tableFixHead table {
			width: 100%;
			min-width: 100%;
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
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnnoYysn" runat="server" />
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
                                        <div class="col-sm-1 control-label">
                                            <span>Filter Cari</span>
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="cboPerwakilan" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboPerwakilan_SelectedIndexChanged" Width="230"></asp:DropDownList>&nbsp;&nbsp;
                                            <asp:DropDownList ID="cboUnit" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboUnit_SelectedIndexChanged" Width="200"></asp:DropDownList>&nbsp;&nbsp;
                                             <asp:DropDownList ID="cbotahunajaran" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbothnAjaran_SelectedIndexChanged" Width="130"></asp:DropDownList>&nbsp;&nbsp;
                                             <asp:DropDownList ID="cboKelas" runat="server" CssClass="form-control" AutoPostBack="false" Width="150"></asp:DropDownList>                    
                                        </div>
                                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                        <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>  

                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12">
                                <%--<div class="table-responsive overflow-x-table">--%>
                                <div class="tableFixHead table-responsive overflow-x-table">
                                    <asp:GridView ID="grdAccount" SkinID="GridView" runat="server" AutoGenerateColumns="false" AlternatingRowStyle-CssClass="alt">
                                        <Columns>
                                        </Columns>
                                    </asp:GridView>
                               </div>
                                <%--</div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
        <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, e) {
            $('#BodyContent_grdAccount').gridviewScroll({
                width: '99%',
                height: 400,
                freezesize: 0, // Freeze Number of Columns. 
            });
        });
    </script>
            </ContentTemplate>
     
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>

 
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
