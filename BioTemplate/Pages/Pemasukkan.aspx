<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pemasukkan.aspx.cs" Inherits="BioTemplate.Pages.Pemasukkan" %>
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
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
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
                            <a href="MasterUnit.aspx">Master Unit</a>
                        </li>
                        <li>
                            <a href="Pemasukkan.aspx">Pemasukkan</a>
                        </li>
                        <li>
                            <a href="Pengeluaran.aspx">Pengeluaran</a>
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
                    <h1>Pemasukkan Saldo Kas</h1>
                    <br />
                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#forminput">
                                  <i class="fa fa-plus"></i> Tambah Pemasukkan</button>
                    <button type="button" class="btn btn-primary navbar-right" data-toggle="modal" data-target="#formsaldo">
                                  <i class="fa fa-list-alt"></i> Lihat Saldo</button>

                    <asp:TextBox ID="jmlhsaldo" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="saldodel" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="unitdel" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="totdelsaldo" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="jsaldo" runat="server" CssClass="navbar-right" Visible="false"></asp:TextBox>
                    <%-- FORM INPUT MODAL --%>
                        <div class="modal fade" id="forminput" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                          <div class="modal-dialog modal-dialog-centered" role="document">
                            <div class="modal-content">
                              <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                  <span aria-hidden="true">&times;</span>
                                </button>
                                <h2 class="modal-title" id="exampleModalLongTitle">Tambah Pemasukkan</h2>
                              </div>
                              <div class="modal-body">
                                  <label for="Unit"><h3>Pilih unit</h3></label>
                                  <asp:DropDownList ID="unitdl" runat="server" CssClass="form-control"/>
                                  
                                  <br />
                                  <label for="Unit"><h3>Jumlah Uang</h3></label>
                                  <asp:TextBox ID="Masuk"  runat="server" CssClass="form-control" placeholder="Rp" onkeydown = " return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106 || (event.keyCode >= 48 && event.keyCode <= 57 && isNaN(event.key))) && event.keyCode!=32);"/>

                                  <br />
                                  <label for="Unit"><h3>Tanggal Pemasukkan</h3></label>
                                  <div class="input-group date" id="datepicker" data-provide="datepicker">
                                      <asp:TextBox ID="tgl_masuk" runat="server" CssClass="form-control" Placeholder="dd/mm/yyyy" onkeydown="return false;"></asp:TextBox>
                                      <div class="input-group-addon">
                                           <span class="glyphicon glyphicon-th"></span>
                                      </div>
                                  </div>

                                  <br />
                                  <label for="Unit"><h3>Tahun Periode</h3></label>
                                  <div class="input-group date" id="yearpicker" data-provide="datepicker">
                                  <asp:TextBox ID="periode_masuk" runat="server" CssClass="form-control" Placeholder="yyyy" onkeydown="return false;"></asp:TextBox>
                                      <div class="input-group-addon">
                                          <span class="glyphicon glyphicon-th"></span>
                                      </div>
                                  </div>
                                  </div>
                              <div class="modal-footer">
                                <button type="button" class="btn btn-secondary navbar-left" data-dismiss="modal">Cancel</button>
                                  <asp:LinkButton ID="simpan" runat="server" OnClientClick="return dataValid()" OnClick="Confirm_Click" CssClass="btn btn-primary"><i class="fa fa-save"></i> Simpan</asp:LinkButton>
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
                                <h2 class="modal-title" id="exampleModalCenterTitle"> Edit Unit</h2>
                              </div>
                              <div class="modal-body">
                                  <asp:TextBox ID ="id" runat="server" Visible="false"></asp:TextBox>
                                  <label for="Unit"><h3>Unit</h3></label>
                                  <asp:DropDownList ID="unitdledit" runat="server" CssClass="form-control" Enabled="false"/>
                                  
                                  <br />
                                  <label for="Unit"><h3>Jumlah Uang</h3></label>
                                  <asp:TextBox ID="jmlhmasukedit"  runat="server" CssClass="form-control" placeholder="Rp" onkeydown = " return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106 || (event.keyCode >= 48 && event.keyCode <= 57 && isNaN(event.key))) && event.keyCode!=32);"/>

                                  <br />
                                  <label for="Unit"><h3>Tanggal Pemasukkan</h3></label>
                                  <div class="input-group date" id="datepicker" data-provide="datepicker">
                                      <asp:TextBox ID="tglmasukedit" runat="server" CssClass="form-control" Placeholder="dd/mm/yyyy"></asp:TextBox>
                                      <div class="input-group-addon">
                                           <span class="glyphicon glyphicon-th"></span>
                                      </div>
                                  </div>

                                  <br />
                                  <label for="Unit"><h3>Tahun Periode</h3></label>
                                  <div class="input-group date" id="yearpicker" data-provide="datepicker">
                                  <asp:TextBox ID="thnmasukedit" runat="server" CssClass="form-control" Placeholder="yyyy"></asp:TextBox>
                                      <div class="input-group-addon">
                                          <span class="glyphicon glyphicon-th"></span>
                                      </div>
                                  </div>

                                  <asp:TextBox ID ="saldoedit" runat="server" Visible="false"></asp:TextBox>
                                  <asp:TextBox ID ="saldotemp" runat="server" Visible="false"></asp:TextBox>
                                  <asp:TextBox ID ="saldoafteredit" runat="server" Visible="false"></asp:TextBox>
                                  </div>
                              <div class="modal-footer">
                                  <button type="button" class="btn btn-secondary navbar-left" data-dismiss="modal">Cancel</button>
                                  <asp:LinkButton ID="update" runat="server" CssClass="btn btn-primary" OnClientClick="return dataValidEdit();" OnClick="update_Click"><i class="fa fa-refresh"></i> Ubah</asp:LinkButton>
                                <asp:LinkButton ID="delete" runat="server" OnClientClick="return confirm('Yakin ingin dihapus ?');" OnClick="delete_Click" CssClass="btn btn-danger"><i class="fa fa-trash-o"></i> Hapus</asp:LinkButton>
                              </div>
                          </div>
                        </div>
                       </div>
                    <%-- AKHIR FORM EDIT MODAL --%>
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
                                  <asp:GridView ID="gvSaldo" runat="server" BorderColor="transparent" ClientIDMode="Static" DataKeyNames="Saldo" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="table table-striped table-responsive table-bordered-hover">
                                    <Columns>
                                        <asp:TemplateField HeaderText="UNIT">
                                                <ItemTemplate>
                                                    <asp:Label ID="Unit" runat="server" Text='<%#Eval("Unit") %>' Font-Size="Medium"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SALDO">
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
                                  <asp:TextBox ID="TextBox1" runat="server" Visible="false"/>
                                  <asp:TextBox ID="TextBox2" runat="server" Visible="false"/>
                              </div>
                            </div>
                          </div>
                        </div>
                    <%-- AKHIR FORM SHOW SALDO --%>
                    <br />
                    <br />
                        <asp:GridView ID="gvBioCash" runat="server" BorderColor="Black" DataKeyNames="id" ClientIDMode="Static" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="table table-striped table-responsive table-bordered-hover" CellPadding="4" ForeColor="#333333" GridLines="Vertical" OnRowDeleting="RowDeleting" >
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="idmasuklabel" runat="server" Text='<%#Eval("id") %>' Font-Size="Medium"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TANGGAL MASUK" >
                                    <ItemTemplate>
                                        <asp:Label ID="tgllabel" runat="server" Text='<%#Eval("tgl_masuk") %>' Font-Size="Medium"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PERIODE">
                                    <ItemTemplate>
                                        <asp:Label ID="thnlabel" runat="server" Text='<%#Eval("thn_periode") %>' Font-Size="Medium"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UNIT">
                                    <ItemTemplate>
                                        <asp:Label ID="unitlabel" runat="server" Text='<%#Eval("Unit") %>' Font-Size="Medium"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="JUMLAH">
                                    <ItemTemplate>
                                        <asp:Label ID="jmlhlabel" runat="server" Text='<%#Eval("jmlh_masuk") %>' Font-Size="Medium"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AKSI">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_edit" runat="server" OnClick="btn_edit_Click" CssClass="btn btn-success"><i class="fa fa-edit"></i> Ubah</asp:LinkButton>
                                        <asp:LinkButton ID="btn_delete" OnClientClick="return confirm('Yakin ingin dihapus ?');" CommandName="Delete" runat="server" CssClass="btn btn-danger"><i class="fa fa-trash"></i> Hapus</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                            <HeaderStyle BackColor="#eb9d46" ForeColor="White" Font-Size="Large"/>
                            <RowStyle ForeColor="black" />
                        </asp:GridView>
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
        <script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.min.js"></script>
        <script type="text/javascript" src="https://cdn.datatables.net/responsive/1.0.7/js/dataTables.responsive.min.js"></script>
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
            // Config box

            //Pop up modal Edit
            function openModal() {
                    $('[id*=formedit]').modal('show');
            }

            //Validation isEmpty edit
            function dataValidEdit() {    
                var unitdledit = document.getElementById("unitdledit").value;
                var jmlhmasukedit = document.getElementById("jmlhmasukedit").value;
                var tglmasukedit = document.getElementById("tglmasukedit").value;
                var thnmasukedit = document.getElementById("thnmasukedit").value;

                if (unitdledit == '')    
               {    
                alert("Pilih kas kecil");    
                return false;    
                }    

                if (jmlhmasukedit == '')    
               {    
                alert("Masukkan jumlah uang");    
                return false;    
               }   

                if (tglmasukedit == '')    
               {    
               alert("Pilih tanggal pemasukkan");    
               return false;    
                }  

                if (thnmasukedit == '')
                {    
                alert("Pilih tahun periode");    
                return false;    
                } 
            }    

            //Validation isEmpty input
            function dataValid() {    
                var Unit= document.getElementById("unitdl").value;    
                var masuk = document.getElementById("Masuk").value;     
                var tgl_masuk = document.getElementById("tgl_masuk").value;
                var periode_masuk = document.getElementById("periode_masuk").value;

                if (Unit == '')    
               {    
                alert("Pilih kas kecil");    
                return false;    
                }    

                if (masuk == '')    
               {    
                alert("Masukkan jumlah uang");    
                return false;    
               }   

                if (tgl_masuk == '')    
               {    
               alert("Pilih tanggal pemasukkan");    
               return false;    
                }  

                if (periode_masuk == '')    
               {    
               alert("Pilih tahun periode");    
               return false;    
               } 
            }

            //Gridview Bootstrap
            $(function () {
                $('[id*=gvBioCash]').prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                    "responsive": true,
                    "sPaginationType": "full_numbers"
                });
            });

        //change page
            $(document).ready(function(){  
              $("#cpage").click(function(e) {
                e.preventDefault();
                $('html, body').animate({
                  scrollTop: $($.attr(this, 'href')).offset().top
                }, 2000);
              });
            });

            //date picker
                $("#datepicker").datepicker({
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