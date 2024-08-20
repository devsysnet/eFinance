<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Rpenyusutanasset.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.Rpenyusutanasset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-8">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-8">

                                            <label>lokasi :   </label>
                                            <asp:DropDownList ID="cboLokasi" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboLokasi_SelectedIndexChanged" Width="150"></asp:DropDownList>
                                            <asp:DropDownList ID="cboSubLokasi" runat="server" Width="150"></asp:DropDownList>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <label>Cabang : </label>
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Enabled="true"></asp:DropDownList>
                                            <asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Search" OnClick="btnPosting_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <asp:GridView ID="grdAccount" DataKeyNames="KodeAsset" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Kode Asset" SortExpression="Ket" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("KodeAsset").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nama Asset" SortExpression="ptd" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="Grup" runat="server" Text='<%# Eval("namaasset") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tanggal Asset" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="Grup" runat="server" Text='<%# Eval("tglAsset") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nilai Perolehan" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="Grup" runat="server" Text='<%# Eval("nilaiPerolehan", "{0:#,0.00}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nilai Penyusutan" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="Grup" runat="server" Text='<%# Eval("nilaipenyThn", "{0:#,0.00}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nilai Buku" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="Grup" runat="server" Text='<%# Eval("nilaiBuku", "{0:#,0.00}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Uraian" SortExpression="ptd" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="Grup" runat="server" Text='<%# Eval("Uraian") %>'></asp:Label>
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


