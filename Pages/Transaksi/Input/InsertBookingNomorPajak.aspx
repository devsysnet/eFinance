<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="InsertBookingNomorPajak.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.InsertBookingNomorPajak" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabAksesoris" runat="server">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">Date <span class="mandatory">*</span></label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="dtDate" runat="server" CssClass="form-control date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">Booking Number <span class="mandatory">*</span></label>
                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtNoSurat" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">Booking Tax Date <span class="mandatory">*</span></label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="dtSurat" runat="server" CssClass="form-control date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="form-inline">
                                        <label class="col-sm-3 control-label">Booking Tax <span class="mandatory">*</span></label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtKolom1" runat="server" CssClass="form-control width-50"></asp:TextBox>
                                            <asp:TextBox ID="txtKolom2" runat="server" CssClass="form-control width-50"></asp:TextBox>
                                            <asp:TextBox ID="txtNoPajak" runat="server" CssClass="form-control width-80 numeric" Text="0"></asp:TextBox> To
                                            <asp:TextBox ID="txtNoPajak2" runat="server" CssClass="form-control width-80 numeric" Text="0"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-5 control-label text-right"></label>
                                    <div class="col-sm-5">
                                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-primary" Text="Save" />
                                        <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" CssClass="btn btn-danger" Text="Reset" />
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
