<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransCashBankUnit.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransCashBankUnit" %>
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
        function round(value, decimals) {
            return Number(Math.round(value + 'e' + decimals) + 'e-' + decimals);
        }
        function Calculate() {
            var debit = 0, kredit = 0, hasilDebit = 0, hasilKredit = 0;
            var gridView = document.getElementById("<%=grdKasBank.ClientID %>");
            var nilai = removeMoney(document.getElementById("<%=txtValue.ClientID %>").value);
            var transaksi = document.getElementById("<%=cboTransaction.ClientID %>").value;
            for (var i = 1; i < gridView.rows.length; i++) {
                debit = removeMoney(gridView.rows[i].cells[5].getElementsByTagName("INPUT")[0].value) * 1;
                kredit = removeMoney(gridView.rows[i].cells[6].getElementsByTagName("INPUT")[0].value) * 1;

                if (debit != 0)
                    hasilDebit += debit;
                if (kredit != 0)
                    hasilKredit += kredit;
            }

            if (transaksi == "1") {
                if (hasilDebit != 0)
                    document.getElementById("BodyContent_txtTotalDebit").value = money(round(hasilDebit + nilai, 2));
                else
                    document.getElementById("BodyContent_txtTotalDebit").value = money(round(nilai + hasilDebit, 2));

                if (hasilKredit != 0)
                    document.getElementById("BodyContent_txtTotalKredit").value = money(round(hasilKredit, 2));
                else
                    document.getElementById("BodyContent_txtTotalKredit").value = "0.00";

            }
            if (transaksi == "2") {
                if (hasilDebit != 0)
                    document.getElementById("BodyContent_txtTotalDebit").value = money(round(hasilDebit, 2));
                else
                    document.getElementById("BodyContent_txtTotalDebit").value = "0.00";

                if (hasilKredit != 0)
                    document.getElementById("BodyContent_txtTotalKredit").value = money(round(hasilKredit + nilai, 2));
                else
                    var fromSup = document.getElementById("<%=cboFromToSupp.ClientID %>").value;

                if (fromSup == "2" || fromSup == "3" || fromSup == "4" || fromSup == "5" || fromSup == "7")
                    document.getElementById("BodyContent_txtTotalKredit").value = money(round(nilai + hasilKredit, 2));
                else if (fromSup == "6") {
                    document.getElementById("BodyContent_txtTotalDebit").value = money(round(nilai + hasilDebit, 2));
                    document.getElementById("BodyContent_txtTotalKredit").value = money(round(nilai + hasilKredit, 2));
                }
                else
                    document.getElementById("BodyContent_txtTotalKredit").value = money(round(nilai + hasilKredit, 2));

            }

        }

    </script>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Jenis Transaksi <span class="mandatory">*</span></label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboTransaction_SelectedIndexChanged" CssClass="form-control" ID="cboTransaction">
                                            <asp:ListItem Value="0">---Pilih Jenis Transaksi---</asp:ListItem>
                                            <asp:ListItem Value="1">Penerimaan</asp:ListItem>
                                            <asp:ListItem Value="2">Pengeluaran</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Akun <span class="mandatory">*</span></label>
                                    <div class="col-sm-7">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="cboAccount"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Tanggal <span class="mandatory">*</span></label>
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" CssClass="form-control date" ID="dtKas"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Dari/Untuk <span class="mandatory">*</span></label>
                                    <div class="col-sm-5">
                                        <div runat="server" id="formFromToCus">
                                            <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboFromToCus_SelectedIndexChanged" CssClass="form-control" ID="cboFromToCus">
                                                <%--<asp:ListItem Value="0">---Pilih Dari/Untuk---</asp:ListItem>
                                                <asp:ListItem Value="1">Lain-lain</asp:ListItem>
                                                <asp:ListItem Value="9">Permintaan</asp:ListItem>
                                                <asp:ListItem Value="10">Dana BOS</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>
                                        <div runat="server" id="forFormToSupp" visible="false">
                                            <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboFromToSupp_SelectedIndexChanged" CssClass="form-control" ID="cboFromToSupp">
                                                <%--<asp:ListItem Value="0">---Pilih Dari/Untuk---</asp:ListItem>
                                                <asp:ListItem Value="1">Lain-lain</asp:ListItem>
                                                <asp:ListItem Value="11">Transfer</asp:ListItem>
                                                <asp:ListItem Value="2">Pembelian</asp:ListItem>
                                                <asp:ListItem Value="3">Permintaan</asp:ListItem>
                                                <asp:ListItem Value="4">Gaji Karyawan</asp:ListItem>
                                                <asp:ListItem Value="5">Iuran Bulanan</asp:ListItem>
                                                <asp:ListItem Value="6">Kas Bon</asp:ListItem>
                                                <asp:ListItem Value="7">THR / Bonus</asp:ListItem>
                                                <asp:ListItem Value="8">Absensi</asp:ListItem>
                                                <asp:ListItem Value="10">Dana BOS</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="formPO" visible="false">
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-4 control-label">Faktur <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:HiddenField runat="server" ID="hdnnoPO" />
                                                <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtPO"></asp:TextBox>
                                                <asp:ImageButton ID="btnPO" CssClass="btn-image form-control" runat="server" ImageUrl="~/assets/images/icon_search.png" OnClick="btnPO_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="formOther" visible="false">
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-4 control-label">Catatan Lain-lain <span class="mandatory">*</span></label>
                                            <div class="col-sm-7">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtOther"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="formPR" visible="false">
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-4 control-label">Kode PR <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:HiddenField runat="server" ID="hdnnoPR" />
                                                <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtPR"></asp:TextBox>
                                                <asp:ImageButton ID="btnPR" CssClass="btn-image form-control" runat="server" ImageUrl="~/assets/images/icon_search.png" OnClick="btnPR_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="formGaji" visible="false">
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-4 control-label">Kode Gaji <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:HiddenField runat="server" ID="hdnnoGaji" />
                                                <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtKodeGaji"></asp:TextBox>
                                                <asp:ImageButton ID="btnGaji" CssClass="btn-image form-control" runat="server" ImageUrl="~/assets/images/icon_search.png" OnClick="btnGaji_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="formIuran" visible="false">
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-4 control-label">Kode Iuran <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:HiddenField runat="server" ID="hdnnoIuran" />
                                                <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtKodeIuran"></asp:TextBox>
                                                <asp:ImageButton ID="btnIuran" CssClass="btn-image form-control" runat="server" ImageUrl="~/assets/images/icon_search.png" OnClick="btnIuran_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="formKasBon" visible="false">
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-4 control-label">Nama Karyawan <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:HiddenField runat="server" ID="hdnnoKasBon" />
                                                <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtKodeKasbon"></asp:TextBox>
                                                <asp:ImageButton ID="btnKasBon" CssClass="btn-image form-control" runat="server" ImageUrl="~/assets/images/icon_search.png" OnClick="btnKasBon_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                 <div runat="server" id="Formcabanggaji" visible="false">
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-4 control-label">Cabang <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:HiddenField runat="server" ID="hdnnocabanggaji" />
                                                <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtcabanggaji"></asp:TextBox>
                                                <asp:ImageButton ID="btncabanggaji" CssClass="btn-image form-control" runat="server" ImageUrl="~/assets/images/icon_search.png" OnClick="btncabanggaji_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="formTHR" visible="false">
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-4 control-label">Kode THR <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:HiddenField runat="server" ID="hdnnoTHR" />
                                                <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtKodeTHR"></asp:TextBox>
                                                <asp:ImageButton ID="btnTHR" CssClass="btn-image form-control" runat="server" ImageUrl="~/assets/images/icon_search.png" OnClick="btnTHR_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="formAbsen" visible="false">
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-4 control-label">Kode Absensi <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:HiddenField runat="server" ID="hdnnoAbsensi" />
                                                <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtKodeAbsensi"></asp:TextBox>
                                                <asp:ImageButton ID="btnAbsen" CssClass="btn-image form-control" runat="server" ImageUrl="~/assets/images/icon_search.png" OnClick="btnAbsen_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="formPRKas" visible="false">
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-4 control-label">Kode PR Kas <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:HiddenField runat="server" ID="hdnnoPRKas" />
                                                <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtKodePRKas"></asp:TextBox>
                                                <asp:ImageButton ID="btnPRKas" CssClass="btn-image form-control" runat="server" ImageUrl="~/assets/images/icon_search.png" OnClick="btnPRKas_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="formDanaBOS" visible="false">
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-4 control-label">Catatan Dana BOS <span class="mandatory">*</span></label>
                                            <div class="col-sm-7">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtDanaBOS"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="formTransfer" visible="false">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Unit <span class="mandatory">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:DropDownList runat="server" ID="cboUnitTransfer" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="formPengembalian" visible="false">
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-4 control-label">Jenis Piutang <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:HiddenField runat="server" ID="hdnnoPengembalian" />
                                                <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtKodePengembalian"></asp:TextBox>
                                                <asp:ImageButton ID="btnPengembalian" CssClass="btn-image form-control" runat="server" ImageUrl="~/assets/images/icon_search.png" OnClick="btnPengembalian_Click" />
                                                <%--<asp:DropDownList runat="server" ID="cboCabang" CssClass="form-control"></asp:DropDownList>--%>
                                            </div>
                                        </div>
                                        <div class="form-inline">
                                            <%--<label class="col-sm-4 control-label">Jenis Piutang <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:HiddenField runat="server" ID="HiddenField11" />
                                                <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="TextBox1"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton1" CssClass="btn-image form-control" runat="server" ImageUrl="~/assets/images/icon_search.png" OnClick="btnPengembalian_Click" />
                                                <%--<asp:DropDownList runat="server" ID="cboCabang" CssClass="form-control"></asp:DropDownList>--%>
                                        </div>
                                    </div>
                                    <div class="form-inline">
                                        <label class="col-sm-4 control-label">Unit <span class="mandatory">*</span></label>
                                        <div class="col-sm-6">
                                            <%--<asp:HiddenField runat="server" ID="HiddenField11" />
                                                <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="TextBox1"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton1" CssClass="btn-image form-control" runat="server" ImageUrl="~/assets/images/icon_search.png" OnClick="btnPengembalian_Click" />--%>
                                            <asp:DropDownList runat="server" ID="cboCabang" CssClass="form-control" Width="200"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="formBPJS" visible="false">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Unit <span class="mandatory">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:DropDownList runat="server" ID="cboUnitBPJS" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="formTampKeluar" visible="false">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Tamp Keluar <span class="mandatory">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:DropDownList runat="server" ID="cboTampKeluar" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboTampKeluar_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="formKegiatan" visible="false">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Kegiatan <span class="mandatory">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:DropDownList runat="server" ID="cboKegiatan" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Mata Uang </label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="cboCurrency" class="form-control" runat="server" OnSelectedIndexChanged="cboCurrency_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group" runat="server" id="showhidekurs">
                                <label class="col-sm-3 control-label">
                                    <asp:Label ID="Kurstrs" runat="server" CssClass="form-label" Text="kurs"></asp:Label></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" class="form-control" ID="txtKurs"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Nilai <span class="mandatory">*</span></label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtValue" Text="0.00" onKeyUp="Calculate()" onKeyDown="Calculate()"></asp:TextBox>
                                </div>
                            </div>
                            <div runat="server" id="formAngsuran" visible="false">
                                <div class="form-group">
                                    <div class="form-inline">
                                        <label class="col-sm-3 control-label">Angsuran <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txtAngsuran"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Uraian <span class="mandatory">*</span></label>
                                <div class="col-sm-7">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtRemark" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="nonPO" runat="server">
                        <div class="col-sm-12 overflow-x-table">
                            <asp:GridView ID="grdKasBank" SkinID="GridView" runat="server" OnRowCommand="grdKasBank_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <%# Container.DataItemIndex + 1 %>
                                                <asp:HiddenField ID="hdnParameter" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Akun" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" ID="txtAccount"  CssClass="form-control" Width="120" OnTextChanged="txtAccount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:HiddenField ID="hdnAccount" runat="server" />
                                                        <asp:ImageButton ID="btnImgAccount" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image form-control" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                        <asp:ImageButton ID="btnClear" runat="server" ImageUrl="~/assets/images/icon_trash.png" CssClass="btn-image form-control" CommandName="Clear" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Akun" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <div class="text-left">
                                                <asp:Label runat="server" ID="lblDescription"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Cabang" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <div class="text-left">
                                           <asp:DropDownList ID="cbocabangunit" class="form-control" runat="server"></asp:DropDownList>

                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Uraian" ItemStyle-Width="12%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtRemarkDetil"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Debet" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" CssClass="form-control money" ID="txtDebit" AutoPostBack="true" OnTextChanged="txtDebit_TextChanged" onKeyUp="Calculate()" onKeyDown="Calculate()"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kredit" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" CssClass="form-control money" ID="txtKredit" AutoPostBack="true" OnTextChanged="txtKredit_TextChanged" onKeyUp="Calculate()" onKeyDown="Calculate()"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="col-sm-5">
                            </div>
                            <div class="col-sm-7">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label text-right">Total Debet </label>
                                    <div class="col-sm-4 text-right">
                                        <asp:TextBox ID="txtTotalDebit" runat="server" Enabled="false" CssClass="form-control money" Text="0.00" onKeyUp="return Calculate(event);" onKeyDown="return Calculate(event);"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-2 control-label text-right">Total Kredit </label>
                                    <div class="col-sm-4 text-right">
                                        <asp:TextBox ID="txtTotalKredit" runat="server" Enabled="false" CssClass="form-control money" Text="0.00" onKeyUp="return Calculate(event);" onKeyDown="return Calculate(event);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <asp:Button runat="server" ID="btnAddRow" CssClass="btn btn-success" Text="Tambah Baris" OnClick="btnAddRow_Click"></asp:Button>
                                <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgBank" runat="server" PopupControlID="panelBank" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelBank" runat="server" align="center" Width="60%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Account</h4>
            </div>
            <div class="row">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-5">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" MaxLength="50" placeholder="Masukan kata"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body col-overflow-500">
                <asp:Label runat="server" ID="lblMessageError"></asp:Label>
                <div class="table-responsive">
                    <asp:HiddenField runat="server" ID="hdnParameterProd" />
                    <asp:GridView ID="grdBank" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdBank_PageIndexChanging" OnSelectedIndexChanged="grdBank_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Akun" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblKdRek" Text='<%#Eval("kdRek")%>'></asp:Label>
                                        <asp:HiddenField ID="hdnNoRek" runat="server" Value='<%#Eval("noRek")%>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Akun" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblKet" Text='<%#Eval("Ket")%>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelectSub" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnBatal" runat="server" Text="Tutup" CssClass="btn btn-danger" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>


    <asp:LinkButton ID="LinkButton2" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgPO" runat="server" PopupControlID="panelSales" TargetControlID="LinkButton2" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelSales" runat="server" align="center" Width="60%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data PO</h4>
                <asp:HiddenField runat="server" ID="HiddenField1" />
            </div>
            <div class="row">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-5">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtSearchPO" runat="server" CssClass="form-control" MaxLength="50" placeholder="Masukan kata"></asp:TextBox>
                            <asp:Button ID="btnSearchPO" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearchPO_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body col-overflow-500">
                <div class="table-responsive">
                    <asp:GridView ID="grdPO" DataKeyNames="noPO" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPO_PageIndexChanging" OnSelectedIndexChanged="grdPO_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="kodePO" HeaderStyle-CssClass="text-center" HeaderText="Kode Faktur" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="tglPO" HeaderStyle-CssClass="text-center" HeaderText="Tgl Faktur" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="nama" HeaderStyle-CssClass="text-center" HeaderText="Supplier" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="sisa" HeaderStyle-CssClass="text-center" HeaderText="Sisa Hutang" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelectSub" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="Button2" runat="server" Text="Tutup" CssClass="btn btn-danger" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton3" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgReimbersment" runat="server" PopupControlID="panelReimbersment" TargetControlID="LinkButton3" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelReimbersment" runat="server" align="center" Width="60%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Reimbersment</h4>
                <asp:HiddenField runat="server" ID="HiddenField2" />
            </div>
            <div class="row">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-5">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtKodeReimbersment" runat="server" CssClass="form-control" MaxLength="50" placeholder="Masukan kata"></asp:TextBox>
                            <asp:Button ID="btnCariReimbersment" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnCariReimbersment_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body col-overflow-500">
                <div class="table-responsive">
                    <asp:GridView ID="grdReimbersment" DataKeyNames="noReimbusmen" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdReimbersment_PageIndexChanging" OnSelectedIndexChanged="grdReimbersment_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="kodeReimbusment" HeaderStyle-CssClass="text-center" SortExpression="kodeReimbusment" HeaderText="Kode" ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="tglReimburment" HeaderStyle-CssClass="text-center" SortExpression="tglReimburment" HeaderText="Tgl" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="nilai" HeaderStyle-CssClass="text-center" SortExpression="nilai" HeaderText="Nilai" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="keterangan" HeaderStyle-CssClass="text-center" SortExpression="keterangan" HeaderText="Keterangan" ItemStyle-Width="20%" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelectSub" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="Button3" runat="server" Text="Tutup" CssClass="btn btn-danger" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton4" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgPR" runat="server" PopupControlID="panelPR" TargetControlID="LinkButton4" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelPR" runat="server" align="center" Width="60%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data PR</h4>
                <asp:HiddenField runat="server" ID="HiddenField3" />
            </div>
            <div class="row">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-5">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtSearchPR" runat="server" CssClass="form-control" MaxLength="50" placeholder="Masukan kata"></asp:TextBox>
                            <asp:Button ID="btnSearchPR" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearchPR_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body col-overflow-500">
                <div class="table-responsive">
                    <asp:GridView ID="grdPR" DataKeyNames="noPR" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPR_PageIndexChanging" OnSelectedIndexChanged="grdPR_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="kodePR" HeaderText="Kode PR" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="tglPR" HeaderText="Tgl PR" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="peminta" HeaderText="Peminta" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" />
                            <asp:BoundField DataField="uraian" HeaderText="Uraian" HeaderStyle-CssClass="text-center" ItemStyle-Width="30%" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelectSub" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="Button4" runat="server" Text="Tutup" CssClass="btn btn-danger" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton5" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgGaji" runat="server" PopupControlID="panelGaji" TargetControlID="LinkButton5" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelGaji" runat="server" align="center" Width="80%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Gaji</h4>
                <asp:HiddenField runat="server" ID="HiddenField4" />
            </div>
            <div class="row">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-5">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtSearchGaji" runat="server" CssClass="form-control" MaxLength="50" placeholder="Masukan kata"></asp:TextBox>
                            <asp:Button ID="btnSearchGaji" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearchGaji_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body col-overflow-500">
                <div class="table-responsive">
                    <asp:GridView ID="grdGaji" DataKeyNames="noGaji" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdGaji_PageIndexChanging" OnSelectedIndexChanged="grdGaji_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="kode" HeaderText="Kode" HeaderStyle-CssClass="text-center" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="tgl" HeaderText="Tgl" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="total" HeaderText="Nilai" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <asp:BoundField DataField="unit" HeaderText="Unit" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelectSub" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="Button5" runat="server" Text="Tutup" CssClass="btn btn-danger" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton6" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgIuran" runat="server" PopupControlID="panelIuran" TargetControlID="LinkButton6" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelIuran" runat="server" align="center" Width="80%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Iuran</h4>
                <asp:HiddenField runat="server" ID="HiddenField5" />
            </div>
            <div class="row">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-5">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtSearchIuran" runat="server" CssClass="form-control" MaxLength="50" placeholder="Masukan kata"></asp:TextBox>
                            <asp:Button ID="btnSearchIuran" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearchIuran_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body col-overflow-500">
                <div class="table-responsive">
                    <asp:GridView ID="grdIuran" DataKeyNames="noIuran" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdIuran_PageIndexChanging" OnSelectedIndexChanged="grdIuran_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="kode" HeaderText="Kode" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="jnsIuran" HeaderText="Iuran" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="tgl" HeaderText="Tgl" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="total" HeaderText="Nilai" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <asp:BoundField DataField="unit" HeaderText="Unit" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelectSub" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="Button6" runat="server" Text="Tutup" CssClass="btn btn-danger" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton7" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgKasBon" runat="server" PopupControlID="panelKasBon" TargetControlID="LinkButton7" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelKasBon" runat="server" align="center" Width="60%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Kasbon</h4>
                <asp:HiddenField runat="server" ID="HiddenField6" />
            </div>
            <div class="row">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-5">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtSearchKasBon" runat="server" CssClass="form-control" MaxLength="50" placeholder="Masukan kata"></asp:TextBox>
                            <asp:Button ID="btnSearchKasBon" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearchKasBon_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body col-overflow-500">
                <div class="table-responsive">
                    <asp:GridView ID="grdKasBon" DataKeyNames="noKaryawan" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdKasBon_PageIndexChanging" OnSelectedIndexChanged="grdKasBon_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="kode" HeaderText="Kode" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="nama" HeaderText="Nama" HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelectSub" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="Button7" runat="server" Text="Tutup" CssClass="btn btn-danger" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>


    <asp:LinkButton ID="LinkButton71" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgcabanggaji" runat="server" PopupControlID="panelcabanggaji" TargetControlID="LinkButton71" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelcabanggaji" runat="server" align="center" Width="60%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Cabang</h4>
                <asp:HiddenField runat="server" ID="HiddenField11" />
            </div>
            <div class="row">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-5">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtSearchcabanggaji" runat="server" CssClass="form-control" MaxLength="50" placeholder="Masukan kata"></asp:TextBox>
                            <asp:Button ID="btnSearchcabanggaji" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearchcabanggaji_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal-body col-overflow-500">
                <div class="table-responsive">
                    <asp:GridView ID="grdcabanggaji" DataKeyNames="nocabang" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdcabanggaji_PageIndexChanging" OnSelectedIndexChanged="grdcabanggaji_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField="namacabang" HeaderText="Nama Cabang" HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelectSub" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <div class="modal-footer">
                <asp:Button ID="Button71" runat="server" Text="Tutup" CssClass="btn btn-danger" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>


    <asp:LinkButton ID="LinkButton8" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgTHR" runat="server" PopupControlID="panelTHR" TargetControlID="LinkButton8" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelTHR" runat="server" align="center" Width="80%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data THR / Bonus</h4>
                <asp:HiddenField runat="server" ID="HiddenField7" />
            </div>
            <div class="row">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-5">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtSearchTHR" runat="server" CssClass="form-control" MaxLength="50" placeholder="Masukan kata"></asp:TextBox>
                            <asp:Button ID="btnSearchTHR" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearchTHR_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body col-overflow-500">
                <div class="table-responsive">
                    <asp:GridView ID="grdTHR" DataKeyNames="noTHR" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdTHR_PageIndexChanging" OnSelectedIndexChanged="grdTHR_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="kodethr" HeaderText="Kode" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="tgl" HeaderText="Tgl" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="nilai" HeaderText="Nilai" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <asp:BoundField DataField="unit" HeaderText="Unit" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelectSub" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="Button8" runat="server" Text="Tutup" CssClass="btn btn-danger" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton9" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgAbsensi" runat="server" PopupControlID="panelAbsensi" TargetControlID="LinkButton9" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelAbsensi" runat="server" align="center" Width="60%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Absensi</h4>
                <asp:HiddenField runat="server" ID="HiddenField8" />
            </div>
            <div class="row">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-5">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtSearchAbsensi" runat="server" CssClass="form-control" MaxLength="50" placeholder="Masukan kata"></asp:TextBox>
                            <asp:Button ID="btnSearchAbsensi" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearchAbsensi_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body col-overflow-500">
                <div class="table-responsive">
                    <asp:GridView ID="grdAbsensi" DataKeyNames="noTotAbsen" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdAbsensi_PageIndexChanging" OnSelectedIndexChanged="grdAbsensi_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="kodeTotAbsen" HeaderText="Kode" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="tgl" HeaderText="Tgl" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="nilai" HeaderText="Nilai" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelectSub" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="Button9" runat="server" Text="Tutup" CssClass="btn btn-danger" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton10" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgPRKas" runat="server" PopupControlID="panelPRKas" TargetControlID="LinkButton10" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelPRKas" runat="server" align="center" Width="60%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data PR Kas</h4>
                <asp:HiddenField runat="server" ID="HiddenField9" />
            </div>
            <div class="row">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-5">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtSearchPRKas" runat="server" CssClass="form-control" MaxLength="50" placeholder="Masukan kata"></asp:TextBox>
                            <asp:Button ID="btnSearchPRKas" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearchPRKas_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body col-overflow-500">
                <div class="table-responsive">
                    <asp:GridView ID="grdPRKas" DataKeyNames="noKas" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPRKas_PageIndexChanging" OnSelectedIndexChanged="grdPRKas_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="nomorKode" HeaderText="Kode" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="tgl" HeaderText="Tgl" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="nilai" HeaderText="Nilai" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelectSub" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="Button10" runat="server" Text="Tutup" CssClass="btn btn-danger" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton11" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgPengembalian" runat="server" PopupControlID="panelPengembalian" TargetControlID="LinkButton11" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelPengembalian" runat="server" align="center" Width="50%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Pengembalian</h4>
                <asp:HiddenField runat="server" ID="HiddenField10" />
            </div>
            <div class="row">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-5">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtSearchPengembelian" runat="server" CssClass="form-control" MaxLength="50" placeholder="Masukan kata"></asp:TextBox>
                            <asp:Button ID="btnSearchPengembelian" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearchPengembelian_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body col-overflow-500">
                <div class="table-responsive">
                    <asp:GridView ID="grdPengembalian" DataKeyNames="nopo" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPengembalian_PageIndexChanging" OnSelectedIndexChanged="grdPengembalian_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="Hutang" HeaderStyle-CssClass="text-center" HeaderText="Piutang" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />--%>
                            <%--<asp:BoundField DataField="norek" HeaderStyle-CssClass="text-center" HeaderText="Norek" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center"/>--%>
                            <asp:BoundField DataField="kodePO" HeaderStyle-CssClass="text-center" HeaderText="No PO" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField DataField="nama" HeaderStyle-CssClass="text-center" HeaderText="Supplier" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="sisa" HeaderStyle-CssClass="text-center" HeaderText="Sisa Hutang" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelectSub" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="Button11" runat="server" Text="Tutup" CssClass="btn btn-danger" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
