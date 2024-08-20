<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Rdaftarperpanjangsertif.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.Rdaftarperpanjangsertif" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">

                    <div id="tabGrid" runat="server">
                         <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdAssetUpdate" DataKeyNames="noAset" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdAssetUpdate_PageIndexChanging" OnRowCommand="grdAssetUpdate_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                    <asp:HiddenField ID="noAset" runat="server" Value='<%# Bind("noAset") %>' />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="tglAsset" HeaderText="Tanggal_Asset" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="tgljthtempo" HeaderText="Tanggal Jatuh Tempo" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%"  ItemStyle-HorizontalAlign="left" DataFormatString="{0:dd-MMM-yyyy}"/>
                                            <asp:BoundField DataField="namaAsset" HeaderText="Nama Aset" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left"  />
                                            <asp:BoundField DataField="penggunaan" HeaderText="Penggunaan" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="kota" HeaderText="Kota" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="left"/>
                                            <asp:BoundField DataField="nomorids" HeaderText="Nomor Sertifikat" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="left"/>
                                            <asp:BoundField DataField="nilaiPerolehan" HeaderText="Nilai Perolehan" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="right" DataFormatString="{0:N}"/>                                         
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
