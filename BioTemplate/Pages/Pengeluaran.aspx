﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pengeluaran.aspx.cs" Inherits="BioTemplate.Pages.Pengeluaran" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../Fonts/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="../CSS/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="../CSS/plugins/toastr/toastr.min.css" rel="stylesheet" />
    <link href="../CSS/animate.css" rel="stylesheet" />
    <link href="../CSS/style2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link href="../CSS/plugins/datapicker/datepicker3.css" rel="stylesheet" />
    <link href="../CSS/plugins/dataTables/datatables.min.css" rel="stylesheet" />
    <link href="../CSS/plugins/clockpicker/clockpicker.css" rel="stylesheet" />
    <link href="../CSS/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="../CSS/plugins/faAnimation/faAnimation.css" rel="stylesheet" />
    <link href="../CSS/plugins/sweetalert/sweetalert.css" rel="stylesheet" />
    <link href="../CSS/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="../CSS/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css" rel="stylesheet" />
</head>
<%--<body class="fixed-sidebar fixed-nav pace-done mdskin2">--%>
<body class="fixed-sidebar fixed-nav pace-done mdskin2">
    <form id="form1" runat="server">
        <div id="wrapper">
            <nav class="navbar-default navbar-static-side " role="navigation">
                <div class="sidebar-collapse" id="sideMenu" runat="server">
                    <ul class="nav metismenu sidebar-nav" runat="server">
                        <li class="sidebar-brand">
                            <a href="Default.aspx" >BioFarma</a>
                        </li>    
                        <li>
                            <a href="Masterkas.aspx">Master Kas</a>
                        </li>
                        <li>
                            <a href="Pemasukkan.aspx">Pemasukkan</a>
                        </li>
                        <li class="dropdown">
                              <a href="#" class="dropdown-toggle" data-toggle="dropdown">Pengeluaran <span class="caret"></span></a>
                              <ul class="dropdown-menu" role="menu">
                                  <li style="color:whitesmoke;">===============================</li>
                                  <li style="color:whitesmoke;"><a href="Pengeluaran.aspx">Semua</a></li>
                                <li style="color:whitesmoke;"><a href="Jasa.aspx">Jasa</a></li>
                              </ul>
                            </li>
                        <li>
                            <a href="Laporan.aspx">Laporan</a>
                        </li>
                    </ul>
                </div>
            </nav>

            <div id="page-wrapper" class="gray-bg">
                <div class="row border-bottom">
                    <nav class="navbar navbar-fixed-top white-bg" role="navigation" style="margin-bottom: 0">
                        <div class="navbar-header">
                            <a class="navbar-minimalize minimalize-styl-2 btn " href="#">
                                <i class="fa fa-bars"></i>
                            </a>
                            <div class="navbar-form-custom">
                                <div class="form-group">
                                    <input type="text" placeholder="Cari Sesuatu ..." class="form-control search" name="top-search" style="color:white" id="top-search" />
                                </div>
                            </div>
                        </div>
                        <ul class="nav navbar-top-links navbar-right">
                            <li>
                                <span class="m-r-sm text-muted welcome-message">Welcome to BIO FARMA Template</span>
                            </li>
                            <li>
                                <li class="dropdown">
                                    <a class="dropdown-toggle count-info faa-parent animated-hover" data-toggle="dropdown" href="#">
                                        <i class="fa fa-exclamation-triangle faa-tada"></i>&nbsp&nbsp<span class="label label-danger" id="spnCitoNotification" runat="server">0</span>
                                    </a>
                                    <ul class="dropdown-menu dropdown-messages">
                                        <asp:Repeater ID="rptNotificationCito" runat="server">
                                            <HeaderTemplate>
                                                <li>
                                                    <p id="pHeaderCITO" runat="server"></p>
                                                </li>
                                                <li class="divider"></li>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <li>
                                                    <div class="dropdown-messages-box" style="color:red">
                                                        <a href='<%: ResolveClientUrl("~/Pages/Approval/ListPendingApproval.aspx") %>' class="pull-left">
                                                            <img alt="image" class="img-circle" src='<%# Eval("ICNLG")%>' />
                                                       
                                                             </a>
                                                        <div class="media-body">

                                                            <strong style="color:red"><%# Eval("DOCTY")%></strong> (<%# Eval("DOCCO")%>)
                                                <br>
                                                            <small class="text-muted" >You Have <%# Eval("DOCCO")%> CITO Documents Pending </small>
                                                        </div>
                                                    </div>
                                                </li>
                                                <li class="divider"></li>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div class="text-center link-block" style="color:red">
                                                    <a href='<%: ResolveClientUrl("~/Pages/Approval/ListPendingApproval.aspx") %>'></i><strong>Read All Messages</strong>
                                                    </a>
                                                </div>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </li>
                            </li>
                            <li class="dropdown">
                                <a class="dropdown-toggle count-info faa-parent animated-hover" data-toggle="dropdown" href="#">
                                    <i class="fa fa-bell faa-tada"></i>&nbsp&nbsp<span class="label label-success" id="spnNotification" runat="server">0</span>
                                </a>
                                <ul class="dropdown-menu dropdown-messages">
                                    <asp:Repeater ID="rptApproval" runat="server">
                                        <HeaderTemplate>
                                            <li>
                                                <p id="pHeader" runat="server"></p>
                                            </li>
                                            <li class="divider"></li>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <li>
                                                <div class="dropdown-messages-box">
                                                    <a href='<%: ResolveClientUrl("#") %>' class="pull-left">
                                                        <img alt="image" class="img-circle" src='<%# Eval("ICNLG")%>' />
                                                    
                                                        </a>
                                                    <div class="media-body">

                                                        <strong><%# Eval("DOCTY")%></strong> (<%# Eval("DOCCO")%>)
                                                <br>
                                                        <small class="text-muted">You Have <%# Eval("DOCCO")%> Documents Pending </small>
                                                    </div>
                                                </div>
                                            </li>
                                            <li class="divider"></li>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div class="text-center link-block">
                                                <a href='<%: ResolveClientUrl("#") %>'></i><strong>Read All Messages</strong>
                                                </a>
                                            </div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </ul>
                            </li>
                            <li>
                                <a href='<%: ResolveClientUrl("~/Login.aspx") %>'>
                                    <i class="fa fa-sign-out" title="Log Out"></i>Log out
                                </a>
                            </li>
                        </ul>

                    </nav>
                </div>
                <div class="wrapper wrapper-content animated fadeInRight">
                    <h1>Pengeluaran kas</h1>
                    <br />
                    <div class="modal fade" id="confirmmasuk" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                      <div class="modal-dialog" role="document">
                        <div class="modal-content">
                          <div class="modal-body">
                              <asp:Label ID="warning1" runat="server" CssClass="h4"></asp:Label>
                              <br />
                              <br />
                              <asp:Label ID="warning2" runat="server" CssClass="h4"></asp:Label>
                          </div>
                          <div class="modal-footer">
                              <button type="button" class="btn btn-secondary navbar-left" data-dismiss="modal">Batal</button>
                                  <asp:LinkButton ID="confirmmin" runat="server" OnClientClick="return dataValid();" OnClick="confirmmin_Click" CssClass="btn btn-primary"><i class="fa fa-arrow-circle-o-right"></i> Lanjut</asp:LinkButton>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="modal fade" id="confirmperiode" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                      <div class="modal-dialog" role="document">
                        <div class="modal-content">
                          <div class="modal-body">
                              <asp:Label ID="Label1" runat="server" CssClass="h4"></asp:Label>
                              <br />
                              <br />
                              <asp:Label ID="Label2" runat="server" CssClass="h4"></asp:Label>
                          </div>
                          <div class="modal-footer">
                                  <asp:LinkButton ID="btnhutang" runat="server" OnClick="confirmmin_Click" CssClass="btn btn-success"><i class="fa fa-arrow-circle-o-right"></i> Menjadi kredit</asp:LinkButton>
                                  <asp:LinkButton ID="btnperiode" runat="server" OnClick="btnperiode_Click" CssClass="btn btn-primary"><i class="fa fa-arrow-circle-o-right"></i> Lanjut</asp:LinkButton>
                          </div>
                        </div>
                      </div>
                    </div>

                    <div class="modal fade" id="updatemin" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                      <div class="modal-dialog" role="document">
                        <div class="modal-content">
                          <div class="modal-body">
                              <asp:Label ID="updatelabel1" runat="server" CssClass="h4"></asp:Label>
                              <br />
                              <br />
                              <asp:Label ID="updatelabel2" runat="server" CssClass="h4"></asp:Label>
                          </div>
                          <div class="modal-footer">
                                  <button type="button" class="btn btn-secondary navbar-left" data-dismiss="modal">Batal</button>
                                  <asp:LinkButton ID="updateminus" OnClick="updateminus_Click" runat="server" CssClass="btn btn-primary"><i class="fa fa-arrow-circle-o-right"></i> Lanjut</asp:LinkButton>
                          </div>
                        </div>
                      </div>
                    </div>

                    <div class="modal fade" id="updateperiode" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                      <div class="modal-dialog" role="document">
                        <div class="modal-content">
                          <div class="modal-body">
                              <asp:Label ID="updatelabel3" runat="server" CssClass="h4"></asp:Label>
                              <br />
                              <br />
                              <asp:Label ID="updatelabel4" runat="server" CssClass="h4"></asp:Label>
                          </div>
                          <div class="modal-footer">
                                  <button type="button" class="btn btn-secondary navbar-left" data-dismiss="modal">Batal</button>
                                  <asp:LinkButton ID="updateperiode" OnClick="updateperiode_Click" runat="server" CssClass="btn btn-primary"><i class="fa fa-arrow-circle-o-right"></i> Lanjut</asp:LinkButton>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="container-fluid">
                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#forminput"><i class="fa fa-plus"></i> Tambah Pengeluaran</button>
                    <button type="button" class="btn btn-primary navbar-right" data-toggle="modal" data-target="#formsaldo"><i class="fa fa-list-alt"></i> Lihat Saldo</button>
                    </div>
                    <asp:TextBox ID="jmlhsaldo" runat="server" Visible="false" placeholder="jmlhsaldo"/>
                    <asp:TextBox ID="jsaldo" runat="server" Visible="false" placeholder="jsaldo"/>
                    <asp:TextBox ID="kasdbtext" runat="server" Visible="false"/>
                    <asp:TextBox ID="saldodel" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="kasdel" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="periodedel" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="unitdel" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="hargadel" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="bagiandel" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="vendordel" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="satuandel" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="keterangandel" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="tgldel" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="totdelsaldo" runat="server" Visible="false" placeholder="totdelsaldo"></asp:TextBox>
                    <%-- FORM INPUT MODAL --%>
                        <div class="modal fade" id="forminput" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                          <div class="modal-dialog modal-dialog-centered" role="document">
                            <div class="modal-content">
                              <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                  <span aria-hidden="true">&times;</span>
                                </button>
                                <h2 class="modal-title" id="inputpengeluaran">Tambah Data Pengeluaran</h2>
                              </div>
                              <div class="modal-body">
                                  
                                  <label><h3>Plih Kas</h3></label>
                                  <asp:DropDownList ID="kasDl" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="kasDl_SelectedIndexChanged"/>
                                   
                                  <br />
                                  <label><h3>Satuan</h3></label>
                                  <asp:TextBox ID="satuan" runat="server" CssClass="form-control"></asp:TextBox>
                                  
                                  <br />
                                  <label><h3>Harga / pcs</h3></label>
                                  <asp:TextBox ID="harga" runat="server" CssClass="form-control" placeholder="Rp" onblur= "multiply()" onkeydown = " return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106 || (event.keyCode >= 48 && event.keyCode <= 57 && isNaN(event.key))) && event.keyCode!=32);"/>

                                  <br />
                                  <label><h3>Quantity</h3></label>
                                  <asp:TextBox ID="quantity" runat="server" CssClass="form-control" onblur= "multiply()" onkeydown = " return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106 || (event.keyCode >= 48 && event.keyCode <= 57 && isNaN(event.key))) && event.keyCode!=32);"></asp:TextBox>

                                  <br />
                                  <label><h3>Total harga</h3></label>
                                  <asp:TextBox ID="jmlhkeluar" runat="server" CssClass="form-control" onkeydown="return false;"></asp:TextBox>

                                  <br />
                                  <label><h3>Keperluan</h3></label>
                                  <textarea ID="keperluan" runat="server" class="form-control" rows="5"></textarea>

                                  <br />
                                  <label><h3>Tanggal Pengeluaran</h3></label>
                                  <div class="input-group date" id="datepicker" data-provide="datepicker">
                                      <asp:TextBox ID="tgl_keluar" runat="server" CssClass="form-control" Placeholder="dd/mm/yyyy" onkeydown="return false;"></asp:TextBox>
                                      <div class="input-group-addon">
                                           <span class="glyphicon glyphicon-th"></span>
                                      </div>
                                  </div>
                                  <br />
                                  <label><h3>Plih periode</h3></label>
                                  <asp:DropDownList ID="periodeDl" runat="server" CssClass="form-control"/>

                                  <br />
                                  <label><h3>Plih bagian</h3></label>
                                  <asp:DropDownList ID="bagianDl" runat="server" CssClass="form-control"/>

                                  <br />
                                  <label><h3>Vendor</h3></label>
                                  <asp:TextBox ID="vendor" runat="server" CssClass="form-control"></asp:TextBox>

                                  <br />
                                  <label for="radioya"><h3>Jasa : </h3></label>
                                  <asp:RadioButton ID="radioya" runat="server" CssClass="radio radio-inline" Text="Ya" GroupName="jasa"/>
                                  <asp:RadioButton ID="radiotidak" runat="server" CssClass="radio radio-inline" Text="Tidak" GroupName="jasa"/>
                              </div>
                              <div class="modal-footer">
                                <button type="button" class="btn btn-secondary navbar-left" data-dismiss="modal">Batal</button>
                                  <asp:LinkButton ID="Confirm" runat="server" OnClientClick="return isDataValid();" OnClick="Confirm_Click" CssClass="btn btn-primary"><i class="fa fa-save"></i> Simpan</asp:LinkButton>
                              </div>
                            </div>
                          </div>
                        </div>
                    <%-- AKHIR FORM INPUT MODAL --%>
                    <%-- FORM EDIT MODAL --%>
                    <div class="modal fade" id="formedit" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                          <div class="modal-dialog modal-dialog-centered" role="document">
                            <div class="modal-content">
                              <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                  <span aria-hidden="true">&times;</span>
                                </button>
                                <h2 class="modal-title" id="exampleModalCenterTitle">Edit Pengeluaran</h2>
                              </div>
                              <div class="modal-body">
                                  <asp:TextBox ID ="id" runat="server" Visible="false"></asp:TextBox>
                                  
                                  <label><h3>Kas</h3></label>
                                  <asp:DropDownList ID="kasDledit" runat="server" CssClass="form-control" Enabled="false"/>

                                  <br />
                                  <label><h3>Periode</h3></label>
                                  <asp:TextBox ID="periodeDledit" runat="server" CssClass="form-control" onkeydown="return false;" ></asp:TextBox>
                                  <hr class="styled" />

                                  <br />
                                  <label><h3>Satuan</h3></label>
                                  <asp:TextBox ID="satuanedit" runat="server" CssClass="form-control"></asp:TextBox>

                                  <br />
                                  <label><h3>Harga / pcs</h3></label>
                                  <asp:TextBox ID="hargaedit"  runat="server" CssClass="form-control" placeholder="Rp" onblur= "multiplyEdit()" onkeydown = " return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106 || (event.keyCode >= 48 && event.keyCode <= 57 && isNaN(event.key))) && event.keyCode!=32);"/>

                                  <br />
                                  <label><h3>Quantity</h3></label>
                                  <asp:TextBox ID="quantityedit" runat="server" CssClass="form-control" onblur= "multiplyEdit()" onkeydown = " return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106 || (event.keyCode >= 48 && event.keyCode <= 57 && isNaN(event.key))) && event.keyCode!=32);"></asp:TextBox>

                                  <br />
                                  <label><h3>Total harga</h3></label>
                                  <asp:TextBox ID="jmlhkeluaredit" runat="server" CssClass="form-control" onkeydown="return false;"></asp:TextBox>

                                  <br />
                                  <label><h3>Keperluan</h3></label>
                                  <textarea ID="keteranganedit" runat="server" class="form-control" rows="5"></textarea>

                                  <br />
                                  <label><h3>Tanggal Pengeluaran</h3></label>
                                  <div class="input-group date" id="datepicker" data-provide="datepicker">
                                      <asp:TextBox ID="tgledit" runat="server" CssClass="form-control" Placeholder="dd/mm/yyyy" onkeydown="return false;"></asp:TextBox>
                                      <div class="input-group-addon">
                                           <span class="glyphicon glyphicon-th"></span>
                                      </div>
                                  </div>

                                  <br />
                                  <label><h3>Plih bagian</h3></label>
                                  <asp:DropDownList ID="bagianDledit" runat="server" CssClass="form-control"/>

                                  <br />
                                  <label><h3>Vendor</h3></label>
                                  <asp:TextBox ID="vendoredit" runat="server" CssClass="form-control"></asp:TextBox>

                                  <br />
                                  <label for="radioya"><h3>Jasa : </h3></label>
                                  <asp:RadioButton ID="radioyaedit" runat="server" CssClass="radio radio-inline" Text="Ya" GroupName="jasa" Checked="false" />
                                  <asp:RadioButton ID="radiotidakedit" runat="server" CssClass="radio radio-inline" Text="Tidak" GroupName="jasa" Checked="false"/>
                                  
                                  <asp:TextBox ID ="saldoedit" runat="server" Visible="false"></asp:TextBox>
                                  <asp:TextBox ID ="saldotemp" runat="server" Visible="false"></asp:TextBox>
                                  <asp:TextBox ID ="saldoafteredit" runat="server" Visible="false"></asp:TextBox>
                              </div>
                              <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Batal</button>
                                  <asp:LinkButton ID="update" runat="server" OnClientClick="return isDataValidEdit()" OnClick="Update_Click" CssClass="btn btn-primary"><i class="fa fa-refresh"></i> Ubah</asp:LinkButton>
                                <asp:LinkButton ID="delete" runat="server" OnClientClick="return confirm('Yakin ingin dihapus ?');" OnClick="delete_Click" CssClass="btn btn-danger navbar-left"><i class="fa fa-trash-o"></i> Hapus</asp:LinkButton>
                              </div>
                            </div>
                          </div>
                        </div>
                    <br />
                    <%-- AKHIR FORM EDIT MODAL --%>
                    <br />
                        <asp:GridView ID="gvBioCash" runat="server" BorderColor="Transparent" DataKeyNames="id" ClientIDMode="Static" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="table table-striped table-responsive table-bordered-hover" CellPadding="4" ForeColor="#333333" GridLines="Vertical" OnRowDeleting="RowDeleting" >
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="idkeluarlabel" runat="server" Text='<%#Eval("id") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                         <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tanggal">
                                    <ItemTemplate>
                                        <asp:Label ID="tgllabel" runat="server" Text='<%#Eval("tgl_keluar") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Kas">
                                    <ItemTemplate>
                                        <asp:Label ID="kaslabel" runat="server" Text='<%#Eval("Kas") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bagian">
                                    <ItemTemplate>
                                        <asp:Label ID="bagianlabel" runat="server" Text='<%#Eval("nama_bagian") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Periode">
                                    <ItemTemplate>
                                        <asp:Label ID="periodelabel" runat="server" Text='<%#Eval("thn_periode") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Harga">
                                    <ItemTemplate>
                                        <asp:Label ID="hargalabel" runat="server" Text='<%#Eval("harga") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QTY">
                                    <ItemTemplate>
                                        <asp:Label ID="quantitylabel" runat="server" Text='<%#Eval("unit") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Keperluan">
                                    <ItemTemplate>
                                        <asp:Label ID="keteranganlabel" runat="server" Text='<%#Eval("keterangan") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Vendor">
                                    <ItemTemplate>
                                        <asp:Label ID="vendorlabel" runat="server" Text='<%#Eval("vendor") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Satuan">
                                    <ItemTemplate>
                                        <asp:Label ID="satuanlabel" runat="server" Text='<%#Eval("satuan") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Kredit">
                                    <ItemTemplate>
                                        <asp:Label ID="pphlabel" runat="server" Text='<%#Eval("pph") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Debit">
                                    <ItemTemplate>
                                        <asp:Label ID="jmlhlabel" runat="server" Text='<%#Eval("jmlh_keluar") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jasa">
                                    <ItemTemplate>
                                        <asp:Label ID="jasalabel" runat="server" Text='<%#Eval("jasa") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Aksi">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_edit" runat="server" OnClick="btn_edit_Click" CssClass="btn btn-circle btn-success" ToolTip="Ubah data"><i class="fa fa-edit"></i> </asp:LinkButton>
                                        <asp:LinkButton ID="btn_delete" OnClientClick="return confirm('Yakin ingin dihapus ?');" CommandName="Delete" runat="server" CssClass="btn btn-circle btn-danger" ToolTip="Hapus data"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                            <HeaderStyle BackColor="#eb9d46" ForeColor="White" Font-Size="Medium"/>
                            <RowStyle ForeColor="black" />
                        </asp:GridView>

                    <%-- FORM SHOW SALDO --%>
                        <div class="modal fade" id="formsaldo" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                          <div class="modal-dialog modal-dialog-centered" role="document">
                            <div class="modal-content">
                              <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                  <span aria-hidden="true">&times;</span>
                                </button>
                                <h2 class="modal-title" id="showsaldo">Saldo Saat Ini</h2>
                              </div>
                              <div class="modal-body">
                                  <asp:GridView ID="gvSaldo" runat="server" BorderColor="Transparent" ClientIDMode="Static" DataKeyNames="Saldo" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="table table-striped table-responsive table-bordered-hover">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Kas">
                                                <ItemTemplate>
                                                    <asp:Label ID="Kas" runat="server" Text='<%#Eval("Kas") %>' Font-Size="Medium"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Periode">
                                                <ItemTemplate>
                                                    <asp:Label ID="Kas" runat="server" Text='<%#Eval("thn_periode") %>' Font-Size="Medium"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Saldo">
                                                <ItemTemplate>
                                                    <asp:Label ID="Saldo" runat="server" Text='<%#Eval("Saldo") %>' Font-Size="Medium"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                      <EmptyDataRowStyle HorizontalAlign="Center" />
                                     <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                      <HeaderStyle BackColor="#eb9d46" ForeColor="White" Font-Size="Large"/>
                                      <RowStyle Font-Size="Medium" ForeColor="Black"/>
                                </asp:GridView>
                              </div>
                            </div>
                          </div>
                        </div>
                    <%-- AKHIR FORM SHOW SALDO --%>

                    </div>
                <div class="footer">
                    <div>
                        <strong>Copyright</strong> &copy; 2016 PT Bio Farma (Persero)
                    </div>
                </div>
            </div>
        </div>

        <!-- Mainly scripts -->
        <script src='<%: ResolveClientUrl("~/JS/jquery-2.1.1.js") %>'></script>
        <script src='<%: ResolveClientUrl("~/JS/bootstrap.min.js") %>'></script>
        <script src='<%: ResolveClientUrl("~/JS/plugins/metisMenu/jquery.metisMenu.js") %>'></script>
        <script src='<%: ResolveClientUrl("~/JS/plugins/slimscroll/jquery.slimscroll.js") %>'></script>

        <!-- Custom and plugin javascript -->
        <script src='<%: ResolveClientUrl("~/JS/inspinia.js") %>'></script>
        <script src='<%: ResolveClientUrl("~/JS/plugins/toastr/toastr.min.js") %>'></script>
        <!-- Date picker -->
        <script src='<%: ResolveClientUrl("~/JS/plugins/datapicker/bootstrap-datepicker.js") %>'></script>
        <script src='<%: ResolveClientUrl("~/JS/plugins/clockpicker/clockpicker.js") %>'></script>
        <%--Data Tables--%>
        <script src='<%: ResolveClientUrl("~/JS/plugins/dataTables/datatables.min.js") %>'></script>
        <%--<script src='<%: ResolveClientUrl("~/Assets/DataTables/DT/js/jquery.dataTables.js") %>'></script>--%>
        <%--Icheck--%>
        <script src='<%: ResolveClientUrl("~/JS/plugins/iCheck/icheck.min.js") %>'></script>
        <%--List JS--%>
        <script src='<%: ResolveClientUrl("~/JS/plugins/listjs/list.js") %>'></script>
        <%--Icheck--%>
        <script src='<%: ResolveClientUrl("~/JS/plugins/iCheck/icheck.min.js") %>'></script>
        <script src='<%: ResolveClientUrl("~/JS/plugins/RandColor/randomColor.js") %>'></script>
        <%--Input Mask--%>
        <script src='<%: ResolveClientUrl("~/JS/plugins/jasny/jasny-bootstrap.min.js") %>'></script>
        <script src='<%: ResolveClientUrl("~/JS/plugins/sweetalert/sweetalert.min.js") %>'></script>
        <script>
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": false,
                "progressBar": true,
                "positionClass": "toast-bottom-right",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
            var options = {
                valueNames: ['name']
            };
            var menuList = new List('wrapper', options);
        </script>
        <div class="theme-config">
            <div class="theme-config-box">
                <div class="spin-icon">
                    <i class="fa fa-cogs fa-spin"></i>
                </div>
                <div class="skin-setttings">
                    <div class="title">Configuration</div>
                    <div class="setings-item">
                        <span>Collapse menu
                        </span>

                        <div class="switch">
                            <div class="onoffswitch">
                                <input type="checkbox" name="collapsemenu" class="onoffswitch-checkbox" id="collapsemenu">
                                <label class="onoffswitch-label" for="collapsemenu">
                                    <span class="onoffswitch-inner"></span>
                                    <span class="onoffswitch-switch"></span>
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="setings-item">
                        <span>Fixed sidebar
                        </span>

                        <div class="switch">
                            <div class="onoffswitch">
                                <input type="checkbox" name="fixedsidebar" class="onoffswitch-checkbox" id="fixedsidebar">
                                <label class="onoffswitch-label" for="fixedsidebar">
                                    <span class="onoffswitch-inner"></span>
                                    <span class="onoffswitch-switch"></span>
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="setings-item">
                        <span>Top navbar
                        </span>

                        <div class="switch">
                            <div class="onoffswitch">
                                <input type="checkbox" name="fixednavbar" class="onoffswitch-checkbox" id="fixednavbar">
                                <label class="onoffswitch-label" for="fixednavbar">
                                    <span class="onoffswitch-inner"></span>
                                    <span class="onoffswitch-switch"></span>
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="setings-item">
                        <span>Top navbar v.2
                        <br>
                            <small>*Primary layout</small>
                        </span>

                        <div class="switch">
                            <div class="onoffswitch">
                                <input type="checkbox" name="fixednavbar2" class="onoffswitch-checkbox" id="fixednavbar2">
                                <label class="onoffswitch-label" for="fixednavbar2">
                                    <span class="onoffswitch-inner"></span>
                                    <span class="onoffswitch-switch"></span>
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="setings-item">
                        <span>Boxed layout
                        </span>

                        <div class="switch">
                            <div class="onoffswitch">
                                <input type="checkbox" name="boxedlayout" class="onoffswitch-checkbox" id="boxedlayout">
                                <label class="onoffswitch-label" for="boxedlayout">
                                    <span class="onoffswitch-inner"></span>
                                    <span class="onoffswitch-switch"></span>
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="setings-item">
                        <span>Fixed footer
                        </span>

                        <div class="switch">
                            <div class="onoffswitch">
                                <input type="checkbox" name="fixedfooter" class="onoffswitch-checkbox" id="fixedfooter">
                                <label class="onoffswitch-label" for="fixedfooter">
                                    <span class="onoffswitch-inner"></span>
                                    <span class="onoffswitch-switch"></span>
                                </label>
                            </div>
                        </div>
                    </div>

                    <div class="title">Skins</div>
                    <div class="setings-item default-skin">
                        <span class="skin-name ">
                            <a href="#" class="s-skin-0">Default
                            </a>
                        </span>
                    </div>
                    <div class="setings-item blue-skin">
                        <span class="skin-name ">
                            <a href="#" class="s-skin-1">Blue light
                            </a>
                        </span>
                    </div>
                    <div class="setings-item yellow-skin">
                        <span class="skin-name ">
                            <a href="#" class="s-skin-3">Yellow/Purple
                            </a>
                        </span>
                    </div>
                    <div class="setings-item ultra-skin">
                        <span class="skin-name ">
                            <a href="#" class="md-skin">Material Design
                            </a>
                        </span>
                    </div>
                    <div class="setings-item yellow-skin">
                        <span class="skin-name ">
                            <a href="#" class="md-skin-2">MD Yellow/Purple
                            </a>
                        </span>
                    </div>
                </div>
            </div>
        </div>
        <script>
            // Config 
            function isDataValid() {
                var kasDl = document.getElementById("kasDl").value;
                var satuan = document.getElementById("satuan").value;
                var harga = document.getElementById("harga").value;
                var jmlhkeluar = document.getElementById("jmlhkeluar").value;
                var quantity = document.getElementById("quantity").value;
                var keperluan = document.getElementById("keperluan").value
                var tglkeluar = document.getElementById("tgl_keluar").value;
                var periodeDl = document.getElementById("periodeDl").value;
                var bagianDl = document.getElementById("bagianDl").value;
                var vendor = document.getElementById("vendor").value;

                if (kasDl == '--Pilih kas--') {
                    alert("Pilih kas");
                    return false;
                }

                if (satuan == '') {
                    alert("Satuan harus diisi");
                    return false;
                }

                if (harga == '') {
                    alert("harga harus diisi");
                    return false;
                }

                if (quantity == '') {
                    alert("Quantity harus diisi");
                    return false;
                }

                if (jmlhkeluar == '') {
                    alert("Total harga harus terisi");
                    return false;
                }

                if (keperluan == '') {
                    alert("Keperluan harus dipilih");
                    return false;
                }

                if (tglkeluar == '') {
                    alert("Tanggal harus dipilih");
                    return false;
                }

                if (periodeDl == '--Pilih periode--') {
                    alert("Pilih periode");
                    return false;
                }

                if (bagianDl == '--Pilih bagian--') {
                    alert("Pilih bagian");
                    return false;
                }

                if (vendor == '') {
                    alert("Vendor harus diisi");
                    return false;
                }

                if (!document.getElementById("<%= radioya.ClientID %>").checked && !document.getElementById("<%= radiotidak.ClientID %>").checked)    
                {    
                alert("Jasa harus dipilih");    
                return false;    
                }
            }

            function isDataValidEdit() {
                var satuanedit = document.getElementById("satuanedit").value;
                var hargaedit = document.getElementById("hargaedit").value;
                var quantityedit = document.getElementById("quantityedit").value;
                var jmlhkeluaredit = document.getElementById("jmlhkeluaredit").value;
                var keperluanedit = document.getElementById("keteranganedit").value;
                var tgledit = document.getElementById("tgledit").value;
                var bagianDledit = document.getElementById("bagianDledit").value;
                var vendoredit = document.getElementById("vendoredit").value;

                if (satuanedit == '') {
                    alert("Satuan harus diisi");
                    return false;
                }

                if (hargaedit == '') {
                    alert("Harga harus diisi");
                    return false;
                }

                if (quantityedit == '') {
                    alert("Quantity harus diisi");
                    return false;
                }

                if (jmlhkeluaredit == '') {
                    alert("Total harga harus terisi");
                    return false;
                }

                if (keperluanedit == '') {
                    alert("Keperluan harus diisi");
                    return false;
                }

                if (tgledit == '') {
                    alert("Tangga; harus dipilih");
                    return false;
                }

                if (bagianDledit == '--Pilih bagian--') {
                    alert("Bagian harus dipilih");
                    return false;
                }

                if (vendoredit == '') {
                    alert("Vendor harus diisi");
                    return false;
                }

                if (!document.getElementById("<%= radioyaedit.ClientID %>").checked && !document.getElementById("<%= radiotidakedit.ClientID %>").checked)    
                {    
                alert("Jasa harus dipilih");    
                return false;    
                }
            }

            //Open Modal
            function openModalUpdatePeriode() {
                    $('[id*=updateperiode]').modal('show');
            }

            //Open Modal
            function openModalUpdateMin() {
                    $('[id*=updatemin]').modal('show');
            }

            //Open Modal
            function openModalConfirmPeriode() {
                    $('[id*=confirmperiode]').modal('show');
            }

            //Open Modal
            function openModalConfirm() {
                    $('[id*=confirmmasuk]').modal('show');
            }

            //Open Modal
            function multiplyEdit()
                        {

                            var txt1 = document.getElementById("hargaedit").value;
                            var txt2 = document.getElementById("quantityedit").value;

                                    var total = txt1 * txt2;
                                    document.getElementById("jmlhkeluaredit").value = total;

                        }

            function multiply()
                        {

                            var txt1 = document.getElementById("harga").value;
                            var txt2 = document.getElementById("quantity").value;

                                    var total = txt1 * txt2;
                                    document.getElementById("jmlhkeluar").value = total;

                        }

            function openModalInput() {
                    $('[id*=forminput]').modal('show');
            }

            //Open Modal
            function openModal() {
                    $('[id*=formedit]').modal('show');
            }
            
            //Gridview Bootstrap
            $(function () {
                $('[id*=gvBioCash]').prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                    "responsive": true,
                    "sPaginationType": "full_numbers"
                });
            });

            //date picker
                $("#datepicker").datepicker( {
                    
                    autoclose: true
                });

            //year picker
                $("#yearpicker").datepicker( {
                    format: "yyyy",
                    viewMode: "years", 
                    minViewMode: "years",
                    autoclose: true
                });

            // Enable/disable fixed top navbar
            $('#fixednavbar').click(function () {
                if ($('#fixednavbar').is(':checked')) {
                    $(".navbar-static-top").removeClass('navbar-static-top').addClass('navbar-fixed-top');
                    $("body").removeClass('boxed-layout');
                    $("body").addClass('fixed-nav');
                    $('#boxedlayout').prop('checked', false);

                    if (localStorageSupport) {
                        localStorage.setItem("boxedlayout", 'off');
                    }

                    if (localStorageSupport) {
                        localStorage.setItem("fixednavbar", 'on');
                    }
                } else {
                    $(".navbar-fixed-top").removeClass('navbar-fixed-top').addClass('navbar-static-top');
                    $("body").removeClass('fixed-nav');
                    $("body").removeClass('fixed-nav-basic');
                    $('#fixednavbar2').prop('checked', false);

                    if (localStorageSupport) {
                        localStorage.setItem("fixednavbar", 'off');
                    }

                    if (localStorageSupport) {
                        localStorage.setItem("fixednavbar2", 'off');
                    }
                }
            });

            // Enable/disable fixed top navbar
            $('#fixednavbar2').click(function () {
                if ($('#fixednavbar2').is(':checked')) {
                    $(".navbar-static-top").removeClass('navbar-static-top').addClass('navbar-fixed-top');
                    $("body").removeClass('boxed-layout');
                    $("body").addClass('fixed-nav').addClass('fixed-nav-basic');
                    $('#boxedlayout').prop('checked', false);

                    if (localStorageSupport) {
                        localStorage.setItem("boxedlayout", 'off');
                    }

                    if (localStorageSupport) {
                        localStorage.setItem("fixednavbar2", 'on');
                    }
                } else {
                    $(".navbar-fixed-top").removeClass('navbar-fixed-top').addClass('navbar-static-top');
                    $("body").removeClass('fixed-nav').removeClass('fixed-nav-basic');
                    $('#fixednavbar').prop('checked', false);

                    if (localStorageSupport) {
                        localStorage.setItem("fixednavbar2", 'off');
                    }
                    if (localStorageSupport) {
                        localStorage.setItem("fixednavbar", 'off');
                    }
                }
            });

            // Enable/disable fixed sidebar
            $('#fixedsidebar').click(function () {
                if ($('#fixedsidebar').is(':checked')) {
                    $("body").addClass('fixed-sidebar');
                    $('.sidebar-collapse').slimScroll({
                        height: '100%',
                        railOpacity: 0.9
                    });

                    if (localStorageSupport) {
                        localStorage.setItem("fixedsidebar", 'on');
                    }
                } else {
                    $('.sidebar-collapse').slimscroll({ destroy: true });
                    $('.sidebar-collapse').attr('style', '');
                    $("body").removeClass('fixed-sidebar');

                    if (localStorageSupport) {
                        localStorage.setItem("fixedsidebar", 'off');
                    }
                }
            });

            // Enable/disable collapse menu
            $('#collapsemenu').click(function () {
                if ($('#collapsemenu').is(':checked')) {
                    $("body").addClass('mini-navbar');
                    SmoothlyMenu();

                    if (localStorageSupport) {
                        localStorage.setItem("collapse_menu", 'on');
                    }

                } else {
                    $("body").removeClass('mini-navbar');
                    SmoothlyMenu();

                    if (localStorageSupport) {
                        localStorage.setItem("collapse_menu", 'off');
                    }
                }
            });

            // Enable/disable boxed layout
            $('#boxedlayout').click(function () {
                if ($('#boxedlayout').is(':checked')) {
                    $("body").addClass('boxed-layout');
                    $('#fixednavbar').prop('checked', false);
                    $('#fixednavbar2').prop('checked', false);
                    $(".navbar-fixed-top").removeClass('navbar-fixed-top').addClass('navbar-static-top');
                    $("body").removeClass('fixed-nav');
                    $("body").removeClass('fixed-nav-basic');
                    $(".footer").removeClass('fixed');
                    $('#fixedfooter').prop('checked', false);

                    if (localStorageSupport) {
                        localStorage.setItem("fixednavbar", 'off');
                    }

                    if (localStorageSupport) {
                        localStorage.setItem("fixednavbar2", 'off');
                    }

                    if (localStorageSupport) {
                        localStorage.setItem("fixedfooter", 'off');
                    }


                    if (localStorageSupport) {
                        localStorage.setItem("boxedlayout", 'on');
                    }
                } else {
                    $("body").removeClass('boxed-layout');

                    if (localStorageSupport) {
                        localStorage.setItem("boxedlayout", 'off');
                    }
                }
            });

            // Enable/disable fixed footer
            $('#fixedfooter').click(function () {
                if ($('#fixedfooter').is(':checked')) {
                    $('#boxedlayout').prop('checked', false);
                    $("body").removeClass('boxed-layout');
                    $(".footer").addClass('fixed');

                    if (localStorageSupport) {
                        localStorage.setItem("boxedlayout", 'off');
                    }

                    if (localStorageSupport) {
                        localStorage.setItem("fixedfooter", 'on');
                    }
                } else {
                    $(".footer").removeClass('fixed');

                    if (localStorageSupport) {
                        localStorage.setItem("fixedfooter", 'off');
                    }
                }
            });

            // SKIN Select
            $('.spin-icon').click(function () {
                $(".theme-config-box").toggleClass("show");
            });
            function removeAllSkin() {
                $("body").removeClass("skin-1");
                $("body").removeClass("skin-2");
                $("body").removeClass("skin-3");
                $("body").removeClass("mdskin");
                $("body").removeClass("mdskin2");
            }
            // Default skin
            $('.s-skin-0').click(function () {
                removeAllSkin();
            });

            // Blue skin
            $('.s-skin-1').click(function () {
                removeAllSkin();
                $("body").addClass("skin-1");
            });

            // Inspinia ultra skin
            $('.s-skin-2').click(function () {
                removeAllSkin();
                $("body").addClass("skin-2");
            });

            // Yellow skin
            $('.s-skin-3').click(function () {
                removeAllSkin();
                $("body").addClass("skin-3");
            });

            // Yellow skin
            $('.md-skin').click(function () {
                removeAllSkin();
                $("body").addClass("mdskin");
            });
            $('.md-skin-2').click(function () {
                removeAllSkin();
                $("body").addClass("mdskin2");
            });

            if (localStorageSupport) {
                var collapse = localStorage.getItem("collapse_menu");
                var fixedsidebar = localStorage.getItem("fixedsidebar");
                var fixednavbar = localStorage.getItem("fixednavbar");
                var fixednavbar2 = localStorage.getItem("fixednavbar2");
                var boxedlayout = localStorage.getItem("boxedlayout");
                var fixedfooter = localStorage.getItem("fixedfooter");

                if (collapse == 'on') {
                    $('#collapsemenu').prop('checked', 'checked');
                }
                if (fixedsidebar == 'on') {
                    $('#fixedsidebar').prop('checked', 'checked');
                }
                if (fixednavbar == 'on') {
                    $('#fixednavbar').prop('checked', 'checked');
                }
                if (fixednavbar2 == 'on') {
                    $('#fixednavbar2').prop('checked', 'checked');
                }
                if (boxedlayout == 'on') {
                    $('#boxedlayout').prop('checked', 'checked');
                }
                if (fixedfooter == 'on') {
                    $('#fixedfooter').prop('checked', 'checked');
                }
            }
        </script>
    </form>
</body>
</html>

