<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="UserView.aspx.cs" Inherits="eFinance.Pages.Master.View.UserView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <asp:HiddenField ID="hdnFileImage" runat="server" />
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
                                    <asp:GridView ID="grdUserView" DataKeyNames="noUser" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdUserView_PageIndexChanging"
                                        OnSelectedIndexChanged="grdUserView_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="userid" HeaderText="User ID" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="namaUser" HeaderText="Nama Lengkap" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="namacabang" HeaderText="Perwakilan/Unit" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
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
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Cabang <span class="mandatory">*</span></label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="cboCabang" runat="server" Enabled="false" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Nama Lengkap <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtFullName" runat="server" Enabled="false" CssClass="form-control" MaxLength="50" placeholder="Full Name"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="form-inline">
                                            <label class="col-sm-3 control-label text-right">User ID <span class="mandatory">*</span></label>
                                            <div class="controls col-sm-4">
                                                <asp:TextBox ID="txtUserID" runat="server" Enabled="false" CssClass="form-control" MaxLength="10" placeholder="User ID"></asp:TextBox>
                                                <span class="font-det">Max. 10 Digits</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Group User <span class="mandatory">*</span></label>
                                        <div class="col-sm-6">
                                            <asp:GridView ID="grdGroupUser" DataKeyNames="noAkses" SkinID="GridView" runat="server">
                                                <Columns>
                                                    <asp:BoundField DataField="hakAkses" HeaderStyle-CssClass="text-center" HeaderText="Akses" ItemStyle-Width="100%" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Photo <span class="mandatory">*</span></label>
                                        <div class="col-sm-6">
                                            <div class="profile-photo">
                                                <div class="profile-photo">
                                                    <asp:Image ID="imgItem" runat="server" CssClass="photo" Width="100" Height="100" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label"></label>
                                        <div class="col-sm-5">
                                            <div class="checkbox">
                                                <label>
                                                    <asp:CheckBox ID="chkIsActive" Enabled="false" runat="server" CssClass="checked" />
                                                    <span class="lbl">Active</span>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Alert <span class="mandatory">*</span></label>
                                        <div class="col-sm-9">
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
                                            <asp:TextBox ID="txtEmail" runat="server" Enabled="false" CssClass="form-control" type="email" MaxLength="100" placeholder="Email"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Telp</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtNoTelp" runat="server" Enabled="false" CssClass="form-control phone" type="phone" MaxLength="100" placeholder="Enter Phone Number"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Keterangan</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtKeterangan" runat="server" Enabled="false" CssClass="form-control" MaxLength="200" placeholder="User Description" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>        
                            </div>
                        </div>
                        
                        <div class="form-group row">
                            <div class="col-sm-12">
                                <div class="text-center">
                                    <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click" CausesValidation="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
