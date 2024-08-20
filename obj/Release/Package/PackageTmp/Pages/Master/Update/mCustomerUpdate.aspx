<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="mCustomerUpdate.aspx.cs" Inherits="eFinance.Pages.Master.Update.mCustomerUpdate" %>
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
            var GridVwHeaderChckbox = document.getElementById("<%=grdCustomer.ClientID %>");
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
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group ">
                                        <div class="form-inline">
                                            <div class="col-sm-12">
                                                <label>Search :</label>
                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search.."></asp:TextBox>
                                                <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnCari_Click" CausesValidation="false" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="grdCustomer" DataKeyNames="noCust" SkinID="GridView" runat="server" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdCustomer_PageIndexChanging"
                    OnSelectedIndexChanged="grdCustomer_SelectedIndexChanged">
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
                        <asp:BoundField DataField="kdCust" SortExpression="ManufactureCode" HeaderText="Kode Customer" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="namaCust" SortExpression="ManufactureName" HeaderText="Nama Customer" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="KotaCust" SortExpression="ManufactureName" HeaderText="Kota Customer" ItemStyle-Width="20%" />
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
        <div class="card">
            <div class="panel panel-default">
                <div id="Tabs" role="tabpanel">
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
                        <div role="tabpanel" class="tab-pane active" id="tab-Company">
                            <div class="row">
                                <fieldset class="fsStyle">
                                    <legend class="legendStyle"><b>General Data</b></legend>
                                    <div class="col-sm-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Nama <span class="mandatory">*</span></label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNama" placeholder="Enter Name"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnNoCust" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Nama Alias</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNamaAlias" placeholder="Enter Name Alias"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Alamat</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtAlamat" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Kota</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtKota" placeholder="Enter Kota"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Telphone</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control phone" ID="txtTelp" placeholder="Enter Telpon"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Nomor fax</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNoFax" placeholder="Enter Nomor Fax"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Email</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" placeholder="Enter Email"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Website</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtWebsite" placeholder="Enter Website"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <label class="col-sm-5 control-label text-right">TOP</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtTerm" type="text" placeholder="Enter Term of Payment"></asp:TextBox>Hari
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Kredit</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtKredit" placeholder="Enter Kredit"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Salesman</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="cboSalesman" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Group customer</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="cboGroupCust" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Cetak PPN</label>
                                                <div class="col-sm-6">
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
                                                <label class="col-sm-5 control-label text-right">Alamat</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtAlamatKores" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Kota</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtKota2" placeholder="Enter Kota"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Telphone Kores</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtTelpKores" placeholder="Enter Telpon"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Nomor Fax</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNoFax2" placeholder="Enter Nomor Fax"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Status</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="cboSts" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="1">Ya</asp:ListItem>
                                                        <asp:ListItem Value="0">Tidak</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="pjs-ex1-fullname" class="col-sm-5 control-label text-right">No NIK</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNIK" type="text" placeholder="Enter No NIK"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">No. NPWP <span class="mandatory">*</span></label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNoNPWP" placeholder="Enter No NPWP"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Nama NPWP</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNamaNPWP" placeholder="Enter Nama NPWP"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Alamat NPWP</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtAlamatNPWP" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Kota</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtKota3" placeholder="Enter Kota"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Tanggal NPWP</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control date" ID="dtNPWP"></asp:TextBox>&nbsp;
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Nomor PKP</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNoPKP" placeholder="Enter No PKP"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Tanggal PKP</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control date" ID="dtPKP"></asp:TextBox>&nbsp;
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Status WAPU</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="cboStsWAPU" CssClass="form-control" runat="server">
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
                            <div class="row" style="margin-top: 10px;">
                                <fieldset class="fsStyle">
                                    <legend class="legendStyle"><b>Delivery Address</b></legend>
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
                                </fieldset>
                            </div>
                        </div>
                        <div role="tabpanel" class="tab-pane" id="tab-Kontak">
                            <div class="row" style="margin-top: 10px;">
                                <fieldset class="fsStyle">
                                    <legend class="legendStyle"><b>Kontak</b></legend>
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
                                                                            <asp:HiddenField ID="hdnNoCP" runat="server" />
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
                                </fieldset>
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
                    </div>
                    <div class="form-group row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click"></asp:Button>
                                <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click"></asp:Button>
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