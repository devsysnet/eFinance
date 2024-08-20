<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransPencarianUpdate.aspx.cs" Inherits="eFinance.Pages.Transaksi.Update.TransPencarianUpdate" %>
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
                                            <%--<label>Dari :</label>
                                            <asp:TextBox runat="server" CssClass="form-control date" ID="tglFrom"></asp:TextBox>
                                            <label>Sampai :</label>
                                            <asp:TextBox runat="server" CssClass="form-control date" ID="tglTo"></asp:TextBox>--%>

                                            <label>Cari :</label>
                                            <asp:TextBox ID="txtSearchAsset" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:Button ID="btnSearchAsset" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearchAsset_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"  />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdAssetUpdate" DataKeyNames="nodeposito" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdAssetUpdate_PageIndexChanging" OnRowCommand="grdAssetUpdate_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="kdTransaksi" HeaderText="Kode Transaksi" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left"/>
                                            <asp:BoundField DataField="nomor" HeaderText="Nomor Giro" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left"  />
                                            <asp:BoundField DataField="NamaBank" HeaderText="Nama Bank" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left"  />
                                            <asp:BoundField DataField="jenisTransaksi" HeaderText="Jenis Transaksi" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="center"  />
                                            <asp:BoundField DataField="jenis" HeaderText="Kategori Asset" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="center"  />
                                            <asp:BoundField DataField="nominal" HeaderText="Nilai" HeaderStyle-CssClass="text-center" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                            <asp:BoundField DataField="tglDeposito" HeaderText="Tanggal Giro" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="center" DataFormatString="{0:dd-MMM-yyyy}"/>
                                            <asp:BoundField DataField="tglJatuhTempo" HeaderText="Tanggal Jatuh Tempo" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="center" DataFormatString="{0:dd-MMM-yyyy}"/>
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Cair" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectDel" runat="server" class="btn btn-xs btn-labeled btn-danger" Text="Perpanjang" CommandName="SelectDelete" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
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
                             <div class="form-horizontal">
                                 <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Kode Transaksi <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="kdTransaksi"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Rekening Pencairan <span class="mandatory">*</span> </label>
                                <div class="col-sm-3">
                                     <asp:DropDownList runat="server" CssClass="form-control" ID="cboAccount"></asp:DropDownList>
                                </div>
                                
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Tanggal Pencairan<span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" CssClass="form-control date" ID="tglcair"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Jenis Asset Lancar<span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                     <asp:DropDownList runat="server" AutoPostBack="false"  Enabled="false" CssClass="form-control" ID="cbojenissrt">                                          
                                      </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Nomor <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="nomorGiro"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Bank <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="namabank"></asp:TextBox>
                                </div>
                            </div>
              
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Tanggal <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control date" ID="tglGiro"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Tanggal Jatuh Tempo <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control date" ID="tglJatuhTempo"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Nominal <span class="mandatory">*</span></label>
                                <div class="form-inline">
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" Enabled="false" CssClass="form-control money" Text="0" ID="nominal"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                           
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Uraian </label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="deskripsi" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                          <div class="form-group row" style="margin-top:30px">
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
                <div id="tabform1" runat="server" visible="false">
                        <div class="row">
                             <div class="form-horizontal">
                                 <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Kode Transaksi <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="kodetransaksi"></asp:TextBox>
                                </div>
                            </div>
                             
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Nomor <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="nomor"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Bank <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="bank"></asp:TextBox>
                                </div>
                            </div>
                                          <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Tanggal <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control date" ID="tanggal"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Perpanjang Tanggal Jatuh Tempo <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server"  CssClass="form-control date" ID="tanggalJatuhTempo"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Nominal <span class="mandatory">*</span></label>
                                <div class="form-inline">
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" Enabled="false" CssClass="form-control money" Text="0" ID="nominal1"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                           
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Uraian </label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="uraian1" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                          <div class="form-group row" style="margin-top:30px">
                                <div class="col-md-12">
                                    <div class="text-center">
                                        <asp:Button runat="server" ID="Button1" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpanPerpanjang_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                        <asp:Button runat="server" ID="Button2" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
  
    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgAddData" runat="server" PopupControlID="panelAddData" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelAddData" runat="server" align="center" Style="display: none" Width="60%" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Barang Aset</h4>
            </div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="modal-body ">
                <div class="modal-body col-overflow-400">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group ">
                                <div class="form-inline">
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:Button ID="btnAsset" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnAsset_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="grdDataAsset" DataKeyNames="noRek" SkinID="GridView" AllowPaging="true" PageSize="10" runat="server" >
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <%# Container.DataItemIndex + 1 %>
                                            <%--<asp:HiddenField ID="hdnnorek" runat="server" Value='<%# Bind("noRek") %>' />--%>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="kdRek" HeaderStyle-CssClass="text-center" HeaderText="Kode Rekening" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="ket" HeaderStyle-CssClass="text-center" HeaderText="Nama Rekening" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="" ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-primary btn-xs" Text="Pilih" CausesValidation="false" CommandName="Select" />
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
