<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransInvoiceGen.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransInvoiceGen" %>
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
            var tax = document.getElementById("<%=cboTax.ClientID %>").value;
            var pph23= document.getElementById("<%=cbopph.ClientID %>").value;
            var GridView = document.getElementById("<%=grdInvoice.ClientID %>");
            var subTotal = 0;
                for (var i = 1; i < GridView.rows.length; i++) {
                    if (GridView.rows[i].cells[0].innerHTML.trim() != "No records data") {
                        var qty = GridView.rows[i].cells[2].getElementsByTagName("INPUT")[0].value * 1;
                        var harga = removeMoney(GridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value) * 1;
                        if (qty != 0 || harga != 0)
                        {
                            GridView.rows[i].cells[5].getElementsByTagName("INPUT")[0].value = money(qty * harga)
                        }
                        subTotal += (removeMoney(GridView.rows[i].cells[5].getElementsByTagName("INPUT")[0].value) * 1);

                    }
                }
                if (tax == "1") {
                    if (pph23 == "1") {
                        var pph = ((subTotal) * 2 / 100) * 1;
                        var subtotal1 = ((subTotal - pph)) * 1;
                        var ppn1 = ((subTotal - pph) * 11 / 100) * 1;
                        var Netto = (subTotal -pph + ppn1) * 1;
                        document.getElementById("<%=txtAmount.ClientID %>").value = money(subTotal); 
                        document.getElementById("<%=txtpph.ClientID %>").value = money(pph);
                        document.getElementById("<%=txtsubtotal.ClientID %>").value = money(subtotal1);
                        document.getElementById("<%=txtPPN.ClientID %>").value = money(ppn1);
                        document.getElementById("<%=txtTotalAmount.ClientID %>").value = money(Netto * 1);
                    }
                    if (pph23 == "2") {
                        var pph= 0;
                        var ppn1 = ((subTotal - pph) * 11 / 100) * 1;
                        var subtotal1 = ((subTotal - pph)) * 1;
                        var Netto = (subTotal -pph + ppn1) * 1;
                        document.getElementById("<%=txtAmount.ClientID %>").value = money(subTotal); 
                        document.getElementById("<%=txtpph.ClientID %>").value = money(pph);
                        document.getElementById("<%=txtsubtotal.ClientID %>").value = money(subtotal1);
                        document.getElementById("<%=txtPPN.ClientID %>").value = money(ppn1);
                        document.getElementById("<%=txtTotalAmount.ClientID %>").value = money(Netto * 1);
                    }
                }
                else
                {
                    var ppn1 = 0;
                    var pph = 0;
                    var Netto = (subTotal + ppn1) * 1;
                    document.getElementById("<%=txtAmount.ClientID %>").value = money(subTotal);
                    document.getElementById("<%=txtpph.ClientID %>").value = money(pph);
                    document.getElementById("<%=txtsubtotal.ClientID %>").value = money(subtotal1);
                    document.getElementById("<%=txtPPN.ClientID %>").value = money(ppn1);
                    document.getElementById("<%=txtTotalAmount.ClientID %>").value = money(Netto * 1);
                }
                    
        }

        function SelectCus1() {
            popup = window.open("<%=Func.BaseUrl%>Pages/Popup/PopupCustomer.aspx?tipe=8", "Popup", "width=1000,height=500");
            popup.focus();
            return false
        }
    </script>
    <asp:HiddenField ID="hdnPersenPPn" runat="server" />
    <div class="row">
        <div class="panel">
            <div class="panel-body">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-sm-6">
                        <div class="form-horizontal">
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kode :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="cbokode" OnSelectedIndexChanged="cbokode_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1">New</asp:ListItem>
                                        <asp:ListItem Value="2">Old</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="cbolistkodeold">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Invoice Date<span class="mandatory">*</span> :</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" CssClass="form-control date" ID="dtInv"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group row">
                             <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Pelanggan</label>
                                  <div class="col-sm-3">
                                        <asp:HiddenField runat="server" ID="hdnNoCustomer" />
                                           <asp:TextBox ID="txtNamaPelanggan" Enabled="false" TextMode="MultiLine" runat="server" CssClass="form-control col-sm-5" Width="200" />
                                           <asp:ImageButton ID="btnBrowsePelanggan" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image " OnClick="btnBrowsePelanggan_Click" />
                                  </div>
                              </div>
                               <div class="form-group">
                                  <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Alamat Pelanggan</label>
                                       <div class="col-sm-3">
                                           <asp:TextBox ID="txtAlamatPelanggan" TextMode="MultiLine" runat="server" CssClass="form-control" Enabled="false" Width="300" ></asp:TextBox>
                                       </div>
                               </div>
                              
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Sales Name :</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="cboSales" class="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                          </div> 
                            <div class="form-group row" style="display:none">
                               <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Bank</label>
                                  <div class="col-sm-3">
                                        <asp:HiddenField runat="server" ID="hdnNoBank" />
                                           <asp:TextBox ID="txtBankName" Enabled="false"  runat="server" CssClass="form-control col-sm-5" Width="200" />
                                           <asp:ImageButton ID="btnBrowseBank" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image " OnClick="btnBrowseBank_Click" />
                                  </div>
                            </div>
                             <div class="form-group" style="display:none">
                                  <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">No Rek</label>
                                       <div class="col-sm-3">
                                           <asp:TextBox ID="txtNoRek" runat="server" CssClass="form-control" Enabled="false"  ></asp:TextBox>
                                       </div>
                               </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-horizontal">
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">TOP<span class="mandatory">*</span> :</label>
                                <div class="col-sm-2">
                                     <asp:TextBox runat="server" class="form-control" ID="txtTop"></asp:TextBox>
                                </div>
                                Days
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Currency<span class="mandatory">*</span> :</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="cboCurrency" class="form-control" runat="server" OnSelectedIndexChanged="cboCurrency_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:Label ID="Kurstrs" runat="server" CssClass="form-label" Text="kurs"></asp:Label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" class="form-control" ID="txtKurs"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">PO No :</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" class="form-control" ID="txtPOno"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Ref No :</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" class="form-control" ID="txtNoref"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tax Status :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="cboTax" OnSelectedIndexChanged="cboTax_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="2">No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tax No :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="cboTaxNo" OnSelectedIndexChanged="cboTaxNo_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1">New</asp:ListItem>
                                        <asp:ListItem Value="2">Old</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="cbolistkodetaxnew">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="cbolistkodetaxold">
                                    </asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Potong PPH :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="cbopph" OnSelectedIndexChanged="cboTax_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="2">No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Remarks :</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtRemarks" Width="340" Height="65" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="grdInvoice" SkinID="GridView" runat="server" OnRowCommand="grdKasBank_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Akun" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" ID="txtAccount"  CssClass="form-control" Width="120"   AutoPostBack="true"></asp:TextBox>
                                                        <asp:HiddenField ID="hdnAccount" runat="server" />
                                                        <asp:ImageButton ID="btnImgAccount" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image form-control" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                        <asp:ImageButton ID="btnClear" runat="server" ImageUrl="~/assets/images/icon_trash.png" CssClass="btn-image form-control" CommandName="Clear" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Akun" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <div class="text-left">
                                                <asp:Label runat="server" ID="lblDescription"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Product Name" ItemStyle-Width="30%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtProductName"></asp:TextBox>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty" ItemStyle-Width="7%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <asp:TextBox runat="server" CssClass="form-control numeric" ID="txtQty" Text="0" onblur="return Calculate()" onkeyup="return Calculate()"></asp:TextBox>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan" ItemStyle-Width="7%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtSatuan"></asp:TextBox>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Harga" ItemStyle-Width="12%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtHarga" Text="0.00" onblur="return Calculate()" onkeyup="return Calculate()"></asp:TextBox>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Jumlah" ItemStyle-Width="12%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <asp:TextBox runat="server" CssClass="form-control money" ID="txtJumlah" Text="0.00" Enabled="false"></asp:TextBox>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-right">
                        <div class="form-group row">
                            <label for="pjs-ex1-fullname" class="col-sm-10 control-label text-right">Amount :</label>
                            <div class="col-sm-2">
                                <asp:TextBox runat="server" CssClass="form-control money" ID="txtAmount" Text="0.00" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                         <div class="form-group row">
                            <label for="pjs-ex1-fullname" class="col-sm-10 control-label text-right">PPH :</label>
                            <div class="col-sm-2">
                                <asp:TextBox runat="server" CssClass="form-control money" ID="txtpph" Text="0.00" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="pjs-ex1-fullname" class="col-sm-10 control-label text-right">Sub Total :</label>
                            <div class="col-sm-2">
                                <asp:TextBox runat="server" CssClass="form-control money" ID="txtsubtotal" Text="0.00" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="pjs-ex1-fullname" class="col-sm-10 control-label text-right">PPN <asp:Label ID="lblPersenPPn" runat="server" Text='<%# Bind("PersenPPn") %>'></asp:Label>% :</label>
                            <div class="col-sm-2">
                                <asp:TextBox runat="server" CssClass="form-control money" ID="txtPPN" Text="0.00"  Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="pjs-ex1-fullname" class="col-sm-10 control-label text-right">Total :</label>
                            <div class="col-sm-2">
                                <asp:TextBox runat="server" CssClass="form-control money" ID="txtTotalAmount" Text="0.00" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-12">
                        <div class="text-center">
                            <asp:Button runat="server" ID="btnAdd" CssClass="btn btn-success" Text="Add Row" OnClick="btnAdd_Click"></asp:Button>
                            <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click"></asp:Button>
                            <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:LinkButton ID="LinkButton3" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgBank" runat="server" PopupControlID="panelBank" TargetControlID="LinkButton3" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelBank" runat="server" align="center" Width="60%" Style="display: none" CssClass="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Account</h4>
            </div>
            <div class="row">
                <div class="col-sm-7">
                </div>
                <div class="col-sm-5">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" MaxLength="50" placeholder="Masukan kata"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body col-overflow-500">
                <asp:Label runat="server" ID="lblMessageError"></asp:Label>
                <div class="table-responsive">
                    <asp:HiddenField runat="server" ID="hdnParameterProd" />
                    <asp:GridView ID="GridView1" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdBank_PageIndexChanging" OnSelectedIndexChanged="grdBank_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Akun" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblKdRek" Text='<%#Eval("kdRek")%>'></asp:Label>
                                        <asp:HiddenField ID="hdnNoRek" runat="server" Value='<%#Eval("noRek")%>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Akun" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <div class="text-left">
                                        <asp:Label runat="server" ID="lblKet" Text='<%#Eval("Ket")%>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnSelectSub" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnBatal" runat="server" Text="Tutup" CssClass="btn btn-danger" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>
      <asp:LinkButton ID="LinkButton1" runat="server" Style=""></asp:LinkButton>
       <asp:ModalPopupExtender ID="dlgAddData" runat="server" PopupControlID="panelAddDataPelanggan" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
        </asp:ModalPopupExtender>
            <asp:Panel ID="panelAddDataPelanggan" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Add Data Pelanggan</h4>
                    </div>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    <div class="modal-body ">
                        <div class="modal-body col-overflow-400">
                            <div class="table-responsive">
                                <asp:GridView ID="grdDataPelanggan" DataKeyNames="noCust" ShowFooter="true" SkinID="GridView" runat="server" OnSelectedIndexChanged="grdDataPelanggan_SelectedIndexChanged">
                                    <Columns>
                                        <asp:BoundField DataField="noCust" SortExpression="noCust" HeaderText="No." ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="namaCust" SortExpression="namaCust" HeaderText="Nama Pelanggan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="alamatCust" SortExpression="alamatCust" HeaderText="Alamat Pelanggan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Action" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-primary" Text="Select" CausesValidation="false" CommandName="Select"  />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
    <asp:LinkButton ID="LinkButton2" runat="server" Style=""></asp:LinkButton>
       <asp:ModalPopupExtender ID="dlgBank1" runat="server" PopupControlID="panelAddDataBank" TargetControlID="LinkButton2" BackgroundCssClass="modal-background">
        </asp:ModalPopupExtender>
            <asp:Panel ID="panelAddDataBank" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Add Data Bank</h4>
                    </div>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                    <div class="modal-body ">
                        <div class="modal-body col-overflow-400">
                            <div class="table-responsive">
                                <asp:GridView ID="grdBank" DataKeyNames="noBank" ShowFooter="true" SkinID="GridView" runat="server" OnSelectedIndexChanged="grdDataBank_SelectedIndexChanged">
                                    <Columns>
                                        <asp:BoundField DataField="noBank" SortExpression="noBank" HeaderText="No." ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="bankname" SortExpression="bankname" HeaderText="Nama Bank" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="bankcode" SortExpression="bankcode" HeaderText="No Rek" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Action" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-primary" Text="Select" CausesValidation="false" CommandName="Select"  />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

