<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstKursPembukuan.aspx.cs" Inherits="eFinance.Pages.Master.Input.MstKursPembukuan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-6">
                            <div class="form-horizontal">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="col-sm-6">
                                            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control" ID="cboBulan">
                                                <asp:ListItem Value="1">Januari</asp:ListItem>
                                                <asp:ListItem Value="2">Februari</asp:ListItem>
                                                <asp:ListItem Value="3">Maret</asp:ListItem>
                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                <asp:ListItem Value="5">Mei</asp:ListItem>
                                                <asp:ListItem Value="6">Juni</asp:ListItem>
                                                <asp:ListItem Value="7">Juli</asp:ListItem>
                                                <asp:ListItem Value="8">Agustus</asp:ListItem>
                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                <asp:ListItem Value="10">Oktober</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">Desember</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control" ID="cboTahun">
                                                <asp:ListItem Value="2017">2017</asp:ListItem>
                                                <asp:ListItem Value="2018">2018</asp:ListItem>
                                                <asp:ListItem Value="2019">2019</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                    </div>
                    <div class="col-md-6">
                        <div class="table-responsive">
                            <asp:GridView ID="grdInstansi" SkinID="GridView" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <%# Container.DataItemIndex + 1 %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Mata Uang" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:Label ID="lblCurrency" runat="server" CssClass="form-label"></asp:Label>
                                                <asp:HiddenField ID="hdnCurrency" runat="server" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Kurs" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox ID="txtInstansi" runat="server" CssClass="form-control money"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-md-3">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="col-md-12">
            <div class="text-center">
                <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click"></asp:Button>
                <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Reset"></asp:Button>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
