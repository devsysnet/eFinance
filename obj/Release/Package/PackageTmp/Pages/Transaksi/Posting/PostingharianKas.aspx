<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="PostingharianKas.aspx.cs" Inherits="eFinance.Pages.Transaksi.Posting.PostingharianKas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="form-horizontal">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-1 control-label text-right">Filter</label>
                                <div class="col-sm-11">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="cboAccount" OnSelectedIndexChanged="cboAccount_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <div class="form-inline">
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="dtMulai" runat="server" CssClass="form-control date" Enabled="false"></asp:TextBox>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <asp:GridView ID="grdHarianGL" DataKeyNames="nomorKode" SkinID="GridView" runat="server" AllowPaging="false" PageSize="10" OnPageIndexChanging="grdHarianGL_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="nomorKode" SortExpression="nomorKode" HeaderText="Nomor Transaksi" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="tgl" SortExpression="tgl" HeaderText="Tgl Transaksi" ItemStyle-Width="10%" DataFormatString="{0:dd MMM yyyy}" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="Type" SortExpression="Jenis" HeaderText="Jenis Transaksi" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="kdrek" SortExpression="kdrek" HeaderText="Kode Rekening" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="ket" SortExpression="ket" HeaderText="Rekening" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="Type" SortExpression="Jenis" HeaderText="Jenis Transaksi" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="Cust" SortExpression="Cust" HeaderText="Dari/Untuk" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="Uraian" SortExpression="Uraian" HeaderText="Keterangan" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="Debet">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:Label ID="lblDebet" runat="server" Text='<%# Bind("nilai", "{0:#,0.00}") %>' Width="180"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Posting" OnClick="btnPosting_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
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
