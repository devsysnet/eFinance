<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransArSiswaupdate.aspx.cs" Inherits="eFinance.Pages.Transaksi.Update.TransArSiswaupdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdKelasSiswa.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
            Calculate();
        }
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
            var total = 0;
            var gridView = document.getElementById("<%=grdKelasSiswa.ClientID %>");

            for (var i = 1; i < gridView.rows.length; i++) {
                bayar = removeMoney(gridView.rows[i].cells[9].getElementsByTagName("INPUT")[0].value) * 1;

                if (gridView.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked == true)
                    if (bayar != 0)
                        total += bayar;
            }
            if (total != 0)
                document.getElementById("BodyContent_txtTotal").value = money(total);
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
                            <div class="col-sm-12">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <label>Filter :</label>
                                            <asp:DropDownList ID="cboTahunAjaran" runat="server" CssClass="form-control" Width="170"></asp:DropDownList>
                                            <asp:DropDownList ID="cboJnsTrans" runat="server" CssClass="form-control" Width="250"></asp:DropDownList>
                                            <asp:DropDownList ID="cboKelas" runat="server" CssClass="form-control" Width="130"></asp:DropDownList>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdKelasSiswa" DataKeyNames="noSiswa" SkinID="GridView" runat="server" AllowPaging="false" PageSize="100" OnPageIndexChanging="grdKelasSiswa_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:HiddenField ID="hdnNoSiswa" runat="server" Value='<%# Bind("nopiutang") %>' />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nisn" HeaderText="NISN" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="nis" HeaderText="NIS" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="namasiswa" HeaderText="Nama Siswa" HeaderStyle-CssClass="text-center" ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Left" />
                                           
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
