<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransRealisasiKasbon.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransRealisasiKasbon" %>

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
        function Calculate() {
            var debit = 0, kredit = 0, hasilDebit = 0, hasilKredit = 0, total = 0;
            var nilai = removeMoney(document.getElementById("<%=txtNilai.ClientID %>").value);
            var GridView = document.getElementById("<%=grdMemoJurnal.ClientID %>");

            for (var i = 1; i < GridView.rows.length; i++) {
                debit = removeMoney(GridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value) * 1;
                kredit = removeMoney(GridView.rows[i].cells[5].getElementsByTagName("INPUT")[0].value) * 1;

                if (debit != 0)
                    hasilDebit += debit;
                if (kredit != 0)
                    hasilKredit += kredit;
            }

            if (hasilDebit != 0)
                document.getElementById("BodyContent_txtDebitTotal").value = money(hasilDebit);

            total = (hasilKredit + nilai);
            document.getElementById("BodyContent_txtKreditTotal").value = money(total);
        }

    </script>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-8">
                                <div class="form-inline">
                                    <div class="col-sm-12">
                                        <%--<asp:TextBox ID="dtMulai" runat="server" CssClass="form-control date"></asp:TextBox>
                        <asp:TextBox ID="dtSampai" runat="server" CssClass="form-control date"></asp:TextBox>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-inline">
                                    <div class="col-sm-12">
                                        <asp:TextBox ID="txtCari" runat="server" CssClass="form-control" placeholder="Masukan kata"></asp:TextBox>
                                        <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnCari_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <asp:GridView ID="grdKas" DataKeyNames="nokasbondana" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdKas_PageIndexChanging" OnSelectedIndexChanged="grdKas_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="kdTran" HeaderText="Kode" ItemStyle-Width="10%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Tgl" HeaderText="Tanggal" ItemStyle-Width="8%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                                        <asp:BoundField DataField="nomorKode" HeaderText="Kode Kas & Bank" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="peminta" HeaderText="Peminta" ItemStyle-Width="20%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Input" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row">
                            <div class="panel">
                                <div class="panel-body">
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-6">
                                            <div class="form-horizontal">
                                                <div class="form-group row">
                                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Type<span class="mandatory">*</span> :</label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="cboType" runat="server">
                                                            <asp:ListItem Value="RealisasiKasbon">Realisasi Kasbon</asp:ListItem>
                                                            <asp:ListItem Value="Pengembalian">Pengembalian</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kode :</label>
                                                    <div class="col-sm-5">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtKode" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tgl<span class="mandatory">*</span> :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox runat="server" CssClass="form-control date" ID="dtDate"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kode Kas :</label>
                                                    <div class="col-sm-5">
                                                        <asp:HiddenField runat="server" ID="hdnnoKas" />
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtKodeKas" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Peminta :</label>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPeminta" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Cek = ganti harga :</label>
                                                    <div class="col-sm-4">
                                                        <asp:CheckBox ID="chkAktif" runat="server" AutoPostBack="true" OnCheckedChanged="chkAktif_CheckedChanged" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nilai :</label>
                                                    <div class="col-sm-4">
                                                        <asp:HiddenField runat="server" ID="hdnNilaiLama" />
                                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtNilai" Enabled="false" AutoPostBack="true" OnTextChanged="txtNilai_TextChanged"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row" runat="server" id="tampil2">
                                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Account Kas/Bank :</label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="cboAccount" Width="250"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Catatan :</label>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" ID="txtCatatan" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <asp:HiddenField runat="server" ID="hdnRowIndexMemoJurnal" />
                                        <asp:GridView ID="grdMemoJurnal" SkinID="GridView" runat="server" AllowPaging="true" OnSelectedIndexChanged="grdMemoJurnal_SelectedIndexChanged" OnRowCommand="grdMemoJurnal_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <%# Container.DataItemIndex + 1 %>.
                                                             <asp:HiddenField ID="txtHdnValue" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kode Akun" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <div class="form-group">
                                                                <div class="form-inline">
                                                                    <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtAccount"></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnAccount" runat="server" />
                                                                    <%--<asp:ImageButton ID="imgButtonProduct" CssClass="btn-image form-control" runat="server" ImageUrl="~/assets/images/icon_search.png" CommandName="Select" />--%>
                                                                    <asp:ImageButton ID="btnImgAccount" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image form-control" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                                    <asp:ImageButton ID="btnClear" runat="server" ImageUrl="~/assets/images/icon_trash.png" CssClass="btn-image form-control" CommandName="Clear" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Akun" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtDescription" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Keterangan" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:TextBox runat="server" class="form-control" ID="txtRemark" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Debit" ItemStyle-Width="12%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:TextBox runat="server" class="form-control money" Text="0.00" ID="txtDebit" onblur="return Calculate()"></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kredit" ItemStyle-Width="12%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" Enabled="false" ID="txtKredit" onblur="return Calculate()"></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 text-right">
                                            <div class="form-group row">
                                                <label for="pjs-ex1-fullname" class="col-sm-8 control-label text-right">Total :</label>
                                                <div class="col-sm-2">
                                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtDebitTotal" Text="0.00" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtKreditTotal" Text="0.00" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <asp:Button runat="server" ID="btnAdd" CssClass="btn btn-success" Text="Tambah Baris" OnClick="btnAdd_Click"></asp:Button>
                                                <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                                <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click"></asp:Button>
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


    <asp:LinkButton ID="LinkButton2" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="panelAddMenu" TargetControlID="LinkButton2" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelAddMenu" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content size-sedang">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Detil Akun</h4>
            </div>
            <div class="modal-body ">
                <div class="row">
                    <div class="col-sm-7">
                    </div>
                    <div class="col-sm-5">
                        <div class="form-group ">
                            <div class="form-inline">
                                <div class="col-sm-12">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Masukan kata"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnCariAkun_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:HiddenField ID="txtHdnPopup" runat="server" />
                    <asp:GridView ID="grdAkun" runat="server" SkinID="GridView" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdAkun_PageIndexChanging" OnSelectedIndexChanged="grdAkun_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <%# Container.DataItemIndex + 1 %>.
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Kode Akun">
                                <ItemTemplate>
                                    <asp:Label ID="lblKodeAkun" runat="server" Text='<%# Bind("kdRek") %>'></asp:Label>
                                    <asp:HiddenField ID="hidNoAkun" runat="server" Value='<%# Eval("noRek") %>'></asp:HiddenField>
                                 </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nama Akun">
                                <ItemTemplate>
                                    <asp:Label ID="lblNamaAkun" runat="server" Text='<%# Bind("ket") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ShowHeader="False">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:Button ID="btnSelect" runat="server" CommandName="Select" Text="Pilih" CssClass="btn btn-primary btn-sm" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnCloseMenu" runat="server" Text="Close" CssClass="btn btn-danger btn-sm" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
