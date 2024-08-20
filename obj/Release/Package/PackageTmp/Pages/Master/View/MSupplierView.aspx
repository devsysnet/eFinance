<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MSupplierView.aspx.cs" Inherits="eFinance.Pages.Master.View.MSupplierView" %>

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
                                            <label>Filter :</label>
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
                                    <asp:GridView ID="grdBarang" DataKeyNames="nosupplier" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdBarang_PageIndexChanging" OnRowCommand="grdBarang_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="kodesupplier" HeaderText="Kode" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="namasupplier" HeaderText="Nama" HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="alamat" HeaderText="Alamat" HeaderStyle-CssClass="text-center" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="left" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Detail" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
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
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kode Supplier <span class="mandatory">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtKode" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama Supplier <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtnamaSupplier" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">NPWP </label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtnpwp" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Alamat</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtAlamat" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kota</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboKota" runat="server" Enabled="false">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Telp </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txttelpKantor" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Fax </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtFax" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Email </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtemail" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama PIC </label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtnamaPIC" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Telp PIC </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txttelpPIC" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Account </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtnoaccount" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Bank </label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtBank" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterangan 1</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="keterangan" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterangan 2</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="keterangan1" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterangan 3</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="keterangan2" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Batal" OnClick="btnCancel_Click"></asp:Button>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
