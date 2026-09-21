[System.Serializable]
public class Usuario
{
    public int id;
    public string nombre;
    public int edad;

    public Usuario(int id, string nombre, int edad)
    {
        this.id = id;
        this.nombre = nombre;
        this.edad = edad;
    }
}