<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstKolekte.aspx.cs" Inherits="eFinance.Pages.Master.Input.MstKolekte" %>
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

            var GridView = document.getElementById("<%=grdMemoJurnal.ClientID %>");
            var subTotal = 0, subTotal1 = 0;
            for (var i = 1; i < GridView.rows.length; i++) {
                subTotal += (removeMoney(GridView.rows[i].cells[5].getElementsByTagName("INPUT")[0].value) * 1);
                subTotal1 += (removeMoney(GridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value) * 1);
            }
            var ppn1 = ((subTotal) * 10 / 100) * 1;
            var Netto = (subTotal + ppn1) * 1;
            document.getElementById("<%=txtDebitTotal.ClientID %>").value = money(subTotal1);
            document.getElementById("<%=txtKreditTotal.ClientID %>").value = money(subTotal);

        }
    </script>
    <div class="row">
        <div class="col-sm-12">
        <div class="panel">
            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-horizontal">
                            <div class="form-group row" style="display:none">
                                <label for="pjs-ex1-fullname"  class="col-sm-4 control-label text-right">Jenis Transaksi<span class="mandatory">*</span> :</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="cboType" runat="server">
                                        <asp:ListItem Value="Memo Jurnal">Memo Jurnal</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Jenis Kolekte<span class="mandatory">*</span> :</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" Width="300" CssClass="form-control" ID="dtDate"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row"  style="display:none">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Mata Uang :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cboCurrencyTrans" class="form-control" runat="server" OnSelectedIndexChanged="cboCurrencyTrans_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2"  style="display:none">
                                    <asp:Label ID="Kurstrs2" runat="server" CssClass="form-label" Text="kurs IDR"></asp:Label>
                                </div>
                                <div class="col-sm-3"  style="display:none">
                                    <asp:TextBox runat="server" class="form-control money"  ID="txtkursrate"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="table-responsive">
                            <asp:GridView ID="grdMemoJurnal" SkinID="GridView" runat="server" AllowPaging="false" OnSelectedIndexChanged="grdMemoJurnal_SelectedIndexChanged" OnRowCommand="grdMemoJurnal_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <%# Container.DataItemIndex + 1 %>.
                                             <asp:HiddenField ID="txtHdnValue" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Akun" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <div class="form-group">
                                                   <div class="form-inline">
                                                        <div class="text-center">
                                                   
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtAccount"  OnTextChanged="txtAccount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:HiddenField ID="hdnAccount" runat="server" />
                                                    
                                                    <asp:ImageButton ID="imgButtonProduct" CssClass="btn-image form-control " runat="server" ImageUrl="~/assets/images/icon_search.png" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'/>
                                                    <asp:ImageButton ID="btnClear" runat="server" ImageUrl="~/assets/images/icon_trash.png" CssClass="btn-image form-control" CommandName="Clear" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                </div>
                                                   </div>
                                               </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Akun" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtDescription" Enabled="false"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                
                                     <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Jenis" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                     <asp:DropDownList ID="cbojns" runat="server">
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
                                                <asp:TextBox runat="server" class="form-control" ID="txtRemark" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false"  HeaderStyle-CssClass="text-center" HeaderText="Debit" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" class="form-control money" Text="0.00" ID="txtDebit" onblur="return Calculate()" onkeyup="return Calculate()"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false"  HeaderStyle-CssClass="text-center" HeaderText="Kredit" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" class="form-control money" Text="0.00" ID="txtKredit" onblur="return Calculate()" onkeyup="return Calculate()"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="row" style="display:none">
                        <div class="col-md-12 text-right">
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-8 control-label text-right">Total :</label>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtDebitTotal" Text="0.00" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtKreditTotal" Text="0.00" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <asp:Button runat="server" ID="btnAdd" CssClass="btn btn-success" Text="Tambah Baris" OnClick="btnAdd_Click"></asp:Button>
                                <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click"></asp:Button>
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
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Masukan kata"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnCariProduct_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
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
                            <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-Width="1%" HeaderStyle-CssClass="text-center">
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
            <div class="modal-footer">
                <asp:Button ID="btnCloseMenu" runat="server" Text="Close" CssClass="btn btn-danger btn-sm" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
