using EatMall.Datos;
using System;
using System.Web.UI;

namespace EatMall.Vista.Usuario.GestionAdmin
{
    public partial class GestionUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Cargar Primeros 25 usuarios
                CargarUsuarios(1);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = txtBusqueda.Text.Trim();

            if (!string.IsNullOrEmpty(busqueda))
            {
                btnAnterior.Visible = false;
                btnSiguiente.Visible = false;

                BusquedaD datos = new BusquedaD();
                gvUsuarios.DataSource = datos.MtBuscarUsuario(busqueda);
                gvUsuarios.DataBind();
            }
            else
            {
                CargarUsuarios(1);

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(), "warning",
                    "Swal.fire({ icon: 'warning', title: 'Campo vacío', text: 'Ingresa un usuario para buscar.' });",
                    true
                );
            }
        }

        private void CargarUsuarios(int pagina)
        {
            btnSiguiente.Visible = true; 
            btnAnterior.Visible = pagina > 1; //Visible solo si pagina es mayor a 1

            ClienteD datos = new ClienteD();
            gvUsuarios.DataSource = datos.MtListarUsuarios(pagina, 25);
            gvUsuarios.DataBind();

            //Guarda la pagina actual para que los botones sepan en que pagina estan
            ViewState["PaginaActual"] = pagina;
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            //Recupera cual era la pagina
            int pagina = Convert.ToInt32(ViewState["PaginaActual"]);
            CargarUsuarios(pagina + 1); //Carga la siguiente
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            //Recupera cual era la pagina
            int pagina = Convert.ToInt32(ViewState["PaginaActual"]);
            if (pagina > 1) CargarUsuarios(pagina - 1);//Carga la anterior
        }
    }
}