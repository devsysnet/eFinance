<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="mtabelpiutang.aspx.cs" Inherits="eFinance.Pages.Master.Input.mtabelpiutang" %>
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
                                <asp:GridView ID="grdkomponengaji" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Jenis Piutang">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtkomponengaji" runat="server" CssClass="form-control" placeholder="Jenis Piutang"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="COA Debet">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <div class="text-center">
                                                    <asp:DropDownList ID="cbnorek" CssClass="form-control" runat="server"  Width="250"></asp:DropDownList>
                                                </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="COA Kredit">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <div class="text-center">
                                                    <asp:DropDownList ID="cbnorek1" CssClass="form-control" runat="server"  Width="250"></asp:DropDownList>
                                                </div>
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