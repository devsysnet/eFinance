<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="ApprovePR1.aspx.cs" Inherits="eFinance.Pages.Transaksi.Approval.ApprovePR1" %>
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
            var GridView = document.getElementById("<%=grdPRViewDetil.ClientID %>");
            var tipe = document.getElementById("<%=cboTransaction.ClientID %>").value;
            var subTotal = 0;
            for (var i = 1; i < GridView.rows.length; i++) {
                if (GridView.rows[i].cells[0].innerHTML.trim() != "No records data") {
                    if (tipe == "1" || tipe == "4" || tipe == "5")
                    {
                        //var qty = removeMoney(GridView.rows[i].cells[3].getElementsByTagName("INPUT")[0].value) * 1;
                        var harga = removeMoney(GridView.rows[i].cells[8].getElementsByTagName("INPUT")[0].value) * 1;
                        //GridView.rows[i].cells[6].getElementsByTagName("INPUT")[0].value = money(qty * harga)

                        subTotal += (removeMoney(GridView.rows[i].cells[8].getElementsByTagName("INPUT")[0].value) * 1);
                    }
                    else if (tipe == "2")
                    {
                        subTotal += (removeMoney(GridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value) * 1);
                    }
                    else if (tipe == "3")
                    {
                        subTotal += (removeMoney(GridView.rows[i].cells[4].getElementsByTagName("INPUT")[0].value) * 1);
                    }
                    
                }

             }

            document.getElementById("<%=txtSubTotal.ClientID %>").value = money(subTotal);
           }
        
    </script>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnlvlApproveH" runat="server" Value="0" />
    <asp:HiddenField ID="hdnnoCabangPRH" runat="server" Value="0" />
    <asp:HiddenField ID="hdnIdD" runat="server" />
    <div id="tabGrid" runat="server">
        
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdPRView" DataKeyNames="noPR" SkinID="GridView" runat="server" AllowPaging="false" PageSize="10" OnPageIndexChanging="grdPRView_PageIndexChanging"
                    OnSelectedIndexChanged="grdPRView_SelectedIndexChanged" OnRowDataBound="grdPRView_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnlvlApprove" runat="server" Value='<%# Bind("posisiApprove") %>'  />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TglPR" HeaderStyle-CssClass="text-center" HeaderText="Tgl Permintaan" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="KodePR" HeaderStyle-CssClass="text-center" HeaderText="Kode Permintaan" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="peminta" HeaderStyle-CssClass="text-center" HeaderText="Peminta" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="cabang" HeaderStyle-CssClass="text-center" HeaderText="Lokasi Peminta" ItemStyle-Width="17%" ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Level" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="lblApproveUser" runat="server" Text='<%# Bind("approveUser") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Alert" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblAlert" runat="server" Text='<%# Bind("statusApproveX") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="UserInput" HeaderStyle-CssClass="text-center" HeaderText="User Input" ItemStyle-Width="17%" ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Detil" CommandName="Select" />
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
                                        <label class="col-sm-4 control-label">Kode Permintaan </label>
                                        <div class="col-sm-5">
                                            <asp:HiddenField ID="hdnJns" runat="server" />
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtkodePR" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>                                  
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Tanggal Permintaan </label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="dtKas"></asp:TextBox>
                                        </div>
                                    </div>  
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Tanggal Pelaksanaan </label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="dtKas1"></asp:TextBox>
                                        </div>
                                    </div>  
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Jenis Permintaan </label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList runat="server" CssClass="form-control" Width="200" Enabled="false" ID="cboTransaction">
                                                <asp:ListItem Value="1">Barang Aset</asp:ListItem>
                                                <asp:ListItem Value="2">Dana</asp:ListItem>
                                                <asp:ListItem Value="3">Jasa</asp:ListItem>
                                                <asp:ListItem Value="4">Barang Operasional</asp:ListItem>
                                                <asp:ListItem Value="5">Barang Sales</asp:ListItem>
                                             </asp:DropDownList>
                                        </div>
                                    </div>   
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group" id="showKatDana" runat="server">
                                        <label class="col-sm-3 control-label">Kategori Dana </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList runat="server" CssClass="form-control" Width="200" Enabled="false" ID="cboKategoriDana">
                                                <asp:ListItem Value="">--Pilih Kategori Dana--</asp:ListItem>
                                                <asp:ListItem Value="Rutin">Rutin</asp:ListItem>
                                                <asp:ListItem Value="NonRutin">Non Rutin</asp:ListItem>
                                                <asp:ListItem Value="Project">Project</asp:ListItem>
                                                <asp:ListItem Value="Kegiatan">Kegiatan</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" id="showProject" runat="server">
                                        <label class="col-sm-3 control-label">Project </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList runat="server" CssClass="form-control" Enabled="false" Width="300" ID="cboProject">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" id="showKegiatan" runat="server">
                                        <label class="col-sm-3 control-label">Kegiatan </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList runat="server" CssClass="form-control" Enabled="false" Width="300" ID="cboKegiatan">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-sm-3 control-label">Peminta </label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtpeminta" Enabled="false" ></asp:TextBox>
                                        </div>
                                    </div>                               
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Peruntukan untuk </label>
                                        <div class="col-sm-7">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txturaian" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 overflow-x-table">
                                <asp:GridView ID="grdPRViewDetil" SkinID="GridView" runat="server" OnRowDataBound="grdPRViewDetil_RowDataBound" OnSelectedIndexChanged="grdPRViewDetil_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnnoPR" runat="server" />
                                                    <asp:HiddenField ID="hdnnoPRD" runat="server" />
                                                    <asp:HiddenField ID="hdnParameter" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kategori Barang" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="text-center">
                                                            <asp:TextBox runat="server" ID="txtkodebrg" Enabled="false" Width="120" CssClass="form-control"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnAccount" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Barang" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:Label runat="server" ID="lblnamaBarang"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Sisa Budget" ItemStyle-Width="12%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox ID="txtSisaBgt" runat="server" CssClass="form-control money" Enabled="false"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty Besar" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtNilaiBesar" Enabled="false"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label ID="lblsatuanBesar" runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nilai" ItemStyle-Width="12%">
                                            <ItemTemplate>
                                                <div class="text-left">
                                                    <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txtQty" Enabled="false"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtsatuan"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Alokasi Budget" ItemStyle-Width="12%">
                                            <ItemTemplate>
                                                <div class="text-right">
                                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtNilaiD" onchange="Calculate()" ></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Pembuat PO" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cboSelect" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboSelect_SelectedIndexChanged">
                                                        <asp:ListItem Value="">--Pilih Pembuat PO--</asp:ListItem>
                                                        <asp:ListItem Value="Unit">Unit</asp:ListItem>
                                                        <asp:ListItem Value="Perwakilan">Perwakilan</asp:ListItem>
                                                        <asp:ListItem Value="Yayasan">Yayasan</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Keterangan" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtKeterangan"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Button ID="btnDetailD" runat="server" class="btn btn-primary btn-sm" Text="History PR" CommandName="Select" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-12 text-right">
                                <div class="form-group">
                                    <label class="col-sm-10 control-label">Sub Total</label>
                                    <div class="col-sm-2">
                                        <asp:TextBox runat="server" ID="txtSubTotal" CssClass="form-control money" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-12">
                                <div class="text-center">
                                     <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Approve" OnClick="btnSimpan_Click"></asp:Button>
                                    <asp:Button runat="server" ID="btnReject" CssClass="btn btn-success" Text="Reject" OnClick="btnReject_Click"></asp:Button>
                                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click"></asp:Button>
                                 </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgReject" runat="server" PopupControlID="panelReject" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelReject" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Reject</h4>
            </div>
            <div class="modal-body col-overflow-500">
                <div class="row">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <label class="col-sm-2 control-label">Catatan </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtCatatanReject" runat="server" CssClass="form-control" TextMode="MultiLine" Width="600" Height="150" />
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="modal-footer">
                    <asp:Button ID="btnBatal" runat="server" Text="Tutup" CssClass="btn btn-default" OnClick="btnBatal_Click" />
                    <asp:Button ID="btnRejectData" runat="server" Text="Reject" CssClass="btn btn-danger" OnClick="btnRejectData_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton2" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgHistory" runat="server" PopupControlID="panelHistory" TargetControlID="LinkButton2" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelHistory" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Report History Permintaan Barang</h4>
            </div>
            <div class="modal-body col-overflow-400">
                <div class="row">
                    <asp:GridView ID="grdHistoryPR" SkinID="GridView" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TglPR" HeaderStyle-CssClass="text-center" HeaderText="Tgl Permintaan" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="KodePR" HeaderStyle-CssClass="text-center" HeaderText="Kode Permintaan" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="peminta" HeaderStyle-CssClass="text-center" HeaderText="Peminta" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="cabang" HeaderStyle-CssClass="text-center" HeaderText="Lokasi Peminta" ItemStyle-Width="17%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="qty" HeaderStyle-CssClass="text-center" HeaderText="Qty / Nilai" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                     </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
