<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="mstKolekteView.aspx.cs" Inherits="eFinance.Pages.Master.View.mstKolekteView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function money(money) {
            var num = money;
            if (num != "") {
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
        function moneyCustom(money) {
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
                            money = array.join('') + '.' + array2[1].substring(0, 3) + '0';
                        } else {
                            money = array.join('') + '.' + array2[1].substring(0, 3);
                        }
                    }
                }
            } else {
                money = '0.000';
            }
            return money;
        }
        function Calculate() {

            var GridView = document.getElementById("<%=grdMemoJurnal.ClientID %>");
            var debit = 0, kredit = 0, hasilDebit = 0, hasilKredit = 0;
            for (var i = 1; i < GridView.rows.length; i++) {
                kredit = (removeMoney(GridView.rows[i].cells[5].getElementsByTagName("INPUT")[0].value) * 1);
                debit = (removeMoney(GridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value) * 1);
                if (debit != 0)
                    hasilDebit += debit;
                if (kredit != 0)
                    hasilKredit += kredit;
            }
      

            document.getElementById("BodyContent_txtDebitTotal").value = moneyCustom(hasilDebit);
            document.getElementById("BodyContent_txtKreditTotal").value = moneyCustom(hasilKredit);

        }
         function DeleteAll() {
            bootbox.confirm({
                message: "Are you sure to delete data?",
                callback: function (result) {
                    if (result == true) {
                        document.getElementById('<%=hdnMode.ClientID%>').value = "deleteall";
                        eval("<%=execBind%>");
                    }
                },
                className: "bootbox-sm"
            });
         return false;
        }
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdMemoJurnalD.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
                            <asp:Button Visible="false" ID="cmdMode" runat="server" OnClick="cmdMode_Click" Style="display: none;" Text="Mode" />

    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-8">
                                <div class="form-group" style="display:none">
                                            <label class="col-sm-2 control-label">Cabang : </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Width="250"></asp:DropDownList>
                                            </div>
                                  </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-sm-8">
                                <asp:Button  Visible="false"  ID="btnDelete" runat="server" CssClass="btn btn-danger" OnClientClick="return DeleteAll()" Text="Delete all selected" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdMemoJurnalD" DataKeyNames="nokolekte" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdMemoJurnalD_PageIndexChanging"
                                        OnSelectedIndexChanged="grdMemoJurnalD_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField  Visible="false"  HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
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
                                            <asp:BoundField DataField="jeniskolekte" HeaderStyle-CssClass="text-center" HeaderText="Jenis Kolekte" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left" />
                                             <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Detail" CommandName="Select" />
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
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-horizontal">
                                        <div class="form-group row">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Jenis Kolekte<span class="mandatory">*</span> :</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" Enabled="false" CssClass="form-control"  ID="txtKode"></asp:TextBox>
    <asp:HiddenField ID="hdnkolekte" runat="server" />

                                            </div>
                                        </div>
                                        <div class="form-group row" style="display:none">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Jenis Transaksi<span class="mandatory">*</span> :</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="cboType" runat="server" Enabled="false">
                                                    <asp:ListItem Value="0">---Pilih Jenis Transaksi---</asp:ListItem>
                                                    <asp:ListItem Value="Memo Jurnal">Memo Jurnal</asp:ListItem>
                                                    <asp:ListItem Value="Kas/Bank Keluar">Kas/Bank Keluar</asp:ListItem>
                                                    <asp:ListItem Value="Kas/Bank Masuk">Kas/Bank Masuk</asp:ListItem>
                                                    <asp:ListItem Value="Pembayaran Siswa">Pembayaran Siswa</asp:ListItem>
                                                  </asp:DropDownList>
                                            </div>
                                        </div>
                                         <div class="form-group row" style="display:none">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Cabang<span class="mandatory">*</span> :</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="cabang"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row" style="display:none">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tanggal<span class="mandatory">*</span> :</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox runat="server" CssClass="form-control date" ID="dtDate"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row" style="display:none">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Mata Uang :</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cboCurrencyTrans" class="form-control" runat="server" OnSelectedIndexChanged="cboCurrencyTrans_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2" style="display:none">
                                                <asp:Label ID="Kurstrs2" runat="server" CssClass="form-label" Text="kurs IDR"></asp:Label>
                                            </div>
                                            <div class="col-sm-3" style="display:none">
                                                <asp:TextBox runat="server" class="form-control money"  ID="txtkursrate"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="grdMemoJurnal" SkinID="GridView" runat="server" AllowPaging="false"  OnRowCommand="grdMemoJurnal_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>.
                                                        <asp:HiddenField ID="txtHdnValue" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Akun" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                               <div class="form-group">
                                                   <div class="form-inline">
                                                        <div class="text-center">
                                                   
                                                        <asp:TextBox Enabled="false"  runat="server" CssClass="form-control" ID="txtAccount" OnTextChanged="txtAccount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:HiddenField ID="hdnAccount" runat="server" />
                                                    
                                                    <asp:ImageButton Visible="false" ID="imgButtonProduct" CssClass="btn-image form-control " runat="server" ImageUrl="~/assets/images/icon_search.png" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'/>
                                                    <asp:ImageButton Visible="false" ID="btnClear" runat="server" ImageUrl="~/assets/images/icon_trash.png" CssClass="btn-image form-control" CommandName="Clear" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                </div>
                                                   </div>
                                               </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Akun" ItemStyle-Width="75%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox Enabled="false" runat="server" CssClass="form-control" ID="txtDescription"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                    
                                     <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Jenis" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                     <asp:DropDownList Enabled="false"  ID="cbojns" runat="server">
                                                        <asp:ListItem Value="1">Penerimaan</asp:ListItem>
                                                        <asp:ListItem Value="2">Pengeluaran</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>


                                                    </asp:DropDownList>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField Visible="false" HeaderStyle-CssClass="text-center" HeaderText="Keterangan" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtRemark" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField  Visible="false" HeaderStyle-CssClass="text-center" HeaderText="Debit" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" CssClass="form-control" Text="0.00" ID="txtDebit" onKeyDown="Calculate()" onkeyup="Calculate()"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField  Visible="false" HeaderStyle-CssClass="text-center" HeaderText="Kredit" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" CssClass="form-control " Text="0.00" ID="txtKredit" onKeyDown="Calculate()" onkeyup="Calculate()"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnNoMemo" runat="server" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="row" style="display:none">
                                <div class="col-md-12 text-right">
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-8 control-label text-right">Total :</label>
                                        <div class="col-sm-2">
                                            <asp:TextBox runat="server" CssClass="form-control money" Enabled="false" ID="txtDebitTotal" Text="0.00"  onKeyUp="return Calculate(event);" onKeyDown="return Calculate(event);"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox runat="server" CssClass="form-control money" Enabled="false" ID="txtKreditTotal" Text="0.00"  onKeyUp="return Calculate(event);" onKeyDown="return Calculate(event);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-md-12">
                                    <div class="text-center">
                                        <asp:Button ID="btnAddrow" Visible="false" runat="server" CssClass="btn btn-success" Text="Tambah Baris" CausesValidation="false" OnClick="btnAddrow_Click" />
                                        <asp:Button ID="btnSubmit" Visible="false" runat="server" CssClass="btn btn-primary" Text="Ubah" CausesValidation="false" OnClick="btnSubmit_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Batal" CausesValidation="false" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:LinkButton ID="LinkButton2" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="panelAddMenu" TargetControlID="LinkButton2" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelAddMenu" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content size-sedang">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Akun</h4>
            </div>
            <div class="modal-body ">
                <div class="row">
                    <div class="col-sm-7">
                    </div>
                    <div class="col-sm-5">
                        <div class="form-group ">
                            <div class="form-inline">
                                <div class="col-sm-12">
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Masukan kata"></asp:TextBox>
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnCariProduct_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:HiddenField ID="txtHdnPopup" runat="server" />
                    <asp:GridView ID="grdProduct" runat="server" SkinID="GridView" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdProduct_PageIndexChanging" OnSelectedIndexChanged="grdProduct_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="No" HeaderStyle-Width="3%" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <%# Container.DataItemIndex + 1 %>.
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Akun" HeaderStyle-Width="10%" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblKodeReagent" runat="server" Text='<%# Bind("kdRek") %>'></asp:Label>
                                    <asp:HiddenField ID="hidNoReagent" runat="server" Value='<%# Eval("noRek") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nama Akun" HeaderStyle-Width="30%" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblNamaReagent" runat="server" Text='<%# Bind("ket") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-Width="1%">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:Button ID="btnSelect" runat="server" CommandName="Select" Text="Pilih" CssClass="btn btn-primary btn-sm" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

