<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransSuratMenyuratView.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.TransSuratMenyuratView" %>
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
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control date"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdSuratMenyurat" DataKeyNames="nosurat" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" >
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
                                            

                                        </Columns>
                                    </asp:GridView>
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