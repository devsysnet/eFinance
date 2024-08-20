<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstProductLocation.aspx.cs" Inherits="eFinance.Pages.Master.Input.MstProductLocation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:HiddenField ID="hdnId" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
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
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" CausesValidation="false" OnClick="btnCari_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="grdVoucher" DataKeyNames="noBarang" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10"
                            OnSelectedIndexChanged="grdVoucher_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <%# Container.DataItemIndex + 1 %>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="kodebarang" HeaderStyle-CssClass="text-center" HeaderText="Kode Product" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="namabarang" HeaderStyle-CssClass="text-center" HeaderText="Nama Product" ItemStyle-Width="25%" />
                                <%--<asp:BoundField DataField="manufactur" HeaderStyle-CssClass="text-center" HeaderText="Manufactur" ItemStyle-Width="30%" />--%>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Input" CommandName="Select" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row" style="margin-top: 10px;">
                            <div class="col-sm-12">
                                <div class="form-horizontal">
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kode Barang :</label>
                                        <asp:HiddenField ID="hdnnobrg" runat="server" />
                                        <%--<asp:HiddenField ID="hdnNolay" runat="server" />
                                        <asp:HiddenField ID="hdnNolayy" runat="server" />--%>
                                        <asp:Label runat="server" class="col-6 col-form-label" ID="lblKodeBarang"></asp:Label>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Barang :</label>
                                        <asp:Label runat="server" class="col-6 col-form-label" ID="lblNamaBarang"></asp:Label>
                                    </div>
                                    <%--<div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Jenis :</label>
                                        <asp:Label runat="server" class="col-6 col-form-label" ID="lblJenis"></asp:Label>
                                        <asp:HiddenField ID="hdnSuhuLink" runat="server" />
                                        
                                    </div>--%>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Minimum Safety Stok :</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtMinStok"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kapasitas :</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtKapasitas"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="grdProdLoc" SkinID="GridView" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <%# Container.DataItemIndex + 1 %>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Gudang" ItemStyle-Width="50%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <asp:DropDownList ID="cboGudang" runat="server" CssClass="form-control" OnSelectedIndexChanged="cboGudang_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Lokasi Gudang" ItemStyle-Width="50%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <asp:HiddenField ID="hdnsuhu" runat="server" Value="0"/>
                                            <asp:HiddenField ID="hdnSuhuKe" runat="server" Value="0"/>
                                            <asp:DropDownList ID="cboLokasiGudang" runat="server" CssClass="form-control" OnSelectedIndexChanged="cboLokasiGudang_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="form-group row">
                            <div class="col-md-12">
                                <div class="text-center">
                                    <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabView" runat="server" visible="false">
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
