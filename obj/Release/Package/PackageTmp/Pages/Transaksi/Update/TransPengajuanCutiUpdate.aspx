<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransPengajuanCutiUpdate.aspx.cs" Inherits="eFinance.Pages.Transaksi.Update.TransPengajuanCutiUpdate" %>
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
                                    <asp:GridView ID="grdBarang" DataKeyNames="nocuti" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdBarang_PageIndexChanging" OnRowCommand="grdBarang_RowCommand" >
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="NO." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="kodeCuti" HeaderText="Kode" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="tglpengajuan" HeaderText="Tanggal Pengajuan" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="nama" HeaderText="Nama" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="saldo" HeaderText="Sisa Cuti" HeaderStyle-CssClass="text-center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="tglmulaicuti" HeaderText="Dari Tanggal" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="tglselesaicuti" HeaderText="Sampai Tanggal" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="uraian" HeaderText="Uraian" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left" />
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
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kode</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtKode" Enabled="false" Width="100"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal Pengajuan</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox runat="server" CssClass="form-control date" ID="dttgl" placeholder="Tanggal Pinjam"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtnama" Enabled="false"  Width="200"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Sisa Cuti</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtsaldo" Enabled="false" Width="50"></asp:TextBox>
                                            </div>
                                        </div> 
                                         <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Dari Tanggal</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox runat="server" CssClass="form-control date" ID="dttgldr" placeholder="Tanggal Pinjam"></asp:TextBox>
                                            </div>
                                        </div>
                                         <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Sampai Tanggal</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox runat="server" CssClass="form-control date" ID="dttglsd" placeholder="Tanggal Pinjam"></asp:TextBox>
                                            </div>
                                        </div>
                                         <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Uraian</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox runat="server" CssClass="form-control" type="text" ID="txtUraian" placeholder="Uraian" TextMode="MultiLine" Width="300"></asp:TextBox>
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