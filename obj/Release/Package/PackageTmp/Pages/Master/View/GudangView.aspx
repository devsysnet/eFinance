<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="GudangView.aspx.cs" Inherits="eFinance.Pages.Master.View.GudangView" %>
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
                                    <asp:GridView ID="grdGudang" DataKeyNames="noGudang" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdGudang_PageIndexChanging"
                                        OnSelectedIndexChanged="grdGudang_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="kdGudang" SortExpression="kdGudang" HeaderText="Gudang ID" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="namaGudang" SortExpression="namaGudang" HeaderText="Gudang" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
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
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Gudang :</label>
                                    <div class="col-sm-5">
                                        <asp:Label runat="server" ID="lblKdGudang"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Gudang :</label>
                                    <div class="col-sm-5">
                                        <asp:Label runat="server" ID="lblNamaGudang"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Jenis Gudang :</label>
                                    <div class="col-sm-5">
                                        <asp:Label runat="server" ID="lblJenisGudang">
                                        </asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Keterangan</label>
                                    <div class="col-sm-5">
                                        <asp:Label runat="server" ID="lblKet"></asp:Label>

                                    </div>
                                </div>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
