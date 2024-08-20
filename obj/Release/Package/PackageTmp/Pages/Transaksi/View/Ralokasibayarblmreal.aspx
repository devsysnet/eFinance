<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Ralokasibayarblmreal.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.Ralokasibayarblmreal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>
    <asp:HiddenField ID="hdnnoYysn" runat="server" />
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
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
                            <div class="col-sm-12 overflow-x-table">
                                <asp:GridView ID="grdAccount" DataKeyNames="namaCabang" SkinID="GridView" runat="server">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Unit" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="namacabang" runat="server" Text='<%# Eval("namaCabang").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tanggal" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("payment_date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Nilai Transaksi" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:Label ID="kredit" runat="server" Text='<%# Eval("amount") %>'></asp:Label>
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
  </ContentTemplate>
     <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
 </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

