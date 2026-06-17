using EatMall.Datos;
using EatMall.Logica;
using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EatMall.Vista.Usuario.GestionAdmin
{
    public partial class GestionUsuarios : System.Web.UI.Page
    {
        ClienteL datos = new ClienteL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            ClienteD clienteD = new ClienteD();
            gvUsuarios.DataSource = clienteD.MtListarTodosUsuarios();
            gvUsuarios.DataBind();
        }

        protected void gvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int idUsuario = Convert.ToInt32(gvUsuarios.DataKeys[e.Row.RowIndex].Value);
                CheckBoxList chkRoles = (CheckBoxList)e.Row.FindControl("chkRoles");

                List<Rol> todosLosRoles = datos.MtObtenerTodosLosRoles();
                List<Rol> rolesUsuario = datos.MtObtenerRolesPorUsuario(idUsuario);

                chkRoles.DataSource = todosLosRoles;
                chkRoles.DataTextField = "Nombre";
                chkRoles.DataValueField = "Id";
                chkRoles.DataBind();

                foreach (ListItem item in chkRoles.Items)
                {
                    item.Selected = rolesUsuario.Any(r => r.Id.ToString() == item.Value);
                }
            }
        }

        protected void btnGuardarRol_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idUsuario = Convert.ToInt32(btn.CommandArgument);

            GridViewRow fila = (GridViewRow)btn.NamingContainer;
            CheckBoxList chkRoles = (CheckBoxList)fila.FindControl("chkRoles");

            List<Rol> rolesActuales = datos.MtObtenerRolesPorUsuario(idUsuario);

            foreach (ListItem item in chkRoles.Items)
            {
                int idRol = Convert.ToInt32(item.Value);
                bool yaLoTiene = rolesActuales.Any(r => r.Id == idRol);

                if (item.Selected && !yaLoTiene)
                    datos.MtCambiarRol(idUsuario, idRol, true);

                if (!item.Selected && yaLoTiene)
                    datos.MtCambiarRol(idUsuario, idRol, false);
            }

            CargarUsuarios();
        }
    }
}