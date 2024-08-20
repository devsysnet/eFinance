<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstAccountBank.aspx.cs" Inherits="eFinance.Pages.Master.View.MstAccountBank" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <asp:HiddenField ID="hdnnoYysn" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="card">
                            <div class="card-body" style="margin-top: 5px;">
                                <div class="row">
                                    <div class="col-sm-20">
                                        <div class="form-group ">
                                            <div class="form-inline">
                                                  <div class="col-sm-20">
                                                    <div class="form-group ">
                                                       <%-- <div class="form-inline">
                                                            <div class="col-sm-20">--%>
                                                                <%--<label>Search :</label>--%>
                                                                <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged" Width="300"></asp:DropDownList>
                                                                <%--<asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search.."></asp:TextBox>--%>
                                                                <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnCari_Click" CausesValidation="false" />
                                                                <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary btn-sm" Text="Download" OnClick="btnExport_Click" CausesValidation="false" />
                                                               
                                                           <%-- </div>
                                                        </div>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:GridView ID="grdAccount" DataKeyNames="noRek" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Account Code" SortExpression="kdRek" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="kdRek" runat="server" Text='<%# Eval("kdrek") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remark" SortExpression="Ket" ItemStyle-Width="60%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket").ToString()%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit" SortExpression="Unit" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="pos" runat="server" Text='<%# Eval("namacabang") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       

                                        <%--<asp:BoundField DataField="kdRek" SortExpression="kdRek" HeaderText="Account Code" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="Ket" SortExpression="Ket" HeaderText="Remark" ItemStyle-Width="50%" />
                                        <asp:BoundField DataField="pos" SortExpression="pos" HeaderText="Posisi" ItemStyle-Width="5%" />
                                        <asp:BoundField DataField="Grup" SortExpression="Grup" HeaderText="Group" ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="Kelompok" SortExpression="Kelompok" HeaderText="Kelompok" ItemStyle-Width="20%" />--%>
                                       </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
