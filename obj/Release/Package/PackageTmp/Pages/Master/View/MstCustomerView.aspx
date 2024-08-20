<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstCustomerView.aspx.cs" Inherits="eFinance.Pages.Master.View.MstCustomerView" %>
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
                <div class="form-horizontal">
                    <div class="col-sm-12">
                        <div class="form-group ">
                            <div class="form-inline">
                                <div class="col-sm-8">
                                    <div class="form-inline">
                                        <label class="col-sm-1 control-label">Search</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                            <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnCari_Click" CausesValidation="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="grdCustomer" DataKeyNames="noCust" SkinID="GridView" runat="server" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdCustomer_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="kdCust" SortExpression="ManufactureCode" HeaderText="Kode Customer" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="namaCust" SortExpression="ManufactureName" HeaderText="Nama Customer" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="AgamaCust" SortExpression="ManufactureName" HeaderText="Agama Customer" ItemStyle-Width="20%" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <a href="javascript:void()" class="btn btn-primary btn-sm" onclick="return EditRow('<%#Eval("noCust")%>')">Detail</a>
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
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" id="tab-Company">
                            <div class="row" style="margin-top: 10px;">
                                <fieldset class="fsStyle">
                                    <legend class="legendStyle"><b>General Data</b></legend>
                                    <div class="col-sm-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Nama <span class="mandatory">*</span></label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="txtNama" placeholder="Enter Name"></asp:Label>
                                                    <asp:HiddenField ID="hdnNoCust" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Nama Alias</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="txtNamaAlias" placeholder="Enter Name Alias"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Alamat</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="txtAlamat" TextMode="MultiLine"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Agama</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5 " ID="txtAgama" placeholder="Enter Agama"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Telpon</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="txtTelp" placeholder="Enter Telpon"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Nomor Fax</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="txtNoFax" placeholder="Enter Nomor Fax"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Email</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="txtEmail" placeholder="Enter Email"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Website</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="txtWebsite" placeholder="Enter Website"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="form-inline">
                                                    <label class="col-sm-5 control-label text-right">TOP</label>
                                                    <div class="col-sm-6">
                                                        <asp:Label runat="server" CssClass="col-sm-5" ID="txtTerm" placeholder="Enter Term of Payment"></asp:Label>Hari
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Kredit</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="txtKredit" placeholder="Enter Kredit"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Salesman</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="cboSalesman" CssClass="col-sm-5" runat="server" Enabled="false"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Group Cust</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="cboGroupCust" CssClass="form-control" runat="server" Enabled="false"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Cetak</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="cboCetak" CssClass="form-control" runat="server" Enabled="false">
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
                                                <label class="col-sm-5 control-label text-right">Alamat Kores</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="txtAlamatKores" TextMode="MultiLine"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Agama</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5 " ID="txtAgama2" placeholder="Enter Agama"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Telphone</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5 " ID="txtTelpKores" placeholder="Enter Telpon"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Nomor Fax</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="txtNoFax2" placeholder="Enter Nomor Fax"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Status</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="cboSts" CssClass="form-control" runat="server" Enabled="false">
                                                        <asp:ListItem Value="1">Ya</asp:ListItem>
                                                        <asp:ListItem Value="0">Tidak</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Nomor NPWP <span class="mandatory">*</span></label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="txtNoNPWP" placeholder="Enter No NPWP"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Nama NPWP</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="txtNamaNPWP" placeholder="Enter Nama NPWP"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Alamat NPWP</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="txtAlamatNPWP" TextMode="MultiLine"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Agama NPWP</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="txtAgama3" placeholder="Enter Agama"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Tanggal NPWP</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="dtNPWP"></asp:Label>&nbsp;
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Nomor PKP</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5 " ID="txtNoPKP" placeholder="Enter No PKP"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Tanggal PKP</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="dtPKP"></asp:Label>&nbsp;
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Status Wapu</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="cboStsWAPU" CssClass="form-control" runat="server" Enabled="false">
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
                            <fieldset class="fsStyle">
                                <legend class="legendStyle"><b>General Data</b></legend>
                                <div class="row" style="margin-top: 10px;">
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
                                                        <asp:TextBox ID="txtDelivery" runat="server" Enabled="false" CssClass="form-control" TextMode="MultiLine" Text='<%# Bind("alamat") %>'></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" HeaderText="Region">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox ID="txtRegion" runat="server" Enabled="false" CssClass="form-control" Text='<%# Bind("wilayah") %>'></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" HeaderText="Phone">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox ID="txtPhone" runat="server" Enabled="false" CssClass="form-control" Text='<%# Bind("notelp") %>'></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" HeaderText="Contact">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox ID="txtContact" runat="server" Enabled="false" CssClass="form-control" Text='<%# Bind("contact") %>'></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" HeaderText="Gudang">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:TextBox ID="txtGudang" runat="server" Enabled="false" CssClass="form-control" Text='<%# Bind("nmGudangCust") %>'></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </div>
                        <div role="tabpanel" class="tab-pane" id="tab-Kontak">
                            <div class="row" style="margin-top: 10px;">
                                <fieldset class="fsStyle">
                                    <legend class="legendStyle"><b>General Data</b></legend>
                                    <div class="col-sm-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Nama CP</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5 " ID="txtNamaCp" placeholder="Enter Name"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Bagian CP</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="txtBagianCP" placeholder="Enter Name"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Nama CP</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="txtAlamatCP" TextMode="MultiLine"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Jabatan CP</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="txtJabatanCP" placeholder="Enter Jabatan"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Telphone CP</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="txtNoTelpCP" placeholder="Enter No Telp"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Telphone CP</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-5" ID="txtNoHpCP" placeholder="Enter No HP"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Email CP</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" CssClass="col-sm-6" ID="txtEmailCP" placeholder="Enter Email"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Tanggal lahir CP</label>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" class="col-sm-6" ID="dtTglLahir"></asp:Label>&nbsp;
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
