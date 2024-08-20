<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstCabangUpdate.aspx.cs" Inherits="eFinance.Pages.Master.Update.MstCabangUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function DeleteAll() {
            bootbox.confirm({
                message: "Are you sure to delete data?",
                callback: function (result) {
                    if (result == true) {
                        document.getElementById('<%=hdnMode.ClientID%>').value = "deleteall";
                        eval("<%=execBind%>");
                    }
                },
                className: "bootbox-sm"
            });
            return false;
        }
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdCabang.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <asp:Button ID="cmdMode" runat="server" OnClick="cmdMode_Click" Style="display: none;" Text="Mode" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <asp:HiddenField runat="server" ID="hdnID" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-8">
                                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" Text="Delete / Delete all" OnClientClick="return DeleteAll()" CausesValidation="false" />
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <label>Filter :</label>
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnCari_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdCabang" DataKeyNames="noCabang" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdCabang_PageIndexChanging"
                                        OnSelectedIndexChanged="grdCabang_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
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
                                            <asp:BoundField DataField="kdCabang" HeaderStyle-CssClass="text-center" HeaderText="Kode Cabang" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="namaCabang" HeaderStyle-CssClass="text-center" HeaderText="Nama Cabang" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="namaOfficerFin" HeaderStyle-CssClass="text-center" HeaderText="Nama Office" ItemStyle-Width="20%" />
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Detail" CommandName="Select" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kategori Usaha</label>
                                    <div class="col-sm-5">
                                         <asp:DropDownList runat="server" class="form-control " ID="Kategori" width="180">
                                        <asp:ListItem Value=""> ---Pilih Kategori Usaha--- </asp:ListItem>
                                        <asp:ListItem Value="Sekolah"> Sekolah </asp:ListItem>
                                        <asp:ListItem Value="Yayasan"> Yayasan </asp:ListItem>
                                        <asp:ListItem Value="Properti"> Properti </asp:ListItem>
                                        <asp:ListItem Value="Trade"> Trade </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama Cabang</label>
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" class="form-control" ID="txtNama"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Alamat</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox runat="server" class="form-control " ID="txtAlamat" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kota</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox runat="server" class="form-control " ID="txtKota"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kode Pos</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox runat="server" class="form-control " ID="txtKodePos"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">No Telpon</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox runat="server" class="form-control " ID="txtNoTelp"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">No Fax</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox runat="server" class="form-control " ID="txtNoFax"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Email</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox runat="server" class="form-control " ID="txtEmail"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Office Finance</label>
                                    <div class="col-sm-5">
                                        <asp:TextBox runat="server" class="form-control " ID="txtOffice"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Status</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList runat="server" class="form-control " ID="cboStatus" AutoPostBack="true" OnSelectedIndexChanged="cboStatus_SelectedIndexChanged">
                                            <asp:ListItem Value=""> ---Pilih Status--- </asp:ListItem>
                                            <asp:ListItem Value="0"> Pusat </asp:ListItem>
                                            <asp:ListItem Value="4"> Kantor Pusat </asp:ListItem>
                                            <asp:ListItem Value="1"> Perwakilan </asp:ListItem>
                                            <asp:ListItem Value="3"> Kantor Perwakilan </asp:ListItem>
                                            <asp:ListItem Value="2"> Unit </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Parent Unit</label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="cboParent" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kategori Unit</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList runat="server" class="form-control" ID="cboUnit">
                                            <asp:ListItem Value="">-</asp:ListItem>
                                            <asp:ListItem Value="TK"> TK </asp:ListItem>
                                            <asp:ListItem Value="SD"> SD </asp:ListItem>
                                            <asp:ListItem Value="SMP"> SMP </asp:ListItem>
                                            <asp:ListItem Value="SMA"> SMA </asp:ListItem>
                                            <asp:ListItem Value="SMK"> SMK </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Menggunakan MHS</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" class="form-control" ID="mhs">
                                        <asp:ListItem Value="1">Ya</asp:ListItem>
                                        <asp:ListItem Value="0">Tidak</asp:ListItem>
                                      </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Pelunasan</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" class="form-control" ID="allPelunasan">
                                        <asp:ListItem Value="1">Ya</asp:ListItem>
                                        <asp:ListItem Value="0">Tidak</asp:ListItem>
                                      </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Cetak Voucher</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" class="form-control" ID="cetakVoucher">
                                        <asp:ListItem Value="1">Ya</asp:ListItem>
                                        <asp:ListItem Value="0">Tidak</asp:ListItem>
                                      </asp:DropDownList>
                                </div>
                            </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="text-center">
                                            <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
