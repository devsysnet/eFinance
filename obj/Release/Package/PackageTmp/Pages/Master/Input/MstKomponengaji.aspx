﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstKomponengaji.aspx.cs" Inherits="eFinance.Pages.Master.Input.MstKomponengaji" %>
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
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Komponen Gaji">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtkomponengaji" runat="server" CssClass="form-control" placeholder="komponengaji"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kategori">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cboKategori" runat="server" CssClass="form-control"> </asp:DropDownList>
                                                 </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Jenis">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cboJenis" runat="server" CssClass="form-control" Width="150">
                                                    <asp:ListItem Value="1">Tetap</asp:ListItem>
                                                    <asp:ListItem Value="0">Tidak Tetap</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Unsur PPH21" >
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cbopph21" runat="server" CssClass="form-control" Width="80">
                                                    <asp:ListItem Value="1">Ya</asp:ListItem>
                                                    <asp:ListItem Value="0">Tidak</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Unsur Absensi" >
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cbopengurang" runat="server" CssClass="form-control" Width="80">
                                                     <asp:ListItem Value="1">Ya</asp:ListItem>
                                                     <asp:ListItem Value="0">Tidak</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Unsur Iuaran">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <div class="text-center">
                                                    <asp:DropDownList ID="cbobpjs" runat="server" CssClass="form-control" Width="80">
                                                    <asp:ListItem Value="1">Ya</asp:ListItem>
                                                    <asp:ListItem Value="0">Tidak</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tahunan/Bulanan">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <div class="text-center">
                                                    <asp:DropDownList ID="cbothnbln" runat="server" CssClass="form-control" Width="100">
                                                    <asp:ListItem Value="1">Bulanan</asp:ListItem>
                                                    <asp:ListItem Value="0">Tahunan</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="COA">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <div class="text-center">
                                                    <asp:DropDownList ID="cbnorek" CssClass="form-control" runat="server"  Width="150"></asp:DropDownList>
                                                </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Jenis Kegiatan">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <div class="text-center">
                                                    <asp:DropDownList ID="cbojeniskegiatan" CssClass="form-control" runat="server"  Width="150"></asp:DropDownList>
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