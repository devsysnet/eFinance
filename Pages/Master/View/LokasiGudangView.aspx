<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="LokasiGudangView.aspx.cs" Inherits="eFinance.Pages.Master.View.LokasiGudangView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" ></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdLokGudang" DataKeyNames="noLokasiGudang" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdLokGudang_PageIndexChanging"
                                        OnSelectedIndexChanged="grdLokGudang_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="kdLokGud" SortExpression="kdLokGud" HeaderText="Lokasi Gudang ID" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="namaLokGud" SortExpression="namaLokGud" HeaderText="Lokasi Gudang" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Detail" CommandName="Select" />
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
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Gudang :</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList runat="server" ID="cboGudang" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama :</label>
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" CssClass="form-control"  Enabled="false" ID="txtnama" placeholder="Enter Name"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Keterangan</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" CssClass="form-control" Width="300" Enabled="false" ID="txtKet" TextMode="MultiLine" placeholder="Enter Description"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Suhu</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtSuhuDari" placeholder="Enter Suhu Dari"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="col-sm-1">
                                            -
                                        </div>
                                        <div class="col-sm-10">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtSuhuKe" placeholder="Enter Suhu Ke"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                              <%--  <div class="form-group">
                                    <div class="form-inline">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Produk :</label>
                                        <div class="col-sm-8">
                                            <asp:Button runat="server" ID="btnPilihProduk" CssClass="btn btn-primary" Text="Check Produk" OnClick="btnPilihProduk_Click"></asp:Button>
                                            <asp:Label runat="server" ID="lblJumlahPilih" Text="0"></asp:Label>
                                            <span>/</span>
                                            <asp:Label runat="server" ID="lblJumlahData"></asp:Label>
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="text-center">
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
                                <ItemTemplate>
                                    <div class="text-center">
                                        <asp:CheckBox ID="chkCheck" Enabled="false" runat="server" CssClass="px chkCheck" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnCloseProduk" runat="server" Text="Close" CssClass="btn btn-danger btn-sm" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
