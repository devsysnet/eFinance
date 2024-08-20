<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstKursPajak.aspx.cs" Inherits="eFinance.Pages.Master.Input.MstKursPajak" %>

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
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">No Keputusan</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNoKeputusan"  type="text"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Tanggal Peraturan</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox runat="server" CssClass="form-control date" ID="dtTgl1" type="text"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Periode Pajak</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox runat="server" CssClass="form-control date" ID="dtTgl2" type="text" ></asp:TextBox>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
