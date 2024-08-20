<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TranskenaikanGajiberkala.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TranskenaikanGajiberkala" %>
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
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal Transaksi</label>
                                        <div class="col-sm-5">
                                             <asp:TextBox runat="server" CssClass="form-control date" ID="dtKas"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kenaikan Bulan</label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="cboMonth" runat="server" Width="120">
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
                                            <asp:DropDownList ID="cboYear" runat="server" Width="100">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Kenaikan</label>
                                        <div class="col-sm-5">
                                              <asp:DropDownList ID="cboStatus" runat="server" CssClass="form-control"  Width="150">
                                              <asp:ListItem Value="-">----</asp:ListItem>
                                              <asp:ListItem Value="1">Nilai</asp:ListItem>
                                              <asp:ListItem Value="2">Persented</asp:ListItem>
                                              </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nilai</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server"  CssClass="form-control money" ID="TxtNilai" type="text" Text="0.00" placeholder="Nilai"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <%--<asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Visible="false" Text="Semua Siswa" OnClick="btnSimpan_Click" />--%>
                                                <asp:Button ID="btnPilihSiswa" runat="server" CssClass="btn btn-success" Text="Pilih Karyawan" OnClick="btnPilihSiswa_Click" />
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
                                            <label>Golongan :</label>
                                            <asp:DropDownList ID="cboKelas" runat="server" CssClass="form-control" Width="120"></asp:DropDownList>
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
                                                        <asp:HiddenField ID="hdnNoSiswa" runat="server" Value='<%# Bind("nokaryawan") %>' />
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
                                            <asp:BoundField DataField="idPeg" HeaderText="ID.Peg" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="nama" HeaderText="Nama" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="alamat" HeaderText="Alamat" HeaderStyle-CssClass="text-center" ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="golongan" HeaderText="Golongan" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
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
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
