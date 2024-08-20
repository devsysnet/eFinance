<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransPRView.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TransPRView" %>

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
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="col-sm-6">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <asp:TextBox ID="dtMulai" runat="server" CssClass="form-control date"></asp:TextBox>
                        <asp:TextBox ID="dtSampai" runat="server" CssClass="form-control date"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="col-sm-6">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Cabang : </label>
                        <asp:DropDownList ID="cboCabang" class="form-control" runat="server"></asp:DropDownList>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"/>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdPRView" DataKeyNames="nopr" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPRView_PageIndexChanging"
                    OnRowCommand="grdPRView_RowCommand" OnRowDataBound="grdPRView_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnstsappr" runat="server" Value='<%# Bind("stsApv") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="tglPR" HeaderText="Tanggal" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="KodePR" HeaderText="Kode PR" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="peminta" HeaderText="Peminta" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="jenisx" HeaderText="Jenis" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Status" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="Lblsts" runat="server" Text='<%# Bind("statuspr") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Detil" CommandName="SelectDetil" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnClose" runat="server" class="btn btn-danger btn-sm" Text="Close" CommandName="SelectClose" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
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
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Kode Permintaan </label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" ID="txtKdPR" Enabled="false" Width="140" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Tanggal Permintaan </label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="dtDate"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Jenis Permintaan </label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList runat="server" CssClass="form-control" Width="250" Enabled="false" ID="cboTransaction">
                                                <asp:ListItem Value="1">Barang Aset</asp:ListItem>
                                                <asp:ListItem Value="2">Dana</asp:ListItem>
                                                <asp:ListItem Value="3">Jasa</asp:ListItem>
                                                <asp:ListItem Value="4">Barang Operasional</asp:ListItem>
                                                <asp:ListItem Value="5">Barang Sales</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Dana BOS </label>
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
                                            <asp:DropDownList runat="server" CssClass="form-control" Width="200" Enabled="false" ID="cboKategoriDana">
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
                                            <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="dtDate1"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group" id="showProject" runat="server">
                                        <label class="col-sm-4 control-label">Project <span class="mandatory">*</span></label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList runat="server" CssClass="form-control" Enabled="false" Width="300" ID="cboProject">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" id="showKegiatan" runat="server">
                                        <label class="col-sm-4 control-label">Kegiatan <span class="mandatory">*</span></label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList runat="server" CssClass="form-control" Enabled="false" Width="300" ID="cboKegiatan">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Peminta </label>
                                        <div class="col-sm-5">
                                            <asp:HiddenField runat="server" ID="hdnNoUser" />
                                            <asp:TextBox ID="txtNamaPeminta" Enabled="false" runat="server" CssClass="form-control" Width="250" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Peruntukan untuk </label>
                                        <div class="col-sm-7">
                                            <asp:TextBox runat="server" TextMode="MultiLine" CssClass="form-control" ID="txtKeterangan" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group" runat="server" id="ketreject">
                                        <label class="col-sm-4 control-label">Alasan Reject </label>
                                        <div class="col-sm-7">
                                            <asp:TextBox runat="server" TextMode="MultiLine" CssClass="form-control" ID="txtReject" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <asp:GridView ID="grdDetail" SkinID="GridView" runat="server" OnRowDataBound="grdDetail_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="txtHdnValue" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kode Barang" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <div class="col-sm-10">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtKode" Enabled="false"></asp:TextBox>
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
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtNilai"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cboSatuan" class="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Harga Satuan" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtHargaStn"></asp:TextBox>
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
                                                    <asp:TextBox runat="server" class="form-control" Enabled="true" ID="Keterangan"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Pembuat PO" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" class="form-control" Enabled="true" ID="pilihan"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Dana BOS" ItemStyle-Width="4%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cboDana" class="form-control" runat="server" Width="100">
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
                    <div class="form-group row">
                        <div class="col-sm-12 text-right">
                            <div class="form-group">
                                <label class="col-sm-10 control-label">Sub Total</label>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" ID="txtSubTotal" CssClass="form-control money" Enabled="false" Text="0.00"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click"></asp:Button>
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

