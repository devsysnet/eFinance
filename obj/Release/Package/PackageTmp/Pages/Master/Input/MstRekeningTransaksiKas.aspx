<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstRekeningTransaksiKas.aspx.cs" Inherits="eFinance.Pages.Master.Input.MstRekeningTransaksiKas" %>
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
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Transaksi</label>
                                <div class="col-sm-5">
                                    <asp:DropDownList ID="cboLokasi" runat="server">
                                    </asp:DropDownList>
                                </div>
                                
                            </div>
                        </div>
                        <div class="col-md-3">
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
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Rekening" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                     <asp:DropDownList ID="cboRekening" runat="server"></asp:DropDownList>
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
                    <asp:Button runat="server" ID="btnAdd" CssClass="btn btn-success" Text="Add Row" OnClick="btnAdd_Click"></asp:Button>
                    <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click"></asp:Button>
                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>

 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
