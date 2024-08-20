<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MSupplieUpdater.aspx.cs" Inherits="eFinance.Pages.Master.Update.MSupplieUpdater" %>
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
                                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" Text="Delete / Delete all"  OnClientClick="return DeleteAll()" CausesValidation="false" />
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
                                    <asp:GridView ID="grdCabang" DataKeyNames="nosupplier" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdCabang_PageIndexChanging"
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
                                           <asp:BoundField DataField="kodesupplier" HeaderText="Kode" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="namasupplier" HeaderText="Nama" HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="alamat" HeaderText="Alamat" HeaderStyle-CssClass="text-center" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="left" />
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
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kode Supplier <span class="mandatory">*</span></label>
                                            <div class="col-sm-3">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtKode" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama Supplier <span class="mandatory">*</span></label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtnamaSupplier" ></asp:TextBox>
                                            </div>
                                        </div>
                                         <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Supplier</label>
                                            <div class="col-sm-3">
                                                 <asp:DropDownList ID="cbosupp" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Alamat</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtAlamat" TextMode="MultiLine" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">NPWP </label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtnpwp" ></asp:TextBox>
                                            </div>
                                        </div>
                                        
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Alamat NPWP</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtAlamatNPWP" TextMode="MultiLine" ></asp:TextBox>
                                            </div>
                                        </div>
                                         <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kota</label>
                                            <div class="col-sm-3">
                                                 <asp:DropDownList ID="cboKota" runat="server">
                                                  </asp:DropDownList>
                                            </div>
                                        </div>
                                         <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Telp </label>
                                            <div class="col-sm-3">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txttelpKantor" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Fax </label>
                                            <div class="col-sm-3">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtFax" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Email </label>
                                            <div class="col-sm-3">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtemail" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama PIC </label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtnamaPIC" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Telp PIC </label>
                                            <div class="col-sm-3">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txttelpPIC" ></asp:TextBox>
                                            </div>
                                        </div>
                                         <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">No Account </label>
                                            <div class="col-sm-3">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtnoaccount"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Bank </label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtBank" ></asp:TextBox>
                                            </div>
                                        </div>
                                         <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterangan 1</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="keterangan" TextMode="MultiLine" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterangan 2</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="keterangan1" TextMode="MultiLine" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterangan 3</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="keterangan2" TextMode="MultiLine" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="text-center">
                                                     <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"/>
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