using Dominio;
using LecturaDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Principal
{
    public partial class VentanaCategoriasyMarcas : Form
    {
        //Constructor
        public VentanaCategoriasyMarcas()
        {
            InitializeComponent();
        }

        //Listas obj
        private List<Categoria> ListaLecturaCategorias;

        private List<Marca> listaLecturaMarcas;
        
        //Cargo img de tablas 
        private void cargarmarca()
        {
            LecturaMarca lecturaMar = new LecturaMarca();
            listaLecturaMarcas = lecturaMar.listar();
            dgvMarcas.DataSource = lecturaMar.listar();
        }
        private void cargarcategoria()
        {
            LecturaCategoria lecturaCat = new LecturaCategoria();
            ListaLecturaCategorias = lecturaCat.listar();
            dgvListaCategorias.DataSource = lecturaCat.listar();
        }

        //Metodos para Agregar datos a la dgv
        private void Agregar(Marca marca)
        {
            LecturaMarca lecturaMarca = new LecturaMarca();
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Seguro desea Agregar esta Marca?", "Modificando", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    marca.Descripcion = txtAgregarMarca.Text;
                    lecturaMarca.agregar(marca);

                    LecturaMarca lecturaMar = new LecturaMarca();
                    listaLecturaMarcas = lecturaMar.listar();
                    dgvMarcas.DataSource = lecturaMar.listar();

                }
                txtAgregarMarca.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Agregar(Categoria categoria)
        {
            LecturaCategoria lecturaCat = new LecturaCategoria();
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Seguro desea agregar esta Categoria?", "Agregando", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    categoria.Descripcion = textAgregarCategoria.Text;
                    lecturaCat.agregar(categoria);

                    ListaLecturaCategorias = lecturaCat.listar();
                    dgvListaCategorias.DataSource = lecturaCat.listar();

                }
                textAgregarCategoria.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        //Metodos para modificar datos de las dgv
        private void Modificar(Categoria categoria)
        {
            LecturaCategoria lecturaCat = new LecturaCategoria();
            try
            {
                if (string.IsNullOrEmpty(textAgregarCategoria.Text))
                {
                    lblCamporequeridoCategoria.Text = "*Campo Requerido*";
                    return;
                }
                DialogResult respuesta = MessageBox.Show("¿Seguro desea modificar esta Marca?", "Modificando", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    categoria.Descripcion = textAgregarCategoria.Text;
                    lecturaCat.modificar(categoria);

                    ListaLecturaCategorias = lecturaCat.listar();
                    dgvListaCategorias.DataSource = lecturaCat.listar();

                }
                textAgregarCategoria.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void Modificar(Marca marca)
        {
            LecturaMarca lecturaMar = new LecturaMarca();
            try
            {
                if(string.IsNullOrEmpty(txtAgregarMarca.Text))
                {
                    lblCamporequeridoMarca.Text = "*Campo Requerido*";
                    return;
                }
                DialogResult respuesta = MessageBox.Show("¿Seguro desea modificar esta Marca?", "Modificando", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    marca.Descripcion = txtAgregarMarca.Text;
                    lecturaMar.modificar(marca);

                    listaLecturaMarcas = lecturaMar.listar();
                    dgvMarcas.DataSource = lecturaMar.listar();
                }
                txtAgregarMarca.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Metodos para eliminar datos de la dgv
        private void Eliminar(Categoria categoria)
        {
            LecturaCategoria cat = new LecturaCategoria();

            try
            {
                DialogResult respuesta = MessageBox.Show("¿Seguro desea eleminar esta categoria?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    cat.eliminar(categoria.Id);
                    cargarcategoria();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Eliminar(Marca marca)
        {
            LecturaMarca mar = new LecturaMarca();

            try
            {
                DialogResult respuesta = MessageBox.Show("¿Seguro desea eleminar esta categoria?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    mar.eliminar(marca.Id);
                    cargarmarca();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Filtros
        private void FiltrarMarca()
        {
            List<Marca> listaFiltrada;
            try
            {
                string opcion = cboMarcas.SelectedItem.ToString();
                if (opcion == "Nro ID asc.")
                {
                    listaFiltrada = listaLecturaMarcas.OrderBy(x => x.Id).ToList();
                }
                else if (opcion == "Nro ID des.")
                {
                    listaFiltrada = listaLecturaMarcas.OrderByDescending(x => x.Id).ToList();
                }
                else
                {
                    listaFiltrada = listaLecturaMarcas.OrderBy(x => x.Descripcion).ToList();
                }
                dgvMarcas.DataSource = null;
                dgvMarcas.DataSource = listaFiltrada;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void FiltrarCategoria()
        {
            List<Categoria> listaFiltrada;
            try
            {
                string opcion = cboCategoria.SelectedItem.ToString();
                if (opcion == "Nro ID asc.")
                {
                    listaFiltrada = ListaLecturaCategorias.OrderBy(x => x.Id).ToList();
                }
                else if (opcion == "Nro ID des.")
                {
                    listaFiltrada = ListaLecturaCategorias.OrderByDescending(x => x.Id).ToList();
                }
                else
                {
                    listaFiltrada = ListaLecturaCategorias.OrderBy(x => x.Descripcion).ToList();
                }
                dgvListaCategorias.DataSource = null;
                dgvListaCategorias.DataSource = listaFiltrada;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        
        //Validaciones
        private bool ValidarFiltroMarca ()
        {
            if (string.IsNullOrEmpty(txtAgregarMarca.Text))
            {
                lblCamporequeridoMarca.Text = "*Campo Requerido*";
                return true;
            }
            return false;
        }
        private bool ValidarFiltroCategoria ()
        {
            if (string.IsNullOrEmpty(textAgregarCategoria.Text))
            {
                lblCamporequeridoCategoria.Text = "*Campo Requerido*";
                return true;
            }
            return false;
        }       

        //Elementos del Frm
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void VentanaCategorias_Load(object sender, EventArgs e)
        {
            //refresh Categorias
            cargarcategoria();
            cboCategoria.Items.Add("Orden alfabetico");
            cboCategoria.Items.Add("Nro ID asc.");
            cboCategoria.Items.Add("Nro ID des.");
            //refresh Marcas
            cargarmarca();
            cboMarcas.Items.Add("Orden alfabetico");
            cboMarcas.Items.Add("Nro ID asc.");
            cboMarcas.Items.Add("Nro ID des.");

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarFiltroCategoria())
            {
                lblCamporequeridoCategoria.Text = "";
                Categoria categoria = new Categoria();
                Agregar(categoria);
            }
        }

        private void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            if (!ValidarFiltroMarca())
            {
                lblCamporequeridoMarca.Text = "";
                Marca marca = new Marca();
                Agregar(marca);
            }
        }
        
        private void btnEditarCategoria_Click_1(object sender, EventArgs e)
        {
            
                Categoria categoria = (Categoria)dgvListaCategorias.CurrentRow.DataBoundItem;
                Modificar(categoria);
            
        }

        private void btnEditarMarca_Click(object sender, EventArgs e)
        {
                Marca marca = (Marca)dgvMarcas.CurrentRow.DataBoundItem;
                Modificar(marca);  
        }

        private void btnEliminarCategoria_Click(object sender, EventArgs e)
        {
            Categoria categoria = (Categoria)dgvListaCategorias.CurrentRow.DataBoundItem;

            Eliminar(categoria);
        }

        private void btnEliminarMarca_Click(object sender, EventArgs e)
        {
            Marca marca = (Marca)dgvMarcas.CurrentRow.DataBoundItem;

            Eliminar(marca);
        }

        private void cboCategoria_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            FiltrarCategoria();
        }

        private void cboMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarMarca();
        }
    }
}
