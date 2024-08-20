<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RRekaptransaksi1.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RRekaptransaksi1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                         <div class="row">
                            <div class="col-sm-12">
                                <div class="form-horizontal">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-1 control-label">Periode :</label>
                                            <div class="col-sm-6">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="dtMulai" runat="server" CssClass="form-control date"></asp:TextBox>&nbsp; 
                                                    <asp:TextBox ID="dtSampai" runat="server" CssClass="form-control date"></asp:TextBox>&nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-horizontal">
                                     <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Cabang : </label>
                                            <div class="col-sm-10">
                                               <asp:DropDownList ID="cboCabang" runat="server" CssClass="form-control" Width="350" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Transaksi : </label>
                                            <div class="col-sm-10">
                                                <div class="input-group">
                                                    <div class="form-inline">
                                                        <div class="input-group-btn">
                                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cbotransaksi" Width="150"></asp:DropDownList>
                                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdRekap" DataKeyNames="noTransaksi" SkinID="GridView" runat="server" AllowPaging="false" PageSize="10" OnPageIndexChanging="grdRekap_PageIndexChanging" OnRowCommand="grdRekap_RowCommand">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="0%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%--<%# Container.DataItemIndex + 1 %>--%>
                                                   </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nomorkode" HeaderText="Kode Transaksi" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="Tgl" HeaderText="Tanggal" HeaderStyle-CssClass="text-center" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="namaSiswa" HeaderText="Nama" HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="kelas" HeaderText="Kelas" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="bln" HeaderText="Kelas" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="nilai" HeaderText="Nilai" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
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

    <%--</asp:Panel>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

