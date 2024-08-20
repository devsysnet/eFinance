<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransPRUpdate.aspx.cs" Inherits="eFinance.Pages.Transaksi.Update.TransPRUpdate" %>

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
            var GridView = document.getElementById("<%=grdDetail.ClientID %>");
            var tipe = document.getElementById("<%=cboTransaction.ClientID %>").value;
            var subTotal = 0;
            for (var i = 1; i < GridView.rows.length; i++) {
                if (GridView.rows[i].cells[0].innerHTML.trim() != "No records data") {
                    //if (tipe == "1" || tipe == "4") {
                    //    var qty = removeMoney(GridView.rows[i].cells[3].getElementsByTagName("INPUT")[0].value) * 1;
                    //    var harga = removeMoney(GridView.rows[i].cells[5].getElementsByTagName("INPUT")[0].value) * 1;
                    //    GridView.rows[i].cells[6].getElementsByTagName("INPUT")[0].value = money(qty * harga)

                    //    subTotal += (removeMoney(GridView.rows[i].cells[6].getElementsByTagName("INPUT")[0].value) * 1);
                    //}
                    if (tipe == "1" || tipe == "4" || tipe == "5") {
                        var konfersi = GridView.rows[i].cells[0].getElementsByTagName("INPUT")[1].value;
                        var qtyBesar = removeMoney(GridView.rows[i].cells[3].getElementsByTagName("INPUT")[0].value) * 1;
                        var qty = removeMoney(GridView.rows[i].cells[5].getElementsByTagName("INPUT")[0].value) * 1;
                        var harga = removeMoney(GridView.rows[i].cells[7].getElementsByTagName("INPUT")[0].value) * 1;
                        GridView.rows[i].cells[8].getElementsByTagName("INPUT")[0].value = money(((qtyBesar * konfersi) + qty) * harga)

                        subTotal += (removeMoney(GridView.rows[i].cells[8].getElementsByTagName("INPUT")[0].value) * 1);

                    }
                    else if (tipe == "2") {
                        subTotal += (removeMoney(GridView.rows[i].cells[3].getElementsByTagName("INPUT")[0].value) * 1);
                    }
                    else if (tipe == "3") {
                        subTotal += (removeMoney(GridView.rows[i].cells[3].getElementsByTagName("INPUT")[0].value) * 1);
                    }
                }
            }
            document.getElementById("<%=txtSubTotal.ClientID %>").value = money(subTotal);
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
                                            <label>Cari :</label>
                                            <asp:TextBox ID="txtSearchAwal" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:Button ID="btnSearchAwal" runat="server" CssClass="btn btn-sm btn-primary" Text="Cari" OnClick="btnSearchAwal_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdPRUpdate" DataKeyNames="noPR" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPRUpdate_PageIndexChanging" OnRowCommand="grdPRUpdate_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="NO." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="kodePR" HeaderText="Kode PR" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="tglPR" HeaderText="Tgl PR" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="peminta" HeaderText="Peminta" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="sts" HeaderText="Status" HeaderStyle-CssClass="text-left" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="reasson" HeaderText="Alasan Reject" HeaderStyle-CssClass="text-left" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Ubah" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectDel" runat="server" class="btn btn-xs btn-labeled btn-danger" Text="Hapus" CommandName="SelectDelete" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Kode Permintaan</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtKdPR" runat="server" CssClass="form-control" Width="140" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Tanggal Permintaan <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="dtDate" runat="server" CssClass="form-control date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Jenis Permintaan <span class="mandatory">*</span></label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList runat="server" CssClass="form-control" Width="250" Enabled="false" ID="cboTransaction" AutoPostBack="true" OnSelectedIndexChanged="cboTransaction_SelectedIndexChanged">
                                                <asp:ListItem Value="1">Barang Aset</asp:ListItem>
                                                <asp:ListItem Value="2">Dana</asp:ListItem>
                                                <asp:ListItem Value="3">Jasa</asp:ListItem>
                                                <asp:ListItem Value="4">Barang Operasional</asp:ListItem>
                                                <asp:ListItem Value="5">Barang Sales</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" runat="server" id="divDanaBOSH">
                                        <label class="col-sm-4 control-label">Dana BOS</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboDanaH" class="form-control" runat="server" Width="155" Enabled="false">
                                                <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                                <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group" id="showKatDana" runat="server">
                                        <label class="col-sm-4 control-label">Kategori Dana <span class="mandatory">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList runat="server" CssClass="form-control" Width="200" Enabled="false" ID="cboKategoriDana" AutoPostBack="true" OnSelectedIndexChanged="cboKategoriDana_SelectedIndexChanged">
                                                <asp:ListItem Value="">--Pilih Kategori Dana--</asp:ListItem>
                                                <asp:ListItem Value="Rutin">Rutin</asp:ListItem>
                                                <asp:ListItem Value="NonRutin">Non Rutin</asp:ListItem>
                                                <asp:ListItem Value="Project">Project</asp:ListItem>
                                                <asp:ListItem Value="Kegiatan">Kegiatan</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Tanggal Pelaksanaan</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="dtDate1" runat="server" CssClass="form-control date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group" id="showProject" runat="server">
                                        <label class="col-sm-4 control-label">Project <span class="mandatory">*</span></label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList runat="server" CssClass="form-control" Width="300" ID="cboProject">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                     <div class="form-group" id="showKegiatan" runat="server">
                                        <label class="col-sm-4 control-label">Project <span class="mandatory">*</span></label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList runat="server" CssClass="form-control" Width="300" ID="cboKegiatan">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-4 control-label">Peminta <span class="mandatory">*</span></label>
                                            <div class="col-sm-4">
                                                <asp:HiddenField runat="server" ID="hdnNoUser" />
                                                <div class="input-group-btn">
                                                    <asp:TextBox ID="txtNamaPeminta" Enabled="false" runat="server" CssClass="form-control" />
                                                    <asp:ImageButton ID="btnBrowsePeminta" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image form-control" OnClick="btnBrowsePeminta_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Peruntukan untuk</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtKeterangan" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdDetail" SkinID="GridView" runat="server" OnRowDataBound="grdDetail_RowDataBound" OnRowCommand="grdDetail_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:HiddenField ID="txtHdnValue" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                                        <asp:HiddenField ID="hdnKonversiGrid" runat="server" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kode Barang" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="form-inline">
                                                            <div class="text-center">
                                                                <asp:TextBox runat="server" CssClass="form-control" Width="100" ID="txtKode" Enabled="false"></asp:TextBox>
                                                                <asp:ImageButton ID="btnBrowseBiaya" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image form-control" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                                <asp:ImageButton ID="btnClear" runat="server" ImageUrl="~/assets/images/icon_trash.png" CssClass="btn-image form-control" CommandName="Clear" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Barang" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <div class="text-left">
                                                        <asp:Label runat="server" ID="lblNama"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty Besar" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtNilaiBesar" onchange="Calculate();"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%--<asp:DropDownList ID="cboSatuan" class="form-control" runat="server"></asp:DropDownList>--%>
                                                        <asp:Label ID="lblsatuanBesar" runat="server" ></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtNilai" onchange="Calculate();"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%--<asp:DropDownList ID="cboSatuan" class="form-control" runat="server"></asp:DropDownList>--%>
                                                        <asp:Label ID="lblsatuan" runat="server"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Harga Satuan" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtHargaStn" onchange="Calculate();"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Alokasi Budget" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtBudgetPR"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Keterangan" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" class="form-control" ID="Keterangan"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Dana BOS" ItemStyle-Width="4%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:DropDownList ID="cboDana" class="form-control" runat="server" Width="100" Enabled="false">
                                                            <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                                            <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 text-right">
                                <div class="form-group">
                                    <label class="col-sm-10 control-label">Sub Total</label>
                                    <div class="col-sm-2">
                                        <asp:TextBox runat="server" ID="txtSubTotal" CssClass="form-control money" Enabled="false" Text="0.00"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="text-center">
                                    <asp:Button ID="btnAddrow" runat="server" CssClass="btn btn-success" Text="Tambah Baris" CausesValidation="false" OnClick="btnAddrow_Click" />
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Ubah" CausesValidation="false" OnClick="btnSubmit_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"/>
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Batal" CausesValidation="false" OnClick="btnCancel_Click" />
                                </div>
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
                <h4 class="modal-title">Data Peminta</h4>
            </div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="modal-body ">
                <div class="modal-body col-overflow-400">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group ">
                                <div class="form-inline">
                                    <label class="col-sm-3 control-label">Cari</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtSearchMinta" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:Button ID="btnMinta" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnMinta_Click" CausesValidation="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="grdDataPeminta" DataKeyNames="nokaryawan" ShowFooter="true" SkinID="GridView" runat="server"  OnSelectedIndexChanged="grdDataPeminta_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:HiddenField ID="hdnNoUserD" runat="server" Value='<%# Bind("nokaryawan") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="idPeg" HeaderStyle-CssClass="text-center" HeaderText="ID Peg" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="nama" HeaderStyle-CssClass="text-center" HeaderText="Nama Peminta" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="" ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-xs btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="panelAddDataBiaya" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Detil</h4>
            </div>

            <div class="modal-body ">
                <asp:Label runat="server" ID="lblMessageError"></asp:Label>
                <div class="modal-body col-overflow-400">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group ">
                                <div class="form-inline">
                                    <label class="col-sm-3 control-label">Cari</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnCari_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:HiddenField ID="txtHdnPopup" runat="server" />
                        <asp:GridView ID="grdDataBiaya" DataKeyNames="noBiaya" ShowFooter="true" SkinID="GridView" runat="server" OnSelectedIndexChanged="grdDataBiaya_SelectedIndexChanged" OnRowDataBound="grdDataBiaya_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:HiddenField runat="server" ID="hdnSatuanBesar" Value='<%# Bind("satuanbesar") %>' />
                                            <asp:HiddenField runat="server" ID="hdnSatuan" Value='<%# Bind("satuan") %>' />
                                            <asp:HiddenField runat="server" ID="hdnkonfersi" Value='<%# Bind("konfersi") %>' />
                                            <asp:HiddenField runat="server" ID="hdnharga" Value='<%# Bind("harga") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Kode" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="Kode" runat="server" Text='<%# Bind("Kode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nama" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="Nama" runat="server" Text='<%# Bind("Nama") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="" ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-xs btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

