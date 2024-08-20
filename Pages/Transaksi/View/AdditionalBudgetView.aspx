<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="AdditionalBudgetView.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.AdditionalBudgetView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
             <div class="col-sm-5">
                <div class="form-inline">
                    <div class="col-sm-10">
                        <label>Periode :   </label>
                        <asp:DropDownList ID="cboMonth" runat="server" Width="120">
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
                        <asp:DropDownList ID="cboYear" runat="server" Width="100">
                        </asp:DropDownList>
                        
                     </div>
                </div>
            </div>
            
            <div class="col-sm-7">
                <div class="form-inline">
                    <div class="col-sm-15">
                        <label>Cabang : </label>
                         <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Enabled="true" Width="180"></asp:DropDownList>
                        <asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Search" OnClick="btnPosting_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdAccount" DataKeyNames="kdAdditional" SkinID="GridView" runat="server" >
                         <Columns>
                         <asp:TemplateField HeaderText="Kode Transaksi" SortExpression="Ket" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="center">
                          <ItemTemplate>
                          <asp:Label ID="Ket" runat="server" Text='<%# Eval("kdAdditional").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Tanggal" SortExpression="tgl" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="ptd"  runat="server" Text='<%# Eval("tgl") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField> 
                          <asp:TemplateField HeaderText="Budget" SortExpression="Ket" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField>
                             <asp:TemplateField HeaderText="Remaks" SortExpression="Ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Ket" runat="server" Text='<%# Eval("remaks").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField>
                           <asp:TemplateField HeaderText="Nilai" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="ptd"  runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>  
               
                        </Columns>
                      </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
