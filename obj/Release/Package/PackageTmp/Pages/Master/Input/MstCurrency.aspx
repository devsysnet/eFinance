<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstCurrency.aspx.cs" Inherits="eFinance.Pages.Master.Input.MstCurrency" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-group row">
                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Currency Name :</label>
                            <div class="col-sm-4">
                                <asp:TextBox runat="server" CssClass="form-control" required ID="txtCurrencyName" type="text" placeholder="Enter Currency Name"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Code :</label>
                            <div class="col-sm-4">
                                <asp:TextBox runat="server" CssClass="form-control" required ID="txtCode" type="text" placeholder="Enter Code"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Country :</label>
                            <div class="col-sm-4">
                                <asp:TextBox runat="server" CssClass="form-control" required ID="txtCountry" type="text" placeholder="Enter Country"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Currency Status :</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="cboStatus" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="1">Aktif</asp:ListItem>
                                    <asp:ListItem Value="0">Non Aktif</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Set As Default :</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="cboSet" CssClass="form-control" runat="server" Enabled="false">
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group row">
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

</asp:Content>
