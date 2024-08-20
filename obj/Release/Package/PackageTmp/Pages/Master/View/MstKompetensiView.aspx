<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstKompetensiView.aspx.cs" Inherits="eFinance.Pages.Master.View.MstKompetensiView" %>
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
                                <div class="form-inline">
                                    <div class="col-sm-12">
                                        <label>Search : </label>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" MaxLength="50" PlaceHolder="Jenis Kompetensi"></asp:TextBox>
                                        <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnCari_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdKompetensi" DataKeyNames="noKomptenesi" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdKompetensi_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:BoundField DataField="kompetensi" SortExpression="kompetensi" HeaderText="Jenis Kompetensi" ItemStyle-Width="30%" />
                                            <asp:BoundField DataField="sts" SortExpression="sts" HeaderText="STATUS" ItemStyle-Width="25%" />
                                           

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--END GRID--%>
                    <%--START FORM--%>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Jenis Kompetensi <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtNamaArea" runat="server" CssClass="form-control" MaxLength="50" placeholder="Nama Area"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Status <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cboStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="">-------</asp:ListItem>
                                        <asp:ListItem Value="1">Aktif</asp:ListItem>
                                        <asp:ListItem Value="0">Tidak Aktif</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label"></label>
                                <div class="col-sm-5">
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--END FORM--%>
                    <%--START VIEW--%>
                    <div id="tabView" runat="server" visible="false">
                    </div>
                    <%--END VIEW--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

