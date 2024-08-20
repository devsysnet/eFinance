<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MJenisTrsUpdate.aspx.cs" Inherits="eFinance.Pages.Master.Update.MJenisTrsUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function money(money) {
            var num = money;
            if (num != "") {
                var string = String(num);
                var string = string.replace('.', ' ');

                var array2 = string.toString().split(' ');
                num = parseInt(num).toFixed(0);

                var array = num.toString().split('');

                var index = -3;
                while (array.length + index > 0) {
                    array.splice(index, 0, ',');
                    index -= 4;
                }
                if (array2.length == 1) {
                    money = array.join('') + '.00';
                } else {
                    money = array.join('') + '.' + array2[1];
                    if (array2.length == 1) {
                        money = array.join('') + '.00';
                    } else {
                        if (array2[1].length == 1) {
                            money = array.join('') + '.' + array2[1].substring(0, 2) + '0';
                        } else {
                            money = array.join('') + '.' + array2[1].substring(0, 2);
                        }
                    }
                }
            } else {
                money = '0.00';
            }
            return money;
        }
    </script>
    <asp:HiddenField ID="hdnId" runat="server" />

    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-8">
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Masukan kata"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdBarang" DataKeyNames="noTransaksi" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdBarang_PageIndexChanging" OnRowCommand="grdBarang_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="jenisTransaksi" HeaderText="Jenis Transaksi" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="kategori" HeaderText="Kategori" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="urutan" HeaderText="Urutan" HeaderStyle-CssClass="text-center" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Edit" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectDelete" runat="server" class="btn btn-xs btn-labeled btn-danger" Text="Delete" CommandName="SelectDelete" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Transaksi <span class="mandatory">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" class="form-control " ID="jenisTransaksi" placeholder="Jenis Transaksi" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group" runat="server" id="divDanaBOSH">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kategori Transaksi</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboposbln" runat="server">
                                                <asp:ListItem Value="">---Pilih Kategori Transaksi---</asp:ListItem>
                                                <asp:ListItem Value="1">SPP</asp:ListItem>
                                                <asp:ListItem Value="5">Bulanan Di Luar SPP</asp:ListItem>
                                                <asp:ListItem Value="2">Kegiatan</asp:ListItem>
                                                <asp:ListItem Value="3">Biaya Lain</asp:ListItem>
                                                <asp:ListItem Value="4">Uang Pangkal</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">COA DEBIT</label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="cbonorekDb" runat="server">                                                
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">COA KREDIT</label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="cbonorekKd" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" runat="server" id="divkodeVA">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kode Virtual Account <span class="mandatory">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" class="form-control " ID="kodeva" placeholder="Kode Virtual Account"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group" runat="server" id="divdenda">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Denda <span class="mandatory">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" class="form-control money" ID="txtdenda" placeholder="Denda"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group" runat="server" id="divurutan">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Urutan <span class="mandatory">*</span></label>
                                        <div class="col-sm-2">
                                            <asp:TextBox runat="server" CssClass="form-control int" ID="txtUrutan" placeholder="Urutan" MaxLength="2"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group" runat="server" id="divlevel">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Level Pelunasan <span class="mandatory">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cbopelunasan" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbopelunasan_SelectedIndexChanged">
                                                <asp:ListItem Value="0">-Pilih Level Pelunasan-</asp:ListItem>
                                                <asp:ListItem Value="2">Unit</asp:ListItem>
                                                <asp:ListItem Value="3">Perwakilan</asp:ListItem>
                                                <asp:ListItem Value="4">Yayasan</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" runat="server" id="divBank">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Bank <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cbobank" runat="server">
                                                <asp:ListItem Value="">---Pilih Bank---</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" runat="server" id="div2">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">No Index MHS <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" class="form-control" ID="nomhs" placeholder="Nomor Index MHS" Width="100"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group" runat="server" id="div1">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Kegiatan <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cbojnskegiatan" runat="server">
                                                <asp:ListItem Value="0">---Pilih Jenis Kegiatan---</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" runat="server" id="divopenclose">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Transaksi Bank<span class="mandatory">*</span></label>
                                    <div class="col-sm-4">
                                    <asp:DropDownList ID="closeopen" runat="server">
                                        <asp:ListItem Value="0">---Pilih Jenis Kegiatan---</asp:ListItem>
                                         <asp:ListItem Value="Close">Close</asp:ListItem>
                                         <asp:ListItem Value="Open">Open</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                                <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Batal" OnClick="btnCancel_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
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

