<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstPertanyaan.aspx.cs" Inherits="eFinance.Pages.Master.Input.MstPertanyaan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="form-group row" style="display:none">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Group <span class="mandatory">*</span> :</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="cboGroup" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboGroup_SelectedIndexChanged" runat="server">
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="hdnKelompok" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kode Soal <span class="mandatory">*</span> :</label>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" class="form-control" ID="txtAccountCode" type="text"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Keterangan <span class="mandatory">*</span> :</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" class="form-control"  ID="txtRemark" type="text"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Parent :</label>
                                <div class="form-inline">
                                    <div class="col-sm-5">
                                        <asp:HiddenField runat="server" ID="hdnID" />
                                        <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtParent" type="text"></asp:TextBox>
                                        <asp:ImageButton ID="imgButtonProduct" CssClass="btn-image form-control" runat="server" ImageUrl="~/assets/images/icon_search.png" OnClick="imgButtonProduct_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row" style="display:none">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Posisi <span class="mandatory">*</span>:</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cboPosition" class="form-control" runat="server">
                                        <asp:ListItem Value="0">---Pilih Posisi---</asp:ListItem>
                                        <asp:ListItem Value="1">Debet</asp:ListItem>
                                        <asp:ListItem Value="2">Kredit</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kategori <span class="mandatory">*</span> :</label>
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="cboType" class="form-control" runat="server">
                                        <asp:ListItem Value="0">---Pilih Kategori---</asp:ListItem>
                                        <asp:ListItem Value="1">Header</asp:ListItem>
                                        <asp:ListItem Value="2">Detail</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row" style="display:none">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tipe Transaksi :</label>
                                <div class="col-sm-5">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="cboJnsTipe" Enabled="true" Width="250"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row" style="display:none">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kelompok Arus Kas :</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="cboRekMon" class="form-control" runat="server" >
                                        <asp:ListItem Value="0">---Pilih Kelompok Arus Kas---</asp:ListItem>
                                        <asp:ListItem Value="AKTIVITAS OPERASI">AKTIVITAS OPERASI</asp:ListItem>
                                        <asp:ListItem Value="AKTIVITAS INVESTASI">AKTIVITAS INVESTASI</asp:ListItem>
                                        <asp:ListItem Value="AKTIVITAS PENDANAAN">AKTIVITAS PENDANAAN</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row" style="display:none">
                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Cabang :</label>
                                <div class="col-sm-5">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Enabled="true" Width="250"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-md-12">
                                    <div class="text-center">
                                        <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" ></asp:Button>
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
                <h4 class="modal-title">Soal Data</h4>
            </div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="modal-body ">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group ">
                            <div class="form-inline">
                                <div class="col-sm-8">
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group ">
                                        <div class="form-inline">
                                            <div class="col-sm-12">
                                                <label>Search :</label>
                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search.."></asp:TextBox>
                                                <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnCari_Click" CausesValidation="false" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="grdRekening" DataKeyNames="nosoal" ShowFooter="true" SkinID="GridView" runat="server" AllowPaging="true" PageSize="5" OnPageIndexChanging="grdRekening_PageIndexChanging" OnSelectedIndexChanged="grdRekening_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="kodesoal" SortExpression="Kode Soal" HeaderText="Account Code" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="uraian" SortExpression="Ket" HeaderText="Remark" ItemStyle-Width="20%" />
                            <asp:BoundField DataField="jenis" SortExpression="Jenis" HeaderText="Group" ItemStyle-Width="15%" />
                            <asp:BoundField DataField="tingkat" SortExpression="Tingkat" HeaderText="Group" ItemStyle-Width="10%" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="Select">
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
    </asp:Panel>
</asp:Content>
