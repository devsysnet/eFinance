<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransAdjustmentStok.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransAdjustmentStok" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="col-sm-6">
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tanggal :</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox runat="server" class="form-control date" ID="dtAdjustment"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Gudang :</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="cboGudang" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Jenis :</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="cboJenis" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboJenis_SelectedIndexChanged">
                                            <asp:ListItem Value="">---Pilih Jenis---</asp:ListItem>
                                            <asp:ListItem Value="0">Keluar</asp:ListItem>
                                            <asp:ListItem Value="1">Masuk</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row" id="showhideklasifikasiIn" runat="server">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Klasifikasi :</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="cboKlasifikasiIn" runat="server" CssClass="form-control" AutoPostBack="true" >
                                           
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row" id="showhideklasifikasiOut" runat="server">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Klasifikasi :</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="cboKlasifikasiOut" runat="server" CssClass="form-control" AutoPostBack="true">
                                            
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row" id="showhideLain" runat="server">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Ket Lain-lain :</label>
                                    <div class="col-sm-6">
                                        <asp:Textbox runat="server" CssClass="form-control" ID="txtLain"></asp:Textbox>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Uraian :</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtUraian" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive overflow-x-table">
                        <asp:GridView ID="grdAdj" SkinID="GridView" runat="server" AllowPaging="true"  OnRowCommand="grdAdj_RowCommand" OnSelectedIndexChanged="grdAdj_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <%# Container.DataItemIndex + 1 %>.
                                    <asp:HiddenField ID="txtHdnValue" runat="server" Value="<%# Container.DataItemIndex + 1 %>" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kode Barang" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <div class="text-center" style="width:200px">
                                            <div class="col-md-6">
                                                <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtBatch" Width="110"></asp:TextBox>
                                                <asp:HiddenField ID="hdnProduct" runat="server" />
                                                <asp:HiddenField ID="hdnSupCust" runat="server" />
                                                <asp:HiddenField ID="konfersiBesar1" runat="server" />
                                                     <asp:HiddenField ID="konfersiBesar" runat="server" />
                                            </div>
                                            <div class="col-md-6">
                                                <asp:ImageButton ID="imgButtonProduct" CssClass="btn-image" runat="server" ImageUrl="~/assets/images/icon_search.png" CommandName="Select" />
                                                    <asp:ImageButton ID="ImageButtonClear" CssClass="btn-image " runat="server" ImageUrl="~/assets/images/icon_trash.gif" CommandName="Clear" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                            </div>
                                            

                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Barang" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <div class="text-left">
                                            <asp:Label runat="server" CssClass="form-label" ID="txtProductName" Width="120"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" class="form-control money" Text="0.00" ID="txtQtySatuan"  Width="80" onblur="return Calculate()" onkeyup="return Calculate()"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                  <asp:TextBox runat="server" CssClass="form-control" ID="lblUnit" Enabled="false"  Width="100"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty Satuan Besar" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" class="form-control money" Text="0.00" ID="txtQtySatuanBesar"  Width="80" onblur="return Calculate()" onkeyup="return Calculate()"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan Besar" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                  <asp:TextBox runat="server" CssClass="form-control" ID="lblUnitSatuanBesar" Enabled="false"  Width="100"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty Satuan Besar 1" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox runat="server" class="form-control money" Text="0.00" ID="txtQtySatuanBesar1"  Width="80" onblur="return Calculate()" onkeyup="return Calculate()"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Satuan Besar 1" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                  <asp:TextBox runat="server" CssClass="form-control" ID="lblUnitSatuanBesar1" Enabled="false"  Width="100"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
  
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="On Hand" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <asp:Label runat="server" CssClass="form-label" ID="lblonHand" Width="80"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="On Hold" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <asp:Label runat="server" CssClass="form-label" ID="lblonHold" Width="80"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                              <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Adj" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <asp:TextBox runat="server" CssClass="form-control money" ID="txtQty" Width="80" onblur="return Calculate()" onkeyup="return Calculate()"></asp:TextBox>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <asp:Button runat="server" ID="btnAdd" CssClass="btn btn-success" Text="Add Row" OnClick="btnAdd_Click"></asp:Button>
                                <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click"></asp:Button>
                                <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click"></asp:Button>
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
                <h4 class="modal-title">Add Product</h4>
            </div>
            <div class="modal-body ">
                <asp:Label runat="server" ID="lblMessageError"></asp:Label>
                <div class="row">
                    <div class="col-sm-7">
                    </div>
                    <div class="col-sm-5">
                        <div class="form-group ">
                            <div class="form-inline">
                                <div class="col-sm-12">
                                    <label>Search :</label>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnCariProduct_Click" CausesValidation="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:HiddenField ID="txtHdnPopup" runat="server" />
                    <asp:GridView ID="grdProduct" runat="server" SkinID="GridView" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdProduct_PageIndexChanging" OnSelectedIndexChanged="grdProduct_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <%# Container.DataItemIndex + 1 %>.
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Kode Barang">
                                <ItemTemplate>
                                    <asp:Label ID="lblBatchNo" runat="server" Text='<%# Bind("kodeBarang") %>'></asp:Label>
                                    <asp:HiddenField ID="hdnnoProduk" runat="server" Value='<%# Eval("noBarang") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hdnOnHold" runat="server" Value='<%# Eval("sisaSASMT") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hdnOnHand" runat="server" Value='<%# Eval("sisaSA") %>'></asp:HiddenField>
                                     <asp:HiddenField ID="hdnSatuan" runat="server" Value='<%# Eval("satuan") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hdnSatuanBesar" runat="server" Value='<%# Eval("satuanBesar") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hdnSatuanBesar1" runat="server" Value='<%# Eval("satuanBesar1") %>'></asp:HiddenField>
                                                                        <asp:HiddenField ID="hdnkonfersiBesar" runat="server" Value='<%# Eval("konfersibesar") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hdnkonfersiBesar1" runat="server" Value='<%# Eval("konfersi1") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nama Barang">
                                <ItemTemplate>
                                    <asp:Label ID="lblNama" runat="server" Text='<%# Bind("namaBarang") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Stok">
                                <ItemTemplate>
                                    <asp:Label ID="lblSupCus" runat="server" Text='<%# Bind("sisaSASMT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ShowHeader="False">
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:Button ID="btnSelect" runat="server" CommandName="Select" Text="Pilih" CssClass="btn btn-primary btn-sm" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnCloseMenu" runat="server" Text="Close" CssClass="btn btn-danger btn-sm" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
