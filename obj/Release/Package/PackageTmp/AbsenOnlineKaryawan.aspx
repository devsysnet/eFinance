<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="AbsenOnlineKaryawan.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.AbsenOnlineKaryawan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <style>
        #BodyContent_imgkaryawan{
            width:70px;
             height:70px;
             border-radius:50%;
        }
        #BodyContent_btnMasuk{
            width:100px;
             height:50px;
             border-radius:10px;
        }
        #BodyContent_btnKeluar{
            width:100px;
             height:50px;
             border-radius:10px;
             color:#3652AD;
             background:white;
             border-color:#3652AD;
        }
            #BodyContent_btnKeluar:hover {
                color:white;
            }
    </style>


    <div class="row" >
        <div class="col-sm-12">
            <div class="panel" style="background:#F4F4F4;border:0px solid #F4F4F4">
                <div class="panel-body" style="background:#F4F4F4;border:0px solid #F4F4F4">
                    <div class="row">
                        <div class="form-horizontal">
                           <div class="col-md-4"></div>
                           <div class="col-md-4">
                              <table>
                                  <tr>
                                      <td>
                                           <asp:Image ID="imgkaryawan" runat="server" />
                                         
                                      </td>
                                       <td>
                                                <span style="font-size:20px;font-weight:bold;margin-left:20px;color:black">
                                                    <asp:Label ID="lblNameUser" runat="server"></asp:Label>
                                                    
                                                </span>
                                           <br />
                                             <span style="font-size:15px;margin-left:20px;color:black">
                                                <asp:Label ID="lblCabang" runat="server" Text="perwakilan"></asp:Label>
                                            </span>
                                      </td>
                                      <td>
                                          <div>
                                             
                                          </div>
                                      </td>
                                  </tr>
                              </table>
                                   
                               
                  
                                   
                           </div>
                            <div class="col-md-4"></div>
                        </div>
                    </div>
                    <div class="row">
                       <div class="form-horizontal">
                            <div class="col-md-3">  </div>
                           <div class="col-md-4"  style="width:35%;height:auto;padding-bottom:30px;border-radius:20px;box-shadow:0 .5rem 1rem rgba(0,0,0,.15)!important;background-color:white;margin-top:20px">
                               <table style="width:100%;margin-top:30px">
                                   <tr>
                                       <td align="center">
                                           <span style="font-size:20px;color:black; font-weight:bold">
                                               Live Attendance
                                           </span>
                                           
                                       </td>
                                   </tr>
                                   <tr>
                                       <td align="center">
                    
                   <span id="jam1" style="font-size:50px;color:#3652AD;font-weight:bold">             
                                           </span>
                        <script language="javascript">
                            function jam1() {
                                var waktu = new Date();
                                var jam = waktu.getHours();
                                var menit = waktu.getMinutes();
                                var detik = waktu.getSeconds();

                                if (jam < 10) {
                                    jam = "0" + jam;
                                }
                                if (menit < 10) {
                                    menit = "0" + menit;
                                }
                                if (detik < 10) {
                                    detik = "0" + detik;
                                }
                                var jam_div = document.getElementById('jam1');
                                jam_div.innerHTML = jam + ":" + menit + ":" + detik;
                                setTimeout("jam1()", 1000);
                            }
                            jam1();
						</script>
                                       </td>
                                   </tr>
                                    <tr>
                                       <td align="center">
                                           <span style="font-size:20px;color:black;">
                                                                    
                                            <script language="javascript">
                        var NamaHari = new Array("Minggu", "Senin", "Selasa", "Rabu", "Kamis", "Jumat",
						"Sabtu");
                        var NamaBulan = new Array("Januari", "Februari", "Maret", "April", "Mei",
						"Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember");
                        var sekarang = new Date();
                        var HariIni = NamaHari[sekarang.getDay()];
                        var BulanIni = NamaBulan[sekarang.getMonth()];
                        var tglSekarang = sekarang.getDate();
                        var TahunIni = sekarang.getFullYear();
                        document.write(HariIni + ", " + tglSekarang + " " + BulanIni + " " + TahunIni);
					</script>
                    
                      </span>
                        </td>
                                   </tr>
                                   <tr>
                                       <td align="center" style="height:100px">
                                           <div id="formalertAbsenM" runat="server" visible="false">
                                            <span id="alertAbsenM" runat="server" style="background-color:#4CCD99; font-size:14pt; font-weight:bold; padding:20px; border-radius:10px;color:white;" />  

                                           </div>
                                            <div id="formalertAbsenK"  visible="false" runat="server">
                                              <span id="alertAbsenK"  runat="server"   style="background-color:#FA7070; font-size:14pt; font-weight:bold; padding:20px; border-radius:10px;color:white;" />          

                                           </div>
                                            <div id="formalertAbsenS"  visible="false"  runat="server">
                                              <span id="alertAbsenS"  runat="server"   style="background-color:#387ADF; font-size:14pt; font-weight:bold; padding:20px; border-radius:10px;color:white;" /> 

                                           </div>
                                       </td>
                                   </tr>
                                       <tr>
                                       <td align="center">
                                           <span style="font-size:20px;color:black;">
                                               Office Hours
                                           </span>
                                           
                                       </td>
                                   </tr>
                                    <tr>
                                       <td align="center">
                                           <span id="jammasuk" runat="server" style="font-size:25px;color:black; font-weight:bold">
                                              
                                           </span>
                                           -
                                           <span id="jamkeluar" runat="server" style="font-size:25px;color:black; font-weight:bold">
                                              
                                           </span>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="height:50px">

                                       </td>
                                   </tr>
                                   <tr>
                                       <td align="center">
                                                  <asp:Button ID="btnMasuk" runat="server" OnClick="btnSimpan_Click" CssClass="btn btn-primary" Text="Masuk" CausesValidation="false" />
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                  <asp:Button ID="btnKeluar" runat="server" OnClick="btnKeluar_Click" CssClass="btn btn-primary" Text="Keluar" CausesValidation="false" />
                                       </td>
                                   </tr>
                               </table>
                               
                                    
                           </div>
                            <div class="col-md-4">
                                 <div style="margin-top:-30px;padding-bottom:20px;margin-left:180px">
                                     <table>
                                         <tr>
                                             <td>
                                                 <svg xmlns="http://www.w3.org/2000/svg" width="26" height="26" fill="#3652AD" class="bi bi-bell-fill" viewBox="0 0 16 16">
                                                      <path d="M8 16a2 2 0 0 0 2-2H6a2 2 0 0 0 2 2m.995-14.901a1 1 0 1 0-1.99 0A5 5 0 0 0 3 6c0 1.098-.5 6-2 7h14c-1.5-1-2-5.902-2-7 0-2.42-1.72-4.44-4.005-4.901"/>
                                                  </svg>  
                                             </td>
                                             <td>
                                                <span style="font-weight:bold;font-size:20px;margin-left:10px">
                                                     Pengumuman
                                         
                                                 </span>
                                             </td>
                                         </tr>
                                     </table>
                                              
                                     
                                            </div>
                                 <div class="row">

                                    <div class="card">
                                        <div class="card-title" ">
                                           
                                        </div>
                                        <div class="card-body">
                                            <div  style="width:70%;height:auto;padding-bottom:30px;border-radius:20px;box-shadow:0 .5rem 1rem rgba(0,0,0,.15)!important;background-color:white;margin-left:160px;padding:30px;height:300px">

                                             <asp:Label ID="lblIPAddress" runat="server"></asp:Label>
                                                </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                       </div>
                    </div>
                   
                </div>

            </div>
            
        </div>
    </div>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
        <asp:PostBackTrigger ControlID="btnmasuk" />
        <asp:PostBackTrigger ControlID="btnkeluar" />
    </Triggers>
    </asp:UpdatePanel>

</asp:Content>