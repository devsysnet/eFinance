<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RDetailTransaksicash1.aspx" Inherits="eFinance.Pages.Transaksi.View.RDetailTransaksicash1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="col-sm-6">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Periode :   </label>
                        <asp:DropDownList ID="cboMonth" runat="server" Width="120">
                            <asp:ListItem Value="1">Januari</asp:ListItem>
                            <asp:ListItem Value="2">Februari</asp:ListItem>
                            <asp:ListItem Value="3">Maret</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">Mei</asp:ListItem>
                            <asp:ListItem Value="6">Juni</asp:ListItem>
                            <asp:ListItem Value="7">Juli</asp:ListItem>
                            <asp:ListItem Value="8">Agustus</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">Oktober</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">Desember</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="cboYear" runat="server" Width="100">
                        </asp:DropDownList>

                    </div>
                </div>
            </div>

            <div class="col-sm-6">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Filter : </label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Width="250"></asp:DropDownList>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdAccount" DataKeyNames="kdrek" SkinID="GridView" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="COA" SortExpression="kdrek" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai" SortExpression="ptd" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="Nilai" runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdAccount1" DataKeyNames="kdrek" SkinID="GridView" runat="server" ShowHeader="false">
                    <Columns>
                        <asp:TemplateField HeaderText="COA" SortExpression="COA" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="Keterangan" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="ket" runat="server" Text='<%# Eval("ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai" SortExpression="nilai" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="nilai" runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
         <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdAccount2" DataKeyNames="kdrek" SkinID="GridView" runat="server" ShowHeader="false">
                    <Columns>
                        <asp:TemplateField HeaderText="COA" SortExpression="COA" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="Keterangan" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="ket" runat="server" Text='<%# Eval("ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai" SortExpression="nilai" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="nilai" runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
     <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdAccount3" DataKeyNames="kdrek" SkinID="GridView" runat="server" ShowHeader="false">
                    <Columns>
                        <asp:TemplateField HeaderText="COA" SortExpression="COA" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="kdrek" runat="server" Text='<%# Eval("kdrek").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keterangan" SortExpression="Keterangan" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="ket" runat="server" Text='<%# Eval("ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nilai" SortExpression="nilai" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="nilai" runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <%--</div>--%>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
