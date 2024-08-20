<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RdataVAbrivsSAK.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RdataVAbrivsSAK" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
     <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                      <div class="row">
                            <div class="col-sm-12">
                                <div class="form-horizontal">
                                    <div class="col-sm-7">
                                        <div class="form-group">
                                            &nbsp;&nbsp;&nbsp;<label>Perwakilan : </label>
                                            <asp:DropDownList ID="cboPerwakilan" runat="server" Width="250" CssClass="form-control" AutoPostBack="false" ></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                             <div class="col-sm-10">
                                                <div class="input-group">
                                                    <div class="form-inline">
                                                        <%--<div class="input-group-btn">--%>
                                                           <asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Search" OnClick="btnPosting_Click"></asp:Button>&nbsp;&nbsp;&nbsp;
                                                           <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>
                                                    <%--</div>--%>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:GridView ID="grdAccount" DataKeyNames="nova" SkinID="GridView" runat="server" Width="100%">

                                    <Columns>
                                        <asp:TemplateField HeaderText="KodeVA" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="nova" runat="server" Text='<%# Eval("nova").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tanggal Mutasi" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="tgl" runat="server" Text='<%# Eval("tgl").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mutasi BRI" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="mutasiBRI" runat="server" Text='<%# Eval("mutasiBRI") %>' ItemStyle-HorizontalAlign="right"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Nilai SAK" SortExpression="sak" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:Label ID="SAK" runat="server" Text='<%# Eval("SAK") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="selisih" SortExpression="sak" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:Label ID="SAK" runat="server" Text='<%# Eval("selisih") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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

