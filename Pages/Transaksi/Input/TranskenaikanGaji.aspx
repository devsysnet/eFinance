<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TranskenaikanGaji.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TranskenaikanGaji" %>

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
                <asp:GridView ID="grdKas" DataKeyNames="nokaryawan" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdKas_PageIndexChanging"
                    OnSelectedIndexChanged="grdKas_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnnokaryawan" runat="server" Value='<%# Bind("nokaryawan") %>' />
                                    <asp:HiddenField ID="hdjenis" runat="server" Value='<%# Bind("jenis") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="nik" HeaderStyle-CssClass="text-center" HeaderText="NIK" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="nama" HeaderStyle-CssClass="text-center" HeaderText="Nama" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Jab" HeaderStyle-CssClass="text-center" HeaderText="Jabatan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="tgldiangkat" HeaderStyle-CssClass="text-center" HeaderText="Tanggal Diangkat" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="gol" HeaderStyle-CssClass="text-center" HeaderText="Golongan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="jenis" HeaderStyle-CssClass="text-center" HeaderText="Jenis Kenaikan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
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
                                        <label class="col-sm-5 control-label">Tanggal Permintaan <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:HiddenField ID="hdnnokaryawan1" runat="server" />
                                            <asp:HiddenField ID="hdnjns" runat="server" />
                                            <asp:TextBox runat="server" CssClass="form-control date" ID="dtPO"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Nama Karyawan</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="Txtnama" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Tempat/Tanggal Lahir</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="Txttempatlahir" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">NIP/No.Kartu Pegawai</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="Txtnik" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Jabatan</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="TxtJabatan" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Pangkat/Golongan</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="Txtpangkat" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Tanggal Diangkat</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txttgldiangkat" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">SK Terakhir</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtnoSK" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Masa Kerja</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtlamakerja" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Gaji Pokok Lama</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtgapoklm" runat="server" CssClass="form-control money" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group" runat="server" id="showhidegol">
                                        <label class="col-sm-5 control-label">Ke Golongan</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtgolbaru" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Gaji Pokok Baru</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtgajibru" runat="server" CssClass="form-control money" Enabled="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Terhitung Tanggal</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control date" ID="txttglmulai" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click"></asp:Button>
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

