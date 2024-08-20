<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="kesanggupansiswa.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.kesanggupansiswa" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function OpenReport() {
            //            OpenReportViewer();
            window.open('../../../Report/ReportViewer.aspx', 'name', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,modal=yes,width=1000,height=600');
        }
       
    </script>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
             <div class="col-sm-5">
                <div class="form-inline">
                    <div class="col-sm-10">
                        <label>Tahun Ajaran :   </label>
                         <asp:DropDownList ID="cboYear" runat="server" Width="125">
                        </asp:DropDownList>
                      </div>
                </div>
            </div>
            <div class="col-sm-5">
                <div class="form-inline">
                          <div class="col-sm-12">
                              <asp:DropDownList ID="cboKelas" runat="server" CssClass="form-control" Width="120"></asp:DropDownList>
                              <asp:DropDownList ID="cboJnsTrans" runat="server" CssClass="form-control" Width="130"></asp:DropDownList>
                              <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Enabled="true" Width="230"></asp:DropDownList>
                               <asp:Button runat="server" ID="Button1" CssClass="btn btn-primary" Text="Search" OnClick="btnPosting_Click"></asp:Button> 
                    </div>
                </div>
            </div>
          </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdAccount" DataKeyNames="namaSiswa" SkinID="GridView" runat="server">
                     <Columns>
                         <asp:TemplateField HeaderText="Nama Siswa" SortExpression="Ket" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Ket" runat="server" Text='<%# Eval("namaSiswa").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Kelas" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("kelas") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                           <asp:TemplateField HeaderText="Jenis Transaksi" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("Jenistransaksi") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>   
                           <asp:TemplateField HeaderText="Nilai Invoice" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="Nilai Bayar" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("nilaibayar") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                           <asp:TemplateField HeaderText="Saldo Piutang" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Grup"  runat="server" Text='<%# Eval("saldo") %>'></asp:Label>
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
