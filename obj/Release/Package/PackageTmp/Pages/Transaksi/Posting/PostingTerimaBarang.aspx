<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="PostingTerimaBarang.aspx.cs" Inherits="eFinance.Pages.Transaksi.Posting.PostingTerimaBarang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdReceive.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
          
    </script>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="col-sm-7">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <asp:DropDownList runat="server" CssClass="form-control" Width="200" ID="cboTransaction" AutoPostBack="true" OnSelectedIndexChanged="cboTransaction_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Pilih Jenis Permintaan--</asp:ListItem>
                            <asp:ListItem Value="1">Barang Aset</asp:ListItem>
                            <asp:ListItem Value="3">Jasa</asp:ListItem>
                            <asp:ListItem Value="4">Barang Operasional</asp:ListItem>
                            <asp:ListItem Value="5">Barang Sales</asp:ListItem>
                         </asp:DropDownList>    
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdReceive" DataKeyNames="nosuratjalan" SkinID="GridView" runat="server" AllowPaging="false" OnRowDataBound="grdReceive_RowDataBound" >
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnnoTerima" runat="server" Value='<%#Bind("nosuratjalan") %>' />
                                    <asp:HiddenField ID="hdnjenis" runat="server" Value='<%#Bind("jenis") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                            <HeaderTemplate>
                                <div class="text-center">
                                    <asp:CheckBox ID="chkCheckAll" runat="server" CssClass="px" onclick="return CheckAllData(this);" />
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:CheckBox ID="chkCheck" runat="server" CssClass="px chkCheck" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="tglTerima" HeaderText="Tgl Terima" ItemStyle-Width="7%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                        <asp:BoundField DataField="nosuratjalan" HeaderText="Nomor Surat Jalan" ItemStyle-Width="8%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="left" />
                        <asp:BoundField DataField="KodePO" HeaderText="Kode PO" ItemStyle-Width="8%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="left" />
                        <asp:BoundField DataField="KodePR" HeaderText="Kode PR" ItemStyle-Width="8%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="left" />
                        <asp:BoundField DataField="peminta" HeaderText="Peminta" ItemStyle-Width="12%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="penerima" HeaderText="Penerima" ItemStyle-Width="12%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="namabarang" HeaderText="Nama Barang" ItemStyle-Width="15%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="qtyTerima" HeaderText="Qty Terima" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row text-center" id="button" runat="server">
            <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-primary" Text="Posting" OnClick="btnSubmit_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>


