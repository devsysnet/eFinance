<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransUpdateNilaiSoalKPI.aspx.cs" Inherits="eFinance.Pages.Transaksi.Update.TransUpdateNilaiSoalKPI" %>
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
                                <asp:GridView ID="grdnilaikpi" DataKeyNames="noujian" SkinID="GridView" runat="server" AllowPaging="false" PageSize="10" OnSelectedIndexChanged="grdnilaikpi_SelectedIndexChanged"  OnPageIndexChanging="grdnilaikpi_PageIndexChanging" OnRowCommand="grdnilaikpi_RowCommand">
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
                                                        <asp:Button ID="btnDetail" runat="server"  class="btn btn-xs btn-labeled btn-warning"  Text="Edit" CommandName="Select"  />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectDel" runat="server" class="btn btn-xs btn-labeled btn-danger" Text="Hapus" CommandName="delete" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
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
                            <div class="col-sm-12">
                                <div class="form-horizontal">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">Tanggal </label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="tgl" runat="server" CssClass="form-control date" ></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">Unit <span class="mandatory">*</span></label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="unit" Enabled="false" OnSelectedIndexChanged="cboUnit_SelectedIndexChanged">
                                                    
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                       
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">Karyawan <span class="mandatory">*</span></label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList runat="server" CssClass="form-control" Enabled="false" ID="karyawan" >
                                                  
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">Tipe Soal <span class="mandatory">*</span></label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList runat="server" CssClass="form-control" Enabled="false" ID="tipesoal" >
                                                   
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                          <div class="form-group">
                                            <label class="col-sm-4 control-label">Catatan </label>
                                            <div class="col-sm-5">
                                              <asp:TextBox ID="keterangan" TextMode="MultiLine" runat="server" CssClass="form-control  " value="0" Width="180" ></asp:TextBox>
                                            </div>
                                        </div>
                                </div>
                            </div>
                        </div>
              </div>
         <div class="row">
              <div class="tab-panel" id="tab-piutang">
                                                <div class="panel">
                                                    <div class="panel-body">
                                                        <div class="table-responsive" style="width=200px">
                                                        <asp:GridView ID="grddetailnilaikpi" Width="900" SkinID="GridView" runat="server">
                                                            <Columns>

                                                                   <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Uraian" ItemStyle-Width="1%">
                                                                    <ItemTemplate>
                                                                       <asp:Label runat="server" ID="soalkpi"  ></asp:Label>
                                                                          <asp:HiddenField ID="hdnnoujiand" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nilai" ItemStyle-Width="1%">
                                                                    <ItemTemplate>
                                                                       <asp:TextBox runat="server" ID="nilaikpi" CssClass="form-control"  ></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
 
                           
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                    </div>
                                            </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                   <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click"></asp:Button>
                                                <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Back" OnClick="btnCancel_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>  
         </div>
      </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
