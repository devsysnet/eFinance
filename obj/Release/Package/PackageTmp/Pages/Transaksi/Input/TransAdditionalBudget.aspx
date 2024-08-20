<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransAdditionalBudget.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransAdditionalBudget" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Akun :</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" class="form-control" ID="txtRekening"></asp:TextBox>
                                    <asp:HiddenField ID="hdnNoRek" runat="server" />

                                </div>
                                <div class="col-sm-1">
                                    <asp:ImageButton ID="imgButtonProduct" class="btn-image" runat="server" ImageUrl="~/assets/images/icon_search.png" OnClick="imgButtonProduct_Click" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tanggal Additional :</label>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" class="form-control date" ID="dtAdd"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nilai :</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" class="form-control money" ID="txtNilai" Text="0.00"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Keterangan :</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" class="form-control" ID="txtRemark" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-md-12">
                                    <div class="text-center">
                                        <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click"></asp:Button>
                                        <asp:Button runat="server" ID="btnResetRek" CssClass="btn btn-danger" Text="Reset" OnClick="btnResetRek_Click"></asp:Button>
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
    <asp:ModalPopupExtender ID="dlgParentAccount" runat="server" PopupControlID="panelParent" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelParent" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content size-sedang">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Account Data</h4>
            </div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="modal-body ">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group ">
                            <div class="form-inline">
                                <div class="col-sm-7">
                                </div>
                                <div class="col-sm-5">
                                    <div class="form-group ">
                                        <div class="form-inline">
                                            <div class="col-sm-12">
                                                <label>Cari :</label>
                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnCari_Click" CausesValidation="false" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="grdRekening" DataKeyNames="noRek" ShowFooter="true" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdRekening_PageIndexChanging" OnSelectedIndexChanged="grdRekening_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="kdRek" HeaderStyle-CssClass="text-center" HeaderText="Akun" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Ket" HeaderStyle-CssClass="text-center" HeaderText="Nama Akun" ItemStyle-Width="20%" />
                            <asp:BoundField DataField="Grup" HeaderStyle-CssClass="text-center" HeaderText="Group" ItemStyle-Width="15%" />
                            <asp:BoundField DataField="Kelompok" HeaderStyle-CssClass="text-center" HeaderText="Kelompok" ItemStyle-Width="20%" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Pilih" CommandName="Select" />
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
