<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransRevisiBudget.aspx.cs" Inherits="eFinance.Pages.Transaksi.Approval.TransRevisiBudget" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnnoCabangBudget" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <label class="col-sm-5 control-label">Tahun <span class="mandatory">*</span></label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="cboYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboYear_SelectedIndexChanged" Enabled="false">
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
                            <div class="table-responsive overflow-x-table">
                                <asp:GridView ID="grdBudget" DataKeyNames="ket" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Akun" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="text-center">
                                                            <asp:HiddenField ID="hdnnoBudgetD" runat="server" Value='<%# Eval("noBudgetD") %>' />
                                                            <asp:HiddenField ID="hdnnoBudget" runat="server" Value='<%# Eval("noBudget") %>' />
                                                            <asp:HiddenField ID="hdnNoRek" runat="server" Value='<%# Eval("noRek") %>' />
                                                            <asp:Label runat="server" ID="txtkdRek" Text='<%# Eval("kdRek") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Akun" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Januari" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("jan") %>' ID="txtJanuari"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Februari" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("feb") %>' ID="txtFebuari"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Maret" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("mar") %>' ID="txtMaret"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="April" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("apr") %>' ID="txtApril"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Mei" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("mei") %>' ID="txtMei"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Juni" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("jun") %>' ID="txtJuni"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Juli" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("jul") %>' ID="txtJuli"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Agustus" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("ags") %>' ID="txtAgustus"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="September" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("sep") %>' ID="txtSeptember"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Oktober" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("okt") %>' ID="txtOktober"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="November" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("nov") %>' ID="txtNovember"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Desember" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("dese") %>' ID="txtDesember"></asp:TextBox>
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
                                <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Revisi" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click"></asp:Button>
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
