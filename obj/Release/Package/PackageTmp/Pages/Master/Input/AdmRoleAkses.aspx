<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="AdmRoleAkses.aspx.cs" Inherits="eFinance.Pages.Master.Input.AdmRoleAkses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnNoMenu" runat="server" Value="99999" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="form-inline">
                                        <label class="col-sm-3 control-label">Search</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 table-responsive overflow-x-table">
                                <asp:GridView ID="grdAkses" DataKeyNames="noAkses" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdAkses_PageIndexChanging" OnRowCommand="grdAkses_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="hakAkses" SortExpression="hakAkses" HeaderText="Hak Akses" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="jenisAkses" SortExpression="jenisAkses" HeaderText="Jenis Akses" ItemStyle-Width="10" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="keteranganAkses" SortExpression="keteranganAkses" HeaderText="Keterangan" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Detail" CommandName="DetailRow" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label text-right">Hak Menu :</label>
                                    <div class="col-sm-5">
                                        <asp:Label ID="lblHakMenu" class="control-label" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label text-right">Keterangan :</label>
                                    <div class="col-sm-5">
                                        <asp:Label ID="lblKeterangan" class="control-label" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="text-center">
                                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-danger btn-sm" Text="Back" OnClick="btnBack_Click" CausesValidation="false" />
                                            <asp:Button ID="btnAddMenu" runat="server" CssClass="btn btn-success btn-sm" Text="Add Menu" OnClick="btnAddMenu_Click" />
                                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Save Changes" OnClick="btnSave_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-6 table-responsive overflow-x-table">
                                <asp:GridView ID="grdMenu" DataKeyNames="noMenu" SkinID="GridView" runat="server" OnRowCommand="grdMenu_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="namaMenu" SortExpression="namaMenu" HeaderText="Menu" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="judulMenu" SortExpression="judulMenu" HeaderText="Title Menu" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="#" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Button ID="btnSelect" runat="server" class="btn btn-primary btn-sm" Text="Select" CommandName="SelectRow" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="#" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Button ID="btnDelete" runat="server" class="btn btn-danger btn-sm" Text="Delete" CommandName="DeleteRow" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-sm-6 table-responsive overflow-x-table">
                                <asp:GridView ID="grdMenuTwo" DataKeyNames="noMenu" SkinID="GridView" runat="server" OnRowDataBound="grdMenuTwo_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="namaMenu" SortExpression="namaMenu" HeaderText="Menu" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="true" />
                                        <asp:BoundField DataField="judulMenu" SortExpression="judulMenu" HeaderText="Title Menu" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="true" />
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnNoAkses" runat="server" Value='<%# Bind("noAkses") %>' />
                                                <asp:CheckBox ID="chkCheck" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="#" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="100%">
                                                        <asp:GridView ID="grdChildMenuTwo" DataKeyNames="noMenu" runat="server" CssClass="ChildGrid" ShowHeader="false" AutoGenerateColumns="false" Width="100%" OnRowDataBound="grdChildMenuTwo_RowDataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="namaMenu" SortExpression="namaMenu" HeaderText="Menu" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="judulMenu" SortExpression="judulMenu" HeaderText="Title Menu" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdnNoAkses" runat="server" Value='<%# Bind("noAkses") %>' />
                                                                        <asp:CheckBox ID="chkCheck" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        </div>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
    <asp:ModalPopupExtender ID="dlgAddMenu" runat="server" PopupControlID="panelAddMenu" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelAddMenu" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
        <div class="modal-content size-sedang">
            <div class="modal-header">
                <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Add Menu</h4>
            </div>
            <div class="modal-body ">
                <div class="table-responsive">
                    <asp:GridView ID="grdAddMenu" DataKeyNames="noMenu" ShowFooter="true" SkinID="GridView" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="namaMenu" SortExpression="namaMenu" HeaderText="Menu" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="judulMenu" SortExpression="judulMenu" HeaderText="Title Menu" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="#">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <asp:CheckBox ID="chkCheck" runat="server" CssClass="px chkCheck" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnSubmitMenu" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm" CausesValidation="false" OnClick="btnSubmitMenu_Click" />
                <asp:Button ID="btnCloseMenu" runat="server" Text="Close" CssClass="btn btn-danger btn-sm" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
