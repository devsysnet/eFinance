<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Mstparametercabangupdate.aspx.cs" Inherits="eFinance.Pages.Master.Update.Mstparametercabangupdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function DeleteAll() {
            bootbox.confirm({
                message: "Are you sure to delete data?",
                callback: function (result) {
                    if (result == true) {
                        document.getElementById('<%=hdnMode.ClientID%>').value = "deleteall";
                        eval("<%=execBind%>");
                    }
                },
                className: "bootbox-sm"
            });
            return false;
        }
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdCabang.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <asp:Button ID="cmdMode" runat="server" OnClick="cmdMode_Click" Style="display: none;" Text="Mode" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <asp:HiddenField runat="server" ID="hdnID" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-8">
                                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" Text="Delete / Delete all"  OnClientClick="return DeleteAll()" CausesValidation="false" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdCabang" DataKeyNames="noparametercb" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdCabang_PageIndexChanging"
                                        OnSelectedIndexChanged="grdCabang_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <HeaderTemplate>
                                                    <div class="text-center">
                                                        <asp:CheckBox ID="chkCheckAll" runat="server" CssClass="px" onclick="return CheckAllData(this);" />
                                                    </div>
                                                </HeaderTemplate>
                                                 <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:CheckBox ID="chkCheck" runat="server" CssClass="px chkCheck" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="tahunAjaran" SortExpression="Tahun Ajaran" HeaderText="Tahun Ajaran" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="mulaithnajaran" SortExpression="Mulai Tahun Ajaran" HeaderText="Mulai Tahun Ajaran" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="akhirthnajaran" SortExpression="Akhir Tahun Ajaran" HeaderText="Akhir Tahun Ajaran" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="absengaji" SortExpression="Absensi Include Gaji" HeaderText="Absensi Include Gaji" ItemStyle-Width="8%" />
                                            <asp:BoundField DataField="absempotonggaji" SortExpression="Terlambat Potong Gaji" HeaderText="Terlambat Potong Gaji" ItemStyle-Width="8%" />
                                            <asp:BoundField DataField="upahminimum" SortExpression="Upah Minimum" HeaderText="Upah Minimum" ItemStyle-Width="10%" />
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Detail" CommandName="Select" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row">
                            <div class="form-horizontal">
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tahun Ajaran Baru</label>
                                <div class="col-sm-5">
                                    <asp:DropDownList ID="cboTA" runat="server" CssClass="form-control" Width="150"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal Awal Tahun Ajaran Baru</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" CssClass="form-control date" ID="dtKas"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal Akhir Tahun Ajaran Baru</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" CssClass="form-control date" ID="dtKas1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Absensi Include Ke Gaji</label>
                                <div class="col-sm-5">
                                     <asp:DropDownList ID="cboitunggaji" runat="server" Width="100">
                                            <asp:ListItem Value="1">Ya</asp:ListItem>
                                            <asp:ListItem Value="2">Tidak</asp:ListItem>
                                      </asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal Perhitungan Gaji</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" CssClass="form-control date" ID="dtdari"></asp:TextBox>  sd  <asp:TextBox runat="server" CssClass="form-control date" ID="dtsampai"></asp:TextBox>  
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Keterlambatan Absen Potong Gaji</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="cboPotonggaji" runat="server" Width="100">
                                            <asp:ListItem Value="1">Ya</asp:ListItem>
                                            <asp:ListItem Value="2">Tidak</asp:ListItem>
                                      </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                               <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jam Masuk</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" CssClass="form-control time"  ID="jammasuk" Width="80"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                               <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jam Keluar</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" CssClass="form-control time" ID="jamkeluar" Width="80"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">UMR</label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" CssClass="form-control money" ID="txtNilai" Width="200" Text="0.00"></asp:TextBox>
                                </div>
                            </div>
                           
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Pembayaran Gaji</label>
                                <div class="col-sm-6">
                                     <asp:DropDownList ID="cbogaji" runat="server" Width="100">
                                        <asp:ListItem Value="2">Unit</asp:ListItem>
                                        <asp:ListItem Value="3">Perwakilan</asp:ListItem>
                                        <asp:ListItem Value="4">Pusat</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                              <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Pembayaran Tagihan</label>
                                <div class="col-sm-6">
                                     <asp:DropDownList ID="cbojnsbyr" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="cboLokasi_SelectedIndexChanged">
                                        <asp:ListItem Value="Non_PaymentGateWay">Non_PaymentGateWay</asp:ListItem>
                                        <asp:ListItem Value="PaymentGateWay">PaymentGateWay</asp:ListItem>
                                        <asp:ListItem Value="API">API/Virtual Account</asp:ListItem>
                                     </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label"></label>
                                    <div class="col-sm-6"  id="showhidekode" runat="server">
                                      <asp:DropDownList ID="cbobank" runat="server" Width="300">
                                        <asp:ListItem Value="">---Pilih Bank---</asp:ListItem>
                                   </asp:DropDownList>
                                    </div>
                            </div> 
                                          <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kode Bank</label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="kdbank" Width="200"  ></asp:TextBox>
                                </div>
                            </div>
                                 <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Biaya Admin Bank</label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" CssClass="form-control money" ID="biayaadmbank" Width="200" Text="0.00"></asp:TextBox>
                                </div>
                            </div>
                                 <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">IP Cabang</label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="ipcabang" Width="200" Text="0.00"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kepala Sekolah</label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="kepalasekolah" Width="200" Text="0.00"></asp:TextBox>
                                </div>
                            </div>
                                 <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Admin</label>
                                    <div class="col-sm-6"   runat="server">
                                          <asp:DropDownList ID="cbojnsbank" runat="server" Width="300">
                                            <asp:ListItem Value="">---Pilih Jenis Admin---</asp:ListItem>
                                            <asp:ListItem Value="ya">Ya</asp:ListItem>
                                            <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                       </asp:DropDownList>
                                    </div>
                            </div>
                               <div class="form-group">
                                    <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Strategi Cuti</label>
                                    <div class="col-sm-6"   runat="server">
                                          <asp:DropDownList ID="cboscuti" runat="server" Width="300">
                                            <asp:ListItem Value="">---Pilih Strategi Cuti---</asp:ListItem>
                                            <asp:ListItem Value="hangus">Hangus</asp:ListItem>
                                            <asp:ListItem Value="tidakhangus">Tidak Hangus</asp:ListItem>
                                       </asp:DropDownList>
                                    </div>
                            </div>  
                           </div>
                            </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="text-center">
                                            <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click" />
                                            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </asp:Content>
