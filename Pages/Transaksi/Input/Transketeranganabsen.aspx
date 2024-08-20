<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Transketeranganabsen.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.Transketeranganabsen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
                
    </script>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <div class="form-inline">
                                    <label class="col-sm-2 control-label">Tanggal</label>
                                    <div class="col-sm-9">
                                         <asp:TextBox runat="server" class="form-control date" ID="dtAbsen"></asp:TextBox>
                                          <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <asp:GridView ID="grdKetAbsen" DataKeyNames="nokaryawan" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnNoKaryawan" runat="server" Value='<%# Bind("nokaryawan") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="idpeg" SortExpression="nik" HeaderText="ID.Pegawai" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" HeaderText="Nama Karyawan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("nama") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" HeaderText="Alasan Ketidakhadiran">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                     <asp:DropDownList ID="cboIjin" class="form-control" runat="server">
                                                            <asp:ListItem Value="Ijin" Selected="True">Ijin</asp:ListItem>
                                                            <asp:ListItem Value="Dinas">Dinas</asp:ListItem>
                                                            <asp:ListItem Value="Sakit">Sakit</asp:ListItem>
                                                            <asp:ListItem Value="Alpha">Tanpa Ijin</asp:ListItem>
                                                       </asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                          </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" HeaderText="Keterangan">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtKeterangan" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
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
                                <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
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
