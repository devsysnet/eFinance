<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="LokasiGudangInsert.aspx.cs" Inherits="eFinance.Pages.Master.Input.LokasiGudangInsert" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdAddProduk.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[6].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }

    </script>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Gudang :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" ID="cboGudang">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama :</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" class="form-control" ID="txtnama" placeholder="Enter Name"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Keterangan</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" class="form-control" ID="txtKet" TextMode="MultiLine" placeholder="Enter Description"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Suhu</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" class="form-control" ID="txtSuhuDari" placeholder="Enter Suhu Dari"></asp:TextBox>
                                </div>
                                <div class="col-sm-3">
                                    <div class="col-sm-1">
                                        -
                                    </div>
                                    <div class="col-sm-10">
                                        <asp:TextBox runat="server" class="form-control" ID="txtSuhuKe" placeholder="Enter Suhu Ke"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Jenis Gudang :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" ID="DropDownList1">
                                        <asp:ListItem Value="1">Baik</asp:ListItem>
                                        <asp:ListItem Value="3">Reject</asp:ListItem>
                                        <asp:ListItem Value="2">Karantina</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <%--   <div class="form-group">
                                <div class="form-inline">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Produk :</label>
                                    <div class="col-sm-8">
                                        <asp:Button runat="server" ID="btnPilihProduk" CssClass="btn btn-primary" Text="Pilih Produk" OnClick="btnPilihProduk_Click"></asp:Button>
                                        <asp:Button runat="server" ID="btnPilihSemuaProduk" CssClass="btn btn-primary" Text="Pilih Semua Produk" OnClick="btnPilihSemuaProduk_Click"></asp:Button>
                                        <asp:Label runat="server" ID="lblJumlahPilih"></asp:Label>
                                        <span>/</span>
                                        <asp:Label runat="server" ID="lblJumlahData"></asp:Label>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="text-center">
                                        <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click"></asp:Button>
                                        <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click"></asp:Button>
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
    <asp:ModalPopupExtender ID="dlgAddProduk" runat="server" PopupControlID="panelAddProduk" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelAddProduk" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content size-sedang">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Add Produk</h4>
            </div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="modal-body ">
                <div class="form-group">
                    <label for="pjs-ex1-fullname" class="col-sm-2 control-label text-right">Brand :</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="cboBrand" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="grdAddProduk" DataKeyNames="noproduct" ShowFooter="true" SkinID="GridView" runat="server" OnRowDataBound="grdAddProduk_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField runat="server" ID="hdnStsPilih" Value='<%#Eval("stsPilih") %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="prodno" SortExpression="prodno" HeaderText="Product Code" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="prodnm" SortExpression="prodnm" HeaderText="Product" ItemStyle-Width="15%" />
                            <asp:BoundField DataField="brand" SortExpression="brand" HeaderText="Brand" ItemStyle-Width="15%" />
                            <asp:BoundField DataField="groups" SortExpression="groups" HeaderText="Groups" ItemStyle-Width="15%" />
                            <asp:BoundField DataField="manufactur" SortExpression="manufactur" HeaderText="Manufactur" ItemStyle-Width="15%" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
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
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnSubmitProduk" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm" CausesValidation="false" OnClick="btnSubmitProduk_Click" />
                <asp:Button ID="btnCloseProduk" runat="server" Text="Close" CssClass="btn btn-danger btn-sm" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
