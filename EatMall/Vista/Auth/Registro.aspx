<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="EatMall.Vista.Auth.Registro" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Registro - EatMall</title>
    
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/Vista/Assets/CSS/StyleLR.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="registro-container">
            <asp:LinkButton ID="btnVolver" runat="server" CssClass="btn-volver-link" OnClick="btnVolver_Click">
                <i class="bi bi-arrow-left-circle-fill me-2"></i> Volver
            </asp:LinkButton>

            <div class="header">
                <img src="../Assets/Img/LogoEatMall.png" alt="EatMall" class="logo" />
                <h2>REGISTRO</h2>
            </div>

            <div class="form-group">
                <asp:TextBox ID="txtDocumento" runat="server" placeholder="Número de Documento" CssClass="input-estilo"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtNombre" runat="server" placeholder="Nombre" CssClass="input-estilo"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtApellido" runat="server" placeholder="Apellido" CssClass="input-estilo"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtEmail" runat="server" placeholder="Correo Electrónico" CssClass="input-estilo" TextMode="Email"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtTelefono" runat="server" placeholder="Teléfono" CssClass="input-estilo"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtPassword" runat="server" placeholder="Contraseña" CssClass="input-estilo" TextMode="Password"></asp:TextBox>
            </div>

            <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse" CssClass="btn-registrar" OnClick="btnRegistrar_Click" />

            <div class="social-login mt-4">
                <p class="small text-muted">O regístrate con</p>
                <img src="https://cdn-icons-png.flaticon.com/512/300/300221.png" alt="Google" style="width: 30px; cursor:pointer;" />
            </div>
        </div>
    </form>
</body>
</html>