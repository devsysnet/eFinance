<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransCashBankView2.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TransCashBankView2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function OpenReport() {
            //            OpenReportViewer();
            window.open('../../../Report/ReportViewer.aspx', 'name', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,modal=yes,width=1000,height=600');
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
            var debit = 0, kredit = 0, hasilDebit = 0, hasilKredit = 0;
            var gridView = document.getElementById("<%=grdKasBank.ClientID %>");

            for (var i = 1; i < gridView.rows.length; i++) {
                debit = removeMoney(gridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value) * 1;
                kredit = removeMoney(gridView.rows[i].cells[5].getElementsByTagName("INPUT")[0].value) * 1;

                if (debit != 0)
                    hasilDebit += debit;
                else if (kredit != 0)
                    hasilKredit += kredit;
            }
            if (hasilDebit != 0)
                document.getElementById("BodyContent_txtTotalDebit").value = money(hasilDebit);
            else if (hasilKredit != 0)
                document.getElementById("BodyContent_txtTotalKredit").value = money(hasilKredit);
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
                         <asp:DropDownList ID="cboCabang" class="form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdKas" DataKeyNames="noKas" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdKas_PageIndexChanging"
                    OnSelectedIndexChanged="grdKas_SelectedIndexChanged" OnRowCommand="grdKasView_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                     <asp:HiddenField ID="hdnIdPrint" runat="server" value='<%# Eval("noKas") %>'/>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Tgl" SortExpression="Tgl" HeaderText="Tanggal" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="nomorKode" SortExpression="nomorKode" HeaderText="Nomor Kode" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="ket" SortExpression="nomorKode" HeaderText="Akun" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Cust" SortExpression="Cust" HeaderText="Dari/untuk" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="uraian" SortExpression="Cust" HeaderText="Uraian" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="nilaikas" SortExpression="nilai" HeaderText="Nilai" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right" />
                        <asp:BoundField DataField="namauser" SortExpression="nilai" HeaderText="Dibuat Oleh" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Detil" CommandName="Select" />
                                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-success btn-sm" Text="Print" CommandName="detail" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'/>
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
                                        <label class="col-sm-5 control-label">Transaction <span class="mandatory">*</span></label>
                                        <div class="col-sm-5"><asp:TextBox CssClass="form-control" runat="server" ID="Type" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Account <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cboAccount" Enabled="false"></asp:DropDownList>
                                        </div>
                                    </div> 
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Date <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control date" Enabled="false" ID="dtKas"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-sm-5 control-label">Currency <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cboCurrency" Enabled="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label"><asp:Label ID="Kurstrs" runat="server" CssClass="form-label" Text="kurs"></asp:Label></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtKurs" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Value <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txtValue" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Remark </label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtRemark" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <asp:GridView ID="grdKasBank" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnKasBank" runat="server" />
                                                    <asp:HiddenField ID="hdnParameter" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Account" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="text-center">
                                                            <asp:TextBox runat="server" ID="txtAccount" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnAccount" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Description" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:Label runat="server" ID="lblDescription"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Remark" ItemStyle-Width="12%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtRemark"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Debit" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txtDebit" onblur="return Calculate(event);"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Credit" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txtKredit" onblur="return Calculate(event);"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="form-inline">
                                        <label class="col-sm-8 control-label">Total </label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtTotalDebit" runat="server" Enabled="false" CssClass="form-control money" Width="160" Text="0.00"></asp:TextBox>
                                            <asp:TextBox ID="txtTotalKredit" runat="server" Enabled="false" CssClass="form-control money" Width="160" Text="0.00"></asp:TextBox>
                                        </div>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
