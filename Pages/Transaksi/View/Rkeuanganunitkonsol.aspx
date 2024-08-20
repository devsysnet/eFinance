<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Rkeuanganunitkonsol.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.Rkeuanganunitkonsol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
             <div class="col-sm-6">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Dari Periode :   </label>
                              <asp:DropDownList ID="cboMonth" runat="server" Width="100">
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
                                 <asp:DropDownList ID="cboYear1" runat="server" Width="90">
                               </asp:DropDownList>
                               <label>Sd :   </label>
                               <asp:DropDownList ID="cboMonth1" runat="server" Width="100">
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
                               <asp:DropDownList ID="cboYear" runat="server" Width="90">
                               </asp:DropDownList>
                                <asp:DropDownList ID="cboType" runat="server" Width="100">
                               <asp:ListItem Value="1">Detail</asp:ListItem>
                               <asp:ListItem Value="2">Rekap</asp:ListItem>
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
            <div class="col-sm-12 overflow-x-table" id="detail">
                <asp:GridView ID="grdAccount" DataKeyNames="kdrek" SkinID="GridView" runat="server">
                        <Columns>
                          <asp:TemplateField HeaderText="COA" SortExpression="Ket" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Ket" runat="server" Text='<%# Eval("kdrek")%>'></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField>   
                         <asp:TemplateField HeaderText="Remark" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Nilai" SortExpression="Jan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="COA" SortExpression="Ket" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Ket" runat="server" Text='<%# Eval("kdrek1")%>'></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField>   
                         <asp:TemplateField HeaderText="Remark" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket1").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Nilai" SortExpression="Jan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("nilai1") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>                                                     
                        </Columns>
                      </asp:GridView>
            </div>
              <div class="col-sm-12 overflow-x-table" id="rekap">
                <asp:GridView ID="grdRekap" DataKeyNames="kdrek" SkinID="GridView" runat="server">
                        <Columns>
                          <asp:TemplateField HeaderText="COA" SortExpression="Ket" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Ket" runat="server" Text='<%# Eval("kdrek")%>'></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField>   
                         <asp:TemplateField HeaderText="Remark" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Nilai" SortExpression="Jan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="COA" SortExpression="Ket" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Ket" runat="server" Text='<%# Eval("kdrek1")%>'></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField>   
                         <asp:TemplateField HeaderText="Remark" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Ket" runat="server" Text='<%# Eval("Ket1").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Nilai" SortExpression="Jan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("nilai1") %>'></asp:Label>
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
