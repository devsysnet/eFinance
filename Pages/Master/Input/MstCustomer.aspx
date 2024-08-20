<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstCustomer.aspx.cs" Inherits="eFinance.Pages.Master.Input.MstCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnTabName" runat="server"></asp:HiddenField>
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
            var tabName = $("#<%=hdnTabName.ClientID%>").val();
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("#<%=hdnTabName.ClientID%>").val($(this).attr("href").replace("#", ""));
            });
        };
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-body" id="Tabs" role="tabpanel">
                            <!-- Nav tabs -->
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="active">
                                    <a href="#tab-Company" data-toggle="tab" role="tab">Data Umum
                                    </a>
                                </li>
                                <li>
                                    <a href="#tab-Transaction" data-toggle="tab" role="tab">Delivery Address</a>
                                </li>
                                <li>
                                    <a href="#tab-Kontak" data-toggle="tab" role="tab">Kontak Person</a>
                                </li>
                                <li>
                                    <a href="#tab-Document" data-toggle="tab" role="tab">Upload Document</a>
                                </li>
                            </ul>
                            <!-- Tab panes -->
                            <div class="tab-content tab-content-bordered">
                                <div class="tab-pane active" id="tab-Company">
                                    <div class="row">
                                        <fieldset class="fsStyle">
                                            <legend class="legendStyle"><b>Data Umum</b></legend>
                                            <div class="col-sm-6">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Nama <span class="mandatory">*</span></label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNama" type="text" placeholder="Enter Name"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Nama Alias</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNamaAlias" type="text" placeholder="Enter Name Alias"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Alamat</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtAlamat" TextMode="MultiLine" type="text"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Kota</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtKota" type="text" placeholder="Enter Kota"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Telepon</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control phone" ID="txtTelp" type="text" placeholder="Enter Telpon"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Nomor Fax</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control phone" ID="txtNoFax" type="text" placeholder="Enter Nomor Fax"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Email</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" type="text" placeholder="Enter Email"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Website</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtWebsite" type="text" placeholder="Enter Website"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="form-inline">
                                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Term of Payment</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox runat="server" CssClass="form-control" Width="150" ID="txtTerm" type="text" placeholder="Enter Term of Payment"></asp:TextBox>
                                                                <span>Hari</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Kredit Limit</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtKredit" type="text" placeholder="Enter Kredit"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Salesman</label>
                                                        <div class="col-sm-3">
                                                            <asp:DropDownList ID="cboSalesman" CssClass="form-control" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Group Customer</label>
                                                        <div class="col-sm-3">
                                                            <asp:DropDownList ID="cboGroupCust" CssClass="form-control" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Cetak PPN</label>
                                                        <div class="col-sm-3">
                                                            <asp:DropDownList ID="cboCetak" CssClass="form-control" runat="server">
                                                                <asp:ListItem Value="0">Tidak</asp:ListItem>
                                                                <asp:ListItem Value="1">Ya</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Alamat Koresponden</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtAlamatKores" TextMode="MultiLine" type="text"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Kota</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtKota2" type="text" placeholder="Enter Kota"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Telepon</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control phone" ID="txtTelpKores" type="text" placeholder="Enter Telpon"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Nomor Fax</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNoFax2" type="text" placeholder="Enter Nomor Fax"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Status Pajak</label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList ID="cboSts" CssClass="form-control" runat="server">
                                                                <asp:ListItem Value="1">Ya</asp:ListItem>
                                                                <asp:ListItem Value="0">Tidak</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">No NIK</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNIK" type="text" placeholder="Enter No NIK"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">No NPWP <span class="mandatory">*</span></label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNoNPWP" type="text" placeholder="Enter No NPWP"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Nama NPWP</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNamaNPWP" type="text" placeholder="Enter Nama NPWP"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Alamat NPWP</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtAlamatNPWP" TextMode="MultiLine" type="text"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Kota</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtKota3" type="text" placeholder="Enter Kota"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Tanggal NPWP</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control date" ID="dtNPWP"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">No PKP</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNoPKP" type="text" placeholder="Enter No PKP"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Tanggal PKP</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" CssClass="form-control date" ID="dtPKP"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Status WAPU</label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList ID="cboStsWAPU" class="form-control" runat="server">
                                                                <asp:ListItem Value="0">Tidak</asp:ListItem>
                                                                <asp:ListItem Value="1">Ya</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="tab-Transaction">
                                    <div class="row">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdDelivery" SkinID="GridView" runat="server">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" HeaderText="Delivery Address">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <asp:TextBox ID="txtDelivery" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" HeaderText="Region">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <asp:TextBox ID="txtRegion" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" HeaderText="Phone">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" HeaderText="Contact">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <asp:TextBox ID="txtContact" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" HeaderText="Gudang">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <asp:TextBox ID="txtGudang" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="text-center">
                                                    <asp:Button runat="server" ID="btnAdd" CssClass="btn btn-primary" Text="AddRow" OnClick="btnAdd_Click"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="tab-Kontak">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <!---legen1--->
                                            <fieldset class="fsStyle">
                                                <legend class="legendStyle"><b>Kontak</b></legend>
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grdContact" SkinID="GridView" runat="server" ShowHeader="false" GridLines="None">
                                                        <Columns>
                                                            <asp:TemplateField ShowHeader="false">
                                                                <ItemTemplate>

                                                                    <div class="row">
                                                                        <div class="col-sm-12">
                                                                            <div class="col-sm-6">
                                                                                <div class="form-horizontal">
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label text-right">Nama</label>
                                                                                        <div class="col-sm-5">
                                                                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label text-right">Tanggal Lahir </label>
                                                                                        <div class="col-sm-5">
                                                                                            <asp:TextBox ID="dtLahir" runat="server" CssClass="form-control date"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label text-right">Alamat </label>
                                                                                        <div class="col-sm-5">
                                                                                            <asp:TextBox ID="txtAlamat" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label text-right">No. HP </label>
                                                                                        <div class="col-sm-5">
                                                                                            <asp:TextBox ID="txtNomorHP" runat="server" CssClass="form-control phone"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-horizontal">
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-4 control-label text-right">Part </label>
                                                                                        <div class="col-sm-5">
                                                                                            <asp:TextBox ID="txtBagian" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-4 control-label text-right">Jabatan </label>
                                                                                        <div class="col-sm-5">
                                                                                            <asp:TextBox ID="txtJabatan" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-4 control-label text-right">No. Telp </label>
                                                                                        <div class="col-sm-5">
                                                                                            <asp:TextBox ID="txtTelp" runat="server" CssClass="form-control phone"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-4 control-label text-right">Email </label>
                                                                                        <div class="col-sm-5">
                                                                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="text-center">
                                                                <asp:Button ID="btnAddContact" runat="server" CssClass="btn btn-success" Text="Add Row" OnClick="btnAddContact_Click" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </fieldset>
                                        </div>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="tab-Document">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <!---legen1--->
                                            <fieldset class="fsStyle">
                                                <legend class="legendStyle"><b>Document</b></legend>
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grdDocument" SkinID="GridView" runat="server">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                                <ItemTemplate>
                                                                    <div class="text-center">
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Surat" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <div class="text-center">
                                                                        <asp:TextBox ID="txtNamaSurat" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Masa Berlaku Dari" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <div class="text-center">
                                                                        <div class="form-inline">
                                                                            <asp:TextBox ID="dtAwal" runat="server" CssClass="form-control date"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Masa Berlaku Ke" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <div class="text-center">
                                                                        <div class="form-inline">
                                                                            <asp:TextBox ID="dtAkhir" runat="server" CssClass="form-control date"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Upload" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <div class="form-inline">
                                                                        <label for="BodyContent_grdDocument_flUpload_<%# Container.DataItemIndex %>" class="btn">Upload</label>
                                                                        <asp:FileUpload ID="flUpload" runat="server" Style="display: none;"></asp:FileUpload>
                                                                        <span style="font-style: italic; font-weight: bold; font-size: smaller;">Max. 4 mb</span>
                                                                    </div>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="text-center">
                                                                <asp:Button ID="btnAddDocument" runat="server" CssClass="btn btn-success" Text="Add Row" OnClick="btnAddDocument_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </fieldset>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="text-center">
                                            <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click"></asp:Button>
                                            <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
