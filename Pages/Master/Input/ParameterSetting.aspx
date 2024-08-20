<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="ParameterSetting.aspx.cs" Inherits="eFinance.Pages.Master.Input.ParameterSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="TabName" runat="server" />
    <script type="text/javascript">
        $(function () {

            SetTabs();
        });
        Sys.Application.add_init(appl_init);
        function appl_init() {
            var pgRegMgr = Sys.WebForms.PageRequestManager.getInstance();
            pgRegMgr.add_beginRequest(SetTabs);
            pgRegMgr.add_endRequest(SetTabs);
        }
        function SetTabs() {
            var tabName = $("#<%=TabName.ClientID%>").val();
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("#<%=TabName.ClientID%>").val($(this).attr("href").replace("#", ""));
            });

        };
        function CheckAllDataRek1(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdAddRekCash.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
        function CheckAllDataRek2(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdAddBank.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
        function CheckAllDataRek3(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdUser.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[2].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }

        var popup;
        function SelectSup1() {
            popup = window.open("<%=Func.BaseUrl%>Pages/Popup/PopupUser.aspx?tipe=1", "Popup", "width=800,height=500");
            popup.focus();
            return false
        }
        function SelectSup2() {
            popup = window.open("<%=Func.BaseUrl%>Pages/Popup/PopupUser.aspx?tipe=2", "Popup", "width=800,height=500");
            popup.focus();
            return false
        }
        function SelectSup3() {
            popup = window.open("<%=Func.BaseUrl%>Pages/Popup/PopupUser.aspx?tipe=3", "Popup", "width=800,height=500");
            popup.focus();
            return false
        }
        function SelectSup4() {
            popup = window.open("<%=Func.BaseUrl%>Pages/Popup/PopupUser.aspx?tipe=4", "Popup", "width=800,height=500");
            popup.focus();
            return false
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
                <div class="panel-body" id="Tabs" role="tabpanel">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active">
                            <a href="#tab-Company" data-toggle="tab" role="tab">Company Profile
                            </a>
                        </li>
                        <li>
                            <a href="#tab-Transaction" data-toggle="tab" role="tab">Kode Transaksi</a>
                        </li>
                        <li>
                            <a href="#tab-Purchasing" data-toggle="tab" role="tab">Parameter Setting</a>
                        </li>
                        <li>
                            <a href="#tab-Finance" data-toggle="tab" role="tab">Accounting</a>
                        </li>
                        <li>
                            <a href="#tab-Auto" data-toggle="tab" role="tab">Auto Jurnal</a>
                        </li>
                       
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content tab-content-bordered">
                        <div class="tab-pane active" id="tab-Company">
                            <div class="row">
                                <fieldset class="fsStyle">
                                    <legend class="legendStyle"><b>Company Profile</b></legend>
                                    <div class="col-sm-12">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Company Name :</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" class="form-control" ID="txtNama" placeholder="Enter Company Name"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Company Address :</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" class="form-control" ID="txtAlamat" TextMode="MultiLine" type="text" placeholder="Enter Company Address"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Phone / Fax :</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox runat="server" class="form-control phone" ID="txtPhone" Width="200" type="phone" placeholder="Enter Phone"></asp:TextBox>
                                                        <asp:TextBox runat="server" class="form-control phone" ID="txtFax" Width="200" type="fax" placeholder="Enter Fax"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Email</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" class="form-control" ID="txtCompanyEmail" placeholder="Enter Email"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Company Logo</label>
                                                <div class="col-sm-5">
                                                    <asp:FileUpload ID="flUpload" runat="server" Class="fileupload" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <asp:Button runat="server" ID="btnSimpanCompany" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpanCompany_Click"></asp:Button>
                                                <asp:Button runat="server" ID="btnResetCompany" CssClass="btn btn-danger" Text="Reset" OnClick="btnResetCompany_Click"></asp:Button>

                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                        <div class="tab-pane" id="tab-Transaction">
                            <div class="row">
                                <fieldset class="fsStyle">
                                    <legend class="legendStyle"><b>Transaction Code</b></legend>
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdTransaksiKode" DataKeyNames="noSN" SkinID="GridView" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="namaSN" SortExpression="namaSN" HeaderText="Transaction" ItemStyle-Width="70%" ItemStyle-HorizontalAlign="Left" />
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Code">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtKode" runat="server" CssClass="form-control input-sm" Text='<%# Bind("kodeSN") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="text-center">
                                                    <asp:Button runat="server" ID="btnSimpanKode" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpanKode_Click"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                        <div class="tab-pane" id="tab-Purchasing">
                            <div class="row">
                                <fieldset class="fsStyle">
                                    <legend class="legendStyle"><b>Parameter Setting</b></legend>
                                    <div class="col-sm-12">
                                          <div class="col-sm-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                 <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Tahun Ajaran :</label>
                                                    <div class="col-sm-9">
                                                     <asp:TextBox runat="server" class="form-control" ID="txtthnajaran"  placeholder="Tahun Ajaran" Width="100"></asp:TextBox>&nbsp;
                                                     </div>
                                                    </div>
                                                <div class="form-group">
                                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Mulai System :</label>
                                                    <div class="col-sm-9">
                                                         <asp:TextBox runat="server" class="form-control date" ID="dtsistem"></asp:TextBox>
                                                    </div>
                                                </div>
                                                 <div class="form-group">
                                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Kenaikan Gaji Tahunan :</label>
                                                    <div class="col-sm-9">
                                                         <asp:TextBox runat="server" class="form-control" ID="txtgajithn"  Width="50"></asp:TextBox>Tahunan
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Kenaikan Gaji Golongan :</label>
                                                    <div class="col-sm-9">
                                                         <asp:TextBox runat="server" class="form-control" ID="txtgajigol"   Width="50"></asp:TextBox>&nbsp;Tahunan
                                                    </div>
                                                </div>
                                                 <div class="form-group">
                                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Alert Kenaikan Gaji :</label>
                                                    <div class="col-sm-9">
                                                         <asp:TextBox runat="server" class="form-control" ID="txtalertgaji" Width="50"></asp:TextBox>&nbsp;Bulan
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Jurnal Gaji :</label>
                                                   <div class="col-sm-9">
                                                     <asp:DropDownList ID="cbodasarjurnal" runat="server" CssClass="form-control" Width="150">
                                                     <asp:ListItem Value="statuspegawai">Status Pegawai</asp:ListItem>
                                                     <asp:ListItem Value="komponengaji">Komponen Gaji</asp:ListItem>
                                                     </asp:DropDownList>
                                                     </div>
                                                </div>
                                                 <div class="form-group">
                                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Pembayaran Gaji :</label>
                                                    <div class="col-sm-9">
                                                         <asp:DropDownList ID="cbobayargaji" runat="server" CssClass="form-control" Width="150">
                                                         <asp:ListItem Value="yayasan">Yayasan</asp:ListItem>
                                                         <asp:ListItem Value="cabang">Cabang/Perwakilan</asp:ListItem>
                                                         <asp:ListItem Value="unit">Unit</asp:ListItem>
                                                         </asp:DropDownList>
                                                    </div>
                                                </div>
                                                         <div class="form-group">
                                                 <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">System Cash :</label>
                                                     <div class="col-sm-9">
                                                     <asp:DropDownList ID="cbosystemkas" runat="server" CssClass="form-control" Width="150">
                                                     <asp:ListItem Value="Cash">Cash Basis</asp:ListItem>
                                                     <asp:ListItem Value="Akrual">Akrual Basis</asp:ListItem>
                                                     </asp:DropDownList>
                                                     </div>
                                                </div>
                                                <div class="form-group">
                                                 <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">System Budget :</label>
                                                     <div class="col-sm-9">
                                                     <asp:DropDownList ID="cbosystembudget" runat="server" CssClass="form-control" Width="150">
                                                     <asp:ListItem Value="Tahun Ajaran">Tahun Ajaran</asp:ListItem>
                                                     <asp:ListItem Value="Tahun Pajak">Tahun Pajak</asp:ListItem>
                                                     </asp:DropDownList>
                                                     </div>
                                                </div>     
                                        </div>
                                       </div>
                                   <%-- </div>
                                      <div class="col-sm-12">--%>
                                          <div class="col-sm-6">
                                            <div class="form-horizontal">
                                        
                                               <div class="form-group">
                                                 <label for="pjs-ex1-fullname" class="col-sm-6 control-label text-right">Jam Masuk :</label>
                                                    <div class="col-sm-6">
                                                     <asp:TextBox ID="txtjammasuk" runat="server" CssClass="form-control time"  Width="80"></asp:TextBox>
                                                     </div>
                                                    </div>
                                                 <div class="form-group">
                                                    <label for="pjs-ex1-fullname" class="col-sm-6 control-label text-right">Jam Keluar :</label>
                                                    <div class="col-sm-6">
                                                       <asp:TextBox ID="txtjamkeluar" runat="server" CssClass="form-control time"  Width="80"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                 <label for="pjs-ex1-fullname" class="col-sm-6 control-label text-right">Persen Naik Gaji Golongan :</label>
                                                    <div class="col-sm-6">
                                                     <asp:TextBox ID="txtpersengol" runat="server" CssClass="form-control money" value="0.00"  Width="80"></asp:TextBox>
                                                     </div>
                                                    </div>
                                                <div class="form-group">
                                                 <label for="pjs-ex1-fullname" class="col-sm-6 control-label text-right">Persen Naik Gaji Rutin :</label>
                                                      <div class="col-sm-6">
                                                     <asp:TextBox ID="txtpersenrutin" runat="server" CssClass="form-control money" value="0.00"  Width="80"></asp:TextBox>
                                                     </div>
                                                </div>
                                          
                                                <div class="form-group">
                                                 <label for="pjs-ex1-fullname" class="col-sm-6 control-label text-right">Pelunasan :</label>
                                                    <div class="col-sm-6">
                                                     <asp:DropDownList ID="cbopelunasan" runat="server" CssClass="form-control" Width="150">
                                                     <asp:ListItem Value="1">Ada Pelunasan Dibayar Di Muka</asp:ListItem>
                                                     <asp:ListItem Value="0">Tidak Ada Pelunasan Dibayar Di Muka</asp:ListItem>
                                                     </asp:DropDownList>
                                                     </div>
                                                </div>
                                                <div class="form-group">
                                                 <label for="pjs-ex1-fullname" class="col-sm-6 control-label text-right">Akun Piutang Berubah Di Akhir Tahun :</label>
                                                    <div class="col-sm-6">
                                                     <asp:DropDownList ID="cboakunpiutang" runat="server" CssClass="form-control" Width="150">
                                                     <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                                     <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                                     </asp:DropDownList>
                                                     </div>
                                                </div>
                                                 <div class="form-group">
                                                 <label for="pjs-ex1-fullname" class="col-sm-6 control-label text-right">SMTP Host :</label>
                                                      <div class="col-sm-6">
                                                     <asp:TextBox ID="smtp" runat="server" CssClass="form-control  "   Width="150"></asp:TextBox>
                                                     </div>
                                                </div>
                                                 <div class="form-group">
                                                 <label for="pjs-ex1-fullname" class="col-sm-6 control-label text-right">Email Sender :</label>
                                                      <div class="col-sm-6">
                                                     <asp:TextBox ID="emailsender" runat="server" CssClass="form-control "  Width="150"></asp:TextBox>
                                                     </div>
                                                </div>
                                                 <div class="form-group">
                                                 <label for="pjs-ex1-fullname" class="col-sm-6 control-label text-right">Password Sender :</label>
                                                      <div class="col-sm-6">
                                                     <asp:TextBox ID="passwordsender" runat="server" CssClass="form-control" Width="150"></asp:TextBox>
                                                     </div>
                                                </div>
                                            </div>
                                       </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="text-center">
                                                <asp:Button runat="server" ID="btnSimpanPurchasing" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpanPurchasing_Click"></asp:Button>
                                                <asp:Button runat="server" ID="btnResetPurchasing" CssClass="btn btn-danger" Text="Reset" OnClick="btnResetPurchasing_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                        <div class="tab-pane" id="tab-Finance">
                            <div class="row">
                                <fieldset class="fsStyle">
                                    <legend class="legendStyle"><b>Finance & Accounting</b></legend>
                                    <div class="col-sm-12">
                                        <div class="col-sm-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Posisi Keuangan :</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox runat="server" class="form-control" TextMode="MultiLine" ID="txtBalanceSheet" placeholder="Enter Balance Sheet"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Aktivitas :</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox runat="server" class="form-control" TextMode="MultiLine" ID="txtProfitLoss" placeholder="Enter Profit Loss"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                     <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Basis Laporan :</label>
                                                    <div class="col-sm-9">
                                                        <asp:DropDownList ID="cbobasis" runat="server" Width="150">
                                                        <asp:ListItem Value="Akrual">Akrual Basis</asp:ListItem>
                                                        <asp:ListItem Value="Cash">Cash Basis</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <%--<div class="form-group">
                                                <div class="form-inline">
                                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Current Profit and Loss Account :</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox runat="server" class="form-control" Width="400" ID="txtCurrentProfit"></asp:TextBox>
                                                        <asp:HiddenField runat="server" ID="hdnNoRekCurrentProfit" />
                                                        <asp:HiddenField runat="server" ID="hdnJenisRekening" />
                                                        <asp:ImageButton ID="btnImgAddNoRekCurrent" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image" OnClientClick="SelectRekening1()" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Previous Profit and Loss Account :</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox runat="server" class="form-control" Width="400" ID="txtPreviousProfit" type="text"></asp:TextBox>
                                                        <asp:HiddenField runat="server" ID="hdnNoRekPrevious" />
                                                        <asp:ImageButton ID="btnImgAddNoRekPrevious" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image" OnClientClick="SelectRekening2()" />
                                                    </div>
                                                </div>
                                            </div>--%>
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">PPN :</label>
                                                    <div class="col-sm-8">
                                                       <asp:TextBox ID="txtPPN" runat="server" CssClass="form-control money" value="0.00"  Width="80"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                           
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">PPH :</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtPPH" runat="server" CssClass="form-control money" value="0.00"  Width="80"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Beginning Date :</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" class="form-control date" ID="dtBeginningDate"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">
                                                        Signer Invoice 
                                                    <asp:ImageButton ID="btnImgSelectUser" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image" OnClick="btnImgSelectUser_Click" />
                                                        :</label>
                                                    <div class="col-sm-6">
                                                        <asp:Label runat="server" ID="lblSignerInvoice" Text=""></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hdnNoUser" />

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="text-center">
                                                <asp:Button runat="server" ID="btnSimpanFinance" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpanFinance_Click"></asp:Button>
                                                <asp:Button runat="server" ID="btnResetFinance" CssClass="btn btn-danger" Text="Reset" OnClick="btnResetFinance_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                        <div class="tab-pane" id="tab-Auto">
                            <div class="row">
                                <fieldset class="fsStyle">
                                    <legend class="legendStyle"><b>Auto Jurnal</b></legend>
                                    <div class="col-sm-12">
                                        <div class="form-horizontal">
                                            <div class="form-group ">
                                                <div class="form-inline">
                                                    <label class="col-sm-1 control-label">Transaksi</label>
                                                    <div class="col-sm-9">
                                                        <asp:DropDownList ID="cboTransaksi" runat="server" Width="200" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdAutoJurnal" SkinID="GridView" runat="server" OnRowCommand="grdAutoJurnal_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" HeaderText="Jenis">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <asp:DropDownList ID="cboJenis" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" HeaderText="Posisi">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <asp:DropDownList ID="cboPosisi" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="D">Debet</asp:ListItem>
                                                                    <asp:ListItem Value="K">Credit</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="30%" HeaderText="Kode Rekening">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <div class="form-inline">
                                                                    <asp:HiddenField runat="server" ID="hdnNoRek" />
                                                                    <asp:TextBox ID="txtKdRek" Enabled="false" runat="server" CssClass="form-control" Width="200" />
                                                                    <asp:ImageButton ID="btnBrowse" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image " CommandName="Browse" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                                    <asp:ImageButton ID="btnEmpty" runat="server" ImageUrl="~/assets/images/icon_trash.gif" CssClass="btn-image " CommandName="Empty" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" HeaderText="Keterangan">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <asp:Label ID="lblKet" runat="server" />
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" HeaderText="Mata Uang">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <asp:DropDownList ID="cboMataUang" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <asp:Button ID="btnAddRowAJ" runat="server" CssClass="btn btn-success" Text="Add Row" OnClick="btnAddRowAJ_Click" />
                                                <asp:Button runat="server" ID="btnSimpanAJ" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpanAJ_Click"></asp:Button>
                                                <asp:Button ID="btnResetAJ" runat="server" CssClass="btn btn-danger" Text="Reset" OnClick="btnResetAJ_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    <asp:LinkButton ID="LinkButton2" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgAddRekCash" runat="server" PopupControlID="panelAddRekCash" TargetControlID="LinkButton2" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelAddRekCash" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content size-sedang">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Add Account Code</h4>
            </div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="modal-body ">
                <div class="table-responsive">
                    <asp:GridView ID="grdAddRekCash" DataKeyNames="noRek" ShowFooter="true" SkinID="GridView" runat="server" OnRowDataBound="grdAddRekCash_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField runat="server" ID="hdnStsPilih" Value='<%#Eval("stsPilih") %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="kdrek" SortExpression="kdrek" HeaderText="Account Code" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="ket" SortExpression="ket" HeaderText="Description" ItemStyle-Width="15%" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <HeaderTemplate>
                                    <div class="text-center">
                                        <asp:CheckBox ID="chkCheckAll" runat="server" CssClass="px" onclick="return CheckAllData(this);" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="text-center">
                                        <asp:CheckBox ID="chkCheck" runat="server" CssClass="px chkCheck" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnSubmitRekCash" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm" CausesValidation="false" OnClick="btnSubmitNoRek_Click" />
                <asp:Button ID="btnCloseProduk" runat="server" Text="Close" CssClass="btn btn-danger btn-sm" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton3" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgAddRekBank" runat="server" PopupControlID="panelAddRekBank" TargetControlID="LinkButton3" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelAddRekBank" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content size-sedang">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Add Account</h4>
            </div>
            <asp:Label ID="lblPesan" runat="server"></asp:Label>
            <div class="modal-body ">
                <div class="table-responsive">
                    <asp:GridView ID="grdAddBank" DataKeyNames="noRek" ShowFooter="true" SkinID="GridView" runat="server" OnRowDataBound="grdAddBank_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField runat="server" ID="hdnStsPilih" Value='<%#Eval("stsPilih") %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="kdrek" SortExpression="kdrek" HeaderText="Account Code" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="ket" SortExpression="ket" HeaderText="Description" ItemStyle-Width="15%" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <HeaderTemplate>
                                    <div class="text-center">
                                        <asp:CheckBox ID="chkCheckAll" runat="server" CssClass="px" onclick="return CheckAllData(this);" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="text-center">
                                        <asp:CheckBox ID="chkCheck" runat="server" CssClass="px chkCheck" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnSubmitRekBank" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm" CausesValidation="false" OnClick="btnSubmitRekBank_Click" />
                <asp:Button ID="Button2" runat="server" Text="Close" CssClass="btn btn-danger btn-sm" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton4" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgUser" runat="server" PopupControlID="panelUser" TargetControlID="LinkButton4" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelUser" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content size-sedang">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Add User</h4>
            </div>
            <asp:Label ID="lblPesanUser" runat="server"></asp:Label>
            <div class="modal-body ">
                <div class="table-responsive">
                    <asp:GridView ID="grdUser" DataKeyNames="noUser" ShowFooter="true" SkinID="GridView" runat="server" OnRowDataBound="grdUser_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField runat="server" ID="hdnStsPilih" Value='<%#Eval("stsPilih") %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="namauser" SortExpression="namauser" HeaderText="User" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <HeaderTemplate>
                                    <div class="text-center">
                                        <asp:CheckBox ID="chkCheckAll" runat="server" CssClass="px" onclick="return CheckAllData(this);" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="text-center">
                                        <asp:CheckBox ID="chkCheck" runat="server" CssClass="px chkCheck" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnSubmitUser" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm" CausesValidation="false" OnClick="btnSubmitUser_Click" />
                <asp:Button ID="btnCloseUser" runat="server" Text="Close" CssClass="btn btn-danger btn-sm" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton5" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgRekeningAJ" runat="server" PopupControlID="panelRekeningAJ" TargetControlID="LinkButton5" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelRekeningAJ" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Kode Rekening</h4>
                <asp:HiddenField runat="server" ID="hdnRowIndexAJ" />
            </div>
            <asp:Label ID="lblPesanRekAJ" runat="server"></asp:Label>
            <div class="modal-body ">
                <div class="table-responsive">
                    <asp:GridView ID="grdRekAJ" DataKeyNames="noRek" ShowFooter="true" SkinID="GridView" AllowPaging="true" PageSize="10" runat="server" OnPageIndexChanging="grdRekAJ_PageIndexChanging" OnSelectedIndexChanged="grdRekAJ_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="kdrek" SortExpression="kdrek" HeaderText="Kode Rek" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="ket" SortExpression="ket" HeaderText="Keterangan" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <asp:Button ID="btnAdd" runat="server" class="btn btn-primary btn-sm" Text="Add" CommandName="Select" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnCloseRekAJ" runat="server" Text="Close" CssClass="btn btn-danger btn-sm" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="mpe3" runat="server" PopupControlID="panel2" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panel2" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content size-sedang">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Add User</h4>
            </div>
            <asp:Label ID="Label2" runat="server"></asp:Label>
            <div class="modal-body ">
                <div class="row">
                    <div class="col-sm-7">
                        <%--<asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" Text="Delete / Delete all" OnClick="btnDelete_Click" CausesValidation="false" />--%>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group ">
                            <div class="form-inline">
                                <label class="col-sm-3 control-label">Search</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtSearchUser" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    <asp:Button ID="btnCariUser" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnCariUser_Click" CausesValidation="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <asp:GridView ID="grdUserNew" runat="server" DataKeyNames="noUser" SkinID="GridView" AllowPaging="true" PageSize="5" OnPageIndexChanging="grdUserNew_PageIndexChanging"
                        OnSelectedIndexChanged="grdUserNew_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <%# Container.DataItemIndex + 1 %>.
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name Penanda" SortExpression="kodeReagent">
                                <ItemTemplate>
                                    <asp:Label ID="lblInstansi" runat="server" Text='<%# Bind("namauser") %>'></asp:Label>
                                    <asp:HiddenField ID="hdnInstansi" runat="server" Value='<%# Eval("noUser") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ShowHeader="False">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:Button ID="btnSelect" runat="server" CommandName="Select" Text="Add" CssClass="btn btn-primary btn-sm" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnCloseMenu" runat="server" Text="Close" CssClass="btn btn-danger btn-sm" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>
    <script type="text/javascript">
        ///// untuk "tipe" = menunjukan perbedaan pengiriman no index yang dikirim
        ///// untuk "jenisQuery" = perbedaan sql yang akan dipakai pada storeProcedure yang telah dibuat
        var popup;
        function SelectRekening1() {
            popup = window.open("<%=Func.BaseUrl%>Pages/Popup/PopupRekening.aspx?tipe=1&jenisQuery=RLberjalan", "Popup", "width=800,height=450");
            popup.focus();
            return false
        }
        function SelectRekening2() {
            popup = window.open("<%=Func.BaseUrl%>Pages/Popup/PopupRekening.aspx?tipe=2&jenisQuery=RLtahunlalu", "Popup", "width=800,height=450");
            popup.focus();
            return false
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
