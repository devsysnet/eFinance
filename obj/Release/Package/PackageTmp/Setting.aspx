<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="eFinance.Setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnFileImage" runat="server" />
       <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-body">
                            <%--START GRID--%>
                            <div id="tabGrid" runat="server">
                                <div class="row">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Branch </label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cboCabang" runat="server" CssClass="form-control" Enabled="false" Width="350">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Full Name </label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" MaxLength="50" placeholder="Full Name" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="form-inline">
                                                <label class="col-sm-3 control-label text-right">User ID </label>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control" MaxLength="10" placeholder="User ID" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Email</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="100" placeholder="Email"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Telp</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtNoTelp" runat="server" CssClass="form-control" MaxLength="100" placeholder="Enter Phone Number"></asp:TextBox>
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
                                                <asp:FileUpload ID="flUpload" Class="fileupload" runat="server" />
                                                <asp:CheckBox ID="cekGnt" runat="server" CssClass="px" AutoPostBack="true" OnCheckedChanged="cekGnt_CheckedChanged" />&nbsp;<asp:Label ID="lblec" runat="server" Text="Cek = ganti logo"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label"></label>
                                            <div class="col-sm-5">
                                                <div class="checkbox">
                                                    <label>
                                                        <asp:CheckBox ID="chkPassword" runat="server" CssClass="checked" OnCheckedChanged="chkPassword_CheckedChanged" AutoPostBack="true" />
                                                        <span class="lbl">Change Password</span>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label text-right">Password <span class="mandatory">*</span></label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" MaxLength="20" TextMode="Password" placeholder="Password"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label text-right">Confirm Password <span class="mandatory">*</span></label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" MaxLength="20" TextMode="Password" placeholder="Konfirmasi Password"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label text-right"></label>
                                            <div class="col-sm-5">
                                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="btnSave_Click" />
                                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnReset_Click" CausesValidation="false" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="tabForm" runat="server" visible="false">
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
