<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransBudgetSumberPendapatan.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransBudgetSumberPendapatan" %>
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

            var GridView = document.getElementById("<%=grdBudget.ClientID %>");
            var vol = 0, hargaSatuan = 0, jumlah = 0;
            for (var i = 0; i < GridView.rows.length; i++) {
                    vol = (removeMoney(document.getElementById("BodyContent_grdBudget_txtVol_" + i).value));
                    hargaSatuan = (removeMoney(document.getElementById("BodyContent_grdBudget_txtHargaSat_" + i).value));
                    document.getElementById("BodyContent_grdBudget_txtJumlah_" + i).value = money(vol * hargaSatuan);
                    document.getElementById("BodyContent_grdBudget_txtSubJumlah_" + i).value = money(vol * hargaSatuan);
                    document.getElementById("BodyContent_grdBudget_txtTotal_" + i).value = money(vol * hargaSatuan);
               
            }
           

        }
    </script>
    <asp:HiddenField ID="hdnIdDetail" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            
                            <div class="col-sm-12">
                                   <label style="margin-left:10px">Tahun Ajaran : </label>
                                            <asp:DropDownList ID="cboTA" runat="server" CssClass="form-control" Width="150"></asp:DropDownList>
                                
                            </div>
                            
                        </div>
                    </div>
                    <br />
                        <div class="col-sm-12">
                            <div class="table-responsive overflow-x-table">
                                <asp:GridView ID="grdBudget" SkinID="GridView" runat="server" Width="120%" OnRowDataBound="GridView31_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField  HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnParameter" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                                </div>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Akun" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="text-center">
                                                            <asp:HiddenField ID="hdnNoRek" runat="server" Value='<%# Eval("noRek") %>' />
                                                            <asp:Label runat="server" ID="txtkdRek" Text='<%# Eval("kdRek") %>' Width="80"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Akun" ItemStyle-Width="18%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:Label runat="server" ID="txtKet" Text='<%# Eval("Ket") %>' Width="200"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Vol" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" Text="0.00" class="form-control money" ID="txtVol" Width="130" onkeyup="return Calculate()" onblur="return Calculate()"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Sat" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" class="form-control money" Text="0.00" ID="txtSat" Width="130"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Harga Satuan" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" class="form-control money" Text="0.00" ID="txtHargaSat" onkeyup="return Calculate()" onblur="return Calculate()" Width="130"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Jumlah" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" class="form-control money" Enabled="false"  ID="txtJumlah" Width="130"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Sub Jumlah" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" class="form-control money" Enabled="false" ID="txtSubJumlah" Width="130"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Total" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" class="form-control money" Enabled="false" ID="txtTotal" Width="130"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Total" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" class="form-control"  ID="txtKeterangan" Width="130"></asp:TextBox>
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
                                <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click"></asp:Button>
                                <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click"></asp:Button>
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