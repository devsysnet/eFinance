<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransAbsensiUpdate.aspx.cs" Inherits="eFinance.Pages.Transaksi.Update.TransAbsensiUpdate" %>
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
                                        <asp:LinkButton ID="lnkPickDate" OnClick="lnkPickDate_Click" runat="server" CausesValidation="false" ><img src="<%=Func.BaseUrl%>Assets/images/calendar-icon.gif" /></asp:LinkButton>
                                        <asp:TextBox ID="dtAbsen" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                        <asp:Calendar ID="TglAbsen" runat="server" OnSelectionChanged="TglAbsen_SelectionChanged" Visible="false" OnDayRender="TglAbsen_DayRender"></asp:Calendar>                                                                        
                                    </div>   
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <asp:GridView ID="grdAbsensiUpdate" DataKeyNames="nokaryawan" SkinID="GridView" runat="server" OnRowDataBound="grdAbsensiUpdate_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnNoKaryawan" runat="server" Value='<%# Bind("nokaryawan") %>' />
                                                    <asp:HiddenField ID="hdnSts" runat="server" Value='<%# Bind("statusMasukKeluar") %>' />
                                                    <asp:HiddenField ID="hdnjammasuk" runat="server" Value='<%# Bind("jammasuk") %>' />
                                                    <asp:HiddenField ID="hdnmenitmasuk" runat="server" Value='<%# Bind("menitmasuk") %>' />
                                                    <asp:HiddenField ID="hdnjamkeluar" runat="server" Value='<%# Bind("jamkeluar") %>' />
                                                    <asp:HiddenField ID="hdnmenitkeluar" runat="server" Value='<%# Bind("menitkeluar") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="nik" HeaderStyle-CssClass="text-center" HeaderText="NIK" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
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
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" ItemStyle-CssClass="text-center" HeaderText="Ubah">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:CheckBox ID="chkCheck" runat="server" CssClass="px chkCheck" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" ItemStyle-CssClass="text-center" HeaderText="Hapus">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:CheckBox ID="chkCheck2" runat="server" CssClass="px chkCheck" />
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
                                <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Ubah" OnClick="btnSimpan_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                <asp:Button runat="server" ID="btnHapus" CssClass="btn btn-success" Text="Hapus" OnClick="btnHapus_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
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
