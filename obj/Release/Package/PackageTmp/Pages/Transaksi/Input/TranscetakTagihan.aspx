<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TranscetakTagihan.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TranscetakTagihan" %>

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
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-3">
                                    <div class="form-group ">
                                        <label class="col-sm-2 control-label text-right">Filter</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="cboKelas" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group ">
                                        <div class="form-inline">
                                            <div class="col-sm-12">
                                                <asp:DropDownList ID="cbofromMonth" runat="server" Width="120">
                                                <asp:ListItem Value="1">Januari</asp:ListItem>
                                                <asp:ListItem Value="2">Februari</asp:ListItem>
                                                <asp:ListItem Value="3">Maret</asp:ListItem>
                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                <asp:ListItem Value="5">Mei</asp:ListItem>
                                                <asp:ListItem Value="6">Juni</asp:ListItem>
                                                <asp:ListItem Value="7">Juli</asp:ListItem>
                                                <asp:ListItem Value="8">Agustus</asp:ListItem>
                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                <asp:ListItem Value="10">Oktober</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">Desember</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="cbofromYear"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                               
                                <div class="col-sm-3">
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
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdKelasSiswa" DataKeyNames="noSiswa" SkinID="GridView" runat="server" AllowPaging="false" PageSize="100" OnPageIndexChanging="grdKelasSiswa_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:HiddenField ID="hdnNoSiswa" runat="server" Value='<%# Bind("nopiutang") %>' />
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
                                            <asp:BoundField DataField="nis" HeaderText="NIS" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="nisn" HeaderText="NISN" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="namasiswa" HeaderText="Nama Siswa" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="tgl" HeaderText="Tanggal Tagihan" HeaderStyle-CssClass="text-center" DataFormatString="{0:dd MM yyyy}" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="nilaiuangsekolah" HeaderText="Uang Sekolah" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right" />
                                            <asp:BoundField DataField="nilaiuangkegiatan" HeaderText="Uang Kegiatan" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right" />
                                            <asp:BoundField DataField="nilaitunggakan" HeaderText="Tunggakan" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right" />
                                            <asp:BoundField DataField="nilailain2" HeaderText="Biaya Lain" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right" />
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Catatan" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox ID="txtcatatan" runat="server" CssClass="form-control" TextMode="MultiLine" Width="200"></asp:TextBox>
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
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSubmit_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="tabForm" runat="server">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
