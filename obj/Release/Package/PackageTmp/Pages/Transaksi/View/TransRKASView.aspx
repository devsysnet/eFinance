<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransRKASView.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TransRKASView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <style type="text/css">
        .ChildGrid {
            background: #fff;
            margin-left: 4%;
            width: 96%;
        }

            .ChildGrid td {
                font-size: 11px;
            }

                .ChildGrid td .form-control {
                    padding: 1px 3px;
                    height: 20px;
                    font-size: 11px;
                }

            .ChildGrid th {
                color: #fff;
                font-size: 12px;
                background: #aaaaaa !important;
                border: #aaaaaa;
            }
    </style>
    <script type="text/javascript">
        function expandCollapse(name) {
            var div = document.getElementById(name);
            var img = document.getElementById('img' + name);
            if (div.style.display == 'block') {
                div.style.display = 'none';
                img.src = '<%=Func.BaseUrl%>assets/images/plus.png';
            } else {
                div.style.display = 'block';
                img.src = '<%=Func.BaseUrl%>assets/images/minus.png';
            }
        }
    </script>
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
            var gridView = document.getElementById("<%=grdTahap.ClientID %>");
            var nilai = removeMoney(document.getElementById("<%=txtJmlPagu.ClientID %>").value);

            for (var j = 1; j < gridView.rows.length; j++) {
                persen = removeMoney(gridView.rows[j].cells[1].getElementsByTagName("INPUT")[0].value) * 1;

                gridView.rows[j].cells[2].getElementsByTagName("INPUT")[0].value = money(round(nilai * persen / 100, 2));
            }
        }
        //fungsi Funcsum tidak di pake karena tidak jalan di tiap baris (pakai backend)
        function Funcsum() {
            var gridViewX = document.getElementById("<%=grdRekA.ClientID %>");
            var tahap = removeMoney(document.getElementById("<%=hdnJmlTahap.ClientID %>").value);
            var subtotal = 0;

               for (var i = 1; i < gridViewX.rows.length; i++) {
                    volume = removeMoney(gridViewX.rows[i].cells[3].getElementsByTagName("INPUT")[0].value) * 1;
                    harga = removeMoney(gridViewX.rows[i].cells[4].getElementsByTagName("INPUT")[0].value) * 1;
                    
                    if (volume != "" && harga != "") {
                        gridViewX.rows[i].cells[5].getElementsByTagName("INPUT")[0].value = money(round(volume * harga, 2));
                        subtotal = removeMoney(gridViewX.rows[i].cells[5].getElementsByTagName("INPUT")[0].value) * 1;

                        if (tahap == 1) {
                            gridViewX.rows[i].cells[6].getElementsByTagName("INPUT")[0].value = money(round(subtotal / tahap, 2));
                        }

                        if (tahap == 2) {
                            gridViewX.rows[i].cells[6].getElementsByTagName("INPUT")[0].value = money(round(subtotal / tahap, 2));
                            gridViewX.rows[i].cells[7].getElementsByTagName("INPUT")[0].value = money(round(subtotal / tahap, 2));
                        }

                        if (tahap == 3) {
                            gridViewX.rows[i].cells[6].getElementsByTagName("INPUT")[0].value = money(round(subtotal / tahap, 2));
                            gridViewX.rows[i].cells[7].getElementsByTagName("INPUT")[0].value = money(round(subtotal / tahap, 2));
                            gridViewX.rows[i].cells[8].getElementsByTagName("INPUT")[0].value = money(round(subtotal / tahap, 2));
                        }

                        if (tahap == 4) {
                            gridViewX.rows[i].cells[6].getElementsByTagName("INPUT")[0].value = money(round(subtotal / tahap, 2));
                            gridViewX.rows[i].cells[7].getElementsByTagName("INPUT")[0].value = money(round(subtotal / tahap, 2));
                            gridViewX.rows[i].cells[8].getElementsByTagName("INPUT")[0].value = money(round(subtotal / tahap, 2));
                            gridViewX.rows[i].cells[9].getElementsByTagName("INPUT")[0].value = money(round(subtotal / tahap, 2));
                        }
                    }
                }
            }
    </script>
    <asp:HiddenField runat="server" ID="hdnRowDataIndex" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-left">Cabang <span class="mandatory">*</span></label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="cboCabang" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-left">Tahun <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboThn" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboThn_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-left">Jumlah Siswa <span class="mandatory">*</span></label>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtJmlSiswa" runat="server" CssClass="form-control numeric" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-left">Jumlah Pagu <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtJmlPagu" runat="server" CssClass="form-control money" onBlur="Calculate();" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-left">Jumlah Tahap <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList runat="server" ID="cboJmlTahap" CssClass="form-control" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="cboJmlTahap_SelectedIndexChanged">
                                                <asp:ListItem Value="">--Pilih Tahap--</asp:ListItem>
                                                <asp:ListItem Value="1">1 Tahap</asp:ListItem>
                                                <asp:ListItem Value="2">2 Tahap</asp:ListItem>
                                                <asp:ListItem Value="3">3 Tahap</asp:ListItem>
                                                <asp:ListItem Value="4">4 Tahap</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <asp:GridView ID="grdTahap" SkinID="GridView" runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tahap" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <div class="text-left">
                                                        <asp:Label runat="server" ID="lblTahap"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Persen (%)" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtPersen" onBlur="Calculate();"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Jumlah" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtJumlah" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-12">
                                <div class="text-center">
                                    <asp:Button runat="server" ID="btnProses" CssClass="btn btn-success" Text="Proses" OnClick="btnProses_Click"></asp:Button>
                                    <asp:Button runat="server" ID="btnLoad" CssClass="btn btn-warning" Text="Refresh" OnClick="btnLoad_Click"></asp:Button>
                                    <asp:Button runat="server" ID="btnTambah" CssClass="btn btn-primary" Text="Tambah Barang" OnClick="btnTambah_Click"></asp:Button>
                                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" SkinID="GridView" DataKeyNames="norek,noTRKAS" runat="server" OnRowDataBound="GridView1_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <a href="JavaScript:expandCollapse('<%# Eval("norek") %>');">
                                                        <img src="<%=Func.BaseUrl%>assets/images/minus.png" id="img<%# Eval("norek") %>" />
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:HiddenField ID="txtHdnValue" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                                        <asp:HiddenField ID="hdnNoRekGrid" runat="server" Value='<%# Bind("norek") %>' />
                                                        <asp:HiddenField ID="hdnNoTRKASGrid" runat="server" Value='<%# Bind("noTRKAS") %>' />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Akun" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <div class="text-left">
                                                        <asp:Label runat="server" ID="lblAkun" Text='<%# Eval("kdRek") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Keterangan" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                    <div class="text-left">
                                                        <asp:Label runat="server" ID="lblKeterangan" Text='<%# Eval("Ket") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Volume" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Harga Satuan" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Jumlah" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtJumlah" Enabled="false" Text='<%# Eval("jumlahA","{0:n2}") %>'></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tahap 1" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtTahap1" Text='<%# Eval("tahap1","{0:n2}") %>' Enabled="false"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tahap 2" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtTahap2" Text='<%# Eval("tahap2","{0:n2}") %>' Enabled="false"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tahap 3" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtTahap3" Text='<%# Eval("tahap3","{0:n2}") %>' Enabled="false"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tahap 4" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtTahap4" Text='<%# Eval("tahap4","{0:n2}") %>' Enabled="false"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td colspan="100">
                                                            <div id="<%# Eval("norek") %>" style="display: block;">
                                                                <asp:GridView ID="GridView2" runat="server" CssClass="ChildGrid" AutoGenerateColumns="false">
                                                                    <%--ShowFooter="true"--%>
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kode Brg/Akun" ItemStyle-Width="8%">
                                                                            <ItemTemplate>
                                                                                <div class="text-left">
                                                                                    <asp:Label runat="server" ID="lblKodeBrgX" Text='<%# Eval("kodeBarang") %>'></asp:Label>
                                                                                    <asp:HiddenField ID="hdnNoRek" runat="server"  />
                                                                                    <asp:HiddenField ID="hdnNoTRKAS" runat="server" />
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Brg/Akun" ItemStyle-Width="25%">
                                                                            <ItemTemplate>
                                                                                <div class="text-left">
                                                                                    <asp:Label runat="server" ID="lblKeteranganX" Text='<%# Eval("namaBarang") %>'></asp:Label>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Volume" ItemStyle-Width="5%">
                                                                            <ItemTemplate>
                                                                                <div class="text-center">
                                                                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtVolumeX" Enabled="false" Text='<%# Eval("volume") %>'></asp:TextBox>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--<asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan" ItemStyle-Width="5%">
                                                                            <ItemTemplate>
                                                                                <div class="text-center">
                                                                                    <asp:Label runat="server" ID="lblSatuanX"></asp:Label>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Harga Satuan" ItemStyle-Width="8%">
                                                                            <ItemTemplate>
                                                                                <div class="text-center">
                                                                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtHargaStnX" Enabled="false" Text='<%# Eval("hargaSatuan","{0:n2}") %>'></asp:TextBox>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Jumlah" ItemStyle-Width="8%">
                                                                            <ItemTemplate>
                                                                                <div class="text-center">
                                                                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtJumlahX" Enabled="false" Text='<%# Eval("jumlahAD","{0:n2}") %>'></asp:TextBox>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tahap 1" ItemStyle-Width="8%">
                                                                            <ItemTemplate>
                                                                                <div class="text-center">
                                                                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtTahap1X" Enabled="false" Text='<%# Eval("tahap1","{0:n2}") %>'></asp:TextBox>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tahap 2" ItemStyle-Width="8%">
                                                                            <ItemTemplate>
                                                                                <div class="text-center">
                                                                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtTahap2X" Enabled="false" Text='<%# Eval("tahap2","{0:n2}") %>'></asp:TextBox>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tahap 3" ItemStyle-Width="8%">
                                                                            <ItemTemplate>
                                                                                <div class="text-center">
                                                                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtTahap3X" Enabled="false" Text='<%# Eval("tahap3","{0:n2}") %>'></asp:TextBox>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tahap 4" ItemStyle-Width="8%">
                                                                            <ItemTemplate>
                                                                                <div class="text-center">
                                                                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtTahap4X" Enabled="false" Text='<%# Eval("tahap4","{0:n2}") %>'></asp:TextBox>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabView" runat="server" visible="false">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-left">Akun <span class="mandatory">*</span></label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="cboAkun" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboAkun_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <div class="table-responsive">
                                    <asp:HiddenField ID="hdnNoRekD" runat="server" />
                                    <asp:HiddenField ID="hdnNoTRKASD" runat="server" />
                                    <asp:HiddenField ID="hdnJmlTahap" runat="server" />
                                    <asp:GridView ID="grdRekA" SkinID="GridView" runat="server" OnRowDataBound="grdRekA_RowDataBound" OnRowCommand="grdRekA_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kode Barang" ItemStyle-Width="18%">
                                                <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="form-inline">
                                                            <div class="text-center">
                                                                <asp:HiddenField runat="server" ID="hdnnoTRKAS_AD" />
                                                                <asp:HiddenField runat="server" ID="hdnBrgX" />
                                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtkdBrgX" Width="100" Enabled="false"></asp:TextBox>
                                                                <asp:ImageButton ID="btnImgAccount" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image form-control" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                                <asp:ImageButton ID="btnClear" runat="server" ImageUrl="~/assets/images/icon_trash.png" CssClass="btn-image form-control" CommandName="Clear" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Keterangan" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <div class="text-left">
                                                        <asp:Label runat="server" ID="lblKeteranganX"></asp:Label>
                                                        <%--<asp:DropDownList runat="server" ID="cboBarang" CssClass="form-control"></asp:DropDownList>--%>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Volume" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtVolumeX" OnTextChanged="txtVolumeX_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Label runat="server" ID="lblSatuanX"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Harga Satuan" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtHargaStnX" OnTextChanged="txtHargaStnX_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Jumlah" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtJumlahX" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tahap 1" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtTahap1X"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tahap 2" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtTahap2X"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tahap 3" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtTahap3X"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tahap 4" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtTahap4X"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-12">
                                <div class="text-center">
                                    <asp:Button ID="btnAddrowRekD" runat="server" Text="Tambah Baris" CssClass="btn btn-success" CausesValidation="false" OnClick="btnAddrowRekD_Click" />
                                    <asp:Button ID="btnSaveRekD" runat="server" Text="Simpan" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnSaveRekD_Click" />
                                    <asp:Button ID="btnBatalRekD" runat="server" Text="Kembali" CssClass="btn btn-danger" CausesValidation="false" OnClick="btnBatalRekD_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:LinkButton ID="LinkButton2" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgBarang" runat="server" PopupControlID="panelBarang" TargetControlID="LinkButton2" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelBarang" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Barang</h4>
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
                <asp:Label ID="lblMessageBarang" runat="server"></asp:Label>
                <asp:HiddenField ID="hdnRow" runat="server" />
                <div class="table-responsive">
                    <asp:GridView ID="grdBarang" SkinID="GridView" DataKeyNames="nobarang" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdBarang_PageIndexChanging" OnSelectedIndexChanged="grdBarang_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kode Barang" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblKdBrg" Text='<%#Eval("kodeBarang")%>'></asp:Label>
                                        <asp:HiddenField ID="hdnNoBrg" runat="server" Value='<%#Eval("nobarang")%>'/>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Barang" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblBrg" Text='<%#Eval("namaBarang")%>'></asp:Label>
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
                <asp:Button ID="btnTutup" runat="server" Text="Tutup" CssClass="btn btn-danger" CausesValidation="false" OnClick="btnTutup_Click" />
            </div>
        </div>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
