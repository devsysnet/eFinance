<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransArSiswa.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransArSiswa" %>

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
                            <div class="col-sm-8">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tahun Ajaran</label>
                                        <div class="col-sm-5">
                                              <asp:DropDownList ID="cboTA" runat="server" CssClass="form-control" Width="150"></asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal Transaksi</label>
                                        <div class="col-sm-5">
                                             <asp:TextBox runat="server" CssClass="form-control date" ID="dtKas"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Pembayaran</label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="cboKategori" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nilai Bayar</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server"  CssClass="form-control money" ID="TxtNilai" type="text" Text="0.00" placeholder="Nilai"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <%--<asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Visible="false" Text="Semua Siswa" OnClick="btnSimpan_Click" />--%>
                                                <asp:Button ID="btnPilihSiswa" runat="server" CssClass="btn btn-success" Text="Cari Siswa" OnClick="btnPilihSiswa_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"/>
                                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                           
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <label>Kelas :</label>
                                            <asp:DropDownList ID="cboKelas" runat="server" CssClass="form-control" Width="120"></asp:DropDownList>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click" CausesValidation="false" />
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
                                                        <asp:HiddenField ID="hdnNoSiswa" runat="server" Value='<%# Bind("noSiswa") %>' />
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
                                            <asp:BoundField DataField="nis" HeaderText="NIS" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="nisn" HeaderText="NISN" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="namasiswa" HeaderText="Nama Siswa" HeaderStyle-CssClass="text-center" ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="kelas" HeaderText="Kelas" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"/>

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
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
