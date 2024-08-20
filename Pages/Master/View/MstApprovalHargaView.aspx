<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstApprovalHargaView.aspx.cs" Inherits="eFinance.Pages.Master.View.MstApprovalHargaView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">

    </script>
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="cboUntukHeader" runat="server">
                                        <asp:ListItem Value="-">--Pilih Peruntukan--</asp:ListItem>
                                        <asp:ListItem Value="Unit">Unit</asp:ListItem>
                                        <asp:ListItem Value="Perwakilan">Perwakilan</asp:ListItem>
                                        <asp:ListItem Value="Yayasan">Yayasan</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-8 text-right">
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <div class="col-sm-12">
                                                <label>Search :</label>
                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnCari_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                             </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdApprovalScheme" SkinID="GridView" DataKeyNames="noApprove" runat="server" AllowPaging="true" PageSize="10"
                                        OnPageIndexChanging="grdApprovalScheme_PageIndexChanging" OnSelectedIndexChanged="grdApprovalScheme_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:HiddenField ID="hdnnoParameterApprove" runat="server" Value='<%# Eval("noParameterApprove") %>' />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="namaParameterApprove" HeaderStyle-CssClass="text-center" SortExpression="namaParameterApprove" HeaderText="Parameter" ItemStyle-Width="15%" />
                                            <asp:BoundField DataField="namaUser" SortExpression="namaUser" HeaderStyle-CssClass="text-center" HeaderText="User Name" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="namaCabang" HeaderStyle-CssClass="text-center" SortExpression="namaCabang" HeaderText="Cabang" ItemStyle-Width="12%" />
                                            <asp:BoundField DataField="hakAkses" SortExpression="hakAkses" HeaderStyle-CssClass="text-center" HeaderText="Jabatan" ItemStyle-Width="15%" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnDetail" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Detail" CommandName="Select" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                            <div class="row">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Peruntukan</label>
                                        <div class="controls col-sm-4">
                                            <asp:DropDownList ID="cboUntuk" runat="server" Enabled="false">
                                                <asp:ListItem Value="-">--Pilih Peruntukan--</asp:ListItem>
                                                <asp:ListItem Value="Unit">Unit</asp:ListItem>
                                                <asp:ListItem Value="Perwakilan">Perwakilan</asp:ListItem>
                                                <asp:ListItem Value="Yayasan">Yayasan</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Kategori</label>
                                        <div class="controls col-sm-4">
                                            <asp:DropDownList ID="cboCategory" runat="server" Enabled="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="text-center">
                                    <div class="col-sm-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdApproval" SkinID="GridView" Width="60%" runat="server" HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Jabatan" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <div class="form-inline">
                                                                    <asp:DropDownList ID="cboJabatan" runat="server" Enabled="false">
                                                                        <asp:ListItem Value="0">--Pilih Jabatan--</asp:ListItem>
                                                                        <asp:ListItem Value="Kepala Yayasan">Kepala Yayasan</asp:ListItem>
                                                                        <asp:ListItem Value="Kepala Perwakilan">Kepala Perwakilan</asp:ListItem>
                                                                        <asp:ListItem Value="Kepala Sekolah">Kepala Sekolah</asp:ListItem>
                                                                        <asp:ListItem Value="Bendahara">Bendahara</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Dari Nilai" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <asp:TextBox runat="server" ID="txtdrNilai" CssClass="form-control money" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Sampai Nilai" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <asp:TextBox runat="server" ID="txtkeNilai" CssClass="form-control money" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="text-center">
                                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnReset_Click" />
                                    </div>
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
