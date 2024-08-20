<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Rkeuanganunit.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.Rkeuanganunit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
             <div class="col-sm-6">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Periode :   </label>
                         <asp:DropDownList ID="cboYear" runat="server" Width="100">
                        </asp:DropDownList>
                        
                     </div>
                </div>
            </div>
            
            <div class="col-sm-6">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Filter : </label>
                         <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Width="250"></asp:DropDownList>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click"></asp:Button>
                        <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdAccount" DataKeyNames="kdrek" SkinID="GridView" runat="server">
                        <Columns>
                          <asp:TemplateField HeaderText="COA" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Ket" runat="server" Text='<%# Eval("kdrek")%>'></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField>   
                         <asp:TemplateField HeaderText="Remark" SortExpression="Ket" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Jan" SortExpression="Jan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("Jan") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                            <asp:TemplateField HeaderText="Feb" SortExpression="Feb" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("Feb") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField> 
                           <asp:TemplateField HeaderText="Mar" SortExpression="Mar" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("Mar") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>  
                           <asp:TemplateField HeaderText="Apr" SortExpression="Apr" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("Apr") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="Mei" SortExpression="Mei" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("Mei") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField> 
                          <asp:TemplateField HeaderText="Jun" SortExpression="Jun" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("Jun") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField> 
                           <asp:TemplateField HeaderText="Jul" SortExpression="Jul" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("Jul") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                           <asp:TemplateField HeaderText="Ags" SortExpression="Ags" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("Ags") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="Sep" SortExpression="Sep" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("Sep") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="Okt" SortExpression="Okt" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("Okt") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Nov" SortExpression="Nov" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("Nov") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Des" SortExpression="Des" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("Des") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>                                               
                        </Columns>
                      </asp:GridView>
            </div>
        </div>
    </div>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>

<%--     <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, e) {
            $('#BodyContent_grdAccount').gridviewScroll({
                width: '99%',
                height: 400,
                freezesize: 2, // Freeze Number of Columns. 
            });
        });
    </script>--%>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
