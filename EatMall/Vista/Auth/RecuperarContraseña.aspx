<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperarContraseña.aspx.cs" Inherits="EatMall.Vista.Auth.RecuperarContraseña" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title>Rcuperar Contraseña - EatMall</title>

	<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
	<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css" />
	<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
	<link href="/Vista/Assets/CSS/StyleLR.css" rel="stylesheet" />
</head>
<body>
	<form id="form2" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

		<div class="login-page">
			<div class="login-container">
				<asp:LinkButton ID="btnVolver" runat="server" CssClass="btn-volver-link" OnClick="btnVolver_Click">
                    <i class="bi bi-arrow-left-circle-fill me-2"></i> Volver
				</asp:LinkButton>

				<div class="header">
					<img src="../Assets/Img/LogoEatMall.png" alt="EatMall" class="logo" />
					<h2>RECUPERAR CONTRASEÑA</h2>
				</div>

				<div class="form-group">
					<asp:TextBox ID="txtEmail" runat="server" placeholder="Ingresa tu correo para validar" CssClass="input-estilo" TextMode="Email"></asp:TextBox>
				</div>

				<div class="form-group">
					<asp:TextBox ID="txtNuevaPass" runat="server" placeholder="Nueva Contraseña" CssClass="input-estilo" TextMode="Password"></asp:TextBox>
				</div>

				<div class="form-group">
					<asp:TextBox ID="txtConfirmarPass" runat="server" placeholder="Confirmar Contraseña" CssClass="input-estilo" TextMode="Password"></asp:TextBox>
				</div>

				<div class="form-label mb-3">
					<asp:Label ID="lblMensaje" CssClass="text-danger fw-bold" runat="server" Text=""></asp:Label>
				</div>
				<asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn-ingresar" OnClick="btnIngresar_Click" />
			</div>
		</div>
	</form>
</body>
</html>
