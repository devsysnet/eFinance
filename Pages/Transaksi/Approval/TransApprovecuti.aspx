<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransApprovecuti.aspx.cs" Inherits="eFinance.Pages.Transaksi.Approval.TransApprovecuti" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <script>
          function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdBudget.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <asp:HiddenField ID="hdnnoCabangBudget" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <%--<div class="col-sm-3">
                                <div class="form-group">
                                    <label class="col-sm-5 control-label">Tahun <span class="mandatory">*</span></label>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ID="cboYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboYear_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>--%>
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
                            <div class="table-responsive overflow-x-table" id="pajak"  runat="server">
                                <asp:GridView ID="grdBudget" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnnocuti" runat="server" Value='<%# Bind("nocuti") %>'  />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                            <HeaderTemplate>
                                                <div class="text-center">
                                                    <asp:CheckBox ID="chkCheckAll" runat="server" CssClass="px" onclick="return CheckAllData(this);" />
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:CheckBox ID="chkCheck" runat="server" CssClass="px chkCheck" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Karyawan" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:Label runat="server" ID="txtKet" Text='<%# Eval("nama") %>' Width="300"></asp:Label>
                                                     <asp:HiddenField ID="hdnNoRek" runat="server" Value='<%# Eval("nokaryawan") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tanggal Pengajuan" ItemStyle-HorizontalAlign="center" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("tglpengajuan") %>' ID="txtJanuari" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tanggal Mulai" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("tglmulaicuti") %>' ID="txtFebuari" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tanggal Selesai" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("tglSelesaicuti") %>' ID="txtMaret" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Saldo" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("saldocuti") %>' ID="txtApril" Width="130"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Total Cuti" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" class="control-label" Text='<%# Eval("totalcuti") %>' ID="txtMei" Width="130"></asp:Label>
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
                                <asp:Button runat="server" Visible="false" ID="btnReject" CssClass="btn btn-success" Text="Reject" OnClick="btnReject_Click"></asp:Button>
                                <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click"></asp:Button>
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
