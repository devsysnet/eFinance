<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransSaldoAwalGL.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransSaldoAwalGL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function Calculate() {
            const formatter = new Intl.NumberFormat('en-US', {


                // These options are needed to round to whole numbers if that's what you want.
                //minimumFractionDigits: 0, // (this suffices for whole numbers, but will print 2500.10 as $2,500.1)
                //maximumFractionDigits: 0, // (causes 2500.99 to be printed as $2,501)
            });

        
            var GridView = document.getElementById("<%=grdSaldoGL.ClientID %>");
            var totalDebet = 0;
            var totalKredit = 0;
            for (var i = 1; i < GridView.rows.length - 1; i++) {
                GridView.rows[i].cells[3].getElementsByTagName("INPUT")[0].value = formatter.format(removeMoney(GridView.rows[i].cells[3].getElementsByTagName("INPUT")[0].value))
                GridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value = formatter.format(removeMoney(GridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value))

                var debet = removeMoney(GridView.rows[i].cells[3].getElementsByTagName("INPUT")[0].value) * 1;
                var kredit = removeMoney(GridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value) * 1;
                totalDebet += debet;
                totalKredit += kredit;
            }
            document.getElementById("BodyContent_grdSaldoGL_txtTotalDebet").value = formatter.format(totalDebet);
            document.getElementById("BodyContent_grdSaldoGL_txtTotalKredit").value = formatter.format(totalKredit);

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
    </script>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <div class="form-inline">
                                    <label class="col-sm-2 control-label">Date</label>
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="cboMonth" runat="server" Width="120">
                                            <asp:ListItem Value="1">Januari</asp:ListItem>
                                            <asp:ListItem Value="2">Februari</asp:ListItem>
                                            <asp:ListItem Value="3">Maret</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">Mei</asp:ListItem>
                                            <asp:ListItem Value="6">Juni</asp:ListItem>
                                            <asp:ListItem Value="7">Juli</asp:ListItem>
                                            <asp:ListItem Value="8">Agustus</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">Oktober</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">Desember</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="cboYear" runat="server" Width="150">
                                        </asp:DropDownList>
                                        <%--<asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click"></asp:Button>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <asp:GridView ID="grdSaldoGL" DataKeyNames="kdrek" SkinID="GridView" runat="server" ShowFooter="true" OnRowDataBound="grdSaldoGL_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Bind("pos") %>' />
                                                    <asp:HiddenField ID="hdnNoRek" runat="server" Value='<%# Bind("noRek") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="kdrek" SortExpression="kdrek" HeaderText="Account Code" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("ket") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div class="text-right">
                                                    Total
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="Debet">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtDebet" runat="server" CssClass="form-control" value="0.00" Width="180" Text='<%# Bind("debet") %>' onblur="return Calculate()" onchange="return Calculate()"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtTotalDebet" runat="server" CssClass="form-control" value="0.00" Width="180" Enabled="false"></asp:TextBox>
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="Kredit">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtKredit" runat="server" CssClass="form-control " value="0.00" Width="180" Text='<%# Bind("kredit") %>' onblur="return Calculate()" onchange="return Calculate()"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtTotalKredit" runat="server" CssClass="form-control " value="0.00" Width="180" Enabled="false"></asp:TextBox>
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row">
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
