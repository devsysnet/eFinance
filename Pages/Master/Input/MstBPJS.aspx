<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstBPJS.aspx.cs" Inherits="eFinance.Pages.Master.Input.MstBPJS" %>
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
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Jenis Iuran">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtjenisIuran" runat="server" CssClass="form-control" placeholder="Jenis Iuran"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Persen Perusahaan">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtpersenperusahaan" runat="server" CssClass="form-control money" placeholder="persen" Width="50"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Persen Karyawan">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtpersenkaryawan" runat="server" CssClass="form-control money" placeholder="persen" Width="50"></asp:TextBox>
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
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Dasar Perhitungan" >
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cbokategori" runat="server" CssClass="form-control" Width="120">
                                                    <asp:ListItem Value="1">Gaji Pokok</asp:ListItem>
                                                    <asp:ListItem Value="2">Gaji Pokok dan Tunjangan</asp:ListItem>
                                                    <asp:ListItem Value="3">Setting Persen Cabang</asp:ListItem>
                                                    <asp:ListItem Value="5">UMR</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Link Nilai Komponen Gaji">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <div class="text-center">
                                                    <asp:DropDownList ID="cbokomponengaji" CssClass="form-control" runat="server"  Width="120"></asp:DropDownList>
                                                </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="COA Debet">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <div class="text-center">
                                                    <asp:DropDownList ID="cbnorek" CssClass="form-control" runat="server"  Width="150"></asp:DropDownList>
                                                </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="COA Kredit">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <div class="text-center">
                                                    <asp:DropDownList ID="cbnorekkd" CssClass="form-control" runat="server"  Width="150"></asp:DropDownList>
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
