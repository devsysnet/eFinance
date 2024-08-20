<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MJenisTrsView.aspx.cs" Inherits="eFinance.Pages.Master.View.MJenisTrsView" %>

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
                                <asp:DropDownList ID="cboCabang" runat="server" CssClass="form-control" Width="350" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Masukan kata"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdBarang" DataKeyNames="noTransaksi" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdBarang_PageIndexChanging" OnRowCommand="grdBarang_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="jenisTransaksi" HeaderText="Jenis Transaksi" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="kategori" HeaderText="Kategori" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left" />
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
                    <div id="tabForm" runat="server">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Transaksi</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control " ID="txjnstrs" placeholder="Jenis Transaksi" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kategori </label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control " ID="txtnkategori" placeholder="Kategori" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">COA Debet</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control " ID="txtcoadebet" placeholder="COA Debet" Enabled="false" ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">COA Kredit</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control " ID="txtcoakredit" placeholder="COA Kredit" Enabled="false" ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kode VA</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control " ID="kodeva" placeholder="KodeVA" Enabled="false" ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Bank</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control " ID="Textbank" placeholder="KodeVA" Enabled="false" ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis kegiatan</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control " ID="Textkegiatan" placeholder="-" Enabled="false" ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Denda</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control " ID="txtdenda" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Urutan</label>
                                        <div class="col-sm-2">
                                            <asp:TextBox runat="server" CssClass="form-control int" ID="txtUrutan" Enabled="false"  Width="100"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Transaksi Bank</label>
                                        <div class="col-sm-2">
                                            <asp:TextBox runat="server" CssClass="form-control int" ID="closeopen" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">No Index MHS</label>
                                        <div class="col-sm-2">
                                            <asp:TextBox runat="server" CssClass="form-control int" ID="nomhs" Enabled="false" Width="100"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Kembali" OnClick="btnCancel_Click"></asp:Button>
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

