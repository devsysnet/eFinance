<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransApproveBudget.aspx.cs" Inherits="eFinance.Pages.Transaksi.Approval.TransApproveBudget" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnnoCabangBudget" runat="server" />
   <style>
        .tableFixHead tr td:first-child {
			position: -webkit-sticky;
			position: sticky;
			left: 0;
            margin-top:20px;
			background: #ccc;
            z-index:1;
		}
       .tableFixHead tr td:first-child + td {
			position: -webkit-sticky;
			position: sticky;
			left: 10px;
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
			height: 60vh;
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
			text-align: center;
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

    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <label class="col-sm-5 control-label">Tahun <span class="mandatory">*</span></label>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ID="cboYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboYear_SelectedIndexChanged" Width="100">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-9">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">Cabang <span class="mandatory">*</span></label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="cboCabang" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                        
                            <div class="tableFixHead table-responsive overflow-x-tablee" id="pajak" visible="false" runat="server">
                                <asp:GridView ID="grdBudget" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnnoBudget" runat="server" Value='<%# Bind("noBudget") %>'  />
                                                    <asp:HiddenField ID="hdnnoBudgetD" runat="server" Value='<%# Bind("noBudgetD") %>'  />
                                                    <asp:HiddenField ID="hdnlvlApprove" runat="server" Value='<%# Bind("posisiApprove") %>'  />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Akun" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:Label runat="server" ID="txtKet" Text='<%# Eval("Ket") %>' Width="300"></asp:Label>
                                                     <asp:HiddenField ID="hdnNoRek" runat="server" Value='<%# Eval("noRek") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Januari" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget1", "{0:#,0.00}") %>' ID="txtJanuari" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Februari" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget2", "{0:#,0.00}") %>' ID="txtFebuari" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Maret" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget3", "{0:#,0.00}") %>' ID="txtMaret" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="April" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget4", "{0:#,0.00}") %>' ID="txtApril" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Mei" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget5", "{0:#,0.00}") %>' ID="txtMei" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Juni" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget6", "{0:#,0.00}") %>' ID="txtJuni" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Juli" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget7", "{0:#,0.00}") %>' ID="txtJuli" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Agustus" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget8", "{0:#,0.00}") %>' ID="txtAgustus" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="September" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget9", "{0:#,0.00}") %>' ID="txtSeptember" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Oktober" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget10", "{0:#,0.00}") %>' ID="txtOktober" Width="130"></asp:Label>                
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="November" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget11", "{0:#,0.00}") %>' ID="txtNovember" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Desember" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget12", "{0:#,0.00}") %>' ID="txtDesember" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                             <div class="tableFixHead table-responsive overflow-x-table" id="tahunan" visible="false" runat="server">
                                <asp:GridView ID="GridView2" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnnoBudget" runat="server" Value='<%# Bind("noBudget") %>'  />
                                                    <asp:HiddenField ID="hdnnoBudgetD" runat="server" Value='<%# Bind("noBudgetD") %>'  />
                                                    <asp:HiddenField ID="hdnlvlApprove" runat="server" Value='<%# Bind("posisiApprove") %>'  />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Akun" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:Label runat="server" ID="txtKet" Text='<%# Eval("Ket") %>' Width="300"></asp:Label>
                                                     <asp:HiddenField ID="hdnNoRek" runat="server" Value='<%# Eval("noRek") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nilai" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget1", "{0:#,0.00}") %>' ID="txtJanuari" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="tableFixHead table-responsive overflow-x-table" id="tahunAjaran" visible="false" runat="server">
                                <asp:GridView ID="GridView1" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnnoBudget" runat="server" Value='<%# Bind("noBudget") %>'  />
                                                    <asp:HiddenField ID="hdnnoBudgetD" runat="server" Value='<%# Bind("noBudgetD") %>'  />
                                                    <asp:HiddenField ID="hdnlvlApprove" runat="server" Value='<%# Bind("posisiApprove") %>'  />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Akun" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:Label runat="server" ID="txtKet" Text='<%# Eval("Ket") %>' Width="300"></asp:Label>
                                                     <asp:HiddenField ID="hdnNoRek" runat="server" Value='<%# Eval("noRek") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Juli" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget7", "{0:#,0.00}") %>' ID="txtJuli" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Agustus" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget8", "{0:#,0.00}") %>' ID="txtAgustus" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="September" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget9", "{0:#,0.00}") %>' ID="txtSeptember" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Oktober" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget10", "{0:#,0.00}") %>' ID="txtOktober" Width="130"></asp:Label>                
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="November" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget11", "{0:#,0.00}") %>' ID="txtNovember" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Desember" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget12", "{0:#,0.00}") %>' ID="txtDesember" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Januari" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget1", "{0:#,0.00}") %>' ID="txtJanuari" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Februari" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget2", "{0:#,0.00}") %>' ID="txtFebuari" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Maret" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget3", "{0:#,0.00}") %>' ID="txtMaret" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="April" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget4", "{0:#,0.00}") %>' ID="txtApril" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Mei" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget5", "{0:#,0.00}") %>' ID="txtMei" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Juni" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("budget6", "{0:#,0.00}") %>' ID="txtJuni" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row" runat="server" id="showhidebutton">
                        <div class="col-md-12">
                            <div class="text-center">
                                <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Approve" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                <asp:Button runat="server" ID="btnReject" CssClass="btn btn-success" Text="Reject" OnClick="btnReject_Click"></asp:Button>
                                <%--<asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click"></asp:Button>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgReject" runat="server" PopupControlID="panelReject" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelReject" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Reject</h4>
            </div>
            <div class="modal-body col-overflow-500">
                <div class="row">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <label class="col-sm-2 control-label">Catatan </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtCatatanReject" runat="server" CssClass="form-control" TextMode="MultiLine" Width="600" Height="150" />
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="modal-footer">
                    <asp:Button ID="btnBatal" runat="server" Text="Tutup" CssClass="btn btn-default" OnClick="btnBatal_Click" />
                    <asp:Button ID="btnRejectData" runat="server" Text="Reject" CssClass="btn btn-danger" OnClick="btnRejectData_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
