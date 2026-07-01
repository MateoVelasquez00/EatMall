<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Admin.Master" AutoEventWireup="true" CodeBehind="AdministradorCC.aspx.cs" Inherits="EatMall.Vista.Usuario.GestionAdminCC.AdministradorCC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <style>
        .bienvenida-titulo {
            font-size: 26px;
            font-weight: 700;
            color: #1d4ed8;
            margin-bottom: 28px;
        }

        .seccion-titulo {
            font-size: 18px;
            font-weight: 600;
            color: #0b1c30;
            margin-bottom: 10px;
        }

        .texto {
            font-size: 14px;
            color: #3d4a42;
            line-height: 1.7;
            margin-bottom: 16px;
        }

        .lista-descripcion {
            list-style: none;
            padding: 0;
            margin: 0 0 20px;
        }

            .lista-descripcion li {
                display: flex;
                align-items: flex-start;
                gap: 8px;
                font-size: 14px;
                color: #3d4a42;
                padding: 4px 0;
                line-height: 1.6;
            }

                .lista-descripcion li::before {
                    content: '';
                    width: 6px;
                    height: 6px;
                    border-radius: 50%;
                    background: #1d4ed8;
                    flex-shrink: 0;
                    margin-top: 8px;
                }

        .sugerencia-box {
            background: #f0f4fd;
            border: 1px solid #9bcfff;
            border-left: 4px solid #1d4ed8;
            border-radius: 8px;
            padding: 16px 20px;
        }

        .sugerencia-titulo {
            font-size: 13px;
            font-weight: 700;
            color: #1d4ed8;
            margin-bottom: 6px;
        }

        .divider {
            border: none;
            border-top: 1px solid #bccac0;
            margin: 28px 0;
        }
    </style>

    <h2 class="bienvenida-titulo">Bienvenido al Panel de Administración de Centro Comercial</h2>

    <p class="seccion-titulo">Administrador de Centro Comercial</p>
    <p class="texto">
        El rol de Administrador de Centro Comercial (AdminCC) tiene acceso a la gestión
        de su centro comercial asignado. Es el responsable de mantener actualizada la
        información de las plazoletas y los locales que operan dentro de su centro comercial.
    </p>

    <ul class="lista-descripcion">
        <li>Gestionar los locales de tu centro comercial — crear y editar</li>
        <li>Administrar las plazoletas de tu centro comercial — crear, editar y desabilitar.</li>
        <li>Actualizar el perfil del centro comercial y sus datos de contacto.</li>
        <li>Supervisar el estado (Abierto / Cerrado) de cada local en tiempo real.</li>
        <li>Visualizar y actualizar datos de CentroComercial, Plazoletas y Locales </li>
    </ul>

    <hr class="divider" />

    <div class="sugerencia-box">
        <div class="sugerencia-titulo">Sugerencia</div>
        <p class="texto" style="margin: 0;">
            Para comenzar, dirígete a <strong>PerfilCC</strong> desde el menú lateral
            para revisar los registros actuales de tu centro comercial.
            Si necesitas agregar un nuevo <strong>Local</strong> al sistema, usa la opción <strong>Locales</strong> del menú lateral.
        </p>
    </div>

</asp:Content>