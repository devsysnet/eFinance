<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="UserInsert.aspx.cs" Inherits="eFinance.Pages.Master.Input.UserInsert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-horizontal">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Cabang <span class="mandatory">*</span></label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="cboCabang" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Nama Lengkap <span class="mandatory">*</span></label>
                                            <div class="col-sm-7">
                                                <div class="input-group">
                                                    <div class="form-inline">
                                                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" MaxLength="50" placeholder="Full Name"></asp:TextBox>
                                                        &nbsp;<asp:ImageButton ID="btnKaryawan" runat="server" ImageUrl="~/assets/images/icon_search.png" CssClass="btn-image" OnClick="btnKaryawan_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="form-inline">
                                                <label class="col-sm-3 control-label text-right">User ID <span class="mandatory">*</span></label>
                                                <div class="controls col-sm-4">
                                                    <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control" MaxLength="10" placeholder="User ID"></asp:TextBox>
                                                    <span class="font-det">Max. 10 Digits</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Group User <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:ImageButton ID="btnGroup" CssClass="btn-image" runat="server" ImageUrl="~/assets/images/icon_search.png" OnClick="btnGroup_Click" CausesValidation="false" />
                                                <asp:GridView ID="grdGroupUser" DataKeyNames="noAkses" SkinID="GridView" runat="server">
                                                    <Columns>
                                                        <asp:BoundField DataField="hakAkses" HeaderStyle-CssClass="text-center" HeaderText="Akses" ItemStyle-Width="100%" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label"></label>
                                            <div class="col-sm-5">
                                                <div class="checkbox">
                                                    <label>
                                                        <asp:CheckBox ID="chkIsActive" runat="server" CssClass="checked" />
                                                        <span class="lbl">Aktif</span>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label text-right">Password <span class="mandatory">*</span></label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" MaxLength="20" TextMode="Password" placeholder="Password"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label text-right">Konf. Password <span class="mandatory">*</span></label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" MaxLength="20" TextMode="Password" placeholder="Konfirmasi Password"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Alert <span class="mandatory">*</span></label>
                                            <div class="col-sm-9">
                                                <asp:ImageButton ID="btnAlert" CssClass="btn-image" runat="server" ImageUrl="~/assets/images/icon_search.png" OnClick="btnAlert_Click" CausesValidation="false" />
                                                <asp:GridView ID="grdTaskAlert" DataKeyNames="noTaskAlert" SkinID="GridView" runat="server">
                                                    <Columns>
                                                        <asp:BoundField DataField="namaTask" HeaderStyle-CssClass="text-center" HeaderText="Alert" ItemStyle-Width="100%" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Email</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" type="email" MaxLength="100" placeholder="Email"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Telpon</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtNoTelp" runat="server" CssClass="form-control phone" type="phone" MaxLength="100" placeholder="Enter Phone Number"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Keterangan</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtKeterangan" runat="server" CssClass="form-control" MaxLength="200" placeholder="User Description" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Photo </label>
                                            <div class="col-sm-4">
                                                <asp:FileUpload ID="flUpload" runat="server" Class="fileupload" />
                                            </div>
                                        </div>
                                        
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="text-center">
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"/>
                                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click" CausesValidation="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <asp:LinkButton ID="LinkButton2" runat="server" Style="display: none"></asp:LinkButton>
            <asp:ModalPopupExtender ID="dlgKaryawan" runat="server" PopupControlID="panelAddDataKaryawan" TargetControlID="LinkButton2" BackgroundCssClass="modal-background">
            </asp:ModalPopupExtender>
            <asp:Panel ID="panelAddDataKaryawan" runat="server" align="center" Style="display: none" CssClass="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Data Karyawan</h4>
                    </div>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    <div class="modal-body ">
                        <div class="modal-body col-overflow-400">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group ">
                                        <div class="form-inline">
                                            <label class="col-sm-3 control-label">Cari</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtSearchKaryawan" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                <asp:Button ID="btnSearchKaryawan" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearchKaryawan_Click" CausesValidation="false" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="grdDataKaryawan" DataKeyNames="nokaryawan" ShowFooter="true" SkinID="GridView" runat="server" OnSelectedIndexChanged="grdDataKaryawan_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnNoUserD" runat="server" Value='<%# Bind("nokaryawan") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="idPeg" HeaderStyle-CssClass="text-center" HeaderText="ID Peg" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="nama" HeaderStyle-CssClass="text-center" HeaderText="Nama User" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-primary" Text="Pilih" CausesValidation="false" CommandName="Select" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:LinkButton ID="LinkButton4" runat="server" Style="display: none"></asp:LinkButton>
            <asp:ModalPopupExtender ID="dlgtaskAlert" runat="server" PopupControlID="panel2" TargetControlID="LinkButton4" BackgroundCssClass="modal-background">
            </asp:ModalPopupExtender>
            <asp:Panel ID="panel2" runat="server" align="center" Width="60%" Style="display: none" CssClass="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Task Alert</h4>
                        <asp:HiddenField runat="server" ID="HiddenField1" />
                    </div>
                    <div class="row">
                        <div class="col-sm-7">
                        </div>
                        <div class="col-sm-5">
                            <div class="form-inline">
                                <div class="col-sm-12">
                                    <asp:TextBox ID="txtSearchTask" runat="server" CssClass="form-control" MaxLength="50" placeholder="Search..."></asp:TextBox>
                                    <asp:Button ID="btnSearchTask" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearchTask_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-body col-overflow-500">
                        <div class="table-responsive">
                            <asp:GridView ID="grsTaskAlert" DataKeyNames="noTaskAlert" SkinID="GridView" runat="server" AllowPaging="true" PageSize="100" OnPageIndexChanging="grsTaskAlert_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <%# Container.DataItemIndex + 1 %>
                                                <asp:HiddenField ID="hdnNoTaskAlert" runat="server" Value='<%#Eval("noTaskAlert")%>' />
                                                <asp:HiddenField ID="hdnStsPilih" runat="server" Value='<%# Eval("stsPilih") %>' />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Task Alert" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <div class="text-left">
                                                <asp:Label runat="server" ID="lblKdProd" Text='<%#Eval("namaTask")%>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelectSub" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="Button4" runat="server" Text="Submit" CssClass="btn btn-primary" CausesValidation="false" OnClick="Button4_Click" />
                    </div>
                </div>
            </asp:Panel>

            <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>
            <asp:ModalPopupExtender ID="dlgGroup" runat="server" PopupControlID="panel1" TargetControlID="LinkButton1" BackgroundCssClass="modal-background">
            </asp:ModalPopupExtender>
            <asp:Panel ID="panel1" runat="server" align="center" Width="60%" Style="display: none" CssClass="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Group User</h4>
                    </div>
                    <div class="row">
                        <div class="col-sm-7">
                        </div>
                        <div class="col-sm-5">
                            <div class="form-inline">
                                <div class="col-sm-12">
                                    <asp:TextBox ID="txtSearchGroup" runat="server" CssClass="form-control" MaxLength="50" placeholder="Search..."></asp:TextBox>
                                    <asp:Button ID="btnSearchGroup" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearchGroup_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-body col-overflow-500">
                        <div class="table-responsive">
                            <asp:GridView ID="grdGroup" DataKeyNames="noAkses" SkinID="GridView" runat="server" AllowPaging="true" PageSize="100" OnPageIndexChanging="grdGroup_PageIndexChanging" OnRowDataBound="grdGroup_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <%# Container.DataItemIndex + 1 %>
                                                <asp:HiddenField ID="hdnNoAkses" runat="server" Value='<%#Eval("noAkses")%>' />
                                                <asp:HiddenField ID="hdnStsPilih" runat="server" Value='<%# Eval("stsPilih") %>' />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Hak Akses" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <div class="text-left">
                                                <asp:Label runat="server" ID="lblKdProd" Text='<%#Eval("hakAkses")%>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelectSub" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="Button2" runat="server" Text="Submit" CssClass="btn btn-primary" CausesValidation="false" OnClick="Button2_Click" />
                    </div>
                </div>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
