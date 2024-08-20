<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstTarifPajak.aspx.cs" Inherits="eFinance.Pages.Master.Input.MstTarifPajak" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <asp:GridView ID="grdtarifpajak" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Dari Penghasilan">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txttarifpajak" runat="server" CssClass="form-control money" MaxLength="20" placeholder="Dari Penghasilan"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Sampai Penghasilan">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txttarifpajak1" runat="server"  CssClass="form-control money" MaxLength="20" placeholder="Sampai Penghasilan"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="persen">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtpersen" runat="server"  CssClass="form-control money" MaxLength="10" placeholder="Persen" Width="80"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tingkat">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txttingkat" runat="server"  CssClass="form-control money" MaxLength="10" placeholder="Tingkat Perhitungan"  Width="80"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="text-center">
                                <asp:Button ID="btnAddRow" runat="server" CssClass="btn btn-success" Text="Add Row" OnClick="btnAddRow_Click" />
                                <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click" />
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

