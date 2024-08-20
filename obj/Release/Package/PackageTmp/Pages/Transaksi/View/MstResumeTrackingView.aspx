<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstResumeTrackingView.aspx.cs" Inherits="eFinance.Pages.Master.View.MstResumeTrackingView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnnoYysn" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                                 <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Width="300"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                                 <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdSiswa"  SkinID="GridView" runat="server" AllowPaging="False" PageSize="100" OnPageIndexChanging="grdSiswa_PageIndexChanging"
                                         OnRowDataBound="grdSiswa_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                        <asp:HiddenField ID="hdnJmlTunggakan" runat="server" Value='<%# Bind("tanda") %>' />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Unit" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnik" runat="server" Text='<%# Bind("namaCabang") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="COA" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnis" runat="server" Text='<%# Bind("Ket") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tanggal Terakhir Input Transaksi Masuk" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnisn" runat="server" Text='<%# Bind("tglmasuk","{0:dd MMMM, yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tanggal Terakhir Input Transaksi Keluar" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNamaSiswa" runat="server" Text='<%# Bind("tglkeluar","{0:dd MMMM, yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tanggal Posting" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblkelas" runat="server" Text='<%# Bind("tglposting","{0:dd MMMM, yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Tanggal Terakhir Input" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbllokasiUnit" runat="server" Text='<%# Bind("createdBy","{0:dd MMMM, yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <%--  <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="text-center">
                                                   
                                                </div>
                                            </div>
                                </div>--%>
                        </div>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-12">
                                    <div class="panel">
                                        <div class="panel-body" id="Tabs" role="tabpanel">
                                            <!-- Nav tabs -->
                                            <ul class="nav nav-tabs" role="tablist">
                                                <li class="active">
                                                    <a href="#tab-umum" data-toggle="tab" role="tab">Umum
                                                    </a>
                                                </li>
                                                <li>
                                                    <a href="#tab-piutang" data-toggle="tab" role="tab">Pembayaran</a>
                                                </li>
                                                <li>
                                                    <a href="#tab-akun" data-toggle="tab" role="tab">Akun</a>
                                                </li>
                                            </ul>
                                            <!-- Tab panes -->
                                         
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
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>