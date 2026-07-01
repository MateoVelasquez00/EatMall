<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EatMall.Vista.Auth.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
	<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
	<link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />
	<link href="/Vista/Assets/CSS/StyleLR.css" rel="stylesheet" />
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title></title>
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
					<h2>INICIAR SESIÓN</h2>
				</div>

				<div class="form-group">
					<label class="form-label-custom">Correo Electrónico</label>
					<asp:TextBox ID="txtEmail" runat="server" CssClass="input-estilo" placeholder="ejemplo@correo.com" TextMode="Email"></asp:TextBox>
				</div>

				<div class="form-group">
					<label class="form-label-custom">Contraseña</label>
					<asp:TextBox ID="txtPassword" runat="server" CssClass="input-estilo" placeholder="********" TextMode="Password"></asp:TextBox>
				</div>

				<div class="d-flex justify-content-between mb-4">
					<div class="form-check">
						<input class="form-check-input" type="checkbox" id="remember" />
						<label class="form-check-label small text-muted" for="remember">Recuérdame</label>
					</div>
					<a href="#" class="forgot-link">¿Olvidaste tu contraseña?</a>
				</div>

				<div class="form-check form-switch d-flex align-items-center justify-content-center gap-2 mb-4">
					<asp:CheckBox ID="chkTipo" runat="server" Checked="false" CssClass="custom-switch" />
					<label class="form-check-label text-muted small fw-bold" style="cursor: pointer;" for="chkTipo">MODO ADMINISTRADOR</label>
				</div>

				<div class="form-label mb-3">
					<asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
				</div>
				<asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn-ingresar" OnClick="btnIngresar_Click" />

				<div class="register-link mt-4">
					<p class="small text-muted">¿No tienes cuenta? <a href="Registro.aspx" class="text-orange fw-bold">Regístrate aquí</a></p>
				</div>
			</div>
		</div>
	</form>
</body>
</html>
