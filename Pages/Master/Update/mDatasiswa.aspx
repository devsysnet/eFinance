<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="mDatasiswa.aspx.cs" Inherits="eFinance.Pages.Master.Update.mDatasiswa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" />
        <script type="text/javascript">
        function DeleteAll() {
            bootbox.confirm({
                message: "Are you sure to update data?",
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
            </script>
        <asp:Button ID="cmdMode" runat="server" OnClick="cmdMode_Click" Style="display: none;" Text="Mode" />
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
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <label>Search :</label>
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdSiswa" DataKeyNames="noSiswa" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdSiswa_PageIndexChanging" OnRowCommand="grdSiswa_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="NO." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nis" HeaderText="NIS" HeaderStyle-CssClass="text-center" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="namaSiswa" HeaderText="Nama" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="alamat" HeaderText="Alamat" HeaderStyle-CssClass="text-center" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Ubah" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectDel" runat="server" class="btn btn-xs btn-labeled btn-danger" Text="Hapus" CommandName="SelectDelete" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
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
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">NIS <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="nis" type="text" placeholder="Masukkan NIS" Enabled="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama Siswa <span class="mandatory">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:TextBox runat="server" class="form-control " ID="nama" type="text" placeholder="Masukkan Nama Siswa"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal Masuk<span class="mandatory">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="dttglmasuk" runat="server" CssClass="form-control date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">NIK </label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" class="form-control " ID="nik" type="text" placeholder="Masukkan NIK"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">NISN </label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" class="form-control " ID="nisn" type="text" placeholder="Masukkan NISN"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Kelamin <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="cbojnskelamin" runat="server">
                                                <asp:ListItem Value="0" Selected="True">--Pilih Jenis Kelamin---</asp:ListItem>
                                                <asp:ListItem Value="1">Laki Laki</asp:ListItem>
                                                <asp:ListItem Value="2">Perempuan</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Alamat</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox runat="server" class="form-control " ID="Alamat" TextMode="MultiLine" type="text" placeholder="Name"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kota <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <%--<asp:DropDownList ID="cboKota" class="form-control" runat="server"></asp:DropDownList>--%>
                                            <asp:TextBox runat="server" class="form-control " ID="cboKota" type="text" placeholder="Kota"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Agama <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="cboAgama" class="form-control" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Dapat Discount/Beasiswa <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                             <asp:DropDownList ID="discount" runat="server" Width="100">
                                             <asp:ListItem Value="1">Ya</asp:ListItem>
                                             <asp:ListItem Value="0">Tidak</asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                 <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kota Lahir <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" class="form-control " ID="cboKotalahir" type="text" placeholder="Kota"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal Lahir</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="dtTgllahir" runat="server" CssClass="form-control date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama Orang Tua <span class="mandatory">*</span></label>
                                        <div class="col-sm-7">
                                            <asp:TextBox runat="server" class="form-control " ID="namaOrangtua" type="text" placeholder="Nama Orang Tua"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Telp</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" class="form-control " ID="telp" type="text"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">No.VA</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" class="form-control " ID="novirtual" type="text"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Email Orang Tua</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" class="form-control " ID="emailtxt" type="text"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterangan 1</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox runat="server" class="form-control " ID="keterangan" TextMode="MultiLine" type="text" placeholder="Name"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterangan 2</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox runat="server" class="form-control " ID="keterangan1" TextMode="MultiLine" type="text" placeholder="Name"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterangan 3</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox runat="server" class="form-control " ID="keterangan2" TextMode="MultiLine" type="text" placeholder="Name"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Status</label>
                                        <div class="col-sm-7">
                                        <asp:DropDownList ID="cboaktif" runat="server" Width="150">
                                                <asp:ListItem Value="1">Aktif</asp:ListItem>
                                                <asp:ListItem Value="0">Non Aktif</asp:ListItem>
                                            </asp:DropDownList>
                                         </div>
                                    </div>
                                     <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal Keluar</label>
                                    <div class="col-sm-5">
                                      <asp:TextBox ID="dtkeluar" runat="server" CssClass="form-control date"></asp:TextBox>
                                    </div>
                                </div>
                                   
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="text-center">
                                    <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Save"   UseSubmitBehavior="false" OnClientClick="DeleteAll()" />
                                    <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Back" OnClick="btnCancel_Click"></asp:Button>
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
