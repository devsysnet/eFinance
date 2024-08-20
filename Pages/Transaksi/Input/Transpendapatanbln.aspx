<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Transpendapatanbln.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.Transpendapatanbln" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdKelasSiswa.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <asp:HiddenField ID="hdnnoYysn" runat="server" />

    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-8">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                                <asp:DropDownList ID="cboPerwakilan" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboPerwakilan_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:DropDownList ID="cboCabang" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboUnit_SelectedIndexChanged" Width="200"></asp:DropDownList>
                                                <label>Jenis :</label>
                                                <asp:DropDownList ID="cboStatus" runat="server" CssClass="form-control"  Width="170" AutoPostBack="true" OnSelectedIndexChanged="cboStatus_SelectedIndexChanged"></asp:DropDownList>
                                         </div>
                                         
                                   
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <label>Tgl :</label>
                                            <asp:TextBox runat="server" class="form-control date" ID="dtPO"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdKelasSiswa" DataKeyNames="nokaryawan" SkinID="GridView" runat="server" AllowPaging="false" PageSize="100" OnPageIndexChanging="grdKelasSiswa_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:HiddenField ID="hdnnokaryawan" runat="server" Value='<%# Bind("nokaryawan") %>' />
                                                        <%--<asp:HiddenField ID="hdnJenisSekolah" runat="server" Value='<%# Bind("jnsSekolah") %>' />--%>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <HeaderTemplate>
                                                    <div class="text-center">
                                                        <asp:CheckBox ID="chkCheckAll" runat="server" CssClass="px" onclick="return CheckAllData(this);" />
                                                    </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:CheckBox ID="chkCheck" runat="server" CssClass="px chkCheck" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nik" HeaderText="NIK" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="nama" HeaderText="Nama" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="jabatan" HeaderText="Jabatan" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="golongan" HeaderText="golongan" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <%--<asp:BoundField DataField="tgl" HeaderText="Tanggal Diangkat" HeaderStyle-CssClass="text-center" DataFormatString="{0:dd MM yyyy}" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />--%>
                                            <%--<asp:BoundField DataField="nilai" HeaderText="Gaji Pokok" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right" />--%>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" HeaderText="Nilai">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="nilai" runat="server" CssClass="form-control money" AutoPostBack="true"  Width="150"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                            </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="button" runat="server">
                            <div class="col-sm-12 ">
                                <div class="text-center">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSubmit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

