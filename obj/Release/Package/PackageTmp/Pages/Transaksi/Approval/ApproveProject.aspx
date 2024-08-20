<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="ApproveProject.aspx.cs" Inherits="eFinance.Pages.Transaksi.Approval.ApproveProject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    
    <div id="tabGrid" runat="server">       
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdProject" DataKeyNames="noProject" SkinID="GridView" runat="server" OnRowDataBound="grdProject_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnnoProject" runat="server" Value='<%# Bind("noProject") %>'  />
                                    <asp:HiddenField ID="hdnlvlApprove" runat="server" Value='<%# Bind("posisiApprove") %>'  />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Project" HeaderStyle-CssClass="text-center" HeaderText="Project" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="cabang" HeaderStyle-CssClass="text-center" HeaderText="Lokasi Peminta" ItemStyle-Width="17%" />
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
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Approve" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:RadioButton runat="server" GroupName="Sts" ID="rdoApprove" AutoPostBack="true" OnCheckedChanged="rdo_CheckedChanged" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Reject" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:RadioButton runat="server" GroupName="Sts" ID="rdoReject" AutoPostBack="true" OnCheckedChanged="rdo_CheckedChanged" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Keterangan" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:TextBox runat="server" ID="txtKeterangan" CssClass="form-control"></asp:TextBox>
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
                    <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
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
