<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TranskenaikanGajiberkalaupd.aspx.cs" Inherits="eFinance.Pages.Transaksi.Update.TranskenaikanGajiberkalaupd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
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
                                            <label>Search :</label>
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdBarang" DataKeyNames="noNaikkala" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdBarang_PageIndexChanging" OnRowCommand="grdBarang_RowCommand" >
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="NO." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="kdtran" HeaderText="Kode" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="tgl" HeaderText="Tanggal Transaksi" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="nama" HeaderText="Nama" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="jns" HeaderText="Jenis Kenaikan" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="nilainaik" HeaderText="Nilai Kenaikan" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Ubah" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="5%">
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
                    <div id="tabForm" runat="server">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-12">
                                    <div class="panel">
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kode Transaksi</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtKode" type="text" placeholder="Kode Transaksi" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtnama" type="text" placeholder="Nama" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                         <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal Transakasi</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox runat="server" CssClass="form-control date" ID="dttgl" placeholder="Tanggal Pinjam"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kenaikan Bulan</label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="cboMonth" runat="server" Width="120">
                                                <asp:ListItem Value="1">Januari</asp:ListItem>
                                                <asp:ListItem Value="2">Februari</asp:ListItem>
                                                <asp:ListItem Value="3">Maret</asp:ListItem>
                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                <asp:ListItem Value="5">Mei</asp:ListItem>
                                                <asp:ListItem Value="6">Juni</asp:ListItem>
                                                <asp:ListItem Value="7">Juli</asp:ListItem>
                                                <asp:ListItem Value="8">Agustus</asp:ListItem>
                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                <asp:ListItem Value="10">Oktober</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">Desember</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="cboYear" runat="server" Width="100">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Kenaikan Gaji</label>
                                            <div class="col-sm-2">
                                               <asp:DropDownList ID="cboStatus" runat="server" CssClass="form-control"  Width="150">
                                               <asp:ListItem Value="-">----</asp:ListItem>
                                                <asp:ListItem Value="1">Nilai</asp:ListItem>
                                                <asp:ListItem Value="2">Persented</asp:ListItem>
                                             </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nilai</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox runat="server" CssClass="form-control money" ID="txtPinjaman" type="text" placeholder="Nilai Pinjam"></asp:TextBox>
                                            </div>
                                        </div>                                                                                    
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="text-center">
                                                    <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click" />
                                                    <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Back" OnClick="btnCancel_Click"></asp:Button>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

