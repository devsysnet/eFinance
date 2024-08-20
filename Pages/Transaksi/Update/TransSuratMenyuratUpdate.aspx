<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransSuratMenyuratUpdate.aspx.cs" Inherits="eFinance.Pages.Transaksi.Update.TransSuratMenyuratUpdate" %>
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
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeHolder="Masukan No Surat"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdSuratMenyurat" DataKeyNames="nosurat" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdSuratMenyurat_PageIndexChanging" OnRowCommand="grdSuratMenyurat_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="NO." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="jenis" HeaderText="Jenis" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="nomorSurat" HeaderText="No Surat" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="tglsurat" HeaderText="Tanggal Surat" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="center" />
                                            <asp:BoundField DataField="perihal" HeaderText="Perihal" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="nofile" HeaderText="No File" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="center" />
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
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-5 control-label">Jenis</label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="cboJenis" runat="server" Width="120">
                                                <asp:ListItem Value="Masuk">Masuk</asp:ListItem>
                                                <asp:ListItem Value="Keluar">Keluar</asp:ListItem>
                                            </asp:DropDownList>
                                           
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="col-sm-5 control-label">No Surat</label>
                                         <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNoSurat"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Tanggal Surat<span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="dtDate" runat="server" class="form-control date col-sm-5"></asp:TextBox>
                                        </div>
                                    </div>

                                     <div class="form-group">
                                        <label class="col-sm-5 control-label">Perihal</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtPerihal" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                     <div class="form-group">
                                        <label class="col-sm-5 control-label">No File</label>
                                         <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNoFile"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
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