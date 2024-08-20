<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransAbsensi.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransAbsensi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
                
    </script>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-group">
                            <div class="form-inline">
                                <label class="col-sm-1 control-label">Tanggal <span class="mandatory">*</span></label>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkPickDate" OnClick="lnkPickDate_Click" runat="server" CausesValidation="false"><img src="<%=Func.BaseUrl%>Assets/images/calendar-icon.gif" /></asp:LinkButton>
                                        <asp:TextBox ID="dtAbsen" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        <asp:Calendar ID="TglAbsen" runat="server" OnSelectionChanged="TglAbsen_SelectionChanged" Visible="false" OnDayRender="TglAbsen_DayRender"></asp:Calendar>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <asp:GridView ID="grdAbsensi" DataKeyNames="nokaryawan" SkinID="GridView" AutoGenerateColumns="false" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnNoKaryawan" runat="server" Value='<%# Bind("nokaryawan") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="nik" HeaderStyle-CssClass="text-center" HeaderText="NIK" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" HeaderText="Nama Karyawan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("nama") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="2%" ItemStyle-CssClass="text-center" HeaderText="Jam Msk">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cbojammasuk" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="2%" ItemStyle-CssClass="text-center" HeaderText="Menit Msk">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cbomenitmasuk" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="2%" ItemStyle-CssClass="text-center" HeaderText="Jam Plg">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cbojamkeluar" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="2%" ItemStyle-CssClass="text-center" HeaderText="Menit Plg">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cbomenitkeluar" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
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
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click"></asp:Button>
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
