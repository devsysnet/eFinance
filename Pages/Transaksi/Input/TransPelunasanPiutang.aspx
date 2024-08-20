<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransPelunasanPiutang.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransPelunasanPiutang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdPelunasanDetail.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[11].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }

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
            var nilaiBayarHeader = removeMoney(document.getElementById("<%=hdnNilaiBayar.ClientID %>").value) * 1;
            var GridView = document.getElementById("<%=grdPelunasanDetail.ClientID %>");
            var totnilaibayar = 0, totBank = 0, totPendapatan = 0, totUangMuka = 0, totBayar = 0, totSaldo = 0, totalBayar = 0, saldo = 0;;
            for (var i = 1; i < GridView.rows.length; i++) {
                if (i != GridView.rows.length - 1) {
                    var saldoPiutang = removeMoney(GridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value) * 1;
                    var nilaibayar = removeMoney(GridView.rows[i].cells[5].getElementsByTagName("INPUT")[0].value) * 1;
                    var badm = removeMoney(GridView.rows[i].cells[6].getElementsByTagName("INPUT")[0].value) * 1;
                    var pendapatan = removeMoney(GridView.rows[i].cells[7].getElementsByTagName("INPUT")[0].value) * 1;
                    var uangmuka = removeMoney(GridView.rows[i].cells[8].getElementsByTagName("INPUT")[0].value) * 1;

                    if (GridView.rows[i].cells[11].getElementsByTagName("INPUT")[0].checked == true) {                    
                        var totalBayar = ((nilaibayar + badm) - pendapatan) + uangmuka;
                        var saldo = saldoPiutang - nilaibayar;
                        GridView.rows[i].cells[9].getElementsByTagName("INPUT")[0].value = money(totalBayar);
                        GridView.rows[i].cells[10].getElementsByTagName("INPUT")[0].value = money(saldo);
                        totnilaibayar += nilaibayar;
                        totBank += badm;
                        totPendapatan += pendapatan;
                        totUangMuka += uangmuka;
                        totBayar += totalBayar;
                        totSaldo += saldo;
                    }
                    else
                    {
                        GridView.rows[i].cells[9].getElementsByTagName("INPUT")[0].value = "0.00";
                        GridView.rows[i].cells[10].getElementsByTagName("INPUT")[0].value = "0.00";
                    }
                }
            }
            document.getElementById("BodyContent_grdPelunasanDetail_txttotNilaiBayar").value = money(totnilaibayar);
            document.getElementById("BodyContent_grdPelunasanDetail_txttotBank").value = money(totBank);
            document.getElementById("BodyContent_grdPelunasanDetail_txttotPendapatan").value = money(totPendapatan);
            document.getElementById("BodyContent_grdPelunasanDetail_txttotUangMuka").value = money(totUangMuka);
            document.getElementById("BodyContent_grdPelunasanDetail_txttotBayar").value = money(totBayar);
            document.getElementById("BodyContent_grdPelunasanDetail_txttotSaldo").value = money(totSaldo);

        }
    </script>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="col-sm-8">
                <%--<asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" OnClientClick="return DeleteAll()" Text="Delete all selected" />--%>
            </div>
            <div class="col-sm-4">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Search : </label>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search.."></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdPelunasan" DataKeyNames="nopelunasan" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPelunasan_PageIndexChanging"
                    OnSelectedIndexChanged="grdPelunasan_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnMataUang" runat="server" Value='<%#Eval("nomatauang1") %>' />
                                    <asp:HiddenField ID="hdnCust" runat="server" Value='<%#Eval("noCust") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="tgl" SortExpression="tgl" HeaderText="Date Alocation" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="nomorkode" SortExpression="nomorkode" HeaderText="Code Alocation" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="customer" SortExpression="customer" HeaderText="Customer Name" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="namaMataUang" SortExpression="namaMataUang" HeaderText="Currency" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="nilaipelunasan" SortExpression="nilaipelunasan" HeaderText="Value" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:n2}" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Edit" CommandName="Select" />
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
                                <div class="form-group">
                                    <label class="col-sm-5 control-label">Kode Bayar </label>
                                    <div class="col-sm-5">
                                        <asp:Label runat="server" ID="lblKodeBayar"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label">Tanggal Bayar </label>
                                    <div class="col-sm-5">
                                        <asp:Label runat="server" ID="lblTanggal"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label">Customer </label>
                                    <div class="col-sm-5">
                                        <asp:Label runat="server" ID="lblCustomer"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label">Nilai Bayar </label>
                                    <div class="col-sm-5">
                                        <asp:Label runat="server" ID="lblNilaiBayar"></asp:Label>
                                        <asp:HiddenField ID="hdnNilaiBayar" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <asp:GridView ID="grdPelunasanDetail" SkinID="GridView" runat="server" ShowFooter="true" >
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnPiutang" runat="server" />
                                                    <asp:HiddenField ID="hdnNoCus" runat="server" />
                                                    <asp:HiddenField ID="hdnNoMataUang" runat="server" />
                                                 </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Faktur" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:Label runat="server" ID="lblFaktur" Width="200"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nilai Faktur" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:Label runat="server" ID="lblNilaiFaktur"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kurs" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:Label runat="server" ID="lblKurs"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Saldo Piutang" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                 <div class="text-left">
                                                     <asp:TextBox runat="server" ID="txtSaldoPiut" Width="150" CssClass="form-control money" Enabled="true"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div class="text-right">
                                                    <asp:Label runat="server" ID="lblJudul" Text="Grand Total"></asp:Label>
                                                <div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nilai Bayar" ItemStyle-Width="6%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox runat="server" ID="txtNilaiBayar" Width="150" CssClass="form-control money" onchange="Calculate();"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox runat="server" ID="txttotNilaiBayar" Width="150" CssClass="form-control money" Text="0.00" Enabled="false"></asp:TextBox>
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Badan Adm. Bank" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox runat="server" ID="txtBank" Width="150" CssClass="form-control money" onchange="Calculate();"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox runat="server" ID="txttotBank" Width="150" CssClass="form-control money" Text="0.00" Enabled="false"></asp:TextBox>
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Pendapatan" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox runat="server" ID="txtPendapatan" Width="150" CssClass="form-control money" onchange="Calculate();"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox runat="server" ID="txttotPendapatan" Width="150" CssClass="form-control money" Text="0.00" Enabled="false"></asp:TextBox>
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Uang Muka" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox runat="server" ID="txtUangMuka" Width="150" CssClass="form-control money" onchange="Calculate();"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox runat="server" ID="txttotUangMuka" Width="150" CssClass="form-control money" Text="0.00" Enabled="false"></asp:TextBox>
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Total Bayar" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox runat="server" ID="txtTotalBayar" Width="150" CssClass="form-control money" Text="0.00" Enabled="false"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox runat="server" ID="txttotBayar" Width="150" CssClass="form-control money" Text="0.00" Enabled="false"></asp:TextBox>
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Saldo" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox runat="server" ID="txtSaldo" Width="150" CssClass="form-control money" Text="0.00" Enabled="false"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox runat="server" ID="txttotSaldo" Width="150" CssClass="form-control money" Text="0.00" Enabled="false"></asp:TextBox>
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                            <HeaderTemplate>
                                                <div class="text-center">
                                                    <!--<asp:CheckBox ID="chkCheckAll" runat="server" CssClass="px" onclick="return CheckAllData(this);" />-->
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:CheckBox ID="chkCheck" runat="server" CssClass="px chkCheck" onclick="Calculate();" />
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
