namespace InmobiliariaLorenzo.Models
{
public interface IRepositorio<T>
{
    int Alta(T entidad);
    int Baja(int id);
    int Modificacion(T entidad);
    T ObtenerPorId(int id); // <- ESTE ES EL QUE DEBÉS IMPLEMENTAR
    IList<T> ObtenerTodos();
}

}