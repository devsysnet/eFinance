<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstKaryawanView.aspx.cs" Inherits="eFinance.Pages.Master.View.MstKaryawanView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="TabName" runat="server" />
    <script type="text/javascript">

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

    </script>
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <asp:HiddenField ID="hdnnoYysn" runat="server" />
    <div class="row">
        <div class="form-horizontal">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-body">
                        <div id="tabGrid" runat="server">
                            <div class="card">
                                <div class="card-body" style="margin-top: 5px;">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-1 control-label text-right">Filter</label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="cboPerwakilan" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboPerwakilan_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="cboUnit" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboUnit_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Masukan kata" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="grdKaryawan" DataKeyNames="noKaryawan" SkinID="GridView" runat="server" AllowPaging="true" PageSize="100" OnPageIndexChanging="grdKaryawan_PageIndexChanging"
                                                    OnSelectedIndexChanged="grdKaryawan_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <div class="text-center">
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="idPeg" HeaderText="ID Karyawan" ItemStyle-Width="12%" HeaderStyle-CssClass="text-center" />
                                                        <asp:BoundField DataField="nama" HeaderText="Nama" ItemStyle-Width="25%" HeaderStyle-CssClass="text-center" />
                                                        <asp:BoundField DataField="golonganx" HeaderText="Golongan" ItemStyle-Width="20%" HeaderStyle-CssClass="text-center" />
                                                        <asp:BoundField DataField="jabatanx" HeaderText="Jabatan" ItemStyle-Width="15%" HeaderStyle-CssClass="text-center" />
                                                        <asp:BoundField DataField="lamakerja" HeaderText="Lama Kerja" ItemStyle-Width="8%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="namaCabang" HeaderText="Unit" ItemStyle-Width="22%" HeaderStyle-CssClass="text-center" />
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
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNUPTK" placeholder="NUPTK" Enabled="false"></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnnoKaryawan" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label text-right">Nama Karyawan</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNama" placeholder="Nama" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label text-right">Tempat Lahir</label>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txttempatlahir" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label text-right">Tanggal Lahir</label>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="dtlahir" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label text-right">Jenis Kelamin</label>
                                                                <div class="col-sm-7">
                                                                    <asp:DropDownList ID="cboStatus" runat="server" CssClass="form-control" Width="150" Enabled="false">
                                                                        <asp:ListItem Value="-">----</asp:ListItem>
                                                                        <asp:ListItem Value="1">Laki-Laki</asp:ListItem>
                                                                        <asp:ListItem Value="2">Perempuan</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label text-right">Agama</label>
                                                                <div class="col-sm-7">
                                                                    <asp:DropDownList ID="cboAgama" CssClass="form-control" runat="server" Width="100" Enabled="false"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                             <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Alamat</label>
                                                                <div class="col-sm-7">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtalamat" TextMode="MultiLine" Enabled="false" placeholder="Alamat"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label text-right">KewargaNegaraan</label>
                                                                <div class="col-sm-7">
                                                                    <asp:DropDownList ID="cboKewarganegaraan" runat="server" CssClass="form-control" Width="80" Enabled="false">
                                                                        <asp:ListItem Value="-">----</asp:ListItem>
                                                                        <asp:ListItem Value="1">WNI</asp:ListItem>
                                                                        <asp:ListItem Value="2">WNA</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label text-right">Golongan Darah</label>
                                                                <div class="col-sm-7">
                                                                    <asp:DropDownList ID="cboGolongandarah" runat="server" CssClass="form-control" Width="70" Enabled="false">
                                                                        <asp:ListItem Value="-">----</asp:ListItem>
                                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                                        <asp:ListItem Value="B">B</asp:ListItem>
                                                                        <asp:ListItem Value="AB">AB</asp:ListItem>
                                                                        <asp:ListItem Value="O">O</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="display:none">
                                                                <div class="form-inline">
                                                                    <label class="col-sm-4 control-label text-right">Tinggi Badan</label>
                                                                    <div class="col-sm-7">
                                                                        <asp:TextBox runat="server" ID="txttinggibadan" CssClass="form-control money" Text="0.00" placeholder="Tinggi Badan" Enabled="false" Width="80"></asp:TextBox>
                                                                        <span>CM</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="display:none">
                                                                <div class="form-inline">
                                                                    <label class="col-sm-4 control-label text-right">Berat Badan</label>
                                                                    <div class="col-sm-7">
                                                                        <asp:TextBox runat="server" ID="txtberatbadan" CssClass="form-control money" Text="0.00" placeholder="Berat Badan" Width="80" Enabled="false"></asp:TextBox>
                                                                        <span>KG</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label text-right">Status Tanggungan</label>
                                                                <div class="col-sm-3">
                                                                    <asp:DropDownList ID="cbostatusPTK" CssClass="form-control" runat="server" Enabled="false"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label text-right">Status Perkawinan</label>
                                                                <div class="col-sm-4">
                                                                    <asp:DropDownList ID="cboPerkawinan" runat="server" CssClass="form-control" Enabled="false">
                                                                        <asp:ListItem Value="-">---</asp:ListItem>
                                                                        <asp:ListItem Value="1">Kawin</asp:ListItem>
                                                                        <asp:ListItem Value="0">Belum Kawin</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">No SK Pengangkatan Tetap Jabatan</label>
                                                                <div class="col-sm-7">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="TextnoSK" placeholder="No SK Pengangkatan Tetap Jabatan" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            
                                                           
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Jabatan</label>
                                                                <div class="col-sm-5">
                                                                    <asp:DropDownList ID="cboJabatan" CssClass="form-control" runat="server" Enabled="false"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Akta 4</label>
                                                                <div class="col-sm-7">
                                                                    <asp:DropDownList ID="cboAkta" runat="server" CssClass="form-control" Enabled="false">
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
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="dtMasuk" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tanggal Capeg TMT</label>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox ID="dtCapeg" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tanggal Tetap TMT</label>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox ID="dtAngkat" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Golongan</label>
                                                                <div class="col-sm-3">
                                                                    <asp:DropDownList ID="cboGolPegawai" CssClass="form-control" runat="server" Enabled="false"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Departemen</label>
                                                                <div class="col-sm-4">
                                                                    <asp:DropDownList ID="cboDepartemen" CssClass="form-control" runat="server" Enabled="false"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                           
                                                            <div class="form-group" style="display:none">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">No BPJS Kesehatan</label>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtnoBPJSSehat" placeholder="No BPJS Kesehatan" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="display:none">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tgl BPJS Kesehatan</label>
                                                                <div class="col-sm-3">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="dtBPJSSehat" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="display:none">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">No BPJS Ketenagakerjaan</label>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtnoBPJSKerja" placeholder="No BPJS Ketenagakerjaan" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="display:none">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tgl BPJS Ketenagakerjaan</label>
                                                                <div class="col-sm-3">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="dtnoBPJSKerja" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="display:none">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">No KWI</label>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtnoKWI" placeholder="No KWI" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="display:none">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">TMT KWI</label>
                                                                <div class="col-sm-3">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="dtTMTKWI" placeholder="TMT KWI" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Telephon</label>
                                                                <div class="col-sm-7">
                                                                    <asp:TextBox runat="server" CssClass="form-control phone" ID="txttelp" placeholder="Telephone" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Handphone</label>
                                                                <div class="col-sm-7">
                                                                    <asp:TextBox runat="server" CssClass="form-control phone" ID="txthp" placeholder="HandPhone" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Email</label>
                                                                <div class="col-sm-7">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" placeholder="Email" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Bank</label>
                                                                <div class="col-sm-5">
                                                                    <asp:DropDownList ID="cboBank" CssClass="form-control" runat="server" Enabled="false"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nomor Rekening</label>
                                                                <div class="col-sm-7">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtnorek" placeholder="Nomor Rekening" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Rekening</label>
                                                                <div class="col-sm-7">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txnamaRekening" placeholder="Nama Rekening" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Status Pegawai</label>
                                                                <div class="col-sm-4">
                                                                    <asp:DropDownList ID="cboStsPegawai" CssClass="form-control" runat="server" Enabled="false"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="display:none">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Jenis Pegawai</label>
                                                                <div class="col-sm-4">
                                                                    <asp:DropDownList ID="statuspegawai" CssClass="form-control" runat="server" Enabled="false"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">PPH 21</label>
                                                                <div class="col-sm-7">
                                                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" Enabled="false">
                                                                        <asp:ListItem Value="1">Ya</asp:ListItem>
                                                                        <asp:ListItem Value="0">Tidak</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Unit Kerja</label>
                                                                <div class="col-sm-7">
                                                                    <asp:DropDownList ID="cboCabangInput" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Status Aktif</label>
                                                                <div class="col-sm-3">
                                                                    <asp:DropDownList ID="cboAktif" runat="server" CssClass="form-control" Enabled="false">
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

                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="form-group row">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click"></asp:Button>
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
    </div>
    <div id="tabView" runat="server" visible="false">
    </div>
</asp:Content>


