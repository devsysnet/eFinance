<%@ Page Title="" Language="C#"MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstVBankCodeView.aspx.cs" Inherits="eFinance.Pages.Master.View.MstVBankCodeView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    
    <asp:HiddenField ID="hdnId" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-8">

                            </div>
                            <div class="col-sm-4">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <label>Search :</label>
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                            <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnCari_Click" CausesValidation="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdVoucher" DataKeyNames="nobank" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdVoucher_PageIndexChanging"
                                        OnSelectedIndexChanged="grdVoucher_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="kdRek" SortExpression="kdRek" HeaderText="Kode Rekening" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="kodeVoucher" SortExpression="kodeVoucher" HeaderText="Kode Voucher" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="stsKasBank" SortExpression="stsKasBank" HeaderText="Status" ItemStyle-Width="20%" />
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Detail" CommandName="Select" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row">
                            <div class="col-sm-12">
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
                                                        <asp:HiddenField ID="hdnNoBank" runat="server" Value='<%# Eval("nobank") %>' />
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
                                                        <asp:HiddenField ID="hdnCurrency" runat="server" Value='<%# Eval("nomatauang") %>' />
                                                        <asp:DropDownList ID="cboCurrency" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Bank Voucher">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox ID="txtBank" runat="server" CssClass="form-control" Text='<%# Eval("kodeVoucher") %>' Enabled="false"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Categori">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:HiddenField ID="hdnKategori" runat="server" Value='<%# Eval("stsKasBank") %>'/>
                                                        <asp:DropDownList ID="cboKategori" runat="server" CssClass="form-control" Enabled="false">
                                                            <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                                            <asp:ListItem Value="Bank">Bank</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-12">
                                <div class="text-center">
                                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabView" runat="server" visible="false">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
