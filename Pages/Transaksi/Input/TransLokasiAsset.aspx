<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransLokasiAsset.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransLokasiAsset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdLokasiAsset.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="form-horizontal">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label">Lokasi Aset <span class="mandatory">*</span></label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="cboLokasi" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboLokasi_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label">Sub Lokasi Aset <span class="mandatory">*</span></label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="cboSubLokasi" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 overflow-x-table">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdLokasiAsset" DataKeyNames="noAset" SkinID="GridView" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <%# Container.DataItemIndex + 1 %>
                                                            <asp:HiddenField ID="hdnNoAsset" runat="server" Value='<%# Bind("noAset") %>' />
                                                            <asp:HiddenField ID="hdnTglAsset" runat="server" Value='<%# Bind("tglAsset") %>' />
                                                            <asp:HiddenField ID="hdnnoCabang" runat="server" Value='<%# Bind("noCabang") %>' />
                                                            <asp:HiddenField ID="hdnnoBarang" runat="server" Value='<%# Bind("noBarang") %>' />
                                                            <asp:HiddenField ID="hdnDanaBOS" runat="server" Value='<%# Bind("danaBOS") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                    <HeaderTemplate>
                                                        <div class="text-center">
                                                            <asp:CheckBox ID="chkCheckAll" runat="server" CssClass="px" onclick="return CheckAllData(this);" />
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:CheckBox ID="chkCheck" runat="server" CssClass="px chkCheck" />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="kodeTerima" HeaderText="Kode Penerimaan Aset" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="namaAsset" HeaderText="Nama Aset" HeaderStyle-CssClass="text-center" ItemStyle-Width="23%" />
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Aset Baru" ItemStyle-Width="18%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:TextBox ID="txtNamaAsset" runat="server" CssClass="form-control" Width="200" Text='<%# Bind("namaAsset") %>'></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              <%--  <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="COA (Debit)" ItemStyle-Width="12%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:DropDownList ID="cborekdb" runat="server" Width="200"></asp:DropDownList>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="COA (Kredit)" ItemStyle-Width="12%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:DropDownList ID="cborekkd" runat="server" Width="200"></asp:DropDownList>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="button" runat="server">
                                <div class="col-sm-12 ">
                                    <div class="text-center">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSubmit_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="tabForm" runat="server">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
