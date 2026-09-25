using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using System.Collections;
using UnityEngine.UI;


public class GestorUsuario : MonoBehaviour
{
    public TMP_InputField inputNombre;
    public TMP_InputField inputEdad;
    public TMP_InputField inputID;

    public Slider sliderEliminar;
    public TMP_Text textoCuentaAtras;

    public TMP_Text textoResultado;

    public List<Usuario> usuarios = new List<Usuario>();

    private int siguienteID = 1;


    public void AñadirUsuario()
    {
        string nombre = inputNombre.text;

        int edad;

        if (nombre == "")
        {
            textoResultado.text = "Escribe  un nombre.";
            return;
        }

        if (!int.TryParse(inputEdad.text, out edad))
        {
            textoResultado.text = "La edad tiene que ser numero";
            return;
        }

        Usuario nuevoUsuario = new Usuario(siguienteID, nombre, edad);

        usuarios.Add(nuevoUsuario);

        siguienteID++;

        textoResultado.text =
            "Usuario añadido:\n" +
            "ID: " + nuevoUsuario.id +
            "\nNombre: " + nuevoUsuario.nombre +
            "\nEdad: " + nuevoUsuario.edad;

        inputNombre.text = "";
        inputEdad.text = "";
    }
    public void MostrarTodos()
    {
        textoResultado.text = "";

        if (usuarios.Count == 0)
        {
            textoResultado.text = "No hay gente registrada";
            return;
        }

        foreach (Usuario usuario in usuarios)
        {
            textoResultado.text +=
                "ID: " + usuario.id +
                "  Nombre: " + usuario.nombre +
                "  Edad: " + usuario.edad +
                "\n";
        }
    }
    public void BuscarUsuario()
    {
        int id;

        if (!int.TryParse(inputID.text, out id))
        {
            textoResultado.text = "Introduce un ID válido.";
            return;
        }

        foreach (Usuario usuario in usuarios)
        {
            if (usuario.id == id)
            {
                textoResultado.text =
                    "Usuario encontrado:\n" +
                    "ID: " + usuario.id +
                    "\nNombre: " + usuario.nombre +
                    "\nEdad: " + usuario.edad;

                return;
            }
        }

        textoResultado.text = "No existe ningun usuario con ese ID.";
    }
    public void MostrarMayorEdad()
    {
        if (usuarios.Count == 0)
        {
            textoResultado.text = "No hay gentuza registrada";
            return;
        }

        int mayorEdad = usuarios[0].edad;

        foreach (Usuario usuario in usuarios)
        {
            if (usuario.edad > mayorEdad)
            {
                mayorEdad = usuario.edad;
            }
        }

        textoResultado.text = "Usuarios de mayor edad:\n";

        foreach (Usuario usuario in usuarios)
        {
            if (usuario.edad == mayorEdad)
            {
                textoResultado.text +=
                    usuario.nombre + " - " +
                    usuario.edad + " años\n";
            }
        }
    }

    public void EliminarUsuario()
    {
        int id;

        if (!int.TryParse(inputID.text, out id))
        {
            textoResultado.text = "Introduce un ID válido";
            return;
        }

        foreach (Usuario usuario in usuarios)
        {
            if (usuario.id == id)
            {
                StartCoroutine(EliminarConEspera(usuario));
                return;
            }
        }

        textoResultado.text = "No existe usuario con edse ID";
    }

    IEnumerator EliminarConEspera(Usuario usuario)
    {
        sliderEliminar.gameObject.SetActive(true);
        textoCuentaAtras.gameObject.SetActive(true);

        sliderEliminar.minValue = 0;
        sliderEliminar.maxValue = 3;

        float tiempo = 0;

        while (tiempo < 3)
        {
            tiempo += Time.deltaTime;

            sliderEliminar.value = tiempo;

            textoCuentaAtras.text =
                "Eliminando en " +
                Mathf.CeilToInt(3 - tiempo) +
                "...";

            yield return null;
        }

        usuarios.Remove(usuario);

        sliderEliminar.gameObject.SetActive(false);
        textoCuentaAtras.gameObject.SetActive(false);

        textoResultado.text = "Usuario eliminado";

        MostrarTodos();
    }
}