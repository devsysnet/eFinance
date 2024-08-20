<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="mPasienView.aspx.cs" Inherits="eFinance.Pages.Master.View.mPasienView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" />
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
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                           <div class="row">
                             <div class="col-sm-8">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <label>Cabang :</label>
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged" Width="300"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
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
                   </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdSiswa" DataKeyNames="noPasien" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdSiswa_PageIndexChanging"
                                        OnSelectedIndexChanged="grdSiswa_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="NO." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="noRegister" HeaderText="Nomor Kartu" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="namaPasien" HeaderText="Nama Pasien" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="tgllahir" HeaderText="Tanggal Lahir" HeaderStyle-CssClass="text-center" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="alamat" HeaderText="Alamat" HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
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
                    <div id="tabForm" runat="server">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-12">
                                    <div class="panel-body" id="Tabs" role="tabpanel">
                                        <!-- Nav tabs -->
                                        <ul class="nav nav-tabs" role="tablist">
                                            <li class="active">
                                                <a href="#tab-umum" data-toggle="tab" role="tab">Umum
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#tab-piutang" data-toggle="tab" role="tab">Medical Record</a>
                                            </li>
                                        </ul>
                                        <!-- Tab panes -->
                                        <div class="tab-content tab-content-bordered">
                                            <div class="tab-pane active" id="tab-umum">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-horizontal">
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nomor Kartu :</label>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtnoRegister" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tanggal Register :</label>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtTglregister" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="form-inline">
                                                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">NIK :</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNIK" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nama Lengkap :</label>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNama" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Jenis Kelamin :</label>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox ID="txtgender" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Agama :</label>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox ID="txtAgama" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Status BPJS :</label>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox ID="txtstsBpjs" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                             <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Nomoe BPJS :</label>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox ID="txtnoBPJS" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-horizontal">
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Alamat :</label>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtAlamat" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kota :</label>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtKota" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="form-inline">
                                                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Kota Lahir :</label>
                                                                    <div class="col-sm-5">
                                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtKotaLhr" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tgl Lahir :</label>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtTglLahir" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Golongan Darah :</label>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox ID="txtGoldarah" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Telpon :</label>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox ID="txtTelp" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Keterangan :</label>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox ID="txtUraian" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                              <div class="tab-pane" id="tab-piutang">
                                                <div class="row">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="grdPiutSiswa" SkinID="GridView" runat="server">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                                    <ItemTemplate>
                                                                        <div class="text-center">
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="tglPeriksa" HeaderStyle-CssClass="text-center" HeaderText="Tgl Periksa" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                                                                <asp:BoundField DataField="anamnesa" HeaderStyle-CssClass="text-center" HeaderText="Anamnesa" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="diagnosa" HeaderStyle-CssClass="text-center" HeaderText="Diagnosa" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="tindakan" HeaderStyle-CssClass="text-center" HeaderText="Tindakan / Terapi" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="tinggibadan" HeaderStyle-CssClass="text-center" HeaderText="Tinggi" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="beratbadan" HeaderStyle-CssClass="text-center" HeaderText="Berat" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="suhubadan" HeaderStyle-CssClass="text-center" HeaderText="Suhu" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="tekanandarah" HeaderStyle-CssClass="text-center" HeaderText="Tekanan Darah" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="obat" HeaderStyle-CssClass="text-center" HeaderText="Obat" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                                                                
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Back" OnClick="btnCancel_Click"></asp:Button>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

