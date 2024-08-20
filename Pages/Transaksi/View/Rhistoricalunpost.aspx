<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Rhistoricalunpost.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.Rhistoricalunpost" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-horizontal">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-1 control-label">Periode :</label>
                                            <div class="col-sm-6">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="dtMulai" runat="server" CssClass="form-control date"></asp:TextBox>&nbsp; 
                                                    <asp:TextBox ID="dtSampai" runat="server" CssClass="form-control date"></asp:TextBox>&nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-horizontal">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Cabang : </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Account : </label>
                                            <div class="col-sm-10">
                                                <div class="input-group">
                                                    <div class="form-inline">
                                                        <div class="input-group-btn">
                                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cboAccount"></asp:DropDownList>
                                                            <asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Search" OnClick="btnPosting_Click"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <asp:GridView ID="grdAccount" DataKeyNames="norek" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Unit/Perwakilan" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="Unit" runat="server" Text='<%# Eval("namaCabang").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tanggal Unpost" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("tgl") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rekening" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="Rekening" runat="server" Text='<%# Eval("ket") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tanggal Mulai Unpost" SortExpression="Ket" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="tgl" runat="server" Text='<%# Eval("tglunpost") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      </Columns>
                                </asp:GridView>
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

