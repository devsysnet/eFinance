<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="mParameterdisc.aspx.cs" Inherits="eFinance.Pages.Master.Input.mParameterdisc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
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
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama Discount <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" class="form-control " ID="namaDiskon" placeholder="Nama Discount"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Transaksi</label>
                                <div class="col-sm-5">
                                     <asp:DropDownList ID="cbojnstransaksi" runat="server">
                                     </asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Mulai Berlaku</label>
                                <div class="col-sm-4">
                                   <asp:TextBox runat="server" class="form-control date" ID="dtsistem"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Selesai Berlaku</label>
                                <div class="col-sm-4">
                                   <asp:TextBox runat="server" class="form-control date" ID="dtsistem1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Discount</label>
                                <div class="col-sm-5">
                                    <asp:DropDownList ID="cboStatus" runat="server" CssClass="form-control"  Width="150">
                                              <asp:ListItem Value="-">----</asp:ListItem>
                                              <asp:ListItem Value="Nilai">Nilai</asp:ListItem>
                                              <asp:ListItem Value="Persen">Persented</asp:ListItem>
                                     </asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nilai Discount<span class="mandatory">*</span></label>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txtnilai" placeholder="Nilai Denda"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">COA Rekening<span class="mandatory">*</span></label>
                                <div class="col-sm-2">
                                   <asp:DropDownList ID="cbonorek" runat="server" Width="200">
                                         <asp:ListItem Value="">---Pilih COA ---</asp:ListItem>
                                     </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="text-center">
                                        <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"  />
                                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click" />
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


