<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransDO.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransDO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function OpenReport() {
            //            OpenReportViewer();
            window.open('../../../Report/ReportViewer.aspx', 'name', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,modal=yes,width=1000,height=600');
        }
    </script>
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div id="tabGrid" runat="server">
                <div class="row">
                    <div class="col-sm-8">
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group ">
                            <div class="form-inline">
                                <div class="col-sm-12">
                                    <label>Search :</label>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" CausesValidation="false" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="table-responsive">
                            <asp:GridView ID="grdPOLocal" DataKeyNames="noPicking" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPOLocal_PageIndexChanging"
                                OnSelectedIndexChanged="grdPOLocal_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <%# Container.DataItemIndex + 1 %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="kdPicking" HeaderText="Kode Picking" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center" />
                                    <asp:BoundField DataField="tglPicking" HeaderText="Tanggal Picking" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center" DataFormatString="{0:dd-MMM-yyyy}" />
                                    <asp:BoundField DataField="kdCust" HeaderText="Kode Customer" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center" />
                                    <asp:BoundField DataField="namaCust" HeaderText="Nama Customer" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="text-center" />
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
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label class="col-sm-5 control-label">DO Date <span class="mandatory">*</span></label>
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" class="form-control date" ID="dtDO"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="form-inline">
                                        <label class="col-sm-5 control-label">Expedisi Code <span class="mandatory">*</span></label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtKodeSupplier" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:HiddenField ID="hdnNoSupplier" runat="server" />
                                            <asp:ImageButton ID="btnImgSelectSupplier" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image form-control" OnClick="btnImgSelectSupplier_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label">Supplier Name</label>
                                    <div class="col-sm-5">
                                        <asp:Label ID="lblSupplierName" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label">Supplier Address</label>
                                    <div class="col-sm-5">
                                        <asp:Label ID="lblSupplierAddress" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label">No Polisi</label>
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" class="form-control" ID="txtNomorPolisi"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label">Sopir</label>
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" class="form-control" ID="TxtSopir"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                  <div class="form-inline">
                                        <label class="col-sm-5 control-label">Supplier Asuransi</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtNamaSup" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:HiddenField ID="hdnNoSup" runat="server" />
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image form-control" OnClick="ImageButton1_Click" />
                                        </div>
                                    </div>
                                </div>
                                </div>
                            </div>
                             <div class="col-sm-6">
                                <div class="form-group">
                                    <div class="form-inline">
                                        <label class="col-sm-5 control-label">Customer Code <span class="mandatory">*</span></label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:HiddenField ID="hdnNoCust" runat="server" />
                                            <asp:ImageButton ID="btnImgSelectCustomer" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image form-control" OnClick="btnImgSelectCustomer_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label">Customer Name</label>
                                    <div class="col-sm-5">
                                        <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label">Address</label>
                                    <div class="col-sm-5">
                                        <asp:Label ID="lblCustomerAddress" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label">Keterangan</label>
                                    <div class="col-sm-7">
                                        <asp:TextBox runat="server" ID="txtCatatan" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label">Jenis</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="cboJenis" runat="server" OnSelectedIndexChanged="cboJenis_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Baru</asp:ListItem>
                                            <asp:ListItem Value="0">Lama</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="cboKode" runat="server" Visible="false">
                                            <asp:ListItem Value="1">Baru</asp:ListItem>
                                            <asp:ListItem Value="0">Lama</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                 <%--  <div class="form-group">
                                      <div class="col-sm-5">
                                        <asp:CheckBox ID="ckbCoo" runat="server" Text="Cetak Suhu" CssClass="px chkCheck" />
                                    </div>
                                </div>--%>
                                  
                 
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <asp:HiddenField ID="hdnRowIndex" runat="server" />
                                <asp:GridView ID="grdDODetail" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" HeaderText="Nama Product">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProduct" runat="server" Text='<%# Bind("prodnm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" HeaderText="Origin">
                                            <ItemTemplate>
                                                                                <asp:HiddenField runat="server" ID="hdnNoPicking" Value='<%# Eval("noPickingD") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnNoProduct" Value='<%# Eval("noProduct") %>' />
                                                <asp:Label ID="lblProdut" runat="server" Text='<%# Bind("manufactur") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" HeaderText="Qty Satuan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQtyPicking" runat="server" Width="80" Text='<%# Bind("qty") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" HeaderText="Qty Satuan Besar">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQtyPickingSatuanBesar" runat="server" Width="80" Text='<%# Bind("qtySatuanBesar") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" HeaderText="Qty Satuan Besar1">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQtyPickingSatuanBesar1" runat="server" Width="80" Text='<%# Bind("qtySatuanBesar1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" HeaderText="Tgl Picking">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTglPicking" runat="server" Text='<%# Bind("tglPicking", "{0:dd MMM yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 ">
                            <div class="text-center">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:LinkButton ID="LinkButton2" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgSupplier" runat="server" PopupControlID="panelSupplier" TargetControlID="LinkButton2" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelSupplier" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Supplier</h4>
            </div>
            <div class="modal-body col-overflow-400">
                <div class="row">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <label>Search : </label>
                            <asp:TextBox ID="txtSearchSupplier" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:Button ID="btnSearchSupplier" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearchSupplier_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>
                <asp:GridView ID="grdSupplier" SkinID="GridView" DataKeyNames="noSupplier" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdSupplier_PageIndexChanging" OnSelectedIndexChanged="grdSupplier_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="kodeSupplier" SortExpression="kodeSupplier" HeaderText="Kode Supplier" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="namaSupplier" SortExpression="namaSupplier" HeaderText="Nama Supplier" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="alamat" SortExpression="alamat" HeaderText="Alamat Supplier" ItemStyle-Width="25%" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="#" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnSubmit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CommandName="Select" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnBataSupplier" runat="server" Text="Close" CssClass="btn btn-danger" />
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgCustomer" runat="server" PopupControlID="panelCustomer" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelCustomer" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Customer</h4>
            </div>
            <div class="modal-body col-overflow-400">
                <div class="row">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <label>Search : </label>
                            <asp:TextBox ID="txtSearchCustomer" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:Button ID="btnSearchCustomer" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearchCustomer_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>
                <asp:GridView ID="grdCustomer" SkinID="GridView" DataKeyNames="noCust" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdCustomer_PageIndexChanging" OnSelectedIndexChanged="grdCustomer_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="kdCust" SortExpression="kdCust" HeaderText="Kode Customer" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="namaCust" SortExpression="namaCust" HeaderText="Nama Customer" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="alamatCust" SortExpression="alamatCust" HeaderText="Alamat Customer" ItemStyle-Width="25%" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="#" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnSubmit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CommandName="Select" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnCloseCustomer" runat="server" Text="Close" CssClass="btn btn-danger" />
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton3" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgPicking" runat="server" PopupControlID="panelPicking" TargetControlID="LinkButton3" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelPicking" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg-full">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Picking</h4>
            </div>
            <div class="modal-body col-overflow-400">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                <div class="row">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <label>Search : </label>
                            <asp:TextBox ID="txtSearchPicking" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:Button ID="btnSearchPicking" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearchPicking_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>
                <asp:GridView ID="grdPicking" SkinID="GridView" DataKeyNames="noPickingD" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPicking_PageIndexChanging" OnSelectedIndexChanged="grdPicking_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hdnNoProduct" runat="server" Value='<%# Bind("noProduct") %>' />
                                    </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="kdPicking" SortExpression="kdPicking" HeaderText="Kode Picking" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="tglPicking" SortExpression="tglPicking" HeaderText="Tgl Picking" ItemStyle-Width="10%" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="kdSO" SortExpression="kdSO" HeaderText="Kode SO" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="tglSO" SortExpression="tglSO" HeaderText="Tgl SO" ItemStyle-Width="10%" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="prodno" SortExpression="prodno" HeaderText="Kode Product" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="prodnm" SortExpression="prodnm" HeaderText="Nama Product" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="manufactur" SortExpression="manufactur" HeaderText="Manufacture" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="packing" SortExpression="packing" HeaderText="Packing" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="qtyPicking" SortExpression="qtyPicking" HeaderText="Qty Picking" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="sisaqtyPicking" SortExpression="sisaqtyPicking" HeaderText="Sisa Qty Picking" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="prodno" SortExpression="prodno" HeaderText="Kode Product" ItemStyle-Width="15%" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty DO" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQtyDO" runat="server" CssClass="form-control numeric" Width="80" Text="0"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="#" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnSubmit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CommandName="Select" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="modal-footer">
                <asp:Button ID="Button2" runat="server" Text="Close" CssClass="btn btn-danger" />
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="LinkButton4" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgSupplierAs" runat="server" PopupControlID="panel1" TargetControlID="LinkButton4" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panel1" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Data Supplier Asuransi</h4>
            </div>
            <div class="modal-body col-overflow-400">
                <div class="row">
                    <div class="form-inline">
                        <div class="col-sm-12">
                            <label>Search : </label>
                            <asp:TextBox ID="txtSearcAsu" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:Button ID="btnAsu" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnAsu_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>
                <asp:GridView ID="grdSuppAsu" SkinID="GridView" DataKeyNames="noSupplier" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdSuppAsu_PageIndexChanging" OnSelectedIndexChanged="grdSuppAsu_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="kodeSupplier" SortExpression="kodeSupplier" HeaderText="Kode Supplier" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="namaSupplier" SortExpression="namaSupplier" HeaderText="Nama Supplier" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="alamat" SortExpression="alamat" HeaderText="Alamat Supplier" ItemStyle-Width="25%" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="#" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnSubmit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Pilih" CommandName="Select" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="modal-footer">
                <asp:Button ID="Button3" runat="server" Text="Close" CssClass="btn btn-danger" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
