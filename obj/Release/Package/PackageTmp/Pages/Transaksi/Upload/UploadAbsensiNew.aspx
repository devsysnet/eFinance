<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="UploadAbsensiNew.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.UploadAbsensiNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="form-inline">
                                    <label class="col-sm-3 control-label">Upload File Extention .csv / .txt <span class="mandatory">*</span></label>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ID="cboflUpload" runat="server" CssClass="form-control" Width="120">
                                            <asp:ListItem Value="csv/txt">CSV / TXT</asp:ListItem>
                                        </asp:DropDownList>    
                                        <asp:FileUpload ID="flUpload" Class="fileupload" runat="server" />
                                        <asp:Button runat="server" ID="btnUpload" CssClass="btn btn-primary" Text="Upload" OnClick="btnUpload_Click"></asp:Button>
                                        <span>Download template absensi</span>
                                        <asp:Button runat="server" ID="btnDownload" CssClass="btn btn-success" Text="Download" OnClick="btnDownload_Click"></asp:Button>      
                                        <br /><span class="mandatory">* Sebelum upload harap hapus kolom nama karyawan</span>                                  
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row" runat="server" id="gridUpload">
                        <div class="col-sm-12 overflow-x-table">
                            <asp:GridView ID="grdUploadAbs" SkinID="GridView" runat="server" AllowPaging="true" PageSize="30" OnPageIndexChanging="grdUploadAbs_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <%# Container.DataItemIndex + 1 %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="tgl" HeaderText="Tanggal" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" />
                                    <asp:BoundField DataField="nik" HeaderText="NIK" HeaderStyle-CssClass="text-center" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="nama" HeaderText="Nama Karyawan" HeaderStyle-CssClass="text-center" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="jamMasuk" HeaderText="Jam Masuk" HeaderStyle-CssClass="text-center" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="jamKeluar" HeaderText="Jam Keluar" HeaderStyle-CssClass="text-center" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
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
        <asp:PostBackTrigger ControlID="btnUpload" />
        <asp:PostBackTrigger ControlID="btnDownload" />
    </Triggers>
</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
