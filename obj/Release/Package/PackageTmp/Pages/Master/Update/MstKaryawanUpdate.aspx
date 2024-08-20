<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstKaryawanUpdate.aspx.cs" Inherits="eFinance.Pages.Master.Update.MstKaryawanUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="TabName" runat="server" />
    <script type="text/javascript">
        function EditRow(id) {
            document.getElementById('<%=hdnMode.ClientID%>').value = "edit";
            document.getElementById('<%=hdnId.ClientID%>').value = id;
            eval("<%=execBind%>");
            return false;
        }
        function DeleteRow(id, name) {
            bootbox.confirm({
                message: "Anda yakin untuk menghapus data <b>" + name + "</b> ?",
                callback: function (result) {
                    if (result == true) {
                        document.getElementById('<%=hdnMode.ClientID%>').value = "delete";
                        document.getElementById('<%=hdnId.ClientID%>').value = id;
                        eval("<%=execBind%>");
                    }
                },
                className: "bootbox-sm"
            });
            return false;
        }
        $(function () {

            SetTabs();
        });
        Sys.Application.add_init(appl_init);
        function appl_init() {
            var pgRegMgr = Sys.WebForms.PageRequestManager.getInstance();
            pgRegMgr.add_beginRequest(SetTabs);
            pgRegMgr.add_endRequest(SetTabs);
        }
        function SetTabs() {
            var tabName = $("#<%=TabName.ClientID%>").val();
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("#<%=TabName.ClientID%>").val($(this).attr("href").replace("#", ""));
            });

        };
        function DeleteAll() {
            bootbox.confirm({
                message: "Are you sure to delete all selected data?",
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
            var GridVwHeaderChckbox = document.getElementById("<%=grdKaryawan.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <asp:Button ID="cmdMode" runat="server" OnClick="cmdMode_Click" Style="display: none;" Text="Mode" />
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <div id="tabGrid" runat="server">
        <div class="card">
            <div class="card-body" style="margin-top: 5px;">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group ">
                            <div class="form-inline">
                                <div class="col-sm-8">
                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" OnClientClick="return DeleteAll()" Text="Delete all selected" />
                                    <asp:DropDownList ID="cbocabang" runat="server" CssClass="form-control" AutoPostBack="false" ></asp:DropDownList>
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
                        </div>
                    </div>
                </div>
                <asp:GridView ID="grdKaryawan" DataKeyNames="noKaryawan" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdKaryawan_PageIndexChanging"
                    OnSelectedIndexChanged="grdKaryawan_SelectedIndexChanged">
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
                        <asp:BoundField DataField="nik" HeaderText="NIK Karyawan" ItemStyle-Width="12%" HeaderStyle-CssClass="text-center" />
                        <asp:BoundField DataField="nama" HeaderText="Nama" ItemStyle-Width="20%" HeaderStyle-CssClass="text-center" />
                        <asp:BoundField DataField="Alamat" HeaderText="Alamat" ItemStyle-Width="30%" HeaderStyle-CssClass="text-center" />
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
    <div id="tabForm" runat="server" visible="false">
        <div class="panel">
            <div class="panel-body" id="Tabs" role="tabpanel">
                <!-- Nav tabs -->
                <ul class="nav nav-tabs" role="tablist">
                    <li class="active">
                        <a href="#tab-Company" data-toggle="tab" role="tab">Data Umum
                        </a>
                    </li>
                    <li>
                        <a href="#tab-Pendidikan" data-toggle="tab" role="tab">Pendidikan</a>
                    </li>
                    <li>
                        <a href="#tab-Identitas" data-toggle="tab" role="tab">Identitas</a>
                    </li>
                    <li>
                        <a href="#tab-Kelurga" data-toggle="tab" role="tab">Keluarga</a>
                    </li>
                    <li>
                        <a href="#tab-PengalamanKerja" data-toggle="tab" role="tab">Pengalaman Kerja</a>
                    </li>
                    <li>
                        <a href="#tab-Iuran" data-toggle="tab" role="tab">Iuran</a>
                    </li>
                </ul>
                <!-- Tab panes -->
                <div class="tab-content tab-content-bordered">
                    <div role="tabpanel" class="tab-pane active" id="tab-Company">
                        <div class="row">
                            <fieldset class="fsStyle">
                                <legend class="legendStyle"><b>Data Umum</b></legend>
                                <div class="col-sm-7">
                                    <div class="form-horizontal">
                                        <div class="form-group" style="display:none">
                                            <label class="col-sm-4 control-label text-right">NUPTK</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtNUPTK" placeholder="NUPTK"></asp:TextBox>
                                                <asp:HiddenField ID="hdnnoKaryawan" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label text-right">Nama Karyawan</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtNama" placeholder="Nama"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label text-right">Tempat Lahir</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txttempatlahir"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label text-right">Tanggal Lahir</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox runat="server" CssClass="form-control date" ID="dtlahir"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label text-right">Jenis Kelamin</label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="cboStatus" runat="server" CssClass="form-control" Width="150">
                                                    <asp:ListItem Value="-">----</asp:ListItem>
                                                    <asp:ListItem Value="1">Laki-Laki</asp:ListItem>
                                                    <asp:ListItem Value="2">Perempuan</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label text-right">Agama</label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="cboAgama" CssClass="form-control" runat="server" Width="100"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Alamat</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtalamat" TextMode="MultiLine" placeholder="Alamat"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label text-right">KewargaNegaraan</label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="cboKewarganegaraan" runat="server" CssClass="form-control" Width="80">
                                                    <asp:ListItem Value="-">----</asp:ListItem>
                                                    <asp:ListItem Value="1">WNI</asp:ListItem>
                                                    <asp:ListItem Value="2">WNA</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label text-right">Golongan Darah</label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="cboGolongandarah" runat="server" CssClass="form-control" Width="70">
                                                    <asp:ListItem Value="-">----</asp:ListItem>
                                                    <asp:ListItem Value="A">A</asp:ListItem>
                                                    <asp:ListItem Value="B">B</asp:ListItem>
                                                    <asp:ListItem Value="AB">AB</asp:ListItem>
                                                    <asp:ListItem Value="O">O</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" style="display:none">
                                            <div class="form-inline" >
                                                <label class="col-sm-4 control-label text-right">Tinggi Badan</label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox runat="server" ID="txttinggibadan" CssClass="form-control money" Text="0.00" placeholder="Tinggi Badan" Width="80"></asp:TextBox>
                                                    <span>CM</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group" style="display:none">
                                            <div class="form-inline">
                                                <label class="col-sm-4 control-label text-right">Berat Badan</label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox runat="server" ID="txtberatbadan" CssClass="form-control money" Text="0.00" placeholder="Berat Badan" Width="80"></asp:TextBox>
                                                    <span>KG</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label text-right">Status Tanggungan</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cbostatusPTK" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label text-right">Status Perkawinan</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="cboPerkawinan" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="-">---</asp:ListItem>
                                                    <asp:ListItem Value="1">Kawin</asp:ListItem>
                                                    <asp:ListItem Value="0">Belum Kawin</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">No SK Pengangkatan Tetap Jabatan</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="TextnoSK" placeholder="No SK Pengangkatan Tetap Jabatan"></asp:TextBox>
                                            </div>
                                        </div>
                                        
                                        

                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Jabatan/Jabatan Fungsional</label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="cboJabatan" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Akta 4</label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="cboAkta" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                                    <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                    </div>
                                </div>
                                <%--pindah kolom--%>
                                <div class="col-sm-5">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tanggal Masuk</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox runat="server" CssClass="form-control date" ID="dtMasuk"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tanggal Capeg TMT</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox runat="server" CssClass="form-control date" ID="dtCapeg"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tanggal Tetap TMT</label>
                                            <div class="col-sm-5">
                                                <asp:Textbox ID="dtAngkat" CssClass="form-control date" runat="server"></asp:Textbox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Golongan</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cboGolPegawai" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Jabatan Struktural/Tugas Tambahan</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="cboDepartemen" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                        <div class="form-group" style="display:none">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">No BPJS Kesehatan</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtnoBPJSSehat" placeholder="No BPJS Kesehatan"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group" style="display:none">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tgl BPJS Kesehatan</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox runat="server" CssClass="form-control date" ID="dtBPJSSehat"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group" style="display:none">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">No BPJS Ketenagakerjaan</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtnoBPJSKerja" placeholder="No BPJS Ketenagakerjaan"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group" style="display:none">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tgl BPJS Ketenagakerjaan</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox runat="server" CssClass="form-control date" ID="dtnoBPJSKerja"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group" style="display:none">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">No KWI</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtnoKWI" placeholder="No KWI"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group" style="display:none">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">TMT KWI</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox runat="server" CssClass="form-control date" ID="dtTMTKWI" placeholder="TMT KWI"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Telephon</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox runat="server" CssClass="form-control phone" ID="txttelp" placeholder="Telephone"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Handphone</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox runat="server" CssClass="form-control phone" ID="txthp" placeholder="HandPhone"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Email</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" placeholder="Email"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Bank</label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="cboBank" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nomor Rekening</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtnorek" placeholder="Nomor Rekening"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Rekening</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txnamaRekening" placeholder="Nama Rekening"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Status Pegawai</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="cboStsPegawai" CssClass="form-control" runat="server" Width="150"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" style="display:none">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Jenis Pegawai</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="statuspegawai" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">PPH 21</label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="pph21" runat="server" CssClass="form-control" Width="150">
                                                    <asp:ListItem Value="1">Ya</asp:ListItem>
                                                    <asp:ListItem Value="0">Tidak</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Unit Kerja</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="cboCabangInput" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Status Aktif</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cboAktif" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="">-Pilih Status-</asp:ListItem>
                                                    <asp:ListItem Value="1">Aktif</asp:ListItem>
                                                    <asp:ListItem Value="0">Tidak Aktif</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div role="tabpanel" class="tab-pane" id="tab-Pendidikan">
                        <div class="row">
                            <fieldset class="fsStyle">
                                <legend class="legendStyle"><b>Pendidikan</b></legend>
                                <div class="table-responsive">
                                    <asp:GridView ID="grdPendidikan" SkinID="GridView" runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" HeaderText="Pendidikan">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:DropDownList ID="cbopendidikan" CssClass="form-control" runat="server"></asp:DropDownList>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="30%" HeaderText="Nama Sekolah">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox ID="txtnmSekolah" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" HeaderText="Dari Tahun">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control date" ID="dtdrthn"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" HeaderText="Sampai Tahun">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control date" ID="dtspthn"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="5%" HeaderText="nilai">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtNilai"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" HeaderText="Jurusan">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtJurusan"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="7%" HeaderText="Tahun Lulus">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtThnLulus"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="text-center">
                                            <asp:Button runat="server" ID="btnPendidikan" CssClass="btn btn-primary" Text="AddRow" OnClick="btnPendidikan_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div role="tabpanel" class="tab-pane" id="tab-Identitas">
                        <div class="row">
                            <fieldset class="fsStyle">
                                <legend class="legendStyle"><b>Identitas</b></legend>
                                <asp:GridView ID="grdContact" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" HeaderText="Jenis Identitas">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:DropDownList ID="cboidentitas" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="30%" HeaderText="Nomor Identitas">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtnoIdentitas" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="6%" HeaderText="Berlaku Sampai">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" CssClass="form-control date" ID="dtexpdatekartu"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="6%" HeaderText="Tanggal Berlaku">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox runat="server" CssClass="form-control date" ID="dtBerlaku"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="text-center">
                                            <asp:Button ID="btnAddContact" runat="server" CssClass="btn btn-success" Text="Add Row" OnClick="btnAddContact_Click" />

                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div role="tabpanel" class="tab-pane" id="tab-Kelurga">
                        <div class="row">
                            <fieldset class="fsStyle">
                                <legend class="legendStyle"><b>Keluarga</b></legend>
                                <div class="table-responsive">
                                    <asp:GridView ID="GridKeluarga" SkinID="GridView" runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" HeaderText="Status Keluarga">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:DropDownList ID="cbokeluarga" CssClass="form-control" runat="server"></asp:DropDownList>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="30%" HeaderText="Nama">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox ID="txtnamakel" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" HeaderText="Nomor Telpon">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox ID="txttelpkel" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" HeaderText="Pendindikan">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:DropDownList ID="cbopendindikankel" CssClass="form-control" runat="server"></asp:DropDownList>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" HeaderText="Tanggal Lahir">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox runat="server" CssClass="form-control date" ID="dtlahirkel"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <asp:Button ID="btnAddkeluarga" runat="server" CssClass="btn btn-warning" Text="Add Row" OnClick="btnAddkeluarga_Click" />

                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </fieldset>
                        </div>
                    </div>

                    <div role="tabpanel" class="tab-pane" id="tab-PengalamanKerja">
                        <div class="row">
                            <fieldset class="fsStyle">
                                <legend class="legendStyle"><b>Pengalaman Kerja</b></legend>

                                <div class="col-sm-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridPengalamnkerja" SkinID="GridView" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="30%" HeaderText="Nama Perusahaan">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:TextBox ID="txtnamaperusahaan" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="30%" HeaderText="Jabatan">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:TextBox ID="txtjabatan" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" HeaderText="Dari Tanggal">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:TextBox ID="dtAwal" runat="server" CssClass="form-control date"></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" HeaderText="Sampai Tanggal">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:TextBox runat="server" CssClass="form-control date" ID="dtAkhir"></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="text-center">
                                                    <asp:Button ID="btnPengalamnkerja" runat="server" CssClass="btn btn-danger" Text="Add Row" OnClick="btnPengalamnkerja_Click" />

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>


                    <div role="tabpanel" class="tab-pane" id="tab-Iuran">
                        <div class="row">
                            <fieldset class="fsStyle">
                                <legend class="legendStyle"><b>Iuran</b></legend>

                                <div class="col-sm-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdIuran" SkinID="GridView" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" HeaderText="Iuran">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <asp:DropDownList ID="cboIuran" CssClass="form-control" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="text-center">
                                                    <asp:Button ID="btnIuran" runat="server" CssClass="btn btn-default" Text="Add Row" OnClick="btnIuran_Click" />

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-12">
                        <div class="text-center">
                            <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses Simpan';"></asp:Button>
                            <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="tabView" runat="server" visible="false">
    </div>
</asp:Content>


