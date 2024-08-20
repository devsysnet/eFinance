<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstJabatan.aspx.cs" Inherits="eFinance.Pages.Master.Input.MstJabatan" %>
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
                                <asp:GridView ID="grdJabatan" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Jabatan" HeaderStyle-Width="60%"> 
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtJabatan" runat="server" CssClass="form-control" placeholder="Jabatan"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Jam Masuk" HeaderStyle-Width="10%"> 
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtjammasuk" runat="server" CssClass="form-control time"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Jam Masuk" HeaderStyle-Width="10%"> 
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtjamkeluar" runat="server" CssClass="form-control time"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Ada Potongan Keterlambatan" HeaderStyle-Width="20%">
                                            <ItemTemplate>
                                                 <asp:DropDownList ID="cboStatus" runat="server" CssClass="form-control">
                                                  <asp:ListItem Value="0">Tidak Dipotong</asp:ListItem>
                                                  <asp:ListItem Value="1">Dipotong</asp:ListItem>
                                                  </asp:DropDownList>
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
                                <asp:Button ID="btnAddRow" runat="server" CssClass="btn btn-success" Text="Tambah Baris" OnClick="btnAddRow_Click" />
                                <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click" />
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

