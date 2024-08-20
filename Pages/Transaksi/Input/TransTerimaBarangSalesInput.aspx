<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransTerimaBarangSalesInput.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransTerimaBarangSalesInput" %>
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
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdKas" DataKeyNames="noPRD" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdKas_PageIndexChanging"
                    OnSelectedIndexChanged="grdKas_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnnoPR" runat="server" Value='<%# Bind("noPR") %>' />
                                    <asp:HiddenField ID="Hiddennobrg" runat="server" Value='<%# Bind("nobarang") %>' />
                                    <asp:HiddenField ID="HiddennoPRD" runat="server" Value='<%# Bind("noPRD") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TglPR" HeaderStyle-CssClass="text-center" HeaderText="Tanggal PR" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="KodePR" HeaderStyle-CssClass="text-center" HeaderText="Kode PR" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="peminta" HeaderStyle-CssClass="text-center" HeaderText="Peminta" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="KodePO" HeaderStyle-CssClass="text-center" HeaderText="Kode PO" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="namaBarang" HeaderStyle-CssClass="text-center" HeaderText="Permintaan" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Uraian" HeaderStyle-CssClass="text-center" HeaderText="Uraian" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Detil" CommandName="Select" />
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
                                        <label class="col-sm-4 control-label">No PO <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" ID="txtkdPO" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Tanggal Terima <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:HiddenField ID="hdnnoPRD" runat="server" />
                                            <asp:HiddenField ID="hdnJnsPR" runat="server" />
                                            <asp:TextBox runat="server" CssClass="form-control date" ID="dtPO"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Penerima <span class="mandatory">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtPenerima"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Keterangan</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtKeterangan" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">No Surat Jalan <span class="mandatory">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtSuratJalan" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                               
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 overflow-x-table">
                            <asp:GridView ID="grdReceive" SkinID="GridView" runat="server" OnRowDataBound="grdReceive_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <%# Container.DataItemIndex + 1 %>
                                                <asp:HiddenField ID="hdnnoPR" runat="server" />
                                                <asp:HiddenField ID="hdnParameter" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kode Barang" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" ID="txtkodebrg" Enabled="false" Width="100" CssClass="form-control"></asp:TextBox>
                                                        <asp:HiddenField ID="hdnAccount" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Barang" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <div class="text-left">
                                                <asp:Label runat="server" ID="lblnamaBarang" Width="150"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <div class="text-left">
                                                <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txtQty" Enabled="False"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <div class="text-right">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtsatuan" Enabled="False"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty Sisa" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <div class="text-left">
                                                <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txtQtySisa" Enabled="False"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tgl Produk">
                                        <ItemTemplate>
                                            <div class="text-left">
                                                <asp:TextBox runat="server" CssClass="form-control date" ID="dtProduk"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Exp Date">
                                        <ItemTemplate>
                                            <div class="text-left">
                                                <asp:TextBox runat="server" CssClass="form-control date" ID="expDt"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Batchnumber" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <div class="text-left">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtbatchnumber"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty Terima" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <div class="text-left">
                                                <asp:TextBox runat="server" CssClass="form-control money" ID="txtQtyterima"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Catatan Perubahan PO" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCatatan" Enabled="False"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click"></asp:Button>
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

