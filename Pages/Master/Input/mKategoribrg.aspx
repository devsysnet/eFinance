<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="mKategoribrg.aspx.cs" Inherits="eFinance.Pages.Master.Input.mKategoribrg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kategori</label>
                                <div class="col-sm-5">
                                    <asp:DropDownList ID="cbojnsBarang" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbojnsBarang_SelectedIndexChanged">
                                        <asp:ListItem Value="0">---Pilih Kategori---</asp:ListItem>
                                        <asp:ListItem Value="1">Asset Tidak Bergerak</asp:ListItem>
                                        <asp:ListItem Value="2">Asset Bergerak</asp:ListItem>
                                        <asp:ListItem Value="3">Jasa</asp:ListItem>
                                        <asp:ListItem Value="4">Inventaris</asp:ListItem>
                                        <asp:ListItem Value="5">Sales</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                        </div>
                    </div>
                    <div class="row">
                       <%-- <div class="col-md-2">
                        </div>--%>
                        <div class="col-md-8">
                            <div class="table-responsive">
                                   <%-- yang kategorinya asset --%>
                                <div runat="server" id="kategoriBrg1" visible="false">
                                    <asp:GridView ID="grdKategoriBrg" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Sub Kategori" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtInstansi" runat="server" CssClass="form-control" placeholder="SubKategori" Width="300"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="COA ASSET/BIAYA" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cbnorek" CssClass="form-control" runat="server" Width="150"></asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="COA PENYUSUTAN" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cborekkd" CssClass="form-control" runat="server" Width="150"></asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="COA BIAYA PENYUSUTAN" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cborekdb" CssClass="form-control" runat="server" Width="150"></asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                </div>

                                 <%-- yang kategorinya sales --%>
                                <div runat="server" id="kategoriBrg2" visible="false">
                                    <asp:GridView ID="grdKategoriBrg2" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Sub Kategori" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtInstansi2" runat="server" CssClass="form-control" placeholder="SubKategori" Width="300"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="COA PENDAPATAN" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cboPendapatan" CssClass="form-control" runat="server" Width="150"></asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="COA PIUTANG" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cboPiutang" CssClass="form-control" runat="server" Width="150"></asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         
                                    </Columns>
                                </asp:GridView>
                                </div>

                                 <%-- yang kategorinya bukan asset atau sales --%>
                                <div runat="server" id="kategoriBrg3" visible="false">
                                    <asp:GridView ID="grdKategoriBrg3" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Sub Kategori" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtInstansi3" runat="server" CssClass="form-control" placeholder="SubKategori" Width="300"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="COA Biaya" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cboBiaya" CssClass="form-control" runat="server" Width="150"></asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         
                                         
                                    </Columns>
                                </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <div class="text-center">
                    <asp:Button runat="server" ID="btnAdd" CssClass="btn btn-success" Text="Tambah Baris" OnClick="btnAdd_Click"></asp:Button>
                    <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
