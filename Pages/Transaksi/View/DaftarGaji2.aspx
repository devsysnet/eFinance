<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="DaftarGaji2.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.DaftarGaji2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
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
                         <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Enabled="true" Width="250"></asp:DropDownList>
                        <asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Search" OnClick="btnPosting_Click"></asp:Button>
                        <asp:Button runat="server" ID="btnPrint" CssClass="btn btn-success" Text="Print" OnClick="btnPrint_Click"></asp:Button>
                        <asp:Button runat="server" ID="btnPrint1" CssClass="btn btn-success" Text="Slip Gaji" OnClick="btnPrint1_Click"></asp:Button>
                        <asp:Button runat="server" ID="btnPrint2" CssClass="btn btn-success" Text="Rekening Bank" OnClick="btnPrint1_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdAccount" DataKeyNames="Nama" SkinID="GridView" runat="server">
                        <Columns>
                         <asp:TemplateField HeaderText="Nama" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Nama" runat="server" Text='<%# Eval("Nama").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Sipil" SortExpression="ptd" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Sipil"  runat="server" Text='<%# Eval("Sipil") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField> 
                          <asp:TemplateField HeaderText="Kep." SortExpression="Kep." ItemStyle-Width="5%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Kep"  runat="server" Text='<%# Eval("Kep") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField> 
                          <asp:TemplateField HeaderText="Gol." SortExpression="Gol." ItemStyle-Width="5%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Gol"  runat="server" Text='<%# Eval("Gol") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField> 
                         <asp:TemplateField HeaderText="Gaji Pokok" SortExpression="Gaji Pokok" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                          <ItemTemplate>
                          <asp:Label ID="Gaji_Pokok"  runat="server" Text='<%# Eval("Gaji_Pokok") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>  
                          <asp:TemplateField HeaderText="Tunjanagan Keluarga" SortExpression="Kel" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Kel"  runat="server" Text='<%# Eval("TunjanganKeluarga ") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="Transport" SortExpression="Kedatangan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="JF"  runat="server" Text='<%# Eval("Kedatangan") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="Pangan" SortExpression="Pangan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Pangan"  runat="server" Text='<%# Eval("Pangan") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Subsidi Yayasan" SortExpression="Subsidi Yayasan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="SubsidiYayasan"  runat="server" Text='<%# Eval("SubsidiYayasan") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField> 
                         <asp:TemplateField HeaderText="Rapel" SortExpression="Rapel" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Rapel"  runat="server" Text='<%# Eval("Rapel") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="Lembur" SortExpression="Lembur" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Lembur"  runat="server" Text='<%# Eval("Lembur") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="Bonus" SortExpression="Bonus" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Bonus"  runat="server" Text='<%# Eval("Bonus") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="THR" SortExpression="THR" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="THR"  runat="server" Text='<%# Eval("THR") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                           <asp:TemplateField HeaderText="Total Pendapatan" SortExpression="Total Pendapatan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="totpendapatan"  runat="server" Text='<%# Eval("totpendapatan") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="Iuran Koperasi" SortExpression="Iuran Koperasi" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="IuranKoperasi"  runat="server" Text='<%# Eval("IuranKoperasi") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Dana Sosial" SortExpression="Dana Sosial" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="DanaSosial"  runat="server" Text='<%# Eval("DanaSosial") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Arisan" SortExpression="Arisan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Arisan"  runat="server" Text='<%# Eval("Arisan") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                           <asp:TemplateField HeaderText="Pinjaman" SortExpression="Pinjaman" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="Pinjaman"  runat="server" Text='<%# Eval("Pinjaman") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>  
                          <asp:TemplateField HeaderText="KWI" SortExpression="KWI" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="KWI"  runat="server" Text='<%# Eval("KWI") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="BPJS Kesehatan" SortExpression="BPJS Kesehatan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="bpjskesehatan"  runat="server" Text='<%# Eval("bpjskesehatan") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>  
                           <asp:TemplateField HeaderText="BPJS Ketenagakerjaan" SortExpression="BPJS Ketenagakerjaan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="bpjsketenakerjaan"  runat="server" Text='<%# Eval("bpjsketenakerjaan") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="Total Potongan" SortExpression="Total Potongan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="totpotongan"  runat="server" Text='<%# Eval("totpotongan") %>'></asp:Label>
                          </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="Total Terima" SortExpression="Total Terima" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                          <ItemTemplate>
                          <asp:Label ID="totalterima"  runat="server" Text='<%# Eval("totalterima") %>'></asp:Label>
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
