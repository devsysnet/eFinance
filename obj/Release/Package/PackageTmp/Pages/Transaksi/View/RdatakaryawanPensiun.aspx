<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RdatakaryawanPensiun.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RdatakaryawanPensiun" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <style type="text/css">
        .ChildGrid {
            background: #fff;
            margin-left: 4%;
            width: 96%;
        }

            .ChildGrid td {
                font-size: 11px;
            }

                .ChildGrid td .form-control {
                    padding: 1px 3px;
                    height: 20px;
                    font-size: 11px;
                }

            .ChildGrid th {
                color: #fff;
                font-size: 12px;
                background: #aaaaaa !important;
                border: #aaaaaa;
            }
    </style>
    <script type="text/javascript">
        function expandCollapse(name) {
            var div = document.getElementById(name);
            var img = document.getElementById('img' + name);
            if (div.style.display == 'block') {
                div.style.display = 'none';
                img.src = '<%=Func.BaseUrl%>assets/images/plus.png';
            } else {
                div.style.display = 'block';
                img.src = '<%=Func.BaseUrl%>assets/images/minus.png';
            }
        }
    </script>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                                 <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Cabang : </label>
                                            <div class="col-sm-7">
                                                <div class="input-group">
                                                    <div class="form-inline">
                                                        <div class="input-group-btn">
                                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Width="200"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                           </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdviewAbsen" SkinID="GridView" runat="server" DataKeyNames="tglpensiun" >
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                   <div class="text-center">
                                                         <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                              </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:BoundField DataField="nama" HeaderText="Nama Karyawan" HeaderStyle-CssClass="text-Left" />
                                              <asp:BoundField DataField="tgllahir" HeaderText="Tanggal Lahir" HeaderStyle-CssClass="text-left" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}"/>
                                              <asp:BoundField DataField="tglpensiun" HeaderText="Tanggal Pensiun" HeaderStyle-CssClass="text-left" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}"/>
                                              <asp:BoundField DataField="tglalert" HeaderText="Tanggal Alert" HeaderStyle-CssClass="text-left" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}"/>
                                              <asp:BoundField DataField="unit" HeaderText="Unit" HeaderStyle-CssClass="text-left" ItemStyle-HorizontalAlign="Center"/>
                                              </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

