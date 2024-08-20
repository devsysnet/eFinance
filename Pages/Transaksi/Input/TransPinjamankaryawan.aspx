<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransPinjamankaryawan.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransPinjamankaryawan" %>

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
        function removeMoney(money) {
            var minus = money.substring(0, 1);
            if (minus == "-") {
                var number = '-' + Number(money.replace(/[^0-9\.]+/g, ""));
            } else {
                var number = Number(money.replace(/[^0-9\.]+/g, ""));
            }
            return number;
        }
       

    </script>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-12">
                                     <div class="form-group">
                                        <label class="col-sm-5 control-label">Sumber Peminjaman <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="cboTA" runat="server" CssClass="form-control" Width="150"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Tanggal Peminjaman <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="dtDate" runat="server" class="form-control date col-sm-5"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                     <div class="form-group">
                                        <label class="col-sm-5 control-label">Jenis Peminjaman <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList runat="server" CssClass="form-control" Width="250" ID="cboTransaction" AutoPostBack="true" OnSelectedIndexChanged="cboTransaction_SelectedIndexChanged">

                                               <asp:ListItem Value="0">--Pilih Jenis Permintaan--</asp:ListItem>
                                                <asp:ListItem Value="Tetap">Tetap</asp:ListItem>
                                                <asp:ListItem Value="Tidak Tetap">Tidak Tetap</asp:ListItem>

                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-5 control-label">Karyawan <span class="mandatory">*</span></label>
                                            <div class="col-sm-5">
                                                <asp:HiddenField runat="server" ID="hdnNoKaryawan" />
                                                <div class="input-group-btn">
                                                    <asp:TextBox ID="txtNamaPeminta" Enabled="false" runat="server" CssClass="form-control" />
                                                    <asp:ImageButton ID="btnBrowsePeminta" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image form-control" OnClick="btnBrowsePeminta_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Jumlah Pinjaman</label>
                                        <div class="col-sm-2">
                                            <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txtPinjaman"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="showangsuran" runat="server">
                                         <div class="form-group">
                                        <label class="col-sm-5 control-label">Angusuran Per Bulan</label>
                                        <div class="col-sm-2">
                                            <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="TextAngsuran"></asp:TextBox>
                                        </div>
                                    </div>
                                    </div>
                                   
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Keterangan</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtKeterangan" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="text-center">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Simpan" CausesValidation="false" OnClick="btnSubmit_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Batal" CausesValidation="false" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgAddData" runat="server" PopupControlID="panelAddDataPeminta" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelAddDataPeminta" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Karyawan</h4>
            </div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="modal-body col-overflow-400">
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group ">
                            <div class="form-inline">
                                <label class="col-sm-3 control-label">Filter</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtSearchMinta" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    <asp:Button ID="btnMinta" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnMinta_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="grdDataPeminta" DataKeyNames="nokaryawan" ShowFooter="true" SkinID="GridView" runat="server" OnSelectedIndexChanged="grdDataPeminta_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField ID="hdnNoUserD" runat="server" Value='<%# Bind("noKaryawan") %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="idPeg" HeaderText="ID Peg" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="nama" HeaderText="Nama Karyawan" ItemStyle-Width="25%" />
                            <asp:BoundField DataField="cabang" HeaderText="Cabang" ItemStyle-Width="25%" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="" ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-primary btn-sm" Text="Pilih" CommandName="Select" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>


