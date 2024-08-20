<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="ApproveStatusKrywn.aspx.cs" Inherits="eFinance.Pages.Transaksi.Approval.ApproveStatusKrywn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <%--START GRID--%>
    <asp:HiddenField ID="hdnnokaryawanH" runat="server" />
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdStsKrywn" DataKeyNames="noTransStsKrywn" SkinID="GridView" runat="server" OnRowDataBound="grdStsKrywn_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnnoTransStsKrywn" runat="server" Value='<%# Bind("noTransStsKrywn") %>'  />
                                    <asp:HiddenField ID="hdnlvlApprove" runat="server" Value='<%# Bind("posisiApprove") %>'  />
                                    <asp:HiddenField ID="hdnnoKrywn" runat="server" Value='<%# Eval("noKaryawan") %>' />
                                    <asp:HiddenField ID="hdnStatus" runat="server" Value='<%# Eval("stsPgwAkhir") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="kode" HeaderText="Kode" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="tgl" HeaderText="Tanggal" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                        <asp:BoundField DataField="nama" HeaderText="Nama Lengkap" HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" />
                        <asp:BoundField DataField="status" HeaderText="Status" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Level" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="lblApproveUser" runat="server" Text='<%# Bind("approveUser") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Alert" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblAlert" runat="server" Text='<%# Bind("statusApproveX") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Approved" ItemStyle-Width="3%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:RadioButton runat="server" GroupName="App" ID="rdoApprove" AutoPostBack="true" OnCheckedChanged="rdo_CheckedChanged" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Not Approved" ItemStyle-Width="3%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:RadioButton runat="server" GroupName="App" ID="rdoNotApprove" AutoPostBack="true" OnCheckedChanged="rdo_CheckedChanged" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:CheckBox ID="chkCheck" runat="server" CssClass="px chkCheck" Enabled="false" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="form-group row" runat="server" id="button">
            <div class="col-md-12">
                <div class="text-center">
                    <asp:Button runat="server" ID="btnApprove" CssClass="btn btn-primary" Text="Submit" OnClick="btnApprove_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>
    <div id="tabForm" runat="server" visible="false">
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
