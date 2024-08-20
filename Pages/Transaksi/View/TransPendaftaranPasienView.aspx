<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransPendaftaranPasienView.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TransPendaftaranPasienView" %>
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
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="col-sm-6">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <asp:TextBox ID="dtMulai" runat="server" CssClass="form-control date"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                    </div>
                </div>
            </div>

            <div class="col-sm-6">
                <div class="form-inline">
                    <div class="col-sm-12">
                        
                        
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdReceiveUpdate" DataKeyNames="noMedik" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdReceiveUpdate_PageIndexChanging"
                    OnRowCommand="grdReceiveUpdate_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="noregister" HeaderText="No Register" ItemStyle-Width="7%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center"  />
                        <asp:BoundField DataField="tglmedik" HeaderText="Tanggal Periksa" ItemStyle-Width="8%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                        <asp:BoundField DataField="namaPasien" HeaderText="Nama Pasien" ItemStyle-Width="12%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="tglLahir" HeaderText="Tanggal Lahir" ItemStyle-Width="12%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                        <asp:BoundField DataField="alamat" HeaderText="Alamat" ItemStyle-Width="15%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                            <ItemTemplate>
                                    <div class="text-center">
                                        <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Detail" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                            
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
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Tanggal Periksa <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:HiddenField ID="hdnNoMedik" runat="server" />
                                            <asp:TextBox ID="dtDate" runat="server" class="form-control date col-sm-5" Enabled="false" ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-5 control-label">Nama Pasien <span class="mandatory">*</span></label>
                                            <div class="col-sm-5">
                                                <asp:HiddenField runat="server" ID="hdnNopasien" />
                                                <div class="input-group-btn">
                                                    <asp:TextBox ID="txtNamaPasien" Enabled="false" runat="server" CssClass="form-control" />
                                                    <asp:ImageButton ID="btnBrowsePasien" runat="server" Enabled="false"  ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image form-control" OnClick="btnBrowsePasien_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Klinik</label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="cboDept" Enabled="false"  CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboLokasi_SelectedIndexChanged">
                                             </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Dokter</label>
                                        <div class="col-sm-2">
                                             <asp:DropDownList ID="cbodokter"  Enabled="false"  runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Keterangan</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtKeterangan" Enabled="false"  runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    

                    <div class="form-group row">
                        <div class="col-md-12">
                            <div class="text-center">
                              <%--  <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Ubah" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>--%>
                                <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click"></asp:Button>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgAddData" runat="server" PopupControlID="panelAddDataPasien" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelAddDataPasien" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data pasien</h4>
            </div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="modal-body col-overflow-400">
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group ">
                            <div class="form-inline">
                                <label class="col-sm-3 control-label">Filter</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtSearchMinta" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    <asp:Button ID="btnMinta" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnMinta_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="grdDataPasien" DataKeyNames="noPasien" ShowFooter="true" SkinID="GridView" runat="server" OnSelectedIndexChanged="grdDataPasien_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField ID="hdnNoUserD" runat="server" Value='<%# Bind("nopasien") %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="noRegister" HeaderText="No Register" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="namaPasien" HeaderText="Nama pasien" ItemStyle-Width="25%" />
                            <asp:BoundField DataField="tgllahir" HeaderText="Tanggal Lahir" ItemStyle-Width="25%" />
                            <asp:BoundField DataField="Alamat" HeaderText="Alamat" ItemStyle-Width="25%" />
                            <asp:BoundField DataField="Alamat" HeaderText="Alamat" ItemStyle-Width="25%" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="" ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-primary btn-sm" Text="Pilih" CommandName="Select" />
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
