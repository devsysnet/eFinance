<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstVBankCode.aspx.cs" Inherits="eFinance.Pages.Master.Input.MstVBankCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="grdVBankCode" SkinID="GridView" runat="server" OnRowDataBound="grdVBankCode_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <%# Container.DataItemIndex + 1 %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" HeaderText="Account Code">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:Label ID="lblCode" runat="server" CssClass="form-control" Text='<%# Eval("kdRek") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Account Name">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:Label ID="lblName" runat="server" CssClass="form-control" Text='<%# Eval("Ket") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Currency">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:DropDownList ID="cboCurrency" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Bank Voucher">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox ID="txtBank" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Categori">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:HiddenField ID="hdnKategori" runat="server" Value='<%# Eval("jenis") %>' />
                                                <asp:DropDownList ID="cboKategori" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                                    <asp:ListItem Value="Bank">Bank</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-12">
                                <div class="text-center">
                                    <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click"></asp:Button>
                                    <asp:Button runat="server" ID="btnResetRek" CssClass="btn btn-danger" Text="Reset" OnClick="btnResetRek_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
