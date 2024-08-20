<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransPOView1.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TransPOView1" %>

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

        function CalculatePPn() {
            var Qty = 0, unitPrice = 0, hasil = 0;
            var gridView = document.getElementById("<%=grdPODetil.ClientID %>");
            var pajak = document.getElementById("<%=cboPajak.ClientID %>").value;
            
            var subTotal = 0;
            for (var i = 1; i < gridView.rows.length; i++) {
                Qty = removeMoney(gridView.rows[i].cells[3].getElementsByTagName("INPUT")[0].value) * 1;
                unitPrice = removeMoney(gridView.rows[i].cells[5].getElementsByTagName("INPUT")[0].value) * 1;

                gridView.rows[i].cells[7].getElementsByTagName("INPUT")[0].value = money(unitPrice * Qty);

                subTotal += (removeMoney(gridView.rows[i].cells[7].getElementsByTagName("INPUT")[0].value) * 1);

            }

            document.getElementById("<%=txtSubTotal.ClientID %>").value = money(subTotal);

            if (pajak == "Ya") {
                var ppn = (subTotal * 10 / 100) * 1;

                document.getElementById("<%=txtSubTotal.ClientID %>").value = money(subTotal);
                document.getElementById("<%=txtPPn.ClientID %>").value = money(ppn);
                document.getElementById("<%=txtTotal.ClientID %>").value = money(subTotal + ppn);
            }
            else {
                var ppn = 0;
                document.getElementById("<%=txtSubTotal.ClientID %>").value = money(subTotal);
                document.getElementById("<%=txtPPn.ClientID %>").value = money(ppn);
                document.getElementById("<%=txtTotal.ClientID %>").value = money(subTotal + ppn);
            }
        }

        function CalculatePPh() {
            var gridView = document.getElementById("<%=grdPODetil.ClientID %>");
            
            var subTotal = 0;
             for (var i = 1; i < gridView.rows.length; i++) {

                 unitPrice = removeMoney(gridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value) * 1;

                 gridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value = money((unitPrice));

                 subTotal += (removeMoney(gridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value) * 1);
             }

             document.getElementById("<%=txtSubTotal.ClientID %>").value = money(subTotal);

            
       }

    </script>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnPersenPajak" runat="server" />
    <asp:HiddenField ID="hdnPersenPPh" runat="server" />  
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
                        <asp:DropDownList ID="cboCabang" class="form-control" runat="server" ></asp:DropDownList>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"/>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdPO" DataKeyNames="noPO" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPO_PageIndexChanging"
                    OnSelectedIndexChanged="grdPO_SelectedIndexChanged" OnRowCommand="grdPOView_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnIdPrint" runat="server" Value='<%# Eval("noPO") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TglPO" HeaderStyle-CssClass="text-center" HeaderText="Tanggal" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="kodePO" HeaderStyle-CssClass="text-center" HeaderText="Kode PO" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="namaSupplier" HeaderStyle-CssClass="text-center" HeaderText="Supplier" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Detil" CommandName="Select" />
                                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-success btn-sm" Text="Print" CommandName="detail" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
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
                                        <label class="col-sm-3 control-label">Kode Beli</label>
                                        <div class="col-sm-5">
                                            <asp:HiddenField runat="server" ID="hdntipePO" />
                                            <asp:TextBox runat="server" ID="txtKodePo" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Tgl Beli <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control date" Enabled="false" ID="dtKas"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Nama Supplier </label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtSupplier" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group" id="showPajak" runat="server">
                                        <label class="col-sm-3 control-label">Pajak <span class="mandatory">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboPajak" runat="server" CssClass="form-control" Enabled="false">
                                                <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                                <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" id="showPPh" runat="server">
                                        <label class="col-sm-3 control-label">PPh <span class="mandatory">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboPPh" runat="server" CssClass="form-control" Enabled="false">
                                                <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                                <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Catatan</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtKeterangan" runat="server" TextMode="MultiLine" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 overflow-x-table">
                            <asp:GridView ID="grdPODetil" SkinID="GridView" runat="server" OnRowDataBound="grdPODetil_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <%# Container.DataItemIndex + 1 %>
                                                <asp:HiddenField ID="hdnParameter" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kode Barang" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" ID="txtkodebrg" Width="100" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Barang" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <div class="text-left">
                                                        <asp:Label runat="server" ID="lblnamaBarang" Width="150"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <div class="text-left">
                                                <asp:TextBox runat="server" CssClass="form-control money" ID="txtQty" Enabled="false" onblur="return Calculate(event);"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtStn" Enabled="false"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Harga Satuan" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txthargaPO" onblur="return Calculate(event);"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Alokasi Budget" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" CssClass="form-control money" ID="txtBudgetPR" Enabled="false"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nilai PO" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txtTotal" Enabled="false" onblur="return Calculate(event);"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 text-right">
                            <div class="form-group">
                                <label class="col-sm-10 control-label">Sub Total</label>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" ID="txtSubTotal" CssClass="form-control money" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="showDetil" runat="server">
                        <div class="col-sm-12 text-right">
                            <div class="form-group">
                                <label class="col-sm-10 control-label">PPn <asp:Label ID="lblPersenPPn" runat="server"></asp:Label>%</label>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" ID="txtPPn" CssClass="form-control money" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="showDetil2" runat="server">
                        <div class="col-sm-12 text-right">
                            <div class="form-group">
                                <label class="col-sm-10 control-label">Total</label>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" ID="txtTotal" CssClass="form-control money" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-12">
                            <div class="text-center">
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

