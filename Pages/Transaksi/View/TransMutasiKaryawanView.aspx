<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransMutasiKaryawanView.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TransMutasiKaryawanView" %>
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
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-8">
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <label>Filter :</label>
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdPinjaman" DataKeyNames="noMutasiKaryawan" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPinjaman_PageIndexChanging" OnRowCommand="grdPinjaman_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="NO." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="KdMutasi" HeaderText="Kode" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="tglMutasi" HeaderText="Tanggal Mutasi" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}"/>
                                            <asp:BoundField DataField="nama" HeaderText="Nama" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="kategori" HeaderText="Kategori" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="center" />
                                            <%--<asp:BoundField DataField="mutasi" HeaderText="Pergantian Jabatan" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="center" />--%>
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Detail" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" Visible="false" HeaderStyle-CssClass="text-center" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectDel" runat="server" class="btn btn-xs btn-labeled btn-danger" Text="Hapus" CommandName="SelectDelete" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
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
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-5 control-label">Kode Transaksi</label>
                                        <div class="col-sm-2">
                                            <asp:TextBox runat="server"  CssClass="form-control" ID="txtKode" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-5 control-label">Tanggal Pinjaman</label>
                                        <div class="col-sm-2">
                                            <asp:TextBox runat="server" Enabled="false" CssClass="form-control  " ID="dttgl"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-sm-5 control-label">Kategori <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                               <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" Width="230" ID="cboTransaction" AutoPostBack="true" OnSelectedIndexChanged="cboTransaction_SelectedIndexChanged">
                                                <asp:ListItem Value="0">--Pilih Kategori--</asp:ListItem>
                                                <asp:ListItem Value="Mutasi">Mutasi</asp:ListItem>
                                                <asp:ListItem Value="MutasiJabatan">Mutasi Jabatan</asp:ListItem>
                                                <asp:ListItem Value="Promosi">Promosi</asp:ListItem>
                                                <asp:ListItem Value="Keluar">Keluar</asp:ListItem>
                                                <asp:ListItem Value="Pensiun">Pensiun</asp:ListItem>
                                                <asp:ListItem Value="Demosi">Demosi</asp:ListItem>
                                         </asp:DropDownList>
                                        </div>
                                    </div>
                                   
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-5 control-label">Karyawan</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server"  CssClass="form-control" ID="txtnama" Enabled="false" Width="300"></asp:TextBox>
                                        </div>
                                    </div>
                                   <div class="form-group" visible="false" id="formpindah" runat="server">
                                        <label class="col-sm-5 control-label">Pindah Ke Unit</label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="cboCabang" AutoPostBack="false"  Width="230"></asp:DropDownList>&nbsp;&nbsp;
                                           
                                        </div>
                                    </div>
                                     <div class="form-group" id="formjabatan" visible="false" runat="server">
                                        <label class="col-sm-5 control-label">Pergantian Jabatan <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                               <asp:DropDownList Visible="false" runat="server" Enabled="false" CssClass="form-control" Width="230" ID="mutasiK" >
                                                <asp:ListItem Value="0">--Pilih Pergantian Jabatan--</asp:ListItem>
                                                <asp:ListItem Value="Promosi">Promosi</asp:ListItem>
                                                <asp:ListItem Value="Diangkat">Diangkat</asp:ListItem>
                                                <asp:ListItem Value="Demosi">Demosi</asp:ListItem>
                                         </asp:DropDownList>
                                              <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" AutoPostBack="true" Width="230" ID="cbojabatan" >
                                             
                                         </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">No SK</label>
                                        <div class="col-sm-5">
                                             <asp:TextBox runat="server" CssClass="form-control" Enabled="false"  ID="txtNoSK" Width="230"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Diskripsi</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtKeterangan" runat="server" Enabled="false" TextMode="MultiLine" class="form-control" Width="500"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <asp:Button ID="btnSimpan" Visible="false" runat="server" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                                <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Batal" OnClick="btnCancel_Click"></asp:Button>
                                            </div>
                                        </div>
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