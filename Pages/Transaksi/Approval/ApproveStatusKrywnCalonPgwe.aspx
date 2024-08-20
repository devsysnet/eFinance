<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="ApproveStatusKrywnCalonPgwe.aspx.cs" Inherits="eFinance.Pages.Transaksi.Approval.ApproveStatusKrywnCalonPgwe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <asp:GridView ID="grdStsKrywn" DataKeyNames="noKaryawan" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnnoKrywn" runat="server" Value='<%# Eval("noKaryawan") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="idPeg" HeaderText="Kode" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="nama" HeaderText="Nama Lengkap" HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" />
                                        <asp:BoundField DataField="tgllahir" HeaderText="Tgl Lahir" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}"/>
                                        <asp:BoundField DataField="tglmasuk" HeaderText="Tgl Masuk" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}"/>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Approved" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:RadioButton runat="server" GroupName="App" ID="rdoApprove" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Not Approved" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:RadioButton runat="server" GroupName="App" ID="rdoNotApprove" />
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
                                    <asp:Button runat="server" ID="btnApprove" CssClass="btn btn-primary" Text="Submit" OnClick="btnApprove_Click"></asp:Button>
                                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click"></asp:Button>
                                </div>
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
