<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RNilaiKPI.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RNilaiKPI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <script type="text/javascript">
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdnilaikpi.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[5].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
        <asp:HiddenField ID="hdnnoYysn" runat="server" />
        <asp:HiddenField ID="hdnId" runat="server" />
        <div id="tabGrid" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                             <div class="form-group">
                                <div class="form-inline">
                                        <div class="col-sm-12">
                        <label>Periode : </label>
                                            <asp:TextBox ID="dtMulai" runat="server" CssClass="form-control date"></asp:TextBox>&nbsp; 
                                                    <asp:TextBox ID="dtSampai" runat="server" CssClass="form-control date"></asp:TextBox>&nbsp;
                        <label>Perwakilan : </label>

                                            <asp:DropDownList ID="cboPerwakilan" runat="server" Width="250" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboPerwakilan_SelectedIndexChanged"></asp:DropDownList>

                        <asp:DropDownList ID="cboCabang" Width="200" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <asp:GridView ID="grdnilaikpi" DataKeyNames="noujian" SkinID="GridView" runat="server" AllowPaging="false" PageSize="10"  OnSelectedIndexChanged="grdSiswa_SelectedIndexChanged" OnPageIndexChanging="grdnilaikpi_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="kodujian" SortExpression="kdrek" HeaderText="Kode Ujian" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="nama" SortExpression="ket" HeaderText="Nama Karyawan" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="tgl" SortExpression="tgl" HeaderText="Tanggal" ItemStyle-Width="10%" DataFormatString="{0:dd MMM yyyy}" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="uraian" SortExpression="ket" HeaderText="Uraian" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="jmlsoal" SortExpression="ket" HeaderText="Jumlah Soal" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="deskripsi" SortExpression="ket" HeaderText="Deskripsi" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                  <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-xs" Text="Detail" CommandName="Select" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                         
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <%--<asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Posting" OnClick="btnPosting_Click"></asp:Button>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
            </div>
     <div id="tabForm" runat="server" visible="false">
         <div class="row">
              <div class="tab-panel" id="tab-piutang">
                                                <div class="panel">
                                                    <div class="panel-body">
                                                        <div class="table-responsive">
                                                        <asp:GridView ID="grddetailnilaikpi" SkinID="GridView" runat="server">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                                    <ItemTemplate>
                                                                        <div class="text-center">
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
 
                                                                <asp:BoundField DataField="nilai" HeaderText="Nilai" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="center"  />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                    </div>
                                            </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Back" OnClick="btnCancel_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>  
         </div>
      </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
