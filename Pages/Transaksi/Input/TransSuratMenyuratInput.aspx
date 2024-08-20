<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransSuratMenyuratInput.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransSuratMenyuratInput" %>
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
                                        <label for="pjs-ex1-fullname" class="col-sm-5 control-label">Jenis</label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="cboJenis" runat="server" Width="120">
                                                <asp:ListItem Value="Masuk">Masuk</asp:ListItem>
                                                <asp:ListItem Value="Keluar">Keluar</asp:ListItem>
                                            </asp:DropDownList>
                                           
                                        </div>
                                    </div>
                                    
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">No Surat</label>
                                         <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNoSurat"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Tanggal Surat<span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="dtDate" runat="server" class="form-control date col-sm-5"></asp:TextBox>
                                        </div>
                                    </div>

                                     <div class="form-group">
                                        <label class="col-sm-5 control-label">Perihal</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtPerihal" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                     <div class="form-group">
                                        <label class="col-sm-5 control-label">No File</label>
                                         <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNoFile"></asp:TextBox>
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



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>


