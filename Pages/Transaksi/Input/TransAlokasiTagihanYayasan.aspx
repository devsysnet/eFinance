<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransAlokasiTagihanYayasan.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransAlokasiTagihanYayasan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdARSiswa.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
            CalculateCheck();
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
        function round(value, decimals) {
            return Number(Math.round(value + 'e' + decimals) + 'e-' + decimals);
        }
        function Calculate() {
            var gridView = document.getElementById("<%=grdARSiswa.ClientID %>");
            for (var i = 1; i < gridView.rows.length; i++) {
                piutang = removeMoney(gridView.rows[i].cells[0].getElementsByTagName("INPUT")[9].value) * 1;
                      
            }
      
        }
        function CalculateCheck() {
            var total = 0;
            var gridView = document.getElementById("<%=grdARSiswa.ClientID %>");

            for (var i = 1; i < gridView.rows.length; i++) {
                bayar = removeMoney(gridView.rows[i].cells[9].getElementsByTagName("INPUT")[0].value) * 1;
                console.log(bayar);
                if (gridView.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked == true)
                    if (bayar != 0)
                        total += bayar;
            } 
            if (total != 0)
                document.getElementById("BodyContent_txtTotal").value = money(round(total, 2));
            else
                document.getElementById("BodyContent_txtTotal").value = money(round(total, 2));

        }
    </script>

    <asp:HiddenField ID="hdnId" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">

                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-3">

                            </div>
                            <div class="col-sm-9">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <label>Unit :</label>
                                            <asp:DropDownList ID="cboUnit" Width="250" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboUnit_SelectedIndexChanged"></asp:DropDownList>

                                            <label>Cari :</label>
                                            <asp:TextBox ID="txtSearchAsset" runat="server" CssClass="form-control" OnTextChanged="tes" AutoPostBack="true"></asp:TextBox>
                                            <asp:Button Visible="false" ID="btnSearchAsset" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" style="margin-left:15px"  />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdAssetUpdate" DataKeyNames="noSiswa" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdAssetUpdate_PageIndexChanging" OnRowCommand="grdAssetUpdate_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                   <asp:HiddenField ID="payment_transaction_id" runat="server" Value='<%# Bind("payment_transaction_id") %>' />
                                                    <asp:HiddenField ID="tglpembayaran" runat="server" Value='<%# Bind("payment_date") %>' />
                                                    <asp:HiddenField ID="hdnnocabangsiswa" runat="server" Value='<%# Bind("nocabang") %>' />
                                                    <asp:HiddenField ID="totalbayar" runat="server" Value='<%# Bind("amount") %>' />

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="payment_date" HeaderText="Tanggal" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="namaSiswa" HeaderText="Nama Siswa" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%"   />
                                            <asp:BoundField DataField="namaOrangtua" HeaderText="Nama Orang Tua" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%"  />
                                            <asp:BoundField DataField="telp" HeaderText="No Telp" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Center"/>
                                            <asp:BoundField DataField="novirtual" HeaderText="No Virtual" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Center"/>
                                              <asp:BoundField DataField="amount" HeaderText="Nilai" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right"/>
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Detail" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row">
                            <div class="responsive">
                                <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="form-inline">
                                        <label class="col-sm-10 control-label">Nilai</label>
                                        <div class="col-sm-2">
                                                        <asp:TextBox ID="txtAmount1" runat="server" CssClass="form-control money" Enabled="false"  Width="160"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                                <asp:GridView ID="grdARSiswa" DataKeyNames="noSiswa" SkinID="GridView" runat="server" AllowPaging="false" PageSize="100" >
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:HiddenField ID="hdnpayment_transaction_id" runat="server" Value='<%# Bind("payment_transaction_id") %>' />
                                                        <asp:HiddenField ID="hdnNoSiswa" runat="server" Value='<%# Bind("noSiswa") %>' />
                                                        <asp:HiddenField ID="hdnNnopiutang" runat="server" Value='<%# Bind("nopiutang") %>' />
                                                        <asp:HiddenField ID="hdntglbayar1" runat="server" Value='<%# Bind("tglbayar") %>' />
                                                         <asp:HiddenField ID="saldo" runat="server" Value='<%# Bind("saldo") %>' />
                                                         <asp:HiddenField ID="hdnjenistransaksi" runat="server" Value='<%# Bind("jenisTransaksi") %>' />
                                                        <asp:HiddenField ID="amount" runat="server" Value='<%# Bind("amount") %>' />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <HeaderTemplate>
                                                    <div class="text-center">
                                                        <asp:CheckBox ID="chkCheckAll" runat="server" CssClass="px" onclick="return CheckAllData(this); return CalculateCheck(this);" />
                                                    </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:CheckBox ID="chkCheck" runat="server" CssClass="px chkCheck" onclick="return CalculateCheck(this);" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:BoundField DataField="namaSiswa" HeaderText="Nama Siswa" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%"   />
                                            <asp:BoundField DataField="jenisTransaksi" HeaderText="Jenis Transaksi" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%"  />
                                             <asp:BoundField DataField="tgl" HeaderText="Tanggal Tagihan" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="tglbayar" HeaderText="Tanggal Bayar" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%"  ItemStyle-HorizontalAlign="Center"/>
                                            <asp:BoundField DataField="noVA" HeaderText="No Virtual" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Center"/>
                                            <asp:BoundField DataField="nilai" HeaderText="Nilai Tagihan" HeaderStyle-CssClass="text-center"  ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right"/>
                                            <asp:BoundField DataField="saldo" HeaderText="Saldo" HeaderStyle-CssClass="text-center"  ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right"/>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" HeaderText="Bayar">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox ID="txtbayar" runat="server" CssClass="form-control money" Text='<%# Bind("saldo") %>' Width="100" onBlur="return CalculateCheck(this);" onKeyup="return CalculateCheck(this);"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                            </div>
                            
                            <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="form-inline">
                                        <label class="col-sm-10 control-label">Total </label>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtTotal" runat="server" Enabled="false" CssClass="form-control " Width="160" Text="0"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="button" runat="server">
                            <div class="col-sm-12 ">
                                <div class="text-center">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
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
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
