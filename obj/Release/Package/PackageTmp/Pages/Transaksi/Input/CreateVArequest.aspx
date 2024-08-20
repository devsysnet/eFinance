<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="CreateVArequest.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.CreateVArequest" %>
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
        function round(value, decimals) {
            return Number(Math.round(value + 'e' + decimals) + 'e-' + decimals);
        }

        function Calculate() {
            var gridView = document.getElementById("<%=grdARSiswa.ClientID %>");
            var nilaiDiskon = document.getElementById("<%=hdnnilaiDiskon.ClientID %>").value; 
            for (var i = 1; i < gridView.rows.length; i++) {
                piutang = removeMoney(gridView.rows[i].cells[0].getElementsByTagName("INPUT")[4].value) * 1;
                      

            }
      
        }

        function CalculateCheck() {
            var total = 0;
            var gridView = document.getElementById("<%=grdARSiswa.ClientID %>");
            
            for (var i = 1; i < gridView.rows.length; i++) {
                bayar = removeMoney(gridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value) * 1;

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
    <asp:HiddenField ID="hdnNoRekDisc" runat="server" />
    <asp:HiddenField ID="hdnjnsTrans" runat="server" />
    <asp:HiddenField ID="hdnnilaiDiskon" runat="server" />
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
                                        <label class="col-sm-1 control-label text-left">Filter Unit </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboPerwakilanUnit" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboPerwakilanUnit_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="cboKelas" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                          <div class="col-sm-2">
                                               <asp:DropDownList ID="cbojenis" runat="server" Width="150">
                                                <asp:ListItem Value="0">Pilih Jenis VA</asp:ListItem>
                                                <asp:ListItem Value="open">Open</asp:ListItem>
                                                <asp:ListItem Value="close">Close</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                      
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label class="col-sm-1 control-label text-left">Nama/NoVA </label>
                                        <div class="col-sm-5">
                                            <div class="form-inline">
                                                <asp:TextBox ID="txtNamaSiswa" runat="server" CssClass="form-control" placeholder="Masukan Nama Siswa"></asp:TextBox>
                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdARSiswa" DataKeyNames="payment_transaction_id" SkinID="GridView" runat="server" AllowPaging="false" PageSize="100" OnPageIndexChanging="grdARSiswa_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:HiddenField ID="hdnNoPiut" runat="server" Value='<%# Bind("payment_transaction_id") %>' />
                                                        <asp:HiddenField ID="hdnNoSiswa" runat="server" Value='<%# Bind("noSiswa") %>' />
                                                        <asp:HiddenField ID="hdnTgl" runat="server" Value='<%# Bind("tgl") %>' />
                                                        <asp:HiddenField ID="hdnnilai" runat="server" Value='<%# Bind("nilai") %>' />
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
                                            <asp:BoundField DataField="namasiswa" HeaderText="Nama Siswa" HeaderStyle-CssClass="text-center" ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="kelas" HeaderText="Kelas" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="va" HeaderText="Nomor VA" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="tgl" HeaderText="Tanggal" HeaderStyle-CssClass="text-center" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="nilai" HeaderText="Nilai" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" DataFormatString="{0:n2}"  ItemStyle-HorizontalAlign="right" />
                                           <%-- <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" HeaderText="Bayar">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox ID="txtbayar" runat="server" CssClass="form-control money" Visible="true" Text='<%# Bind("nilai","{0:n2}") %>' Width="100" onBlur="return CalculateCheck(this);" onKeyup="return CalculateCheck(this);"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                          </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                      
                        <div class="row" id="button" runat="server">
                            <div class="col-sm-12 ">
                                <div class="text-center">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSubmit_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                    <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
