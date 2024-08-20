<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransPelunasanARUpdate1.aspx.cs" Inherits="eFinance.Pages.Transaksi.Update.TransPelunasanARUpdate1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
   
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                             <div class="col-sm-8">
                                <div class="form-group">
                                            <label class="col-sm-2 control-label">Cabang : </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Width="250"></asp:DropDownList>
                                            </div>
                                  </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdMemoJurnalD" DataKeyNames="nomorkode" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdMemoJurnalD_PageIndexChanging"
                                        OnSelectedIndexChanged="grdMemoJurnalD_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nomorkode" HeaderStyle-CssClass="text-center" HeaderText="Kode" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Cust" HeaderStyle-CssClass="text-center" HeaderText="Jenis Transaksi" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Tgl" HeaderStyle-CssClass="text-center" HeaderText="Tanggal" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
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
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-horizontal">
                                        <div class="form-group row">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kode<span class="mandatory">*</span> :</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtKode"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Bank<span class="mandatory">*</span> :</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="cboAccount" Width="300"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tanggal<span class="mandatory">*</span> :</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox runat="server" CssClass="form-control date" ID="dtDate"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                          <%--  <div class="table-responsive">
                                <asp:GridView ID="grdMemoJurnal" SkinID="GridView" runat="server" AllowPaging="false" OnSelectedIndexChanged="grdMemoJurnal_SelectedIndexChanged" OnRowCommand="grdMemoJurnal_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>.
                                                        <asp:HiddenField ID="txtHdnValue" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Akun" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                   <div class="form-inline">
                                                        <div class="text-center">
                                                   
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtAccount"></asp:TextBox>
                                                        <asp:HiddenField ID="hdnAccount" runat="server" />
                                                    
                                                    <asp:ImageButton ID="imgButtonProduct" CssClass="btn-image form-control " runat="server" ImageUrl="~/assets/images/icon_search.png" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'/>
                                                    <asp:ImageButton ID="btnClear" runat="server" ImageUrl="~/assets/images/icon_trash.png" CssClass="btn-image form-control" CommandName="Clear" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                </div>
                                                   </div>
                                               </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Akun" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtDescription"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Keterangan" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtRemark" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Debit" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txtDebit" onblur="return Calculate()" onkeyup="return Calculate()"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kredit" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txtKredit" onblur="return Calculate()" onkeyup="return Calculate()"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnNoMemo" runat="server" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="row">
                                <div class="col-md-12 text-right">
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-8 control-label text-right">Total :</label>
                                        <div class="col-sm-2">
                                            <asp:TextBox runat="server" CssClass="form-control money" ID="txtDebitTotal" Text="0.00" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox runat="server" CssClass="form-control money" ID="txtKreditTotal" Text="0.00" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="form-group row">
                                <div class="col-md-12">
                                    <div class="text-center">
                                        <%--<asp:Button ID="btnAddrow" runat="server" CssClass="btn btn-success" Text="Tambah Baris" CausesValidation="false" OnClick="btnAddrow_Click" />--%>
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Ubah" CausesValidation="false" OnClick="btnSubmit_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                        <%--<asp:Button ID="btnHapus" runat="server" CssClass="btn btn-danger" Text="Hapus" CausesValidation="false" OnClick="btnHapus_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />--%>
                                        <%--<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Batal" CausesValidation="false" OnClick="btnCancel_Click" />--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:LinkButton ID="LinkButton2" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="panelAddMenu" TargetControlID="LinkButton2" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelAddMenu" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content size-sedang">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Akun</h4>
            </div>
            <div class="modal-body ">
                <div class="row">
                    <div class="col-sm-7">
                    </div>
                    <div class="col-sm-5">
                        <div class="form-group ">
                            <div class="form-inline">
                                <div class="col-sm-12">
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Masukan kata"></asp:TextBox>
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnCariProduct_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
              <%--  <div class="table-responsive">
                    <asp:HiddenField ID="txtHdnPopup" runat="server" />
                    <asp:GridView ID="grdProduct" runat="server" SkinID="GridView" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdProduct_PageIndexChanging" OnSelectedIndexChanged="grdProduct_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="No" HeaderStyle-Width="3%" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <%# Container.DataItemIndex + 1 %>.
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Akun" HeaderStyle-Width="10%" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblKodeReagent" runat="server" Text='<%# Bind("kdRek") %>'></asp:Label>
                                    <asp:HiddenField ID="hidNoReagent" runat="server" Value='<%# Eval("noRek") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nama Akun" HeaderStyle-Width="30%" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblNamaReagent" runat="server" Text='<%# Bind("ket") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-Width="1%">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:Button ID="btnSelect" runat="server" CommandName="Select" Text="Pilih" CssClass="btn btn-primary btn-sm" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>--%>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

