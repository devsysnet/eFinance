<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Approvalkenaikangaji.aspx.cs" Inherits="eFinance.Pages.Transaksi.Approval.Approvalkenaikangaji" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive overflow-x-table">
                                <asp:GridView ID="grdGajiKala" DataKeyNames="noNaikgaji" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnnoNaikKala" runat="server" Value='<%# Bind("noNaikKala") %>'  />
                                                    <asp:HiddenField ID="hdnnoKaryawan" runat="server" Value='<%# Bind("noKaryawan") %>'  />
                                                    <asp:HiddenField ID="hdnlvlApprove" runat="server" Value='<%# Bind("posisiApprove") %>'  />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kode" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKode" runat="server" Text='<%# Bind("kdtran") %>' Width="150"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tanggal" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="dtnaikgaji" runat="server" Text='<%# Bind("tgl","{0:dd MMM yyyy}") %>' Width="80"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Unit" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblunit" runat="server" Text='<%# Bind("unit") %>' Width="150"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Karyawan" ItemStyle-Width="30%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNama" runat="server" Text='<%# Bind("nama") %>' Width="250"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Alert" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAlert" runat="server" Text='<%# Bind("statusApproveX") %>' Width="150"></asp:Label>
                                                </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Level" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblApproveUser" runat="server" Text='<%# Bind("approveUser") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Approve" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:RadioButton runat="server" GroupName="ET" ID="rdoApprove" AutoPostBack="True" OnCheckedChanged="rdo_CheckedChanged" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Reject" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:RadioButton runat="server" GroupName="ET" ID="rdoReject" AutoPostBack="True" OnCheckedChanged="rdo_CheckedChanged" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Note" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtKeterangan" runat="server" CssClass="form-control" Width="200" />
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
                    </div>
                    <div class="row" id="button" runat="server">
                        <div class="col-sm-12 ">
                            <div class="text-center">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
